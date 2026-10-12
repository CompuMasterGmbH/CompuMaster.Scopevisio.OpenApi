# ML Engine export reads

AccountingApi provides six sync/native asynchronous/HTTP-info/cancellation forms per workflow:

| Method | POST route | Search request |
|---|---|---|
| SearchHypatosExport | /mlexport/hypatos | IdExportSearchRequest |
| SearchInvoiceExport | /mlexport/invoice | IdExportSearchRequest |
| SearchJournalExport | /mlexport/journal | JournalExportSearchRequest |
| SearchPersonalAccountExport | /mlexport/personal-account | IdExportSearchRequest |

These operations retrieve the caller organisation's export rows under the server-enforced ML Engine Export (Anzeigen) profile. Hypatos returns inline OCR JSON; invoice export includes rows with nonempty raw posting XML; journal projection includes original account types/dimensions, timestamp strings and decimal strings; personal-account projection contains creditor/debtor master identities and type codes. No OCR/import/financial writes occur.

The manufacturer promises ascending identity order for the identity-based exports, and ascending row-number order for journal exports. afterId and afterRowNumber are optional caller-selected keyset cursors. Select them from actual returned rows; the client does not invent a cursor, advance it automatically, decide completion or fetch further pages. The journal request also accepts inclusive from/to posting timestamp bounds in epoch milliseconds. No date/timezone conversion or local range reordering is performed.

Both request types extend SearchRequest, deeply copy source JSON and retain unknown filters. Typed cursor/bound properties are nullable Int64; assigning null omits the field, while zero/negative selections remain explicit for server validation. The inherited indexer can retain explicit JSON null separately. Malformed raw cursor/bound types, fractions and integers outside Int64 fail before HTTP I/O rather than silently coercing them. No cursor is guessed from paging defaults.

Optional search bodies are snapshotted once before HTTP I/O. Null omits the body; an empty request sends {}. The manufacturer declares a string schema but links [structured-object search documentation](https://help.scopevisio.com/en/articles/467360-search-scope-documentation); the client sends one JSON object without quoting it twice. General search/paging/order/count choices remain caller-controlled and server-validated.

The contract declares no fixed success schema. JToken and exact raw JSON/status/headers retain unknown projection fields, nested OCR JSON, posting XML text, timestamp strings and decimal strings. Those strings are not converted to numbers/dates/XML DOMs. Parsed numeric fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Empty bodies return null. Factory names/original diagnostics run before parsing; native cancellation reaches active I/O. No page, aggregate, tenant-completeness or export-success inference is made from route exposure.

Isolated fake-handler tests use .invalid and cover all routes/forms, search snapshots/null-versus-empty bodies, exact projections, native cancellation, original errors, copied models and cursor/bound JSON types/limits. Real profile grants, organisation selection, projection correctness, stable cursors and inclusive windows require separately coordinated acceptance under SDK #64. No GUI, credentials or real server is accessed by these fixtures.
