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
    public partial class AccountingApi
    {
        /// <summary>Retrieves the complete JSON details of a selected dimension.</summary>
        /// <param name="dimensionNameOrNumber">The explicitly selected original dimension name or number, escaped without trimming or numeric conversion.</param>
        /// <param name="page">The original optional zero-based page selector; null omits it.</param>
        /// <param name="pageSize">The original optional page size; null omits it. The documented server maximum is 1,000.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Berichte (Anzeigen) profile. The first page is zero and the documented server page-size maximum is 1,000. The manufacturer supplies no fixed success schema or schema range constraints. Original nullable Int32 values are retained for server validation; no paging defaults are inserted. HTTP 404 can mean a missing dimension name or an unknown dimension. Existing generated methods retain their null-data behavior. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The dimension selector is blank or a relative path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetDimensionEntriesDetails(string dimensionNameOrNumber, int? page = null, int? pageSize = null)
        {
            return GetDimensionEntriesDetailsWithHttpInfo(dimensionNameOrNumber, page, pageSize).Data;
        }

        /// <inheritdoc cref="GetDimensionEntriesDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetDimensionEntriesDetailsWithHttpInfo(string dimensionNameOrNumber, int? page = null, int? pageSize = null)
        {
            if (string.IsNullOrWhiteSpace(dimensionNameOrNumber) || dimensionNameOrNumber == "." || dimensionNameOrNumber == "..")
                throw new ArgumentException("An explicit dimension name or number is required.", nameof(dimensionNameOrNumber));
            var localVarPath = "/dimensions/{dimensionNameOrNumber}/dimensionentries";
            localVarPath = localVarPath.Replace("{dimensionNameOrNumber}", Uri.EscapeDataString(dimensionNameOrNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (page.HasValue) queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue) queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDimensionEntriesDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDimensionEntriesDetails"/>
        public Task<JToken> GetDimensionEntriesDetailsAsync(string dimensionNameOrNumber, int? page = null, int? pageSize = null)
        {
            return GetDimensionEntriesDetailsAsync(dimensionNameOrNumber, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDimensionEntriesDetails"/>
        /// <param name="dimensionNameOrNumber">The explicitly selected original dimension name or number, escaped without trimming or numeric conversion.</param>
        /// <param name="page">The original optional zero-based page selector; null omits it.</param>
        /// <param name="pageSize">The original optional page size; null omits it. The documented server maximum is 1,000.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetDimensionEntriesDetailsAsync(string dimensionNameOrNumber, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            return (await GetDimensionEntriesDetailsAsyncWithHttpInfo(dimensionNameOrNumber, page, pageSize, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDimensionEntriesDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDimensionEntriesDetailsAsyncWithHttpInfo(string dimensionNameOrNumber, int? page = null, int? pageSize = null)
        {
            return GetDimensionEntriesDetailsAsyncWithHttpInfo(dimensionNameOrNumber, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDimensionEntriesDetailsWithHttpInfo"/>
        /// <param name="dimensionNameOrNumber">The explicitly selected original dimension name or number, escaped without trimming or numeric conversion.</param>
        /// <param name="page">The original optional zero-based page selector; null omits it.</param>
        /// <param name="pageSize">The original optional page size; null omits it. The documented server maximum is 1,000.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDimensionEntriesDetailsAsyncWithHttpInfo(string dimensionNameOrNumber, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(dimensionNameOrNumber) || dimensionNameOrNumber == "." || dimensionNameOrNumber == "..")
                throw new ArgumentException("An explicit dimension name or number is required.", nameof(dimensionNameOrNumber));
            var localVarPath = "/dimensions/{dimensionNameOrNumber}/dimensionentries";
            localVarPath = localVarPath.Replace("{dimensionNameOrNumber}", Uri.EscapeDataString(dimensionNameOrNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (page.HasValue) queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue) queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDimensionEntriesDetails", response, ExceptionFactory);
        }

    }
}
