# Dated sales and invoice position pages

Seven additive workflows return one page of positions for the selected document-date range.

| Existing API class | Method | GET path |
|---|---|---|
| CreditApi | GetCreditPositionPage | /credit/all/positions |
| DispatchApi | GetDispatchPositionPage | /dispatch/all/positions |
| OfferApi | GetOfferPositionPage | /offer/all/positions |
| OpportunityApi | GetOpportunityPositionPage | /opportunity/all/positions |
| OrderApi | GetOrderPositionPage | /order/all/positions |
| OutgoingInvoiceApi | GetOutgoingInvoicePositionPage | /outgoinginvoice/all/positions |
| RecurringInvoiceApi | GetRecurringInvoicePositionPage | /recurringinvoice/all/positions |

Each workflow has six sync/data/WithHttpInfo/native async/cancellation forms. Required startDate/endDate are valid dd.MM.yyyy strings sent unchanged; order and range semantics remain server decisions. Optional fields CSV preserves omission versus explicit empty strings and escaping.

Nullable page is zero-based: omitted retains the server default, explicit zero is sent. Explicit page must be nonnegative. Nullable pageSize omits the server default or sends a caller choice from 1 through 5000. No default page size is injected and no extra pages are fetched.

The vendor limits each response to at most 5,000 entries and recommends pagination or shorter date ranges for complete exports. One response does not prove all required positions were retrieved. Success schema and pagination metadata are unspecified; the client does not infer effective page size, total count or a completion rule. Coordinated live pagination/profile/result-format acceptance stays open in #54.

The recurring-invoice route's response description incorrectly refers to a given invoice number despite the verified all/positions route and no number parameter. The immutable snapshot retains this ambiguity; the client does not invent a number parameter or change the route.

The shared parser retains complete JToken/raw/status/headers, unknown fields, nulls, Int64 and date-like strings. Fractions use bounded .NET decimal precision and can round or underflow; raw JSON is exact. Original diagnostics and caller exception hooks run before parsing. Native cancellation prevents pre-canceled calls or cancels active I/O without added retries.

Isolated .invalid fixtures reproduce absent workflows and verify exact paths/queries, omitted/empty values, dates/page boundaries/errors, full replies/hooks and cancellation. Existing generated methods/facade/configuration/auth/global serializers/defaults remain unchanged. No financial writes/imports, credentials/server resources, release or downstream import occur.
