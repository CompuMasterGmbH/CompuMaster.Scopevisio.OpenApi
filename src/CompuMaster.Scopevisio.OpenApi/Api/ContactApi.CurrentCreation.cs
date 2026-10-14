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
        /// <summary>Creates a contact using explicitly supplied current fields.</summary>
        /// <param name="request">The optional complete current contact JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Kontakte (Bearbeiten), additional Personal profile restrictions, catalogue values and field-dependent semantics. A supplied creation object requires lastname and person. Partial updates retain omitted fields and send explicit JSON null or empty text without assuming that every field can be cleared. Person is evaluated only during creation. Employer identifiers, fallback precedence, dates and visibility remain server-authoritative. No resource lookup, implicit account selection, automatic role/default choices or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy KontaktForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied known field has an incompatible JSON kind or an integer exceeds its declared width/range.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateContactWithResult(ContactWriteRequest request = null)
        {
            return CreateContactWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateContactWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateContactWithResultWithHttpInfo(ContactWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/contact/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateContactWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateContactWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateContactWithResultAsync(ContactWriteRequest request = null)
        {
            return CreateContactWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateContactWithResult"/>
        /// <param name="request">The optional complete contact JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateContactWithResultAsync(ContactWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateContactWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateContactWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateContactWithResultAsyncWithHttpInfo(ContactWriteRequest request = null)
        {
            return CreateContactWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateContactWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete contact JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateContactWithResultAsyncWithHttpInfo(ContactWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/contact/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateContactWithResult", response, ExceptionFactory);
        }
    }
}
