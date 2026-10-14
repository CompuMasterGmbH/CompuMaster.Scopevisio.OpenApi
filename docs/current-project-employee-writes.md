# Current project-team-entry writes

Tracked by [SDK #187](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/187), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`ProjectEmployeeWriteRequest` exposes all five current ProjectEmployeeForm properties through optional typed access: ContactId (Int64), ContactSphere and Role (strings), HourlyRate and DailyRate (decimal). Complete copied JSON retains unknown future fields. Historical ProjectEmployeeForm constructors, enums, double values, defaults and methods remain unchanged.

Role is an original configurable catalogue string. The manufacturer explicitly tells callers to consult their Scopevisio client for allowed roles; the new request accepts those values without restricting them to the historical generated enum. ContactSphere is sent unchanged for server validation. No role, contact, rate, currency or billing calculation is selected implicitly.

`ProjectApi.CreateProjectEmployeeWithResult` sends POST `/project/{projectId}/projectemployee/new`. `UpdateProjectEmployeeWithResult` sends POST `/project/{projectId}/projectemployee/{employeeId}`. Each workflow has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Original signed Int64 path identifiers use invariant text without lookup or ID conversion. The update preserves the existing projectId, employeeId argument order. Its employeeId identifies an existing project-team entry, explicitly not a contact; ContactId in the request identifies the personal contact. An arbitrary personal contact ID is never substituted as the update entry ID.

Both request bodies are required. Creation requires an explicitly supplied Int64 contactId, including a caller-selected zero or signed value whose server meaning remains authoritative. Omission is not silently converted to zero. Updates accept a supplied empty object or sparse fields without requiring the contactId again. This follows the manufacturer's [general update documentation](https://help.scopevisio.com/en/articles/467359-general-documentation): omitted fields retain their existing values, while acceptance of explicit null or empty strings depends on the field. Typed null setters omit fields; the raw indexer retains explicit update null, while known creation fields reject null. The client does not assume a mandatory existing contact relationship can be cleared.

```csharp
var request = new ProjectEmployeeWriteRequest
{
    ContactId = selectedPersonalContactId,
    Role = selectedProjectRole,
    HourlyRate = originalHourlyRate
};
var result = await projectApi.CreateProjectEmployeeWithResultAsyncWithHttpInfo(
    selectedProjectId, request, cancellationToken);
```

Typed rates use bounded .NET decimal precision. Raw JSON numeric tokens supplied through the indexer can retain additional precision and magnitude without conversion to decimal, rate calculation or rounding. Known rates require finite JSON numbers and reject numeric strings, incompatible kinds, NaN and infinities before I/O. No zero rate is inserted. ContactId checks Int64 JSON width without string/fraction coercion.

Creation HTTP201 and update HTTP200 have no fixed success schema. Complete JToken results, exact raw JSON, status and response/content headers remain available, with no invented success flag or project-team-entry ID. Empty content gives null Data; explicit JSON null stays a token. Date-looking strings remain strings and parsed fractions use bounded decimal precision while raw response JSON remains exact. The configured exception factory runs before parsing and preserves original failures. The complete request is deeply snapshotted before HTTP; no account selection, lookup, authentication change, implicit chained write or mutation replay is added. Cancellation after sending cannot establish whether the server applied the write.

The server requires `Kontakte, Projekte (Bearbeiten)` and validates personal-contact, project, catalogue and rate semantics. Successful project reads or Teamwork web visibility do not establish write permission.

290 isolated fake-host fixtures cover both routes/all forms, all five fields, required bodies and explicit creation contact, sparse/null updates, open role strings, exact decimals/raw numbers/non-finite rejection, invariant separate IDs, snapshots/future JSON, complete results/headers/errors/factories, legacy APIs and pre/active native cancellation. Actual Demo project/contact/catalogue/rate/permission acceptance and verified cleanup remain pending in a fresh coordinated, explicitly owned and reversible fixture with exact numeric Scope organisation proof. Source merge, separately coordinated publication and pinned downstream adoption remain distinct; this family is not an automatic prerequisite for a bundled DMS release.
