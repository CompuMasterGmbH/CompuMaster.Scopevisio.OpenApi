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
    public partial class ContactApi
    {
        /// <summary>Modifies explicitly supplied Contact fields.</summary>
        /// <param name="contactId">The required Contact master ID.</param>
        /// <param name="modification">The required field map. Omitted birthDate retains it; an empty birthDate string clears it.</param>
        /// <returns>The result payload, or null for an empty response.</returns>
        /// <remarks>The SDK adds no mutation retries. Cancellation after sending does not establish the server outcome.</remarks>
        /// <exception cref="ArgumentException">A required identifier or known request field is invalid.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or its numeric values exceed the parser range.</exception>
        /// <exception cref="ArgumentNullException">The required request body is null.</exception>
        public JToken ModifyContactFields(string contactId, ContactModification modification)
        {
            return ModifyContactFieldsWithHttpInfo(contactId, modification).Data;
        }

        /// <inheritdoc cref="ModifyContactFields"/>
        /// <returns>The status, headers, unchanged raw response and result data; a 204 empty response has null data.</returns>
        public ApiResponse<JToken> ModifyContactFieldsWithHttpInfo(string contactId, ContactModification modification)
        {
            if (string.IsNullOrEmpty(contactId)) throw new ArgumentException("The Contact ID is required.", nameof(contactId));
            if (modification == null) throw new ArgumentNullException(nameof(modification));
            ValidateContactModification(modification);
            var localVarPath = "/contact/modify/{contactId}";
            localVarPath = localVarPath.Replace("{contactId}", Uri.EscapeDataString(contactId));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Patch,
                new List<KeyValuePair<string, string>>(), modification.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ModifyContactFields", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ModifyContactFields"/>
        public Task<JToken> ModifyContactFieldsAsync(string contactId, ContactModification modification)
        {
            return ModifyContactFieldsAsync(contactId, modification, CancellationToken.None);
        }

        /// <inheritdoc cref="ModifyContactFields"/>
        /// <param name="contactId">The required Contact master ID.</param>
        /// <param name="modification">The required field map. Omitted birthDate retains it; an empty birthDate string clears it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<JToken> ModifyContactFieldsAsync(string contactId, ContactModification modification, CancellationToken cancellationToken)
        {
            return (await ModifyContactFieldsAsyncWithHttpInfo(contactId, modification, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ModifyContactFieldsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ModifyContactFieldsAsyncWithHttpInfo(string contactId, ContactModification modification)
        {
            return ModifyContactFieldsAsyncWithHttpInfo(contactId, modification, CancellationToken.None);
        }

        /// <inheritdoc cref="ModifyContactFieldsWithHttpInfo"/>
        /// <param name="contactId">The required Contact master ID.</param>
        /// <param name="modification">The required field map. Omitted birthDate retains it; an empty birthDate string clears it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> ModifyContactFieldsAsyncWithHttpInfo(string contactId, ContactModification modification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(contactId)) throw new ArgumentException("The Contact ID is required.", nameof(contactId));
            if (modification == null) throw new ArgumentNullException(nameof(modification));
            ValidateContactModification(modification);
            var localVarPath = "/contact/modify/{contactId}";
            localVarPath = localVarPath.Replace("{contactId}", Uri.EscapeDataString(contactId));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Patch,
                new List<KeyValuePair<string, string>>(), modification.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ModifyContactFields", response, ExceptionFactory);
        }

        private static void ValidateContactModification(ContactModification modification)
        {
            var value = modification["birthDate"];
            if (value == null) return;
            DateTime parsed;
            if (value.Type != JTokenType.String ||
                (value.Value<string>() != "" && !DateTime.TryParseExact(value.Value<string>(), "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsed)))
                throw new ArgumentException("The birth date must be a valid dd.MM.yyyy JSON string or an empty string to clear it.", nameof(modification));
        }
    }
}
