# Complete bodyless sales-document conversion results

Parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17), focused [#162](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/162), consumer [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

Twenty-one existing bodyless conversions now have additive six-form ...WithResult workflows, retaining old ConvertInvoice terminology and all historical generated methods. Each original internal number remains a string escaped as one segment without trimming, case/numeric conversion, leading-zero removal or lookup. Null/blank/exact-dot selections fail before sending.

| API/source | Explicit targets in this bodyless batch | Current required profile |
|---|---|---|
| OfferApi /offer/{number} | Dispatch, Opportunity, Order, OutgoingInvoice | Angebote (Bearbeiten) |
| OpportunityApi /opportunity/{number} | Dispatch, Offer, Order, OutgoingInvoice | Rechnungen, Potenzial (Opportunities) (Bearbeiten) |
| OrderApi /order/{number} | Dispatch, Offer, Opportunity, OutgoingInvoice | Aufträge (Bearbeiten) |
| OutgoingInvoiceApi /outgoinginvoice/{number} | Dispatch, Offer, Opportunity, Order | Rechnungen (Bearbeiten) |
| RecurringInvoiceApi /recurringinvoice/{number} | Dispatch, Offer, Opportunity, Order, OutgoingInvoice | Rechnungen (Bearbeiten) |

Every route is the case-sensitive original POST source path plus /convertToTarget, where Target is the table's exact target spelling. The pinned operations declare no request body or query. No target defaults, local document calculations, permission checks, account selection, body schema, additional flag or automatic follow-up/retry is invented. The four conversions TO recurring invoices declare an optional body and remain a separate batch. Parent/file document-number selectors are not interchangeable with these internal source numbers.

HTTP200 declares no fixed response schema. JToken retains arbitrary arrays, envelopes, unknown fields, original date strings, scalar results and JSON null; empty content returns null. No resulting identity/hierarchy/completeness is inferred. HTTP-info retains original raw JSON, status and response/content headers. Parsed decimal fractions have bounded .NET precision; raw JSON stays exact. Copied recurring-to-outgoing success prose names a recurring invoice despite the explicit outgoing route; the SDK does not select another target or fabricate a response model from that text.

Documented404 can mean missing source or write permission. Original configured factories run before parsing under the matching new operation name and preserve their returned exception instance. Native cancellation prevents pre-canceled sending and reaches active I/O. Cancellation after admission does not establish the server outcome; no conversion is replayed. Old void/null responses, defaults and token/account/transport behavior remain unchanged.

Initial tests reproduce1722missing forms while21historical regressions pass. Final1743cases cover all126forms, original single-segment selections, bodyless/query-free transmission, arbitrary/scalar/null/empty/raw/header results, original errors/factories, admission guards, active/pre-cancellation and preserved state/defaults. The fixture uses only fake-host handlers; no financial document is converted on a real server. Actual source/target state, number/profile/permission and response behavior require coordinated resettable-account acceptance. Full actual matrix, independent review/merge, publication and exact DMS adoption remain separate.

