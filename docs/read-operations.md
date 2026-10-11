# Additional current read operations

These additive methods follow the pinned current manufacturer method/path/parameters. Each offers synchronous JSON data, `WithHttpInfo`, async convenience and explicit native-cancellation forms. Existing APIs/defaults remain unchanged.

| API group | Method | Current route | Optional request data |
|---|---|---|---|
| Project | SearchTimeEntryRuns | POST /timeentryruns | SearchRequest |
| Project | SearchTimeEntryRunEntries | POST /timeentryrunentries | SearchRequest |
| Contact | GetChildOrganisationContacts | GET /contact/{id}/childOrganisations | Comma-separated fields query |
| Task | GetFilters | POST /filters/{type} | None |
| Additional | GetStatisticsUnits | GET /catalog/accounting/statisticsunit | language query |

Null optional fields, language and search requests are omitted; empty strings/objects are sent explicitly. The server's default statistics language applies when language is omitted. Known language values include `default` and `english`; the SDK does not invent an enum restricting future values.

Contact IDs use invariant long formatting. Filter types are required and encoded as one path value, including reserved characters; the SDK does not infer extra request-body parameters from the vendor's ambiguous filters description. The contract declares no body for that operation.

The methods reuse the bounded JSON response parsing and error/cancellation behavior documented in [structured searches](structured-search.md), including decimal precision, unknown JSON fields, raw HTTP data and original configured exception identity. No success response schema is declared for these five operations, so no fixed typed response shape is assumed.

Isolated fixtures use a reserved `.invalid` host and no credentials. They verify methods, full routes, long IDs, path/query encoding, optional omission versus empty values, unchanged search objects, responses, errors and active/pre-cancellation. Live account/profile acceptance remains in SDK #26; full API coverage remains in #17.
