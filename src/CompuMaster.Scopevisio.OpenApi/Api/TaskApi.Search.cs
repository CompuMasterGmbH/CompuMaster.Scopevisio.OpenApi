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
    public partial class TaskApi
    {
        /// <summary>Searches tasks and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's task read permissions. No fixed response schema is declared by the manufacturer.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTasks(SearchRequest query = null)
        {
            return SearchTasksWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchTasks"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchTasksWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/tasks";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTasks", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTasks"/>
        public Task<JToken> SearchTasksAsync(SearchRequest query = null)
        {
            return SearchTasksAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTasks"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchTasksAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTasksAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTasksWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTasksAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchTasksAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTasksWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchTasksAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/tasks";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTasks", response, ExceptionFactory);
        }

        /// <summary>Searches task comments and retains the complete JSON response.</summary>
        /// <param name="taskId">The task master ID.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's task and comment read permissions. No fixed response schema is declared by the manufacturer.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTaskComments(long taskId, SearchRequest query = null)
        {
            return SearchTaskCommentsWithHttpInfo(taskId, query).Data;
        }

        /// <inheritdoc cref="SearchTaskComments"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchTaskCommentsWithHttpInfo(long taskId, SearchRequest query = null)
        {
            var localVarPath = "/task/{taskId}/comments";
            localVarPath = localVarPath.Replace("{taskId}", Uri.EscapeDataString(taskId.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTaskComments", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTaskComments"/>
        public Task<JToken> SearchTaskCommentsAsync(long taskId, SearchRequest query = null)
        {
            return SearchTaskCommentsAsync(taskId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTaskComments"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="taskId">The task identifier used in the request path.</param>
        public async Task<JToken> SearchTaskCommentsAsync(long taskId, SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTaskCommentsAsyncWithHttpInfo(taskId, query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTaskCommentsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTaskCommentsAsyncWithHttpInfo(long taskId, SearchRequest query = null)
        {
            return SearchTaskCommentsAsyncWithHttpInfo(taskId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTaskCommentsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="taskId">The task identifier used in the request path.</param>
        public async Task<ApiResponse<JToken>> SearchTaskCommentsAsyncWithHttpInfo(long taskId, SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/task/{taskId}/comments";
            localVarPath = localVarPath.Replace("{taskId}", Uri.EscapeDataString(taskId.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTaskComments", response, ExceptionFactory);
        }

    }
}
