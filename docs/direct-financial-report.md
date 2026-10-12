# Direct financial report

`ReportApi.GetFinancialReportByLayout` exposes GET `/proreport/{type}/{name}/{year}/{month}` with synchronous, native asynchronous and cancellation overloads.

The pinned contract declares all four path parameters as strings. The client retains leading zeros and original valid strings, escapes each segment, and rejects missing, blank or relative-dot path selections. Documented type codes (1–5), months (1–12), available years and layouts remain server-validated. The required `layoutName` is a query string and retains original text. This contract stays separate from the numeric selections of `GetFinancialReportDataSource`.

Optional `FinancialReportDataSourceOptions` reuse the manufacturer's matching ten query fields. Null options and unset properties omit those fields, preserving server defaults. Explicit false, empty text and future formatting names are retained. No fields, paging, date conversion or aggregate completeness inference is added.

The complete JSON response remains a `JToken`; HTTP info retains exact raw JSON, status and headers. Dates remain strings and integers retain their original JSON kind. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Empty response bodies return null. The configured exception factory runs before parsing and retains its method name; cancellation reaches active HTTP I/O.

Isolated tests use a fake HTTP handler and an .invalid origin. They verify exact escaped wire requests, omitted defaults, independent boolean choices, unchanged selection strings, validation before transport, arbitrary reply shapes, integer/decimal/date fidelity, factory failures and active/pre-request cancellation. Live reporting permissions, configured layouts, accepted periods and actual financial values require separately coordinated server acceptance.
