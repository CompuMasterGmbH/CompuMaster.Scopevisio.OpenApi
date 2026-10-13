# Complete product-group, sales-template and opportunity reads

Issue #134 contributes to #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Three additive JSON workflows retain all historical generated methods and null-data defaults.

| Workflow | Current GET route | Required profile |
| --- | --- | --- |
| GetProductGroupsDetails | /productGroups | Produktverwaltung (Anzeigen) |
| GetOutgoingInvoiceExportTemplatesDetails | /outgoinginvoice/exporttemplates | Faktura Stammdaten, Opportunities (Anzeigen) |
| GetOpportunityDetails | /opportunity/{number} | Rechnungen, Potenzial (Opportunities) (Anzeigen) |

Each has six synchronous, HTTP-info, asynchronous and native cancellable asynchronous forms. The two catalogs have no current query parameters and send no body. They expose the original server inventory without sorting, grouping, associations, default templates, pagination, document generation or inferred permissions.

The opportunity route takes the original internal number (interne Nummer) and optional fields. Null fields are omitted; empty and original selections remain intact. The number is escaped as one segment without trimming, numeric parsing, zero removal or mapping to another identifier. Null/blank/dot selections fail before HTTP. Copied path-parameter prose mentions an outgoing invoice although the summary/result describes an opportunity; the SDK forwards the explicit caller selection and leaves resolution to the server.

Success schemas and exhaustive error tables are undefined. Complete JToken results retain arbitrary JSON shapes, unknown fields and date strings; HTTP-info retains exact raw JSON/status/response and content headers. Parsed fractional values have .NET decimal precision and may round, underflow or overflow; raw JSON remains exact. No fixed product-group/template/opportunity model is fabricated. The exception factory sees the original response before parsing and preserves its exception instance. Original error status/body remain available.

Native cancellation reaches active HTTP I/O. Authorization/token renewal and account selection are unchanged. No product/document mutation, report generation, fiscal configuration, default catalog lookup or retry is introduced.

Nine missing HTTP-info forms reproduced before implementation; 84 isolated cases cover all 18 forms, bodyless/query-free catalogs, exact opportunity/fields selections, unknown result shapes, raw headers/status, errors/hooks, pre/active cancellation and preserved legacy null data. Actual inventory/profile and opportunity number resolution require coordinated server acceptance; review/merge/publication and downstream adoption remain separate.
