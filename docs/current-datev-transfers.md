# Current DATEV transfers

Tracked by [SDK #205](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/205), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

`DatevExportRequest` exposes all thirteen current DatevExportSettingsForm fields: four nullable signed Int64 UNIX epoch millisecond bounds, six original strings and three nullable booleans. It adds current preferRowTexts and mapping, which the historical eleven-field model lacks. `DatevImportRequest` exposes the original zone and Base64 data strings. Both deeply copy future JSON. Historical models, enums, zero/false constructor defaults and methods remain unchanged; in particular, historical vatMatrixDatevKeys stays false despite the current server's documented true default. Old data-only methods discard data and HTTP-info forms retain raw/status/headers with null Data.

`AccountingApi.ExportDatevWithResult` and `ImportDatevDataWithResult` each provide six synchronous, HTTP-info, asynchronous and native CancellationToken forms for POST `/datevexport` and `/datevpostings/new`. Both bodies are optional: null sends no body. Supplied export objects may be empty; supplied import objects require an original data string. Known fields require their declared JSON kinds, and timestamps must fit signed Int64. Typed null omits fields; raw null for known fields fails before I/O. Unknown future JSON remains copied. If either creation-date filter bound is supplied, both are required, as documented by the manufacturer. Actual date range ordering, mappings, choices and content remain server-authoritative.

```csharp
var settings = new DatevExportRequest
{
    FromPostingDateTs = selectedFromMilliseconds,
    ToPostingDateTs = selectedToMilliseconds,
    Mapping = selectedMapping,
    PreferRowTexts = true
};
var export = await accountingApi.ExportDatevWithResultAsyncWithHttpInfo(
    settings, cancellationToken);
// export.Data contains the original buffered bytes.
```

Original epoch values are sent without date/timezone conversion, current-year calculation, invented minimum or ordering rules. Original dimension/text/field/strategy/mapping strings retain whitespace, empty text and leading zeros without normalization or lookup. Mapping accepts original `legacy` or numeric account-mapping record-ID text without parsing or resolving it. Omitted booleans remain distinct from false and true. The server documents default current-year posting bounds, posting text, external document number, standard export strategy, vatMatrixDatevKeys true, preferRowTexts false and no mapping when omitted; the client inserts none of these.

Export requires `Datenexport (Anzeigen)`. The primary HTTP200 declares no fixed success schema, MIME, filename, encoding or format guarantee. The additive result therefore preserves complete buffered original bytes and status/combined headers without JSON parsing, decoding, transcoding, filename inference or EXTF validation. RawJsonContent is null for the byte result. Empty content yields an empty byte array; a transport that supplies no bytes may yield null. Request JSON retains application/json while Accept is */* so returned media are preserved. Buffering is not streaming and actual export behavior remains unverified.

Import requires `Datenimport (Bearbeiten)`. Original zone remains independent from data and is neither looked up nor defaulted; actual settings may use the standard zone when omitted. `DatevImportRequest.FromBytes` copies all original bytes into Base64 without filesystem access, text conversion or DATEV structure validation. Supplying raw Base64 text also remains available without decoding or repair. The existing DatevPostings constructor's first argument remains zone; use its named data argument when calling the legacy constructor.

Import HTTP200 has no fixed successful schema and directs callers to response errors. It does not prove a single posting, per-row success or aggregate success. Complete JToken data, exact raw JSON, status and response/content headers remain available. Empty content gives null Data, explicit JSON null stays a token, date-like text stays a string, and parsed fractions use bounded decimal precision while raw JSON remains exact. The configured exception factory runs before import parsing or export wrapping and preserves original failures, including documented400 mandatory parameter,404 zone and500 invalid data/posting errors.

The complete request and future data are snapshotted before active HTTP. No account-context selection, chart/zone/mapping lookup, calculation, hidden write, retries or mutation replay is added. Import may create financial postings; cancellation after sending cannot establish resulting posting state. Actual tests require owned reversible fixtures and verified cleanup, not an assumption that Demo access or a successful read grants financial write permission.

342 isolated fake-host cases cover all fifteen fields, required/optional bodies, paired creation-date bounds, Int64 limits, original strings, nullable flags, copied snapshots/future JSON, all byte values/media/empty exports, full import results/headers/errors/factories/cancellation and unchanged legacy models/defaults. Actual Demo DATEV/posting/zone/mapping/tax/profile acceptance and complete owned cleanup remain open, requiring a fresh coordinated reversible window with exact numeric Scope organisation proof and no PROD fallback. Source merge, separate SDK publication and exact consumer adoption remain distinct. This source is excluded from delivered DMS2026.10.14's unchanged published SDK2026.10.13 pin; that release waiver does not authorize SDK tests or publication.
