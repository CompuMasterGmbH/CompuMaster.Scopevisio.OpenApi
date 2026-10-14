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
    public partial class ContactApi
    {
        /// <summary>Creates a contact property using explicitly supplied current fields.</summary>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Kontakte bearbeiten (Bearbeiten), catalogue, employer and permission semantics. Both creation and update require a body; no individual fields are required. Partial updates retain omitted fields and send explicit JSON null or empty text without assuming every field can be cleared. Original epoch milliseconds, multi-sphere and employer-name strings are not normalized or looked up. No account selection, inserted defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy ContactPropertyForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required contact selector or request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, or the contact selector is a path dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateContactPropertyWithResult(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request)
        {
            return CreateContactPropertyWithResultWithHttpInfo(contactIdOrLegacyNumber, request).Data;
        }

        /// <inheritdoc cref="CreateContactPropertyWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateContactPropertyWithResultWithHttpInfo(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request)
        {
            if (contactIdOrLegacyNumber == null) throw new ArgumentNullException(nameof(contactIdOrLegacyNumber));
            if (contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..") throw new ArgumentException("The contact selector cannot be a path dot segment.", nameof(contactIdOrLegacyNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/contactProperty/new";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateContactPropertyWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateContactPropertyWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateContactPropertyWithResultAsync(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request)
        {
            return CreateContactPropertyWithResultAsync(contactIdOrLegacyNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateContactPropertyWithResult"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the contact property.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateContactPropertyWithResultAsync(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateContactPropertyWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateContactPropertyWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateContactPropertyWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request)
        {
            return CreateContactPropertyWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateContactPropertyWithResultWithHttpInfo"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the contact property.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateContactPropertyWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber, ContactPropertyWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (contactIdOrLegacyNumber == null) throw new ArgumentNullException(nameof(contactIdOrLegacyNumber));
            if (contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..") throw new ArgumentException("The contact selector cannot be a path dot segment.", nameof(contactIdOrLegacyNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/contactProperty/new";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateContactPropertyWithResult", response, ExceptionFactory);
        }
    }
}
