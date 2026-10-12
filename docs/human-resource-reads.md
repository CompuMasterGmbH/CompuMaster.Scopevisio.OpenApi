# Typed human-resource reads

HumanResourcesApi.GetHumanResourceDetails retrieves GET /humanresource/{id}, preserving the Int64 identifier and optional fields/formatValues. Null formatValues omits the parameter and retains the vendor's server default true; explicit false/true is sent. Null fields omits it; empty fields remains explicit. No default or ID conversion is introduced.

The returned HumanResourceRecord retains the complete JSON object and exposes nullable schema properties: Int64 identifiers/work-day totals/validity timestamps, decimal booked ratio/fee and text metadata. Unknown fields and explicit nulls are retained. Numeric getters follow unformatted schema values; when the server returns formatted text, use the raw field or request formatValues=false. Existing defaults stay unchanged.

GetHumanResourceAvailability sends one JSON object to POST /humanresourcesavailability/v2. HumanResourceAvailabilityRequest retains required validFrom/validTill as nonblank JSON strings, deeply copied searchSpecification and unknown fields. Raw missing/null/number/Boolean/array/object bounds are rejected before HTTP instead of coerced. Optional searchSpecification accepts an object or raw JSON null; the typed setter deep-copies objects and removes the key when assigned null.

The vendor declares yyyy-MM patterns/examples for sv-year-month but includes contradictory dd.MM.yyyy descriptions. The client documents this ambiguity and sends selected strings unchanged without rewriting formats or enforcing range order. Server validation determines supported month/search semantics; no paging/default/completeness rule is inferred.

HumanResourceAvailabilityResponse retains complete metadata and exposes read-only copies of HumanResourceAvailabilityRecord items. Missing/null records differs from a known empty array. Non-array records or non-object items fail clearly; a top-level non-object result also fails. Empty or JSON-null replies produce null typed data while exact raw response/status/headers remain available.

Both workflows provide six sync/data/WithHttpInfo/native async/cancellation forms using existing transport and shared parser. Unknown fields, nulls, Int64 numbers and date-like strings are retained. Decimal fractions use bounded .NET precision and can round or underflow; raw JSON remains exact. Exception factories and original/custom diagnostics run before parsing. Native cancellation prevents pre-canceled calls and cancels active I/O without an added retry.

Isolated .invalid fixtures reproduce missing workflows and verify wire/default/raw-token/model/deep-copy/shape/error/cancellation contracts and typed native consumer compilation. Coordinated live human-resource/profile/month/search/format acceptance remains open in #56. Generated APIs/facade/configuration/auth/global serializers/defaults remain unchanged. No creation/imports, credentials/server resources/full CI/release/downstream import occur.
