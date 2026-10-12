# Reproducible bounded read generation

[SDK #112](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/112) defines one reviewed recipe for GET /task/{taskId}/subtasks. The local Python 3.10+ standard-library generator has version 1. The committed manifest pins its exact SHA256 and source-parser dependency SHA256, recipe, template, repository-format manufacturer contract, complete selected path fingerprint and emitted source. Output has UTF-8 BOM/CRLF and contains no timestamps, host paths or machine-specific runtime versions.

This establishes a reproducible additive generation boundary. The original OpenAPI Generator 4.3.1 recipe remains incomplete; this tool does not reconstruct it or authorize replacing the SDK. New generation families require separately reviewed recipes, templates, contracts and tests. The current mode accepts only an existing API class, one unconstrained required Int64 path parameter, OAuth, a single GET and documented responses without a fixed content/schema/header contract. Query parameters, request bodies, extra operations, schema constraints, fixed response shapes and unknown operation features fail rather than receive guessed behavior.

## Reproduce and verify

Run from the repository root. Select two previously absent or empty disposable directories; use different names on later runs.

```text
python -m unittest discover -s tools -p test_generate_read_partials.py -v
python tools/generate_read_partials.py --output D:\scratch\scopevisio-read-a
python tools/generate_read_partials.py --output D:\scratch\scopevisio-read-b
python tools/generate_read_partials.py --check-import
```

Compare both emitted source files and both manifest.json files byte-for-byte. Output inside the repository is allowed only under an owned .task-work directory. Repository/source/test directories, repository parents, filesystem roots, relative paths, directory links/junctions and nonempty output directories are rejected. Files use exclusive creation: existing files are never overwritten or deleted. Interrupted output remains available for diagnosis; use a fresh directory afterwards. Generation never writes runtime source, tests, credentials or configuration. The selected source and manifest must be imported separately after review. The offline workflow verifies the exact committed import without writing it.

The bounded C# source parser rejects a method-name collision anywhere in the selected existing API class. Only the explicit owned generated import is exempt, and its bytes must already match the recipe if present. Contract/template/fingerprint drift, unsupported recipe syntax, unknown keys and missing classes fail before creating output. Parser limits remain those of the checked SDK style; compilation, compatibility comparisons and complete diff review supplement this check.

## Complete subtask results

TaskApi.GetSubtaskDetails and its WithHttpInfo and native asynchronous forms return the complete JToken for an explicitly selected task. Original signed Int64 values use invariant text; the contract specifies no minimum. The bodyless request adds no query, pagination, fields or other defaults. The Aufgabe (Anzeigen) profile is required. The manufacturer declares no success schema; arrays, objects, scalar values, future fields and explicit null remain available. An empty response body returns null.

WithHttpInfo retains status, combined response/content headers and exact raw JSON. Date strings remain strings. Parsed fractions use .NET decimal precision and can round, underflow or exceed its range; raw JSON remains exact. The existing exception factory runs before parsing, and HTTP 404 remains ambiguous between a missing task and missing authorization. Native cancellation reaches admission and active HTTP I/O without speculative replay.

Historical GetSubtasks methods retain their void/null-data behavior, configuration identity, authorization and account defaults. Tests use fake .invalid handlers only. Real profile/subtask/404 acceptance, the full legacy wire and cancellation audit, publication and downstream adoption remain separate from reproducibility and isolated test evidence.
