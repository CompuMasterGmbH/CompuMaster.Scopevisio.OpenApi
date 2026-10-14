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
        /// <summary>Updates explicitly supplied fields of a selected debitor personal account.</summary>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The required complete copied sparse update JSON; no field defaults or nested follow-up requests are inserted.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Stammdaten, Debitorenstamm (Bearbeiten), selected-account and catalogue semantics. The body is required; individual update fields may be omitted under general update rules. Explicit null or empty text remains server-authoritative. A supplied nested payment form requires name and is preserved together with paymentTermId; the server may resolve its ID/name or create it. A missing supplied group may also be created by the server. No number-range, currency, group or payment defaults, calculation, lookup, extra request, account selection or mutation replay is added. Cancellation after sending cannot establish account or nested-resource state. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy UpdatePersonalAccountForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required account selector or request is null.</exception>
        /// <exception cref="ArgumentException">A nested payment field is absent, a known kind or Int64 width is invalid, a nested percentage is not finite, or the account-number selector is a dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateDebitorWithResult(string accountNumber, PersonalAccountUpdateRequest request)
        {
            return UpdateDebitorWithResultWithHttpInfo(accountNumber, request).Data;
        }

        /// <inheritdoc cref="UpdateDebitorWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateDebitorWithResultWithHttpInfo(string accountNumber, PersonalAccountUpdateRequest request)
        {
            if (accountNumber == null) throw new ArgumentNullException(nameof(accountNumber));
            if (accountNumber == "." || accountNumber == "..") throw new ArgumentException("The account-number selector cannot be a path dot segment.", nameof(accountNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/debitoraccounts/{accountNumber}";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateDebitorWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateDebitorWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateDebitorWithResultAsync(string accountNumber, PersonalAccountUpdateRequest request)
        {
            return UpdateDebitorWithResultAsync(accountNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateDebitorWithResult"/>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The required complete copied sparse update JSON; no field defaults or nested follow-up requests are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have updated the account or nested resources.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateDebitorWithResultAsync(string accountNumber, PersonalAccountUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateDebitorWithResultAsyncWithHttpInfo(accountNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateDebitorWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateDebitorWithResultAsyncWithHttpInfo(string accountNumber, PersonalAccountUpdateRequest request)
        {
            return UpdateDebitorWithResultAsyncWithHttpInfo(accountNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateDebitorWithResultWithHttpInfo"/>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The required complete copied sparse update JSON; no field defaults or nested follow-up requests are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have updated the account or nested resources.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateDebitorWithResultAsyncWithHttpInfo(string accountNumber, PersonalAccountUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (accountNumber == null) throw new ArgumentNullException(nameof(accountNumber));
            if (accountNumber == "." || accountNumber == "..") throw new ArgumentException("The account-number selector cannot be a path dot segment.", nameof(accountNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/debitoraccounts/{accountNumber}";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateDebitorWithResult", response, ExceptionFactory);
        }
    }
}
