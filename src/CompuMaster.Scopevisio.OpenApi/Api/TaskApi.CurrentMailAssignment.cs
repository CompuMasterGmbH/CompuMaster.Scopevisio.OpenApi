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
        /// <summary>Assigns an explicitly selected existing mail to a selected task.</summary>
        /// <param name="taskId">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Aufgaben,Mailbot,Mailagent (Bearbeiten) and verifies the existing mail and selected target. Required mailId is distinct from target IDs and is not chosen, converted or looked up. The request is deeply copied before HTTP without default mail, account selection, sender/recipient lookup, additional assignment or mutation replay. Cancellation after sending cannot establish whether the mail was assigned. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy AssignMailForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">The explicit mailId is absent, has an incompatible JSON kind or exceeds Int64 width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AssignMailToTaskWithResult(long taskId, MailAssignmentWriteRequest request)
        {
            return AssignMailToTaskWithResultWithHttpInfo(taskId, request).Data;
        }

        /// <inheritdoc cref="AssignMailToTaskWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AssignMailToTaskWithResultWithHttpInfo(long taskId, MailAssignmentWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/task/{taskId}/assignMail";
            localVarPath = localVarPath.Replace("{taskId}", Uri.EscapeDataString(taskId.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AssignMailToTaskWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AssignMailToTaskWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AssignMailToTaskWithResultAsync(long taskId, MailAssignmentWriteRequest request)
        {
            return AssignMailToTaskWithResultAsync(taskId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToTaskWithResult"/>
        /// <param name="taskId">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AssignMailToTaskWithResultAsync(long taskId, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AssignMailToTaskWithResultAsyncWithHttpInfo(taskId, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AssignMailToTaskWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToTaskWithResultAsyncWithHttpInfo(long taskId, MailAssignmentWriteRequest request)
        {
            return AssignMailToTaskWithResultAsyncWithHttpInfo(taskId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToTaskWithResultWithHttpInfo"/>
        /// <param name="taskId">The explicit original signed Int64 task identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToTaskWithResultAsyncWithHttpInfo(long taskId, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/task/{taskId}/assignMail";
            localVarPath = localVarPath.Replace("{taskId}", Uri.EscapeDataString(taskId.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AssignMailToTaskWithResult", response, ExceptionFactory);
        }
    }
}
