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
        /// <summary>Retrieves contact conversions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetContactConversionDataSource(string startDate, string endDate, string fields = null)
        {
            return GetContactConversionDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetContactConversionDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetContactConversionDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/conversion";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactConversionDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactConversionDataSource"/>
        public Task<JToken> GetContactConversionDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetContactConversionDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactConversionDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetContactConversionDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactConversionDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactConversionDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactConversionDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetContactConversionDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactConversionDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactConversionDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/conversion";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactConversionDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves event journal entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetEventJournalDataSource(string startDate, string endDate, string fields = null)
        {
            return GetEventJournalDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetEventJournalDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetEventJournalDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/event";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetEventJournalDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetEventJournalDataSource"/>
        public Task<JToken> GetEventJournalDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetEventJournalDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetEventJournalDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetEventJournalDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetEventJournalDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetEventJournalDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetEventJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetEventJournalDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetEventJournalDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetEventJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/event";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetEventJournalDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves expense entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetExpenseDataSource(string startDate, string endDate, string fields = null)
        {
            return GetExpenseDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetExpenseDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetExpenseDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/expense";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetExpenseDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetExpenseDataSource"/>
        public Task<JToken> GetExpenseDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetExpenseDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetExpenseDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetExpenseDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetExpenseDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetExpenseDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetExpenseDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetExpenseDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetExpenseDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetExpenseDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/expense";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetExpenseDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves human-resource reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="hideTimeUnrestricted">The optional filter for time-unrestricted entries; null retains the documented server default of true, while false or true is sent explicitly.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged. The time-unrestricted filter does not establish a resource's capabilities or rights.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetHumanResourceDataSource(string startDate, string endDate, bool? hideTimeUnrestricted = null, string fields = null)
        {
            return GetHumanResourceDataSourceWithHttpInfo(startDate, endDate, hideTimeUnrestricted, fields).Data;
        }

        /// <inheritdoc cref="GetHumanResourceDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetHumanResourceDataSourceWithHttpInfo(string startDate, string endDate, bool? hideTimeUnrestricted = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/humanResource";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (hideTimeUnrestricted.HasValue) queryParams.Add(new KeyValuePair<string, string>("hideTimeUnrestricted", hideTimeUnrestricted.Value ? "true" : "false"));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetHumanResourceDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetHumanResourceDataSource"/>
        public Task<JToken> GetHumanResourceDataSourceAsync(string startDate, string endDate, bool? hideTimeUnrestricted = null, string fields = null)
        {
            return GetHumanResourceDataSourceAsync(startDate, endDate, hideTimeUnrestricted, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="hideTimeUnrestricted">The optional filter for time-unrestricted entries; null retains the documented server default of true, while false or true is sent explicitly.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetHumanResourceDataSourceAsync(string startDate, string endDate, bool? hideTimeUnrestricted, string fields, CancellationToken cancellationToken)
        {
            return (await GetHumanResourceDataSourceAsyncWithHttpInfo(startDate, endDate, hideTimeUnrestricted, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetHumanResourceDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetHumanResourceDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? hideTimeUnrestricted = null, string fields = null)
        {
            return GetHumanResourceDataSourceAsyncWithHttpInfo(startDate, endDate, hideTimeUnrestricted, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="hideTimeUnrestricted">The optional filter for time-unrestricted entries; null retains the documented server default of true, while false or true is sent explicitly.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetHumanResourceDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? hideTimeUnrestricted, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/humanResource";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (hideTimeUnrestricted.HasValue) queryParams.Add(new KeyValuePair<string, string>("hideTimeUnrestricted", hideTimeUnrestricted.Value ? "true" : "false"));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetHumanResourceDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves human-resource availability for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetHumanResourceAvailabilityDataSource(string startDate, string endDate, string fields = null)
        {
            return GetHumanResourceAvailabilityDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetHumanResourceAvailabilityDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/humanResourceAvailability";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetHumanResourceAvailabilityDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityDataSource"/>
        public Task<JToken> GetHumanResourceAvailabilityDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetHumanResourceAvailabilityDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetHumanResourceAvailabilityDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetHumanResourceAvailabilityDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetHumanResourceAvailabilityDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetHumanResourceAvailabilityDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetHumanResourceAvailabilityDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/humanResourceAvailability";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetHumanResourceAvailabilityDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves opportunities for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOpportunityDataSource(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetOpportunityDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOpportunityDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOpportunityDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOpportunityDataSource"/>
        public Task<JToken> GetOpportunityDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOpportunityDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetOpportunityDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOpportunityDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOpportunityDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOpportunityDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOpportunityDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves personal journal entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetPersonalJournalDataSource(string startDate, string endDate, string fields = null)
        {
            return GetPersonalJournalDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetPersonalJournalDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetPersonalJournalDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalJournal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetPersonalJournalDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetPersonalJournalDataSource"/>
        public Task<JToken> GetPersonalJournalDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetPersonalJournalDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPersonalJournalDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetPersonalJournalDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetPersonalJournalDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetPersonalJournalDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetPersonalJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetPersonalJournalDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPersonalJournalDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetPersonalJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalJournal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetPersonalJournalDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves project reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetProjectDataSource(string startDate, string endDate, string fields = null)
        {
            return GetProjectDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetProjectDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetProjectDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/project";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProjectDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProjectDataSource"/>
        public Task<JToken> GetProjectDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetProjectDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetProjectDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetProjectDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProjectDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetProjectDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProjectDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/project";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProjectDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves sales-project reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetSalesProjectDataSource(string startDate, string endDate, string fields = null)
        {
            return GetSalesProjectDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetSalesProjectDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetSalesProjectDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/salesProject";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetSalesProjectDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetSalesProjectDataSource"/>
        public Task<JToken> GetSalesProjectDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetSalesProjectDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSalesProjectDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetSalesProjectDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetSalesProjectDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetSalesProjectDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetSalesProjectDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetSalesProjectDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSalesProjectDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetSalesProjectDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/salesProject";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetSalesProjectDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves time-entry runs for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTimeEntryRunDataSource(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetTimeEntryRunDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTimeEntryRunDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntryRun";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTimeEntryRunDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTimeEntryRunDataSource"/>
        public Task<JToken> GetTimeEntryRunDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryRunDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTimeEntryRunDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetTimeEntryRunDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTimeEntryRunDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTimeEntryRunDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryRunDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTimeEntryRunDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntryRun";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTimeEntryRunDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves time-entry run entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTimeEntryRunEntryDataSource(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunEntryDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetTimeEntryRunEntryDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTimeEntryRunEntryDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntryRunEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTimeEntryRunEntryDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTimeEntryRunEntryDataSource"/>
        public Task<JToken> GetTimeEntryRunEntryDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunEntryDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryRunEntryDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTimeEntryRunEntryDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetTimeEntryRunEntryDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTimeEntryRunEntryDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTimeEntryRunEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryRunEntryDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryRunEntryDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTimeEntryRunEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntryRunEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTimeEntryRunEntryDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves travel-expense positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. Required profiles, visibility and range semantics are enforced by the server; no local date/paging/completeness default is imposed. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTravelEntryPositionsDataSource(string startDate, string endDate, string fields = null)
        {
            return GetTravelEntryPositionsDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetTravelEntryPositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTravelEntryPositionsDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/travelEntryPositions";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTravelEntryPositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTravelEntryPositionsDataSource"/>
        public Task<JToken> GetTravelEntryPositionsDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetTravelEntryPositionsDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTravelEntryPositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTravelEntryPositionsDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetTravelEntryPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTravelEntryPositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTravelEntryPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetTravelEntryPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTravelEntryPositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTravelEntryPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/travelEntryPositions";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTravelEntryPositionsDataSource", response, ExceptionFactory);
        }

    }
}
