using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Creates a project resource and retains creation warnings and hints.</summary>
        /// <param name="request">The required creation request. Recreation is performed only when explicitly requested.</param>
        /// <returns>The result payload, or null for an empty response.</returns>
        /// <remarks>The SDK adds no mutation retries. Cancellation after sending does not establish the server outcome. A 201 response can contain non-critical errors and hints; the caller decides how to handle them.</remarks>
        /// <exception cref="ArgumentException">A required identifier or known request field is invalid.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or its numeric values exceed the parser range.</exception>
        /// <exception cref="ArgumentNullException">The required request body is null.</exception>
        public ProjectResourceCreationResponse CreateProjectResource(ProjectResourceCreationRequest request)
        {
            return CreateProjectResourceWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateProjectResource"/>
        /// <returns>The status, headers, unchanged raw response and result data; a 204 empty response has null data.</returns>
        public ApiResponse<ProjectResourceCreationResponse> CreateProjectResourceWithHttpInfo(ProjectResourceCreationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            ValidateProjectResourceCreation(request);
            var localVarPath = "/projectresource/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), request.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return WrapProjectResponse(SearchJsonResponse.Parse("CreateProjectResource", response, ExceptionFactory), data => new ProjectResourceCreationResponse(data));
        }

        /// <inheritdoc cref="CreateProjectResource"/>
        public Task<ProjectResourceCreationResponse> CreateProjectResourceAsync(ProjectResourceCreationRequest request)
        {
            return CreateProjectResourceAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectResource"/>
        /// <param name="request">The required creation request. Recreation is performed only when explicitly requested.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<ProjectResourceCreationResponse> CreateProjectResourceAsync(ProjectResourceCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProjectResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProjectResourceWithHttpInfo"/>
        public Task<ApiResponse<ProjectResourceCreationResponse>> CreateProjectResourceAsyncWithHttpInfo(ProjectResourceCreationRequest request)
        {
            return CreateProjectResourceAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProjectResourceWithHttpInfo"/>
        /// <param name="request">The required creation request. Recreation is performed only when explicitly requested.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<ProjectResourceCreationResponse>> CreateProjectResourceAsyncWithHttpInfo(ProjectResourceCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            ValidateProjectResourceCreation(request);
            var localVarPath = "/projectresource/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), request.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapProjectResponse(SearchJsonResponse.Parse("CreateProjectResource", response, ExceptionFactory), data => new ProjectResourceCreationResponse(data));
        }

        private static void ValidateProjectResourceCreation(ProjectResourceCreationRequest request)
        {
            ValidateProjectResourceInteger(request["projectId"], "projectId", true);
            foreach (var name in new[] { "humanResourceId", "productId" })
                ValidateProjectResourceInteger(request[name], name, false);
            foreach (var name in new[] { "projectRole", "projectPosition", "allocationStatus" })
            {
                var token = request[name];
                if (token != null && token.Type != JTokenType.Null && token.Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be a JSON string.", nameof(request));
            }
            var recreate = request["recreate"];
            if (recreate != null && recreate.Type != JTokenType.Null && recreate.Type != JTokenType.Boolean)
                throw new ArgumentException("The recreate field must be a JSON boolean.", nameof(request));

            var fee = request["standardFee"];
            if (fee != null && fee.Type != JTokenType.Null)
            {
                var number = fee.ToString(Newtonsoft.Json.Formatting.None);
                var mantissa = number.Split('e', 'E')[0];
                if ((fee.Type != JTokenType.Integer && fee.Type != JTokenType.Float) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(number, @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$") ||
                    (number.StartsWith("-", StringComparison.Ordinal) && mantissa.Any(character => character >= '1' && character <= '9')))
                    throw new ArgumentException("The standard fee must be a non-negative finite JSON number.", nameof(request));
            }

            var allocations = request["monthlyAllocations"];
            if (allocations != null && allocations.Type != JTokenType.Null)
            {
                if (!(allocations is JArray array))
                    throw new ArgumentException("The monthly allocations must be a JSON array.", nameof(request));
                foreach (var allocation in array)
                {
                    ValidateProjectResourceInteger(allocation, "monthlyAllocations item", true);
                    if (allocation.ToString(Newtonsoft.Json.Formatting.None).StartsWith("-", StringComparison.Ordinal))
                        throw new ArgumentException("Monthly allocations must be non-negative.", nameof(request));
                }
            }
        }

        private static void ValidateProjectResourceInteger(JToken token, string name, bool required)
        {
            if (!required && (token == null || token.Type == JTokenType.Null)) return;
            long value;
            if (token == null || token.Type != JTokenType.Integer ||
                !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture, out value))
                throw new ArgumentException("The " + name + " field must be an Int64 JSON integer.", "request");
        }
    }
}
