# Current outgoing-document XML import

Tracked by [SDK #213](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/213), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`OutgoingInvoiceImportRequest` deeply copies all ten current OutgoingInvoiceImportForm fields: original data/template strings, eight nullable booleans and complete future JSON. It adds current copyVatKeyAndTaxRateToPosition, which the historical model lacks. Historical models, constructor requirements, false defaults and ImportOutgoingInvoices methods remain unchanged. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`OutgoingInvoiceApi.ImportOutgoingInvoicesWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/outgoinginvoices/import`. The primary body is optional: null sends no body. A supplied object requires original data as an XML import string. Missing data, known explicit nulls and incompatible known JSON kinds fail before I/O. Typed null omits fields while constructing a request. Optional template and all booleans remain omitted unless explicitly supplied; future JSON remains copied.

```csharp
var request = new OutgoingInvoiceImportRequest
{
    Data = originalImportXml,
    Template = selectedTemplate,
    DoPost = false,
    CopyVatKeyAndTaxRateToPosition = true
};
var result = await outgoingInvoiceApi.ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

Original XML text, including whitespace, Unicode, quotation marks and empty strings, is transported as JSON data without parsing, Base64 encoding, decoding, domain-format construction, normalization or repair. Original template text is not looked up, trimmed or defaulted. XML/domain/template validity remains server-authoritative; fake wire fixtures do not prove actual financial document acceptance.

Omission remains distinct from false and true for generateDocumentNumbers, doPost, skipDuplicates, createPdf, copyProductToPosition, copyProductToPositionOverwriteMode, copyImpersonalAccountFieldsToPosition and copyVatKeyAndTaxRateToPosition. Explicit options may request server document numbering, posting, duplicate handling, PDF generation, product/account/VAT copying or overwrite behavior. The client performs none of these operations, inserts no defaults and makes no additional requests or calculations. Actual option combinations and effects remain unverified and require test-owned document/product/account/tax/template resources during real tests.

The server requires `Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten)`. HTTP200 has no fixed successful result schema and directs callers to errors in the response. Complete JToken data, exact raw JSON, status and response/content headers remain available without invented invoice IDs, per-row/aggregate success or transaction atomicity. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

The complete request and future nested JSON are deeply snapshotted before active HTTP. No account-context selection, hidden write, retry or mutation replay is added. Cancellation after sending cannot establish which documents, postings or generated files exist or which selected options were applied. Actual tests require owned reversible fixtures and complete cleanup rather than an assumption that Demo access or successful reads grant write permission.

302 isolated fake-host cases cover all ten fields, required/optional bodies, JSON kinds/nulls, original XML/template/future JSON, all eight omitted/false/true flags, copies/snapshots, complete results/errors/factories/native cancellation and legacy models/defaults. Actual Demo XML/document/product/account/tax/template/profile acceptance and owned reversible cleanup remain open, requiring a fresh coordinated window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
