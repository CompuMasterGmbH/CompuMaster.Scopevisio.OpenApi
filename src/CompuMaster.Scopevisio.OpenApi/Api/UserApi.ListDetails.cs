using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class UserApi
    {
        /// <summary>Retrieves the complete JSON user list with explicit optional organisation filtering.</summary>
        /// <param name="onlyActive">The optional deleted-user filter; null omits it and false or true is sent explicitly without a default.</param>
        /// <param name="organisationId">The optional original organisation identifier; null omits it and supplied Int64 values are retained without selecting an account.</param>

        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The current route includes organisationId, absent from the historical bool-only signature, and requires Benutzer (Bearbeiten). When onlyActive is true the server omits deleted users; organisation filtering selects users with rights for that organisation. No success schema or local range is declared. A 404 can indicate an unknown organisation; original failures remain server-authoritative. No default, paging, user identity mapping, organisation lookup, role inference or selected-account change is introduced. Historical typed methods, models and authentication remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>

        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetUsersDetails(bool? onlyActive = null, long? organisationId = null)
        {
            return GetUsersDetailsWithHttpInfo(onlyActive, organisationId).Data;
        }

        /// <inheritdoc cref="GetUsersDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetUsersDetailsWithHttpInfo(bool? onlyActive = null, long? organisationId = null)
        {
            var localVarPath = "/users";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (onlyActive.HasValue) queryParams.Add(new KeyValuePair<string, string>("onlyActive", onlyActive.Value ? "true" : "false"));
            if (organisationId.HasValue) queryParams.Add(new KeyValuePair<string, string>("organisationId", organisationId.Value.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetUsersDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetUsersDetails"/>
        public Task<JToken> GetUsersDetailsAsync(bool? onlyActive = null, long? organisationId = null)
        {
            return GetUsersDetailsAsync(onlyActive, organisationId, CancellationToken.None);
        }

        /// <inheritdoc cref="GetUsersDetails"/>
        /// <param name="onlyActive">The optional deleted-user filter; null omits it and false or true is sent explicitly without a default.</param>
        /// <param name="organisationId">The optional original organisation identifier; null omits it and supplied Int64 values are retained without selecting an account.</param>

        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetUsersDetailsAsync(bool? onlyActive, long? organisationId, CancellationToken cancellationToken)
        {
            return (await GetUsersDetailsAsyncWithHttpInfo(onlyActive, organisationId, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetUsersDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetUsersDetailsAsyncWithHttpInfo(bool? onlyActive = null, long? organisationId = null)
        {
            return GetUsersDetailsAsyncWithHttpInfo(onlyActive, organisationId, CancellationToken.None);
        }

        /// <inheritdoc cref="GetUsersDetailsWithHttpInfo"/>
        /// <param name="onlyActive">The optional deleted-user filter; null omits it and false or true is sent explicitly without a default.</param>
        /// <param name="organisationId">The optional original organisation identifier; null omits it and supplied Int64 values are retained without selecting an account.</param>

        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetUsersDetailsAsyncWithHttpInfo(bool? onlyActive, long? organisationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/users";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (onlyActive.HasValue) queryParams.Add(new KeyValuePair<string, string>("onlyActive", onlyActive.Value ? "true" : "false"));
            if (organisationId.HasValue) queryParams.Add(new KeyValuePair<string, string>("organisationId", organisationId.Value.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetUsersDetails", response, ExceptionFactory);
        }

    }
}
