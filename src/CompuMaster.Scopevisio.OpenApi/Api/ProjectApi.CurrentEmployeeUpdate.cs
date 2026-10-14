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
        /// <summary>Updates explicitly supplied fields of a selected project-team entry.</summary>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="employeeId">The explicit original signed Int64 project-team-entry identifier, not the personal contact ID, sent using invariant text without lookup or conversion.</param>
        /// <param name="request">The required complete project-team-entry JSON, copied before HTTP I/O without inserting defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Kontakte, Projekte (Bearbeiten), personal-contact, catalogue and rate semantics. Both bodies are required; creation requires an explicit contactId. Partial updates retain omitted fields and send explicit JSON null or empty text without assuming every field can be cleared. The employeeId selects a project-team entry, not a contact; no identity lookup or conversion is performed. Original rates and role strings are not normalized or calculated. No account selection, inserted defaults or mutation retry is added. Cancellation after sending cannot establish the server outcome. Parsed result fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy ProjectEmployeeForm methods and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, or a rate is not finite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateProjectEmployeeWithResult(long projectId, long employeeId, ProjectEmployeeWriteRequest request)
        {
            return UpdateProjectEmployeeWithResultWithHttpInfo(projectId, employeeId, request).Data;
        }

        /// <inheritdoc cref="UpdateProjectEmployeeWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateProjectEmployeeWithResultWithHttpInfo(long projectId, long employeeId, ProjectEmployeeWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(false);
            var localVarPath = "/project/{projectId}/projectemployee/{employeeId}";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(CultureInfo.InvariantCulture)));
            localVarPath = localVarPath.Replace("{employeeId}", Uri.EscapeDataString(employeeId.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateProjectEmployeeWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateProjectEmployeeWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateProjectEmployeeWithResultAsync(long projectId, long employeeId, ProjectEmployeeWriteRequest request)
        {
            return UpdateProjectEmployeeWithResultAsync(projectId, employeeId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProjectEmployeeWithResult"/>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="employeeId">The explicit original signed Int64 project-team-entry identifier, not the personal contact ID, sent using invariant text without lookup or conversion.</param>
        /// <param name="request">The required complete project-team-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateProjectEmployeeWithResultAsync(long projectId, long employeeId, ProjectEmployeeWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateProjectEmployeeWithResultAsyncWithHttpInfo(projectId, employeeId, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateProjectEmployeeWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProjectEmployeeWithResultAsyncWithHttpInfo(long projectId, long employeeId, ProjectEmployeeWriteRequest request)
        {
            return UpdateProjectEmployeeWithResultAsyncWithHttpInfo(projectId, employeeId, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateProjectEmployeeWithResultWithHttpInfo"/>
        /// <param name="projectId">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="employeeId">The explicit original signed Int64 project-team-entry identifier, not the personal contact ID, sent using invariant text without lookup or conversion.</param>
        /// <param name="request">The required complete project-team-entry JSON, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateProjectEmployeeWithResultAsyncWithHttpInfo(long projectId, long employeeId, ProjectEmployeeWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var body = request.SnapshotForSend(false);
            var localVarPath = "/project/{projectId}/projectemployee/{employeeId}";
            localVarPath = localVarPath.Replace("{projectId}", Uri.EscapeDataString(projectId.ToString(CultureInfo.InvariantCulture)));
            localVarPath = localVarPath.Replace("{employeeId}", Uri.EscapeDataString(employeeId.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateProjectEmployeeWithResult", response, ExceptionFactory);
        }
    }
}
