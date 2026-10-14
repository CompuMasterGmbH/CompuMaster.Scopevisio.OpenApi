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
    public partial class ProjectApi
    {
        /// <summary>Assigns an explicitly selected existing mail to a selected project.</summary>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte, Mailbot, Mailagent (Bearbeiten) and verifies the existing mail and selected target. Required mailId is distinct from target IDs and is not chosen, converted or looked up. The request is deeply copied before HTTP without default mail, account selection, sender/recipient lookup, additional assignment or mutation replay. Cancellation after sending cannot establish whether the mail was assigned. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy AssignMailForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">The explicit mailId is absent, has an incompatible JSON kind or exceeds Int64 width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AssignMailToProjectWithResult(long projectId, MailAssignmentWriteRequest request)
        {
            return AssignMailToProjectWithResultWithHttpInfo(projectId, request).Data;
        }

        /// <inheritdoc cref="AssignMailToProjectWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AssignMailToProjectWithResultWithHttpInfo(long projectId, MailAssignmentWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/project/{projectId}/assignMail";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AssignMailToProjectWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AssignMailToProjectWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AssignMailToProjectWithResultAsync(long projectId, MailAssignmentWriteRequest request)
        {
            return AssignMailToProjectWithResultAsync(projectId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToProjectWithResult"/>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AssignMailToProjectWithResultAsync(long projectId, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AssignMailToProjectWithResultAsyncWithHttpInfo(projectId, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AssignMailToProjectWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToProjectWithResultAsyncWithHttpInfo(long projectId, MailAssignmentWriteRequest request)
        {
            return AssignMailToProjectWithResultAsyncWithHttpInfo(projectId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToProjectWithResultWithHttpInfo"/>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToProjectWithResultAsyncWithHttpInfo(long projectId, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/project/{projectId}/assignMail";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AssignMailToProjectWithResult", response, ExceptionFactory);
        }
    }
}
