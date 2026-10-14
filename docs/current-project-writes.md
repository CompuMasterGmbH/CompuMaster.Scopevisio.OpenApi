# Current project creation and update

Tracked by [SDK #189](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/189), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`ProjectWriteRequest` exposes all36 current ProjectForm properties through optional typed access:22 original strings,8 Int64 values,2 Int32 values,1 boolean,1 decimal number and2 deeply copied arrays. Complete copied JSON retains future fields. Historical ProjectForm constructors, enums, decimal Workdays, Int64 PermissionType, non-nullable defaults and methods remain unchanged. The new Workdays and PermissionType use the current Int32 widths; semantic ranges remain server-authoritative. The string Type member explicitly hides JObject.Type, while a JToken reference retains the JSON token kind.

Category-dependent Type2, Phase and Status strings accept the configured catalogue values documented by the manufacturer instead of being restricted to historical examples. Other strings, including Category, Type, billing/time-recording status and unit, remain unchanged for server validation. Billable and TimeRecordable are strings, not booleans. OwnerUid, project-addressee ContactId, ContactPerson, initiator and dimension selections are explicit caller values without lookup or substitution. The server validates contact/employer relationships and may select omitted category or dimension-name defaults; the client does not choose them.

`ProjectApi.CreateProjectWithResult` sends POST `/project/new`, while `UpdateProjectWithResult` sends POST `/project/{id}` using the original signed Int64 project ID in invariant text. Each has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Both primary route contracts omit requestBody.required, so null sends no body. A supplied creation object requires original name string and Int64 contactId. A supplied empty creation object fails before I/O; empty/zero values explicitly chosen by the caller remain server decisions. Empty update `{}` remains distinct from null body, and no project name, number, prefix, owner, contact, date, cost centre, permission, hours or false/zero default is inserted.

The manufacturer's [general update documentation](https://help.scopevisio.com/en/articles/467359-general-documentation) permits omitted fields to retain existing data. Typed null setters omit fields; the raw indexer retains explicit update null. Whether null or empty text clears a particular field is server-authoritative. Known creation fields reject explicit null. The API does not reinterpret an omitted body as successful creation or invent a response.

```csharp
var request = new ProjectWriteRequest
{
    Name = selectedProjectName,
    ContactId = selectedProjectContactId,
    Type2 = selectedCategoryProjectType,
    EstimatedHoursMode = true,
    EstimatedHours = originalEstimatedHours
};
var result = await projectApi.CreateProjectWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

StartTs, EndTs and DecisionTs are original epoch milliseconds with Int64 width; date/month examples are not applied as normalization rules. HoursPerDay and dimension/contact IDs also retain Int64 width. EstimatedHoursMode preserves omission versus explicit false. EstimatedHours retains an optional decimal with bounded .NET precision; complete raw numeric tokens may be supplied without decimal conversion or rounding. The server evaluates estimated hours only when its explicit mode is true. Non-finite or incompatible known numbers are rejected before I/O, and no local estimate, rate or time-unit calculation is introduced.

CustomFields copies complete original entries and requires string customFieldName plus explicit customFieldValue. The manufacturer's nominal object schema also documents scalar examples, so mixed scalar/object/array values and the original §§ multi-selection separator remain unchanged; explicit update null remains available. The server validates configured types. ProjectEmployees copies complete original nested project-team entries and reuses the current five-field validation from #187. Creation nested entries require personal contactId; sparse update fields and explicit null remain server-authoritative. Employee/team-entry IDs are never inferred from personal contact IDs, and no member lookup, local array merge, guessed list replacement or additional write is performed.

The complete request is deeply snapshotted before active HTTP I/O, including both arrays and nested future fields. Creation201 explicitly declares ProjectNewResponse with optional status(Int32), projectId(Int64), location(string) and errors(ObjectNode); complete future response JSON remains available. Update200 has no fixed success schema. All methods retain JToken data, exact raw JSON, status and response/content headers without invented identifiers or success flags. Empty content gives null Data; explicit JSON null remains a token. Date-like strings stay strings and parsed fractions follow bounded .NET decimal precision while raw JSON is exact. The configured exception factory runs before parsing and preserves original errors. No account selection, authentication change, hidden lookup or mutation replay is added; cancellation after sending cannot establish the server outcome.

The server requires `Projekte (Bearbeiten)` and evaluates actual catalogue, owner, dimension, contact, time, custom-field and nested-member constraints. Isolated fixtures do not establish these permissions or semantics.

724 isolated fake-host cases cover all36 fields, both routes and all6 forms, body/required-field distinctions, scalar/array snapshots, sparse/null updates, original catalogue strings, integer widths, raw hours/non-finite values, mixed custom/nested members, explicit response schema/raw metadata, factories/errors, native cancellation and unchanged legacy APIs. Actual Demo project/contact/owner/catalogue/dimension/custom-field/permission acceptance and owned cleanup require a fresh coordinated reversible fixture with exact numeric Scope organisation proof. Source merge, publication and pinned downstream adoption remain distinct. This independent family is excluded from the selected CM.Dms2026.10.14 release, which retains published SDK2026.10.13.
