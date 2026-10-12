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
        /// <summary>Retrieves contact-property reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetContactPropertiesDataSource(string startDate, string endDate, string fields = null)
        {
            return GetContactPropertiesDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetContactPropertiesDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetContactPropertiesDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/contactProperties";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactPropertiesDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactPropertiesDataSource"/>
        public Task<JToken> GetContactPropertiesDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetContactPropertiesDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertiesDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetContactPropertiesDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactPropertiesDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactPropertiesDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactPropertiesDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetContactPropertiesDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertiesDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactPropertiesDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/contactProperties";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactPropertiesDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves dispatch reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetDispatchDataSource(string startDate, string endDate, string fields = null)
        {
            return GetDispatchDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetDispatchDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetDispatchDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDispatchDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDispatchDataSource"/>
        public Task<JToken> GetDispatchDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetDispatchDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetDispatchDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetDispatchDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDispatchDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDispatchDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetDispatchDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDispatchDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDispatchDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves dispatch-position reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetDispatchPositionsDataSource(string startDate, string endDate, string fields = null)
        {
            return GetDispatchPositionsDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetDispatchPositionsDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDispatchPositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDispatchPositionsDataSource"/>
        public Task<JToken> GetDispatchPositionsDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetDispatchPositionsDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetDispatchPositionsDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetDispatchPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDispatchPositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDispatchPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetDispatchPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchPositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDispatchPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDispatchPositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves opportunity-position reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOpportunityPositionsDataSource(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityPositionsDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetOpportunityPositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOpportunityPositionsDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOpportunityPositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOpportunityPositionsDataSource"/>
        public Task<JToken> GetOpportunityPositionsDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityPositionsDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityPositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOpportunityPositionsDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetOpportunityPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOpportunityPositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOpportunityPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityPositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOpportunityPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOpportunityPositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves creditor-account reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetCreditorAccountDataSource(string startDate, string endDate, string fields = null)
        {
            return GetCreditorAccountDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetCreditorAccountDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetCreditorAccountDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalAccount/creditor";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetCreditorAccountDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetCreditorAccountDataSource"/>
        public Task<JToken> GetCreditorAccountDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetCreditorAccountDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditorAccountDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetCreditorAccountDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetCreditorAccountDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetCreditorAccountDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetCreditorAccountDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetCreditorAccountDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditorAccountDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetCreditorAccountDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalAccount/creditor";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetCreditorAccountDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves debtor-account reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetDebtorAccountDataSource(string startDate, string endDate, string fields = null)
        {
            return GetDebtorAccountDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetDebtorAccountDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetDebtorAccountDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalAccount/debtor";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDebtorAccountDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDebtorAccountDataSource"/>
        public Task<JToken> GetDebtorAccountDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetDebtorAccountDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDebtorAccountDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetDebtorAccountDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetDebtorAccountDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDebtorAccountDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDebtorAccountDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetDebtorAccountDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDebtorAccountDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDebtorAccountDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/personalAccount/debtor";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDebtorAccountDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves product usage in opportunities for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOpportunityProductUsageDataSource(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityProductUsageDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetOpportunityProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOpportunityProductUsageDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOpportunityProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOpportunityProductUsageDataSource"/>
        public Task<JToken> GetOpportunityProductUsageDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityProductUsageDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOpportunityProductUsageDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetOpportunityProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOpportunityProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOpportunityProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetOpportunityProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOpportunityProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOpportunityProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/opportunity";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOpportunityProductUsageDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves project-resource reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetProjectResourceDataSource(string startDate, string endDate, string fields = null)
        {
            return GetProjectResourceDataSourceWithHttpInfo(startDate, endDate, fields).Data;
        }

        /// <inheritdoc cref="GetProjectResourceDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetProjectResourceDataSourceWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/projectResource";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProjectResourceDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProjectResourceDataSource"/>
        public Task<JToken> GetProjectResourceDataSourceAsync(string startDate, string endDate, string fields = null)
        {
            return GetProjectResourceDataSourceAsync(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectResourceDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetProjectResourceDataSourceAsync(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            return (await GetProjectResourceDataSourceAsyncWithHttpInfo(startDate, endDate, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectResourceDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProjectResourceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields = null)
        {
            return GetProjectResourceDataSourceAsyncWithHttpInfo(startDate, endDate, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectResourceDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProjectResourceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/projectResource";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProjectResourceDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves project-revenue reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="revenueType">The optional revenue type sent unchanged; the manufacturer documents Planung, Realisierung and Abweichung. Null omits the filter and an empty string is sent; server validation applies.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. The localized revenue type is not converted to the integer codes used by separate project workflows. The client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetProjectRevenueDataSource(string startDate, string endDate, string revenueType = null, string fields = null)
        {
            return GetProjectRevenueDataSourceWithHttpInfo(startDate, endDate, revenueType, fields).Data;
        }

        /// <inheritdoc cref="GetProjectRevenueDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetProjectRevenueDataSourceWithHttpInfo(string startDate, string endDate, string revenueType = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/projectRevenue";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (revenueType != null) queryParams.Add(new KeyValuePair<string, string>("revenueType", revenueType));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetProjectRevenueDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetProjectRevenueDataSource"/>
        public Task<JToken> GetProjectRevenueDataSourceAsync(string startDate, string endDate, string revenueType = null, string fields = null)
        {
            return GetProjectRevenueDataSourceAsync(startDate, endDate, revenueType, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectRevenueDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="revenueType">The optional revenue type sent unchanged; the manufacturer documents Planung, Realisierung and Abweichung. Null omits the filter and an empty string is sent; server validation applies.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetProjectRevenueDataSourceAsync(string startDate, string endDate, string revenueType, string fields, CancellationToken cancellationToken)
        {
            return (await GetProjectRevenueDataSourceAsyncWithHttpInfo(startDate, endDate, revenueType, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProjectRevenueDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetProjectRevenueDataSourceAsyncWithHttpInfo(string startDate, string endDate, string revenueType = null, string fields = null)
        {
            return GetProjectRevenueDataSourceAsyncWithHttpInfo(startDate, endDate, revenueType, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProjectRevenueDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="revenueType">The optional revenue type sent unchanged; the manufacturer documents Planung, Realisierung and Abweichung. Null omits the filter and an empty string is sent; server validation applies.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetProjectRevenueDataSourceAsyncWithHttpInfo(string startDate, string endDate, string revenueType, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/projectRevenue";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (revenueType != null) queryParams.Add(new KeyValuePair<string, string>("revenueType", revenueType));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetProjectRevenueDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves SUSA creditor reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetSusaCreditorsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaCreditorsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetSusaCreditorsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetSusaCreditorsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/creditors";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetSusaCreditorsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetSusaCreditorsDataSource"/>
        public Task<JToken> GetSusaCreditorsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaCreditorsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaCreditorsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetSusaCreditorsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetSusaCreditorsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetSusaCreditorsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetSusaCreditorsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaCreditorsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaCreditorsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetSusaCreditorsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/creditors";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetSusaCreditorsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves SUSA debtor reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetSusaDebtorsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaDebtorsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetSusaDebtorsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetSusaDebtorsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/debtors";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetSusaDebtorsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetSusaDebtorsDataSource"/>
        public Task<JToken> GetSusaDebtorsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaDebtorsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaDebtorsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetSusaDebtorsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetSusaDebtorsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetSusaDebtorsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetSusaDebtorsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaDebtorsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaDebtorsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetSusaDebtorsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/debtors";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetSusaDebtorsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves SUSA impersonal-account reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetSusaImpersonalAccountsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaImpersonalAccountsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetSusaImpersonalAccountsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetSusaImpersonalAccountsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/impersonalAccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetSusaImpersonalAccountsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetSusaImpersonalAccountsDataSource"/>
        public Task<JToken> GetSusaImpersonalAccountsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaImpersonalAccountsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaImpersonalAccountsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetSusaImpersonalAccountsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetSusaImpersonalAccountsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetSusaImpersonalAccountsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetSusaImpersonalAccountsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetSusaImpersonalAccountsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetSusaImpersonalAccountsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetSusaImpersonalAccountsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/susa/impersonalAccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetSusaImpersonalAccountsDataSource", response, ExceptionFactory);
        }

    }
}
