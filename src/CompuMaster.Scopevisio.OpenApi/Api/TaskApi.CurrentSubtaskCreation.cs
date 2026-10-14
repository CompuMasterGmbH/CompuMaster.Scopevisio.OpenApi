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
        /// <summary>Creates a subtask for an explicitly selected parent task.</summary>
        /// <param name="request">The required complete subtask JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Aufgabe, Kontakte (Bearbeiten), parent-task, responsible-contact, status and time semantics. Creation requires a body and explicit taskId; no parent task is created or looked up implicitly. Original IDs, epoch milliseconds, strings and hours are not normalized, repaired or calculated. No account selection, default values, hidden child/parent write or mutation replay is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Historical TaskForm/SubtaskForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or creation lacks its required field.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateSubtaskWithResult(SubtaskWriteRequest request)
        {
            return CreateSubtaskWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateSubtaskWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateSubtaskWithResultWithHttpInfo(SubtaskWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/subtask/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateSubtaskWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateSubtaskWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateSubtaskWithResultAsync(SubtaskWriteRequest request)
        {
            return CreateSubtaskWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateSubtaskWithResult"/>
        /// <param name="request">The required complete subtask JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the subtask.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateSubtaskWithResultAsync(SubtaskWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateSubtaskWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateSubtaskWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateSubtaskWithResultAsyncWithHttpInfo(SubtaskWriteRequest request)
        {
            return CreateSubtaskWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateSubtaskWithResultWithHttpInfo"/>
        /// <param name="request">The required complete subtask JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the subtask.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateSubtaskWithResultAsyncWithHttpInfo(SubtaskWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/subtask/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateSubtaskWithResult", response, ExceptionFactory);
        }
    }
}
