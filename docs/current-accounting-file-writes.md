# Current journal and outgoing-invoice attachments

Tracked by [SDK #195](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/195), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`FileWriteRequest` exposes the two current FileForm fields, Filename and Data, with complete deeply copied future JSON. A supplied object requires an original data string. Typed null setters omit fields; known explicit nulls or incompatible kinds fail before sending. Existing FileForm constructors, required-data behavior, serializers and generated APIs remain unchanged.

`AccountingApi.AddJournalFileWithResult` sends POST `/journal/{documentNumber}/file/new`. `OutgoingInvoiceApi.AddFileToOutgoingInvoiceWithResult` sends POST `/outgoinginvoice/{number}/file/new`. Each has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Both route bodies are optional: null sends no body, while a supplied empty object fails because it lacks data. An explicit empty data string remains a caller value whose actual acceptance is server-authoritative.

Document numbers retain their original string text and are escaped as one segment without trimming, parsing, leading-zero removal, lookup or conversion into internal IDs. Null and relative-dot selectors fail before I/O. Other strings remain server decisions. No whole document is read or default record selected before adding a file.

Raw Data text, including Base64 whitespace or opaque/incomplete values, is not repaired, decoded or re-encoded; the server validates actual file content. The manufacturer's example is truncated and contains formatting text, so it is not treated as an executable valid-PDF fixture. Filename is optional, and the server assumes PDF when omitted. The client does not invent a PDF filename, extension, content type or content conversion.

```csharp
var request = FileWriteRequest.FromBytes(originalBytes, originalFilename);
var result = await accountingApi.AddJournalFileWithResultAsyncWithHttpInfo(
    selectedJournalDocumentNumber, request, cancellationToken);
```

`FromBytes` copies the original byte array into an immutable Base64 string without file-system access or text conversion. Later caller-array changes do not change the request; all byte values and explicitly empty arrays remain available. Null byte arrays fail clearly. A null filename remains omitted. The complete request and nested future JSON are also deeply snapshotted before active HTTP.

HTTP200 has no fixed successful schema for either route. Complete JToken data, exact raw JSON, status and response/content headers remain available without inventing an attachment ID or success flag. Empty content gives null Data; explicit JSON null remains a token. Date-looking strings stay strings and parsed fractions use bounded .NET decimal precision while raw responses remain exact. The configured exception factory runs before parsing and preserves original errors. No account selection, authentication change, lookup, chained write or mutation replay is added; cancellation after sending cannot establish whether a file was added.

The server requires `Journal (Bearbeiten)` or `Rechnungen (Bearbeiten)` respectively. These are the regular FileForm routes. Contact/project/task attachment routes use TeamworkFileForm with separate collection/create-collection fields and explicitly documented Teamwork access; that different family is not inferred from these methods or from Teamwork web visibility.

233 isolated fake-host cases verify both routes/all six forms, both fields, all byte values and empty arrays, copy/snapshot behavior, original Base64/filename/document strings, optional/required-object distinctions, null/kind/selector validation, complete results/raw metadata, errors/factories/native cancellation and unchanged legacy APIs. Actual Demo journal/invoice/content/filename/profile acceptance and complete owned cleanup need a new coordinated reversible fixture with exact numeric Scope organisation proof and no PROD fallback. Source merge, separately coordinated publication and pinned downstream adoption remain distinct. This source is excluded from delivered CM.Dms2026.10.14, which retains published SDK2026.10.13; that release waiver does not authorize SDK live tests or publication.
