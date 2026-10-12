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
        /// <summary>Retrieves the complete JSON details of a selected task.</summary>
        /// <param name="id">The explicitly selected task master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Aufgabe (Anzeigen) profile. The manufacturer declares no fixed success schema or complete error-status list. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetTaskDetails(long id, string fields = null)
        {
            return GetTaskDetailsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetTaskDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetTaskDetailsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/task/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTaskDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTaskDetails"/>
        public Task<JToken> GetTaskDetailsAsync(long id, string fields = null)
        {
            return GetTaskDetailsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTaskDetails"/>
        /// <param name="id">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetTaskDetailsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetTaskDetailsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTaskDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTaskDetailsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetTaskDetailsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTaskDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTaskDetailsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/task/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTaskDetails", response, ExceptionFactory);
        }

    }
}
