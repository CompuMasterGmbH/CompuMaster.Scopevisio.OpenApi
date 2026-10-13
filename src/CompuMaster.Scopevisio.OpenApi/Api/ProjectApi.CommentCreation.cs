using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Creates a comment for an explicitly selected project.</summary>
        /// <param name="id">The original signed Int64 project master identifier, sent using invariant text without lookup or example defaults.</param>
        /// <param name="request">The optional complete JSON comment, copied before HTTP I/O. Null omits the body; omitted fields do not insert the server category or example text.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte, Pinnwand (Bearbeiten), resource existence and actual authorization. Supplied category values retain their exact spelling and text is sent verbatim. No resource lookup, implicit account selection or mutation retry is added. Cancellation after sending cannot establish whether the comment was created. A result does not imply an invented comment identifier or success flag. Parsed result fractions use bounded .NET decimal precision; exact raw JSON is retained.</remarks>
        /// <exception cref="ArgumentException">A supplied known field is not a JSON string, or a category does not match a documented value exactly.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateProjectCommentWithResult(long id, CommentCreationRequest request = null)
        {
            return CreateProjectCommentWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="CreateProjectCommentWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateProjectCommentWithResultWithHttpInfo(long id, CommentCreationRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/project/{id}/comment/new";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateProjectCommentWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateProjectCommentWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateProjectCommentWithResultAsync(long id, CommentCreationRequest request = null)
        {
            return CreateProjectCommentWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectCommentWithResult"/>
        /// <param name="id">The original signed Int64 project master identifier, sent using invariant text without lookup or example defaults.</param>
        /// <param name="request">The optional complete JSON comment, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the comment.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateProjectCommentWithResultAsync(long id, CommentCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProjectCommentWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProjectCommentWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProjectCommentWithResultAsyncWithHttpInfo(long id, CommentCreationRequest request = null)
        {
            return CreateProjectCommentWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectCommentWithResultWithHttpInfo"/>
        /// <param name="id">The original signed Int64 project master identifier, sent using invariant text without lookup or example defaults.</param>
        /// <param name="request">The optional complete JSON comment, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the comment.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProjectCommentWithResultAsyncWithHttpInfo(long id, CommentCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/project/{id}/comment/new";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateProjectCommentWithResult", response, ExceptionFactory);
        }
    }
}
