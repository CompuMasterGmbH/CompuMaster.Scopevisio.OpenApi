using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class DispatchApi
    {
        /// <summary>Retrieves dispatch positions for one selected page and date range.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null omits the parameter and retains the server default.</param>
        /// <param name="pageSize">The optional entries per page, from 1 through 5000; null omits the parameter and retains the server default.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema and limits each response to at most 5000 entries. This call retrieves one page and does not automatically fetch remaining pages or prove completeness. Use explicit pagination or shorter date ranges. The server enforces profiles, visibility and range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An explicit page is negative or page size is outside 1 through 5000.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetDispatchPositionPage(string startDate, string endDate, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionPageWithHttpInfo(startDate, endDate, fields, page, pageSize).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionPage"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetDispatchPositionPageWithHttpInfo(string startDate, string endDate, string fields = null, int? page = null, int? pageSize = null)
        {
            var queryParams = PositionReadQuery.Create(startDate, endDate, fields, page, pageSize);
            var localVarPath = "/dispatch/all/positions";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDispatchPositionPage", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDispatchPositionPage"/>
        public Task<JToken> GetDispatchPositionPageAsync(string startDate, string endDate, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionPageAsync(startDate, endDate, fields, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionPage"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null omits the parameter and retains the server default.</param>
        /// <param name="pageSize">The optional entries per page, from 1 through 5000; null omits the parameter and retains the server default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetDispatchPositionPageAsync(string startDate, string endDate, string fields, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            return (await GetDispatchPositionPageAsyncWithHttpInfo(startDate, endDate, fields, page, pageSize, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionPageWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDispatchPositionPageAsyncWithHttpInfo(string startDate, string endDate, string fields = null, int? page = null, int? pageSize = null)
        {
            return GetDispatchPositionPageAsyncWithHttpInfo(startDate, endDate, fields, page, pageSize, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionPageWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null omits the parameter and retains the server default.</param>
        /// <param name="pageSize">The optional entries per page, from 1 through 5000; null omits the parameter and retains the server default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDispatchPositionPageAsyncWithHttpInfo(string startDate, string endDate, string fields, int? page, int? pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var queryParams = PositionReadQuery.Create(startDate, endDate, fields, page, pageSize);
            var localVarPath = "/dispatch/all/positions";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDispatchPositionPage", response, ExceptionFactory);
        }

    }
}
