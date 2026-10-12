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
        /// <summary>Retrieves general journal reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, names, visibility, filters, layouts and range/period semantics. The client does not infer access, completeness or consolidated totals. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetGeneralJournalDataSource(string startDate, string endDate, JournalDataSourceOptions options = null)
        {
            return GetGeneralJournalDataSourceWithHttpInfo(startDate, endDate, options).Data;
        }

        /// <inheritdoc cref="GetGeneralJournalDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetGeneralJournalDataSourceWithHttpInfo(string startDate, string endDate, JournalDataSourceOptions options = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/journal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetGeneralJournalDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetGeneralJournalDataSource"/>
        public Task<JToken> GetGeneralJournalDataSourceAsync(string startDate, string endDate, JournalDataSourceOptions options = null)
        {
            return GetGeneralJournalDataSourceAsync(startDate, endDate, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetGeneralJournalDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetGeneralJournalDataSourceAsync(string startDate, string endDate, JournalDataSourceOptions options, CancellationToken cancellationToken)
        {
            return (await GetGeneralJournalDataSourceAsyncWithHttpInfo(startDate, endDate, options, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetGeneralJournalDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetGeneralJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, JournalDataSourceOptions options = null)
        {
            return GetGeneralJournalDataSourceAsyncWithHttpInfo(startDate, endDate, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetGeneralJournalDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetGeneralJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, JournalDataSourceOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/journal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetGeneralJournalDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the financial report for the selected period and layouts.</summary>
        /// <param name="month">The required monthly period from 1 through 12.</param>
        /// <param name="year">The required yearly period, sent unchanged; the server validates supported years.</param>
        /// <param name="definitionType">The required Int64 account-layout type, sent unchanged for server validation. Documented choices are 1 (profit and loss), 2 (balance sheet), 3 (other evaluations), 4 (cost center) and 5 (cost bearer).</param>
        /// <param name="definitionName">The required nonblank account-layout name, sent unchanged without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, names, visibility, filters, layouts and range/period semantics. The client does not infer access, completeness or consolidated totals. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required layout name is null, empty or blank.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The month is outside 1 through 12.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetFinancialReportDataSource(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportDataSourceWithHttpInfo(month, year, definitionType, definitionName, layoutName, options).Data;
        }

        /// <inheritdoc cref="GetFinancialReportDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetFinancialReportDataSourceWithHttpInfo(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException(nameof(month), "The month must be between 1 and 12.");
            if (string.IsNullOrWhiteSpace(definitionName)) throw new ArgumentException("A nonblank account-layout name is required.", nameof(definitionName));
            if (string.IsNullOrWhiteSpace(layoutName)) throw new ArgumentException("A nonblank column-layout name is required.", nameof(layoutName));
            var localVarPath = "/datasource/proReport";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("month", month.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("year", year.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("definitionType", definitionType.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("definitionName", definitionName));
            queryParams.Add(new KeyValuePair<string, string>("layoutName", layoutName));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetFinancialReportDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetFinancialReportDataSource"/>
        public Task<JToken> GetFinancialReportDataSourceAsync(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportDataSourceAsync(month, year, definitionType, definitionName, layoutName, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFinancialReportDataSource"/>
        /// <param name="month">The required monthly period from 1 through 12.</param>
        /// <param name="year">The required yearly period, sent unchanged; the server validates supported years.</param>
        /// <param name="definitionType">The required Int64 account-layout type, sent unchanged for server validation. Documented choices are 1 (profit and loss), 2 (balance sheet), 3 (other evaluations), 4 (cost center) and 5 (cost bearer).</param>
        /// <param name="definitionName">The required nonblank account-layout name, sent unchanged without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetFinancialReportDataSourceAsync(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options, CancellationToken cancellationToken)
        {
            return (await GetFinancialReportDataSourceAsyncWithHttpInfo(month, year, definitionType, definitionName, layoutName, options, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetFinancialReportDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetFinancialReportDataSourceAsyncWithHttpInfo(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options = null)
        {
            return GetFinancialReportDataSourceAsyncWithHttpInfo(month, year, definitionType, definitionName, layoutName, options, CancellationToken.None);
        }

        /// <inheritdoc cref="GetFinancialReportDataSourceWithHttpInfo"/>
        /// <param name="month">The required monthly period from 1 through 12.</param>
        /// <param name="year">The required yearly period, sent unchanged; the server validates supported years.</param>
        /// <param name="definitionType">The required Int64 account-layout type, sent unchanged for server validation. Documented choices are 1 (profit and loss), 2 (balance sheet), 3 (other evaluations), 4 (cost center) and 5 (cost bearer).</param>
        /// <param name="definitionName">The required nonblank account-layout name, sent unchanged without trimming.</param>
        /// <param name="layoutName">The required nonblank column-layout name, sent unchanged without trimming.</param>
        /// <param name="options">The optional query choices; null omits every optional parameter and retains server defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetFinancialReportDataSourceAsyncWithHttpInfo(int month, int year, long definitionType, string definitionName, string layoutName, FinancialReportDataSourceOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException(nameof(month), "The month must be between 1 and 12.");
            if (string.IsNullOrWhiteSpace(definitionName)) throw new ArgumentException("A nonblank account-layout name is required.", nameof(definitionName));
            if (string.IsNullOrWhiteSpace(layoutName)) throw new ArgumentException("A nonblank column-layout name is required.", nameof(layoutName));
            var localVarPath = "/datasource/proReport";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("month", month.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("year", year.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("definitionType", definitionType.ToString(CultureInfo.InvariantCulture)));
            queryParams.Add(new KeyValuePair<string, string>("definitionName", definitionName));
            queryParams.Add(new KeyValuePair<string, string>("layoutName", layoutName));
            if (options != null) queryParams.AddRange(options.ToQueryParameters());
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetFinancialReportDataSource", response, ExceptionFactory);
        }

    }
}
