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
        /// <summary>Updates explicitly supplied fields of a selected payment term.</summary>
        /// <param name="id">The explicit original signed Int64 payment-term identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current payment-term JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Stammdaten, Zahlungsbedingungen (Bearbeiten), unique names, schedule and percentage relationships. The route body is optional; a supplied creation object requires name. The schema does not require netTimeLimit although server 400 descriptions mention it as missing. General sparse-update rules leave omitted fields unchanged, but endpoint-specific acceptance and explicit null or empty text remain server-authoritative. No days, percentages, discounts, schedules or name defaults are inserted or calculated. No account selection, lookup or mutation replay is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Existing PaymentTermForm APIs and historical Object creation results remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required supplied field is absent, a known field has an incompatible JSON kind, an integer exceeds its width, or a percentage is not finite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdatePaymentTermWithResult(long id, PaymentTermWriteRequest request = null)
        {
            return UpdatePaymentTermWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdatePaymentTermWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdatePaymentTermWithResultWithHttpInfo(long id, PaymentTermWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/paymentterm/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdatePaymentTermWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdatePaymentTermWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdatePaymentTermWithResultAsync(long id, PaymentTermWriteRequest request = null)
        {
            return UpdatePaymentTermWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdatePaymentTermWithResult"/>
        /// <param name="id">The explicit original signed Int64 payment-term identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete payment-term JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdatePaymentTermWithResultAsync(long id, PaymentTermWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdatePaymentTermWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdatePaymentTermWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdatePaymentTermWithResultAsyncWithHttpInfo(long id, PaymentTermWriteRequest request = null)
        {
            return UpdatePaymentTermWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdatePaymentTermWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 payment-term identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete payment-term JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdatePaymentTermWithResultAsyncWithHttpInfo(long id, PaymentTermWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/paymentterm/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdatePaymentTermWithResult", response, ExceptionFactory);
        }
    }
}
