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
        /// <summary>Retrieves the complete JSON details of an explicitly selected user.</summary>
        /// <param name="uidOrEmail">The explicitly selected original user identifier or email, escaped without trimming, case normalization or identity conversion.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Benutzer (Anzeigen) profile. The manufacturer declares no fixed success schema or exhaustive error-status list. No current-user, account or identity mapping is inferred. Historical methods and token/account defaults remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The selection is blank or a relative path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetUserDetails(string uidOrEmail)
        {
            return GetUserDetailsWithHttpInfo(uidOrEmail).Data;
        }

        /// <inheritdoc cref="GetUserDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetUserDetailsWithHttpInfo(string uidOrEmail)
        {
            if (string.IsNullOrWhiteSpace(uidOrEmail) || uidOrEmail == "." || uidOrEmail == "..")
                throw new ArgumentException("An explicit user identifier or email is required.", nameof(uidOrEmail));
            var localVarPath = "/users/{uidOrEmail}";
            localVarPath = localVarPath.Replace("{uidOrEmail}", Uri.EscapeDataString(uidOrEmail));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetUserDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetUserDetails"/>
        public Task<JToken> GetUserDetailsAsync(string uidOrEmail)
        {
            return GetUserDetailsAsync(uidOrEmail, CancellationToken.None);
        }

        /// <inheritdoc cref="GetUserDetails"/>
        /// <param name="uidOrEmail">The explicitly selected original user identifier or email, escaped without trimming, case normalization or identity conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetUserDetailsAsync(string uidOrEmail, CancellationToken cancellationToken)
        {
            return (await GetUserDetailsAsyncWithHttpInfo(uidOrEmail, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetUserDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetUserDetailsAsyncWithHttpInfo(string uidOrEmail)
        {
            return GetUserDetailsAsyncWithHttpInfo(uidOrEmail, CancellationToken.None);
        }

        /// <inheritdoc cref="GetUserDetailsWithHttpInfo"/>
        /// <param name="uidOrEmail">The explicitly selected original user identifier or email, escaped without trimming, case normalization or identity conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetUserDetailsAsyncWithHttpInfo(string uidOrEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(uidOrEmail) || uidOrEmail == "." || uidOrEmail == "..")
                throw new ArgumentException("An explicit user identifier or email is required.", nameof(uidOrEmail));
            var localVarPath = "/users/{uidOrEmail}";
            localVarPath = localVarPath.Replace("{uidOrEmail}", Uri.EscapeDataString(uidOrEmail));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetUserDetails", response, ExceptionFactory);
        }

    }
}
