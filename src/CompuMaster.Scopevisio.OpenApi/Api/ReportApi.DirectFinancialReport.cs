using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ReportApi
    {
        /// <summary>Retrieves the financial report for the selected period and layouts.</summary>
        /// <param name="month">The required monthly-period string, retained without integer conversion; the server validates the documented 1 through 12 range.</param>
        /// <param name="year">The required yearly-period string, retained including leading zeros; the server validates supported years.</param>
        /// <param name="definitionType">The required account-layout type string, retained without integer conversion; the server validates the documented codes 1 through 5.</param>
        /// <param name="name">The required nonblank account-layout name, sent as an escaped path segment without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, names, visibility, filters, layouts and range/period semantics. The client does not infer access, completeness or consolidated totals. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required selection is missing/blank, or a path selection is a relative segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetFinancialReportByLayout(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportByLayoutWithHttpInfo(definitionType, name, year, month, layoutName, options).Data;
        }

        /// <inheritdoc cref="GetFinancialReportByLayout"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetFinancialReportByLayoutWithHttpInfo(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            ValidateFinancialReportPathSelection(definitionType, nameof(definitionType));
            ValidateFinancialReportPathSelection(name, nameof(name));
            ValidateFinancialReportPathSelection(year, nameof(year));
            ValidateFinancialReportPathSelection(month, nameof(month));
            if (string.IsNullOrWhiteSpace(layoutName)) throw new ArgumentException("A nonblank column-layout name is required.", nameof(layoutName));
            var localVarPath = "/proreport/{type}/{name}/{year}/{month}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(definitionType)).Replace("{name}", Uri.EscapeDataString(name))
                .Replace("{year}", Uri.EscapeDataString(year)).Replace("{month}", Uri.EscapeDataString(month));
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("layoutName", layoutName));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetFinancialReportByLayout", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetFinancialReportByLayout"/>
        public Task<JToken> GetFinancialReportByLayoutAsync(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportByLayoutAsync(definitionType, name, year, month, layoutName, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFinancialReportByLayout"/>
        /// <param name="month">The required monthly-period string, retained without integer conversion; the server validates the documented 1 through 12 range.</param>
        /// <param name="year">The required yearly-period string, retained including leading zeros; the server validates supported years.</param>
        /// <param name="definitionType">The required account-layout type string, retained without integer conversion; the server validates the documented codes 1 through 5.</param>
        /// <param name="name">The required nonblank account-layout name, sent as an escaped path segment without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetFinancialReportByLayoutAsync(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options, CancellationToken cancellationToken)
        {
            return (await GetFinancialReportByLayoutAsyncWithHttpInfo(definitionType, name, year, month, layoutName, options, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetFinancialReportByLayoutWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetFinancialReportByLayoutAsyncWithHttpInfo(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportByLayoutAsyncWithHttpInfo(definitionType, name, year, month, layoutName, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFinancialReportByLayoutWithHttpInfo"/>
        /// <param name="month">The required monthly-period string, retained without integer conversion; the server validates the documented 1 through 12 range.</param>
        /// <param name="year">The required yearly-period string, retained including leading zeros; the server validates supported years.</param>
        /// <param name="definitionType">The required account-layout type string, retained without integer conversion; the server validates the documented codes 1 through 5.</param>
        /// <param name="name">The required nonblank account-layout name, sent as an escaped path segment without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetFinancialReportByLayoutAsyncWithHttpInfo(string definitionType, string name, string year, string month, string layoutName, FinancialReportDataSourceOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateFinancialReportPathSelection(definitionType, nameof(definitionType));
            ValidateFinancialReportPathSelection(name, nameof(name));
            ValidateFinancialReportPathSelection(year, nameof(year));
            ValidateFinancialReportPathSelection(month, nameof(month));
            if (string.IsNullOrWhiteSpace(layoutName)) throw new ArgumentException("A nonblank column-layout name is required.", nameof(layoutName));
            var localVarPath = "/proreport/{type}/{name}/{year}/{month}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(definitionType)).Replace("{name}", Uri.EscapeDataString(name))
                .Replace("{year}", Uri.EscapeDataString(year)).Replace("{month}", Uri.EscapeDataString(month));
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("layoutName", layoutName));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetFinancialReportByLayout", response, ExceptionFactory);
        }

        private static void ValidateFinancialReportPathSelection(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == "..")
                throw new ArgumentException("A nonblank path selection without relative segments is required.", parameterName);
        }
    }
}
