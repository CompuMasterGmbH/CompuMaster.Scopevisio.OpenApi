# Current organisation creation

Tracked by [SDK #219](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/219), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`OrganisationCreationRequest` deeply copies current organisationName/scoa strings and future JSON. `OrganisationApi.CreateOrganisationWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/organisation/new`. Historical OrganisationForm/ScoaEnum, constructors, defaults and CreateOrganisation methods remain unchanged. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

The primary body is optional: null sends no body. A supplied object requires original organisationName. Known supplied fields must be strings; missing name, known raw nulls or incompatible kinds fail before I/O. Typed null omits a field while constructing a request. Original names/chart selections, including whitespace, empty strings and case, remain unchanged without trimming, naming defaults, lookup, enum fallback or reuse of an existing organisation. Actual content and chart availability remain server-authoritative. Omitted scoa leaves chart setup to the user on the server; the client chooses no chart or setup operation.

```csharp
// Configure timeouts before the first use of this transport.
configuration.Timeout = 300000;
// A caller-owned HttpClient must also allow at least five minutes.
var request = new OrganisationCreationRequest
{
    OrganisationName = selectedNewOrganisationName,
    Scoa = selectedChartName
};
var result = await organisationApi.CreateOrganisationWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

The manufacturer requests a timeout of at least five minutes because chart setup takes time. Default Configuration.Timeout is100000ms; the cached RestClient reads its timeout options on first use. A caller-owned HttpClient can impose its own shorter timeout. Configure both applicable timeouts before first transport use. The new workflow neither changes shared/cached options nor resets the client, sleeps, retries or assumes that five minutes guarantees completion. Cancellation after sending cannot establish whether an organisation exists or chart setup has started or completed.

The server requires `Organisation (Bearbeiten)`. HTTP201 has no fixed successful result schema or guaranteed organisation-ID field. HTTP400 describes an existing organisation name or unavailable standard chart; the client preserves these failures without interpreting them as an upsert/reuse signal or fetching an existing organisation. Complete JToken data/future JSON, exact raw JSON, status and response/content headers remain available without invented IDs, completion flags or chart guarantees. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

The complete request and nested future JSON are deeply snapshotted before active HTTP. No automatic organisation/token/context selection, hidden request, retry or mutation replay is added. A new global organisation is not contained merely by selecting the Demo organisation. Actual tests need separately owned organisations/charts, verified absence and a complete reversible cleanup/reset strategy; this SDK workflow does not establish such ownership or cleanup capability.

145 isolated fake-host cases cover optional body/required name, original/current chart strings, kinds/nulls/copies/future snapshots, full results/errors/factories/native cancellation, unchanged timeouts/tokens/context/transport and legacy models/enums/defaults. Actual explicitly test-owned organisation/chart/profile creation and full owned cleanup/reset remain open, requiring a fresh coordinated reversible window with exact numeric Scope organisation proof plus separate global-organisation ownership and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
