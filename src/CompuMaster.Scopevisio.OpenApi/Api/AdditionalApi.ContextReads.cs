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
        /// <summary>Retrieves the complete JSON hello test response.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The contract declares no profiles and no fixed success schema; OAuth authentication still applies. The request has no query or body. No organisation/account/instance selection, credential acquisition, catalog defaults or inferred permissions are introduced. Original typed methods and their defaults remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetHelloDetails()
        {
            return GetHelloDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetHelloDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetHelloDetailsWithHttpInfo()
        {
            var localVarPath = "/hello";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetHelloDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetHelloDetails"/>
        public Task<JToken> GetHelloDetailsAsync()
        {
            return GetHelloDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetHelloDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetHelloDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetHelloDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetHelloDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetHelloDetailsAsyncWithHttpInfo()
        {
            return GetHelloDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetHelloDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetHelloDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/hello";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetHelloDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON deployment information.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The contract declares no profiles and no fixed success schema; OAuth authentication still applies. The request has no query or body. No organisation/account/instance selection, credential acquisition, catalog defaults or inferred permissions are introduced. Original typed methods and their defaults remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetVersionDetails()
        {
            return GetVersionDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetVersionDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetVersionDetailsWithHttpInfo()
        {
            var localVarPath = "/version";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetVersionDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetVersionDetails"/>
        public Task<JToken> GetVersionDetailsAsync()
        {
            return GetVersionDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetVersionDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetVersionDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetVersionDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetVersionDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetVersionDetailsAsyncWithHttpInfo()
        {
            return GetVersionDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetVersionDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetVersionDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/version";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetVersionDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON accessible organisations.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The contract declares no profiles and no fixed success schema; OAuth authentication still applies. The request has no query or body. No organisation/account/instance selection, credential acquisition, catalog defaults or inferred permissions are introduced. Original typed methods and their defaults remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetOrganisationsDetails()
        {
            return GetOrganisationsDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetOrganisationsDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetOrganisationsDetailsWithHttpInfo()
        {
            var localVarPath = "/organisations";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOrganisationsDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOrganisationsDetails"/>
        public Task<JToken> GetOrganisationsDetailsAsync()
        {
            return GetOrganisationsDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrganisationsDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetOrganisationsDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetOrganisationsDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOrganisationsDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOrganisationsDetailsAsyncWithHttpInfo()
        {
            return GetOrganisationsDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrganisationsDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOrganisationsDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/organisations";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOrganisationsDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON Teamwork-token response with an explicit optional force choice.</summary>
        /// <param name="force">The optional request to retrieve a new token instead of an existing one; null omits it and false or true is retained explicitly.</param>


        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The contract requires OAuth but no profiles and declares no fixed success schema. Force can cause the server to retrieve a new token; omission retains server behavior and does not force refresh. Returned credentials are data and are never installed as the current API token or used to select an account. Preserve credential confidentiality when consuming data or raw responses. Existing typed token results, authentication and defaults remain unchanged. HTTP 400 indicates unavailable Teamwork/refresh access; HTTP 500 can indicate server refresh/access-token retrieval failure. Original diagnostics and unknown fields survive. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>

        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetTeamworkTokenDetails(bool? force = null)
        {
            return GetTeamworkTokenDetailsWithHttpInfo(force).Data;
        }

        /// <inheritdoc cref="GetTeamworkTokenDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetTeamworkTokenDetailsWithHttpInfo(bool? force = null)
        {
            var localVarPath = "/teamworktoken";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (force.HasValue) queryParams.Add(new KeyValuePair<string, string>("force", force.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTeamworkTokenDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTeamworkTokenDetails"/>
        public Task<JToken> GetTeamworkTokenDetailsAsync(bool? force = null)
        {
            return GetTeamworkTokenDetailsAsync(force, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTeamworkTokenDetails"/>
        /// <param name="force">The optional request to retrieve a new token instead of an existing one; null omits it and false or true is retained explicitly.</param>


        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetTeamworkTokenDetailsAsync(bool? force, CancellationToken cancellationToken)
        {
            return (await GetTeamworkTokenDetailsAsyncWithHttpInfo(force, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTeamworkTokenDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTeamworkTokenDetailsAsyncWithHttpInfo(bool? force = null)
        {
            return GetTeamworkTokenDetailsAsyncWithHttpInfo(force, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTeamworkTokenDetailsWithHttpInfo"/>
        /// <param name="force">The optional request to retrieve a new token instead of an existing one; null omits it and false or true is retained explicitly.</param>


        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTeamworkTokenDetailsAsyncWithHttpInfo(bool? force, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/teamworktoken";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (force.HasValue) queryParams.Add(new KeyValuePair<string, string>("force", force.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTeamworkTokenDetails", response, ExceptionFactory);
        }

    }
}
