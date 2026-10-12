using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class HumanResourcesApi
    {
        /// <summary>Creates a human resource using explicitly supplied contact and planning values.</summary>
        /// <param name="request">The required JSON request, copied and serialized before HTTP I/O. Planning strings remain unchanged.</param>
        /// <returns>The complete creation result, or null for an empty or JSON-null response.</returns>
        /// <remarks>The server enforces the Human-Ressourcen Verwaltung (Bearbeiten) profile, contact identity, catalog membership and planning date semantics. The contract contains conflicting yyyy-MM-dd and dd.MM.yyyy descriptions; the client does not choose a format or adjust month boundaries. An omitted allowMultiple retains the server default false. Inspect response errors even on HTTP 201. The SDK adds no mutation retry or automatic follow-up. Cancellation after sending does not establish the server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; exact raw JSON remains available.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required value is absent or a known raw field has an incompatible JSON kind.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON, has an incompatible object shape or exceeds the numeric parser range.</exception>
        public HumanResourceCreationResponse CreateHumanResource(HumanResourceCreationRequest request)
        {
            return CreateHumanResourceWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateHumanResource"/>
        /// <returns>The HTTP status, response/content headers, exact raw response and complete creation result.</returns>
        public ApiResponse<HumanResourceCreationResponse> CreateHumanResourceWithHttpInfo(HumanResourceCreationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/humanresource/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("CreateHumanResource", response, ExceptionFactory), value => new HumanResourceCreationResponse(value));
        }

        /// <inheritdoc cref="CreateHumanResource"/>
        public Task<HumanResourceCreationResponse> CreateHumanResourceAsync(HumanResourceCreationRequest request)
        {
            return CreateHumanResourceAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateHumanResource"/>
        /// <param name="request">The required JSON request, copied and serialized before HTTP I/O. Planning strings remain unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the resource.</exception>
        public async Task<HumanResourceCreationResponse> CreateHumanResourceAsync(HumanResourceCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateHumanResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateHumanResourceWithHttpInfo"/>
        public Task<ApiResponse<HumanResourceCreationResponse>> CreateHumanResourceAsyncWithHttpInfo(HumanResourceCreationRequest request)
        {
            return CreateHumanResourceAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateHumanResourceWithHttpInfo"/>
        /// <param name="request">The required JSON request, copied and serialized before HTTP I/O. Planning strings remain unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the resource.</exception>
        public async Task<ApiResponse<HumanResourceCreationResponse>> CreateHumanResourceAsyncWithHttpInfo(HumanResourceCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/humanresource/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("CreateHumanResource", response, ExceptionFactory), value => new HumanResourceCreationResponse(value));
        }
    }
}
