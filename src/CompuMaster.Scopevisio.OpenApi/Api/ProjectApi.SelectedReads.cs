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
        /// <summary>Retrieves the complete JSON details of a selected project.</summary>
        /// <param name="id">The explicitly selected project master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>Omitting formatValues retains the server default of formatted values; explicit false requests unformatted values. The server requires the Projekte (Anzeigen) profile and declares no complete error-status list. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetProjectDetails(long id, string fields = null, bool? formatValues = null)
        {
            return GetProjectDetailsWithHttpInfo(id, fields, formatValues).Data;
        }

        /// <inheritdoc cref="GetProjectDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetProjectDetailsWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            var localVarPath = "/project/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProjectDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProjectDetails"/>
        public Task<JToken> GetProjectDetailsAsync(long id, string fields = null, bool? formatValues = null)
        {
            return GetProjectDetailsAsync(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectDetails"/>
        /// <param name="id">The explicitly selected project master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetProjectDetailsAsync(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            return (await GetProjectDetailsAsyncWithHttpInfo(id, fields, formatValues, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProjectDetailsAsyncWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            return GetProjectDetailsAsyncWithHttpInfo(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected project master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProjectDetailsAsyncWithHttpInfo(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/project/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProjectDetails", response, ExceptionFactory);
        }

    }
}
