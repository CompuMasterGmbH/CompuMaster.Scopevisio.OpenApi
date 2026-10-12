# Standard dated reporting data sources

The twelve additive ReportApi workflows use the exact dated GET routes in the pinned OpenScope contract. Sync/data/WithHttpInfo/native async/cancellation forms preserve complete JSON results, exact raw responses, status and headers.

| Method | GET path | Additional optional filters |
|---|---|---|
| GetContactPropertiesDataSource | /datasource/contactProperties | fields |
| GetDispatchDataSource | /datasource/dispatch | fields |
| GetDispatchPositionsDataSource | /datasource/positions/dispatch | fields |
| GetOpportunityPositionsDataSource | /datasource/positions/opportunity | fields |
| GetCreditorAccountDataSource | /datasource/personalAccount/creditor | fields |
| GetDebtorAccountDataSource | /datasource/personalAccount/debtor | fields |
| GetOpportunityProductUsageDataSource | /datasource/productUsage/opportunity | fields |
| GetProjectResourceDataSource | /datasource/projectResource | fields |
| GetProjectRevenueDataSource | /datasource/projectRevenue | revenueType, fields |
| GetSusaCreditorsDataSource | /datasource/susa/creditors | accountingEntityNumbers, fields |
| GetSusaDebtorsDataSource | /datasource/susa/debtors | accountingEntityNumbers, fields |
| GetSusaImpersonalAccountsDataSource | /datasource/susa/impersonalAccounts | accountingEntityNumbers, fields |

Dates are mandatory valid dd.MM.yyyy strings sent unchanged; range ordering and inclusivity remain server decisions. Null omits optional values, while empty strings are sent. Fields and accounting entity numbers are CSV text; leading zeros and escaped characters remain intact. Entity numbers do not establish membership, access or consolidated totals.

Project revenue reporting uses the vendor's localized string choices Planung, Realisierung and Abweichung. The client sends the selected string without translating to the integer ProjectRevenueType used by separate project endpoints; the server validates current/future values.

Profiles, visibility, date/filter semantics and report formats require coordinated live acceptance. Success schemas and paging are unspecified, so JToken retains unknown fields, nulls, Int64 values and date-like strings. Fractions use bounded .NET decimal precision and can round or underflow; raw JSON is exact. Route presence does not imply complete or live-verified support.

The projectResource vendor operation ID incorrectly names a statistics journal; SUSA debtors has a creditor summary. SDK method names follow the verified paths; the immutable vendor snapshot retains these discrepancies. Generated methods, facade/configuration/authorization/default behavior remain unchanged. No postings, imports or other financial mutations are performed.

Isolated fixtures reproduce absent workflows first and cover wire contracts, date validation, optional omission/empty values, localized strings, result shapes, original server/custom-hook diagnostics and active/pre-cancellation. They do not access credentials or real servers. Live acceptance stays open in #46.
