# Contact search protocol checkpoint

Tracked by [#20](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/20), parent [#17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17). Uses the contract/inventory pinned by [#18 / PR #19](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/pull/19).

## Manufacturer discrepancy and bounded correction

POST `/contacts` declares `application/json` with a `string` request schema and describes a JSON search filter. The [manufacturer search documentation](https://help.scopevisio.com/en/articles/467360-search-scope-documentation) shows a JSON object with `page`, `pageSize`, `fields`, `formatValues` and `search` members. Its response has a description but no machine-readable response schema. This is insufficient for blind regeneration of the request or response model.

The existing synchronous `GetContactsWithHttpInfo(string)` sends the supplied JSON unchanged and parses `records` into `RecordCollection<Contact>`. The previous asynchronous overload serialized that string again. A fake HTTP handler reproduced the difference: the synchronous case passed, and the asynchronous case sent a quoted/escaped JSON string and failed. The correction sends the same supplied JSON body through both entry points. It does not validate or reformat arbitrary search expressions, infer paging completion or change global serializers.

The existing asynchronous signature and optional null default remain. It delegates to a new overload:

```csharp
var response = await api.GetContactsAsyncWithHttpInfo(jsonSearchExpression, cancellationToken);
```

Both arguments of the new overload are required, preserving existing no-argument and single-argument call resolution. The new implementation resides in `ContactApi.Cancellation.cs`; the old generated-file entry point delegates with `CancellationToken.None`. Existing synchronization, authorization headers, configured exception factory, raw response, typed records and class obsolete annotation are retained.

## Isolated evidence and remaining acceptance

Fixtures verify POST path/media type/bearer headers; unchanged paging, selected fields, explicit null and decimal request values; typed Contact records with 64-bit IDs, null fields and UTC epoch milliseconds; empty-filter defaults; existing exception hooks; legacy signature metadata; pre-cancellation and active native HTTP cancellation without changing authorization state. All requests use a caller-owned HttpClient with a fake handler and an `.invalid` hostname. Cancellation tests bound waits and cancel their own pending request in finally.

Tests are protocol fixtures, not evidence that all contact response fields have stable types or that every backend supports the same input. In particular, the existing JObject-based response parser's handling of arbitrary high-precision response numbers is outside this correction; request decimals are passed unchanged. The complete POST `/contacts` status remains deferred in the inventory pending live/account and full-response acceptance. The permission profile is Kontakte (Anzeigen).

No remote resources or credentials are used. Real-server checks require coordinated access across SDK/DMS repositories and local sessions. Review/merge, successful applicable post-merge checks, separately authorized package publication and exact downstream package/commit selection remain separate gates in #17 and DMS #90. This source change selects no package version.
