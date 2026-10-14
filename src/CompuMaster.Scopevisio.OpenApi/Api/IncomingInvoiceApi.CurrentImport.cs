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
    public partial class IncomingInvoiceApi
    {
        /// <summary>Imports original incoming-invoice file data.</summary>
        /// <param name="request">The optional complete copied invoice JSON; null sends no body without filename, PDF or invoice defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Rechnungseingangsbuch (Bearbeiten). Body is optional and filename/data are not individually schema-required, although server 404 diagnoses missing filename or data. Original filename and Base64 text are retained without file access, decoding, format repair, filename inference, PDF validation, account-context selection, extra writes or replay. The server expects a Base64 PDF; the route description says 30 MB while data/error descriptions say 20 MB, so accepted size and units remain unverified and no guessed threshold is imposed. HTTP 200 has no fixed successful schema; complete response/future JSON remain available without invented invoice IDs or success flags. Requests and future data are copied before HTTP. Cancellation after sending cannot establish resulting invoice state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual PDF/size/invoice/profile acceptance and owned reversible cleanup remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied filename or data field has an incompatible JSON kind.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateIncomingInvoiceWithResult(IncomingInvoiceImportRequest request = null)
        {
            return CreateIncomingInvoiceWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateIncomingInvoiceWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateIncomingInvoiceWithResultWithHttpInfo(IncomingInvoiceImportRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/incominginvoice/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateIncomingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateIncomingInvoiceWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateIncomingInvoiceWithResultAsync(IncomingInvoiceImportRequest request = null)
        {
            return CreateIncomingInvoiceWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateIncomingInvoiceWithResult"/>
        /// <param name="request">The optional complete copied invoice JSON; null sends no body without filename, PDF or invoice defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported the invoice.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateIncomingInvoiceWithResultAsync(IncomingInvoiceImportRequest request, CancellationToken cancellationToken)
        {
            return (await CreateIncomingInvoiceWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateIncomingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateIncomingInvoiceWithResultAsyncWithHttpInfo(IncomingInvoiceImportRequest request = null)
        {
            return CreateIncomingInvoiceWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateIncomingInvoiceWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied invoice JSON; null sends no body without filename, PDF or invoice defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported the invoice.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateIncomingInvoiceWithResultAsyncWithHttpInfo(IncomingInvoiceImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/incominginvoice/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateIncomingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
