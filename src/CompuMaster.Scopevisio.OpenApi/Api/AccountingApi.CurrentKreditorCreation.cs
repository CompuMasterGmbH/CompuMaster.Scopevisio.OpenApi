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
    public partial class AccountingApi
    {
        /// <summary>Creates or resolves a kreditor for explicitly supplied original contact selectors.</summary>
        /// <param name="request">The optional complete copied creation JSON; null sends no body and a supplied object requires an original contact selector.</param>
        /// <returns>The complete JSON result, or null for an empty body. HTTP 200 and 201 have no fixed success schema; original status and complete existing/not-created/new results remain available.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten). A supplied creation object requires at least one original contact selector; multiple selectors are preserved without local precedence or lookup. The server may choose a number range or summary account, generate a number, create a missing group, or resolve/create a supplied nested payment term. HTTP 200 does not prove new creation: it may report an existing account or, for kreditor, exhausted number ranges. No client defaults, financial calculation, extra request, account selection or mutation replay is added. Cancellation after sending cannot establish resulting account or nested-resource state. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy PersonalAccountForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required supplied contact selector or nested payment field is absent, a known kind or Int64 width is invalid, or a nested percentage is not finite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateKreditorWithResult(PersonalAccountCreationRequest request = null)
        {
            return CreateKreditorWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateKreditorWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateKreditorWithResultWithHttpInfo(PersonalAccountCreationRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/createkreditor";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateKreditorWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateKreditorWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateKreditorWithResultAsync(PersonalAccountCreationRequest request = null)
        {
            return CreateKreditorWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateKreditorWithResult"/>
        /// <param name="request">The optional complete copied creation JSON; null sends no body and a supplied object requires an original contact selector.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the account, group or payment term.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateKreditorWithResultAsync(PersonalAccountCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateKreditorWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateKreditorWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateKreditorWithResultAsyncWithHttpInfo(PersonalAccountCreationRequest request = null)
        {
            return CreateKreditorWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateKreditorWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied creation JSON; null sends no body and a supplied object requires an original contact selector.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the account, group or payment term.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateKreditorWithResultAsyncWithHttpInfo(PersonalAccountCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/createkreditor";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateKreditorWithResult", response, ExceptionFactory);
        }
    }
}
