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
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Adds explicitly supplied original file data to a selected outgoing invoice.</summary>
        /// <param name="number">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body; a supplied object requires data.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Rechnungen (Bearbeiten), selected-document and file-content semantics. The route body is optional; a supplied object requires data. Original Base64 text and filename omission remain unchanged without decoding, content conversion, filename or PDF defaults, whole-document reads or identifier lookup. No account selection, chained write or mutation replay is added. Cancellation after sending cannot establish whether the attachment was added. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Existing FileForm and generated APIs remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required document selector is null.</exception>
        /// <exception cref="ArgumentException">A supplied object lacks a data string, a known field has an incompatible JSON kind, or the document selector is a dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AddFileToOutgoingInvoiceWithResult(string number, FileWriteRequest request = null)
        {
            return AddFileToOutgoingInvoiceWithResultWithHttpInfo(number, request).Data;
        }

        /// <inheritdoc cref="AddFileToOutgoingInvoiceWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AddFileToOutgoingInvoiceWithResultWithHttpInfo(string number, FileWriteRequest request = null)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number == "." || number == "..") throw new ArgumentException("The document selector cannot be a path dot segment.", nameof(number));
            var body = request?.SnapshotForSend();
            var localVarPath = "/outgoinginvoice/{number}/file/new";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AddFileToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AddFileToOutgoingInvoiceWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AddFileToOutgoingInvoiceWithResultAsync(string number, FileWriteRequest request = null)
        {
            return AddFileToOutgoingInvoiceWithResultAsync(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddFileToOutgoingInvoiceWithResult"/>
        /// <param name="number">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added the file.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AddFileToOutgoingInvoiceWithResultAsync(string number, FileWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AddFileToOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, FileWriteRequest request = null)
        {
            return AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddFileToOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added the file.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, FileWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number == "." || number == "..") throw new ArgumentException("The document selector cannot be a path dot segment.", nameof(number));
            var body = request?.SnapshotForSend();
            var localVarPath = "/outgoinginvoice/{number}/file/new";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AddFileToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
