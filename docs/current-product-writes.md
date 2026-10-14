# Current product and product-group writes

Tracked by [SDK #183](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/183), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`ProductWriteRequest` exposes the 26 writable properties of the pinned current ProductForm contract as optional typed values and retains complete copied JSON, including future fields. The twenty-seventh property, type, is read-only and derived by the server from typeDisplay. Supplying raw `type`, including null, is rejected before HTTP I/O. The separate request preserves the historical ProductForm constructors, enums, default values, serializers, equality and hash/string output.

Unit is an open original string. The manufacturer's enum examples are not an exhaustive catalogue: its description explicitly permits additional units configured in the Scopevisio client. The historical UnitEnum remains available without restricting the new request. Other catalogue strings, including TypeDisplay and InvoicePostingMode, are sent unchanged for server validation. The client neither chooses a unit nor repairs unknown catalogue values.

`ProductApi.CreateProductWithResult` sends POST `/product/new` with a required body and explicit unit string. The schema requires unit, while copied request-body text says topic even though ProductForm has no topic field. The new request follows the schema and does not invent topic. `UpdateProductWithResult` sends POST `/product/{id}` with the original signed Int64 identifier and optional body. All six synchronous, HTTP-info, asynchronous and native CancellationToken forms retain complete JSON, exact raw content, status and response/content headers. Creation HTTP 201 declares ProductNewResponse with optional status (Int32), productId (Int64), location (string) and errors (ObjectNode); future fields remain available. Update HTTP 200 declares no fixed success schema. Empty response data remains null and no identifier or success flag is invented.

```csharp
var request = new ProductWriteRequest
{
    Unit = selectedCatalogueUnit,
    SingleAmount = originalNetPrice,
    RevenueAccount0 = selectedRevenueAccount
};
var result = await productApi.CreateProductWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

Typed price and tax properties use nullable decimal with bounded .NET decimal precision. The complete JSON snapshot keeps original numeric tokens unchanged without casting to decimal or rounding; raw JSON numbers outside typed decimal precision may be supplied through the indexer. Known numeric fields reject strings, incompatible JSON kinds and non-finite floating-point values. RevenueAccountMode requires Int32 JSON width; its documented 0 through 1 range remains server-authoritative. Original tax rates, account identifiers, open unit values and prices are not converted, inferred or calculated locally.

No zero price, false boolean, default type, tax rate or account mode is inserted. The server may generate an omitted creation number, derive type from TypeDisplay, choose default invoice posting/tax/account settings, or select manual account mode from supplied revenue accounts. Updates retain omitted fields; null body sends no body, while an empty request sends `{}`. Typed null setters omit fields and the indexer allows explicit null on updates. As explained by the [general documentation](https://help.scopevisio.com/de/articles/467359-general-documentation), whether a particular field can be cleared or ignores null/empty values remains server-authoritative. Empty or whitespace strings are not normalized.

`ProductGroupWriteRequest` exposes the required original Name plus complete copied future JSON. `CreateProductGroupWithResult` sends POST `/productGroup/new` in all six forms. Its body is required and its schema requires name, despite copied request-body text mentioning absent topic. The new API requires a supplied string name, leaves its text unchanged, and inserts no group or topic defaults. HTTP 201 has no fixed response schema; full JSON/raw/status/headers remain available. No group is created implicitly by a product request.

Both request constructors deep-copy supplied JSON. The complete request is snapshotted before active HTTP I/O. Produktverwaltung (Bearbeiten), account catalogue membership and actual tax/account/product constraints are validated by the server. The configured exception factory runs before result parsing; result fractions use bounded decimal precision and date-looking strings remain strings. No account selection, resource lookup or mutation replay is added. Cancellation after sending cannot establish whether the server applied the write.

Isolated fixtures verify protocol behavior, original decimals, open units, sparse updates, read-only rejection, snapshots, responses and native cancellation. Actual product/group/unit/account/tax acceptance and cleanup remain pending in a coordinated, explicitly owned and reversible Demo fixture. Publication and downstream adoption are separate steps.
