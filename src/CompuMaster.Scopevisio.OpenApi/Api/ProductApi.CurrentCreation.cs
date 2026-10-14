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
        /// <summary>Creates a product using explicitly supplied current fields.</summary>
        /// <param name="request">The required complete product JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The HTTP 201 ProductNewResponse declares optional status, productId, location and errors; unknown fields remain available.</returns>
        /// <remarks>The server enforces Produktverwaltung (Bearbeiten) and original catalogue/account/tax semantics. Creation requires an explicit unit. Partial product updates retain omitted fields and send explicit JSON null or empty strings without assuming every field can be cleared. No lookup, implicit account selection, inserted price/boolean defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; exact raw JSON remains available. Legacy models and methods remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required creation request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a numeric value is not finite, unit is missing, or read-only type is supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateProductWithResult(ProductWriteRequest request)
        {
            return CreateProductWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateProductWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateProductWithResultWithHttpInfo(ProductWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/product/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateProductWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateProductWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateProductWithResultAsync(ProductWriteRequest request)
        {
            return CreateProductWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductWithResult"/>
        /// <param name="request">The required complete product JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the product.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateProductWithResultAsync(ProductWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProductWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProductWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProductWithResultAsyncWithHttpInfo(ProductWriteRequest request)
        {
            return CreateProductWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductWithResultWithHttpInfo"/>
        /// <param name="request">The required complete product JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the product.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProductWithResultAsyncWithHttpInfo(ProductWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/product/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateProductWithResult", response, ExceptionFactory);
        }
    }
}
