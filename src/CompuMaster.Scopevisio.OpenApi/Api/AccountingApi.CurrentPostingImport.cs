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
        /// <summary>Imports explicitly supplied JSON posting rows.</summary>
        /// <param name="request">The optional complete copied import JSON; null sends no body without account, date, tax, flag, row or dimension defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with PostingsNewResponse status and original count strings, including complete future data.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten), chart settings, the two-or-more-row posting requirement and a summary account for personal accounts. Body and rows are optional in the primary schema; supplied rows require original postingDate, documentNumber, account and amount. Current row and dimension validation is shared with the unchanged correction-import implementation; optional raw nulls remain available and actual field behavior is server-authoritative. SKR03 examples do not choose chart, accounts, VAT, currency, dimensions, payment terms or documents. No account lookup, personal-account inference, tax calculation, balancing, default dates/rows/flags, additional write, account-context selection or replay occurs. Explicit adjustVatKey and autoCreateTax may request server VAT substitution or tax-row generation; cancelDocument remains an original server-evaluated cancellation reference. HTTP 200 declares optional Int32 status and original insertCount/updateCount strings in PostingsNewResponse, without proving aggregate/per-row success, created state or atomicity. All response and future JSON remain available; counts are not converted. Requests and nested JSON are copied before HTTP. Cancellation after sending cannot establish resulting posting state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual financial behavior and owned reversible cleanup remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind, Int64 width or nonfinite number, a supplied row lacks a required field, or a supplied row or dimension is not an object.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ImportPostingsWithResult(PostingImportRequest request = null)
        {
            return ImportPostingsWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportPostingsWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ImportPostingsWithResultWithHttpInfo(PostingImportRequest request = null)
        {
            var body = SnapshotPostingMutation(request, 2);
            var localVarPath = "/postings/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportPostingsWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportPostingsWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ImportPostingsWithResultAsync(PostingImportRequest request = null)
        {
            return ImportPostingsWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportPostingsWithResult"/>
        /// <param name="request">The optional complete copied import JSON; null sends no body without account, date, tax, flag, row or dimension defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created financial postings.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ImportPostingsWithResultAsync(PostingImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportPostingsWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportPostingsWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportPostingsWithResultAsyncWithHttpInfo(PostingImportRequest request = null)
        {
            return ImportPostingsWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportPostingsWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied import JSON; null sends no body without account, date, tax, flag, row or dimension defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created financial postings.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportPostingsWithResultAsyncWithHttpInfo(PostingImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPostingMutation(request, 2);
            var localVarPath = "/postings/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportPostingsWithResult", response, ExceptionFactory);
        }
    }
}
