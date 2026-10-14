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
        /// <summary>Creates a time entry using explicitly supplied current fields.</summary>
        /// <param name="request">The required complete time-entry JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte, Zeiterfassung (Bearbeiten) for creation and Projekte (Bearbeiten) for updates. Creation requires a body and an explicit employeeId or employeeMail rather than both. Do not supply both orderId and orderNumber. Updates may omit both employee selectors and retain omitted fields; explicit JSON null or empty text is sent without assuming all fields can be cleared. Existing time-entry runs may prevent updates. Reference inference, configured full-day boundaries, catalogue values and permission semantics remain server-authoritative. No lookup, implicit account selection, automatic timestamp/default choices or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy TimeEntryForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required creation request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, the employee selector is missing, or mutually exclusive selectors are supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateTimeEntryWithResult(TimeEntryWriteRequest request)
        {
            return CreateTimeEntryWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateTimeEntryWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateTimeEntryWithResultWithHttpInfo(TimeEntryWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/timeentry/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateTimeEntryWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateTimeEntryWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateTimeEntryWithResultAsync(TimeEntryWriteRequest request)
        {
            return CreateTimeEntryWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTimeEntryWithResult"/>
        /// <param name="request">The required complete time-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the time entry.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateTimeEntryWithResultAsync(TimeEntryWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateTimeEntryWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateTimeEntryWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateTimeEntryWithResultAsyncWithHttpInfo(TimeEntryWriteRequest request)
        {
            return CreateTimeEntryWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTimeEntryWithResultWithHttpInfo"/>
        /// <param name="request">The required complete time-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the time entry.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateTimeEntryWithResultAsyncWithHttpInfo(TimeEntryWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(true);
            var localVarPath = "/timeentry/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateTimeEntryWithResult", response, ExceptionFactory);
        }
    }
}
