# Current user batch writes

Tracked by [SDK #217](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/217), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`UserWriteRequest` exposes all fifteen writable current UserForm fields: eight original strings, four nullable booleans and three copied assignment arrays. It includes pendingLogin, updateScopevisioEMail, updateTeamworkEMail and newEmail, missing from the historical model. `UserBatchWriteRequest` is a copied JSON array preserving complete future row data and order. Historical UserForm/Profile/License/TeamworkRole/UserApi, enums, constructor requirements, false defaults and legacy-only password/buyMissingLicenses remain unchanged. Those legacy-only fields are not introduced as current typed request fields. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`UserApi.CreateUsersWithResult` and `UpdateUsersWithResult` each provide six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST/PUT `/users`. Bodies are optional arrays: null sends no body and an explicit empty batch sends `[]`. Each supplied row must be an object. Creation rows require original email/firstName/lastName strings; updates retain sparse fields and optional explicit raw nulls under general update contracts. Known fields must have their declared JSON kinds. Empty/whitespace strings and actual identity/content rules remain server-authoritative rather than being repaired by the client. Typed null omits fields.

```csharp
var user = new UserWriteRequest
{
    Uid = selectedUserUid,
    NewEmail = selectedNewEmail,
    UpdateScopevisioEMail = true,
    UpdateTeamworkEMail = false
};
var request = new UserBatchWriteRequest(new JArray(user));
var result = await userApi.UpdateUsersWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

UID, existing email, pending login, new email, names and gender remain original strings without case conversion, trimming, identity lookup, UID/email XOR, local precedence or default user selection. User UID, user-contact IDs, numeric Scope organisation and opaque Teamwork tenant selectors remain distinct. Omitted administrator/changePassword/updateScopevisioEMail/updateTeamworkEMail remain distinct from false and true. Explicit options are server-evaluated; the client inserts no password, login change, profile, administrator or privilege default and makes no separate Teamwork request.

Status, specialRights and creationTs are current read-only fields and are rejected in writes even when supplied as null. License.validTill is also read-only and rejected in assignment input. The client does not silently drop these fields. Complete response JSON, including read-only and future data, remains available; input restrictions do not truncate output. Reconstruct intended writable selections rather than assuming a complete server user object is a valid write request.

Profiles, licenses and teamworkRoles are deeply copied on assignment and reading. Each supplied assignment must be an object. Profile requires original name/organisation, License requires original type and TeamworkRole requires original teamworkRole/organisation strings. Empty arrays, configured/current catalogue strings, order and future assignment data remain original without lookup, merge, sorting, expiry calculation, license purchase or default role/profile assignment. Actual replacement/clearing/null semantics remain server-authoritative; the client does not claim that every optional null clears an assignment.

Profile/TeamworkRole organisation is an original organisation name. The manufacturer documents `Instanz` for instance-wide profiles; this is not a numeric Scope organisation or opaque Teamwork tenant selector. Assignment scope may therefore exceed the selected organisation. The profile `Firmen und Benutzer` requires administrator true on the server, but the client neither inserts true nor chooses an instance-wide profile. Server user/login/profile/license/role rights and outcomes remain authoritative. Actual tests require separately owned users and assignments, not merely a Demo token, read access or organisation-name match.

The server requires `Benutzer (Bearbeiten)`. Both HTTP200 successes have unspecified result schemas and do not establish per-user/aggregate success or transaction atomicity. Complete JToken data/future errors, exact raw JSON, status and response/content headers remain available. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

Complete batches, rows, assignments and future JSON are deeply snapshotted before active HTTP. No account-context selection, hidden write, lookup, retry or mutation replay is added. Cancellation after sending cannot establish which user/login/assignment changes occurred. 674 isolated fake-host cases cover all writable/read-only/child fields, creation/sparse updates, array/optional/empty/order distinctions, kinds/nulls/required assignments, original logins/flags/scopes, copies/snapshots, complete output/errors/factories/native cancellation and legacy APIs/defaults. Actual test-owned Demo user/login/profile/license/Teamwork-role behavior and complete owned restoration remain open, requiring a fresh coordinated reversible window with exact numeric Scope organisation proof plus separate assignment-scope ownership and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
