using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Retrieves the complete JSON details of a selected opportunity.</summary>
        /// <param name="number">The original explicitly selected internal opportunity number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires Rechnungen, Potenzial (Opportunities) (Anzeigen) and declares no fixed success schema or exhaustive error-status list. The current summary describes an internal opportunity number while copied path-parameter prose mentions an outgoing invoice; the original caller selection is retained without identifier lookup or conversion. No formatting, paging, automatic parent traversal or document creation is inferred. Existing generated methods, null-data defaults, authorization and selected account remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The internal number is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetOpportunityDetails(string number, string fields = null)
        {
            return GetOpportunityDetailsWithHttpInfo(number, fields).Data;
        }

        /// <inheritdoc cref="GetOpportunityDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetOpportunityDetailsWithHttpInfo(string number, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOpportunityDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOpportunityDetails"/>
        public Task<JToken> GetOpportunityDetailsAsync(string number, string fields = null)
        {
            return GetOpportunityDetailsAsync(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityDetails"/>
        /// <param name="number">The original explicitly selected internal opportunity number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetOpportunityDetailsAsync(string number, string fields, CancellationToken cancellationToken)
        {
            return (await GetOpportunityDetailsAsyncWithHttpInfo(number, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOpportunityDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOpportunityDetailsAsyncWithHttpInfo(string number, string fields = null)
        {
            return GetOpportunityDetailsAsyncWithHttpInfo(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityDetailsWithHttpInfo"/>
        /// <param name="number">The original explicitly selected internal opportunity number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOpportunityDetailsAsyncWithHttpInfo(string number, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOpportunityDetails", response, ExceptionFactory);
        }

    }
}
