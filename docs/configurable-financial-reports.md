# Configurable journal and financial reports

ReportApi.GetGeneralJournalDataSource retrieves GET /datasource/journal with required startDate/endDate strings in dd.MM.yyyy format. JournalDataSourceOptions independently controls reportName, unassigned, forcePersonalEntries, hideCancellationEntries, dimension1 through dimension10, accountingEntityNumbers CSV and fields CSV. Dimensions 1, 2 and 3 correspond to cost center, cost bearer and project; remaining dimensions retain the vendor's numeric identities.

ReportApi.GetFinancialReportDataSource retrieves GET /datasource/proReport with required month (1–12), year (Int32), definitionType (Int64), definitionName (account layout) and layoutName (column layout). The server validates years and definition types; documented definition types are 1 profit and loss, 2 balance sheet, 3 other evaluations, 4 cost center and 5 cost bearer. Valid required names are sent unchanged; null, empty and whitespace-only names fail before HTTP. Journal dates are validated and sent unchanged without imposing order/range rules.

FinancialReportDataSourceOptions provides budgetGroupName, organisations CSV, showAccounts, showGuvAccounts, showEmptyRows, showLineNumbers, useForeignLanguage and the three formatting choices:

| Choice | Documented values | Documented server default |
|---|---|---|
| AmountSuffix | NoSuffix, DebitCredit | DebitCredit |
| AmountFormatting | NoDecimalDigits, TwoDecimalDigits, DivideByThousand | TwoDecimalDigits |
| PercentFormatting | NoDecimalDigits, OneDecimalDigit, TwoDecimalDigits | TwoDecimalDigits |

Null options or null properties omit optional query keys. Explicit false/true and empty strings remain observable. Null organisations keeps the current organisation server default. No formatting/default is injected; exact caller-selected strings are sent for server validation, including future values. Integers use invariant formatting. Names and CSV strings retain whitespace, Unicode, leading zeros and escaping.

Each workflow provides six sync/data/WithHttpInfo/native async/cancellation forms through existing ReportApi and transport. Options are projected into query strings before HTTP I/O. Profiles, visibility, entity membership, filter/layout/report semantics and access remain server-enforced. Success schemas and paging are unspecified; route presence does not prove completeness, access or consolidated totals.

The shared parser retains complete JToken results, unknown fields, nulls, Int64 values, date-like strings and exact raw responses/status/headers. Fractions use bounded .NET decimal precision and can round or underflow; raw JSON remains exact. Exception hooks and original diagnostics run before parsing, and native cancellation prevents pre-canceled calls or cancels active I/O without added retries.

Isolated .invalid fixtures reproduce missing workflows and independently exercise every Boolean option, formatting choices/defaults, string escaping/empty values, required date/month/layout names, invariant period/type integers, standalone overload compilation, replies/errors and cancellation. Coordinated live reporting/profile/layout/dimension/entity/format acceptance remains open in #52. Existing generated API/facade/config/auth/global serializer/default behavior remains unchanged; no financial mutations, credentials/server resources, release or downstream import occur.
