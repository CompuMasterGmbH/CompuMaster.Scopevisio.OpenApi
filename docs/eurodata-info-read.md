# Bodyless contact Eurodata information

ContactApi.GetContactEurodataInfo exposes six sync/data/HTTP-info/native asynchronous/cancellation forms for GET /contact/{id}/eurodataInfo. It preserves the original Int64 identity using invariant text and makes one bodyless request with no invented field or paging parameters.

The pinned manufacturer contract declares an optional string requestBody under */* but provides no name, description or meaning for it. This workflow uses the valid optional-body omission. Supplying undocumented GET-body data remains explicitly deferred for manufacturer/live clarification; adding the route does not establish complete parameter support. The 400 description says contact to update despite this GET read; original diagnostics are preserved without inferring a mutation, existence or permission state.

The manufacturer declares no fixed success schema. Complete JToken/raw/status/response and content headers retain unknown fields, integer timestamps and date-like strings. Parsed numeric fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Empty response bodies return null. Existing exception-factory names/ordering and custom original exceptions remain unchanged; native cancellation reaches HTTP I/O and prevents pre-canceled requests.

The server enforces the Lohn (Anzeigen) profile and contact visibility. Fake .invalid fixtures verify exact bodyless routes and invariant Int64 bounds, complete reply shapes/raw data, original profile/400/custom-hook errors and pre/active cancellation. Three absent-wire cases failed before implementation. Real Eurodata result/profile/visibility and optional-body clarification remain under SDK #70. No GUI, credentials, real server or mutation is accessed by these fixtures.
