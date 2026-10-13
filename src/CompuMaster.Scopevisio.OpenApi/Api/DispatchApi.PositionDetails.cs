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
    public partial class DispatchApi
    {
        /// <summary>Retrieves the complete JSON details of a selected dispatch note.</summary>
        /// <param name="number">The original explicitly selected internal dispatch note number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="page">The optional zero-based page number; null omits it and an explicit Int32 value is retained.</param>
        /// <param name="pageSize">The optional requested page size; null omits it. The server documents a maximum of 5,000 and validates the original value.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires Lieferscheine (Anzeigen) and supplies no fixed success schema. The documented first page is zero, maximum requested size and returned entry count are 5,000; callers must select pages explicitly. No page defaults, local ranges, clamping, automatic pagination or total/envelope interpretation are inferred. HTTP 404 can mean a missing document or authorization; copied opportunity error text mentions an offer and remains server-authoritative. Original internal-number strings, fields and supplied Int32 paging values are forwarded without conversion or lookup. Existing generated methods and null-data defaults remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The internal number is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetDispatchPositionsDetails(string number, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionsDetailsWithHttpInfo(number, fields, page, pageSize).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionsDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetDispatchPositionsDetailsWithHttpInfo(string number, string fields = null, int? page = null, int? pageSize = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal dispatch note number is required.", nameof(number));
            var localVarPath = "/dispatch/{number}/positions";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (page.HasValue) queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue) queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDispatchPositionsDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDispatchPositionsDetails"/>
        public Task<JToken> GetDispatchPositionsDetailsAsync(string number, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionsDetailsAsync(number, fields, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionsDetails"/>
        /// <param name="number">The original explicitly selected internal dispatch note number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="page">The optional zero-based page number; null omits it and an explicit Int32 value is retained.</param>
        /// <param name="pageSize">The optional requested page size; null omits it. The server documents a maximum of 5,000 and validates the original value.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetDispatchPositionsDetailsAsync(string number, string fields, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            return (await GetDispatchPositionsDetailsAsyncWithHttpInfo(number, fields, page, pageSize, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionsDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDispatchPositionsDetailsAsyncWithHttpInfo(string number, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionsDetailsAsyncWithHttpInfo(number, fields, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionsDetailsWithHttpInfo"/>
        /// <param name="number">The original explicitly selected internal dispatch note number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="page">The optional zero-based page number; null omits it and an explicit Int32 value is retained.</param>
        /// <param name="pageSize">The optional requested page size; null omits it. The server documents a maximum of 5,000 and validates the original value.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDispatchPositionsDetailsAsyncWithHttpInfo(string number, string fields, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal dispatch note number is required.", nameof(number));
            var localVarPath = "/dispatch/{number}/positions";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (page.HasValue) queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue) queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDispatchPositionsDetails", response, ExceptionFactory);
        }

    }
}
