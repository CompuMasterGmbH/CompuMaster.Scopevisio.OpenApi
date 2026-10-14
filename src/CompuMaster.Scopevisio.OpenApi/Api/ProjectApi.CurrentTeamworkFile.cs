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
        /// <summary>Adds original Teamwork file data to an explicitly selected project.</summary>
        /// <param name="projectId">The original signed Int64 project ID, formatted invariantly without minimum rules, number/contact/UID/tenant translation or lookup.</param>
        /// <param name="request">The optional complete copied file JSON; null sends no body without filename, PDF or collection defaults.</param>
        /// <returns>The complete JSON result, or null for empty content. The manufacturer declares HTTP 200 without a fixed successful schema.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), selected-entity visibility and independent Teamwork access. Body is optional; a supplied object requires original Base64 data. Original filename/data/collection-name strings and omitted/false/true createCollectionIfNotExist are preserved without decode/format repair, filename/PDF/collection defaults, lookup, ID translation or local collection writes. The manufacturer describes possible missing-collection creation and upload-without-collection wording; actual assignment behavior remains server-authoritative and resulting files/collections need owned cleanup. HTTP 200 is unspecified; 400 may mean missing data or Teamwork access, and 404 missing entity or authorization. Full/future JSON is retained without inferred file IDs, success or tenant permission. Requests are copied before HTTP without context changes, extra calls or replay. Cancellation after sending cannot establish file/collection state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual Demo/Teamwork behavior and separate tenant binding remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind, a supplied object lacks data.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AddProjectFileWithResult(long projectId, TeamworkFileWriteRequest request = null)
        {
            return AddProjectFileWithResultWithHttpInfo(projectId, request).Data;
        }

        /// <inheritdoc cref="AddProjectFileWithResult"/>
        /// <returns>The complete JSON, exact raw content, status and response/content headers.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AddProjectFileWithResultWithHttpInfo(long projectId, TeamworkFileWriteRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/project/{projectId}/file/new";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(CultureInfo.InvariantCulture));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AddProjectFileWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AddProjectFileWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AddProjectFileWithResultAsync(long projectId, TeamworkFileWriteRequest request = null)
        {
            return AddProjectFileWithResultAsync(projectId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddProjectFileWithResult"/>
        /// <param name="projectId">The original signed Int64 project ID, formatted invariantly without minimum rules, number/contact/UID/tenant translation or lookup.</param>
        /// <param name="request">The optional complete copied file JSON; null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation occurs before or during transport; files or collections may already exist.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AddProjectFileWithResultAsync(long projectId, TeamworkFileWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AddProjectFileWithResultAsyncWithHttpInfo(projectId, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AddProjectFileWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddProjectFileWithResultAsyncWithHttpInfo(long projectId, TeamworkFileWriteRequest request = null)
        {
            return AddProjectFileWithResultAsyncWithHttpInfo(projectId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddProjectFileWithResultWithHttpInfo"/>
        /// <param name="projectId">The original signed Int64 project ID, formatted invariantly without minimum rules, number/contact/UID/tenant translation or lookup.</param>
        /// <param name="request">The optional complete copied file JSON; null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation occurs before or during transport; files or collections may already exist.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddProjectFileWithResultAsyncWithHttpInfo(long projectId, TeamworkFileWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/project/{projectId}/file/new";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(CultureInfo.InvariantCulture));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AddProjectFileWithResult", response, ExceptionFactory);
        }
    }
}
