using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OrderApi
    {
        /// <summary>Retrieves the complete JSON parent of a selected order.</summary>
        /// <param name="number">The original explicitly selected document number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The current parent route selects a document number, unlike the internal-number terminology of document details. The server requires Aufträge (Anzeigen) and supplies no fixed success schema. HTTP 404 can mean an unknown document, missing authorization or no parent. Copied manufacturer descriptions do not establish the parent document type or resolve the order parameter text mentioning an offer; no identifier mapping or automatic traversal is inferred. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The document number is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetOrderParentDetails(string number, string fields = null)
        {
            return GetOrderParentDetailsWithHttpInfo(number, fields).Data;
        }

        /// <inheritdoc cref="GetOrderParentDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetOrderParentDetailsWithHttpInfo(string number, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit document number is required.", nameof(number));
            var localVarPath = "/order/{number}/parent";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOrderParentDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOrderParentDetails"/>
        public Task<JToken> GetOrderParentDetailsAsync(string number, string fields = null)
        {
            return GetOrderParentDetailsAsync(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderParentDetails"/>
        /// <param name="number">The original explicitly selected document number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetOrderParentDetailsAsync(string number, string fields, CancellationToken cancellationToken)
        {
            return (await GetOrderParentDetailsAsyncWithHttpInfo(number, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOrderParentDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOrderParentDetailsAsyncWithHttpInfo(string number, string fields = null)
        {
            return GetOrderParentDetailsAsyncWithHttpInfo(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderParentDetailsWithHttpInfo"/>
        /// <param name="number">The original explicitly selected document number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOrderParentDetailsAsyncWithHttpInfo(string number, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit document number is required.", nameof(number));
            var localVarPath = "/order/{number}/parent";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOrderParentDetails", response, ExceptionFactory);
        }

    }
}
