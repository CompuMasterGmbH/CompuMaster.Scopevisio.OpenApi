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
        /// <summary>Retrieves the filters for the specified type.</summary>
        /// <param name="type">The required filter type used in the endpoint path.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema for this operation. Required account permissions are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        /// <exception cref="ArgumentException">The required filter type is null or empty.</exception>
        public JToken GetFilters(string type)
        {
            return GetFiltersWithHttpInfo(type).Data;
        }

        /// <inheritdoc cref="GetFilters"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> GetFiltersWithHttpInfo(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("The filter type is required.", nameof(type));
            var localVarPath = "/filters/{type}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(type));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetFilters", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetFilters"/>
        public Task<JToken> GetFiltersAsync(string type)
        {
            return GetFiltersAsync(type, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFilters"/>
        /// <param name="type">The required filter type used in the endpoint path.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetFiltersAsync(string type, CancellationToken cancellationToken)
        {
            return (await GetFiltersAsyncWithHttpInfo(type, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetFiltersWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetFiltersAsyncWithHttpInfo(string type)
        {
            return GetFiltersAsyncWithHttpInfo(type, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFiltersWithHttpInfo"/>
        /// <param name="type">The required filter type used in the endpoint path.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetFiltersAsyncWithHttpInfo(string type, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("The filter type is required.", nameof(type));
            var localVarPath = "/filters/{type}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(type));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetFilters", response, ExceptionFactory);
        }

    }
}
