# Complete invoice Teamwork file-link responses

Issue #136 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. GetIncomingInvoiceTeamworkFileLinkResponse and GetOutgoingInvoiceTeamworkFileLinkResponse add six compatible synchronous/HTTP-info/asynchronous/native-cancellation forms each alongside unchanged generated methods and null-data defaults.

| Workflow | GET route | Original selection | Manufacturer link meaning/profile |
| --- | --- | --- | --- |
| Incoming response | /incominginvoice/{idOrNumber}/teamworkFileLink | Incoming invoice ID or internal number | Main file; Rechnungseingangsbuch (Anzeigen) |
| Outgoing response | /outgoinginvoice/{number}/teamworkFileLink | Outgoing invoice document number | Last file; Rechnungen (Anzeigen) |

Original selectors are escaped independently without trimming, numeric parsing, zero removal or lookup. Blank/dot selections fail before HTTP. Requests have no body or query. The vendor says the resulting link targets the Teamwork web application, where a separately logged-in session is required; SDK authorization does not log the browser into that application. Incoming 404 prose incorrectly mentions an outgoing invoice; the original diagnostic remains unchanged. A 404 can indicate a missing invoice, authorization or file.

The current contract supplies no fixed success schema/content representation. Data therefore retains the complete response text decoded by the existing transport. Plain URLs, JSON strings/objects, HTML, empty text and unknown future formats are not parsed, trimmed, normalized or interpreted as a specific URI. RawJsonContent is null because JSON representation is not established. HTTP status and original combined response/content headers remain available. No byte-identity, MIME inference, URL extraction or successful web-app navigation is claimed; transport decoding limitations remain applicable.

Accept is */* with copied authorization/default headers. The original configured factory sees the response before additive access and its exception instance remains intact. Native cancellation reaches active HTTP I/O. No link is opened by these methods, no associated file is downloaded and no browser login/account/token selection or mutation/retry is added. Historical APIs and authentication behavior remain unchanged.

Ninety-six missing response-form cases reproduced before implementation. The 155 isolated cases cover all 12 entry points with complete UTF-8/whitespace/plain/JSON/HTML/empty responses (including JSON decimals never parsed), exact original selectors/bodyless/query-free requests, status/content headers, error/hook identity, pre/active cancellation for both native forms and all four historical HTTP-info defaults. Actual main/last-file resolution, response representation, account/profile and web-app behavior require coordinated server acceptance; review/merge, publication and downstream adoption remain separate.
