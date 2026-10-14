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
        /// <summary>Creates a project using explicitly supplied current fields.</summary>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O without inserting defaults. Null sends no body.</param>
        /// <returns>The complete JSON result, or null for an empty body. HTTP 201 declares ProjectNewResponse with optional status, projectId, location and errors; complete future JSON is retained.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), contact, owner, catalogue, cost-centre, time and custom-field semantics. The route body is optional; a supplied creation object requires name and contactId. Omitted update fields remain unchanged, and explicit null or empty text remains server-authoritative. Original numbers, rates, timestamps, strings and nested arrays are sent without default values, lookups, conversions, local array merges or secondary writes. No account selection or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy ProjectForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or a supplied creation object lacks required fields.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateProjectWithResult(ProjectWriteRequest request = null)
        {
            return CreateProjectWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateProjectWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateProjectWithResultWithHttpInfo(ProjectWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/project/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateProjectWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateProjectWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateProjectWithResultAsync(ProjectWriteRequest request = null)
        {
            return CreateProjectWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectWithResult"/>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the project.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateProjectWithResultAsync(ProjectWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProjectWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProjectWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProjectWithResultAsyncWithHttpInfo(ProjectWriteRequest request = null)
        {
            return CreateProjectWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the project.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProjectWithResultAsyncWithHttpInfo(ProjectWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/project/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateProjectWithResult", response, ExceptionFactory);
        }
    }
}
