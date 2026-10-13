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
    public partial class OpportunityApi
    {
        /// <summary>Searches opportunities and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Angebote, Aufträge, Lieferscheine and Rechnungen (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchOpportunities(SearchRequest query = null)
        {
            return SearchOpportunitiesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchOpportunities"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchOpportunitiesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/opportunities";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchOpportunities", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchOpportunities"/>
        public Task<JToken> SearchOpportunitiesAsync(SearchRequest query = null)
        {
            return SearchOpportunitiesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchOpportunities"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchOpportunitiesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchOpportunitiesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchOpportunitiesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchOpportunitiesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchOpportunitiesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchOpportunitiesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchOpportunitiesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/opportunities";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchOpportunities", response, ExceptionFactory);
        }

    }
}
