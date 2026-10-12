using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class HumanResourcesApi
    {
        /// <summary>Imports explicitly supplied project-resource monthly planning records.</summary>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O; the schema does not require individual fields or a minimum record count.</param>
        /// <returns>The complete JSON import result, or null for an empty body. HTTP 200 can contain individual record failures and does not establish complete import success.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), CSV/JSON validity, data precedence and project/catalog existence. Original month/date strings and numeric values remain unchanged; month normalization and resource fee truncation are server behavior. No import retry or partial-failure replay is added. Cancellation after sending does not establish server outcome. Parsed result fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request object is null.</exception>
        /// <exception cref="ArgumentException">A known request field or record has an incompatible raw JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ImportProjectResources(ProjectResourceImportRequest request)
        {
            return ImportProjectResourcesWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportProjectResources"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete import result.</returns>
        public ApiResponse<JToken> ImportProjectResourcesWithHttpInfo(ProjectResourceImportRequest request)
        {
            var body = SnapshotPlanningImport(request, "resources");
            var localVarPath = "/projectresource/import";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportProjectResources", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportProjectResources"/>
        public Task<JToken> ImportProjectResourcesAsync(ProjectResourceImportRequest request)
        {
            return ImportProjectResourcesAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportProjectResources"/>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all records.</exception>
        public async Task<JToken> ImportProjectResourcesAsync(ProjectResourceImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportProjectResourcesAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportProjectResourcesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ImportProjectResourcesAsyncWithHttpInfo(ProjectResourceImportRequest request)
        {
            return ImportProjectResourcesAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportProjectResourcesWithHttpInfo"/>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all records.</exception>
        public async Task<ApiResponse<JToken>> ImportProjectResourcesAsyncWithHttpInfo(ProjectResourceImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPlanningImport(request, "resources");
            var localVarPath = "/projectresource/import";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportProjectResources", response, ExceptionFactory);
        }

        /// <summary>Imports explicitly supplied project-revenue monthly planning records.</summary>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O; the schema does not require individual fields or a minimum record count.</param>
        /// <returns>The complete JSON import result, or null for an empty body. HTTP 200 can contain individual record failures and does not establish complete import success.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), CSV/JSON validity, data precedence and project/catalog existence. Original month/date strings and numeric values remain unchanged; month normalization and resource fee truncation are server behavior. No import retry or partial-failure replay is added. Cancellation after sending does not establish server outcome. Parsed result fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request object is null.</exception>
        /// <exception cref="ArgumentException">A known request field or record has an incompatible raw JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ImportProjectRevenues(ProjectRevenueImportRequest request)
        {
            return ImportProjectRevenuesWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportProjectRevenues"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete import result.</returns>
        public ApiResponse<JToken> ImportProjectRevenuesWithHttpInfo(ProjectRevenueImportRequest request)
        {
            var body = SnapshotPlanningImport(request, "revenues");
            var localVarPath = "/projectrevenue/import";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportProjectRevenues", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportProjectRevenues"/>
        public Task<JToken> ImportProjectRevenuesAsync(ProjectRevenueImportRequest request)
        {
            return ImportProjectRevenuesAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportProjectRevenues"/>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all records.</exception>
        public async Task<JToken> ImportProjectRevenuesAsync(ProjectRevenueImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportProjectRevenuesAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportProjectRevenuesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ImportProjectRevenuesAsyncWithHttpInfo(ProjectRevenueImportRequest request)
        {
            return ImportProjectRevenuesAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportProjectRevenuesWithHttpInfo"/>
        /// <param name="request">The required complete import object, copied and serialized before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all records.</exception>
        public async Task<ApiResponse<JToken>> ImportProjectRevenuesAsyncWithHttpInfo(ProjectRevenueImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPlanningImport(request, "revenues");
            var localVarPath = "/projectrevenue/import";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportProjectRevenues", response, ExceptionFactory);
        }

        private static string SnapshotPlanningImport(JObject request, string arrayKey)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            ValidatePlanningImportKind(snapshot["data"], "data", JTokenType.String);
            var records = snapshot[arrayKey];
            if (records != null && records.Type != JTokenType.Null)
            {
                if (!(records is JArray array)) throw new ArgumentException("The " + arrayKey + " field must be a JSON array or null.", nameof(request));
                foreach (var record in array)
                {
                    if (!(record is JObject item)) throw new ArgumentException("Import records must be JSON objects.", nameof(request));
                    foreach (var name in arrayKey == "resources" ? new[] { "projectNumber", "date", "comment" } : new[] { "revenueGroup", "projectNumber", "date", "comment" })
                        ValidatePlanningImportKind(item[name], name, JTokenType.String);
                    if (arrayKey == "resources")
                    {
                        ValidatePlanningImportInteger(item["humanResourceId"], "humanResourceId", false);
                        ValidatePlanningImportInteger(item["days"], "days", true);
                        ValidatePlanningImportInteger(item["extraDays"], "extraDays", true);
                    }
                    var amountKey = arrayKey == "resources" ? "fee" : "amount";
                    var amount = item[amountKey];
                    if (amount != null && amount.Type != JTokenType.Null &&
                        ((amount.Type != JTokenType.Integer && amount.Type != JTokenType.Float) ||
                        !System.Text.RegularExpressions.Regex.IsMatch(amount.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                        throw new ArgumentException("The " + amountKey + " field must be a finite JSON number or null.", nameof(request));
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidatePlanningImportKind(JToken token, string name, JTokenType kind)
        {
            if (token != null && token.Type != JTokenType.Null && token.Type != kind)
                throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidatePlanningImportInteger(JToken token, string name, bool int32)
        {
            if (token == null || token.Type == JTokenType.Null) return;
            long value;
            if (token.Type != JTokenType.Integer || !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ||
                (int32 && (value < int.MinValue || value > int.MaxValue)))
                throw new ArgumentException("The " + name + " field must be a JSON integer within its specified " + (int32 ? "Int32" : "Int64") + " width.", "request");
        }
    }
}
