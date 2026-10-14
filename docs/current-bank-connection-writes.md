# Current bank-connection writes

Tracked by [SDK #201](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/201), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`BankConnectionWriteRequest` exposes all eight current BankConnectionForm fields: number, bankCode, iban, swiftBic, bankName, accountHolder, standard and description. It deeply copies complete future JSON. The historical seven-field model lacks description and inserts standard false; its constructor, fields, defaults and Object results remain unchanged. The historical `CreateKebitorBankConnection` spelling remains available. The new additive workflow uses `CreateKreditorBankConnectionWithResult`.

`AccountingApi.CreateDebitorBankConnectionWithResult` and `CreateKreditorBankConnectionWithResult` each provide six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/debitoraccounts/{accountNumber}/bankConnection/new` and `/kreditoraccounts/{accountNumber}/bankConnection/new`. Original account-number strings are escaped as one path segment without trimming, numeric conversion, zero stripping, contact-ID conversion or lookup. Null and dot segments fail before I/O; other strings remain server decisions.

Both route bodies are optional: null sends no body and an empty object sends `{}`. No individual fields are schema-required. The manufacturer describes server400 for missing IBAN or bank account number plus bank code. The client preserves supplied original strings without inventing a required-field/XOR rule, banking validation, checksum, formatting repair or lookup. Known supplied creation fields must have their declared JSON kinds; typed null omits a field and a raw null for a known field fails before I/O. Unknown future JSON remains copied.

```csharp
var request = new BankConnectionWriteRequest
{
    Iban = selectedIban,
    Description = selectedDescription,
    Standard = false
};
var result = await accountingApi.CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(
    selectedAccountNumber, request, cancellationToken);
```

Omitted standard, false and true remain distinct without inserting false. Standard may select the server default bank connection for transfers or withdrawals. The client initiates no payment, account-context selection, additional write or mutation replay. Actual bank/account/default-selection behavior remains server-authoritative and unverified. Cancellation after sending cannot establish whether the connection was added or selected.

The manufacturer declares HTTP201 without a fixed successful result schema and requires respective `Stammdaten, Debitorenstamm (Bearbeiten)` or `Stammdaten, Kreditorenstamm (Bearbeiten)` profiles. All new methods retain complete JToken data, exact raw JSON, status and response/content headers. Empty content gives null Data, explicit JSON null stays a token, date-like text stays a string, and parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and retains original failures. Requests and future nested JSON are deeply snapshotted before HTTP I/O.

244 isolated fake-host cases cover all eight fields, both routes/all six forms, optional-body distinctions, original banking/account strings, omission/false/true, JSON kinds/nulls, deep copying and active snapshots, escaped selectors, full responses/status/headers/errors/factories/cancellation and unchanged legacy model/Object results. Actual Demo bank/account/standard/profile acceptance and complete owned cleanup require a fresh coordinated reversible fixture with exact numeric Scope organisation proof and no PROD fallback. Source review/merge, separate SDK publication and exact consumer adoption remain distinct. This source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin; that release waiver does not authorize SDK tests or publication.
