using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OrderApi
    {
        /// <summary>Invokes conversion of the selected order to dispatch note and retains the complete server result.</summary>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Aufträge (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOrderToDispatchWithResult(string number)
        {
            return ConvertOrderToDispatchWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOrderToDispatchWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOrderToDispatchWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOrderToDispatchWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOrderToDispatchWithResult"/>
        public Task<JToken> ConvertOrderToDispatchWithResultAsync(string number)
        {
            return ConvertOrderToDispatchWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToDispatchWithResult"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOrderToDispatchWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOrderToDispatchWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOrderToDispatchWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOrderToDispatchWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOrderToDispatchWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToDispatchWithResultWithHttpInfo"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOrderToDispatchWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOrderToDispatchWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected order to offer and retains the complete server result.</summary>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Aufträge (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOrderToOfferWithResult(string number)
        {
            return ConvertOrderToOfferWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOfferWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOrderToOfferWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOrderToOfferWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOrderToOfferWithResult"/>
        public Task<JToken> ConvertOrderToOfferWithResultAsync(string number)
        {
            return ConvertOrderToOfferWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOfferWithResult"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOrderToOfferWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOrderToOfferWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOfferWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOrderToOfferWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOrderToOfferWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOfferWithResultWithHttpInfo"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOrderToOfferWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOrderToOfferWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected order to opportunity and retains the complete server result.</summary>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Aufträge (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOrderToOpportunityWithResult(string number)
        {
            return ConvertOrderToOpportunityWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOpportunityWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOrderToOpportunityWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOrderToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOrderToOpportunityWithResult"/>
        public Task<JToken> ConvertOrderToOpportunityWithResultAsync(string number)
        {
            return ConvertOrderToOpportunityWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOpportunityWithResult"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOrderToOpportunityWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOrderToOpportunityWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOpportunityWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOrderToOpportunityWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOrderToOpportunityWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOpportunityWithResultWithHttpInfo"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOrderToOpportunityWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOrderToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected order to outgoing invoice and retains the complete server result.</summary>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Aufträge (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOrderToOutgoingInvoiceWithResult(string number)
        {
            return ConvertOrderToOutgoingInvoiceWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOutgoingInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOrderToOutgoingInvoiceWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOrderToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOrderToOutgoingInvoiceWithResult"/>
        public Task<JToken> ConvertOrderToOutgoingInvoiceWithResultAsync(string number)
        {
            return ConvertOrderToOutgoingInvoiceWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOutgoingInvoiceWithResult"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOrderToOutgoingInvoiceWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOrderToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOrderToOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOrderToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOrderToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOrderToOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal order number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOrderToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal order number is required.", nameof(number));
            var localVarPath = "/order/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOrderToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
