# Explicit deprecated project resource and revenue range searches

Parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), focused [#158](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/158), consumer [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

The pinned manufacturer contract explicitly deprecates POST /projectresources and /projectrevenue. SearchProjectResourcesLegacy and SearchProjectRevenuesLegacy provide six explicitly obsolete full JSON/native-cancellation forms each for callers maintaining those epoch-range workflows. New code should select the existing modern SearchProjectResources/SearchProjectRevenues v2 date-string workflows. Legacy values are never converted or redirected to v2.

ProjectResourcesLegacySearchRequest is a copied extensible JObject with required original Int64 projectStartFrom/projectStartTo Unix epoch milliseconds and optional copied nullable searchSpecification. ProjectRevenuesLegacySearchRequest extends it with optional existing ProjectRevenueType. Dates retain their full signed range, including zero and independently selected bounds. Example first-/last-day-of-year extensions do not create defaults or range ordering rules. Typed null setters omit fields; inherited indexers retain nullable search JSON/future fields. Filters excluded by the shared searchSpecification schema remain caller-original and server-authoritative.

Optional revenueType accepts the declared Int32 integers 0 planning, 1 realization and 2 deviation. Absence preserves the server default of planning without inserting zero; a typed null setter omits the property. Explicit noninteger, null, overflow or out-of-range revenue values are rejected before HTTP. Modern v2 date strings remain caller-selected with no reinterpretation of inconsistent copied format prose.

Requests are deeply copied, validated and serialized before HTTP admission. Required integer kinds/widths, optional search object/null, finite JSON numbers and applicable revenue values are checked. JSON is the recommended media type; no queries, paging defaults, automatic follow-ups or retries are inserted. The server requires Projekte (Anzeigen). No allocations, personnel, accounts or finances are changed by these searches.

Unspecified HTTP200 success shapes remain full JToken, including arbitrary object/array/scalar/JSON null. Empty content returns null. HTTP-info retains original raw JSON/status/response and content headers. Parsed fractions have bounded .NET decimal precision; raw JSON stays exact. Original factories run before parsing with the matching new operation name. Native cancellation prevents pre-canceled admission and reaches active I/O.

Historical GetProjectResources/GetProjectRevenue generated methods retain original form/body defaults and JObject results. Existing typed modern v2 models/workflows and common serializers/account/OAuth/token behavior are unchanged.

Initial tests reproduced 326 missing-model/form failures with two historical regressions passing. Final 328 cases cover the twelve forms across both workflows, original full-width/zero/culture/independent ranges, defensive searches and snapshots, required kinds/widths, omitted/explicit revenue choices, arbitrary success shapes/raw/headers, original factories/errors, malformed/overflow responses and pre/active cancellation. These fake-host cases do not establish actual legacy range/search/revenue/profile behavior or v2 equivalence. Coordinated server matrix, publication and DMS adoption remain separate.

