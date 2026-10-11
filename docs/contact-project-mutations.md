# Explicit Contact and Project mutations

Four additive workflows follow the pinned manufacturer routes. Existing APIs and defaults remain unchanged.

| Method | Route | Request/result |
|---|---|---|
| ModifyContactFields | PATCH /contact/modify/{contactId} | ContactModification / complete JSON result |
| CreateProjectResource | POST /projectresource/new | ProjectResourceCreationRequest / ProjectResourceCreationResponse |
| RemoveTimeEntry | DELETE /timeentry/{id} | ID or legacy number / empty 204 result |
| RemoveTimeEntryByLegacyNumber | DELETE /timeentry/legacynumber/{legacynumber} | Explicit legacy number / empty 204 result |

Each workflow offers synchronous, asynchronous and native-cancellation entry points. WithHttpInfo retains status, headers and unchanged raw response. Required identifiers/body/project ID are checked before HTTP I/O.

## Caller intent and validation

Contact BirthDate is a valid dd.MM.yyyy string. Omission retains it; an empty string explicitly clears it. Setting the property to null removes it. Raw JSON null, non-string values and invalid dates are rejected before transport. Unknown modification fields remain available through the JSON indexer; the server decides whether it supports them.

Project creation requires an Int64 JSON projectId. Other IDs and allocation counts are checked as Int64 integer tokens without coercion; roles, positions and statuses remain catalog strings. StandardFee must be a finite non-negative JSON number, including correct rejection of a negative value too small for decimal conversion. MonthlyAllocations is a copied array of non-negative integer days; the server validates project duration and calendar capacity. Optional null setters omit fields; low-level explicit-null optional values remain caller-controlled.

Recreate is an explicit boolean choice. Unset retains the server default of false; true can replace all related project revenue rows. The SDK never turns it on automatically. A successful HTTP201 can still contain non-critical Errors and Hints, including catalog warnings and revenue conflicts. The caller receives them intact and decides how to proceed.

The SDK adds no mutation retries. Cancellation after sending a request does not prove that the server did not apply it; callers must resolve the server outcome before retrying ambiguous operations.

## Verification boundary

Isolated fixtures use a reserved .invalid host and no credentials. They cover all four methods/paths, escaped identities, unchanged bodies, precise fee wire text, required/raw typed input validation, omission/clear/recreate intent, retained HTTP201 warnings, HTTP204 emptiness, server/custom errors and active/pre-cancellation with one request and no SDK retry.

No real server resources were created, modified or removed. Coordinated live acceptance with test-owned resources remains in SDK #30. Complete API contract/live coverage remains in #17. Project resource/revenue read workflows are documented separately in [typed project reads](project-resources-revenues.md).
