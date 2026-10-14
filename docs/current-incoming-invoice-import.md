# Current incoming-invoice file import

Tracked by [SDK #209](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/209), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`IncomingInvoiceImportRequest` deeply copies current filename/data strings and complete future JSON. `IncomingInvoiceApi.CreateIncomingInvoiceWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/incominginvoice/new`. Historical IncomingInvoiceForm/CreateIncomingInvoice models, constructors, defaults and methods remain unchanged. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data; the new workflow preserves complete JSON data.

The primary body is optional and declares no individually required fields. Null sends no body, while `{}`, filename-only, data-only and empty strings remain original supplied objects. Server404 describes missing filename/data. The client does not turn those diagnostics into invented schema-required fields. Known supplied fields must be strings; typed null omits a field and known explicit raw null fails before I/O. Complete future JSON remains copied. Original text, whitespace, empty strings, filename punctuation and Base64 data remain unchanged without trimming, decoding, format repair, lookup or filename/extension inference.

```csharp
var request = IncomingInvoiceImportRequest.FromBytes(
    originalPdfBytes, selectedFilename);
var result = await incomingInvoiceApi.CreateIncomingInvoiceWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

The byte factory encodes original bytes, including an empty array, into an immutable Base64 snapshot. It performs no filesystem access, text conversion, PDF validation, filename choice or content inference. Supplied PDF structure and filename/data validity remain server-authoritative; fake byte fixtures prove transport preservation, not actual invoice acceptance.

The server requires `Rechnungseingangsbuch (Bearbeiten)` and expects an invoice as a Base64 PDF. Current route description states a maximum30MB, while the data field and404 descriptions state20MB; historical descriptions also state20MB. Exact accepted size and units remain unverified. The client neither promises30MB support nor imposes a guessed threshold or silently modifies files. Real size/PDF/profile acceptance needs controlled small owned fixtures first and explicit server evidence before drawing conclusions about limits.

HTTP200 has no fixed successful response schema. All new methods retain complete JToken data, exact raw JSON, status and response/content headers without invented invoice IDs, aggregate success or server-validation claims. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures, including server404 filename/data/size diagnostics.

The complete request and nested future JSON are deeply snapshotted before active HTTP. No account-context selection, hidden write, retry, mutation replay or unrelated attachment operation is added. Cancellation after sending cannot establish whether an invoice was imported. The import is distinct from journal/outgoing-invoice attachment and TeamworkFileForm bridge routes.

95 isolated fake-host cases cover both fields, optional body/omissions/empty strings/kinds/nulls, original filename/Base64, every byte/empty arrays, copied future JSON/snapshots, full results/status/headers/errors/factories/native cancellation and legacy models/defaults. Actual Demo PDF/invoice/profile/size acceptance and complete owned reversible cleanup remain open, requiring a fresh coordinated window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
