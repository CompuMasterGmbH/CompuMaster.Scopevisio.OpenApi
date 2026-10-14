# Current JSON posting import

Tracked by [SDK #207](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/207), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`PostingImportRequest` exposes current adjustVatKey, autoCreateTax and copied rows. `PostingImportRowRequest` exposes all twenty-four current PostingRow fields: thirteen original strings, five nullable signed Int64 values, five nullable decimal values and copied dimensions. Complete future JSON remains available. New rows include current ustId and correctly spelled netTimeLimit/discountAccount; historical missing fields and nettimeLimit/disountAccount spellings remain unchanged. Legacy Postings/PostingRow, constructors, zero/false defaults, CreatePostings methods and correction-import APIs remain available with their previous behavior. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`AccountingApi.ImportPostingsWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/postings/new`. The primary body and rows are optional. Null sends no body; an empty object, omitted/null rows and explicit empty arrays remain distinct. Supplied rows require original postingDate, documentNumber, account and amount. The existing checked correction-import validation is reused against the same current row schema without changing correction behavior. Known fields validate JSON kinds, Int64 widths and finite numbers; required nulls fail before I/O while optional explicit raw nulls remain available. Their actual behavior remains server-authoritative. Typed null omits fields.

```csharp
var first = new PostingImportRowRequest
{
    PostingDate = selectedPostingMilliseconds,
    DocumentNumber = selectedDocumentNumber,
    Account = selectedFirstAccount,
    Amount = selectedFirstAmount
};
var second = new PostingImportRowRequest
{
    PostingDate = selectedPostingMilliseconds,
    DocumentNumber = selectedDocumentNumber,
    Account = selectedSecondAccount,
    Amount = selectedSecondAmount
};
var request = new PostingImportRequest { Rows = new JArray(first, second) };
var result = await accountingApi.ImportPostingsWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

The manufacturer requires two or more rows for a posting and a summary account when an account is personal. The client does not infer account types, look up summary accounts, balance amounts, synthesize rows or declare a one-row fixture to be a valid server posting. Row counts, chart, personal-account/summary selection, date ranges, currencies, tax/payment choices and financial validity remain server-enforced. Manufacturer examples and allowable values reference SKR03; other charts require checking actual client settings. Original strings, including whitespace and leading zeros, remain unchanged without normalization or lookup.

Original signed Int64 epoch milliseconds and payment periods are retained without date/timezone conversion, inserted dates or local minimum/ordering rules. Nullable numeric fields preserve omission and explicitly supplied zero, negative and fractional values without rates, discount, tax, balancing or currency calculations. Typed decimals have bounded .NET precision; inherited JSON can preserve other finite numeric values already represented by the supplied JToken. Dimensions retain original separate dimensionId/dimensionAccountNumber strings, sparse objects, optional nulls, empty arrays and future JSON without choosing or creating dimensions. Arrays are deeply copied on assignment/read and the complete body is snapshotted before active HTTP. Row order and independent choices remain unchanged.

Omitted adjustVatKey/autoCreateTax remain distinct from false and true. These explicit options can ask the server to replace supplied VAT keys using chart settings or generate tax posting rows. The client performs neither operation. Original cancelDocument is retained as a server-evaluated cancellation reference without cancellation lookup or additional requests. These server effects require explicit ownership and reversible cleanup in actual tests.

The route requires `Datenimport (Bearbeiten)` and declares HTTP200 with PostingsNewResponse: optional Int32 status and original insertCount/updateCount strings. The complete JSON and future data are retained without converting count strings, inventing per-row/aggregate success, unconditional new creation or transaction atomicity. Response error information remains available even with successful HTTP status. Empty content gives null Data, explicit JSON null stays a token and date-like text stays a string. Raw JSON remains exact while parsed fractions use bounded decimal precision. Status and original response/content headers remain available; the configured exception factory runs before parsing and retains original failures.

No account-context selection, hidden writes, retries or mutation replay is added. Cancellation after sending cannot establish resulting posting/tax/cancellation state. 551 isolated fake-host cases cover all twenty-seven wrapper/row fields, dimensions, required/optional/null contracts, kinds/widths/nonfinite values, originals, flags, copies/snapshots/order, declared count strings/future results, errors/factories/native cancellation and legacy names/defaults. Actual Demo posting/chart/tax/personal-summary/dimension/profile acceptance and complete owned reversible cleanup remain open, requiring a fresh coordinated window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
