using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
        /// <summary>Imports original DATEV posting data.</summary>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten). The route body is optional; a supplied object requires original Base64 data. Zone remains an original string without lookup or default selection; actual settings may use the standard zone. HTTP 200 has no fixed success schema and directs callers to response errors; no single-posting, per-row or aggregate success is inferred. Original file content is not decoded, parsed or repaired by the client. The server may create financial postings. Requests and future JSON are copied before HTTP, without extra calls, account-context selection or replay. Cancellation after sending cannot establish posting state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual posting/zone/profile behavior and reversible ownership remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or a supplied import object lacks data.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ImportDatevDataWithResult(DatevImportRequest request = null)
        {
            return ImportDatevDataWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportDatevDataWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ImportDatevDataWithResultWithHttpInfo(DatevImportRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/datevpostings/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportDatevDataWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportDatevDataWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ImportDatevDataWithResultAsync(DatevImportRequest request = null)
        {
            return ImportDatevDataWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportDatevDataWithResult"/>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created financial postings.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ImportDatevDataWithResultAsync(DatevImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportDatevDataWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportDatevDataWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportDatevDataWithResultAsyncWithHttpInfo(DatevImportRequest request = null)
        {
            return ImportDatevDataWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportDatevDataWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created financial postings.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportDatevDataWithResultAsyncWithHttpInfo(DatevImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/datevpostings/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportDatevDataWithResult", response, ExceptionFactory);
        }
    }
}
