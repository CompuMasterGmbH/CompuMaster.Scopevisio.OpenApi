using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ContactApi
    {
        /// <summary>Retrieves the complete Eurodata information for a contact using a bodyless request.</summary>
        /// <param name="id">The required Int64 contact identifier, sent unchanged using invariant text.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The server enforces the Lohn profile and contact visibility. The manufacturer specifies no fixed success schema and declares an optional GET string body under any media type without explaining its semantics; this workflow omits that optional body. Supplying undocumented GET-body data remains deferred for manufacturer/live clarification. No fields, paging, mutation or permission inference is added. Date-like strings and integer timestamps remain unchanged. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetContactEurodataInfo(long id)
        {
            return GetContactEurodataInfoWithHttpInfo(id).Data;
        }

        /// <inheritdoc cref="GetContactEurodataInfo"/>
        /// <returns>The status, response/content headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetContactEurodataInfoWithHttpInfo(long id)
        {
            var localVarPath = "/contact/{id}/eurodataInfo";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactEurodataInfo", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactEurodataInfo"/>
        public Task<JToken> GetContactEurodataInfoAsync(long id)
        {
            return GetContactEurodataInfoAsync(id, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEurodataInfo"/>
        /// <param name="id">The required Int64 contact identifier, sent unchanged using invariant text.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetContactEurodataInfoAsync(long id, CancellationToken cancellationToken)
        {
            return (await GetContactEurodataInfoAsyncWithHttpInfo(id, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactEurodataInfoWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactEurodataInfoAsyncWithHttpInfo(long id)
        {
            return GetContactEurodataInfoAsyncWithHttpInfo(id, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEurodataInfoWithHttpInfo"/>
        /// <param name="id">The required Int64 contact identifier, sent unchanged using invariant text.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactEurodataInfoAsyncWithHttpInfo(long id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/eurodataInfo";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactEurodataInfo", response, ExceptionFactory);
        }
    }
}
