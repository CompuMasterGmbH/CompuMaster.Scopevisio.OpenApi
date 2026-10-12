# Campaign, reportsheet and e-commerce SKU reads

Five additive workflows use the pinned manufacturer routes:

| API group | Workflow | Route |
|---|---|---|
| ProductApi | GetECommerceSkus | GET /product/{id}/ecommerceskus |
| ReportsheetApi | GetReportsheetByNumber | GET /reportsheet/{number} |
| ReportsheetApi | SearchReportsheets | POST /reportsheets |
| CampaignApi | SearchCampaigns | POST /campaigns |
| CampaignApi | SearchCampaignRunEntries | POST /campaign/{campaignId}/campaignRunEntries |

Each provides six sync/data/WithHttpInfo/native async/cancellation forms. New CampaignApi and ReportsheetApi groups follow existing shared configuration, REST-origin and exception-hook conventions. OpenScopeApiClient initializes their additive properties with its existing constructor configuration. Explicitly selected replacement group instances remain possible; existing groups, defaults, authorization and account selection are unchanged.

Product/campaign IDs use invariant Int64 formatting. Reportsheet internal numbers stay strings including leading zeros, whitespace, Unicode and escaped characters; missing/blank and relative path-segment numbers are rejected before HTTP. Optional read fields preserves null omission versus explicit empty CSV.

Searches use the existing extensible SearchRequest, sent once as a JSON object. Null sends an empty object without injecting paging/filter defaults. Unknown fields, explicit JSON null, criterion arrays, operator/value strings, selected fields and caller-selected paging/format/count choices stay unchanged. CountOnly uses the established JSON count field. The vendor request schemas say string while its search documentation describes JSON objects; the immutable snapshot retains that discrepancy. Server pagination limits/count behavior remain authoritative, with no local clamping, iteration or completion inference.

ECommerceSkusResponse retains the complete JSON envelope and exposes read-only copies of ECommerceSkuRecord items. IDs/organisation/e-commerce account use nullable Int64 properties; product/platform numbers retain exact strings and leading zeros. Missing/null records differs from an empty array; non-array records/non-object items and top-level non-object replies fail clearly. Unspecified campaign/reportsheet reply schemas remain complete JToken results.

The product endpoint requires the e-commerce setting for displaying extended product fields. Missing setting/profile diagnostics, including the vendor's HTTP 400 message, remain original server errors. The client does not enable a setting or infer access.

Shared parsing retains unknown fields/nulls, Int64 values, date-like strings, exact raw HTTP replies, status and headers. Fractions use bounded .NET decimal precision and can round or underflow; raw JSON is exact. Exception factories run before parsing with original/custom causes retained. Native cancellation prevents pre-canceled calls and cancels active I/O without an added retry.

Isolated .invalid fixtures reproduce absent workflows and cover HTTP/query/body contracts, facade/configuration/default constructors and hooks, search paging/nulls/unknown fields, SKU copies/schema shapes/leading zeros, diagnostic preservation and cancellation. Coordinated live e-commerce-setting/profile/search/report-format acceptance stays open in #58. No campaign/report/product writes, setting changes, credentials/server resources/full CI/release/downstream import occur.
