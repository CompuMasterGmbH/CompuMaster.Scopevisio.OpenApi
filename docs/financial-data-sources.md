# Financial-document reporting data sources

SDK #44/#17 adds twelve GET reporting extracts through existing ReportApi, on merged f357e5cd3b895292187584e64dcf8fc6c18d7c28. Existing generated methods, facade/configuration, authorization, account-selection defaults and old annotations are unchanged.

| ReportApi method | GET path |
|---|---|
| GetCreditNoteDataSource | /datasource/creditNote |
| GetIncomingInvoiceDataSource | /datasource/incomingInvoice |
| GetOfferDataSource | /datasource/offer |
| GetOrderDataSource | /datasource/order |
| GetOutgoingInvoiceDataSource | /datasource/outgoingInvoice |
| GetCreditNotePositionsDataSource | /datasource/positions/creditNote |
| GetIncomingInvoicePositionsDataSource | /datasource/positions/incomingInvoice |
| GetOfferPositionsDataSource | /datasource/positions/offer |
| GetOrderPositionsDataSource | /datasource/positions/order |
| GetOutgoingInvoicePositionsDataSource | /datasource/positions/outgoingInvoice |
| GetStatisticsJournalDataSource | /datasource/statisticsJournal |
| GetTravelEntryDataSource | /datasource/travelEntry |

Each endpoint requires actual dd.MM.yyyy startDate/endDate and accepts optional accountingEntityNumbers/fields CSV. Valid dates are sent unchanged; no undocumented order/range/client-local-date default is imposed. Accounting entity numbers remain text, including leading zeros, rather than being converted to numeric IDs. Null omits each optional filter; explicit empty strings remain explicit. Query values are escaped by the existing transport and cannot introduce additional query parameters.

These are reporting reads, including document headers/positions, statistics journal and travel entries. They do not create invoices, import transactions, perform posting/clearing/rebooking, replace VAT matrices or calculate consolidated totals. The server enforces profile, visibility and entity-filter membership; a CSV entity selection does not prove access. Header and position extracts have distinct paths and may have different required profiles.

Every method has synchronous data/WithHttpInfo and native async variants with/without CancellationToken. Tokens reach HTTP I/O, each workflow sends once and existing exception hooks preserve identity. Raw response text, status and combined headers remain accessible.

The manufacturer declares no fixed success schema or paging parameters. Complete JToken results retain objects/arrays/JSON null, Int64 values, unknown/null fields and date-like strings. Empty bodies yield null data, distinguishable through exact raw text. Fractions use .NET decimal precision (about 28–29 significant digits) and may round/underflow; RawJsonContent remains exact, while out-of-range fractions fail parsing. No fixed table model, automatic paging, role inference or completeness guarantee is introduced.

Authority: immutable contracts/openscope/openapi-2026-10-11.json, repository SHA256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339, with timestamp-only 12 October recheck. Thirty-six absent-workflow cases fail before implementation; 480 new isolated cases cover exact wire/filter/default/date/shape/error/cancellation behavior on Windows net8.0/net48. Live profile/entity-filter/report-format acceptance remains in #44. User-authorized [skip ci] after essential local checks; no credentials/server resources/publication/downstream adoption.
