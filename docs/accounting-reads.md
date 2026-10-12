# Accounting lookup and structured search workflows

This bounded part of SDK #32/#17 adds nine missing read-only Accounting operations. It builds on integrated SDK a2e4d68a4768b835bc27fd91decbd96925e89dcf. Existing public methods, generated sources, serializers, authorization and account-selection defaults remain unchanged.

| Workflow | Method and path | Result |
|---|---|---|
| GetAccountMapping | GET /accountmapping | AccountMappingResponse |
| GetFiscalYears | GET /fiscalyears | Complete JToken |
| GetGainAndLossAdjustmentAccounts | GET /gainandlossadjustmentaccounts | Complete JToken |
| GetStatisticsAccountByNumber | GET /statisticsaccount/{number} | Complete JToken |
| GetStatisticsPosting | GET /statisticspostings/{rowNumber} | Complete JToken |
| GetVatScopes | GET /vatscopes | VatScopesResponse |
| SearchStatisticsAccounts | POST /statisticsaccounts | Complete JToken |
| SearchStatisticsPostings | POST /statisticspostings | Complete JToken |
| SearchTravelEntries | POST /travelentries | Complete JToken |

All expose synchronous data/WithHttpInfo methods, native async overloads with and without CancellationToken, and async WithHttpInfo variants. Tokens reach active HTTP I/O. Raw response text, status, combined case-insensitive headers and configured exception hooks are retained. Successful HTTP responses do not establish account profiles or availability; the server enforces those requirements.

AccountMappingResponse, AccountMappingEntryRecord, VatScopesResponse and VatScopeRecord deeply copy JSON. Known values remain nullable for omission/JSON null; unknown fields remain available through JObject. Mapping entries retain leading-zero account numbers, Int64 identifiers/length values, and Int32 entryCount. VAT case IDs and timestamps retain Int64 values. No epoch-to-date or timezone conversion is inferred. Typed list getters return read-only lists of copied records. Omitted/null lists return null; explicit empty lists remain empty. Non-object roots and non-array/non-object collection shapes fail clearly. Typed root JSON null returns null data, with the exact raw reply retained; untyped JToken methods retain a JSON-null token.

Null optional query values are omitted, preserving active-accounting-area/active-filter/selected-field server defaults. Explicit false, empty fields and full Int64 values are sent unchanged using invariant wire formatting. Statistics account numbers remain strings, including leading zeros, and are escaped as one path segment. Missing/blank or relative-segment account numbers fail before HTTP. Required balanceDate must be a real date in dd.MM.yyyy format (including leap-day validation); valid text is sent unchanged. No arbitrary positivity restrictions are inferred for accounting-area IDs or posting row numbers.

SearchRequest is sent as the documented JSON search object. Null omits the body; an explicit empty object is sent. The pinned OpenAPI string schema conflicts with the linked manufacturer search documentation; this existing discrepancy is retained explicitly. No automatic paging or count/format defaults are imposed. Fractional response numbers use .NET decimal precision (approximately 28–29 significant digits) and may round/underflow; raw JSON remains exact, and out-of-range fractions fail parsing. Unknown response shapes on the seven schema-less operations are retained as JToken.

The source contract is contracts/openscope/openapi-2026-10-11.json (repository SHA256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339). An anonymous 12 October recheck found only the same six timestamp-example differences; routes/schema shapes remain current. The immutable committed snapshot is retained.

Isolated fake-transport fixtures reproduce the absent public workflows before implementation and verify wire forms/defaults/raw/status/headers, typed precision/null/list/clone behavior, errors and pre/active cancellation on Windows net8.0/net48. No server resources, credentials or live calls are involved. Independent review/merge and live profile/date acceptance are tracked separately in #32. User-authorized [skip ci] is used after essential local checks. No booking/import/clearing/rebooking, VAT matrix replacement, file streaming, package publication or downstream dependency adoption is included.
