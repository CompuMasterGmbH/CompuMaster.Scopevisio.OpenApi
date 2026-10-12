using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProductApi
    {
        /// <summary>Retrieves typed e-commerce SKUs for the selected product.</summary>
        /// <param name="id">The required Int64 product identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response or JSON-null reply.</returns>
        /// <remarks>The vendor requires the e-commerce setting for displaying extended product fields. Missing settings/profiles remain original server diagnostics; the client does not change settings or infer access. Unknown fields, date-like strings and exact raw responses remain available. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Profiles, visibility and server-specific formats remain server-enforced; no completeness or extra paging is inferred.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible typed object/record shape or a fraction exceeding the decimal range.</exception>
        public ECommerceSkusResponse GetECommerceSkus(long id, string fields = null)
        {
            return GetECommerceSkusWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetECommerceSkus"/>
        /// <returns>The status, headers, exact raw response and complete JSON.</returns>
        public ApiResponse<ECommerceSkusResponse> GetECommerceSkusWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/product/{id}/ecommerceskus";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapECommerceSkuResponse(SearchJsonResponse.Parse("GetECommerceSkus", response, ExceptionFactory));
        }

        /// <inheritdoc cref="GetECommerceSkus"/>
        public Task<ECommerceSkusResponse> GetECommerceSkusAsync(long id, string fields = null)
        {
            return GetECommerceSkusAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetECommerceSkus"/>
        /// <param name="id">The required Int64 product identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ECommerceSkusResponse> GetECommerceSkusAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetECommerceSkusAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetECommerceSkusWithHttpInfo"/>
        public Task<ApiResponse<ECommerceSkusResponse>> GetECommerceSkusAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetECommerceSkusAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetECommerceSkusWithHttpInfo"/>
        /// <param name="id">The required Int64 product identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<ECommerceSkusResponse>> GetECommerceSkusAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/product/{id}/ecommerceskus";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapECommerceSkuResponse(SearchJsonResponse.Parse("GetECommerceSkus", response, ExceptionFactory));
        }

        private static ApiResponse<ECommerceSkusResponse> WrapECommerceSkuResponse(ApiResponse<JToken> response)
        {
            ECommerceSkusResponse data = null;
            if (response.Data != null && response.Data.Type != JTokenType.Null)
            {
                if (!(response.Data is JObject value)) throw new JsonSerializationException("The e-commerce SKU response must be a JSON object.");
                data = new ECommerceSkusResponse(value);
            }
            return new ApiResponse<ECommerceSkusResponse>(response.StatusCode, response.Headers, response.RawJsonContent, data);
        }
    }
}
