# Contact, project and task comment creation

Tracked by [SDK #169](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/169), parent [SDK #17](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/17) and consuming [DMS #90](https://github.com/CompuMasterGmbH/CompuMaster.Dms/issues/90).

The pinned manufacturer contract declares three POST operations at `/contact/{contactId}/comment/new`, `/project/{projectId}/comment/new` and `/task/{taskId}/comment/new`. Each identifier is an explicit signed Int64 master ID. The JSON body and both `BlogEntryForm` properties are optional. The exact category values are `Notiz`, `Anruf`, `Besprechung`, `Vertrag` and `E-mail`; the server category default is `Notiz`. The declared HTTP 201 response has no fixed success schema.

`CommentCreationRequest` retains complete JSON and unknown fields, deeply copies constructor input, and snapshots the complete request before HTTP I/O. `Category` and `Body` null setters omit their fields. Empty and whitespace text remain unchanged; markup is not converted. An omitted request sends no body; an empty request sends `{}`. Neither inserts the server category default or example comment. Explicit known JSON fields must be strings; JSON null and other kinds are rejected before sending. A supplied category must match its declared spelling exactly.

The additive `CreateContactCommentWithResult`, `CreateProjectCommentWithResult` and `CreateTaskCommentWithResult` workflows each provide synchronous, synchronous WithHttpInfo, asynchronous, asynchronous CancellationToken, asynchronous WithHttpInfo and asynchronous WithHttpInfo CancellationToken forms. For example, with an already configured API and an explicitly chosen contact:

```csharp
var request = new CommentCreationRequest
{
    Category = "Anruf",
    Body = commentText
};
var response = await contactApi.CreateContactCommentWithResultAsyncWithHttpInfo(
    contactId, request, cancellationToken);
```

Results retain complete JSON, exact raw response content, status and response/content headers. Empty content returns null data. No comment identifier or success flag is invented. The configured exception factory runs before JSON parsing. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact and date-looking strings stay strings.

The original `BlogEntryForm` operations and their signatures, serializers and defaults remain unchanged. The new workflows perform no resource lookup, account selection, authorization change or mutation replay. The server enforces resource existence, authorization and the respective Kontakte/Projekte/Aufgaben, Pinnwand (Bearbeiten) profile. Cancellation after sending cannot establish whether a comment was created.

Isolated fixtures verify the request/result contract and cancellation through a fake HTTP handler. Actual comment creation, profile requirements, server defaults and account behavior remain pending under coordinated exclusive access with test-owned resources and complete cleanup. No real comments are created by these fixtures. Publication and downstream adoption are separate steps.
