# Current time-entry creation and updates

Tracked by [SDK #181](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/181), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

The pinned current TimeEntryForm contract declares 24 properties; the historical generated class exposes 19. `TimeEntryWriteRequest` exposes all 24 with typed optional properties and preserves complete JSON, including unknown fields. The previously missing fields are EmployeeMail, OrderNumber, LegacyNumber (wire name `legacynumber`), WorkLocation and CustomFields. A separate request preserves legacy model constructors, equality, hash/string output, serialization and defaults.

`ProjectApi.CreateTimeEntryWithResult` sends POST `/timeentry/new`; `UpdateTimeEntryWithResult` sends POST `/timeentry/{id}` with the original signed Int64 time-entry identifier. Each workflow has synchronous, WithHttpInfo, asynchronous and native CancellationToken forms. Results retain complete JSON, exact raw content, status and response/content headers. Creation HTTP 201 and update HTTP 200 declare no fixed result schema; an empty result has null Data. No identifier or success flag is invented.

Creation requires a request body and an explicitly supplied employee contact ID or email/login. The schema required array contradictorily lists both employeeId and employeeMail, while employeeMail is documented as replacing employeeId and the creation HTTP 400 description reports a missing ID or email. The new request follows those alternative-selector descriptions: supply exactly one, never both. Updates may omit both. Explicit zero IDs and empty or whitespace email strings are passed unchanged for server validation; the client does not pick an employee, resolve an email or impose undocumented text rules. Likewise, do not supply both orderId and orderNumber; the original order number is not expanded from an example template or resolved locally.

```csharp
var request = new TimeEntryWriteRequest
{
    EmployeeMail = selectedEmployeeLogin,
    OrderNumber = selectedOrderNumber,
    WorkLocation = workLocation,
    StartTs = startEpochMilliseconds,
    EndTs = endEpochMilliseconds
};
var result = await projectApi.CreateTimeEntryWithResultAsyncWithHttpInfo(
    request, cancellationToken);
```

No timestamp, false boolean or permission value is inserted. The server may default omitted creation timestamps to the current time. FullDay=true lets the server ignore supplied timestamps and use full-day boundaries configured in the Scopevisio desktop application. Contact and project inference, product references and permission/catalogue semantics remain server-authoritative; no lookup or date/time normalization is performed. PermissionType has the documented 0 through 2 semantic range; the local check enforces the Int32 JSON width while the server validates that semantic range.

Update bodies are optional: null omits the body; an empty object sends `{}`. Omitted fields remain unchanged. Typed null setters omit fields; the JSON indexer allows explicit null for updates. A null selector does not conflict with its explicit non-null alternative. The manufacturer explains that some fields can be cleared using null or empty strings while other fields ignore those values. Whether a particular time-entry field can be cleared remains server-authoritative. Existing time-entry runs may prevent updates unless the server's run settings allow changes.

Known JSON kinds and integer widths are checked before HTTP I/O without coercing fractions, numeric strings or overflow. Constructor input and assigned arrays are deeply copied, and the complete request is snapshotted before sending. Custom-field entries require an original string customFieldName and an explicit value. Mixed custom values remain intact: the vendor object schema conflicts with scalar examples and the [general documentation](https://help.scopevisio.com/de/articles/467359-general-documentation). Creation rejects null custom values; update nulls remain available. Configured names and value types are validated by the server.

Creation requires Projekte, Zeiterfassung (Bearbeiten); update requires Projekte (Bearbeiten). The configured exception factory runs before JSON parsing. Result fractions use bounded .NET decimal precision; raw JSON remains exact and date-looking strings remain strings. No account selection, automatic defaults or mutation replay is added. Cancellation after sending cannot establish whether the server applied the write.

Isolated HTTP fixtures verify protocol behavior and cancellation. Actual time-entry/profile/order/custom-field behavior and complete cleanup remain pending in a coordinated, explicitly owned and reversible Demo fixture. Publication and downstream adoption are separate steps.
