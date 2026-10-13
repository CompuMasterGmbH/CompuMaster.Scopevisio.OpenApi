using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OfferApi
    {
        /// <summary>Invokes conversion of the selected offer to dispatch note and retains the complete server result.</summary>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Angebote (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOfferToDispatchWithResult(string number)
        {
            return ConvertOfferToDispatchWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOfferToDispatchWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOfferToDispatchWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOfferToDispatchWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOfferToDispatchWithResult"/>
        public Task<JToken> ConvertOfferToDispatchWithResultAsync(string number)
        {
            return ConvertOfferToDispatchWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToDispatchWithResult"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOfferToDispatchWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOfferToDispatchWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOfferToDispatchWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOfferToDispatchWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOfferToDispatchWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToDispatchWithResultWithHttpInfo"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOfferToDispatchWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOfferToDispatchWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected offer to opportunity and retains the complete server result.</summary>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Angebote (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOfferToOpportunityWithResult(string number)
        {
            return ConvertOfferToOpportunityWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOpportunityWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOfferToOpportunityWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOfferToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOfferToOpportunityWithResult"/>
        public Task<JToken> ConvertOfferToOpportunityWithResultAsync(string number)
        {
            return ConvertOfferToOpportunityWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOpportunityWithResult"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOfferToOpportunityWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOfferToOpportunityWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOpportunityWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOfferToOpportunityWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOfferToOpportunityWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOpportunityWithResultWithHttpInfo"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOfferToOpportunityWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOpportunity";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOfferToOpportunityWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected offer to order and retains the complete server result.</summary>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Angebote (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOfferToOrderWithResult(string number)
        {
            return ConvertOfferToOrderWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOrderWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOfferToOrderWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOfferToOrderWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOfferToOrderWithResult"/>
        public Task<JToken> ConvertOfferToOrderWithResultAsync(string number)
        {
            return ConvertOfferToOrderWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOrderWithResult"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOfferToOrderWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOfferToOrderWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOrderWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOfferToOrderWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOfferToOrderWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOrderWithResultWithHttpInfo"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOfferToOrderWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOfferToOrderWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected offer to outgoing invoice and retains the complete server result.</summary>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Angebote (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOfferToOutgoingInvoiceWithResult(string number)
        {
            return ConvertOfferToOutgoingInvoiceWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOutgoingInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOfferToOutgoingInvoiceWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOfferToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOfferToOutgoingInvoiceWithResult"/>
        public Task<JToken> ConvertOfferToOutgoingInvoiceWithResultAsync(string number)
        {
            return ConvertOfferToOutgoingInvoiceWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOutgoingInvoiceWithResult"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOfferToOutgoingInvoiceWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOfferToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOfferToOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOfferToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOfferToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOfferToOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal offer number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOfferToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal offer number is required.", nameof(number));
            var localVarPath = "/offer/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOfferToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
