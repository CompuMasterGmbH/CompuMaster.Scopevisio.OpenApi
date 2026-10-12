# Dated operational reporting data sources

SDK #40/#17 extends the existing ReportApi with twelve further reporting extracts. It follows merged basic data-source PR #39 at a586ae5eb3be3df396449fc91d7efd659d560d91; generated APIs, configuration/facade, authorization, account selection and old obsolete annotations stay unchanged.

| ReportApi method | GET path |
|---|---|
| GetContactConversionDataSource | /datasource/conversion |
| GetEventJournalDataSource | /datasource/event |
| GetExpenseDataSource | /datasource/expense |
| GetHumanResourceDataSource | /datasource/humanResource |
| GetHumanResourceAvailabilityDataSource | /datasource/humanResourceAvailability |
| GetOpportunityDataSource | /datasource/opportunity |
| GetPersonalJournalDataSource | /datasource/personalJournal |
| GetProjectDataSource | /datasource/project |
| GetSalesProjectDataSource | /datasource/salesProject |
| GetTimeEntryRunDataSource | /datasource/timeEntryRun |
| GetTimeEntryRunEntryDataSource | /datasource/timeEntryRunEntry |
| GetTravelEntryPositionsDataSource | /datasource/travelEntryPositions |

Every endpoint requires startDate/endDate in dd.MM.yyyy and accepts optional fields CSV. Required dates are validated as real dates, then sent unchanged; no local date default or undocumented order/range interpretation is imposed. Null fields omits the parameter; an empty string is sent.

Only GetHumanResourceDataSource exposes hideTimeUnrestricted. Its nullable bool preserves the manufacturer's default true by omission. Explicit false includes time-unrestricted entries according to the server filter; true is sent explicitly. The client does not infer resource capabilities, rights or completeness from this option. All visibility/profile requirements remain enforced by the server.

Every workflow has synchronous data/WithHttpInfo and native async variants with/without CancellationToken. Tokens reach actual HTTP I/O; requests are sent once without automatic paging or replay. Existing exception hooks and original server diagnostics/raw/status/headers remain intact.

The manufacturer declares no fixed success schemas or paging parameters. Complete JToken replies preserve object/array/null shapes, Int64 values, unknown/null fields and date-like strings. An empty body yields null data; raw text distinguishes it from JSON null. Fractions use .NET decimal precision (about 28–29 significant digits) and may round/underflow; raw JSON remains exact, and out-of-range fractions fail parsing. No fixed table model or total/completeness guarantee is invented.

Names follow verified method/path/summary instead of misleading operation IDs: the opportunity route's ID refers to outgoing invoices, and personalJournal reuses a time-entry ID. These vendor discrepancies remain unchanged in the immutable snapshot.

Authority: contracts/openscope/openapi-2026-10-11.json, repository SHA256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339, with timestamp-only 12 October recheck. Thirty-six absent-workflow cases failed before implementation; 482 new isolated cases cover exact wire/default/filter/date/shape/error/cancellation behavior on Windows net8.0/net48. Coordinated live profile/report-format/HR-filter acceptance remains in #40. User-authorized [skip ci] after essential local checks; no credentials/server resources/financial writes/release/downstream import.
