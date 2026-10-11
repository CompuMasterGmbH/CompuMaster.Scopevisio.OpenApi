using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Searches projects and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's project read permissions. No fixed response schema is declared by the manufacturer.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchProjects(SearchRequest query = null)
        {
            return SearchProjectsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchProjects"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchProjectsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/projects";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProjects", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProjects"/>
        public Task<JToken> SearchProjectsAsync(SearchRequest query = null)
        {
            return SearchProjectsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjects"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchProjectsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchProjectsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchProjectsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchProjectsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchProjectsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/projects";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProjects", response, ExceptionFactory);
        }

        /// <summary>Searches project comments and retains the complete JSON response.</summary>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's project and comment read permissions. No fixed response schema is declared by the manufacturer.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchProjectComments(long projectId, SearchRequest query = null)
        {
            return SearchProjectCommentsWithHttpInfo(projectId, query).Data;
        }

        /// <inheritdoc cref="SearchProjectComments"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchProjectCommentsWithHttpInfo(long projectId, SearchRequest query = null)
        {
            var localVarPath = "/project/{projectId}/comments";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProjectComments", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProjectComments"/>
        public Task<JToken> SearchProjectCommentsAsync(long projectId, SearchRequest query = null)
        {
            return SearchProjectCommentsAsync(projectId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectComments"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="projectId">The project identifier used in the request path.</param>
        public async Task<JToken> SearchProjectCommentsAsync(long projectId, SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchProjectCommentsAsyncWithHttpInfo(projectId, query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectCommentsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchProjectCommentsAsyncWithHttpInfo(long projectId, SearchRequest query = null)
        {
            return SearchProjectCommentsAsyncWithHttpInfo(projectId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectCommentsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="projectId">The project identifier used in the request path.</param>
        public async Task<ApiResponse<JToken>> SearchProjectCommentsAsyncWithHttpInfo(long projectId, SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/project/{projectId}/comments";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProjectComments", response, ExceptionFactory);
        }

    }
}
