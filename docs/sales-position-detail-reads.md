# Current selected sales-position reads and paging

Issue #132 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Seven additive JSON workflows retain historical generated methods, signatures and null-data defaults.

| Workflow | Current GET route | Required profile |
| --- | --- | --- |
| GetOfferPositionsDetails | /offer/{number}/positions | Angebote (Anzeigen) |
| GetOrderPositionsDetails | /order/{number}/positions | Aufträge (Anzeigen) |
| GetDispatchPositionsDetails | /dispatch/{number}/positions | Lieferscheine (Anzeigen) |
| GetOutgoingInvoicePositionsDetails | /outgoinginvoice/{number}/positions | Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen) |
| GetCreditPositionsDetails | /credit/{number}/positions | Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen) |
| GetRecurringInvoicePositionsDetails | /recurringinvoice/{number}/positions | Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen) |
| GetOpportunityPositionsDetails | /opportunity/{number}/positions | Rechnungen, Potenzial (Opportunities) (Anzeigen) |

Each workflow has six synchronous/HTTP-info/asynchronous/native-cancellation forms. The current contract explicitly selects the original internal number (interne Nummer) as a string. It is escaped as one segment without trimming, numeric parsing, zero removal or identifier lookup. Blank and dot selectors fail before HTTP. Parent reads separately use document-number terminology; no translation is inferred.

The old signatures omitted current page and pageSize. New nullable Int32 choices independently omit absent values and retain supplied zero, negative or full-width values using invariant formatting. fields independently retains omission, empty and original strings. The server documents zero-based pages, a maximum page size of 5,000 and at most 5,000 entries per response. Clients select each page explicitly. The SDK inserts no defaults, clamps, date filters, local totals or automatic pagination. Passing larger/negative values in isolated forwarding tests does not prove backend acceptance; server validation remains authoritative.

Success has no fixed JSON schema. Full JToken results retain arrays/envelopes/unknown fields and date strings; raw JSON, HTTP status and response/content headers remain available. Parsed fractions have .NET decimal precision and may round, underflow or overflow; raw JSON remains exact. No position shape, completeness/continuation flag or stable snapshot is fabricated.

HTTP 404 can mean a missing selected document or authorization failure. The opportunity error description mentions an offer; this copied text does not justify remapping the selector. The original configured factory sees the response before parsing and retains its exception instance. Native cancellation reaches active HTTP I/O. No request body, account/token selection, financial/stock mutation, local document calculation or retry is introduced.

Twenty-one absent HTTP-info cases reproduced before implementation. Isolated cases exercise all 42 forms, independent paging omission/full Int32 values under a changed culture, documented maximum and above-maximum forwarding, single-request behavior, exact fields/selectors, JSON/raw/status/headers, errors/hooks and pre/active cancellation. Historical null results remain unchanged. Actual number/profile/position/paging acceptance requires a coordinated exclusive server window; publication and downstream adoption remain separate.
