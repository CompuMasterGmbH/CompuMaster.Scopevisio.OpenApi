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
    public partial class RecurringInvoiceApi
    {
        /// <summary>Updates explicitly supplied recurrence data for an invoice.</summary>
        /// <param name="number">The original internal invoice-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied recurrence JSON; null sends no body and a supplied object requires all four original fields without date or frequency defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Rechnungen (Bearbeiten), recurring-invoice identity and actual date/frequency semantics. The route body is optional but its dedicated schema requires validFrom, validTo, frequency and nextInvoiceDate in a supplied complete object. Original signed Int64 epoch milliseconds and frequency text are retained without calendar/timezone/day truncation, current-year/default choices, date ordering, schedule expansion, next-invoice calculation or enum fallback. The manufacturer states time of day has no importance on the server; the client does not alter it. Original internal invoice-number strings are escaped without lookup, trimming, numeric conversion or leading-zero removal. HTTP 200 has no fixed successful schema; 404 may mean missing invoice or missing write permission. Complete response/future JSON remain available without invented IDs, success flags or future invoices. The request is copied before HTTP without account-context selection, extra writes, invoice creation or replay. Cancellation after sending cannot establish recurrence state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual invoice/recurrence/profile behavior and owned restoration remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required internal invoice-number selector is null.</exception>
        /// <exception cref="ArgumentException">A supplied object lacks a required field, a supplied field has an incompatible JSON kind or Int64 width, or the invoice-number selector is a dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateRecurringInvoiceWithResult(string number, RecurringInvoiceUpdateRequest request = null)
        {
            return UpdateRecurringInvoiceWithResultWithHttpInfo(number, request).Data;
        }

        /// <inheritdoc cref="UpdateRecurringInvoiceWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateRecurringInvoiceWithResultWithHttpInfo(string number, RecurringInvoiceUpdateRequest request = null)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number == "." || number == "..") throw new ArgumentException("The invoice-number selector cannot be a path dot segment.", nameof(number));
            var body = request?.SnapshotForSend();
            var localVarPath = "/recurringinvoice/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateRecurringInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateRecurringInvoiceWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateRecurringInvoiceWithResultAsync(string number, RecurringInvoiceUpdateRequest request = null)
        {
            return UpdateRecurringInvoiceWithResultAsync(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateRecurringInvoiceWithResult"/>
        /// <param name="number">The original internal invoice-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied recurrence JSON; null sends no body and a supplied object requires all four original fields without date or frequency defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed recurrence data.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateRecurringInvoiceWithResultAsync(string number, RecurringInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(number, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateRecurringInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(string number, RecurringInvoiceUpdateRequest request = null)
        {
            return UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateRecurringInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal invoice-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied recurrence JSON; null sends no body and a supplied object requires all four original fields without date or frequency defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed recurrence data.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(string number, RecurringInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number == "." || number == "..") throw new ArgumentException("The invoice-number selector cannot be a path dot segment.", nameof(number));
            var body = request?.SnapshotForSend();
            var localVarPath = "/recurringinvoice/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateRecurringInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
