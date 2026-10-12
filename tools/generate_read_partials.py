"""Deterministic, fail-closed generation of explicitly reviewed Int64 JSON reads."""
import argparse
import hashlib
import json
from pathlib import Path
import re
import stat
from string import Template
from contract_coverage import extract, json_bytes, text_bytes

ROOT = Path(__file__).resolve().parents[1]
VERSION = 1
RECIPE = Path("contracts/openscope/generation/task-subtasks.json")
SDK = Path("src/CompuMaster.Scopevisio.OpenApi")
TOOL = Path("tools/generate_read_partials.py")
MANIFEST = Path("contracts/openscope/generation/task-subtasks.manifest.json")
KEYS = {"version", "contract", "contract_sha256", "template", "template_sha256",
        "path", "operation_sha256", "class", "method", "parameter", "import",
        "summary", "parameter_summary", "remarks"}


def sha(data):
    return hashlib.sha256(data).hexdigest()


def operation_hash(item):
    return sha(json.dumps(item, sort_keys=True, separators=(",", ":"), ensure_ascii=False).encode("utf-8"))


def relative_file(root, name):
    selected = Path(name)
    if selected.is_absolute() or ".." in selected.parts:
        raise ValueError("Input paths must stay within the repository.")
    path = (root / selected).resolve()
    if not path.is_relative_to(root.resolve()) or not path.is_file():
        raise ValueError("The selected repository input does not exist or escapes its root.")
    return path


def validate_operation(spec, item, recipe):
    operation = item.get("get")
    param = recipe["parameter"]
    expected = {"name": param, "in": "path", "required": True,
                "schema": {"type": "integer", "format": "int64"}}
    if set(item) != {"get"} or operation is None:
        raise ValueError("Only a path containing the single reviewed GET operation is supported.")
    if set(operation) - {"tags", "summary", "description", "operationId", "parameters", "responses", "security"}:
        raise ValueError("The operation contains an unsupported contract feature.")
    if operation.get("parameters") != [expected] or "requestBody" in operation:
        raise ValueError("Only one unconstrained required Int64 path parameter and no body/query are supported.")
    if re.findall(r"\{([^}]+)\}", recipe["path"]) != [param]:
        raise ValueError("The single path parameter must match the recipe.")
    responses = operation.get("responses", {})
    if "200" not in responses or any(set(response) != {"description"} for response in responses.values()):
        raise ValueError("Only documented responses without a schema/content/header contract are supported.")
    if operation.get("security", spec.get("security")) != [{"oauth": []}]:
        raise ValueError("The reviewed OAuth security contract is required.")


def plan(root=ROOT):
    root = root.resolve()
    recipe_bytes = relative_file(root, str(RECIPE)).read_bytes()
    recipe = json.loads(recipe_bytes.decode("utf-8-sig"))
    if set(recipe) != KEYS or recipe["version"] != VERSION:
        raise ValueError("The recipe keys/version do not match this bounded generator.")
    for name in ("class", "method", "parameter"):
        if not isinstance(recipe[name], str) or not re.fullmatch(r"[A-Za-z_][A-Za-z0-9_]*", recipe[name]) or recipe[name] in set("abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while async await".split()):
            raise ValueError("C# identifiers must be simple explicit names.")
    if not re.fullmatch(r"/[A-Za-z0-9_/{}/-]+", recipe["path"]):
        raise ValueError("The route is outside the bounded route syntax.")
    if recipe["import"] != "Api/" + recipe["class"] + "." + recipe["method"] + ".Generated.cs":
        raise ValueError("The explicit owned import must match the class and workflow.")
    if not recipe["class"].endswith("Api"):
        raise ValueError("An existing API class is required.")
    for name in ("summary", "parameter_summary", "remarks"):
        value = recipe[name]
        if not isinstance(value, str) or any(c in value for c in "\n\r<>&$"):
            raise ValueError("Documentation must be plain single-line text.")
    contract = relative_file(root, recipe["contract"]).read_bytes()
    template_bytes = relative_file(root, recipe["template"]).read_bytes()
    if sha(contract) != recipe["contract_sha256"] or sha(template_bytes) != recipe["template_sha256"]:
        raise ValueError("Contract/template bytes differ from the reviewed recipe.")
    spec = json.loads(contract.decode("utf-8-sig"))
    if spec.get("openapi") != "3.0.1":
        raise ValueError("The reviewed OpenAPI version is required.")
    item = spec["paths"][recipe["path"]]
    if operation_hash(item) != recipe["operation_sha256"]:
        raise ValueError("The selected path contract differs from the reviewed fingerprint.")
    validate_operation(spec, item, recipe)
    substitutions = {key: recipe[key] for key in ("class", "method", "parameter", "path", "summary", "parameter_summary", "remarks")}
    substitutions.update(version=str(VERSION), contract_sha=sha(contract), operation_sha=operation_hash(item))
    source = text_bytes(Template(template_bytes.decode("utf-8-sig")).substitute(substitutions))
    owned = root / SDK / recipe["import"]
    if owned.exists() and owned.read_bytes() != source:
        raise ValueError("The owned import differs; review it rather than overwrite it.")
    aliases = {recipe["method"] + suffix for suffix in ("", "WithHttpInfo", "Async", "AsyncWithHttpInfo")}
    existing_class = False
    for file in sorted((root / SDK / "Api").glob("*.cs")):
        if file.resolve() == owned.resolve():
            continue
        methods = extract(file.read_text(encoding="utf-8-sig"), file.name)
        for method in methods:
            if method["class"] == recipe["class"]:
                existing_class = True
                if method["method"] in aliases:
                    raise ValueError("The proposed public method collides with existing source: " + file.name)
    if not existing_class:
        raise ValueError("The selected API class does not exist.")
    generated = extract(source.decode("utf-8-sig"), recipe["import"])
    if len(generated) != 6 or {m["method"] for m in generated} != aliases:
        raise ValueError("The template does not emit exactly the six reviewed entry points.")
    manifest = {"generator_version": VERSION, "tool": str(TOOL).replace("\\", "/"),
                "tool_sha256": sha(relative_file(root, str(TOOL)).read_bytes()),
                "support_tool": "tools/contract_coverage.py",
                "support_tool_sha256": sha(relative_file(root, "tools/contract_coverage.py").read_bytes()),
                "recipe": str(RECIPE).replace("\\", "/"), "recipe_sha256": sha(recipe_bytes),
                "contract": recipe["contract"], "contract_sha256": sha(contract),
                "template": recipe["template"], "template_sha256": sha(template_bytes),
                "operation": "GET " + recipe["path"], "operation_sha256": operation_hash(item),
                "import": str(SDK / recipe["import"]).replace("\\", "/"),
                "generated_sha256": sha(source),
                "boundary": "Fresh disposable output only; reviewed source import is a separate action."}
    return recipe, source, json_bytes(manifest)


def validate_output(output, root=ROOT):
    if not output.is_absolute():
        raise ValueError("Output must be an explicit absolute disposable directory.")
    selected = output.resolve()
    root = root.resolve()
    if selected == root or selected in root.parents or selected == Path(selected.anchor):
        raise ValueError("A repository, parent or filesystem root cannot be output.")
    if selected.is_relative_to(root):
        first = selected.relative_to(root).parts[0]
        if not first.startswith(".task-work"):
            raise ValueError("Repository output must stay in an owned .task-work directory.")
    # Reject directory links/junctions rather than silently resolving them.
    for path in (output, *output.parents):
        if path.exists() and (path.is_symlink() or getattr(path.lstat(), "st_file_attributes", 0) & getattr(stat, "FILE_ATTRIBUTE_REPARSE_POINT", 0)):
            raise ValueError("Output through a directory link/junction is forbidden.")
    if selected.exists() and (not selected.is_dir() or any(selected.iterdir())):
        raise ValueError("Output must be absent or empty; no reuse or overwrite is allowed.")
    return selected


def generate(output, root=ROOT):
    recipe, source, manifest = plan(root)
    selected = validate_output(output, root)
    selected.mkdir(parents=True, exist_ok=True)
    source_name = Path(recipe["import"]).name
    # Exclusive creates also reject a competing writer; never replace existing files.
    with (selected / source_name).open("xb") as stream:
        stream.write(source)
    with (selected / "manifest.json").open("xb") as stream:
        stream.write(manifest)
    return source_name


def check_import(root=ROOT):
    recipe, source, manifest = plan(root)
    if relative_file(root, str(SDK / recipe["import"])).read_bytes() != source:
        raise ValueError("The reviewed source import is not reproducible.")
    if relative_file(root, str(MANIFEST)).read_bytes() != manifest:
        raise ValueError("The committed generation manifest is not reproducible.")


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    mode = parser.add_mutually_exclusive_group(required=True)
    mode.add_argument("--output", type=Path)
    mode.add_argument("--check-import", action="store_true")
    args = parser.parse_args()
    try:
        if args.check_import:
            check_import()
            print("Reviewed source import and generation manifest match.")
        else:
            print("Generated " + generate(args.output) + " and manifest.json in fresh disposable output.")
    except (ValueError, KeyError, OSError) as error:
        parser.error(str(error))


if __name__ == "__main__":
    main()
