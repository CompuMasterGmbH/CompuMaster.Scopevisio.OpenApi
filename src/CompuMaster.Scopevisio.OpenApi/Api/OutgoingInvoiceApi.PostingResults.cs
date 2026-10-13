using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Posts the explicitly selected outgoing invoice and retains the complete server response.</summary>
        /// <param name="number">The original internal outgoing-invoice number, escaped as one segment without trimming or conversion.</param>
        /// <returns>The complete JSON result if supplied, or null for an empty response; no fixed success schema is declared.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 400 can report an already posted invoice; HTTP 404 can mean a missing invoice or missing write permission. Success is documented as HTTP 200 without a response schema. HTTP-info forms retain the original raw content, status and headers. No body, query, lookup, financial date/account/period default or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken PostOutgoingInvoiceWithResult(string number)
        {
            return PostOutgoingInvoiceWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="PostOutgoingInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> PostOutgoingInvoiceWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing-invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/post";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("PostOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="PostOutgoingInvoiceWithResult"/>
        public Task<JToken> PostOutgoingInvoiceWithResultAsync(string number)
        {
            return PostOutgoingInvoiceWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="PostOutgoingInvoiceWithResult"/>
        /// <param name="number">The original internal outgoing-invoice number, escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> PostOutgoingInvoiceWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await PostOutgoingInvoiceWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="PostOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> PostOutgoingInvoiceWithResultAsyncWithHttpInfo(string number)
        {
            return PostOutgoingInvoiceWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="PostOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing-invoice number, escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> PostOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing-invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/post";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("PostOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
