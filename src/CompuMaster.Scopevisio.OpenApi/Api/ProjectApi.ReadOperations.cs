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
        /// <summary>Searches time-entry runs and retains the complete JSON result.</summary>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema for this operation. Required account permissions are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTimeEntryRuns(SearchRequest query = null)
        {
            return SearchTimeEntryRunsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchTimeEntryRuns"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchTimeEntryRunsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/timeentryruns";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTimeEntryRuns", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTimeEntryRuns"/>
        public Task<JToken> SearchTimeEntryRunsAsync(SearchRequest query = null)
        {
            return SearchTimeEntryRunsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntryRuns"/>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchTimeEntryRunsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTimeEntryRunsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTimeEntryRunsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTimeEntryRunsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchTimeEntryRunsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntryRunsWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchTimeEntryRunsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/timeentryruns";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTimeEntryRuns", response, ExceptionFactory);
        }

        /// <summary>Searches time-entry run entries and retains the complete JSON result.</summary>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema for this operation. Required account permissions are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTimeEntryRunEntries(SearchRequest query = null)
        {
            return SearchTimeEntryRunEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchTimeEntryRunEntries"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchTimeEntryRunEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/timeentryrunentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTimeEntryRunEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTimeEntryRunEntries"/>
        public Task<JToken> SearchTimeEntryRunEntriesAsync(SearchRequest query = null)
        {
            return SearchTimeEntryRunEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntryRunEntries"/>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchTimeEntryRunEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTimeEntryRunEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTimeEntryRunEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTimeEntryRunEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchTimeEntryRunEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntryRunEntriesWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchTimeEntryRunEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/timeentryrunentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTimeEntryRunEntries", response, ExceptionFactory);
        }

    }
}
