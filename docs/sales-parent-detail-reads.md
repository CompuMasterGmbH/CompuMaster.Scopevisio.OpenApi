# Complete selected sales-document parent reads

Issue #130 contributes to parent #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Five additive JSON parent workflows retain all historical generated methods and their null-data behavior.

| Workflow | Current GET route | Required profile |
| --- | --- | --- |
| GetOfferParentDetails | /offer/{number}/parent | Angebote (Anzeigen) |
| GetOrderParentDetails | /order/{number}/parent | Aufträge (Anzeigen) |
| GetDispatchParentDetails | /dispatch/{number}/parent | Lieferscheine (Anzeigen) |
| GetOutgoingInvoiceParentDetails | /outgoinginvoice/{number}/parent | Rechnungen (Anzeigen) |
| GetCreditParentDetails | /credit/{number}/parent | Rechnungen (Anzeigen) |

Each workflow exposes synchronous, HTTP-info, asynchronous and native cancellable asynchronous forms. Unlike the detail routes' internal-number terminology, the current parent contracts explicitly describe a document number. The original string is escaped as one segment without trimming, numeric parsing, leading-zero removal or translation. No lookup, account selection or automatic parent traversal is performed. Blank and dot-segment selectors fail before HTTP.

The only current query choice is fields. Null omits it; empty and original strings remain intact. No body, formatting/paging flags, search or default selection is added. HTTP 404 is ambiguous: the selected document can be missing, authorization absent or a parent unavailable. Its original status/body is preserved instead of becoming a null result or guessed permission decision.

Success has no fixed schema. Full JToken results retain arbitrary response shapes, unknown fields and original date strings. HTTP-info retains exact raw JSON and response/content headers. Parsed fractions have .NET decimal precision and may round, underflow or overflow; raw JSON remains exact. Manufacturer prose repeatedly calls the parent an outgoing invoice, and the order parameter/error text mentions an offer. The client documents these copied descriptions without inventing a parent-type discriminator, resolving identifiers or interpreting document hierarchy locally.

The configured exception factory sees the original response before parsing and its exception instance remains intact. Native cancellation reaches active HTTP I/O. Authentication/token renewal, historical APIs/defaults and mutation behavior remain unchanged.

Fifteen absent HTTP-info cases reproduced before implementation. The 176 new isolated cases cover all 30 entry points, exact document-number and fields transmission, JSON/raw/status/header preservation, original errors and hooks, pre/active cancellation and historical null data. Actual relationships, selected number resolution and role/profile behavior require separate coordinated server acceptance. Package publication and downstream adoption remain separate.

