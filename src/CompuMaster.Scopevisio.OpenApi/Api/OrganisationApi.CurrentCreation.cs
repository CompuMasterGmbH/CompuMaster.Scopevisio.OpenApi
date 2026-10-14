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
    public partial class OrganisationApi
    {
        /// <summary>Creates an explicitly named organisation.</summary>
        /// <param name="request">The optional complete copied organisation JSON; null sends no body and a supplied object requires its original name without chart or organisation defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Organisation (Bearbeiten) and actual name/chart semantics. Body is optional; a supplied object requires original organisationName and optional scoa strings. Omitted scoa leaves chart setup to the user. The manufacturer requests a timeout of at least five minutes for chart setup; configure Configuration.Timeout and any caller-owned HttpClient timeout before first transport use. The client neither changes shared/cached timeout options nor sleeps, inserts a chart/name, looks up an existing organisation, performs upsert/reuse, switches organisation, installs tokens, makes extra requests or replays a mutation. HTTP 201 has no fixed successful schema or guaranteed organisation identifier. Server 400 exists/missing-chart diagnostics remain original, not a reuse signal. Complete future JSON is copied before HTTP; cancellation after sending cannot establish organisation/chart state. Parsed fractions use bounded decimal precision; raw JSON remains exact. A new global organisation is not contained merely by selecting Demo and needs separately owned reversible fixtures. Actual organisation/chart/profile acceptance and complete cleanup/reset remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or a supplied object lacks an original organisationName.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateOrganisationWithResult(OrganisationCreationRequest request = null)
        {
            return CreateOrganisationWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateOrganisationWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateOrganisationWithResultWithHttpInfo(OrganisationCreationRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/organisation/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateOrganisationWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateOrganisationWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateOrganisationWithResultAsync(OrganisationCreationRequest request = null)
        {
            return CreateOrganisationWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateOrganisationWithResult"/>
        /// <param name="request">The optional complete copied organisation JSON; null sends no body and a supplied object requires its original name without chart or organisation defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created an organisation or started chart setup.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateOrganisationWithResultAsync(OrganisationCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateOrganisationWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateOrganisationWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateOrganisationWithResultAsyncWithHttpInfo(OrganisationCreationRequest request = null)
        {
            return CreateOrganisationWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateOrganisationWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied organisation JSON; null sends no body and a supplied object requires its original name without chart or organisation defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created an organisation or started chart setup.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateOrganisationWithResultAsyncWithHttpInfo(OrganisationCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/organisation/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateOrganisationWithResult", response, ExceptionFactory);
        }
    }
}
