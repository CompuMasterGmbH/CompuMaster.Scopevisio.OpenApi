# Basic reporting data sources

SDK #38/#17 adds seven manufacturer Data sources endpoints through the existing ReportApi group. These are reporting extracts, with complete JToken results instead of invented fixed table models.

| ReportApi method | GET path | Required query |
|---|---|---|
| GetProductDataSource | /datasource/product | None |
| GetTeamworkDocumentMetadataDataSource | /datasource/teamwork | None |
| GetContactDataSource | /datasource/contact | startDate, endDate |
| GetBlogDataSource | /datasource/blog | startDate, endDate |
| GetTaskDataSource | /datasource/task | startDate, endDate |
| GetTimeEntryDataSource | /datasource/timeEntry | startDate, endDate |
| GetAuditLogDataSource | /datasource/auditLog | startDate, endDate |

All expose synchronous and native async data/WithHttpInfo forms, including native CancellationToken overloads. Required dates are validated as actual dd.MM.yyyy dates and sent unchanged. Both date parameters are mandatory for dated endpoints; the client does not impose an undocumented date-order/range interpretation or client-local default.

Optional fields is a comma-separated projection; the Contact data source also accepts the roles CSV. Null omits each optional parameter; explicit empty strings are sent. Known role values are interested, lead, customer, vendor, debitor, kreditor, employee, partner and worker. Inputs remain caller-selected strings; the server validates roles, required profiles and resource visibility. A CSV role is not proof of authorization.

The manufacturer declares no fixed successful response schema and no paging parameters for these seven endpoints. Object/array/JSON-null replies remain complete JToken data. An empty response body returns null data; exact raw text distinguishes it from a JSON-null reply. Int64 values, unknown/null fields and date-like strings remain intact. Fractions use .NET decimal precision (about 28–29 significant digits) and may round/underflow; RawJsonContent remains exact. Invalid JSON or out-of-range fractions fail clearly. No automatic paging, role inference, completeness claim or extra request is introduced.

The existing shared transport/parser retains raw/status/headers and configured exception identity, uses the caller's configuration/token and forwards cancellation to actual HTTP I/O. Existing generated members, ReportApi facade/configuration and old obsolete annotations remain unchanged; this batch neither introduces a new API group nor upgrades a package.

Authority: pinned OpenScope 11 October contract, repository SHA256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339; 12 October recheck found only six timestamp-example changes. Twenty-one absent-workflow cases fail before implementation; 230 isolated new cases cover wire/default/empty/date/shape/error/cancellation behavior on Windows net8.0/net48. Live profile/visibility/report-format acceptance remains in #38. User-authorized [skip ci] after essential platform checks; no credentials/live resources/publication/downstream adoption.
