# Typed project resources and revenues

Four additive read workflows follow the current typed manufacturer schemas. Existing similarly named APIs and defaults remain available.

| Method | Route | Result |
|---|---|---|
| GetProjectResourceDetails | GET /project/{projectId}/projectresources | ProjectResourcesResponse |
| GetProjectRevenueDetails | GET /project/{projectId}/projectrevenue | ProjectRevenuesResponse |
| SearchProjectResources | POST /projectresources/v2 | ProjectResourcesResponse |
| SearchProjectRevenues | POST /projectrevenue/v2 | ProjectRevenuesResponse |

Each method offers synchronous typed data, WithHttpInfo, async convenience and explicit native-cancellation forms. WithHttpInfo preserves status, headers and unchanged raw JSON. Each configured exception factory runs before typed parsing and receives the method's new name.

## Requests

GET methods require the project master ID. Null field selection and revenue type are omitted; the server retains its defaults. Explicit empty fields and planning type zero are transmitted. ProjectRevenueType distinguishes Planning, Realization and Deviation; values outside the documented zero-to-two range fail before HTTP I/O.

POST methods require a typed request with both ProjectStartFrom and ProjectStartTo. Missing/blank bounds and raw values of a non-string JSON type fail before transport. Revenue type values imported through constructors/indexers must be JSON integer tokens in the documented zero-to-two range; fractions, booleans, strings and out-of-range integers are rejected without rounding or coercion. Omitted and explicit-null low-level revenue fields remain distinct and unchanged; live acceptance of explicit JSON null is not established. Dates remain caller-supplied strings: the vendor pattern/examples use yyyy-MM-dd while its descriptions also mention dd.MM.yyyy. The SDK documents this inconsistency without reinterpreting dates or asserting that both forms are accepted by every server.

```csharp
var request = new ProjectRevenuesSearchRequest
{
    ProjectStartFrom = "2026-01-01",
    ProjectStartTo = "2026-12-31",
    RevenueType = ProjectRevenueType.Realization,
    SearchSpecification = new SearchRequest
    {
        Fields = new[] { "projectId", "revenue", "extraRevenue", "averageFee" },
        FormatValues = false
    }
};
var response = await client.ProjectApi.SearchProjectRevenuesAsyncWithHttpInfo(request, cancellationToken);
foreach (ProjectRevenueRecord record in response.Data.Records)
{
    decimal? revenue = record.Revenue;
}
```

The snippet assumes the model namespace import. Nested search specifications are copied on assignment and retrieval. Optional null setters remove fields; inherited JSON indexers preserve custom values and allow explicit JSON null. The manufacturer excludes month, monthRevenue and id from nested search criteria.

## Typed results

Resource and revenue records expose the documented identifiers, text, counts, flags and fractional values. IDs and the response's revenue-type code retain Int64 representation; response codes are not narrowed to the request enum. Nullable getters preserve absence/JSON-null without inventing numeric zero. Unknown record fields and envelope metadata remain accessible through inherited JSON indexers.

Records returns typed copies in a read-only list. Missing or JSON-null records return null; an empty records array returns an empty list. Invalid non-array/non-object record shapes raise JsonSerializationException before returning the response. Empty/JSON-null HTTP results have null typed Data; RawJsonContent distinguishes their original representations.

Fractional fields use .NET decimal precision, about 28–29 significant digits. Very small exponents or more precise fractions may round/underflow; out-of-range values can fail. RawJsonContent remains exact. Typed numeric getters follow the numeric schemas; formatted text may require different handling. When selecting numeric fields, FormatValues=false requests unformatted output through the vendor's search protocol.

Isolated fixtures cover the four paths and request bodies/queries, typed financial precision, long IDs including response revenue codes, months, nulls, unknown metadata, invalid envelopes, date-bound/type validation, errors and active/pre-cancellation. They do not establish account/profile permissions or confirm ambiguous vendor date formats against a live server. Live acceptance remains in SDK #28; full coverage remains in #17. No resource creation or removal is introduced in this batch.
