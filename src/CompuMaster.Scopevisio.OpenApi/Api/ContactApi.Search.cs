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
        /// <summary>Searches contacts and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's contact read permissions. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchContacts(SearchRequest query = null)
        {
            return SearchContactsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchContacts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchContactsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/contacts";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchContacts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchContacts"/>
        public Task<JToken> SearchContactsAsync(SearchRequest query = null)
        {
            return SearchContactsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchContacts"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchContactsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchContactsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchContactsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchContactsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchContactsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchContactsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchContactsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contacts";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchContacts", response, ExceptionFactory);
        }

        /// <summary>Searches contact comments and retains the complete JSON response.</summary>
        /// <param name="contactIdOrLegacyNumber">The contact master ID or legacy number.</param>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's contact and comment read permissions. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        /// <exception cref="ArgumentException">The contact identifier is null or empty.</exception>
        public JToken SearchContactComments(string contactIdOrLegacyNumber, SearchRequest query = null)
        {
            return SearchContactCommentsWithHttpInfo(contactIdOrLegacyNumber, query).Data;
        }

        /// <inheritdoc cref="SearchContactComments"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchContactCommentsWithHttpInfo(string contactIdOrLegacyNumber, SearchRequest query = null)
        {
            if (string.IsNullOrEmpty(contactIdOrLegacyNumber)) throw new ArgumentException("The contact ID or legacy number is required.", nameof(contactIdOrLegacyNumber));
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/comments";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchContactComments", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchContactComments"/>
        public Task<JToken> SearchContactCommentsAsync(string contactIdOrLegacyNumber, SearchRequest query = null)
        {
            return SearchContactCommentsAsync(contactIdOrLegacyNumber, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchContactComments"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="contactIdOrLegacyNumber">The contact identifier used in the request path.</param>
        public async Task<JToken> SearchContactCommentsAsync(string contactIdOrLegacyNumber, SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchContactCommentsAsyncWithHttpInfo(contactIdOrLegacyNumber, query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchContactCommentsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchContactCommentsAsyncWithHttpInfo(string contactIdOrLegacyNumber, SearchRequest query = null)
        {
            return SearchContactCommentsAsyncWithHttpInfo(contactIdOrLegacyNumber, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchContactCommentsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="contactIdOrLegacyNumber">The contact identifier used in the request path.</param>
        public async Task<ApiResponse<JToken>> SearchContactCommentsAsyncWithHttpInfo(string contactIdOrLegacyNumber, SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(contactIdOrLegacyNumber)) throw new ArgumentException("The contact ID or legacy number is required.", nameof(contactIdOrLegacyNumber));
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/comments";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchContactComments", response, ExceptionFactory);
        }

    }
}
