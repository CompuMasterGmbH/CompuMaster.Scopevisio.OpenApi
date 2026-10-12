# Compiled SDK compatibility

Issue #100 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. This check preserves the existing compiled public/protected API while the current OpenScope contract is implemented additively.

The immutable baseline is commit `a72dde455591cef65425b953bc6a6d56d6b03baf`, the source baseline for package `CompuMaster.Scopevisio.OpenApi` version `2026.10.07.0`. The check archives only its SDK project and README, without tests, credentials or local configuration. It builds that source separately and compares all three SDK targets: `netstandard2.0`, `net6.0` and `net48`.

## Reproducing the check

Use Python 3.10 or later, Git with the baseline object available, and .NET SDK 10 plus the .NET 8 and .NET 6 runtimes/reference packs. Windows additionally executes the .NET Framework 4.8 consumer. The manual-only `ApiCompatibility.yml` workflow installs these prerequisites on Windows and uploads the bounded report/logs without using secrets or running remote integration tests.

```powershell
python tools/verify_api_compatibility.py --work-dir .task-work-api-compat/fresh-owned-run
```

Choose a fresh absent or empty directory that belongs to this check. Existing output is retained and rejected rather than deleted or silently reused. Repository-local output must be below a `.task-work*` directory. The SDK source must be committed and clean; new evidence records that exact commit and SDK subtree. The check restores normal public build dependencies, installs the tool locally, builds isolated fixtures/consumers and uses only a fake `.invalid` HTTP handler.

## What is compared

Microsoft.DotNet.ApiCompat.Tool is pinned to `10.0.100`; its restored package SHA-256 must be `7263e0dc9896d7092c2742a314fb9dd25a670b86968894e2e27982ec8b07a3af`. The comparison checks existing public/protected signatures, base types, interfaces, constants, parameter names and attributes. Every framework/package reference is resolved from the project before comparison; unresolved-reference diagnostics invalidate the result.

Only `AsyncStateMachineAttribute` and `DebuggerStepThroughAttribute` are excluded. Compiler-generated state-machine identities and debugger stepping metadata change when generated/hand-maintained method bodies change and are not SDK consumer contracts. There are no API-member, serialization, obsolete-attribute or general compatibility-diagnostic suppressions.

An actual compiled regression showed that this APICompat version does not detect a change from optional default `7` to `8`. The additional PE metadata reader therefore compares existing public/protected parameter Optional/HasDefault flags and typed raw constant blobs, together with public/protected literal and enum constants. It reads assembly metadata without loading or executing SDK assemblies. The current SDK baseline contains 3,807 such parameter/constant records per target.

Durable compiled fixture mutations verify removed public and protected members, changed parameter names, changed defaults, removed optional flags, changed enum values and removed obsolete attributes. Each incompatible variant must fail the combined checks, while an additive member must pass. Checker outcomes are recorded separately so APICompat's optional-default limitation remains visible.

## Unchanged compiled consumers

A representative standalone consumer is compiled against the baseline once, then executed against both baseline and current SDKs. Only the SDK DLL is substituted; the consumer binary hash must remain unchanged. Separate Windows runs cover .NET 8 and .NET Framework 4.8. The consumer exercises shared caller configuration, facade/API access, existing synchronous and asynchronous typed contacts, original raw HTTP-info data, a historical null-data contact result, caller bearer authorization, existing token pre-cancellation, exception-factory identity and public record constructors. All HTTP is intercepted by the owned fake handler. It never logs in to a real account.

## Evidence and limits

`contracts/openscope/api-compatibility.json` records the reviewed local Windows evidence at candidate commit `ed5fec9d551631eccd6d8139191ade3cb7397468` and SDK subtree `cbce7555e4a951df2e1a301defe0ff378fcef218`. All three comparisons and both unchanged consumers passed. The report includes exact baseline archive, tool, checker source, metadata source/project, consumer source/project and built assembly hashes. DLL hashes describe that particular build; source paths, SDK/compiler versions and build inputs can change bytes in another run. New reports identify their own source pin.

This is bounded compiled compatibility evidence. It does not prove every possible source overload-resolution scenario, legacy parameter/result wire semantics, complete native cancellation coverage, server/profile/account behavior, or behavior of every historical consumer. It does not recover an unavailable historical generator recipe. Live acceptance, the serialized integration matrix, package publication and downstream adoption remain separate requirements in #17 and DMS #90.
