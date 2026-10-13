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
        /// <summary>Invokes conversion of the selected outgoing invoice to dispatch note and retains the complete server result.</summary>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertInvoiceToDispatchWithResult(string number)
        {
            return ConvertInvoiceToDispatchWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToDispatchWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertInvoiceToDispatchWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertInvoiceToDispatchWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertInvoiceToDispatchWithResult"/>
        public Task<JToken> ConvertInvoiceToDispatchWithResultAsync(string number)
        {
            return ConvertInvoiceToDispatchWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToDispatchWithResult"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertInvoiceToDispatchWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertInvoiceToDispatchWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToDispatchWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertInvoiceToDispatchWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertInvoiceToDispatchWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToDispatchWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertInvoiceToDispatchWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertInvoiceToDispatchWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected outgoing invoice to offer and retains the complete server result.</summary>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertInvoiceToOfferWithResult(string number)
        {
            return ConvertInvoiceToOfferWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOfferWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertInvoiceToOfferWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertInvoiceToOfferWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertInvoiceToOfferWithResult"/>
        public Task<JToken> ConvertInvoiceToOfferWithResultAsync(string number)
        {
            return ConvertInvoiceToOfferWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOfferWithResult"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertInvoiceToOfferWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertInvoiceToOfferWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOfferWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertInvoiceToOfferWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertInvoiceToOfferWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOfferWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertInvoiceToOfferWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertInvoiceToOfferWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected outgoing invoice to opportunity and retains the complete server result.</summary>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertInvoiceToOpportunityWithResult(string number)
        {
            return ConvertInvoiceToOpportunityWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOpportunityWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertInvoiceToOpportunityWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertInvoiceToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertInvoiceToOpportunityWithResult"/>
        public Task<JToken> ConvertInvoiceToOpportunityWithResultAsync(string number)
        {
            return ConvertInvoiceToOpportunityWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOpportunityWithResult"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertInvoiceToOpportunityWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertInvoiceToOpportunityWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOpportunityWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertInvoiceToOpportunityWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertInvoiceToOpportunityWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOpportunityWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertInvoiceToOpportunityWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertInvoiceToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected outgoing invoice to order and retains the complete server result.</summary>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertInvoiceToOrderWithResult(string number)
        {
            return ConvertInvoiceToOrderWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOrderWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertInvoiceToOrderWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertInvoiceToOrderWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertInvoiceToOrderWithResult"/>
        public Task<JToken> ConvertInvoiceToOrderWithResultAsync(string number)
        {
            return ConvertInvoiceToOrderWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOrderWithResult"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertInvoiceToOrderWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertInvoiceToOrderWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertInvoiceToOrderWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertInvoiceToOrderWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertInvoiceToOrderWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertInvoiceToOrderWithResultWithHttpInfo"/>
        /// <param name="number">The original internal outgoing invoice number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertInvoiceToOrderWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal outgoing invoice number is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertInvoiceToOrderWithResult", response, ExceptionFactory);
        }
    }
}
