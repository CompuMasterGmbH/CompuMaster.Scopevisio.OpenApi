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
        /// <summary>Adds a dimension entry with an explicitly supplied name, number and lock state.</summary>
        /// <param name="dimensionNameOrNumber">The original dimension name-or-number string, escaped as one segment without trimming, numeric conversion, zero removal or lookup. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete dimension-entry JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Berichte (Bearbeiten), selected dimension, entry-number, name and lock semantics. Both bodies are required. Creation requires number, name and an explicit locked boolean. The update selects an entry by body number and changes its name according to the primary summary and errors; number and name are required. The shared schema also requires locked, but name updates retain omission under general sparse-update rules without inserting false. Actual additional-field update effects, including lock or legacy ID, remain unverified and server-authoritative. No dimension or entry lookup, ID conversion, account selection, defaults, local merge or mutation replay is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy DimensionEntryForm APIs remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required dimension selector or request is null.</exception>
        /// <exception cref="ArgumentException">A required supplied field is absent, a known field has an incompatible JSON kind, an entry number exceeds Int64 width, or the dimension selector is a path dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AddDimensionEntryWithResult(string dimensionNameOrNumber, DimensionEntryWriteRequest request)
        {
            return AddDimensionEntryWithResultWithHttpInfo(dimensionNameOrNumber, request).Data;
        }

        /// <inheritdoc cref="AddDimensionEntryWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AddDimensionEntryWithResultWithHttpInfo(string dimensionNameOrNumber, DimensionEntryWriteRequest request)
        {
            if (dimensionNameOrNumber == null) throw new ArgumentNullException(nameof(dimensionNameOrNumber));
            if (dimensionNameOrNumber == "." || dimensionNameOrNumber == "..") throw new ArgumentException("The dimension selector cannot be a path dot segment.", nameof(dimensionNameOrNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/dimensions/{dimensionNameOrNumber}/dimensionentry/new";
            localVarPath = localVarPath.Replace("{dimensionNameOrNumber}", Uri.EscapeDataString(dimensionNameOrNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AddDimensionEntryWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AddDimensionEntryWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AddDimensionEntryWithResultAsync(string dimensionNameOrNumber, DimensionEntryWriteRequest request)
        {
            return AddDimensionEntryWithResultAsync(dimensionNameOrNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddDimensionEntryWithResult"/>
        /// <param name="dimensionNameOrNumber">The original dimension name-or-number string, escaped as one segment without trimming, numeric conversion, zero removal or lookup. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete dimension-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the dimension entry.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AddDimensionEntryWithResultAsync(string dimensionNameOrNumber, DimensionEntryWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AddDimensionEntryWithResultAsyncWithHttpInfo(dimensionNameOrNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AddDimensionEntryWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddDimensionEntryWithResultAsyncWithHttpInfo(string dimensionNameOrNumber, DimensionEntryWriteRequest request)
        {
            return AddDimensionEntryWithResultAsyncWithHttpInfo(dimensionNameOrNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AddDimensionEntryWithResultWithHttpInfo"/>
        /// <param name="dimensionNameOrNumber">The original dimension name-or-number string, escaped as one segment without trimming, numeric conversion, zero removal or lookup. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete dimension-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the dimension entry.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AddDimensionEntryWithResultAsyncWithHttpInfo(string dimensionNameOrNumber, DimensionEntryWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (dimensionNameOrNumber == null) throw new ArgumentNullException(nameof(dimensionNameOrNumber));
            if (dimensionNameOrNumber == "." || dimensionNameOrNumber == "..") throw new ArgumentException("The dimension selector cannot be a path dot segment.", nameof(dimensionNameOrNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/dimensions/{dimensionNameOrNumber}/dimensionentry/new";
            localVarPath = localVarPath.Replace("{dimensionNameOrNumber}", Uri.EscapeDataString(dimensionNameOrNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AddDimensionEntryWithResult", response, ExceptionFactory);
        }
    }
}
