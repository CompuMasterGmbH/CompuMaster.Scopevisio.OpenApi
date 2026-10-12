# Named planning reporting reads

ReportApi.GetBudgetGroupDataSource retrieves GET /datasource/budgetGroup with required budgetGroupName; GetPlanDataSource retrieves GET /datasource/plan with required planName. Both accept optional fields CSV and provide sync/data/WithHttpInfo/native async/cancellation forms.

These endpoints take exact names without dates or numeric IDs. A null, empty or whitespace-only required name fails before HTTP. A valid name retains surrounding whitespace, Unicode and escaped characters without trimming or reinterpretation. Null fields omits the optional parameter; empty fields is sent for server validation.

The server enforces planning profiles, name lookup, visibility and report format. Success schemas and paging are unspecified; no access or completeness is inferred. Complete JToken/raw/status/headers preserve unknown fields, nulls, Int64 numbers and date-like strings. Fractions use bounded .NET decimal precision and can round/underflow; raw JSON remains exact. Existing exception hooks run before parsing and retain original/custom exception identity. Cancellation reaches active native I/O and prevents pre-canceled requests without added retries.

Isolated .invalid HTTP fixtures reproduce absent workflows and cover names/query contracts, omission/empty fields, result shapes, diagnostics and cancellation. Coordinated live planning-profile/name/report-format acceptance stays open in #50. Existing generated API/configuration/facade/auth/global serializers/default behavior remain unchanged. No planning writes occur.
