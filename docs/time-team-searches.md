# Complete time-entry and selected project-team searches

Parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), focused [#156](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/156), consumer [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

ProjectApi adds six-form SearchTimeEntries and SearchProjectEmployees workflows. The exact POST routes are /timeentries and /project/{projectId}/projectemployees, with optional SearchRequest JSON bodies. Time entries require Zeiterfassung (Anzeigen); the selected team requires Kontakte, Projekte (Anzeigen). Required projectId is the original signed Int64 master identifier, formatted invariantly without lookup, minimum inference or account selection. No personnel, allocation, project or time-entry mutation occurs.

The pinned schemas label bodies as strings but link the manufacturer's [JSON-object search documentation](https://help.scopevisio.com/de/articles/467360-search-scope-documentation). Structured forms follow that documented workflow beside unchanged historical string encoding. Null omits the body, an empty object sends {}, and full paging/fields/order/format/count/criteria/future choices are serialized before HTTP admission. No defaults, clamps, date conversion, automatic paging or retry is inserted.

No fixed success schema is declared. Full JToken responses retain arbitrary records, metadata, scalars and JSON null; empty content returns null. HTTP-info retains original raw JSON, status and response/content headers. Parsed decimal fractions have bounded .NET precision; raw JSON remains exact. The configured factory sees the original response/operation name before parsing; team 403/404 remains a server diagnostic. Native cancellation reaches active I/O; pre-cancellation prevents sending.

Historical GetAllTimEntries spelling, JObject result and string-body behavior remain unchanged. Historical GetProjectEmployees retains its void/null result and defaults. Modern project resource/revenue v2 workflows and deprecated epoch-range workflows remain separate.

Initial tests reproduce 134 missing forms with two legacy regressions passing. Final 136 cases verify all twelve forms, full-width invariant project identifiers, optional search choices, serialized snapshots/culture, unknown/count/null/empty/raw/header responses, factories/errors/malformed/overflow and pre/active cancellation. Fake-host tests do not establish actual data/search/profile acceptance. Required coordinated server matrix, publication and DMS adoption remain separate.

