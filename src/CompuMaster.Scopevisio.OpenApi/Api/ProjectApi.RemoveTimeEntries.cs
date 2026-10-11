using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Removes the time entry identified by its ID or legacy number.</summary>
        /// <param name="id">The required time-entry ID or legacy number, sent as one path value.</param>
        /// <remarks>The SDK adds no mutation retries. Cancellation after sending does not establish the server outcome.</remarks>
        /// <exception cref="ArgumentException">A required identifier or known request field is invalid.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or its numeric values exceed the parser range.</exception>
        public void RemoveTimeEntry(string id)
        {
            RemoveTimeEntryWithHttpInfo(id);
        }

        /// <inheritdoc cref="RemoveTimeEntry"/>
        /// <returns>The status, headers, unchanged raw response and result data; a 204 empty response has null data.</returns>
        public ApiResponse<JToken> RemoveTimeEntryWithHttpInfo(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("The time-entry ID is required.", nameof(id));
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("RemoveTimeEntry", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RemoveTimeEntry"/>
        public Task RemoveTimeEntryAsync(string id)
        {
            return RemoveTimeEntryAsync(id, CancellationToken.None);
        }

        /// <inheritdoc cref="RemoveTimeEntry"/>
        /// <param name="id">The required time-entry ID or legacy number, sent as one path value.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task RemoveTimeEntryAsync(string id, CancellationToken cancellationToken)
        {
            await RemoveTimeEntryAsyncWithHttpInfo(id, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="RemoveTimeEntryWithHttpInfo"/>
        public Task<ApiResponse<JToken>> RemoveTimeEntryAsyncWithHttpInfo(string id)
        {
            return RemoveTimeEntryAsyncWithHttpInfo(id, CancellationToken.None);
        }

        /// <inheritdoc cref="RemoveTimeEntryWithHttpInfo"/>
        /// <param name="id">The required time-entry ID or legacy number, sent as one path value.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> RemoveTimeEntryAsyncWithHttpInfo(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("The time-entry ID is required.", nameof(id));
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("RemoveTimeEntry", response, ExceptionFactory);
        }

        /// <summary>Removes the time entry identified explicitly by its legacy number.</summary>
        /// <param name="legacyNumber">The required time-entry legacy number, sent as one path value.</param>
        /// <remarks>The SDK adds no mutation retries. Cancellation after sending does not establish the server outcome.</remarks>
        /// <exception cref="ArgumentException">A required identifier or known request field is invalid.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or its numeric values exceed the parser range.</exception>
        public void RemoveTimeEntryByLegacyNumber(string legacyNumber)
        {
            RemoveTimeEntryByLegacyNumberWithHttpInfo(legacyNumber);
        }

        /// <inheritdoc cref="RemoveTimeEntryByLegacyNumber"/>
        /// <returns>The status, headers, unchanged raw response and result data; a 204 empty response has null data.</returns>
        public ApiResponse<JToken> RemoveTimeEntryByLegacyNumberWithHttpInfo(string legacyNumber)
        {
            if (string.IsNullOrEmpty(legacyNumber)) throw new ArgumentException("The time-entry legacy number is required.", nameof(legacyNumber));
            var localVarPath = "/timeentry/legacynumber/{legacynumber}";
            localVarPath = localVarPath.Replace("{legacynumber}", Uri.EscapeDataString(legacyNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("RemoveTimeEntryByLegacyNumber", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RemoveTimeEntryByLegacyNumber"/>
        public Task RemoveTimeEntryByLegacyNumberAsync(string legacyNumber)
        {
            return RemoveTimeEntryByLegacyNumberAsync(legacyNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="RemoveTimeEntryByLegacyNumber"/>
        /// <param name="legacyNumber">The required time-entry legacy number, sent as one path value.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task RemoveTimeEntryByLegacyNumberAsync(string legacyNumber, CancellationToken cancellationToken)
        {
            await RemoveTimeEntryByLegacyNumberAsyncWithHttpInfo(legacyNumber, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="RemoveTimeEntryByLegacyNumberWithHttpInfo"/>
        public Task<ApiResponse<JToken>> RemoveTimeEntryByLegacyNumberAsyncWithHttpInfo(string legacyNumber)
        {
            return RemoveTimeEntryByLegacyNumberAsyncWithHttpInfo(legacyNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="RemoveTimeEntryByLegacyNumberWithHttpInfo"/>
        /// <param name="legacyNumber">The required time-entry legacy number, sent as one path value.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> RemoveTimeEntryByLegacyNumberAsyncWithHttpInfo(string legacyNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(legacyNumber)) throw new ArgumentException("The time-entry legacy number is required.", nameof(legacyNumber));
            var localVarPath = "/timeentry/legacynumber/{legacynumber}";
            localVarPath = localVarPath.Replace("{legacynumber}", Uri.EscapeDataString(legacyNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("RemoveTimeEntryByLegacyNumber", response, ExceptionFactory);
        }

    }
}
