# Product usage reporting data sources

Five additive ReportApi GET workflows preserve complete JToken results through six sync/data/WithHttpInfo/native async/cancellation forms.

| Method | GET path | Optional endpoint-specific filter |
|---|---|---|
| GetCreditNoteProductUsageDataSource | /datasource/productUsage/creditNote | accountingEntityNumbers |
| GetDispatchProductUsageDataSource | /datasource/productUsage/dispatch | No accountingEntityNumbers |
| GetOfferProductUsageDataSource | /datasource/productUsage/offer | validOffersOnly, accountingEntityNumbers |
| GetOrderProductUsageDataSource | /datasource/productUsage/order | accountingEntityNumbers |
| GetOutgoingInvoiceProductUsageDataSource | /datasource/productUsage/outgoingInvoice | accountingEntityNumbers |

All take required valid startDate/endDate in dd.MM.yyyy and optional showCustomersSeparately, currency and fields. The offer workflow additionally takes validOffersOnly; dispatch has no accountingEntityNumbers parameter. Dates are sent unchanged; the server defines order, range semantics and supported filters.

Nullable booleans distinguish omission from explicit false/true. Null currency keeps the documented server EUR default without sending a client-selected currency; strings (including empty values) are retained for server validation. Accounting entity numbers and fields stay CSV text, including leading zeros and escaped characters. Entity numbers do not prove membership, access or consolidated totals.

The server enforces reporting profiles, visibility, customer grouping, currency and offer validity. Success schema and paging are unspecified; no completeness or automatic paging is inferred. The shared parser retains unknown fields/nulls/Int64/date-like strings, exact raw JSON, status and headers. Fractions use bounded .NET decimal precision and can round or underflow; raw JSON is exact. Exception hooks and original diagnostics run before parsing. Active/pre-cancellation reaches the existing native transport without an added retry.

Isolated .invalid HTTP fixtures reproduce missing workflows and verify wire/default/filter/date/shape/error/cancellation behavior. Coordinated live profile/grouping/currency/valid-offer/report acceptance stays open in #48. Generated APIs, defaults/facade/configuration/authorization/global serializers are unchanged. No bookings or imports are performed.
