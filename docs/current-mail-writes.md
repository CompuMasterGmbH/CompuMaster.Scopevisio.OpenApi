# Current mail assignment and EML import

Tracked by [SDK #197](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/197), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`MailAssignmentWriteRequest` holds one explicitly supplied nullable Int64 MailId plus complete copied future JSON. All assignment bodies require an original JSON Int64 mailId; absence is not converted to zero. The mail ID is distinct from the separately selected contact/project/task target and is not inferred, searched or selected by default. Historical AssignMailForm constructors and default values remain unchanged.

`ContactApi.AssignMailToContactWithResult`, `ProjectApi.AssignMailToProjectWithResult` and `TaskApi.AssignMailToTaskWithResult` send POST `/contact/{contactIdOrLegacyNumber}/assignMail`, `/project/{projectId}/assignMail` and `/task/{taskId}/assignMail`. Each has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. The contact string is escaped as one segment without trimming, numeric conversion, leading-zero removal or lookup; null/dot segments fail before I/O. Project/task IDs preserve original signed Int64 invariant text. Explicit zero or signed IDs remain server decisions, not client defaults.

`IncomingMailWriteRequest` exposes original Encoding/EML strings and nullable SkipStandardContactAssignment, including the current flag absent from the historical IncomingMailForm. `ProjectApi.ImportIncomingMailWithResult` retains the existing incoming-mail API owner even though the current contract tag is Additional. Its six forms send POST `/incomingmail/new` with an optional body: null sends no body, while a supplied object requires an EML string. Known explicit nulls or incompatible kinds fail before sending. Typed null setters omit fields, preserving future JSON and historical models/methods.

```csharp
var request = new IncomingMailWriteRequest
{
    Encoding = "base64",
    Eml = originalBase64Eml,
    SkipStandardContactAssignment = true
};
var result = await projectApi.ImportIncomingMailWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

The server defaults omitted encoding to UTF-8 and omitted SkipStandardContactAssignment to false. It may then associate the imported mail with matching sender/recipient contacts, excluding the current user or configured valid senders. True explicitly asks the server to skip standard matching; false and omission remain distinct in the request. The client does not insert either default, interpret EML headers, normalize newlines, decode/re-encode Base64, deliver mail, search contacts or perform additional assignments. Actual import and association effects remain server-authoritative and need separate acceptance.

All four HTTP200 successes have no fixed response schema. Complete JToken data, exact raw JSON, status and response/content headers remain available without invented imported IDs, assignment counts or success flags. Empty content produces null Data; explicit JSON null stays a token. Date-looking result strings remain strings and parsed fractions use bounded .NET decimal precision while raw JSON remains exact. Original factories run before parsing and preserve their errors. Complete requests/future nested JSON are deeply snapshotted before active HTTP; no account selection, hidden follow-up or mutation replay is added. Cancellation after sending cannot establish whether import or assignment occurred.

Profiles are `Kontakte, Mailbot, Mailagent (Bearbeiten)`, `Projekte, Mailbot, Mailagent (Bearbeiten)` or `Aufgaben,Mailbot,Mailagent (Bearbeiten)` for assignments, and `Mailbot, Mailagent (Bearbeiten)` for import. Existing mail/target existence and permissions are verified by the server; read access or Teamwork web visibility does not establish these capabilities.

315 isolated fake-host cases cover all four current fields, four routes/all six forms, required/optional bodies, original IDs/strings and independent target/mail identities, encoding and omission/false/true flags, integer widths, deep snapshots, complete results/raw metadata, errors/factories/native cancellation and preserved legacy owners/APIs. Actual Demo import/link/profile behavior and complete owned cleanup need a new coordinated reversible fixture with explicit numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this family is outside delivered DMS2026.10.14's unchanged SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
