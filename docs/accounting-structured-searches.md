# Complete accounting structured searches

Parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), focused [#154](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/154), consumer [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

Seven additive AccountingApi workflows expose complete JSON results beside historical string-body methods. Each provides synchronous/HTTP-info/asynchronous forms and native cancellation.

| New workflow | Exact POST route | Body | Server profile |
|---|---|---|---|
| SearchDebitorAccounts | /debitoraccounts | Required explicit SearchRequest | Stammdaten, Debitorenstamm (Anzeigen) |
| SearchKreditorAccounts | /kreditoraccounts | Required explicit SearchRequest | Stammdaten, Kreditorenstamm (Anzeigen) |
| SearchImpersonalAccounts | /impersonalaccounts | Required explicit SearchRequest | Stammdaten, Kontenplan (Anzeigen) |
| SearchJournalEntries | /journal | Optional SearchRequest | Journal (Anzeigen) |
| SearchPersonalJournalEntries | /personaljournal | Optional SearchRequest | Journal (Anzeigen) |
| SearchDimensions | /dimensions | Optional SearchRequest | Journal (Anzeigen) |
| SearchZones | /zones | Optional SearchRequest | Stammdaten, Rechnungskreis (Anzeigen) |

The pinned OpenAPI calls these search schemas strings, but explicitly links the manufacturer's [JSON-object search documentation](https://help.scopevisio.com/de/articles/467360-search-scope-documentation), rechecked 13 October 2026. These structured forms follow that object workflow; historical string transmission remains unchanged. Required searches reject null before I/O rather than inserting a default body; callers can explicitly choose an empty object. Optional null omits the body, while an empty object sends {}. Original paging, fields, ordering, formatting, count, search criteria and future filters remain unchanged. No automatic paging, client clamp, inferred profile, account selection or mutation occurs.

For journal/personal-journal searches the documented createdSince, createdBefore, postingDateSince, postingDateBefore and externalDocumentDateSince epoch-millisecond filters can be assigned through inherited indexers. They remain original signed Int64 values without date conversion, new typed date restrictions or inserted range defaults. SearchRequest is extensible; no schema is guessed from account/journal naming.

Requests serialize to JSON before HTTP admission, so later handler-side changes do not alter the selected payload. Every route returns full JToken and, for HTTP-info forms, unchanged raw JSON/status/response plus content headers. Arbitrary object/array/count/null results remain intact; empty-body null differs from a JSON null token. Parsed decimal fractions have bounded .NET precision; raw JSON is exact. Factories receive the original response and operation name before parsing. The dimension route's documented 404 remains an error under the configured factory. Native tokens prevent pre-canceled I/O and reach active transport; no automatic retry is introduced.

Generated legacy methods keep their previous string-body encoding, required-body guards, void/null results and defaults. Other accounting writes, fiscal configuration, account/OAuth/token behavior and shared serializers are unchanged.

Initial tests reproduced 413 missing-form/signature failures with seven historical regressions passing. Final 420 cases cover every route/form, required rejection/optional omission, explicit empty/full bodies, original epoch filters/culture/snapshots, complete response kinds/raw/headers, errors/factories/malformed/overflow, native cancellation and preserved historical behavior. Isolated handlers use only a fake host. Actual account/journal/dimension/zone/search/profile behavior, full coordinated server matrix, publication and downstream adoption remain separate prerequisites.

