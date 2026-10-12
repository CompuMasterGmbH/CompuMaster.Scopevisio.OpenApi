using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ReportApi
    {
        /// <summary>Retrieves entries for the named budget group.</summary>
        /// <param name="budgetGroupName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces planning profiles and visibility. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">The required name is null, empty or blank.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetBudgetGroupDataSource(string budgetGroupName, string fields = null)
        {
            return GetBudgetGroupDataSourceWithHttpInfo(budgetGroupName, fields).Data;
        }

        /// <inheritdoc cref="GetBudgetGroupDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetBudgetGroupDataSourceWithHttpInfo(string budgetGroupName, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(budgetGroupName)) throw new ArgumentException("A nonblank name is required.", nameof(budgetGroupName));
            var localVarPath = "/datasource/budgetGroup";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("budgetGroupName", budgetGroupName));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetBudgetGroupDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetBudgetGroupDataSource"/>
        public Task<JToken> GetBudgetGroupDataSourceAsync(string budgetGroupName, string fields = null)
        {
            return GetBudgetGroupDataSourceAsync(budgetGroupName, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBudgetGroupDataSource"/>
        /// <param name="budgetGroupName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetBudgetGroupDataSourceAsync(string budgetGroupName, string fields, CancellationToken cancellationToken)
        {
            return (await GetBudgetGroupDataSourceAsyncWithHttpInfo(budgetGroupName, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetBudgetGroupDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetBudgetGroupDataSourceAsyncWithHttpInfo(string budgetGroupName, string fields = null)
        {
            return GetBudgetGroupDataSourceAsyncWithHttpInfo(budgetGroupName, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBudgetGroupDataSourceWithHttpInfo"/>
        /// <param name="budgetGroupName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetBudgetGroupDataSourceAsyncWithHttpInfo(string budgetGroupName, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(budgetGroupName)) throw new ArgumentException("A nonblank name is required.", nameof(budgetGroupName));
            var localVarPath = "/datasource/budgetGroup";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("budgetGroupName", budgetGroupName));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetBudgetGroupDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves entries for the named plan.</summary>
        /// <param name="planName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces planning profiles and visibility. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">The required name is null, empty or blank.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetPlanDataSource(string planName, string fields = null)
        {
            return GetPlanDataSourceWithHttpInfo(planName, fields).Data;
        }

        /// <inheritdoc cref="GetPlanDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetPlanDataSourceWithHttpInfo(string planName, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(planName)) throw new ArgumentException("A nonblank name is required.", nameof(planName));
            var localVarPath = "/datasource/plan";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("planName", planName));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetPlanDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetPlanDataSource"/>
        public Task<JToken> GetPlanDataSourceAsync(string planName, string fields = null)
        {
            return GetPlanDataSourceAsync(planName, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPlanDataSource"/>
        /// <param name="planName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetPlanDataSourceAsync(string planName, string fields, CancellationToken cancellationToken)
        {
            return (await GetPlanDataSourceAsyncWithHttpInfo(planName, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetPlanDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetPlanDataSourceAsyncWithHttpInfo(string planName, string fields = null)
        {
            return GetPlanDataSourceAsyncWithHttpInfo(planName, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPlanDataSourceWithHttpInfo"/>
        /// <param name="planName">The required nonblank name, sent unchanged without trimming or ID conversion.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetPlanDataSourceAsyncWithHttpInfo(string planName, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(planName)) throw new ArgumentException("A nonblank name is required.", nameof(planName));
            var localVarPath = "/datasource/plan";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("planName", planName));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetPlanDataSource", response, ExceptionFactory);
        }

    }
}
