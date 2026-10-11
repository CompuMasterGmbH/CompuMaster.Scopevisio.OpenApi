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
    public partial class AdditionalApi
    {
        /// <summary>Retrieves the statistics units using the requested language.</summary>
        /// <param name="language">The language value, such as "default" or "english"; null retains the server default.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema for this operation. Required account permissions are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetStatisticsUnits(string language = null)
        {
            return GetStatisticsUnitsWithHttpInfo(language).Data;
        }

        /// <inheritdoc cref="GetStatisticsUnits"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> GetStatisticsUnitsWithHttpInfo(string language = null)
        {
            var localVarPath = "/catalog/accounting/statisticsunit";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (language != null) queryParams.Add(new KeyValuePair<string, string>("language", language));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetStatisticsUnits", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetStatisticsUnits"/>
        public Task<JToken> GetStatisticsUnitsAsync(string language = null)
        {
            return GetStatisticsUnitsAsync(language, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsUnits"/>
        /// <param name="language">The language value, such as "default" or "english"; null retains the server default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetStatisticsUnitsAsync(string language, CancellationToken cancellationToken)
        {
            return (await GetStatisticsUnitsAsyncWithHttpInfo(language, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetStatisticsUnitsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetStatisticsUnitsAsyncWithHttpInfo(string language = null)
        {
            return GetStatisticsUnitsAsyncWithHttpInfo(language, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsUnitsWithHttpInfo"/>
        /// <param name="language">The language value, such as "default" or "english"; null retains the server default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetStatisticsUnitsAsyncWithHttpInfo(string language, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/catalog/accounting/statisticsunit";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (language != null) queryParams.Add(new KeyValuePair<string, string>("language", language));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetStatisticsUnits", response, ExceptionFactory);
        }

    }
}
