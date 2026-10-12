using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OfferApi
    {
        /// <summary>Retrieves the complete JSON details of a selected offer.</summary>
        /// <param name="number">The original explicitly selected internal offer number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Angebote (Anzeigen) profile and supplies no fixed success schema or exhaustive error-status list. The original internal number is used without inferring an external document number. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The internal number is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetOfferDetails(string number, string fields = null)
        {
            return GetOfferDetailsWithHttpInfo(number, fields).Data;
        }

        /// <inheritdoc cref="GetOfferDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetOfferDetailsWithHttpInfo(string number, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOfferDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOfferDetails"/>
        public Task<JToken> GetOfferDetailsAsync(string number, string fields = null)
        {
            return GetOfferDetailsAsync(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferDetails"/>
        /// <param name="number">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetOfferDetailsAsync(string number, string fields, CancellationToken cancellationToken)
        {
            return (await GetOfferDetailsAsyncWithHttpInfo(number, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOfferDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOfferDetailsAsyncWithHttpInfo(string number, string fields = null)
        {
            return GetOfferDetailsAsyncWithHttpInfo(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferDetailsWithHttpInfo"/>
        /// <param name="number">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOfferDetailsAsyncWithHttpInfo(string number, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOfferDetails", response, ExceptionFactory);
        }

    }
}
