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
        /// <summary>Creates a product group using explicitly supplied current fields.</summary>
        /// <param name="request">The required complete product-group JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Produktverwaltung (Bearbeiten) and original catalogue/account/tax semantics. Creation requires an explicit group name. Partial product updates retain omitted fields and send explicit JSON null or empty strings without assuming every field can be cleared. No lookup, implicit account selection, inserted price/boolean defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; exact raw JSON remains available. Legacy models and methods remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required creation request is null.</exception>
        /// <exception cref="ArgumentException">The explicit name is missing or has an incompatible JSON kind.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateProductGroupWithResult(ProductGroupWriteRequest request)
        {
            return CreateProductGroupWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateProductGroupWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateProductGroupWithResultWithHttpInfo(ProductGroupWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/productGroup/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateProductGroupWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateProductGroupWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateProductGroupWithResultAsync(ProductGroupWriteRequest request)
        {
            return CreateProductGroupWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductGroupWithResult"/>
        /// <param name="request">The required complete product-group JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the product group.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateProductGroupWithResultAsync(ProductGroupWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProductGroupWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProductGroupWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProductGroupWithResultAsyncWithHttpInfo(ProductGroupWriteRequest request)
        {
            return CreateProductGroupWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductGroupWithResultWithHttpInfo"/>
        /// <param name="request">The required complete product-group JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the product group.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateProductGroupWithResultAsyncWithHttpInfo(ProductGroupWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/productGroup/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateProductGroupWithResult", response, ExceptionFactory);
        }
    }
}
