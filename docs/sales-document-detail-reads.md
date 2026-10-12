# Complete sales-document details

Issue #110 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Six additive detail workflows expose complete JSON alongside unchanged generated void/Object/null-data methods:

| Workflow | Current GET route | Required profile |
| --- | --- | --- |
| GetOfferDetails | /offer/{number} | Angebote (Anzeigen) |
| GetOrderDetails | /order/{number} | Aufträge (Anzeigen) |
| GetDispatchDetails | /dispatch/{number} | Lieferscheine (Anzeigen) |
| GetOutgoingInvoiceDetails | /outgoinginvoice/{number} | Rechnungen (Anzeigen) |
| GetCreditDetails | /credit/{number} | Rechnungen (Anzeigen) |
| GetRecurringInvoiceDetails | /recurringinvoice/{number} | Rechnungen (Anzeigen) |

Each workflow has synchronous, HTTP-info, asynchronous and native cancellable asynchronous forms. The vendor identifies the selected string as the internal number (interne Nummer). The original selection is escaped as one path segment without trimming, numeric parsing, zero stripping or conversion into another identifier. No default document is fetched. Null, blank and dot-segment selections fail before HTTP. The credit schema includes a formatted document-number example despite its internal-number terminology; the client passes the exact caller-selected string and leaves resolution to the server.

The only documented query parameter for these six GETs is fields. Null omits it; empty and original selections remain unchanged. No formatting, paging, search or extension flags from other routes are added. No request body is sent.

The success contracts declare no fixed schemas. Complete JToken results preserve unknown fields, nested data, nulls and future envelopes/arrays/objects. HTTP-info retains exact raw JSON, status and response/content headers. Date-like strings remain strings. Fractional JSON uses .NET decimal precision and can round or exceed its range; raw JSON remains exact. No line-item/document envelope, local total or inferred transaction state is fabricated.

Required profiles and all failures remain server-authoritative. The response tables do not enumerate every possible error. The configured exception factory receives the original response before additive parsing, and its exception instance is preserved. Errors are not converted into guessed document absence or permission decisions. Native cancellation reaches active HTTP I/O. No token/account changes, mutation, local formatting or retry is added.

The audit uses the pinned 11 October contract (repository SHA-256 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339). Eighteen absent HTTP-info forms reproduce before implementation. Isolated cases cover all 36 entry points, exact selectors/query/bodyless GET, complete JSON/raw headers, failures/hooks, pre/active cancellation and historical data defaults. Actual server number resolution/results/permissions, publication and downstream adoption require separate verification. Historical routes absent from the current snapshot remain preserved.
