# Explicit bodyless deletion and token-invalidation results

Parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), focused [#160](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/160), consumer [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

Four additive workflows each provide synchronous/HTTP-info/asynchronous/native-cancellation forms beside unchanged historical methods.

| Workflow | Exact DELETE route | Explicit selection and server contract |
|---|---|---|
| ContactApi.DeleteContactByIdWithResult | /contact/{contactIdOrLegacyNumber} | Original contact ID or legacy number; Kontakte (Bearbeiten), plus Personal for employee contacts |
| UserApi.DeleteUserWithResult | /users/{uidOrEmail} | Original UID/email; Benutzer (Bearbeiten); user is marked deleted |
| AdditionalApi.DeleteTeamworkTokenWithResult | /teamworktoken | Existing configured authorization; server invalidates its stored Teamwork token |
| AuthorizationApi.RevokeTokenWithResult | /token | Existing configured bearer token; server revokes it |

Required string selectors are escaped as one segment without trimming, case changes, numeric parsing, leading-zero removal or lookup. Null/blank/exact-dot selectors fail before admission. Every request is bodyless/query-free, without extra account selection, permission checks, query flags or automatic retry. These operations are called only when consumers explicitly invoke them.

The current contract declares HTTP204 success without a JSON schema. Normal acknowledgements have null data. HTTP-info retains original status, raw response and response/content headers. If the server supplies another successful JSON representation, it remains full JToken with unknown fields; invalid JSON remains visible. Parsed fractions have bounded decimal precision; raw JSON stays exact. Original exception factories receive the original response/new operation name before parsing. Contact404 can mean either missing contact or authorization; no local outcome/permission inference replaces it.

Pre-canceled native calls prevent HTTP admission; active cancellation reaches I/O. Cancellation after sending does not establish whether a deletion/revocation committed, so the SDK performs no replay. Successful or failed token operations do not clear, install or refresh local credentials or reset the configured client/account. Historical DeleteContactById/DeleteUser/DeleteTeamworkToken/RevokeToken signatures, results/defaults and common token/account/transport behavior are unchanged.

Initial isolated tests reproduce190 absent-form failures while four historical regressions pass. Final194cases cover all24forms, original single-segment IDs/emails, bodyless204/unknown JSON/raw/headers, selector guards, errors/factories/malformed success, pre/active cancellation/single transmission and preserved local token/client state. All use fake hosts; no contact/user/token was deleted or invalidated on a real server. Actual outcome/profile/diagnostic acceptance requires a coordinated explicitly owned resource/account scope. Complete server matrix, publication and DMS adoption remain separate.

