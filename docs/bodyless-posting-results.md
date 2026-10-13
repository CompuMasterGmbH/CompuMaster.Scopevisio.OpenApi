# Complete bodyless posting and journal cancellation results

Three additive workflows provide six synchronous, HTTP-info, asynchronous and native-cancellation forms each:

| API | POST route | Explicit selector | Required server profile |
|---|---|---|---|
| CreditApi.PostCreditWithResult | /credit/{number}/post | Original internal credit number | Rechnungen (Bearbeiten) |
| OutgoingInvoiceApi.PostOutgoingInvoiceWithResult | /outgoinginvoice/{number}/post | Original internal invoice number | Rechnungen (Bearbeiten) |
| AccountingApi.CancelPostingDocumentWithResult | /journal/{documentNumber}/cancel | Original journal document number (Buchungsnummer) | Journal (Bearbeiten) |

Original strings are escaped as one segment without trimming, numeric conversion, document lookup or account selection. Null/blank/exact-dot selectors fail before HTTP. Requests contain no body or query. No posting date, account, period, cancellation date or state is inferred. The existing generated void methods, HTTP-info null Data, configuration and defaults remain unchanged.

The pinned contracts declare HTTP 200 without a response schema. Complete JToken results retain unknown objects, arrays, scalar document numbers, JSON null and date strings; empty bodies return null. HTTP-info forms retain exact raw decoded JSON content, status and combined response/content headers. Parsed fractions use bounded decimal precision/range and may round; raw content remains exact. Original exception factories run before parsing, preserve their exceptions and retain the new operation-specific name.

Journal cancellation HTTP 200 can contain validation errors when a posting period is closed or accounts are locked. Callers must inspect the complete response: HTTP status alone does not establish that cancellation succeeded. An already cancelled posting returns its cancellation document number. HTTP 400 can indicate that the selected posting is itself a cancellation posting, and HTTP 404 that it was not found. No guessed error schema, boolean success flag or target-number mapping is manufactured.

Credit/invoice HTTP 400 can indicate an already posted document, while HTTP 404 can mean missing source or missing write permission. Those statuses and diagnostics remain subject to the configured original factory. Profiles and financial validation are server-authoritative.

Native cancellation reaches active HTTP I/O, with a pre-admission check. Cancellation after sending cannot establish whether posting/cancellation occurred. No new retry, follow-up lookup, uncertain write replay or local token/account change is added. This route-specific journal workflow remains separate from the existing optional-body CancelPosting workflow.

321 isolated fake-host cases cover all forms, original internal/journal selectors, body/query absence, exact raw/status/headers, empty and arbitrary unspecified results, HTTP-200 journal validation errors, original factories/errors, malformed success JSON, pre/active cancellation, unchanged local state and three historical void methods. Before implementation 318 missing-form cases failed while the three historical regressions already passed. All test hosts are isolated; no real posting or cancellation was performed. Actual resettable-account/profile/period/cancellation acceptance, the full live matrix, publication and downstream adoption remain separate.

