// Bounded read generator 1. Reviewed imports only.
// Contract SHA256: 40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339. Operation SHA256: 5e121e88932433e7bd35a5ecb8cac6f155c7458cb31220b149c5713abf8cb410.
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
    public partial class TaskApi
    {
        /// <summary>Retrieves the complete JSON subtasks of a selected task.</summary>
        /// <param name="taskId">The explicitly selected task master identifier.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Aufgabe (Anzeigen) profile. The manufacturer declares no fixed success schema; HTTP 404 can mean a missing task or missing authorization. The original signed Int64 selection is sent unchanged. Existing generated methods retain their null-data behavior. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetSubtaskDetails(long taskId)
        {
            return GetSubtaskDetailsWithHttpInfo(taskId).Data;
        }

        /// <inheritdoc cref="GetSubtaskDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetSubtaskDetailsWithHttpInfo(long taskId)
        {
            var localVarPath = "/task/{taskId}/subtasks";
            localVarPath = localVarPath.Replace("{taskId}", taskId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetSubtaskDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetSubtaskDetails"/>
        public Task<JToken> GetSubtaskDetailsAsync(long taskId)
        {
            return GetSubtaskDetailsAsync(taskId, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSubtaskDetails"/>
        /// <param name="taskId">The explicitly selected task master identifier.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetSubtaskDetailsAsync(long taskId, CancellationToken cancellationToken)
        {
            return (await GetSubtaskDetailsAsyncWithHttpInfo(taskId, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetSubtaskDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetSubtaskDetailsAsyncWithHttpInfo(long taskId)
        {
            return GetSubtaskDetailsAsyncWithHttpInfo(taskId, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSubtaskDetailsWithHttpInfo"/>
        /// <param name="taskId">The explicitly selected task master identifier.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetSubtaskDetailsAsyncWithHttpInfo(long taskId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/task/{taskId}/subtasks";
            localVarPath = localVarPath.Replace("{taskId}", taskId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetSubtaskDetails", response, ExceptionFactory);
        }

    }
}
