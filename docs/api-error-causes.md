# Original causes in API errors

SDK #34/#17 preserves RestSharp response.ErrorException as ApiException.InnerException in the default error factory. HTTP status errors (400 and above) and status-zero transport failures retain their existing ApiException type, ErrorCode, Message and ErrorContent. The new constructor is internal; every existing constructor and public/protected signature remains unchanged.

Consumers can inspect the original DNS/socket, TLS or timeout exception, including its nested cause, without reconstructing it from a message. Ordinary HTTP errors with no underlying response exception still have no inner exception. Caller-supplied ExceptionFactory hooks remain independent and receive the unchanged response; their chosen exception instance is propagated unchanged. Successful and redirect status classification is unchanged.

Cancellation remains OperationCanceledException before and during native HTTP I/O. It is not converted into ApiException by this fix. No retries, new requests, authorization changes or global serializer changes are introduced. Both existing Contact methods and additive structured searches exercise this common factory path.

Thirty-six isolated fake-transport/direct-factory cases cover cause identity, messages/content/status, custom hooks, cancellation and single-send behavior. Twenty-one cases failed before the fix. They establish local error-contract behavior, not successful DNS/TLS/live server operation. Independent source review/merge and exact integrated pins are tracked in #34; no package publication or downstream import is implied.

ApiException.cs and Configuration.cs already contain generated-era code and protected custom behavior. This change is confined to one additive internal constructor and two default-factory calls with targeted XML documentation. Their touched files use the repository's UTF-8 BOM/CRLF format.
