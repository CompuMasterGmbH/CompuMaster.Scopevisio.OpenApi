using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Retrieves typed project resources using the current resource and revenue endpoints.</summary>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <returns>The typed JSON envelope, or null for an empty or JSON-null response.</returns>
        /// <remarks>Unknown fields and metadata remain accessible through the JSON indexer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON, has an invalid envelope, or contains a fractional value exceeding the decimal range.</exception>
        public ProjectResourcesResponse GetProjectResourceDetails(long projectId, string fields = null)
        {
            return GetProjectResourceDetailsWithHttpInfo(projectId, fields).Data;
        }

        /// <inheritdoc cref="GetProjectResourceDetails"/>
        /// <returns>The typed envelope with status, headers and unchanged raw JSON.</returns>
        public ApiResponse<ProjectResourcesResponse> GetProjectResourceDetailsWithHttpInfo(long projectId, string fields = null)
        {
            var localVarPath = "/project/{projectId}/projectresources";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapProjectResponse(SearchJsonResponse.Parse("GetProjectResourceDetails", response, ExceptionFactory), data => new ProjectResourcesResponse(data));
        }

        /// <inheritdoc cref="GetProjectResourceDetails"/>
        public Task<ProjectResourcesResponse> GetProjectResourceDetailsAsync(long projectId, string fields = null)
        {
            return GetProjectResourceDetailsAsync(projectId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectResourceDetails"/>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ProjectResourcesResponse> GetProjectResourceDetailsAsync(long projectId, string fields, CancellationToken cancellationToken)
        {
            return (await GetProjectResourceDetailsAsyncWithHttpInfo(projectId, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectResourceDetailsWithHttpInfo"/>
        public Task<ApiResponse<ProjectResourcesResponse>> GetProjectResourceDetailsAsyncWithHttpInfo(long projectId, string fields = null)
        {
            return GetProjectResourceDetailsAsyncWithHttpInfo(projectId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectResourceDetailsWithHttpInfo"/>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<ProjectResourcesResponse>> GetProjectResourceDetailsAsyncWithHttpInfo(long projectId, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/project/{projectId}/projectresources";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapProjectResponse(SearchJsonResponse.Parse("GetProjectResourceDetails", response, ExceptionFactory), data => new ProjectResourcesResponse(data));
        }

        /// <summary>Retrieves typed project revenues using the current resource and revenue endpoints.</summary>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <param name="revenueType">The requested revenue type, or null to retain the server planning default.</param>
        /// <returns>The typed JSON envelope, or null for an empty or JSON-null response.</returns>
        /// <remarks>Unknown fields and metadata remain accessible through the JSON indexer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON, has an invalid envelope, or contains a fractional value exceeding the decimal range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The revenue type is outside the documented range.</exception>
        public ProjectRevenuesResponse GetProjectRevenueDetails(long projectId, string fields = null, ProjectRevenueType? revenueType = null)
        {
            return GetProjectRevenueDetailsWithHttpInfo(projectId, fields, revenueType).Data;
        }

        /// <inheritdoc cref="GetProjectRevenueDetails"/>
        /// <returns>The typed envelope with status, headers and unchanged raw JSON.</returns>
        public ApiResponse<ProjectRevenuesResponse> GetProjectRevenueDetailsWithHttpInfo(long projectId, string fields = null, ProjectRevenueType? revenueType = null)
        {
            ValidateProjectRevenueType(revenueType);
            var localVarPath = "/project/{projectId}/projectrevenue";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (revenueType.HasValue) queryParams.Add(new KeyValuePair<string, string>("revenueType", ((int)revenueType.Value).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapProjectResponse(SearchJsonResponse.Parse("GetProjectRevenueDetails", response, ExceptionFactory), data => new ProjectRevenuesResponse(data));
        }

        /// <inheritdoc cref="GetProjectRevenueDetails"/>
        public Task<ProjectRevenuesResponse> GetProjectRevenueDetailsAsync(long projectId, string fields = null, ProjectRevenueType? revenueType = null)
        {
            return GetProjectRevenueDetailsAsync(projectId, fields, revenueType, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectRevenueDetails"/>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <param name="revenueType">The requested revenue type, or null to retain the server planning default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ProjectRevenuesResponse> GetProjectRevenueDetailsAsync(long projectId, string fields, ProjectRevenueType? revenueType, CancellationToken cancellationToken)
        {
            return (await GetProjectRevenueDetailsAsyncWithHttpInfo(projectId, fields, revenueType, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectRevenueDetailsWithHttpInfo"/>
        public Task<ApiResponse<ProjectRevenuesResponse>> GetProjectRevenueDetailsAsyncWithHttpInfo(long projectId, string fields = null, ProjectRevenueType? revenueType = null)
        {
            return GetProjectRevenueDetailsAsyncWithHttpInfo(projectId, fields, revenueType, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectRevenueDetailsWithHttpInfo"/>
        /// <param name="projectId">The project master ID.</param>
        /// <param name="fields">The optional comma-separated response field names; null omits selection.</param>
        /// <param name="revenueType">The requested revenue type, or null to retain the server planning default.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<ProjectRevenuesResponse>> GetProjectRevenueDetailsAsyncWithHttpInfo(long projectId, string fields, ProjectRevenueType? revenueType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateProjectRevenueType(revenueType);
            var localVarPath = "/project/{projectId}/projectrevenue";
            localVarPath = localVarPath.Replace("{projectId}", projectId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (revenueType.HasValue) queryParams.Add(new KeyValuePair<string, string>("revenueType", ((int)revenueType.Value).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapProjectResponse(SearchJsonResponse.Parse("GetProjectRevenueDetails", response, ExceptionFactory), data => new ProjectRevenuesResponse(data));
        }

        /// <summary>Retrieves typed project resources using the current resource and revenue endpoints.</summary>
        /// <param name="query">The required request containing both project-start date bounds.</param>
        /// <returns>The typed JSON envelope, or null for an empty or JSON-null response.</returns>
        /// <remarks>Unknown fields and metadata remain accessible through the JSON indexer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON, has an invalid envelope, or contains a fractional value exceeding the decimal range.</exception>
        /// <exception cref="ArgumentNullException">The required query is null.</exception>
        /// <exception cref="ArgumentException">A required project-start date bound is missing.</exception>
        public ProjectResourcesResponse SearchProjectResources(ProjectResourcesSearchRequest query)
        {
            return SearchProjectResourcesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchProjectResources"/>
        /// <returns>The typed envelope with status, headers and unchanged raw JSON.</returns>
        public ApiResponse<ProjectResourcesResponse> SearchProjectResourcesWithHttpInfo(ProjectResourcesSearchRequest query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(query.ProjectStartFrom) || string.IsNullOrWhiteSpace(query.ProjectStartTo))
                throw new ArgumentException("Both project-start date bounds are required.", nameof(query));
            var localVarPath = "/projectresources/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapProjectResponse(SearchJsonResponse.Parse("SearchProjectResources", response, ExceptionFactory), data => new ProjectResourcesResponse(data));
        }

        /// <inheritdoc cref="SearchProjectResources"/>
        public Task<ProjectResourcesResponse> SearchProjectResourcesAsync(ProjectResourcesSearchRequest query)
        {
            return SearchProjectResourcesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectResources"/>
        /// <param name="query">The required request containing both project-start date bounds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ProjectResourcesResponse> SearchProjectResourcesAsync(ProjectResourcesSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchProjectResourcesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectResourcesWithHttpInfo"/>
        public Task<ApiResponse<ProjectResourcesResponse>> SearchProjectResourcesAsyncWithHttpInfo(ProjectResourcesSearchRequest query)
        {
            return SearchProjectResourcesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectResourcesWithHttpInfo"/>
        /// <param name="query">The required request containing both project-start date bounds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<ProjectResourcesResponse>> SearchProjectResourcesAsyncWithHttpInfo(ProjectResourcesSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(query.ProjectStartFrom) || string.IsNullOrWhiteSpace(query.ProjectStartTo))
                throw new ArgumentException("Both project-start date bounds are required.", nameof(query));
            var localVarPath = "/projectresources/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapProjectResponse(SearchJsonResponse.Parse("SearchProjectResources", response, ExceptionFactory), data => new ProjectResourcesResponse(data));
        }

        /// <summary>Retrieves typed project revenues using the current resource and revenue endpoints.</summary>
        /// <param name="query">The required request containing both project-start date bounds and optional revenue type.</param>
        /// <returns>The typed JSON envelope, or null for an empty or JSON-null response.</returns>
        /// <remarks>Unknown fields and metadata remain accessible through the JSON indexer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON, has an invalid envelope, or contains a fractional value exceeding the decimal range.</exception>
        /// <exception cref="ArgumentNullException">The required query is null.</exception>
        /// <exception cref="ArgumentException">A required project-start date bound is missing.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The revenue type is outside the documented range.</exception>
        public ProjectRevenuesResponse SearchProjectRevenues(ProjectRevenuesSearchRequest query)
        {
            return SearchProjectRevenuesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchProjectRevenues"/>
        /// <returns>The typed envelope with status, headers and unchanged raw JSON.</returns>
        public ApiResponse<ProjectRevenuesResponse> SearchProjectRevenuesWithHttpInfo(ProjectRevenuesSearchRequest query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(query.ProjectStartFrom) || string.IsNullOrWhiteSpace(query.ProjectStartTo))
                throw new ArgumentException("Both project-start date bounds are required.", nameof(query));
            ValidateProjectRevenueType(query.RevenueType);
            var localVarPath = "/projectrevenue/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapProjectResponse(SearchJsonResponse.Parse("SearchProjectRevenues", response, ExceptionFactory), data => new ProjectRevenuesResponse(data));
        }

        /// <inheritdoc cref="SearchProjectRevenues"/>
        public Task<ProjectRevenuesResponse> SearchProjectRevenuesAsync(ProjectRevenuesSearchRequest query)
        {
            return SearchProjectRevenuesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectRevenues"/>
        /// <param name="query">The required request containing both project-start date bounds and optional revenue type.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ProjectRevenuesResponse> SearchProjectRevenuesAsync(ProjectRevenuesSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchProjectRevenuesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectRevenuesWithHttpInfo"/>
        public Task<ApiResponse<ProjectRevenuesResponse>> SearchProjectRevenuesAsyncWithHttpInfo(ProjectRevenuesSearchRequest query)
        {
            return SearchProjectRevenuesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectRevenuesWithHttpInfo"/>
        /// <param name="query">The required request containing both project-start date bounds and optional revenue type.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<ProjectRevenuesResponse>> SearchProjectRevenuesAsyncWithHttpInfo(ProjectRevenuesSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(query.ProjectStartFrom) || string.IsNullOrWhiteSpace(query.ProjectStartTo))
                throw new ArgumentException("Both project-start date bounds are required.", nameof(query));
            ValidateProjectRevenueType(query.RevenueType);
            var localVarPath = "/projectrevenue/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapProjectResponse(SearchJsonResponse.Parse("SearchProjectRevenues", response, ExceptionFactory), data => new ProjectRevenuesResponse(data));
        }

        private static void ValidateProjectRevenueType(ProjectRevenueType? revenueType)
        {
            if (revenueType.HasValue && !Enum.IsDefined(typeof(ProjectRevenueType), revenueType.Value))
                throw new ArgumentOutOfRangeException(nameof(revenueType), "The documented revenue types are planning, realization and deviation.");
        }

        private static ApiResponse<T> WrapProjectResponse<T>(ApiResponse<JToken> response, Func<JObject, T> create) where T : class
        {
            T data = null;
            if (response.Data != null && response.Data.Type != JTokenType.Null)
            {
                if (!(response.Data is JObject value))
                    throw new JsonSerializationException("The project response must be a JSON object.");
                data = create(value);
            }
            return new ApiResponse<T>(response.StatusCode, response.Headers, response.RawJsonContent, data);
        }
    }
}
