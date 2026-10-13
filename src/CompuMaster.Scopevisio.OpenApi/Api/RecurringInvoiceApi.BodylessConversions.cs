using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class RecurringInvoiceApi
    {
        /// <summary>Invokes conversion of the selected recurring invoice to dispatch note and retains the complete server result.</summary>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertRecurringInvoiceToDispatchWithResult(string number)
        {
            return ConvertRecurringInvoiceToDispatchWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToDispatchWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertRecurringInvoiceToDispatchWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToDispatchWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToDispatchWithResult"/>
        public Task<JToken> ConvertRecurringInvoiceToDispatchWithResultAsync(string number)
        {
            return ConvertRecurringInvoiceToDispatchWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToDispatchWithResult"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertRecurringInvoiceToDispatchWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertRecurringInvoiceToDispatchWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToDispatchWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertRecurringInvoiceToDispatchWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertRecurringInvoiceToDispatchWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToDispatchWithResultWithHttpInfo"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertRecurringInvoiceToDispatchWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToDispatchWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected recurring invoice to offer and retains the complete server result.</summary>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertRecurringInvoiceToOfferWithResult(string number)
        {
            return ConvertRecurringInvoiceToOfferWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOfferWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertRecurringInvoiceToOfferWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOfferWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOfferWithResult"/>
        public Task<JToken> ConvertRecurringInvoiceToOfferWithResultAsync(string number)
        {
            return ConvertRecurringInvoiceToOfferWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOfferWithResult"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertRecurringInvoiceToOfferWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertRecurringInvoiceToOfferWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOfferWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOfferWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertRecurringInvoiceToOfferWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOfferWithResultWithHttpInfo"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOfferWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOfferWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected recurring invoice to opportunity and retains the complete server result.</summary>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertRecurringInvoiceToOpportunityWithResult(string number)
        {
            return ConvertRecurringInvoiceToOpportunityWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOpportunityWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertRecurringInvoiceToOpportunityWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOpportunityWithResult"/>
        public Task<JToken> ConvertRecurringInvoiceToOpportunityWithResultAsync(string number)
        {
            return ConvertRecurringInvoiceToOpportunityWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOpportunityWithResult"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertRecurringInvoiceToOpportunityWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertRecurringInvoiceToOpportunityWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOpportunityWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOpportunityWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertRecurringInvoiceToOpportunityWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOpportunityWithResultWithHttpInfo"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOpportunityWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected recurring invoice to order and retains the complete server result.</summary>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertRecurringInvoiceToOrderWithResult(string number)
        {
            return ConvertRecurringInvoiceToOrderWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOrderWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertRecurringInvoiceToOrderWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOrderWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOrderWithResult"/>
        public Task<JToken> ConvertRecurringInvoiceToOrderWithResultAsync(string number)
        {
            return ConvertRecurringInvoiceToOrderWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOrderWithResult"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertRecurringInvoiceToOrderWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertRecurringInvoiceToOrderWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOrderWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOrderWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertRecurringInvoiceToOrderWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOrderWithResultWithHttpInfo"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOrderWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOrderWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected recurring invoice to outgoing invoice and retains the complete server result.</summary>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact. Copied success prose names a recurring invoice despite the explicit outgoing-invoice route; no alternative target or response model is inferred.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertRecurringInvoiceToOutgoingInvoiceWithResult(string number)
        {
            return ConvertRecurringInvoiceToOutgoingInvoiceWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOutgoingInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertRecurringInvoiceToOutgoingInvoiceWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOutgoingInvoiceWithResult"/>
        public Task<JToken> ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsync(string number)
        {
            return ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOutgoingInvoiceWithResult"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertRecurringInvoiceToOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal recurring invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertRecurringInvoiceToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal recurring invoice number is required.", nameof(number));
            var localVarPath = "/recurringinvoice/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertRecurringInvoiceToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
