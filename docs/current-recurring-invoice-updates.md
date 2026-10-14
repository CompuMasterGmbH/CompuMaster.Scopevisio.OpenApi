# Current recurring-invoice updates

Tracked by [SDK #211](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/211), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`RecurringInvoiceUpdateRequest` deeply copies current validFrom, validTo, frequency, nextInvoiceDate and future JSON. Three nullable signed Int64 epoch values and an original frequency string make selections explicit without date/frequency defaults. Historical RecurringInvoiceForm, its seven enum values, constructors, zero defaults and UpdateRecurringInvoice methods remain unchanged. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`RecurringInvoiceApi.UpdateRecurringInvoiceWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/recurringinvoice/{number}`. The body is optional: null sends no body. The dedicated RecurringInvoiceForm schema requires all four fields in a supplied object. This workflow preserves that complete-object contract, rather than inferring a sparse recurrence contract. Missing fields, required nulls, incompatible JSON kinds and timestamps outside signed Int64 fail before HTTP. Typed null omits fields while constructing a request; a supplied complete request must still satisfy all four required fields. Unknown future JSON remains copied.

```csharp
var request = new RecurringInvoiceUpdateRequest
{
    ValidFrom = selectedValidFromMilliseconds,
    ValidTo = selectedValidToMilliseconds,
    Frequency = selectedFrequency,
    NextInvoiceDate = selectedNextInvoiceMilliseconds
};
var result = await recurringInvoiceApi.UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(
    selectedInternalInvoiceNumber, request, cancellationToken);
```

The original internal invoice-number string is escaped as one path segment without trimming, numeric conversion, leading-zero removal, ID translation or lookup. Null and dot segments fail before I/O; other strings remain server decisions. Original signed Int64 milliseconds are sent without calendar/timezone conversion, day truncation, current-year selection, inserted dates, minimum/ordering rules or next-invoice calculations. The manufacturer says time of day has no importance on the server; the client leaves timestamps unchanged. Current frequency values are daily, weekly, monthly, bimonthly, quarterly, half-yearly and yearly. Original frequency text remains unchanged without trimming, enum fallback or schedule expansion; actual frequency/date validity remains server-authoritative.

The server requires `Rechnungen (Bearbeiten)`. HTTP200 has no fixed successful result schema. HTTP404 may mean the recurring invoice is missing or the user lacks write permission; the client preserves that diagnostic rather than claiming absence. Complete JToken data, exact raw JSON, status and response/content headers remain available without invented invoice IDs, success flags or future invoices. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

The complete request and future nested JSON are deeply snapshotted before active HTTP. No account-context selection, hidden writes, invoice creation, retry or mutation replay is added. Cancellation after sending cannot establish whether recurrence data changed. Actual tests require an explicitly owned recurring invoice, verified original recurrence values and complete restoration; no unrelated invoice schedule is selected by the client.

211 isolated fake-host cases cover all four fields, complete-object/optional-body distinctions, required/kind/null/Int64 contracts, original epochs/frequencies/escaped selectors, copies/snapshots, full results/errors/factories/native cancellation and legacy models/enums/defaults. Actual Demo invoice/recurrence/date/frequency/profile acceptance and complete owned restoration remain open, requiring a fresh coordinated reversible window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
