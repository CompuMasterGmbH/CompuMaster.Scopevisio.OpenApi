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
    public partial class ProductApi
    {
        /// <summary>Searches products and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Produktverwaltung (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The declared ProductResponse has variable effective fields depending on listExtended and the existing e-commerce settings. Null listExtended omits the query and retains the false server default; the SDK neither changes settings nor infers effective fields or SKU support. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public JToken SearchProducts(SearchRequest query = null, bool? listExtended = null)
        {
            return SearchProductsWithHttpInfo(query, listExtended).Data;
        }

        /// <inheritdoc cref="SearchProducts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public ApiResponse<JToken> SearchProductsWithHttpInfo(SearchRequest query = null, bool? listExtended = null)
        {
            var queryParams = new List<KeyValuePair<string, string>>();
            if (listExtended.HasValue) queryParams.Add(new KeyValuePair<string, string>("listExtended", listExtended.Value ? "true" : "false"));
            var localVarPath = "/products";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProducts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProducts"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public Task<JToken> SearchProductsAsync(SearchRequest query = null, bool? listExtended = null)
        {
            return SearchProductsAsync(query, listExtended, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProducts"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public async Task<JToken> SearchProductsAsync(SearchRequest query, bool? listExtended, CancellationToken cancellationToken)
        {
            return (await SearchProductsAsyncWithHttpInfo(query, listExtended, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProductsWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public Task<ApiResponse<JToken>> SearchProductsAsyncWithHttpInfo(SearchRequest query = null, bool? listExtended = null)
        {
            return SearchProductsAsyncWithHttpInfo(query, listExtended, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProductsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="listExtended">The explicit extended-field choice, or null to omit the query and retain the server default.</param>
        public async Task<ApiResponse<JToken>> SearchProductsAsyncWithHttpInfo(SearchRequest query, bool? listExtended, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var queryParams = new List<KeyValuePair<string, string>>();
            if (listExtended.HasValue) queryParams.Add(new KeyValuePair<string, string>("listExtended", listExtended.Value ? "true" : "false"));
            var localVarPath = "/products";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProducts", response, ExceptionFactory);
        }

    }
}
