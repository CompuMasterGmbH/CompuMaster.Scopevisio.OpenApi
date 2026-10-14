# Current task and subtask writes

Tracked by [SDK #191](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/191), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`TaskWriteRequest` exposes all23 current TaskForm fields through optional typed access:11 original strings,7 Int64 values,2 booleans,1 decimal number,1 copied custom-field array and1 Int32 permission value. `SubtaskWriteRequest` exposes all6 current SubtaskForm fields:3 Int64 values,2 strings and1 decimal number. Both retain complete deeply copied JSON with future fields. Historical models, enums, constructors, defaults and generated methods remain unchanged, including the historical Int64 TaskForm.PermissionType and title-case priority enum output.

`TaskApi.CreateTaskWithResult` sends POST `/task/new` with a required body and explicit topic string. `UpdateTaskWithResult` sends POST `/task/{id}` using the original signed Int64 task ID in invariant text and an optional body. `CreateSubtaskWithResult` sends POST `/subtask/new` with a required body and explicit Int64 parent taskId. Each has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Subtask creation does not implicitly create a task or translate a contact/project ID into a parent task. Supplied zero IDs and empty topic text are sent without guessing their server meaning; omission does not insert zero or an empty string.

Creation201 and update200 have no fixed successful response schemas. All forms retain complete JToken data, exact raw JSON, status and response/content headers without inventing a new task/subtask ID or success flag. Empty content produces null Data; explicit JSON null remains a token. Date-looking result strings remain strings and parsed fractions use bounded .NET decimal precision, while raw JSON stays exact. The configured exception factory runs before parsing and preserves original errors.

```csharp
var request = new TaskWriteRequest
{
    Topic = originalTopic,
    Priority = "normal",
    ContactId = selectedContactId,
    SetEstimatedHoursManually = true,
    EstimatedHours = originalEstimatedHours
};
var result = await taskApi.CreateTaskWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

Original priority/status/sphere strings retain spelling, capitalization and whitespace. The current priority contract uses lowercase `normal`, unlike the historical enum's `Normal`; its documented normal default is not inserted. The current contactSphere enum contains `Keditor`; the client does not repair it into another spelling. These strings remain server-validated. Tags are the original string, not a locally parsed or reconstructed list.

ContactId and ContactLegacyId may both be explicitly supplied. The manufacturer says that a found contactId takes precedence and the legacy selector is then ignored. The client sends both original values without choosing, resolving, adding an XOR rule or converting leading-zero legacy text into a number. ResponsibleContactId, project and outgoing-invoice references likewise remain original selections.

The manufacturer's [general update documentation](https://help.scopevisio.com/en/articles/467359-general-documentation) permits sparse updates: omitted fields retain their data, while null or empty text may clear a nullable field or be ignored for a mandatory field. Typed null setters omit fields; the raw indexer retains explicit update null, including topic/status, without promising they can be cleared. Null update body sends no body; empty update `{}` stays distinct. Known creation fields reject explicit null. No priority, status, employee flag, manual-hours mode, permission, ID or timestamp default is inserted.

StartDateTS, DueDateTS and ReminderDateTS retain original Int64 epoch milliseconds. The current date, month boundaries and now/reminder examples are not normalization rules. PermissionType uses current Int32 JSON width, leaving its semantic0 through2 range to the server. Permission profile/login strings remain original and are evaluated only when permissionType equals1. EmployeeDomain and SetEstimatedHoursManually preserve omission versus explicit false; estimated task hours are server-evaluated only when the caller explicitly enables manual mode. Subtask estimated hours remain independently supplied. The client performs no hours, date or unit calculation.

Typed hours use nullable decimal with bounded .NET precision. Complete raw numeric tokens can retain additional precision/magnitude without decimal conversion or rounding. Incompatible kinds, numeric strings, fractions for integer fields, width overflow and non-finite known hour values fail before I/O. CustomFields copies original entries and requires string customFieldName plus explicit mixed customFieldValue, following documented scalar examples despite the nominal object schema. Raw scalar/object/array values and the §§ multi-selection separator remain unchanged; explicit update custom-value null remains available. Configured custom names/types are server-validated.

Requests and nested future/custom JSON are deeply snapshotted before active HTTP. No account selection, authentication change, resource lookup, hidden parent/child writes or mutation replay is added. Cancellation after sending cannot establish whether the server applied a write.

The manufacturer requires `Aufgabe, Kontakte (Bearbeiten)` for task/subtask creation and `Aufgabe (Bearbeiten)` for task updates. Read access or Teamwork web visibility does not establish these rights.

637 isolated fake-host cases verify all29 fields/routes/all6 forms, required/optional bodies, sparse/null updates, original priority/sphere/selector strings, integer widths and original epochs, raw/finite hours, flags, deep snapshots/custom data, complete responses/raw/status/headers, errors/factories/native cancellation and historical APIs. Actual Demo task/contact/project/invoice/catalogue/custom-field/permission acceptance and complete owned cleanup remain pending in a new coordinated reversible fixture with exact numeric Scope organisation proof. Source merge, separately coordinated publication and exact downstream adoption remain distinct; this family is excluded from the selected CM.Dms2026.10.14 release dependency set, which retains published SDK2026.10.13.
