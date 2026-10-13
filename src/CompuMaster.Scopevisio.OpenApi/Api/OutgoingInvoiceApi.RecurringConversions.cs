using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
        /// <summary>Invokes conversion of the selected outgoing invoice to a recurring invoice and retains the complete result.</summary>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="request">The optional copied scheduling request; null omits the body, while a supplied request requires all four scheduling fields.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. An absent optional body is omitted. A supplied body requires original Int64 validFrom, validTo and nextInvoiceDate epoch milliseconds plus one declared frequency; no zero, monthly, date conversion, range ordering or example boundary default is inserted. Requests are copied and serialized before admission. No query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The selector is null, blank or an exact dot segment, or supplied scheduling fields have invalid kinds, widths or frequency choices.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertInvoiceToRecurringInvoiceWithResult(string number, RecurringInvoiceConversionRequest request = null)
        {
            return ConvertInvoiceToRecurringInvoiceWithResultWithHttpInfo(number, request).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToRecurringInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertInvoiceToRecurringInvoiceWithResultWithHttpInfo(string number, RecurringInvoiceConversionRequest request = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToRecurringInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), RecurringConversionBody.Snapshot(request), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertInvoiceToRecurringInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertInvoiceToRecurringInvoiceWithResult"/>
        public Task<JToken> ConvertInvoiceToRecurringInvoiceWithResultAsync(string number, RecurringInvoiceConversionRequest request = null)
        {
            return ConvertInvoiceToRecurringInvoiceWithResultAsync(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToRecurringInvoiceWithResult"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="request">The optional copied scheduling request; null omits the body, while a supplied request requires all four scheduling fields.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertInvoiceToRecurringInvoiceWithResultAsync(string number, RecurringInvoiceConversionRequest request, CancellationToken cancellationToken)
        {
            return (await ConvertInvoiceToRecurringInvoiceWithResultAsyncWithHttpInfo(number, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToRecurringInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertInvoiceToRecurringInvoiceWithResultAsyncWithHttpInfo(string number, RecurringInvoiceConversionRequest request = null)
        {
            return ConvertInvoiceToRecurringInvoiceWithResultAsyncWithHttpInfo(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToRecurringInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="request">The optional copied scheduling request; null omits the body, while a supplied request requires all four scheduling fields.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertInvoiceToRecurringInvoiceWithResultAsyncWithHttpInfo(string number, RecurringInvoiceConversionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToRecurringInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), RecurringConversionBody.Snapshot(request), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertInvoiceToRecurringInvoiceWithResult", response, ExceptionFactory);
        }

    }
}
