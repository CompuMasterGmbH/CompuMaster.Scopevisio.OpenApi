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
    public partial class CreditApi
    {
        /// <summary>Searches credits and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Rechnungen (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchCredits(SearchRequest query = null)
        {
            return SearchCreditsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchCredits"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchCreditsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/credits";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCredits", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCredits"/>
        public Task<JToken> SearchCreditsAsync(SearchRequest query = null)
        {
            return SearchCreditsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCredits"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchCreditsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCreditsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCreditsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCreditsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchCreditsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCreditsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchCreditsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/credits";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCredits", response, ExceptionFactory);
        }

    }
}
