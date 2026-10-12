#!/usr/bin/env python3
"""Checks the compiled SDK baseline using pinned APICompat and isolated consumers."""
import argparse
import hashlib
import json
import os
from pathlib import Path
import shutil
import subprocess
import sys
import zipfile

BASELINE = "a72dde455591cef65425b953bc6a6d56d6b03baf"
TOOL_VERSION = "10.0.100"
TOOL_HASH = "7263e0dc9896d7092c2742a314fb9dd25a670b86968894e2e27982ec8b07a3af"
PROJECT = "src/CompuMaster.Scopevisio.OpenApi/CompuMaster.Scopevisio.OpenApi.csproj"
ASSEMBLY = "CompuMaster.Scopevisio.OpenApi.dll"
FRAMEWORKS = ("netstandard2.0", "net6.0", "net48")
COMPILER_ATTRIBUTES = {
    "T:System.Runtime.CompilerServices.AsyncStateMachineAttribute",
    "T:System.Diagnostics.DebuggerStepThroughAttribute",
}


def sha(path):
    return hashlib.sha256(Path(path).read_bytes()).hexdigest()


def write(path, text):
    Path(path).write_text(text.replace("\r", "").rstrip() + "\n",
                          encoding="utf-8-sig", newline="\r\n")


def command(args, cwd, log=None, expected_success=True):
    env = dict(os.environ, DOTNET_CLI_UI_LANGUAGE="en-US")
    result = subprocess.run([str(arg) for arg in args], cwd=cwd, env=env,
                            encoding="utf-8", errors="strict",
                            stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
    if log:
        write(log, result.stdout)
    if expected_success and result.returncode:
        raise RuntimeError(f"Command failed ({result.returncode}); see {log or result.stdout}")
    return result


def references(project, framework, repo, work, label):
    result = command(["dotnet", "msbuild", project, "-nologo", "-verbosity:quiet",
                      "-target:ResolveReferences", f"-property:TargetFramework={framework}",
                      "-getItem:ReferencePath"], repo, work / f"references-{label}-{framework}.log")
    items = json.loads(result.stdout)["Items"]["ReferencePath"]
    paths = [Path(item["Identity"]).resolve() for item in items]
    if not paths or any(not path.is_file() for path in paths):
        raise RuntimeError("All framework and package references must resolve to existing files.")
    return sorted({str(path.parent) for path in paths})


def api_check(tool, left, right, lrefs, rrefs, exclusions, repo, log, allow_failure=False):
    result = command([tool, "-l", left, "-r", right,
                      "--lref", ",".join(lrefs), "--rref", ",".join(rrefs),
                      "--enable-rule-attributes-must-match",
                      "--enable-rule-cannot-change-parameter-name",
                      "--exclude-attributes-file", exclusions], repo, log,
                     expected_success=not allow_failure)
    diagnostic = result.stdout.lower()
    if any(text in diagnostic for text in
           ("could not resolve", "unable to resolve", "could not be resolved",
            "konnte nicht aufgelöst", "cannot resolve")):
        raise RuntimeError(f"An unresolved reference invalidates this comparison; see {log}")
    return result


def metadata_check(program, left, right, repo, log, allow_failure=False):
    return command(["dotnet", program, left, right], repo, log,
                   expected_success=not allow_failure)


def build_metadata_checker(repo, work):
    folder = work / "metadata-checker"
    folder.mkdir()
    for name in ("MetadataDefaults.csproj", "Program.cs"):
        shutil.copy2(repo / "tools/api-compat/metadata-defaults" / name, folder / name)
    command(["dotnet", "build", folder / "MetadataDefaults.csproj", "--verbosity", "quiet"],
            repo, work / "metadata-checker-build.log")
    return folder / "bin/Debug/net8.0/MetadataDefaults.dll"


def checker_regressions(tool, metadata, repo, work, exclusions):
    """Proves that actual compiled incompatible changes are detected."""
    root = work / "checker-regressions"
    root.mkdir()
    baseline = ('using System; public enum Mode { Standard = 1 } '
                'public class Contract { [Obsolete("Keep callers")] '
                'public virtual int Read(int value = 7) => value; '
                'protected virtual string Format(string text) => text; }')
    variants = {
        "baseline": baseline,
        "additive": baseline.replace("protected virtual", "public int Added() => 1; protected virtual"),
        "removed-public": baseline.replace('[Obsolete("Keep callers")] public virtual int Read(int value = 7) => value; ', ""),
        "removed-protected": baseline.replace('protected virtual string Format(string text) => text;', ""),
        "parameter-name": baseline.replace("int value = 7) => value", "int selected = 7) => selected"),
        "parameter-default": baseline.replace("value = 7", "value = 8"),
        "optional-flag": baseline.replace("int value = 7", "int value"),
        "enum-value": baseline.replace("Standard = 1", "Standard = 2"),
        "removed-attribute": baseline.replace('[Obsolete("Keep callers")] ', ""),
    }
    outputs = {}
    for name, code in variants.items():
        folder = root / name
        folder.mkdir()
        project = folder / "Fixture.csproj"
        write(project, '<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
              '<TargetFramework>net8.0</TargetFramework><AssemblyName>ApiCompatFixture</AssemblyName>'
              '</PropertyGroup></Project>')
        write(folder / "Contract.cs", code)
        command(["dotnet", "build", project, "--verbosity", "quiet"], repo,
                root / f"build-{name}.log")
        outputs[name] = folder / "bin/Debug/net8.0/ApiCompatFixture.dll"
    refs = references(root / "baseline/Fixture.csproj", "net8.0", repo, work, "checker")
    outcomes = {}
    for name, output in outputs.items():
        if name == "baseline":
            continue
        result = api_check(tool, outputs["baseline"], output, refs, refs, exclusions,
                           repo, root / f"comparison-{name}.log", allow_failure=True)
        metadata_result = metadata_check(metadata, outputs["baseline"], output, repo,
                                         root / f"metadata-{name}.log", allow_failure=True)
        expected_break = name != "additive"
        if (result.returncode != 0 or metadata_result.returncode != 0) != expected_break:
            raise RuntimeError(f"Checker regression {name} did not produce its expected result.")
        outcomes[name] = {"expected_break": expected_break, "api_compat_exit_code": result.returncode,
                          "metadata_exit_code": metadata_result.returncode}
    return outcomes


def standalone_consumers(repo, work, baseline_root, current_root):
    """Compiles with the baseline and executes unchanged binaries with both SDKs."""
    outcomes = {}
    source = repo / "tools/api-compat/consumer"
    for framework, sdk_framework in (("net8.0", "net6.0"), ("net48", "net48")):
        if framework == "net48" and os.name != "nt":
            outcomes[framework] = {"state": "deferred", "reason": ".NET Framework execution requires Windows."}
            continue
        folder = work / f"consumer-{framework}"
        shutil.copytree(source, folder)
        left = baseline_root / f"src/CompuMaster.Scopevisio.OpenApi/bin/Debug/{sdk_framework}/{ASSEMBLY}"
        project = folder / "BaselineConsumer.csproj"
        command(["dotnet", "build", project, "--verbosity", "quiet",
                 f"-property:TargetFramework={framework}", f"-property:BaselineAssembly={left}"],
                repo, work / f"consumer-build-{framework}.log")
        output = folder / f"bin/Debug/{framework}"
        executable = output / ("BaselineConsumer.exe" if framework == "net48" else "BaselineConsumer.dll")
        launch = [executable] if framework == "net48" else ["dotnet", executable]
        original_consumer_hash = sha(executable)
        command(launch, output, work / f"consumer-baseline-{framework}.log")
        right = current_root / f"src/CompuMaster.Scopevisio.OpenApi/bin/Debug/{sdk_framework}/{ASSEMBLY}"
        shutil.copy2(right, output / ASSEMBLY)
        if sha(executable) != original_consumer_hash:
            raise RuntimeError("The compiled baseline consumer changed before DLL substitution.")
        command(launch, output, work / f"consumer-current-{framework}.log")
        outcomes[framework] = {"state": "passed", "consumer_sha256": original_consumer_hash,
                               "baseline_sdk_sha256": sha(left), "current_sdk_sha256": sha(right)}
    return outcomes


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--work-dir", required=True, help="An absent or empty, explicitly owned output directory.")
    args = parser.parse_args()
    repo = Path(__file__).resolve().parents[1]
    work = Path(args.work_dir).resolve()
    if work == repo or work in repo.parents:
        raise RuntimeError("The output cannot be the repository or one of its parent directories.")
    if work.is_relative_to(repo) and not work.relative_to(repo).parts[0].startswith(".task-work"):
        raise RuntimeError("Repository-local output must be below an explicitly owned .task-work directory.")
    if work.exists() and any(work.iterdir()):
        raise RuntimeError("Output already contains resources; select a fresh owned directory.")
    work.mkdir(parents=True, exist_ok=True)
    exclusions = repo / "tools/api-compat/compiler-attributes.txt"
    if set(exclusions.read_text(encoding="utf-8-sig").splitlines()) != COMPILER_ATTRIBUTES:
        raise RuntimeError("Only the two documented compiler/debugger attributes may be excluded.")
    current = command(["git", "rev-parse", "HEAD"], repo).stdout.strip()
    sdk_tree = command(["git", "rev-parse", "HEAD:src/CompuMaster.Scopevisio.OpenApi"], repo).stdout.strip()
    dirty = command(["git", "status", "--porcelain", "--", "src/CompuMaster.Scopevisio.OpenApi"], repo).stdout
    if dirty.strip():
        raise RuntimeError("The SDK source must be committed before compatibility evidence is collected.")
    tool_dir = work / "tools"
    command(["dotnet", "tool", "install", "Microsoft.DotNet.ApiCompat.Tool",
             "--version", TOOL_VERSION, "--tool-path", tool_dir], repo, work / "tool-install.log")
    package = tool_dir / f".store/microsoft.dotnet.apicompat.tool/{TOOL_VERSION}/microsoft.dotnet.apicompat.tool/{TOOL_VERSION}/microsoft.dotnet.apicompat.tool.{TOOL_VERSION}.nupkg"
    if sha(package) != TOOL_HASH:
        raise RuntimeError("The restored pinned APICompat package hash differs from the reviewed artifact.")
    tool = tool_dir / ("apicompat.exe" if os.name == "nt" else "apicompat")
    archive = work / "baseline-sdk.zip"
    command(["git", "archive", "--format=zip", f"--output={archive}", BASELINE,
             "src/CompuMaster.Scopevisio.OpenApi", "README.md"], repo, work / "archive.log")
    baseline_root = work / "baseline"
    baseline_root.mkdir()
    with zipfile.ZipFile(archive) as bundle:
        for entry in bundle.infolist():
            target = (baseline_root / entry.filename).resolve()
            if not target.is_relative_to(baseline_root):
                raise RuntimeError("Baseline archive contains an unsafe path.")
        bundle.extractall(baseline_root)
    baseline_project = baseline_root / PROJECT
    command(["dotnet", "build", baseline_project, "--verbosity", "quiet",
             "-property:NoWarn=0618", "-property:CopyLocalLockFileAssemblies=true"],
            repo, work / "baseline-build.log")
    command(["dotnet", "build", repo / PROJECT, "--verbosity", "quiet",
             "-property:NoWarn=0618"], repo, work / "current-build.log")
    metadata = build_metadata_checker(repo, work)
    comparisons = {}
    for framework in FRAMEWORKS:
        left = baseline_root / f"src/CompuMaster.Scopevisio.OpenApi/bin/Debug/{framework}/{ASSEMBLY}"
        right = repo / f"src/CompuMaster.Scopevisio.OpenApi/bin/Debug/{framework}/{ASSEMBLY}"
        lrefs = references(baseline_project, framework, repo, work, "baseline")
        rrefs = references(repo / PROJECT, framework, repo, work, "current")
        api_check(tool, left, right, lrefs, rrefs, exclusions, repo, work / f"comparison-{framework}.log")
        metadata_check(metadata, left, right, repo, work / f"metadata-{framework}.log")
        comparisons[framework] = {"state": "passed", "parameter_defaults_constants": "passed",
                                  "baseline_sha256": sha(left), "current_sha256": sha(right)}
        print(f"Compiled API compatibility passed: {framework}", flush=True)
    regressions = checker_regressions(tool, metadata, repo, work, exclusions)
    consumers = standalone_consumers(repo, work, baseline_root, repo)
    report = {"baseline_commit": BASELINE, "current_commit": current, "sdk_source_tree": sdk_tree,
              "baseline_archive_sha256": sha(archive), "tool_version": TOOL_VERSION,
              "tool_package_sha256": TOOL_HASH, "checker_source_sha256": sha(__file__),
              "metadata_source_sha256": sha(repo / "tools/api-compat/metadata-defaults/Program.cs"),
              "metadata_project_sha256": sha(repo / "tools/api-compat/metadata-defaults/MetadataDefaults.csproj"),
              "consumer_source_sha256": sha(repo / "tools/api-compat/consumer/Program.cs"),
              "consumer_project_sha256": sha(repo / "tools/api-compat/consumer/BaselineConsumer.csproj"),
              "exclusions_sha256": sha(exclusions), "dotnet_sdk": command(["dotnet", "--version"], repo).stdout.strip(),
              "excluded_compiler_attributes": sorted(COMPILER_ATTRIBUTES),
              "comparisons": comparisons, "checker_regressions": regressions, "standalone_consumers": consumers,
              "limitations": ["Signature checks do not establish legacy wire contracts or server behavior.",
                              "Only compiler state-machine identity and debugger stepping metadata are excluded.",
                              "Live/profile/account acceptance, package publication and downstream adoption remain separate."]}
    write(work / "report.json", json.dumps(report, indent=2, ensure_ascii=True))
    print(f"Compatibility evidence: {work / 'report.json'}", flush=True)


if __name__ == "__main__":
    try:
        main()
    except (OSError, RuntimeError, subprocess.SubprocessError, ValueError, KeyError) as error:
        print(f"Compatibility check failed: {error}", file=sys.stderr)
        sys.exit(1)
