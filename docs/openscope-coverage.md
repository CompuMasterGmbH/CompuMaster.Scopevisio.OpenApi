# OpenScope coverage refresh

Tracked by [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), [groundwork #18](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/18) and [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90). Package baseline: **CompuMaster.Scopevisio.OpenApi 2026.10.07.0**, commit `a72dde455591cef65425b953bc6a6d56d6b03baf`.

## Pinned evidence

[The manufacturer contract](https://appload.scopevisio.com/rest/openapi.json) was retrieved anonymously on 11 October 2026. [provenance.json](../contracts/openscope/provenance.json) records the raw-response hash, the repository-format hash, and all six differences from the 10 October research snapshot. These differences are example timestamp values in five schemas; paths, operations and schema shapes are unchanged. Retain the examples in the snapshot; do not silently strip vendor fields to make hashes match. An anonymous 12 October recheck found only six timestamp-example changes at the same pointers; route and schema shapes remain current. Its retrieval evidence is recorded in provenance.json; the immutable snapshot is retained.

The checked contract has OpenAPI 3.0.1, API version 1.0.0, 294 paths, 323 HTTP operations and 156 schemas. The API version alone does not identify an immutable contract. Use the repository snapshot and SHA256 together. The JSON snapshot is reformatted to UTF-8 BOM/CRLF; its hash therefore differs from the raw response. The offline audit verifies the committed hash before extraction.

## Reproduce the inventory

From the repository root, with Python 3.10 or later and no third-party modules:

```text
python -m unittest discover -s tools -p test_contract_coverage.py -v
python tools/contract_coverage.py --check
dotnet test src/CompuMaster.Scopevisio.OpenApi.AsyncTests/CompuMaster.Scopevisio.OpenApi.AsyncTests.csproj -f net8.0
```

To intentionally refresh the report after reviewed source changes:

```text
python tools/contract_coverage.py
git diff -- contracts/openscope/coverage.json
```

These commands do not retrieve the contract, authenticate, read local test configuration or access a DMS server. Python checks are fully offline; .NET restore may access NuGet. The dedicated Contract Coverage workflow runs only these isolated checks. The existing Build and Test workflow includes real Authorization requests; obtaining green inventory checks does not authorize that workflow or local integration tests. The groundwork PR was merged with user-authorized `[skip ci]` after essential local verification. Subsequent batches may use the same explicit authorization; report local checks accurately and do not claim skipped CI as successful. The isolated offline workflow remains available. Real-server workflows still require coordinated exclusive access.

[coverage.json](../contracts/openscope/coverage.json) contains every current operation, inherited and operation parameters, request/response contracts and security references into the pinned snapshot. Each row has an explicit status, reason and implementation phase. All 323 operations initially remain **deferred** for complete contract/live verification. Public source routes are evidence of exposure, not a supported-status assertion. Nothing has been marked unavailable based merely on documentation absence.

| Observation | Count |
|---|---:|
| Existing distinct method/normalized-path routes | 295 |
| Current routes matched | 290 |
| Current routes missing | 33 |
| Legacy routes absent from current documentation | 5 |
| Routes in classes marked unstable/incomplete | 265 |
| Direct HTTP method implementations, including cancellation partials | 598 |

The source inventory includes all API files, public/internal class and method access, result type, parameters, direct response deserialization, native cancellation parameters, facade properties, unstable annotations and WithHttpInfo wrapper delegates. It includes handwritten changes embedded in generated files, notably ContactApi's typed results, as well as `AuthorizationApi.Cancellation.cs` and `AdditionalApi.Cancellation.cs`. It rejects unparsed HTTP calls and calls appearing outside the known API/transport boundary. Fixture tests cover visibility, comments/string braces, placeholder normalization, delegates, legacy routes and ambiguous input.

This is a bounded source parser for the checked C# style, not a C# compiler or a behavioral proof. Wrapper references are not a runtime call graph. A typed result may still deserialize incorrectly. Run compiled standalone-consumer checks and protocol fixtures before promoting a row to supported.

Current operation IDs are unique in this snapshot, but repeated base names with suffixes and inconsistent terminology make them unsuitable as the sole SDK naming authority. Identify an operation by **HTTP method + normalized path** and review SDK names explicitly. Preserve five legacy routes: GET contact comments, incoming invoice by ID/number, licences, project comments and task comments. Missing documentation does not authorize API deletion.

## Generation boundary and review process

The repository records OpenAPI Generator **4.3.1** in `.openapi-generator/VERSION`. It does not retain a complete immutable 2020 input/configuration/template recipe. The ignore file contains examples only and does not protect custom code. Full historical regeneration is therefore not presently reproducible. This PR establishes reproducible *inventory* generation, not a claim that replacing the SDK is safe.

For every future bounded generation batch:

1. Pin the generator version/artifact hash, input-contract hash, configuration, templates and naming overrides in that batch. Generate twice in a disposable, empty output directory and compare results. Never generate into `src`, tests or repository root.
2. Review method/path pairs, colliding SDK names, required/optional parameters, request media types, request null/omission semantics and every documented response/status. Keep the vendor snapshot unchanged; commit any reviewed generation overlay separately.
3. Import only reviewed additive models/operations for the selected group. Keep handwritten methods in separately named partial files, such as `*.Contracts.cs` or `*.Cancellation.cs`. Existing generated files already contain manual edits; protect them until a separate reviewed extraction preserves their public signatures and behavior.
4. Preserve `OpenScopeApiClient.cs`, `OpenScopeApiClient.Async.cs`, `Client/**`, cancellation partials, `Records<T>`, `RecordCollection<T>`, `RecordItem`, `Contact` and existing typed ContactApi behavior. Treat existing public/protected signatures, enum values, serializers, exception hooks, caller-owned HttpClient, OAuth/refresh gate and account defaults as compatibility constraints. This list complements full diff review; it is not a sufficient ignore-file recipe.
5. Add synchronous and asynchronous entry points without replacing existing APIs. Use the established transport and serializer; forward cancellation to active HTTP I/O. Preserve existing no-token overloads/defaults and obsolete wrappers. Do not remove unstable annotations until the relevant behavior is verified.
6. Verify isolated wire/serialization fixtures and standalone source/binary consumption on applicable frameworks before a coordinated live test window. Compare emitted public signatures against the baseline assembly. Record implemented, isolated-tested, live-tested, merged and published states separately.
7. Review the complete diff and merge only the bounded SDK PR. After successful post-merge checks and separately authorized publication, record the exact released package and immutable source commit in DMS #90 before Teamwork/DMS consumption. This groundwork selects no new runtime package version.

## Ordered implementation batches

| Priority | Scope | Required evidence |
|---|---|---|
| 1 | Contract and transport foundation | Pinned snapshot/coverage; protect custom code; preserve OAuth, refresh, account selection, exceptions and cancellation with isolated fixtures and compiled consumers. Native authorization acceptance remains in SDK #15. |
| 2 | Contacts, projects and tasks | Verify existing typed Contact responses first. Add reviewed typed results alongside existing void/Object methods. Cover changed POST comment searches, task/project searches and resource operations. |
| 3 | Accounting and invoices | Review 42 missing Accounting operations and invoice gaps. Test decimal precision, required/default fields, UTC epoch values and per-account permission failures. |
| 4 | Data sources and remaining groups | Review 52 missing Data sources operations, reports, campaigns and material management. Record unsupported backend/account capabilities explicitly. |

Do not bundle these into a full regeneration. Select one operation family per implementation PR, link it to #17 and #90, and maintain its row decisions in the inventory. No current operation is silently omitted from the backlog.

The [general manufacturer documentation](https://help.scopevisio.com/en/articles/467359-general-documentation) distinguishes omitted update fields from explicit null/empty fields and describes UTC millisecond timestamps for custom dates. The [search documentation](https://help.scopevisio.com/en/articles/467360-search-scope-documentation) describes zero-based pages, default page size 100, maximum 1000, selected fields and formatting. Treat these as fixture requirements; do not change existing serializer defaults globally to satisfy one new endpoint. OAuth client registration requires manufacturer-issued client credentials and, for authorization-code flows, a registered redirect URI. Access to Teamwork is not evidence of direct CenterDevice authorization.

The initial issue's manufacturer-SDK research remains negative evidence only. This check confirms the current manufacturer contract and documentation as authority; it does not establish absence of private/partner SDKs. Third-party Python/PHP clients and unrelated products named OpenScope must not be used as replacement contracts.

## Verification limits

No shared server resources were created or removed. No current exclusive window, client registration or backend permissions have been established for this work. Real-server verification remains pending in the appropriate implementation issues. Repository-scoped GitHub concurrency groups do not lock other SDK/DMS repositories or local sessions. Coordinate setup, execution and cleanup across all participants before any server-mutating tests.
