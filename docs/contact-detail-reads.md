# Complete contact detail reads

Issue #102 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Six additive ContactApi workflows expose complete JSON results for historical generated reads that return void/Object with null Data. Existing methods, signatures, default results, caller configuration and account/token behavior remain unchanged.

| Method | Current GET route | Optional query |
| --- | --- | --- |
| GetContactDetails | /contact/{id} | fields, formatValues |
| GetContactDetailsByKeyIdentifier | /contact/{keyIdentifier}/{id} | fields |
| GetContactEmployeeDetails | /contact/{id}/employees | fields |
| GetContactEmployerDetails | /contact/{id}/employers | fields |
| GetContactPropertiesDetails | /contact/{id}/properties | fields |
| GetContactPropertyDetails | /contact/contactProperty/{propertyId} | fields |

Each workflow has synchronous, HTTP-info, asynchronous and native cancellation forms. The native overload adds CancellationToken after the explicitly supplied optional query arguments. Int64 selectors retain their complete signed range and use invariant text; the snapshot imposes no minimum. String key/identifier selectors are explicit, escaped as separate path segments and never trimmed, parsed as numbers or replaced by defaults. Null/blank/dot-segment string selections fail before HTTP. The current server documents ID and LEGACYNUMBER; other original strings are left to server validation rather than interpreted locally.

A null fields value omits field selection; an empty or caller-selected string is retained unchanged. GetContactDetails additionally accepts nullable formatValues: null omits the parameter and retains the documented server default true, false explicitly requests unformatted values, and true explicitly requests formatted values. No default is inserted by the client and other routes receive no speculative formatting or paging parameters.

The manufacturer supplies no fixed success schemas for these six routes. Results therefore remain complete JToken values with future fields, nested data, nulls and collection/object shapes preserved. HTTP-info forms retain original raw JSON, status and response/content headers. Date-like strings are not converted. Fractional JSON is parsed using .NET decimal precision, which can round or reject values beyond its range; the raw response remains exact. No assumed single-contact envelope or relationship model is fabricated.

The first five operations require the Kontakte (Anzeigen) profile; selected-property reads document Kontakte bearbeiten (Anzeigen). Contact/key/employee/employer 400 responses can represent unavailable data or missing read permission. The key-selection prose additionally describes 404 when a legacy number is ambiguous, while its response table lists 400. Contact-properties reads document 403 for missing read rights. The client preserves original diagnostics and does not infer absence, ownership or authorization from those responses. SearchContacts and the already current SearchContactComments remain separate search workflows; no historical comments GET is substituted for the current POST search.

The configured exception factory runs before JSON parsing and its exception instance is preserved. Native cancellation reaches CallApiAsync and the active HTTP transport. No login, token renewal, account switching, local formatting, permission inference or retry is added.

The contract audit uses contracts/openscope/openapi-2026-10-11.json (repository SHA-256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339). An anonymous public recheck on 2026-10-13 Europe/Berlin returned identical raw bytes to the pinned retrieval (SHA-256 255abeb5af135eac55380d3ed5213e25c6d55c59663cf0730b273ba0ce90293c), with zero recursively compared JSON changes, 294 paths and 323 operations.

Isolated regression fixtures reproduce all 18 absent HTTP-info forms before implementation and cover all 36 additive entry points, exact routes/bodyless GET/query semantics, original string escaping, full signed identifiers, complete JSON/raw headers, ambiguous errors, hook order/identity, pre-cancellation and active native cancellation. Historical null-data methods remain exercised with ordinary response headers. Existing generated methods still have their historical duplicate-header limitation; this focused additive change does not alter them. Real result shapes, profile permissions and server formatting require separately coordinated live acceptance. Package publication and downstream adoption remain separate.
