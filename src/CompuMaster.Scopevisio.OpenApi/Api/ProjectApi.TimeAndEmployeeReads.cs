using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
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
        /// <summary>Retrieves the complete JSON details of a selected time entry.</summary>
        /// <param name="id">The explicitly selected time-entry identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Projekte (Anzeigen) profile. The manufacturer declares no fixed success schema or exhaustive GET error-status list. The original signed Int64 selections are sent unchanged. Existing generated methods retain their null-data behavior. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetTimeEntryDetails(long id, string fields = null)
        {
            return GetTimeEntryDetailsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetTimeEntryDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetTimeEntryDetailsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTimeEntryDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTimeEntryDetails"/>
        public Task<JToken> GetTimeEntryDetailsAsync(long id, string fields = null)
        {
            return GetTimeEntryDetailsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryDetails"/>
        /// <param name="id">The explicitly selected time-entry identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetTimeEntryDetailsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetTimeEntryDetailsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTimeEntryDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTimeEntryDetailsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetTimeEntryDetailsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected time-entry identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTimeEntryDetailsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/timeentry/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTimeEntryDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON details of a selected project-team entry.</summary>
        /// <param name="employeeId">The explicitly selected project-team entry identifier, not a contact identifier.</param>
        /// <param name="projectId">The explicitly selected project identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte, Projekte (Anzeigen) profiles. The employee identifier selects a project-team entry rather than a contact. The manufacturer declares no fixed success schema or exhaustive GET error-status list. The original signed Int64 selections are sent unchanged. Existing generated methods retain their null-data behavior. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetProjectEmployeeDetails(long employeeId, long projectId, string fields = null)
        {
            return GetProjectEmployeeDetailsWithHttpInfo(employeeId, projectId, fields).Data;
        }

        /// <inheritdoc cref="GetProjectEmployeeDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetProjectEmployeeDetailsWithHttpInfo(long employeeId, long projectId, string fields = null)
        {
            var localVarPath = "/project/{projectId}/projectemployee/{employeeId}";
            localVarPath = localVarPath.Replace("{employeeId}", employeeId.ToString(CultureInfo.InvariantCulture));
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProjectEmployeeDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProjectEmployeeDetails"/>
        public Task<JToken> GetProjectEmployeeDetailsAsync(long employeeId, long projectId, string fields = null)
        {
            return GetProjectEmployeeDetailsAsync(employeeId, projectId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectEmployeeDetails"/>
        /// <param name="employeeId">The explicitly selected project-team entry identifier, not a contact identifier.</param>
        /// <param name="projectId">The explicitly selected project identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetProjectEmployeeDetailsAsync(long employeeId, long projectId, string fields, CancellationToken cancellationToken)
        {
            return (await GetProjectEmployeeDetailsAsyncWithHttpInfo(employeeId, projectId, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectEmployeeDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProjectEmployeeDetailsAsyncWithHttpInfo(long employeeId, long projectId, string fields = null)
        {
            return GetProjectEmployeeDetailsAsyncWithHttpInfo(employeeId, projectId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectEmployeeDetailsWithHttpInfo"/>
        /// <param name="employeeId">The explicitly selected project-team entry identifier, not a contact identifier.</param>
        /// <param name="projectId">The explicitly selected project identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProjectEmployeeDetailsAsyncWithHttpInfo(long employeeId, long projectId, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/project/{projectId}/projectemployee/{employeeId}";
            localVarPath = localVarPath.Replace("{employeeId}", employeeId.ToString(CultureInfo.InvariantCulture));
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProjectEmployeeDetails", response, ExceptionFactory);
        }

    }
}
