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
    public partial class TaskApi
    {
        /// <summary>Updates explicitly supplied fields of a selected task.</summary>
        /// <param name="id">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current task JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Aufgabe (Bearbeiten), contact, project, invoice, catalogue, time and custom-field semantics. Creation requires a body and explicit topic; the task update body is optional. Omitted update fields retain existing data, and explicit null or empty text remains server-authoritative. Original IDs, epoch milliseconds, strings, hours, boolean flags and mixed custom values are not normalized, repaired or calculated. Contact ID and legacy ID retain the documented server precedence; no selector lookup or XOR rule is added. No account selection, default values, hidden child/parent write or mutation replay is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Historical TaskForm/SubtaskForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or creation lacks its required field.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateTaskWithResult(long id, TaskWriteRequest request = null)
        {
            return UpdateTaskWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateTaskWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateTaskWithResultWithHttpInfo(long id, TaskWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/task/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateTaskWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateTaskWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateTaskWithResultAsync(long id, TaskWriteRequest request = null)
        {
            return UpdateTaskWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateTaskWithResult"/>
        /// <param name="id">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete task JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateTaskWithResultAsync(long id, TaskWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateTaskWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateTaskWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateTaskWithResultAsyncWithHttpInfo(long id, TaskWriteRequest request = null)
        {
            return UpdateTaskWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateTaskWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete task JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateTaskWithResultAsyncWithHttpInfo(long id, TaskWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/task/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateTaskWithResult", response, ExceptionFactory);
        }
    }
}
