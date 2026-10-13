using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AccountingApi
    {
        /// <summary>Requests cancellation of the explicitly selected journal posting and retains the complete server response.</summary>
        /// <param name="documentNumber">The original journal document number (Buchungsnummer), escaped as one segment without trimming or conversion.</param>
        /// <returns>The complete JSON result if supplied, or null for an empty response; no fixed success schema is declared.</returns>
        /// <remarks>The server requires Journal (Bearbeiten). HTTP 200 can contain validation errors because periods or accounts are closed, so callers must inspect the complete result rather than infer successful cancellation from status. An already cancelled posting returns its cancellation document number. HTTP 400 can mean the selected posting is itself a cancellation posting; HTTP 404 means the posting was not found. Success is documented as HTTP 200 without a response schema. HTTP-info forms retain the original raw content, status and headers. No body, query, lookup, financial date/account/period default or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken CancelPostingDocumentWithResult(string documentNumber)
        {
            return CancelPostingDocumentWithResultWithHttpInfo(documentNumber).Data;
        }

        /// <inheritdoc cref="CancelPostingDocumentWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> CancelPostingDocumentWithResultWithHttpInfo(string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber == "." || documentNumber == "..")
                throw new ArgumentException("An explicit journal document number is required.", nameof(documentNumber));
            var localVarPath = "/journal/{documentNumber}/cancel";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CancelPostingDocumentWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CancelPostingDocumentWithResult"/>
        public Task<JToken> CancelPostingDocumentWithResultAsync(string documentNumber)
        {
            return CancelPostingDocumentWithResultAsync(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="CancelPostingDocumentWithResult"/>
        /// <param name="documentNumber">The original journal document number (Buchungsnummer), escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> CancelPostingDocumentWithResultAsync(string documentNumber, CancellationToken cancellationToken)
        {
            return (await CancelPostingDocumentWithResultAsyncWithHttpInfo(documentNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CancelPostingDocumentWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CancelPostingDocumentWithResultAsyncWithHttpInfo(string documentNumber)
        {
            return CancelPostingDocumentWithResultAsyncWithHttpInfo(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="CancelPostingDocumentWithResultWithHttpInfo"/>
        /// <param name="documentNumber">The original journal document number (Buchungsnummer), escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> CancelPostingDocumentWithResultAsyncWithHttpInfo(string documentNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber == "." || documentNumber == "..")
                throw new ArgumentException("An explicit journal document number is required.", nameof(documentNumber));
            var localVarPath = "/journal/{documentNumber}/cancel";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CancelPostingDocumentWithResult", response, ExceptionFactory);
        }
    }
}
