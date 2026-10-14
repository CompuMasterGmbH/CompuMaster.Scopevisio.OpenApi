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
        /// <summary>Imports explicitly supplied original incoming EML content.</summary>
        /// <param name="request">The optional complete original incoming-mail JSON, copied before HTTP I/O. Null omits the body; a supplied object requires EML.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server requires Mailbot, Mailagent (Bearbeiten). It defaults omitted encoding to UTF-8 and omitted skipStandardContactAssignment to false, then may associate matching sender/recipient contacts except the current user or configured valid senders. The client preserves original text, encoding and omission/false/true choices without defaults, EML parsing, decoding, delivery, contact lookup or additional assignments. The complete request is copied before HTTP; no account selection, hidden follow-up or mutation replay is added. Cancellation after sending cannot establish whether import or association occurred. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy IncomingMailForm APIs and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied object lacks EML text or a known field has an incompatible JSON kind.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ImportIncomingMailWithResult(IncomingMailWriteRequest request = null)
        {
            return ImportIncomingMailWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportIncomingMailWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ImportIncomingMailWithResultWithHttpInfo(IncomingMailWriteRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/incomingmail/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportIncomingMailWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportIncomingMailWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ImportIncomingMailWithResultAsync(IncomingMailWriteRequest request = null)
        {
            return ImportIncomingMailWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportIncomingMailWithResult"/>
        /// <param name="request">The optional complete original incoming-mail JSON, copied before HTTP I/O. Null omits the body; a supplied object requires EML.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported or associated the mail.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ImportIncomingMailWithResultAsync(IncomingMailWriteRequest request, CancellationToken cancellationToken)
        {
            return (await ImportIncomingMailWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportIncomingMailWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportIncomingMailWithResultAsyncWithHttpInfo(IncomingMailWriteRequest request = null)
        {
            return ImportIncomingMailWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportIncomingMailWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete original incoming-mail JSON, copied before HTTP I/O. Null omits the body; a supplied object requires EML.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported or associated the mail.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportIncomingMailWithResultAsyncWithHttpInfo(IncomingMailWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/incomingmail/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportIncomingMailWithResult", response, ExceptionFactory);
        }
    }
}
