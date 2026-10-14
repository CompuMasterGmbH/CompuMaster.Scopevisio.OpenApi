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
        /// <summary>Creates a payment term using explicitly supplied current fields.</summary>
        /// <param name="request">The optional complete payment-term JSON, copied before HTTP I/O without inserting defaults. Null sends no body.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Stammdaten, Zahlungsbedingungen (Bearbeiten), unique names, schedule and percentage relationships. The route body is optional; a supplied creation object requires name. The schema does not require netTimeLimit although server 400 descriptions mention it as missing. General sparse-update rules leave omitted fields unchanged, but endpoint-specific acceptance and explicit null or empty text remain server-authoritative. No days, percentages, discounts, schedules or name defaults are inserted or calculated. No account selection, lookup or mutation replay is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Existing PaymentTermForm APIs and historical Object creation results remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required supplied field is absent, a known field has an incompatible JSON kind, an integer exceeds its width, or a percentage is not finite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreatePaymentTermWithResult(PaymentTermWriteRequest request = null)
        {
            return CreatePaymentTermWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreatePaymentTermWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreatePaymentTermWithResultWithHttpInfo(PaymentTermWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/paymentterm/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreatePaymentTermWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreatePaymentTermWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreatePaymentTermWithResultAsync(PaymentTermWriteRequest request = null)
        {
            return CreatePaymentTermWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreatePaymentTermWithResult"/>
        /// <param name="request">The optional complete payment-term JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the payment term.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreatePaymentTermWithResultAsync(PaymentTermWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreatePaymentTermWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreatePaymentTermWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreatePaymentTermWithResultAsyncWithHttpInfo(PaymentTermWriteRequest request = null)
        {
            return CreatePaymentTermWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreatePaymentTermWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete payment-term JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the payment term.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreatePaymentTermWithResultAsyncWithHttpInfo(PaymentTermWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/paymentterm/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreatePaymentTermWithResult", response, ExceptionFactory);
        }
    }
}
