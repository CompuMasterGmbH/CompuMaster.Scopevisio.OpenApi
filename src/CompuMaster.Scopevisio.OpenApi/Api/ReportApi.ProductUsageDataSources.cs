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
        /// <summary>Retrieves product usage in credit notes for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetCreditNoteProductUsageDataSource(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteProductUsageDataSourceWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetCreditNoteProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetCreditNoteProductUsageDataSourceWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetCreditNoteProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetCreditNoteProductUsageDataSource"/>
        public Task<JToken> GetCreditNoteProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteProductUsageDataSourceAsync(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNoteProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetCreditNoteProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetCreditNoteProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetCreditNoteProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetCreditNoteProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetCreditNoteProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetCreditNoteProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetCreditNoteProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/creditNote";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetCreditNoteProductUsageDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves product usage in dispatches for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, visibility and range semantics. Dispatch product usage has no documented accounting entity filter; the client does not infer completeness or access. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetDispatchProductUsageDataSource(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string fields = null)
        {
            return GetDispatchProductUsageDataSourceWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, fields).Data;
        }

        /// <inheritdoc cref="GetDispatchProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetDispatchProductUsageDataSourceWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetDispatchProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetDispatchProductUsageDataSource"/>
        public Task<JToken> GetDispatchProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string fields = null)
        {
            return GetDispatchProductUsageDataSourceAsync(startDate, endDate, showCustomersSeparately, currency, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetDispatchProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately, string currency, string fields, CancellationToken cancellationToken)
        {
            return (await GetDispatchProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetDispatchProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetDispatchProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string fields = null)
        {
            return GetDispatchProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetDispatchProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetDispatchProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately, string currency, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/dispatch";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetDispatchProductUsageDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves product usage in offers for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="validOffersOnly">The optional choice to restrict results to valid offers; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOfferProductUsageDataSource(string startDate, string endDate, bool? showCustomersSeparately = null, bool? validOffersOnly = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferProductUsageDataSourceWithHttpInfo(startDate, endDate, showCustomersSeparately, validOffersOnly, currency, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOfferProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOfferProductUsageDataSourceWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, bool? validOffersOnly = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (validOffersOnly.HasValue) queryParams.Add(new KeyValuePair<string, string>("validOffersOnly", validOffersOnly.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOfferProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOfferProductUsageDataSource"/>
        public Task<JToken> GetOfferProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately = null, bool? validOffersOnly = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferProductUsageDataSourceAsync(startDate, endDate, showCustomersSeparately, validOffersOnly, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="validOffersOnly">The optional choice to restrict results to valid offers; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOfferProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately, bool? validOffersOnly, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOfferProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, validOffersOnly, currency, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOfferProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOfferProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, bool? validOffersOnly = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOfferProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, validOffersOnly, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOfferProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="validOffersOnly">The optional choice to restrict results to valid offers; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOfferProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately, bool? validOffersOnly, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/offer";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (validOffersOnly.HasValue) queryParams.Add(new KeyValuePair<string, string>("validOffersOnly", validOffersOnly.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOfferProductUsageDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves product usage in orders for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOrderProductUsageDataSource(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderProductUsageDataSourceWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOrderProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOrderProductUsageDataSourceWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOrderProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOrderProductUsageDataSource"/>
        public Task<JToken> GetOrderProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderProductUsageDataSourceAsync(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOrderProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOrderProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOrderProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOrderProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOrderProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOrderProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOrderProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/order";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOrderProductUsageDataSource", response, ExceptionFactory);
        }

        /// <summary>Retrieves product usage in outgoing invoices for the selected dates.</summary>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares no fixed success schema or paging parameters. The server enforces profiles, entity-filter membership, visibility and range semantics. The client does not infer consolidated totals, completeness or access from entity numbers. Fractions use .NET decimal precision and may round; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A required date is missing or invalid in dd.MM.yyyy format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken GetOutgoingInvoiceProductUsageDataSource(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceProductUsageDataSourceWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceProductUsageDataSource"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> GetOutgoingInvoiceProductUsageDataSourceWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOutgoingInvoiceProductUsageDataSource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceProductUsageDataSource"/>
        public Task<JToken> GetOutgoingInvoiceProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceProductUsageDataSourceAsync(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceProductUsageDataSource"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetOutgoingInvoiceProductUsageDataSourceAsync(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            return (await GetOutgoingInvoiceProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceProductUsageDataSourceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOutgoingInvoiceProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately = null, string currency = null, string accountingEntityNumbers = null, string fields = null)
        {
            return GetOutgoingInvoiceProductUsageDataSourceAsyncWithHttpInfo(startDate, endDate, showCustomersSeparately, currency, accountingEntityNumbers, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceProductUsageDataSourceWithHttpInfo"/>
        /// <param name="startDate">The required start date in dd.MM.yyyy format, sent unchanged.</param>
        /// <param name="endDate">The required end date in dd.MM.yyyy format, sent unchanged; the server determines range semantics.</param>
        /// <param name="showCustomersSeparately">The optional customer grouping choice; null omits the parameter, while false and true are sent explicitly.</param>
        /// <param name="currency">The optional ISO three-letter currency code sent unchanged; null omits it and retains the documented server default EUR. An empty string is sent for server validation.</param>
        /// <param name="accountingEntityNumbers">The optional comma-separated accounting entity numbers, retained as text including leading zeros; null omits the filter and an empty string is sent.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOutgoingInvoiceProductUsageDataSourceAsyncWithHttpInfo(string startDate, string endDate, bool? showCustomersSeparately, string currency, string accountingEntityNumbers, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateBasicDataSourceDate(startDate, nameof(startDate));
            ValidateBasicDataSourceDate(endDate, nameof(endDate));
            var localVarPath = "/datasource/productUsage/outgoingInvoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("startDate", startDate));
            queryParams.Add(new KeyValuePair<string, string>("endDate", endDate));
            if (showCustomersSeparately.HasValue) queryParams.Add(new KeyValuePair<string, string>("showCustomersSeparately", showCustomersSeparately.Value ? "true" : "false"));
            if (currency != null) queryParams.Add(new KeyValuePair<string, string>("currency", currency));
            if (accountingEntityNumbers != null) queryParams.Add(new KeyValuePair<string, string>("accountingEntityNumbers", accountingEntityNumbers));
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOutgoingInvoiceProductUsageDataSource", response, ExceptionFactory);
        }

    }
}
