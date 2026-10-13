using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OpportunityApi
    {
        /// <summary>Invokes conversion of the selected opportunity to dispatch note and retains the complete server result.</summary>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen, Potenzial (Opportunities) (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOpportunityToDispatchWithResult(string number)
        {
            return ConvertOpportunityToDispatchWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToDispatchWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOpportunityToDispatchWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOpportunityToDispatchWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOpportunityToDispatchWithResult"/>
        public Task<JToken> ConvertOpportunityToDispatchWithResultAsync(string number)
        {
            return ConvertOpportunityToDispatchWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToDispatchWithResult"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOpportunityToDispatchWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOpportunityToDispatchWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToDispatchWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOpportunityToDispatchWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOpportunityToDispatchWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToDispatchWithResultWithHttpInfo"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOpportunityToDispatchWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToDispatch";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOpportunityToDispatchWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected opportunity to offer and retains the complete server result.</summary>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen, Potenzial (Opportunities) (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOpportunityToOfferWithResult(string number)
        {
            return ConvertOpportunityToOfferWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOfferWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOpportunityToOfferWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOpportunityToOfferWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOpportunityToOfferWithResult"/>
        public Task<JToken> ConvertOpportunityToOfferWithResultAsync(string number)
        {
            return ConvertOpportunityToOfferWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOfferWithResult"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOpportunityToOfferWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOpportunityToOfferWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOfferWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOpportunityToOfferWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOpportunityToOfferWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOfferWithResultWithHttpInfo"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOpportunityToOfferWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOffer";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOpportunityToOfferWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected opportunity to order and retains the complete server result.</summary>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen, Potenzial (Opportunities) (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOpportunityToOrderWithResult(string number)
        {
            return ConvertOpportunityToOrderWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOrderWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOpportunityToOrderWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOpportunityToOrderWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOpportunityToOrderWithResult"/>
        public Task<JToken> ConvertOpportunityToOrderWithResultAsync(string number)
        {
            return ConvertOpportunityToOrderWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOrderWithResult"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOpportunityToOrderWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOpportunityToOrderWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOrderWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOpportunityToOrderWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOpportunityToOrderWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOrderWithResultWithHttpInfo"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOpportunityToOrderWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOrder";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOpportunityToOrderWithResult", response, ExceptionFactory);
        }

        /// <summary>Invokes conversion of the selected opportunity to outgoing invoice and retains the complete server result.</summary>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <returns>The complete JSON server result, or null for an empty response.</returns>
        /// <remarks>The server requires Rechnungen, Potenzial (Opportunities) (Bearbeiten). HTTP 200 declares no fixed response schema; the client infers no resulting identifier, document hierarchy or completeness. Documented 404 can mean missing source or write permission. No body, query, target defaults, document calculations, automatic follow-up or retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken ConvertOpportunityToOutgoingInvoiceWithResult(string number)
        {
            return ConvertOpportunityToOutgoingInvoiceWithResultWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOutgoingInvoiceWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> ConvertOpportunityToOutgoingInvoiceWithResultWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ConvertOpportunityToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ConvertOpportunityToOutgoingInvoiceWithResult"/>
        public Task<JToken> ConvertOpportunityToOutgoingInvoiceWithResultAsync(string number)
        {
            return ConvertOpportunityToOutgoingInvoiceWithResultAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOutgoingInvoiceWithResult"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> ConvertOpportunityToOutgoingInvoiceWithResultAsync(string number, CancellationToken cancellationToken)
        {
            return (await ConvertOpportunityToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ConvertOpportunityToOutgoingInvoiceWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ConvertOpportunityToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number)
        {
            return ConvertOpportunityToOutgoingInvoiceWithResultAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="ConvertOpportunityToOutgoingInvoiceWithResultWithHttpInfo"/>
        /// <param name="number">The original internal opportunity number, escaped as one segment without trimming, numeric conversion or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> ConvertOpportunityToOutgoingInvoiceWithResultAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit internal opportunity number is required.", nameof(number));
            var localVarPath = "/opportunity/{number}/convertToOutgoingInvoice";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ConvertOpportunityToOutgoingInvoiceWithResult", response, ExceptionFactory);
        }
    }
}
