using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AuthorizationApi
    {
        /// <summary>Changes the token organisation using an explicitly supplied ID.</summary>
        /// <param name="organisationId">The original signed Int64 Scope organisation ID, formatted invariantly without minimum rules, name lookup, UID or Teamwork tenant translation.</param>
        /// <returns>The complete JSON result, or null for empty content. The manufacturer declares HTTP 200 without a fixed successful schema.</returns>
        /// <remarks>This low-level operation may change server token context. Required selectors are sent unchanged with no body, lookup, translation, default target, configuration mutation, token adoption/installation, fresh-account calls, credential/profile/cache access, extra requests or replay. HTTP 200 has no fixed success schema and does not establish requested organisation, user identity, fresh account or Teamwork tenant. Complete raw/future JSON remains available without a verified-scope flag. Before Demo business operations, prefer the existing staged high-level authorization flow with explicit Configuration.OrganisationId and fresh myaccount proof; a name or raw response alone is insufficient. Numeric Scope organisation, organisation-name strings, user identifiers and opaque Teamwork tenant remain distinct. Cancellation after sending cannot establish server token scope even though local configuration stays unchanged. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual identity/token/restoration behavior remains unverified; historical and guarded higher-level APIs remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response, including server 400 for an invalid selector.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ChangeOrganisationByIdWithResult(long organisationId)
        {
            return ChangeOrganisationByIdWithResultWithHttpInfo(organisationId).Data;
        }

        /// <inheritdoc cref="ChangeOrganisationByIdWithResult"/>
        /// <returns>The complete JSON, exact raw content, status and response/content headers.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ChangeOrganisationByIdWithResultWithHttpInfo(long organisationId)
        {
            var localVarPath = "/token/organisation/{organisationId}";
            localVarPath = localVarPath.Replace("{organisationId}", organisationId.ToString(CultureInfo.InvariantCulture));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ChangeOrganisationByIdWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ChangeOrganisationByIdWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ChangeOrganisationByIdWithResultAsync(long organisationId)
        {
            return ChangeOrganisationByIdWithResultAsync(organisationId, CancellationToken.None);
        }

        /// <inheritdoc cref="ChangeOrganisationByIdWithResult"/>
        /// <param name="organisationId">The original signed Int64 Scope organisation ID, formatted invariantly without minimum rules, name lookup, UID or Teamwork tenant translation.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation occurs before or during transport; server token context may already have changed.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ChangeOrganisationByIdWithResultAsync(long organisationId, CancellationToken cancellationToken)
        {
            return (await ChangeOrganisationByIdWithResultAsyncWithHttpInfo(organisationId, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ChangeOrganisationByIdWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ChangeOrganisationByIdWithResultAsyncWithHttpInfo(long organisationId)
        {
            return ChangeOrganisationByIdWithResultAsyncWithHttpInfo(organisationId, CancellationToken.None);
        }

        /// <inheritdoc cref="ChangeOrganisationByIdWithResultWithHttpInfo"/>
        /// <param name="organisationId">The original signed Int64 Scope organisation ID, formatted invariantly without minimum rules, name lookup, UID or Teamwork tenant translation.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation occurs before or during transport; server token context may already have changed.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ChangeOrganisationByIdWithResultAsyncWithHttpInfo(long organisationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/token/organisation/{organisationId}";
            localVarPath = localVarPath.Replace("{organisationId}", organisationId.ToString(CultureInfo.InvariantCulture));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ChangeOrganisationByIdWithResult", response, ExceptionFactory);
        }
    }
}
