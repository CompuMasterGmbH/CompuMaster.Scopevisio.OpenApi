# Current Teamwork file attachments

Tracked by [SDK #223](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/223), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`TeamworkFileWriteRequest` deeply copies all four current TeamworkFileForm fields: original filename/data/collection strings, nullable createCollectionIfNotExist and future JSON. Its byte factory preserves original bytes as Base64 without filesystem/text conversion or filename/PDF/collection inference. Historical TeamworkFileForm, required data constructor, false default and APIs remain unchanged. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`ContactApi.AddContactFileWithResult`, `ProjectApi.AddProjectFileWithResult` and `TaskApi.AddTaskFileWithResult` each provide six synchronous, HTTP-info, asynchronous and native CancellationToken forms for their current entity `/file/new` routes. Bodies are optional: null sends no body. A supplied object requires original Base64 data; known supplied fields require declared JSON kinds. Missing data, known raw nulls and incompatible kinds fail before I/O. Typed null omits fields. Original filename/Base64/collection text, including whitespace and empty strings, remains unchanged without decoding, validation repair, trimming or lookup.

```csharp
var request = TeamworkFileWriteRequest.FromBytes(originalBytes, selectedFilename);
request.Collection = selectedCollectionName;
request.CreateCollectionIfNotExist = false;
var result = await projectApi.AddProjectFileWithResultAsyncWithHttpInfo(
    selectedProjectId, request, cancellationToken);
```

Collection is an original collection NAME, distinct from collection ID, numeric Scope organisation or opaque Teamwork tenant. Omitted createCollectionIfNotExist remains distinct from false and true; false is not inserted. The manufacturer describes creating a missing collection when requested and upload without collection when false. Actual assignment/missing-collection behavior remains server-authoritative; the client does not resolve collections, choose a default, create a collection in a separate request or promise a particular assignment outcome. Resulting files and any server-created collections require independently verified ownership and complete cleanup in actual tests.

Filename omission lets the server assume PDF; the client does not generate a name/extension or validate/convert arbitrary byte fixtures into PDFs. Original contact ID-or-legacy-number is escaped as one segment without trimming, numeric conversion, zero stripping or lookup; null/dot segments fail before HTTP. Project/task IDs retain signed Int64 invariant formatting without minimum rules or conversion to numbers, contacts, users, collections or tenants.

The server requires respective `Kontakte`, `Projekte` or `Aufgaben (Bearbeiten)` and independently evaluates Teamwork access/visibility. Scope read/write access alone does not establish Teamwork permission. HTTP200 successes have unspecified result schemas; HTTP400 may mean missing data or Teamwork access, and404 missing entity or authorization. Complete JToken data/future errors, exact raw JSON, status and response/content headers remain available without inferred file IDs, success or tenant permission. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact; the configured exception factory runs before parsing and preserves original failures.

The complete request and nested future data are deeply snapshotted before active HTTP. No account-context change, hidden lookup/write, retry or mutation replay is added. Cancellation after sending cannot establish file/collection state. These routes are Teamwork-backed attachments, distinct from regular journal/outgoing file attachment and incoming/outgoing invoice imports; their source implementation does not resolve the previously observed bridge401 or establish direct/first-party OAuth permission.

343 isolated fake-host cases cover all fields, optional/required bodies, strings/kinds/nulls/flags/bytes/future copies, original entity selectors/IDs, active snapshots, full results/errors/factories/native cancellation and legacy defaults/APIs. Actual owned Demo entity/file/collection/profile/Teamwork behavior and complete cleanup remain open, requiring a new exclusive reversible window with exact numeric Scope organisation/fresh-account proof plus distinct opaque Teamwork tenant binding and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
