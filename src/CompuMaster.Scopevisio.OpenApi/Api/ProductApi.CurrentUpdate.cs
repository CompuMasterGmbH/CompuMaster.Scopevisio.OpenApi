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
    public partial class ProductApi
    {
        /// <summary>Updates explicitly supplied fields of a selected product.</summary>
        /// <param name="id">The explicit original signed Int64 product identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current product JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Produktverwaltung (Bearbeiten) and original catalogue/account/tax semantics. Creation requires an explicit unit. Partial product updates retain omitted fields and send explicit JSON null or empty strings without assuming every field can be cleared. No lookup, implicit account selection, inserted price/boolean defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; exact raw JSON remains available. Legacy models and methods remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a numeric value is not finite, or read-only type is supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateProductWithResult(long id, ProductWriteRequest request = null)
        {
            return UpdateProductWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateProductWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateProductWithResultWithHttpInfo(long id, ProductWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/product/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateProductWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateProductWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateProductWithResultAsync(long id, ProductWriteRequest request = null)
        {
            return UpdateProductWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProductWithResult"/>
        /// <param name="id">The explicit original signed Int64 product identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete product JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateProductWithResultAsync(long id, ProductWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateProductWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateProductWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProductWithResultAsyncWithHttpInfo(long id, ProductWriteRequest request = null)
        {
            return UpdateProductWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProductWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 product identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete product JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProductWithResultAsyncWithHttpInfo(long id, ProductWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/product/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateProductWithResult", response, ExceptionFactory);
        }
    }
}
