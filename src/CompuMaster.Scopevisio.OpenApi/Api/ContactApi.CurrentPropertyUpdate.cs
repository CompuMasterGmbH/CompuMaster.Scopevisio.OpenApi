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
        /// <summary>Updates explicitly supplied fields of a selected contact property.</summary>
        /// <param name="propertyId">The explicit original signed Int64 contact-property identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Kontakte bearbeiten (Bearbeiten), catalogue, employer and permission semantics. Both creation and update require a body; no individual fields are required. Partial updates retain omitted fields and send explicit JSON null or empty text without assuming every field can be cleared. Original epoch milliseconds, multi-sphere and employer-name strings are not normalized or looked up. No account selection, inserted defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy ContactPropertyForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind or an integer exceeds its declared width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateContactPropertyWithResult(long propertyId, ContactPropertyWriteRequest request)
        {
            return UpdateContactPropertyWithResultWithHttpInfo(propertyId, request).Data;
        }

        /// <inheritdoc cref="UpdateContactPropertyWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateContactPropertyWithResultWithHttpInfo(long propertyId, ContactPropertyWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(false);
            var localVarPath = "/contact/contactProperty/{propertyId}";
            localVarPath = localVarPath.Replace("{propertyId}", Uri.EscapeDataString(propertyId.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateContactPropertyWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateContactPropertyWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateContactPropertyWithResultAsync(long propertyId, ContactPropertyWriteRequest request)
        {
            return UpdateContactPropertyWithResultAsync(propertyId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateContactPropertyWithResult"/>
        /// <param name="propertyId">The explicit original signed Int64 contact-property identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateContactPropertyWithResultAsync(long propertyId, ContactPropertyWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateContactPropertyWithResultAsyncWithHttpInfo(propertyId, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateContactPropertyWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateContactPropertyWithResultAsyncWithHttpInfo(long propertyId, ContactPropertyWriteRequest request)
        {
            return UpdateContactPropertyWithResultAsyncWithHttpInfo(propertyId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateContactPropertyWithResultWithHttpInfo"/>
        /// <param name="propertyId">The explicit original signed Int64 contact-property identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete contact-property JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateContactPropertyWithResultAsyncWithHttpInfo(long propertyId, ContactPropertyWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(false);
            var localVarPath = "/contact/contactProperty/{propertyId}";
            localVarPath = localVarPath.Replace("{propertyId}", Uri.EscapeDataString(propertyId.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateContactPropertyWithResult", response, ExceptionFactory);
        }
    }
}
