using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AdditionalApi
    {
        /// <summary>Invalidates the stored Teamwork token and retains the server acknowledgement.</summary>
        /// <returns>The complete JSON result if supplied, or null for the documented empty acknowledgement.</returns>
        /// <remarks>The server invalidates its stored Teamwork token. Existing configured credentials are used; the SDK does not clear, install or refresh a local token. Success is documented as HTTP 204 without a response schema. The complete raw status/headers remain available through HTTP-info forms. No body, query, lookup or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken DeleteTeamworkTokenWithResult()
        {
            return DeleteTeamworkTokenWithResultWithHttpInfo().Data;
        }

        /// <inheritdoc cref="DeleteTeamworkTokenWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> DeleteTeamworkTokenWithResultWithHttpInfo()
        {
            var localVarPath = "/teamworktoken";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("DeleteTeamworkTokenWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DeleteTeamworkTokenWithResult"/>
        public Task<JToken> DeleteTeamworkTokenWithResultAsync()
        {
            return DeleteTeamworkTokenWithResultAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteTeamworkTokenWithResult"/>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> DeleteTeamworkTokenWithResultAsync(CancellationToken cancellationToken)
        {
            return (await DeleteTeamworkTokenWithResultAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DeleteTeamworkTokenWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> DeleteTeamworkTokenWithResultAsyncWithHttpInfo()
        {
            return DeleteTeamworkTokenWithResultAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteTeamworkTokenWithResultWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> DeleteTeamworkTokenWithResultAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/teamworktoken";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("DeleteTeamworkTokenWithResult", response, ExceptionFactory);
        }
    }
}
