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
        /// <summary>Retrieves the complete JSON result for an explicitly selected deprecated project resources range.</summary>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The deprecated route requires Projekte (Anzeigen). Required projectStartFrom and projectStartTo remain original Int64 Unix epoch milliseconds; no date conversion or range ordering rule is inferred. Examples do not insert client date defaults. Optional searchSpecification permits an object or JSON null; the server excludes month, monthRevenue and id from search, without client filter rewriting. HTTP 200 declares no response schema. The modern SearchProjectResources v2 workflow is separate and uses caller-selected yyyy-MM-dd date strings. No automatic paging or retry occurs. Parsed fractions use bounded decimal precision; raw JSON remains unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required timestamp has an invalid kind or width, the search value is neither an object nor null, or a number is nonfinite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public JToken SearchProjectResourcesLegacy(ProjectResourcesLegacySearchRequest request)
        {
            return SearchProjectResourcesLegacyWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="SearchProjectResourcesLegacy"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public ApiResponse<JToken> SearchProjectResourcesLegacyWithHttpInfo(ProjectResourcesLegacySearchRequest request)
        {
            var localVarPath = "/projectresources";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyProjectSearch(request, false),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProjectResourcesLegacy", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProjectResourcesLegacy"/>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public Task<JToken> SearchProjectResourcesLegacyAsync(ProjectResourcesLegacySearchRequest request)
        {
            return SearchProjectResourcesLegacyAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectResourcesLegacy"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public async Task<JToken> SearchProjectResourcesLegacyAsync(ProjectResourcesLegacySearchRequest request, CancellationToken cancellationToken)
        {
            return (await SearchProjectResourcesLegacyAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectResourcesLegacyWithHttpInfo"/>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public Task<ApiResponse<JToken>> SearchProjectResourcesLegacyAsyncWithHttpInfo(ProjectResourcesLegacySearchRequest request)
        {
            return SearchProjectResourcesLegacyAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectResourcesLegacyWithHttpInfo"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use SearchProjectResources for the modern v2 workflow with explicitly selected date strings.")]
        public async Task<ApiResponse<JToken>> SearchProjectResourcesLegacyAsyncWithHttpInfo(ProjectResourcesLegacySearchRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/projectresources";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyProjectSearch(request, false),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProjectResourcesLegacy", response, ExceptionFactory);
        }


        /// <summary>Retrieves the complete JSON result for an explicitly selected deprecated project revenues range.</summary>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The deprecated route requires Projekte (Anzeigen). Required projectStartFrom and projectStartTo remain original Int64 Unix epoch milliseconds; no date conversion or range ordering rule is inferred. Examples do not insert client date defaults. Optional searchSpecification permits an object or JSON null; the server excludes month, monthRevenue and id from search, without client filter rewriting. Optional revenueType allows only the declared 0..2 integer choices; omission preserves the server planning default without client insertion. HTTP 200 declares no response schema. The modern SearchProjectRevenues v2 workflow is separate and uses caller-selected yyyy-MM-dd date strings. No automatic paging or retry occurs. Parsed fractions use bounded decimal precision; raw JSON remains unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required timestamp has an invalid kind or width, the search value is neither an object nor null, an explicit revenue type is outside the declared integer choices, or a number is nonfinite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public JToken SearchProjectRevenuesLegacy(ProjectRevenuesLegacySearchRequest request)
        {
            return SearchProjectRevenuesLegacyWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="SearchProjectRevenuesLegacy"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public ApiResponse<JToken> SearchProjectRevenuesLegacyWithHttpInfo(ProjectRevenuesLegacySearchRequest request)
        {
            var localVarPath = "/projectrevenue";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyProjectSearch(request, true),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchProjectRevenuesLegacy", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchProjectRevenuesLegacy"/>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public Task<JToken> SearchProjectRevenuesLegacyAsync(ProjectRevenuesLegacySearchRequest request)
        {
            return SearchProjectRevenuesLegacyAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectRevenuesLegacy"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public async Task<JToken> SearchProjectRevenuesLegacyAsync(ProjectRevenuesLegacySearchRequest request, CancellationToken cancellationToken)
        {
            return (await SearchProjectRevenuesLegacyAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchProjectRevenuesLegacyWithHttpInfo"/>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public Task<ApiResponse<JToken>> SearchProjectRevenuesLegacyAsyncWithHttpInfo(ProjectRevenuesLegacySearchRequest request)
        {
            return SearchProjectRevenuesLegacyAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchProjectRevenuesLegacyWithHttpInfo"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use SearchProjectRevenues for the modern v2 workflow with explicitly selected date strings.")]
        public async Task<ApiResponse<JToken>> SearchProjectRevenuesLegacyAsyncWithHttpInfo(ProjectRevenuesLegacySearchRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/projectrevenue";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyProjectSearch(request, true),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchProjectRevenuesLegacy", response, ExceptionFactory);
        }

        private static string SnapshotLegacyProjectSearch(JObject request, bool revenue)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            foreach (var name in new[] { "projectStartFrom", "projectStartTo" })
            {
                var token = snapshot[name];
                if (token == null || token.Type != JTokenType.Integer ||
                    !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                        System.Globalization.CultureInfo.InvariantCulture, out _))
                    throw new ArgumentException("The required " + name + " field must be an Int64 JSON integer.", nameof(request));
            }
            var search = snapshot["searchSpecification"];
            if (search != null && search.Type != JTokenType.Null && !(search is JObject))
                throw new ArgumentException("The optional search specification must be a JSON object or null.", nameof(request));
            if (revenue && snapshot.Property("revenueType") != null)
            {
                var type = snapshot["revenueType"];
                if (type.Type != JTokenType.Integer ||
                    !int.TryParse(type.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                        System.Globalization.CultureInfo.InvariantCulture, out var parsed) || parsed < 0 || parsed > 2)
                    throw new ArgumentException("An explicit revenue type must be an Int32 integer from zero through two.", nameof(request));
            }
            foreach (var value in snapshot.DescendantsAndSelf().OfType<JValue>())
            {
                if ((value.Value is double d && (double.IsNaN(d) || double.IsInfinity(d))) ||
                    (value.Value is float f && (float.IsNaN(f) || float.IsInfinity(f))))
                    throw new ArgumentException("JSON numbers must be finite.", nameof(request));
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
