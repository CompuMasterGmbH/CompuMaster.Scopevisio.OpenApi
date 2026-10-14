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
        /// <summary>Updates explicitly supplied fields of a selected time entry.</summary>
        /// <param name="id">The explicit original signed Int64 time-entry identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current time-entry JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte, Zeiterfassung (Bearbeiten) for creation and Projekte (Bearbeiten) for updates. Creation requires a body and an explicit employeeId or employeeMail rather than both. Do not supply both orderId and orderNumber. Updates may omit both employee selectors and retain omitted fields; explicit JSON null or empty text is sent without assuming all fields can be cleared. Existing time-entry runs may prevent updates. Reference inference, configured full-day boundaries, catalogue values and permission semantics remain server-authoritative. No lookup, implicit account selection, automatic timestamp/default choices or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy TimeEntryForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, or mutually exclusive selectors are supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateTimeEntryWithResult(long id, TimeEntryWriteRequest request = null)
        {
            return UpdateTimeEntryWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateTimeEntryWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateTimeEntryWithResultWithHttpInfo(long id, TimeEntryWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateTimeEntryWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateTimeEntryWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateTimeEntryWithResultAsync(long id, TimeEntryWriteRequest request = null)
        {
            return UpdateTimeEntryWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateTimeEntryWithResult"/>
        /// <param name="id">The explicit original signed Int64 time-entry identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete time-entry JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateTimeEntryWithResultAsync(long id, TimeEntryWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateTimeEntryWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateTimeEntryWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateTimeEntryWithResultAsyncWithHttpInfo(long id, TimeEntryWriteRequest request = null)
        {
            return UpdateTimeEntryWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateTimeEntryWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 time-entry identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete time-entry JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateTimeEntryWithResultAsyncWithHttpInfo(long id, TimeEntryWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateTimeEntryWithResult", response, ExceptionFactory);
        }
    }
}
