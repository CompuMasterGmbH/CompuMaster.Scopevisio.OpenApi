using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AuthorizationApi
    {
        /// <summary>Revokes the existing configured token and retains the server acknowledgement.</summary>
        /// <returns>The complete JSON result if supplied, or null for the documented empty acknowledgement.</returns>
        /// <remarks>The server revokes the configured bearer token. The SDK does not change local authorization/token/account state or automatically select another token. Success is documented as HTTP 204 without a response schema. The complete raw status/headers remain available through HTTP-info forms. No body, query, lookup or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken RevokeTokenWithResult()
        {
            return RevokeTokenWithResultWithHttpInfo().Data;
        }

        /// <inheritdoc cref="RevokeTokenWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> RevokeTokenWithResultWithHttpInfo()
        {
            var localVarPath = "/token";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("RevokeTokenWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RevokeTokenWithResult"/>
        public Task<JToken> RevokeTokenWithResultAsync()
        {
            return RevokeTokenWithResultAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="RevokeTokenWithResult"/>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> RevokeTokenWithResultAsync(CancellationToken cancellationToken)
        {
            return (await RevokeTokenWithResultAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="RevokeTokenWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> RevokeTokenWithResultAsyncWithHttpInfo()
        {
            return RevokeTokenWithResultAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="RevokeTokenWithResultWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> RevokeTokenWithResultAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/token";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("RevokeTokenWithResult", response, ExceptionFactory);
        }
    }
}
