# Current debitor and kreditor account writes

Tracked by [SDK #199](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/199), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`PersonalAccountCreationRequest` exposes all 17 current PersonalAccountForm fields: twelve original strings, three Int64 values, one copied payment object and one boolean. `PersonalAccountUpdateRequest` exposes all twelve current UpdatePersonalAccountForm fields: nine strings, two Int64 values and one copied payment object. Complete future JSON is deeply copied. Historical creation lacks ExternalNumber/SumAccountNumber/ContoProDiverse; historical update lacks ExternalNumber/SumAccountNumber. Existing models, enum choices, constructors, non-nullable defaults and methods remain unchanged.

`AccountingApi.CreateDebitorWithResult` and `CreateKreditorWithResult` send POST `/createdebitor` and `/createkreditor`. Each has six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Both route bodies are optional: null sends no body. A supplied object requires at least one original ContactId, CustomerNumber or Email, as described by the manufacturer. Multiple selectors are sent unchanged without an invented XOR rule, precedence, lookup or conversion. Their actual validity and resolution remain server-authoritative. The requested PersonalAccountNumber is distinct from a contact selector and is never used to find a contact.

`UpdateDebitorWithResult` and `UpdateKreditorWithResult` send POST `/debitoraccounts/{accountNumber}` and `/kreditoraccounts/{accountNumber}` with required bodies. Their six forms preserve a supplied empty object or sparse fields; typed null omits a field and raw explicit update null remains available under the [general update documentation](https://help.scopevisio.com/en/articles/467359-general-documentation). Whether particular null/empty fields clear or are ignored remains server-authoritative. The original account-number string is escaped as one path segment without trimming, integer parsing, leading-zero removal, contact-ID conversion or lookup. Null and dot segments fail before I/O; other strings remain server decisions.

```csharp
var request = new PersonalAccountCreationRequest
{
    ContactId = selectedContactId,
    PersonalAccountNumber = selectedAccountNumber,
    NumberRangeNumber = selectedNumberRange,
    SumAccountNumber = selectedSummaryAccount,
    VatCode = selectedVatCatalogueValue
};
var result = await accountingApi.CreateDebitorWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

No contact, account number, summary account, range, group, VAT code, currency, language, payment type, payment term or boolean default is inserted. The manufacturer documents server-selected ranges and summary accounts, generated account numbers and default debitor/kreditor ranges; the client preserves omission and original selections. VAT accepts additional configured matrix values instead of being limited to historical enum examples. Payment/language strings, comma-containing payment values, tax identifiers and currency text are not split, repaired, uppercased or calculated locally. Int64 fields reject fractional/string coercion and width overflow without deciding their business range. ContoProDiverse preserves explicit false versus omission; the manufacturer's copied vatNumber description does not establish tax-number semantics for this boolean.

The server may create a missing supplied group and assign it. PaymentTermId and copied PaymentTermForm may both be provided: the server first looks for the ID, then may use the form, match its name or create a new term. The client retains both without lookup, XOR validation, form removal, schedule calculation or a second request. A supplied form requires name and validates current day/finite-percentage types through the reviewed PaymentTermWriteRequest contract. These documented nested server effects require separate ownership and cleanup during actual tests.

Creation HTTP200 is not unconditional new creation. Debitor200 may return the existing personal-account number; kreditor200 may mean an existing account or no free numbers in a range. HTTP201 indicates the declared creation branch. Both statuses have unspecified result schemas, so the SDK preserves status and complete original JSON without inventing a created flag, new account ID or aggregate success. Update200 is also unspecified. All methods retain JToken data, exact raw JSON and response/content headers; empty content gives null Data, explicit JSON null stays a token, date-like text stays a string, and parsed fractions use bounded decimal precision while raw JSON remains exact.

The configured exception factory runs before parsing and preserves original failures. Requests, nested payment/future data and arrays are deeply snapshotted before active HTTP. No account-context selection, hidden record/group/payment lookup, extra write or mutation replay is added. Cancellation after sending cannot establish the resulting account or nested-resource state.

Creation requires `Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten)`; updates require `Stammdaten, Debitorenstamm (Bearbeiten)` or `Stammdaten, Kreditorenstamm (Bearbeiten)`. Actual contact/account/range/catalogue/payment/group permission and behavior remain server-validated; read or Teamwork access is not a write guarantee.

690 isolated fake-host cases cover all 29 fields, four routes/all six forms, body distinctions, at-least-one and multiple selectors, current missing fields, sparse/null updates, Int64 widths, original catalogues/account strings, nested payment validation/snapshots, distinct200/201 results, original factories/errors/native cancellation and unchanged legacy APIs. Actual Demo contact/account/range/group/payment/profile acceptance and complete owned cleanup require a fresh exclusive reversible fixture with exact numeric Scope organisation proof and ownership of nested server effects. Source merge, separately coordinated SDK publication and exact consumer adoption remain distinct; this family is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin, and that release waiver does not authorize SDK tests or publication.
