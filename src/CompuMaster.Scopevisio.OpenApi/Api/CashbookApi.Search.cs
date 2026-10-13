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
    public partial class CashbookApi
    {
        /// <summary>Searches cashbook settings and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Kassenbücher Dashboard (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchCashbooks(SearchRequest query = null)
        {
            return SearchCashbooksWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchCashbooks"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchCashbooksWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/cashbooks";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCashbooks", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCashbooks"/>
        public Task<JToken> SearchCashbooksAsync(SearchRequest query = null)
        {
            return SearchCashbooksAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCashbooks"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchCashbooksAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCashbooksAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCashbooksWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCashbooksAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchCashbooksAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCashbooksWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchCashbooksAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/cashbooks";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCashbooks", response, ExceptionFactory);
        }

        /// <summary>Searches cashbook sheets and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Abgeschlossene Kassenblätter and Kassenbewegung eingeben (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchCashbookSheets(SearchRequest query = null)
        {
            return SearchCashbookSheetsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchCashbookSheets"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchCashbookSheetsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/cashbooksheets";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCashbookSheets", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCashbookSheets"/>
        public Task<JToken> SearchCashbookSheetsAsync(SearchRequest query = null)
        {
            return SearchCashbookSheetsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCashbookSheets"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchCashbookSheetsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCashbookSheetsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCashbookSheetsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCashbookSheetsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchCashbookSheetsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCashbookSheetsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchCashbookSheetsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/cashbooksheets";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCashbookSheets", response, ExceptionFactory);
        }

    }
}
