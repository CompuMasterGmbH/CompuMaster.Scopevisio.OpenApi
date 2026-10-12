# Complete dimension entries with explicit paging

AccountingApi.GetDimensionEntriesDetails and its HTTP-info/native asynchronous forms expose the complete response for GET /dimensions/{dimensionNameOrNumber}/dimensionentries. The historical GetDimensionReportEntries methods accept no page controls and discard successful JSON; they remain unchanged.

The original dimension name or number is escaped as one path segment without trimming, numeric conversion or lookup. Blank and relative path selectors are rejected. Optional page and pageSize are independent nullable Int32 values: null omits the parameter, and explicit values use invariant text without inserted defaults or pagination loops. The manufacturer describes the first page as zero and allows a maximum page size of 1,000. Its schemas specify no explicit range constraints; this additive API forwards supplied values and preserves server validation/errors. Passing an out-of-range value does not establish backend support for that value.

The Berichte (Anzeigen) profile is required. HTTP 404 can indicate a missing dimension name or an unknown dimension. No fixed success schema is declared, so objects, arrays, scalars, future fields and explicit null remain complete JToken values. Requests are bodyless and add no fields/formatting/count options.

WithHttpInfo preserves status, combined response/content headers and exact raw JSON. Dates remain strings. Parsed fractions use .NET decimal precision and may round, underflow or exceed its range; raw JSON remains exact. The caller's exception factory runs before parsing; native cancellation reaches admission and active HTTP I/O, and no speculative replay is introduced. Existing configuration, token/account selection and accounting methods remain intact.

[SDK #116](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/116) tracks isolated paging/escaping/error/cancellation fixtures, compiled compatibility and the remaining coordinated actual dimension/profile/page verification. These fake .invalid tests do not establish real-server acceptance, publication or downstream adoption.
