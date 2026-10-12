# Journal file downloads

AccountingApi.DownloadJournalEntryFile and DownloadStampedJournalEntryFile expose six sync/byte-array/HTTP-info/native asynchronous/cancellation forms each. They read GET /journal/{documentNumber}/file and /journal/{documentNumber}/filewithstamp; existing void/Object GetGeneralJournalEntryFile APIs remain unchanged.

Required document-number strings retain valid original text and are escaped as one path segment. Missing/blank/relative-dot selections fail before HTTP. Requests are bodyless and accept any response media type because the manufacturer specifies no fixed MIME, filename, size or success schema. Stamps depend on the server's invoice settings and are not constructed by the client.

The existing transport buffers the complete response in memory. Data contains original RawBytes, including arbitrary non-UTF8 values and an empty array for an empty file; it is null only when the transport supplies no bytes. Both response and content headers are retained in a case-insensitive map, combining repeated names. This preserves Content-Type, Content-Length and Content-Disposition without inventing a filename. RawJsonContent is null for these binary APIs; use Data for the file contents.

The client does not parse files as JSON, call ApiClient's legacy file deserializer or create temporary/destination files. Header filenames are metadata only and never drive a filesystem write. The caller chooses any later storage and lifecycle. Original HTTP status, profile/file diagnostics and configured hooks remain authoritative; a 404 can mean missing entry, missing file or missing permission and is not converted to an existence claim.

The exception factory runs before wrapping the binary result and retains workflow names. A custom exception remains original; setting the factory to null preserves its existing opt-out behavior. Native cancellation reaches active HTTP I/O and prevents pre-canceled calls. No file content, server, organisation or stamp acceptance is inferred from successful isolated fixtures.

Tests use .invalid fake handlers and cover all forms, exact escaped GETs, arbitrary byte values, empty files, both header sets and cross-set repeated names, untrusted filename metadata, original errors/hooks and native cancellation. Six absent-wire cases and six missing-content-header cases failed before their respective corrections. Real Journal profile grants, actual files and server invoice stamps require coordinated live acceptance under SDK #66. No GUI, credentials, live server or filesystem download occurs here.
