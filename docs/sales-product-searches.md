# Complete sales and product searches

Eight additive searches provide six sync/HTTP-info/async/native-cancellation forms each:

| API | POST route | Required server view profile |
|---|---|---|
| OfferApi.SearchOffers | /offers | Angebote |
| OrderApi.SearchOrders | /orders | Aufträge |
| DispatchApi.SearchDispatches | /dispatches | Lieferscheine |
| OutgoingInvoiceApi.SearchOutgoingInvoices | /outgoinginvoices | Rechnungen |
| CreditApi.SearchCredits | /credits | Rechnungen |
| RecurringInvoiceApi.SearchRecurringInvoices | /recurringinvoices | Rechnungen |
| OpportunityApi.SearchOpportunities | /opportunities | Angebote, Aufträge, Lieferscheine, Rechnungen |
| ProductApi.SearchProducts | /products | Produktverwaltung |

These POSTs search records; they do not create, post or change financial documents/products. Existing generated Get methods, quoted string-body serialization, null-data defaults, models and configuration/token/account behavior are unchanged.

All routes accept the existing extensible SearchRequest. The pinned search-body schema says string, while its prose links [manufacturer search documentation](https://help.scopevisio.com/de/articles/467360-search-scope-documentation) defining JSON objects. A null request omits the body; an empty object sends {}. Original fields, criteria/operators, values/types/explicit nulls, unknown filters, count/paging/format/order selections serialize before HTTP admission. No defaults, clamps, criteria rewriting or page loops are inserted. Count requests retain original fields even when the server ignores paging/order/fields.

OutgoingInvoiceSearchRequest adds a nullable IncludeIndividualNumberRanges convenience property. It belongs in the JSON body; null omits it and leaves the documented false server default. Explicit false/true remain unchanged, and other search/future fields are copied. It does not select a number range or account.

Product listExtended is an independent nullable query choice. Null omits it and retains the false server default; false/true remain explicit. The declared ProductResponse schema lists available product fields, but effective fields depend on that choice and existing e-commerce settings. Complete JSON is retained without changing settings, inferring fields or manufacturing SKU results; the distinct E-Commerce SKUs endpoint remains separate.

Seven sales success schemas are unspecified. The copied recurring-invoice response description says outgoing invoices, while the route/summary describe recurring invoices; no outgoing-invoice model is inferred. Complete JToken/raw/status/combined headers retain future data, arrays, count scalars and explicit JSON null tokens; empty responses have null Data. Date text remains strings. Fractions use bounded .NET decimal precision/range and can round; raw response JSON remains exact. Original factories/errors run before parsing and preserve their exceptions; native cancellation reaches active HTTP I/O. No permission inference, account lookup, mutation or new retry is added.

545 isolated fake-host cases cover every route/form, original optional/full/empty searches, pre-admission serialization, full-width/invariant values, complete date/decimal/raw metadata, count/unknown/null/empty results, malformed/overflow JSON, original factories/errors, product query omission/false/true independent of body, invoice body flag choices/copied convenience properties, native pre/active cancellation and eight preserved legacy methods. Before implementation 537 missing-workflow/model failures were reproduced while all eight legacy regressions passed. Actual records/profiles/count/number-range/extended-field acceptance needs a coordinated resettable test account; source presence and isolated tests do not establish full API support, complete required matrix, publication or downstream adoption.
