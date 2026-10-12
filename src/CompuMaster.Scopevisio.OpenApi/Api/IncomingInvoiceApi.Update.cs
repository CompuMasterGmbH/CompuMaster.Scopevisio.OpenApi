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
    public partial class IncomingInvoiceApi
    {
        /// <summary>Updates explicitly selected fields of an unverified incoming invoice.</summary>
        /// <param name="id">The required original Int64 invoice master identifier, sent using invariant text.</param>
        /// <param name="request">The optional complete JSON update, copied before HTTP I/O. Null omits the body; optional field omission is not translated to a reset or default.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server permits updating only an unverified invoice and enforces Rechnungseingangsbuch (Anzeigen) as documented, actual permissions, payment/discount dependencies and contact-derived values. The client does not fetch existing state, infer omitted-field effects, alter permissions automatically, normalize timestamps or repair workflow/payment data. No new mutation retry is added; cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied known field has an incompatible JSON kind or an integer exceeds its declared width/range.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateIncomingInvoice(long id, IncomingInvoiceUpdateRequest request = null)
        {
            return UpdateIncomingInvoiceWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateIncomingInvoice"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateIncomingInvoiceWithHttpInfo(long id, IncomingInvoiceUpdateRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/incominginvoice/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateIncomingInvoice", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateIncomingInvoice"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateIncomingInvoiceAsync(long id, IncomingInvoiceUpdateRequest request = null)
        {
            return UpdateIncomingInvoiceAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateIncomingInvoice"/>
        /// <param name="id">The required original Int64 invoice master identifier, sent using invariant text.</param>
        /// <param name="request">The optional complete JSON update, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateIncomingInvoiceAsync(long id, IncomingInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateIncomingInvoiceAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateIncomingInvoiceWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateIncomingInvoiceAsyncWithHttpInfo(long id, IncomingInvoiceUpdateRequest request = null)
        {
            return UpdateIncomingInvoiceAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateIncomingInvoiceWithHttpInfo"/>
        /// <param name="id">The required original Int64 invoice master identifier, sent using invariant text.</param>
        /// <param name="request">The optional complete JSON update, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateIncomingInvoiceAsyncWithHttpInfo(long id, IncomingInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/incominginvoice/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateIncomingInvoice", response, ExceptionFactory);
        }
    }
}
