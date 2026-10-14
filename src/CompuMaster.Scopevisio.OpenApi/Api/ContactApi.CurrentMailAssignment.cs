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
        /// <summary>Assigns an explicitly selected existing mail to a selected contact.</summary>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Kontakte, Mailbot, Mailagent (Bearbeiten) and verifies the existing mail and selected target. Required mailId is distinct from target IDs and is not chosen, converted or looked up. The request is deeply copied before HTTP without default mail, account selection, sender/recipient lookup, additional assignment or mutation replay. Cancellation after sending cannot establish whether the mail was assigned. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy AssignMailForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required contact selector or request is null.</exception>
        /// <exception cref="ArgumentException">The explicit mailId is absent or outside JSON Int64 width, or the contact selector is a path dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken AssignMailToContactWithResult(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request)
        {
            return AssignMailToContactWithResultWithHttpInfo(contactIdOrLegacyNumber, request).Data;
        }

        /// <inheritdoc cref="AssignMailToContactWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> AssignMailToContactWithResultWithHttpInfo(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request)
        {
            if (contactIdOrLegacyNumber == null) throw new ArgumentNullException(nameof(contactIdOrLegacyNumber));
            if (contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..") throw new ArgumentException("The target contact selector cannot be a path dot segment.", nameof(contactIdOrLegacyNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/assignMail";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("AssignMailToContactWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="AssignMailToContactWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> AssignMailToContactWithResultAsync(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request)
        {
            return AssignMailToContactWithResultAsync(contactIdOrLegacyNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToContactWithResult"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> AssignMailToContactWithResultAsync(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            return (await AssignMailToContactWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="AssignMailToContactWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToContactWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request)
        {
            return AssignMailToContactWithResultAsyncWithHttpInfo(contactIdOrLegacyNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="AssignMailToContactWithResultWithHttpInfo"/>
        /// <param name="contactIdOrLegacyNumber">The original contact ID or legacy number string, escaped as one path segment without lookup, trimming or number conversion. Null and path dot segments are rejected.</param>
        /// <param name="request">The required complete assignment JSON with an explicit original mailId, copied before HTTP I/O without defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have assigned the mail.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> AssignMailToContactWithResultAsyncWithHttpInfo(string contactIdOrLegacyNumber, MailAssignmentWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (contactIdOrLegacyNumber == null) throw new ArgumentNullException(nameof(contactIdOrLegacyNumber));
            if (contactIdOrLegacyNumber == "." || contactIdOrLegacyNumber == "..") throw new ArgumentException("The target contact selector cannot be a path dot segment.", nameof(contactIdOrLegacyNumber));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend();
            var localVarPath = "/contact/{contactIdOrLegacyNumber}/assignMail";
            localVarPath = localVarPath.Replace("{contactIdOrLegacyNumber}", Uri.EscapeDataString(contactIdOrLegacyNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("AssignMailToContactWithResult", response, ExceptionFactory);
        }
    }
}
