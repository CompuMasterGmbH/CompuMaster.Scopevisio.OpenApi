using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Deletes the explicitly selected contact and retains the server acknowledgement.</summary>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number, escaped as one segment without trimming or conversion.</param>
        /// <returns>The complete JSON result if supplied, or null for the documented empty acknowledgement.</returns>
        /// <remarks>The server requires Kontakte (Bearbeiten), plus Personal when the contact is an employee. A missing contact or missing authorization can both produce the documented 404. Success is documented as HTTP 204 without a response schema. The complete raw status/headers remain available through HTTP-info forms. No body, query, lookup or automatic retry is sent. Cancellation after admission does not establish the server outcome. Historical methods and their defaults remain unchanged. Unexpected JSON fractions use bounded decimal precision; raw content remains exact.</remarks>
        /// <exception cref="ArgumentException">The explicit selector is null, blank or an exact dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">A supplied response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken DeleteContactByIdWithResult(string contactIdOrLegacyNumber)
        {
            return DeleteContactByIdWithResultWithHttpInfo(contactIdOrLegacyNumber).Data;
        }

        /// <inheritdoc cref="DeleteContactByIdWithResult"/>
        /// <returns>The original HTTP status, headers, raw response and complete JSON result if supplied.</returns>
        public ApiResponse<JToken> DeleteContactByIdWithResultWithHttpInfo(string contactIdOrLegacyNumber)
        {
            if (string.IsNullOrWhiteSpace(contactIdOrLegacyNumber) || contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..")
                throw new ArgumentException("An explicit contact ID or legacy number is required.", nameof(contactIdOrLegacyNumber));
            var localVarPath = "/contact/{contactIdOrLegacyNumber}";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("DeleteContactByIdWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DeleteContactByIdWithResult"/>
        public Task<JToken> DeleteContactByIdWithResultAsync(string contactIdOrLegacyNumber)
        {
            return DeleteContactByIdWithResultAsync(contactIdOrLegacyNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteContactByIdWithResult"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number, escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> DeleteContactByIdWithResultAsync(string contactIdOrLegacyNumber, CancellationToken cancellationToken)
        {
            return (await DeleteContactByIdWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DeleteContactByIdWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> DeleteContactByIdWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber)
        {
            return DeleteContactByIdWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteContactByIdWithResultWithHttpInfo"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number, escaped as one segment without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel admission and active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> DeleteContactByIdWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(contactIdOrLegacyNumber) || contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..")
                throw new ArgumentException("An explicit contact ID or legacy number is required.", nameof(contactIdOrLegacyNumber));
            var localVarPath = "/contact/{contactIdOrLegacyNumber}";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("DeleteContactByIdWithResult", response, ExceptionFactory);
        }
    }
}
