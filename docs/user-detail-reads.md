# Explicit user details

UserApi.GetUserDetails and its HTTP-info/native asynchronous forms return complete JToken results for GET /users/{uidOrEmail}. The original required UID-or-email string is escaped as one path segment without trimming, email case normalization, UID conversion or current-user/account inference. Blank and relative-dot selectors are rejected. Requests have no body, query or inferred defaults.

The Benutzer (Anzeigen) profile is required. The manufacturer declares no fixed successful response schema or exhaustive error-status list. Complete objects, arrays, scalars, future fields and explicit null remain available; empty response content returns null. WithHttpInfo preserves status, combined response/content headers and exact raw JSON. Date strings remain strings, and parsed fractions follow .NET decimal precision with possible rounding/underflow/overflow; raw JSON stays exact. The caller's exception factory runs before parsing, original server diagnostics remain authoritative and native cancellation reaches admission and active HTTP I/O.

Historical GetUser methods retain their void/null-data behavior. This additive read changes no identity mapping, DMS user metadata, configuration, account selection or token behavior. Isolated UID/email examples prove unchanged wire text; they do not establish that the server accepts every illustrative string.

[SDK #122](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/122) tracks fake .invalid result/escaping/error/hook/cancellation fixtures, compiled compatibility and remaining coordinated actual UID/email/profile/visibility acceptance. Publication and downstream adoption stay separate.
