using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class UserApi
    {
        /// <summary>Marks the explicitly selected user deleted and retains the server acknowledgement.</summary>
        /// <param name="uidOrEmail">The original user ID or email, escaped as one segment without case changes or lookup.</param>
        /// <returns>The complete JSON result if supplied, or null for the documented empty acknowledgement.</returns>
        /// <remarks>The server requires Benutzer (Bearbeiten). The manufacturer marks users deleted rather than physically removing them. Success is documented as HTTP 204 without a response schema. The complete raw status/headers remain available through HTTP-info forms. No body, query, lookup or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken DeleteUserWithResult(string uidOrEmail)
        {
            return DeleteUserWithResultWithHttpInfo(uidOrEmail).Data;
        }

        /// <inheritdoc cref="DeleteUserWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> DeleteUserWithResultWithHttpInfo(string uidOrEmail)
        {
            if (string.IsNullOrWhiteSpace(uidOrEmail) || uidOrEmail == "." || uidOrEmail == "..")
                throw new ArgumentException("An explicit user ID or email is required.", nameof(uidOrEmail));
            var localVarPath = "/users/{uidOrEmail}";
            localVarPath = localVarPath.Replace("{uidOrEmail}", Uri.EscapeDataString(uidOrEmail));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("DeleteUserWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DeleteUserWithResult"/>
        public Task<JToken> DeleteUserWithResultAsync(string uidOrEmail)
        {
            return DeleteUserWithResultAsync(uidOrEmail, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteUserWithResult"/>
        /// <param name="uidOrEmail">The original user ID or email, escaped as one segment without case changes or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> DeleteUserWithResultAsync(string uidOrEmail, CancellationToken cancellationToken)
        {
            return (await DeleteUserWithResultAsyncWithHttpInfo(uidOrEmail, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DeleteUserWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> DeleteUserWithResultAsyncWithHttpInfo(string uidOrEmail)
        {
            return DeleteUserWithResultAsyncWithHttpInfo(uidOrEmail, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteUserWithResultWithHttpInfo"/>
        /// <param name="uidOrEmail">The original user ID or email, escaped as one segment without case changes or lookup.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> DeleteUserWithResultAsyncWithHttpInfo(string uidOrEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(uidOrEmail) || uidOrEmail == "." || uidOrEmail == "..")
                throw new ArgumentException("An explicit user ID or email is required.", nameof(uidOrEmail));
            var localVarPath = "/users/{uidOrEmail}";
            localVarPath = localVarPath.Replace("{uidOrEmail}", Uri.EscapeDataString(uidOrEmail));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("DeleteUserWithResult", response, ExceptionFactory);
        }
    }
}
