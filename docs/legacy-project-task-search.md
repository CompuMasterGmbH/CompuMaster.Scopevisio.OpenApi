# Legacy Project and Task raw search bodies

SDK #42/#17 repairs GetProjects/GetTasks without replacing their public contracts. Supplied pre-serialized JSON is now sent unchanged in both synchronous and asynchronous paths, instead of being serialized into a quoted JSON string. Null still omits the optional body; explicit empty text remains explicit. No global serializer or unrelated generated method changes.

Existing GetProjects/GetTasks void convenience methods, Task async methods and ApiResponse<Object> WithHttpInfo methods retain signatures/defaults. Legacy Data remains null; exact RawJsonContent/status/headers remain available. Existing caller default headers/Accept selection, access token and exception-hook names/identity are unchanged. Use SearchProjects/SearchTasks for productive parsed complete JSON results.

New GetProjectsAsync/GetTasksAsync and AsyncWithHttpInfo overloads accept the body and native CancellationToken. The original no-token AsyncWithHttpInfo methods delegate with CancellationToken.None; native HTTP I/O is canceled before/during transport without changing authorization state, retrying or treating cancellation as an API error. Handwritten cancellation implementations remain in separate partials.

Eighteen focused cases fail before repair: twelve existing sync/async wire checks reproduce double serialization, six identify missing cancellation overloads. All 44 new isolated cases then pass, covering exact JSON whitespace/Unicode/decimal text, null/empty bodies, default headers, raw/null Data/result types, errors/hooks, single-send and active/pre-cancellation on Windows net8.0/net48. Server profile/search acceptance remains open in #42; local fixtures use .invalid transports only.

Generated files receive only the two targeted body fixes/no-token delegations and required UTF-8 BOM/CRLF formatting. Existing public/protected members and obsolete annotations remain; no release or downstream dependency adoption. User-authorized [skip ci] after essential platform checks.
