# Current impersonal-account import

Tracked by [SDK #203](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/203), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`ImpersonalAccountImportRequest` deeply copies the optional rows array and future JSON. `ImpersonalAccountImportRowRequest` exposes all thirteen current ImpersonalAccountRow fields: seven original strings, five nullable booleans and a copied dimensions array. The historical models, enum choices, required constructor fields, false defaults and misspelled `CreateImpersonalAcocunts` methods remain unchanged. The old data-only methods discard data and their HTTP-info forms retain raw/status/headers with null Data; the additive workflow preserves complete result data.

`AccountingApi.ImportImpersonalAccountsWithResult` provides six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/createimpersonalaccounts`. Body and rows are optional: null sends no body, an empty object sends `{}`, and explicit empty rows remain `[]`. Each supplied row requires original number, name and type strings. Other declared fields require their JSON kinds when supplied; typed null omits a field while known raw null fails before I/O. Unknown future JSON remains copied. Original strings, including empty text, whitespace, leading zeros and configured chart/tax selections, are preserved without lookup or normalization; their actual content remains server-authoritative.

```csharp
var row = new ImpersonalAccountImportRowRequest
{
    Number = selectedAccountNumber,
    Name = selectedName,
    Type = selectedAccountType,
    VatCode = selectedVatCode,
    DirectPosting = false
};
var request = new ImpersonalAccountImportRequest { Rows = new JArray(row) };
var result = await accountingApi.ImportImpersonalAccountsWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

No account, category, VAT code/key, discount account, posting/fixed-tax/deactivation/finance/clearing flag or dimension default is inserted. Omitted flags remain distinct from false and true. The manufacturer states examples and allowable values refer to SKR03 and directs users of other charts to check actual Scopevisio client settings. Configured strings remain available without being restricted to historical enum examples; this does not prove arbitrary selections are valid. There are no account/chart/tax/dimension lookups, financial calculations, posting operations or extra writes in the client.

Dimensions preserve separate original dimensionId and dimensionAccountNumber strings, sparse objects, empty arrays and future data. Supplied dimension rows must be objects and their two known fields must be strings when present. No dimension is selected or created by the client. Arrays are deeply copied on assignment and reading; the entire body, rows and future data are snapshotted before active HTTP. Row order and independent per-row choices remain unchanged.

The route requires `Datenimport (Bearbeiten)` and declares HTTP200 as created or updated without a fixed successful response schema. The client preserves status and complete JSON without inventing a created flag, per-row success or aggregate success. Bulk import may update existing accounts. Actual tests require verified ownership and reversible restoration; a Demo name or successful read is not an import/profile guarantee. Cancellation after sending cannot establish which accounts were created or updated, and no retry, mutation replay or account-context selection is added.

All new methods retain complete JToken data, exact raw JSON and response/content headers. Empty content gives null Data, explicit JSON null stays a token, date-like text stays a string, and parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original failures.

335 isolated fake-host cases cover all row and dimension fields, required/kind/null contracts, optional bodies/rows, original chart values, flags, snapshots/copies, multiple-row order, full results/errors/factories/native cancellation and legacy spelling/defaults. Actual Demo chart/tax/account/dimension/profile acceptance and complete owned cleanup/restoration remain open and require a fresh coordinated reversible window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct; this source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
