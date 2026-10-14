# Current contact-property writes

Tracked by [SDK #185](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/185), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`ContactPropertyWriteRequest` exposes all 13 fields of the pinned ContactPropertyForm contract through optional typed properties and preserves complete copied JSON, including unknown future fields. The existing generated ContactPropertyForm constructors, non-nullable defaults, enums and methods remain unchanged.

SpheresDisplay accepts original strings such as the manufacturer's `Kunde, Lieferant` example, rather than restricting callers to the historical single-sphere enum. EmployeeFilterDisplay accepts comma-separated employer contact names instead of treating the schema's descriptive enum text as a literal employer value. Type and Subtype remain original catalogue strings; the client performs no catalogue lookup or normalization. The string Type member explicitly hides JObject.Type; a JToken reference still exposes the JSON token kind.

`ContactApi.CreateContactPropertyWithResult` sends POST `/contact/{contactIdOrLegacyNumber}/contactProperty/new`. The original string selector is escaped as one path segment without lookup, trimming, number conversion or leading-zero removal. Null and dot-segment selectors are rejected before HTTP; other text remains a server decision. `UpdateContactPropertyWithResult` sends POST `/contact/contactProperty/{propertyId}` using the explicit original signed Int64 property ID in invariant text. Each workflow has six synchronous, HTTP-info, asynchronous and native CancellationToken forms.

Both routes require a request body, but no individual ContactPropertyForm fields are declared required. An empty request sends `{}` without invented name, topic, role, timestamp, permission or false defaults. Typed null setters omit fields. Raw explicit null is allowed in updates and rejected for known creation fields. Omitted update fields remain unchanged; whether a particular explicit null or empty string clears a value is server-authoritative.

ValidFrom and ValidTo retain original epoch milliseconds as nullable Int64 values. The client does not rewrite dates according to the contract's first/last-day-of-year examples. PermissionType preserves optional Int32 width without inserting the documented zero default; its semantic 0 through 2 range is validated by the server. Permission-profile and login strings remain unchanged and are evaluated by the server for permissionType 1. EmployeeDomain preserves omission versus explicit false. The manufacturer requires `Kontakte bearbeiten (Bearbeiten)` for both writes; the client does not infer permission from successful reads.

```csharp
var request = new ContactPropertyWriteRequest
{
    Description = originalDescription,
    SpheresDisplay = "Kunde, Lieferant",
    EmployeeFilterDisplay = selectedEmployerNames
};
var result = await contactApi.CreateContactPropertyWithResultAsyncWithHttpInfo(
    selectedContactIdOrLegacyNumber, request, cancellationToken);
```

Creation HTTP 201 and update HTTP 200 have no fixed success schema. The complete JToken, exact raw JSON, status and response/content headers remain available; no ID or success flag is invented. Empty content produces null Data, while explicit JSON null remains a token. Date-looking result text stays a string, and parsed fractions use bounded .NET decimal precision while raw JSON remains exact. The configured exception factory runs before parsing and preserves original errors. Requests are deeply snapshotted before HTTP I/O; no account selection, mutation replay or automatic creation-to-update chain is added. Cancellation after sending cannot establish whether the server applied the write.

287 isolated fake-host fixtures cover every field, both routes and all six forms, sparse bodies, defaults, explicit nulls, integer widths, open strings, escaped selectors, copied future JSON, raw results, headers, errors, factories, legacy methods and native cancellation. Actual contact/catalogue/employer/permission acceptance and cleanup remain pending in a coordinated, explicitly owned and reversible Demo fixture. Exact Scope organisation identity and, where relevant, opaque Teamwork tenant identity must be proved without default/production fallback. Source merge, package publication and downstream adoption are separate steps.
