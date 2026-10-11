using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ContactApi
    {
        /// <summary>Retrieves the child organisations of an organisation contact.</summary>
        /// <param name="id">The organisation contact master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits field selection.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema for this operation. Required account permissions are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetChildOrganisationContacts(long id, string fields = null)
        {
            return GetChildOrganisationContactsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetChildOrganisationContacts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> GetChildOrganisationContactsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/contact/{id}/childOrganisations";
            localVarPath = localVarPath.Replace("{id}", id.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetChildOrganisationContacts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetChildOrganisationContacts"/>
        public Task<JToken> GetChildOrganisationContactsAsync(long id, string fields = null)
        {
            return GetChildOrganisationContactsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetChildOrganisationContacts"/>
        /// <param name="id">The organisation contact master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits field selection.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetChildOrganisationContactsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetChildOrganisationContactsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetChildOrganisationContactsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetChildOrganisationContactsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetChildOrganisationContactsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetChildOrganisationContactsWithHttpInfo"/>
        /// <param name="id">The organisation contact master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits field selection.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetChildOrganisationContactsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/childOrganisations";
            localVarPath = localVarPath.Replace("{id}", id.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetChildOrganisationContacts", response, ExceptionFactory);
        }

    }
}
