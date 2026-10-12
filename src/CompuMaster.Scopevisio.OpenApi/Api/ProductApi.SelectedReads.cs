using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Retrieves the complete JSON details of a selected product.</summary>
        /// <param name="id">The explicitly selected product master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="listExtended">Whether to request extended product fields, or null to omit the option and retain server/e-commerce settings; the documented option default is false.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>Omitting formatValues retains the server default of formatted values; explicit false requests unformatted values. The server requires the Produktverwaltung (Anzeigen) profile and declares no complete error-status list. The manufacturer documents ProductType, but the effective field set depends on listExtended and e-commerce settings. Complete original JSON is retained without inserting field defaults or converting formatted strings into numbers. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetProductDetails(long id, string fields = null, bool? formatValues = null, bool? listExtended = null)
        {
            return GetProductDetailsWithHttpInfo(id, fields, formatValues, listExtended).Data;
        }

        /// <inheritdoc cref="GetProductDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetProductDetailsWithHttpInfo(long id, string fields = null, bool? formatValues = null, bool? listExtended = null)
        {
            var localVarPath = "/product/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            if (listExtended.HasValue) queryParams.Add(new KeyValuePair<string, string>("listExtended", listExtended.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProductDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProductDetails"/>
        public Task<JToken> GetProductDetailsAsync(long id, string fields = null, bool? formatValues = null, bool? listExtended = null)
        {
            return GetProductDetailsAsync(id, fields, formatValues, listExtended, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductDetails"/>
        /// <param name="id">The explicitly selected product master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="listExtended">Whether to request extended product fields, or null to omit the option and retain server/e-commerce settings; the documented option default is false.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetProductDetailsAsync(long id, string fields, bool? formatValues, bool? listExtended, CancellationToken cancellationToken)
        {
            return (await GetProductDetailsAsyncWithHttpInfo(id, fields, formatValues, listExtended, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProductDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProductDetailsAsyncWithHttpInfo(long id, string fields = null, bool? formatValues = null, bool? listExtended = null)
        {
            return GetProductDetailsAsyncWithHttpInfo(id, fields, formatValues, listExtended, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected product master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="listExtended">Whether to request extended product fields, or null to omit the option and retain server/e-commerce settings; the documented option default is false.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProductDetailsAsyncWithHttpInfo(long id, string fields, bool? formatValues, bool? listExtended, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/product/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            if (listExtended.HasValue) queryParams.Add(new KeyValuePair<string, string>("listExtended", listExtended.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProductDetails", response, ExceptionFactory);
        }

    }
}
