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
        /// <summary>Adds explicitly supplied original file data to a selected journal entry.</summary>
        /// <param name="documentNumber">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body; a supplied object requires data.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Bearbeiten), selected-document and file-content semantics. The route body is optional; a supplied object requires data. Original Base64 text and filename omission remain unchanged without decoding, content conversion, filename or PDF defaults, whole-document reads or identifier lookup. No account selection, chained write or mutation replay is added. Cancellation after sending cannot establish whether the attachment was added. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Existing FileForm and generated APIs remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required document selector is null.</exception>
        /// <exception cref="ArgumentException">A supplied object lacks a data string, a known field has an incompatible JSON kind, or the document selector is a dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AddJournalFileWithResult(string documentNumber, FileWriteRequest request = null)
        {
            return AddJournalFileWithResultWithHttpInfo(documentNumber, request).Data;
        }

        /// <inheritdoc cref="AddJournalFileWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AddJournalFileWithResultWithHttpInfo(string documentNumber, FileWriteRequest request = null)
        {
            if (documentNumber == null) throw new ArgumentNullException(nameof(documentNumber));
            if (documentNumber == "." || documentNumber == "..") throw new ArgumentException("The document selector cannot be a path dot segment.", nameof(documentNumber));
            var body = request?.SnapshotForSend();
            var localVarPath = "/journal/{documentNumber}/file/new";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AddJournalFileWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AddJournalFileWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AddJournalFileWithResultAsync(string documentNumber, FileWriteRequest request = null)
        {
            return AddJournalFileWithResultAsync(documentNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddJournalFileWithResult"/>
        /// <param name="documentNumber">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added the file.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AddJournalFileWithResultAsync(string documentNumber, FileWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AddJournalFileWithResultAsyncWithHttpInfo(documentNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AddJournalFileWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddJournalFileWithResultAsyncWithHttpInfo(string documentNumber, FileWriteRequest request = null)
        {
            return AddJournalFileWithResultAsyncWithHttpInfo(documentNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddJournalFileWithResultWithHttpInfo"/>
        /// <param name="documentNumber">The original document-number string, escaped as one segment without trimming, parsing, leading-zero removal, ID conversion or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete original file JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added the file.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddJournalFileWithResultAsyncWithHttpInfo(string documentNumber, FileWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (documentNumber == null) throw new ArgumentNullException(nameof(documentNumber));
            if (documentNumber == "." || documentNumber == "..") throw new ArgumentException("The document selector cannot be a path dot segment.", nameof(documentNumber));
            var body = request?.SnapshotForSend();
            var localVarPath = "/journal/{documentNumber}/file/new";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AddJournalFileWithResult", response, ExceptionFactory);
        }
    }
}
