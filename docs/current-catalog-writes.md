# Current payment-term and dimension-entry writes

Tracked by [SDK #193](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/193), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`PaymentTermWriteRequest` exposes all seven current PaymentTermForm fields through optional typed access: Name/Description strings, three Int64 day counts and two decimal percentages. Complete copied JSON retains future fields without inserting zero days, percentages or a payment schedule. Existing PaymentTermForm constructors/defaults and APIs remain unchanged; historical payment creation already retains Object results and HTTP metadata, which are preserved.

`AccountingApi.CreatePaymentTermWithResult` sends POST `/paymentterm/new`, while `UpdatePaymentTermWithResult` sends POST `/paymentterm/{id}` with the original signed Int64 ID in invariant text. Both route contracts omit requestBody.required: null sends no body. A supplied creation object requires the original name string. Updates accept sparse fields under the [general update documentation](https://help.scopevisio.com/en/articles/467359-general-documentation), preserving omitted values and explicit raw null without promising that every field can be cleared. Empty objects remain distinct from absent bodies, and empty/whitespace strings are not normalized.

The primary schema requires only name, but both payment error descriptions additionally mention missing netTimeLimit. They also describe uniqueness, day/percentage ordering and percentage bounds. The client does not invent additional required fields, select a missing net limit, calculate discounts or schedules, clamp percentages, or repair copied error wording. Actual endpoint-specific sparse-update acceptance remains unverified; original server errors are retained.

Typed percentages use nullable decimal with bounded .NET precision. Raw JSON numeric values supplied through the indexer may retain additional magnitude without conversion to decimal, rounding or calculation. Known percentages require finite numeric JSON and reject strings, NaN, infinities and incompatible kinds before I/O. Day fields preserve original Int64 width without fractional/string coercion. Semantic bounds and schedule relationships remain server-authoritative.

`DimensionEntryWriteRequest` exposes all four current DimensionEntryForm fields: original Number/Name, explicit Locked and optional LegacyId. The historical generated form has only Number and Name, so existing constructors and serialization are retained while the new fields become available separately. The client never converts LegacyId into an entry number or performs a lookup.

`AddDimensionEntryWithResult` sends POST `/dimensions/{dimensionNameOrNumber}/dimensionentry/new`, retaining the existing Add terminology. Creation requires a body with explicit Int64 number, original name string and explicitly chosen lock boolean. False is distinct from omission; the client does not default the lock state. `UpdateDimensionEntryWithResult` sends POST `/dimensions/{dimensionNameOrNumber}/dimensionentry` with a required body and explicit number/name: the entry number selects the existing entry within that dimension, and the primary operation describes changing its name.

```csharp
var request = new DimensionEntryWriteRequest
{
    Number = selectedEntryNumber,
    Name = originalEntryName,
    Locked = selectedLockState
};
var result = await accountingApi.AddDimensionEntryWithResultAsyncWithHttpInfo(
    selectedDimensionNameOrNumber, request, cancellationToken);
```

The shared dimension schema requires locked as well as number/name, but the name-update summary and errors specify a rename selected by number/name. The update request retains omitted lock state under general sparse-update rules instead of inserting false. Explicit raw update null for optional lock/legacy fields remains available; number/name cannot be omitted because they select the entry and rename. Actual effects or acceptance of additional locked/legacyId fields are unproved and server-authoritative; this API does not claim a verified lock-update capability.

The original dimensionNameOrNumber string is escaped as one path segment without trimming, number parsing, leading-zero removal, name-to-ID conversion or lookup. Null and relative dot segments fail before I/O. Other strings, including empty text, remain server decisions. Explicit entry number and payment IDs retain signed Int64 width without selecting default records.

All four workflows have six synchronous, HTTP-info, asynchronous and native CancellationToken forms. Creation201/update200 have no fixed success schemas. Complete JToken data, exact raw JSON, status and response/content headers remain available without inventing identifiers or success flags. Empty content gives null Data; explicit JSON null stays a token. Date-looking strings remain strings and parsed fractions use bounded decimal precision while raw responses remain exact. The configured exception factory runs before parsing and preserves original failures.

Both request constructors deep-copy complete JSON. The complete request, including future nested data, is snapshotted before active HTTP. No account selection, authentication change, record or dimension lookup, local merge, extra mutation or replay is added. Cancellation after sending cannot establish whether the server applied the write. Profiles are `Stammdaten, Zahlungsbedingungen (Bearbeiten)` for payment terms and `Berichte (Bearbeiten)` for dimension entries; read or Teamwork web visibility does not establish write permission.

525 isolated fake-host cases cover all eleven fields, four routes and all six forms, optional/required bodies, supplied fields/nulls, day/ID widths, raw/finite percentages, explicit lock omission/false, original escaped selectors, copied snapshots/future JSON, complete results/raw metadata, original factories/errors/native cancellation and preserved legacy APIs/Object creation results. Actual Demo catalogue/schedule/percentage/rename/lock/permission behavior and complete owned cleanup need a fresh coordinated reversible fixture with exact numeric Scope organisation proof and no PROD fallback. Source merge, separately coordinated publication and exact consumer adoption remain distinct; this family is excluded from the chosen CM.Dms2026.10.14 release, which retains published SDK2026.10.13.
