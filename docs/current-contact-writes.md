# Current contact creation and updates

Tracked by [SDK #179](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/179), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

The pinned KontaktForm contract declares 108 properties; the historical generated class exposes 88 wire fields. `ContactWriteRequest` provides typed optional access to all 108 current fields and retains complete JSON, including unknown fields. A separate request avoids changing historical constructors, equality, hash/string output, serializers and defaults. The original KontaktForm APIs remain available.

`CreateContactWithResult` sends POST `/contact/new`; `UpdateContactByIdWithResult` sends POST `/contact/{id}` with the original signed Int64 contact master identifier. Each workflow has synchronous, WithHttpInfo, asynchronous and native CancellationToken forms. Results retain complete JSON, exact raw response content, status and response/content headers. HTTP 201 creation declares ContactNewResponse with status (Int32), contactId (Int64), location (string) and errors (ObjectNode); none of these is declared required. Complete and future fields remain available. HTTP 200 update declares no fixed success schema. No contact identifier or success flag is invented.

The request body itself is optional: null omits it. A supplied creation object requires an explicit `Lastname` string and `Person` boolean. False explicitly requests an organisation; no false default is inserted. Empty or whitespace names remain unchanged for server validation. The server evaluates person only when creating a contact.

The update description refers to the manufacturer's general update documentation. Partial updates therefore retain omitted fields and pass explicit JSON null or empty strings unchanged. Typed null setters omit fields; use the inherited JSON indexer for explicit null. Whether a particular field can be cleared or is ignored remains server-authoritative. An empty update sends `{}` and does not populate examples or roles.

```csharp
var request = new ContactWriteRequest
{
    PreferredInvoiceSendMethod = invoiceDispatchMethod,
    CustomerPersonId = selectedCustomerPersonId
};
request["personalDataInsuranceNumber"] = JValue.CreateNull();
var result = await contactApi.UpdateContactByIdWithResultAsyncWithHttpInfo(
    contactId, request, cancellationToken);
```

Known top-level JSON kinds and integer widths are checked before HTTP I/O. PermissionType has the documented 0 through 2 semantic range; this remains server-authoritative after the local Int32 width check. Strings retain original text, including leading zeros and catalogue spelling. Epoch milliseconds are not converted, rounded or normalized to month boundaries. The server validates additional Personal profile requirements, catalogue membership, employment constraints and field-specific defaults. Several vendor enum lists contain replacement characters or comma-joined role lists that contradict their examples; these are not repaired or split into invented client enums.

Constructor input and assigned arrays are deeply copied; the complete request is snapshotted before active HTTP I/O. Custom-field entries require a string name and an explicit value; mixed custom values are preserved because the declared object schema contradicts scalar examples and the general documentation. Creation rejects null custom values; update nulls remain available. Employer objects require an explicit employerId or legacyNumber. Known nested kinds and Int64 widths are checked while future fields remain intact. The server chooses ID/legacy-number precedence and may fall back to the supplied legacyNumber if the employerId is not found; the client performs no lookup. Declared employer year-boundary rules and visibility behavior remain server-authoritative.

The configured exception factory runs before JSON parsing. Parsed result fractions use bounded .NET decimal precision; exact raw JSON is retained and date-looking result strings stay strings. No account selection, automatic role/default choices or mutation replay is added. Cancellation after sending cannot establish whether the server applied the write.

Isolated HTTP fixtures verify request/result behavior and cancellation. Actual contact/profile/catalogue/personal-field behavior and resource lifecycle remain pending in a coordinated, explicitly owned and reversible Demo fixture. No production/default fallback or real contact/profile modification occurs in these fixtures. Publication and downstream adoption are separate steps.
