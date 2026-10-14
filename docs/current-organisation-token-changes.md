# Current low-level organisation token changes

Tracked by [SDK #221](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/221), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`AuthorizationApi.ChangeOrganisationByIdWithResult` and `ChangeOrganisationByNameWithResult` each provide six synchronous, HTTP-info, asynchronous and native CancellationToken forms for PUT `/token/organisation/{organisationId}` and `/token/organisation`. The primary requires a signed Int64 path ID or original name query and declares no body. Historical APIs and their validation/data-discarding behavior remain unchanged.

Original numeric IDs are formatted invariantly without minimum rules, lookup or translation to a name, user UID or Teamwork tenant. The required original name is encoded once as exactly one query value without trimming, case conversion, lookup or default target. Null fails before HTTP; empty text and dot names remain query values for server validation and cannot become path segments. No body, extra query selector or second request is inserted.

```csharp
var rawResult = await authorizationApi.ChangeOrganisationByIdWithResultAsyncWithHttpInfo(
    explicitlySelectedScopeOrganisationId, cancellationToken);
// Inspect the complete response; this primitive does not verify or install scope.
```

These low-level operations may change server token context. HTTP200 and the returned JSON alone do not establish requested numeric organisation, actor identity, fresh account or Teamwork tenant. Complete original/future response data, including returned tokens or a different organisation, remain available without a verified-scope flag. The client does not silently adopt/install tokens, update Configuration.OrganisationId, normalize a target, fall back to another organisation, read credential/profile/cache files or perform a fresh-account call.

For real Demo business operations, use the existing staged high-level authorization flow with an explicitly configured numeric Configuration.OrganisationId. That separate workflow checks raw/typed token organisation and identity, then fresh myaccount organisation/user proof before installation. The new low-level methods do not alter or replace it. A company-name hint is insufficient; numeric Scope organisation, name strings, user identifiers and opaque Teamwork tenant selectors remain distinct. Demo identity/ownership/exclusive-window requirements still apply.

Both primary HTTP200 successes are unspecified JSON shapes; HTTP400 describes invalid name or ID. Complete JToken data, exact raw JSON, status and response/content headers remain available without invented verified identity, success, user or tenant assertions. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

Local configuration, token, organisation and timeout values remain unchanged by these methods. This does not prove unchanged server token scope after a request. Cancellation after sending likewise cannot establish scope or restoration. Native cancellation propagates to HTTP without worker-thread wrappers, extra calls, mutation retries or replay.

114 isolated fake-host cases cover all forms, primary selectors/no body, signed/invariant IDs, exact query encoding, null/empty/dot/Unicode names, complete tokens/future/wrong-target passthrough, headers/status/errors/factories, no-install/configuration behavior, pre/active cancellation and legacy APIs/validation. Actual explicitly owned token/organisation/identity behavior and restoration remain open, requiring a new exclusive finite window with exact numeric Demo organisation and fresh account proof plus separate Teamwork tenant when needed, without PROD fallback or old grant/cache reuse. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
