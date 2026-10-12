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
        /// <summary>Retrieves credit-note reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetCreditNoteDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetCreditNoteDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetCreditNoteDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetCreditNoteDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetCreditNoteDataSource"/>
        public Task<JToken> GetCreditNoteDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNoteDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetCreditNoteDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetCreditNoteDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetCreditNoteDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetCreditNoteDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNoteDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetCreditNoteDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetCreditNoteDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves incoming-invoice reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetIncomingInvoiceDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoiceDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoiceDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetIncomingInvoiceDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/incomingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetIncomingInvoiceDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetIncomingInvoiceDataSource"/>
        public Task<JToken> GetIncomingInvoiceDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoiceDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoiceDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetIncomingInvoiceDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetIncomingInvoiceDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoiceDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetIncomingInvoiceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoiceDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoiceDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetIncomingInvoiceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/incomingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetIncomingInvoiceDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves offer reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOfferDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOfferDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOfferDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOfferDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOfferDataSource"/>
        public Task<JToken> GetOfferDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOfferDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOfferDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOfferDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOfferDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOfferDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOfferDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves order reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOrderDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOrderDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOrderDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOrderDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOrderDataSource"/>
        public Task<JToken> GetOrderDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOrderDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOrderDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOrderDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOrderDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOrderDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOrderDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves outgoing-invoice reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOutgoingInvoiceDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOutgoingInvoiceDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOutgoingInvoiceDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceDataSource"/>
        public Task<JToken> GetOutgoingInvoiceDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOutgoingInvoiceDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOutgoingInvoiceDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOutgoingInvoiceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOutgoingInvoiceDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOutgoingInvoiceDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves credit-note reporting positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetCreditNotePositionsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNotePositionsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetCreditNotePositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetCreditNotePositionsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetCreditNotePositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetCreditNotePositionsDataSource"/>
        public Task<JToken> GetCreditNotePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNotePositionsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNotePositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetCreditNotePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetCreditNotePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetCreditNotePositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetCreditNotePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNotePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNotePositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetCreditNotePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetCreditNotePositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves incoming-invoice reporting positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetIncomingInvoicePositionsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoicePositionsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoicePositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetIncomingInvoicePositionsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/incomingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetIncomingInvoicePositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetIncomingInvoicePositionsDataSource"/>
        public Task<JToken> GetIncomingInvoicePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoicePositionsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoicePositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetIncomingInvoicePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetIncomingInvoicePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoicePositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetIncomingInvoicePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetIncomingInvoicePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoicePositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetIncomingInvoicePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/incomingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetIncomingInvoicePositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves offer reporting positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOfferPositionsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferPositionsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOfferPositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOfferPositionsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOfferPositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOfferPositionsDataSource"/>
        public Task<JToken> GetOfferPositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferPositionsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferPositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOfferPositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOfferPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOfferPositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOfferPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferPositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOfferPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOfferPositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves order reporting positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOrderPositionsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderPositionsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOrderPositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOrderPositionsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOrderPositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOrderPositionsDataSource"/>
        public Task<JToken> GetOrderPositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderPositionsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderPositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOrderPositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOrderPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOrderPositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOrderPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderPositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderPositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOrderPositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOrderPositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves outgoing-invoice reporting positions for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOutgoingInvoicePositionsDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoicePositionsDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoicePositionsDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOutgoingInvoicePositionsDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOutgoingInvoicePositionsDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOutgoingInvoicePositionsDataSource"/>
        public Task<JToken> GetOutgoingInvoicePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoicePositionsDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoicePositionsDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOutgoingInvoicePositionsDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOutgoingInvoicePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoicePositionsDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOutgoingInvoicePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoicePositionsDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoicePositionsDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOutgoingInvoicePositionsDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/positions/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOutgoingInvoicePositionsDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves statistics journal reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetStatisticsJournalDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetStatisticsJournalDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetStatisticsJournalDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetStatisticsJournalDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/statisticsJournal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetStatisticsJournalDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetStatisticsJournalDataSource"/>
        public Task<JToken> GetStatisticsJournalDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetStatisticsJournalDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsJournalDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetStatisticsJournalDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetStatisticsJournalDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetStatisticsJournalDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetStatisticsJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetStatisticsJournalDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsJournalDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetStatisticsJournalDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/statisticsJournal";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetStatisticsJournalDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves travel reporting entries for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetTravelEntryDataSource(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetTravelEntryDataSourceWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetTravelEntryDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetTravelEntryDataSourceWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/travelEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTravelEntryDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTravelEntryDataSource"/>
        public Task<JToken> GetTravelEntryDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetTravelEntryDataSourceAsync(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTravelEntryDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetTravelEntryDataSourceAsync(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetTravelEntryDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTravelEntryDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTravelEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers = null, string fields = null)
        {
            return GetTravelEntryDataSourceAsyncWithHttpInfo(startDate, endDate, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetTravelEntryDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTravelEntryDataSourceAsyncWithHttpInfo(string startDate, string endDate, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/travelEntry";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTravelEntryDataSource", response, ExceptionFactory);
        }

    }
}
