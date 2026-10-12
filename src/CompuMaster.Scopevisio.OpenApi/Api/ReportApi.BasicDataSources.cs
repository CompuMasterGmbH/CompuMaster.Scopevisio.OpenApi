using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Retrieves the product reporting data source.</summary>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetProductDataSource(string fields = null)
        {
            return GetProductDataSourceWithHttpInfo(fields).Data;
        }

        /// <inheritdoc cref="GetProductDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetProductDataSourceWithHttpInfo(string fields = null)
        {
            var localVarPath = "/datasource/product";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProductDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProductDataSource"/>
        public Task<JToken> GetProductDataSourceAsync(string fields = null)
        {
            return GetProductDataSourceAsync(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductDataSource"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetProductDataSourceAsync(string fields, CancellationToken cancellationToken)
        {
            return (await GetProductDataSourceAsyncWithHttpInfo(fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProductDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProductDataSourceAsyncWithHttpInfo(string fields = null)
        {
            return GetProductDataSourceAsyncWithHttpInfo(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductDataSourceWithHttpInfo"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProductDataSourceAsyncWithHttpInfo(string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/datasource/product";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProductDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves Teamwork document metadata as a reporting data source.</summary>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTeamworkDocumentMetadataDataSource(string fields = null)
        {
            return GetTeamworkDocumentMetadataDataSourceWithHttpInfo(fields).Data;
        }

        /// <inheritdoc cref="GetTeamworkDocumentMetadataDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTeamworkDocumentMetadataDataSourceWithHttpInfo(string fields = null)
        {
            var localVarPath = "/datasource/teamwork";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTeamworkDocumentMetadataDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTeamworkDocumentMetadataDataSource"/>
        public Task<JToken> GetTeamworkDocumentMetadataDataSourceAsync(string fields = null)
        {
            return GetTeamworkDocumentMetadataDataSourceAsync(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTeamworkDocumentMetadataDataSource"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTeamworkDocumentMetadataDataSourceAsync(string fields, CancellationToken cancellationToken)
        {
            return (await GetTeamworkDocumentMetadataDataSourceAsyncWithHttpInfo(fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTeamworkDocumentMetadataDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTeamworkDocumentMetadataDataSourceAsyncWithHttpInfo(string fields = null)
        {
            return GetTeamworkDocumentMetadataDataSourceAsyncWithHttpInfo(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTeamworkDocumentMetadataDataSourceWithHttpInfo"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTeamworkDocumentMetadataDataSourceAsyncWithHttpInfo(string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/datasource/teamwork";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTeamworkDocumentMetadataDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the contact reporting data source for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="roles">The optional comma-separated role filter, sent unchanged. Known values are interested, lead, customer, vendor, debitor, kreditor, employee, partner and worker; the server validates membership and visibility.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetContactDataSource(string startDate, string endDate, string roles = null, string fields = null)
        {
            return GetContactDataSourceWithHttpInfo(startDate, endDate, roles, fields).Data;
        }

        /// <inheritdoc cref="GetContactDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetContactDataSourceWithHttpInfo(string startDate, string endDate, string roles = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/contact";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (roles != null) queryParams.Add(new KeyValuePair<string, string>("roles", roles));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactDataSource"/>
        public Task<JToken> GetContactDataSourceAsync(string startDate, string endDate, string roles = null, string fields = null)
        {
            return GetContactDataSourceAsync(startDate, endDate, roles, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="roles">The optional comma-separated role filter, sent unchanged. Known values are interested, lead, customer, vendor, debitor, kreditor, employee, partner and worker; the server validates membership and visibility.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetContactDataSourceAsync(string startDate, string endDate, string roles, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactDataSourceAsyncWithHttpInfo(startDate, endDate, roles, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactDataSourceAsyncWithHttpInfo(string startDate, string endDate, string roles = null, string fields = null)
        {
            return GetContactDataSourceAsyncWithHttpInfo(startDate, endDate, roles, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="roles">The optional comma-separated role filter, sent unchanged. Known values are interested, lead, customer, vendor, debitor, kreditor, employee, partner and worker; the server validates membership and visibility.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactDataSourceAsyncWithHttpInfo(string startDate, string endDate, string roles, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/contact";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (roles != null) queryParams.Add(new KeyValuePair<string, string>("roles", roles));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the blog-entry reporting data source for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetBlogDataSource(string startDate, string endDate, string fields = null)
        {
            return GetBlogDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetBlogDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetBlogDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/blog";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetBlogDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetBlogDataSource"/>
        public Task<JToken> GetBlogDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetBlogDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBlogDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetBlogDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetBlogDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetBlogDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetBlogDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetBlogDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBlogDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetBlogDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/blog";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetBlogDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the task reporting data source for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTaskDataSource(string startDate, string endDate, string fields = null)
        {
            return GetTaskDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetTaskDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTaskDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/task";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTaskDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTaskDataSource"/>
        public Task<JToken> GetTaskDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetTaskDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTaskDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTaskDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetTaskDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTaskDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTaskDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetTaskDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTaskDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTaskDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/task";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTaskDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the time-entry reporting data source for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTimeEntryDataSource(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetTimeEntryDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTimeEntryDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTimeEntryDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTimeEntryDataSource"/>
        public Task<JToken> GetTimeEntryDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTimeEntryDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetTimeEntryDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTimeEntryDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTimeEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetTimeEntryDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTimeEntryDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTimeEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/timeEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTimeEntryDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves the audit-log reporting data source for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces required profiles and visibility. This method requests one data source without inferring completeness, role rights or date-range semantics. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetAuditLogDataSource(string startDate, string endDate, string fields = null)
        {
            return GetAuditLogDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetAuditLogDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetAuditLogDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/auditLog";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetAuditLogDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetAuditLogDataSource"/>
        public Task<JToken> GetAuditLogDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetAuditLogDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetAuditLogDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetAuditLogDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetAuditLogDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetAuditLogDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetAuditLogDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetAuditLogDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetAuditLogDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetAuditLogDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/auditLog";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetAuditLogDataSource", response, ExceptionFactory);
        }

        private static void ValidateBasicDataSourceDate(string value, string parameterName)
        {
            if (value == null || !DateTime.TryParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
                throw new ArgumentException("The required date must be valid in dd.MM.yyyy format.", parameterName);
        }
    }
}
