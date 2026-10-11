# Structured searches

The additive `SearchContacts`, `SearchProjects` and `SearchTasks` methods accept `SearchRequest` and return `JToken`. Their `WithHttpInfo` variants also retain status, headers and the unchanged raw response. Existing `GetContacts`, `GetProjects`, `GetTasks` signatures and defaults remain available.

`SearchContactComments`, `SearchProjectComments` and `SearchTaskComments` use the current POST comment-search routes. Existing GET comment APIs remain available.

```csharp
var query = new SearchRequest
{
    Page = 2,
    PageSize = 250,
    Fields = new[] { "id", "name" },
    FormatValues = false,
    Order = new[] { "id = desc" },
    Search = new JArray(new JObject
    {
        ["field"] = "name",
        ["value"] = "Example",
        ["operator"] = "contains"
    })
};
var result = await client.ProjectApi.SearchProjectsAsyncWithHttpInfo(query, cancellationToken);
JToken records = result.Data?["records"];
```

The snippet assumes `Newtonsoft.Json.Linq` and `CompuMaster.Scopevisio.OpenApi.Model` imports. The existing API groups keep their incomplete/unstable annotations because this batch does not verify every operation.

## Request behavior

Unspecified properties are omitted. Null `query` omits the body; an empty `SearchRequest` sends `{}`. The server applies its defaults and bounds: page zero, page size 100, maximum page size 1000. The client sends supplied values unchanged.

`Fields` and `Order` copy assigned/returned arrays. `Search` copies an assigned array; its getter returns the mutable criteria within this request. The copy constructor deeply copies a JSON object, including custom filters. Setting an optional property to null removes that field; use the inherited indexer with `JValue.CreateNull()` when explicit JSON null is required.

Criteria preserve operator spelling, comma-separated values, JSON value types and omitted versus explicit-null values. Specialized filters remain accessible through the JSON indexer. `CountOnly = true` maps to `"count":true`. Count searches preserve the response's actual shape instead of forcing a records envelope.

The [manufacturer search documentation](https://help.scopevisio.com/en/articles/467360-search-scope-documentation) describes JSON objects with paging, field selection, ordering and criteria. The pinned OpenAPI specifies string schemas for these JSON bodies and declares no success response schemas for these six operations. The additive methods follow the documented JSON-object protocol; the pinned contract is retained unchanged.

## Results, errors and cancellation

Results retain unknown fields, paging metadata, nulls, arrays and count scalars. Integers retain their JSON integer representation; fractional numbers are parsed as decimals and date strings remain strings. Fractional values outside the .NET decimal range and invalid JSON raise the original JSON parsing exception. Empty response bodies produce null data; JSON null remains a JSON-null token. The raw response always remains available in `RawJsonContent`.

Each group's existing exception factory runs before JSON parsing and receives the new search method name. Its exception is thrown unchanged. Bearer authorization uses the existing configuration; no global serializer or authorization state is modified.

Async convenience overloads use `CancellationToken.None`. Explicit-token overloads require both query and token, preventing ambiguity in existing/default calls. Pre-cancellation stops before transport; active cancellation is forwarded to HTTP I/O.

## Verification boundary

Fake-handler tests use a reserved `.invalid` host and no credentials. They cover all six routes, sync/async entry points, field/paging/criteria payloads, preserved financial precision, long IDs, epoch fields, date strings, count responses, errors and cancellation. They establish local protocol behavior, not account permissions or complete real-server compatibility. Live acceptance remains tracked in SDK #24; full contract coverage remains tracked in #17.
