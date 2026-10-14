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
        /// <summary>Updates explicitly supplied fields of a selected project.</summary>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current project JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), contact, owner, catalogue, cost-centre, time and custom-field semantics. The route body is optional; a supplied creation object requires name and contactId. Omitted update fields remain unchanged, and explicit null or empty text remains server-authoritative. Original numbers, rates, timestamps, strings and nested arrays are sent without default values, lookups, conversions, local array merges or secondary writes. No account selection or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy ProjectForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or a supplied creation object lacks required fields.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateProjectWithResult(long id, ProjectWriteRequest request = null)
        {
            return UpdateProjectWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateProjectWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateProjectWithResultWithHttpInfo(long id, ProjectWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/project/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateProjectWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateProjectWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateProjectWithResultAsync(long id, ProjectWriteRequest request = null)
        {
            return UpdateProjectWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProjectWithResult"/>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateProjectWithResultAsync(long id, ProjectWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateProjectWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateProjectWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProjectWithResultAsyncWithHttpInfo(long id, ProjectWriteRequest request = null)
        {
            return UpdateProjectWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProjectWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProjectWithResultAsyncWithHttpInfo(long id, ProjectWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/project/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateProjectWithResult", response, ExceptionFactory);
        }
    }
}
