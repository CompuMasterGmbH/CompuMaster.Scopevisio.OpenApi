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
        /// <summary>Searches time entries and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Zeiterfassung (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTimeEntries(SearchRequest query = null)
        {
            return SearchTimeEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchTimeEntries"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchTimeEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/timeentries";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTimeEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTimeEntries"/>
        public Task<JToken> SearchTimeEntriesAsync(SearchRequest query = null)
        {
            return SearchTimeEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntries"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchTimeEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTimeEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTimeEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTimeEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchTimeEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTimeEntriesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchTimeEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/timeentries";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTimeEntries", response, ExceptionFactory);
        }


        /// <summary>Searches the explicitly selected project team and retains the complete JSON response.</summary>
        /// <param name="projectId">The original signed Int64 project master identifier, formatted invariantly without lookup.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Kontakte, Projekte (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Original project IDs retain their complete signed Int64 range without a guessed minimum or account/identity lookup. Documented 403/404 diagnostics remain server-authoritative. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchProjectEmployees(long projectId, SearchRequest query = null)
        {
            return SearchProjectEmployeesWithHttpInfo(projectId, query).Data;
        }

        /// <inheritdoc cref="SearchProjectEmployees"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchProjectEmployeesWithHttpInfo(long projectId, SearchRequest query = null)
        {
            var localVarPath = "/project/{projectId}/projectemployees";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProjectEmployees", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProjectEmployees"/>
        public Task<JToken> SearchProjectEmployeesAsync(long projectId, SearchRequest query = null)
        {
            return SearchProjectEmployeesAsync(projectId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectEmployees"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="projectId">The original signed Int64 project master identifier, formatted invariantly without lookup.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchProjectEmployeesAsync(long projectId, SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchProjectEmployeesAsyncWithHttpInfo(projectId, query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectEmployeesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchProjectEmployeesAsyncWithHttpInfo(long projectId, SearchRequest query = null)
        {
            return SearchProjectEmployeesAsyncWithHttpInfo(projectId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectEmployeesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="projectId">The original signed Int64 project master identifier, formatted invariantly without lookup.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchProjectEmployeesAsyncWithHttpInfo(long projectId, SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/project/{projectId}/projectemployees";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProjectEmployees", response, ExceptionFactory);
        }

    }
}
