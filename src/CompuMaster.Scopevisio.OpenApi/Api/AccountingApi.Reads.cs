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
    public partial class AccountingApi
    {
        /// <summary>Retrieves the account mapping configuration for the selected accounting area.</summary>
        /// <param name="accountingArea">The optional accounting area identifier; null uses the active accounting area.</param>
        /// <returns>The typed JSON result retaining unknown fields, or null for an empty response body or JSON null.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or has an incompatible root or collection shape, or a fractional value exceeds the decimal range.</exception>
        public AccountMappingResponse GetAccountMapping(long? accountingArea = null)
        {
            return GetAccountMappingWithHttpInfo(accountingArea).Data;
        }

        /// <inheritdoc cref="GetAccountMapping"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<AccountMappingResponse> GetAccountMappingWithHttpInfo(long? accountingArea = null)
        {
            var localVarPath = "/accountmapping";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (accountingArea.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("accountingArea", accountingArea.Value.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetAccountMapping", response, ExceptionFactory), data => new AccountMappingResponse(data));
        }

        /// <inheritdoc cref="GetAccountMapping"/>
        public Task<AccountMappingResponse> GetAccountMappingAsync(long? accountingArea = null)
        {
            return GetAccountMappingAsync(accountingArea, CancellationToken.None);
        }

        /// <inheritdoc cref="GetAccountMapping"/>
        /// <param name="accountingArea">The optional accounting area identifier; null uses the active accounting area.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<AccountMappingResponse> GetAccountMappingAsync(long? accountingArea, CancellationToken cancellationToken)
        {
            return (await GetAccountMappingAsyncWithHttpInfo(accountingArea, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetAccountMappingWithHttpInfo"/>
        public Task<ApiResponse<AccountMappingResponse>> GetAccountMappingAsyncWithHttpInfo(long? accountingArea = null)
        {
            return GetAccountMappingAsyncWithHttpInfo(accountingArea, CancellationToken.None);
        }

        /// <inheritdoc cref="GetAccountMappingWithHttpInfo"/>
        /// <param name="accountingArea">The optional accounting area identifier; null uses the active accounting area.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<AccountMappingResponse>> GetAccountMappingAsyncWithHttpInfo(long? accountingArea, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/accountmapping";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (accountingArea.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("accountingArea", accountingArea.Value.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetAccountMapping", response, ExceptionFactory), data => new AccountMappingResponse(data));
        }

        /// <summary>Retrieves fiscal years and posting-period information.</summary>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken GetFiscalYears()
        {
            return GetFiscalYearsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetFiscalYears"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> GetFiscalYearsWithHttpInfo()
        {
            var localVarPath = "/fiscalyears";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetFiscalYears", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetFiscalYears"/>
        public Task<JToken> GetFiscalYearsAsync()
        {
            return GetFiscalYearsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetFiscalYears"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetFiscalYearsAsync(CancellationToken cancellationToken)
        {
            return (await GetFiscalYearsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetFiscalYearsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetFiscalYearsAsyncWithHttpInfo()
        {
            return GetFiscalYearsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetFiscalYearsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetFiscalYearsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/fiscalyears";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetFiscalYears", response, ExceptionFactory);
        }

        /// <summary>Retrieves gain and loss adjustment accounts for the balance date.</summary>
        /// <param name="balanceDate">The required balance date in dd.MM.yyyy format.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ArgumentException">The balance date is missing or is not a valid date in the required format.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken GetGainAndLossAdjustmentAccounts(string balanceDate)
        {
            return GetGainAndLossAdjustmentAccountsWithHttpInfo(balanceDate).Data;
        }

        /// <inheritdoc cref="GetGainAndLossAdjustmentAccounts"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> GetGainAndLossAdjustmentAccountsWithHttpInfo(string balanceDate)
        {
            ValidateAccountingBalanceDate(balanceDate);
            var localVarPath = "/gainandlossadjustmentaccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("balanceDate", balanceDate));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetGainAndLossAdjustmentAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetGainAndLossAdjustmentAccounts"/>
        public Task<JToken> GetGainAndLossAdjustmentAccountsAsync(string balanceDate)
        {
            return GetGainAndLossAdjustmentAccountsAsync(balanceDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetGainAndLossAdjustmentAccounts"/>
        /// <param name="balanceDate">The required balance date in dd.MM.yyyy format.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetGainAndLossAdjustmentAccountsAsync(string balanceDate, CancellationToken cancellationToken)
        {
            return (await GetGainAndLossAdjustmentAccountsAsyncWithHttpInfo(balanceDate, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetGainAndLossAdjustmentAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetGainAndLossAdjustmentAccountsAsyncWithHttpInfo(string balanceDate)
        {
            return GetGainAndLossAdjustmentAccountsAsyncWithHttpInfo(balanceDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetGainAndLossAdjustmentAccountsWithHttpInfo"/>
        /// <param name="balanceDate">The required balance date in dd.MM.yyyy format.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetGainAndLossAdjustmentAccountsAsyncWithHttpInfo(string balanceDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateAccountingBalanceDate(balanceDate);
            var localVarPath = "/gainandlossadjustmentaccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("balanceDate", balanceDate));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetGainAndLossAdjustmentAccounts", response, ExceptionFactory);
        }

        /// <summary>Retrieves the statistics account selected by its account number.</summary>
        /// <param name="number">The required account number, retained as a string including leading zeros.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ArgumentException">The account number is missing or is a relative path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken GetStatisticsAccountByNumber(string number, string fields = null)
        {
            return GetStatisticsAccountByNumberWithHttpInfo(number, fields).Data;
        }

        /// <inheritdoc cref="GetStatisticsAccountByNumber"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> GetStatisticsAccountByNumberWithHttpInfo(string number, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..") throw new ArgumentException("The account number is missing or is a relative path segment.", nameof(number));
            var localVarPath = "/statisticsaccount/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetStatisticsAccountByNumber", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetStatisticsAccountByNumber"/>
        public Task<JToken> GetStatisticsAccountByNumberAsync(string number, string fields = null)
        {
            return GetStatisticsAccountByNumberAsync(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsAccountByNumber"/>
        /// <param name="number">The required account number, retained as a string including leading zeros.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetStatisticsAccountByNumberAsync(string number, string fields, CancellationToken cancellationToken)
        {
            return (await GetStatisticsAccountByNumberAsyncWithHttpInfo(number, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetStatisticsAccountByNumberWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetStatisticsAccountByNumberAsyncWithHttpInfo(string number, string fields = null)
        {
            return GetStatisticsAccountByNumberAsyncWithHttpInfo(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsAccountByNumberWithHttpInfo"/>
        /// <param name="number">The required account number, retained as a string including leading zeros.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetStatisticsAccountByNumberAsyncWithHttpInfo(string number, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..") throw new ArgumentException("The account number is missing or is a relative path segment.", nameof(number));
            var localVarPath = "/statisticsaccount/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetStatisticsAccountByNumber", response, ExceptionFactory);
        }

        /// <summary>Retrieves the statistics posting selected by its row number.</summary>
        /// <param name="rowNumber">The selected Int64 posting row number.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken GetStatisticsPosting(long rowNumber, string fields = null)
        {
            return GetStatisticsPostingWithHttpInfo(rowNumber, fields).Data;
        }

        /// <inheritdoc cref="GetStatisticsPosting"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> GetStatisticsPostingWithHttpInfo(long rowNumber, string fields = null)
        {
            var localVarPath = "/statisticspostings/{rowNumber}";
            localVarPath = localVarPath.Replace("{rowNumber}", rowNumber.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetStatisticsPosting", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetStatisticsPosting"/>
        public Task<JToken> GetStatisticsPostingAsync(long rowNumber, string fields = null)
        {
            return GetStatisticsPostingAsync(rowNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsPosting"/>
        /// <param name="rowNumber">The selected Int64 posting row number.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetStatisticsPostingAsync(long rowNumber, string fields, CancellationToken cancellationToken)
        {
            return (await GetStatisticsPostingAsyncWithHttpInfo(rowNumber, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetStatisticsPostingWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetStatisticsPostingAsyncWithHttpInfo(long rowNumber, string fields = null)
        {
            return GetStatisticsPostingAsyncWithHttpInfo(rowNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStatisticsPostingWithHttpInfo"/>
        /// <param name="rowNumber">The selected Int64 posting row number.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetStatisticsPostingAsyncWithHttpInfo(long rowNumber, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/statisticspostings/{rowNumber}";
            localVarPath = localVarPath.Replace("{rowNumber}", rowNumber.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetStatisticsPosting", response, ExceptionFactory);
        }

        /// <summary>Retrieves VAT scopes for the active organisation.</summary>
        /// <param name="active">The optional active-scope filter; null retains the server default, and false is sent explicitly.</param>
        /// <returns>The typed JSON result retaining unknown fields, or null for an empty response body or JSON null.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or has an incompatible root or collection shape, or a fractional value exceeds the decimal range.</exception>
        public VatScopesResponse GetVatScopes(bool? active = null)
        {
            return GetVatScopesWithHttpInfo(active).Data;
        }

        /// <inheritdoc cref="GetVatScopes"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<VatScopesResponse> GetVatScopesWithHttpInfo(bool? active = null)
        {
            var localVarPath = "/vatscopes";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetVatScopes", response, ExceptionFactory), data => new VatScopesResponse(data));
        }

        /// <inheritdoc cref="GetVatScopes"/>
        public Task<VatScopesResponse> GetVatScopesAsync(bool? active = null)
        {
            return GetVatScopesAsync(active, CancellationToken.None);
        }

        /// <inheritdoc cref="GetVatScopes"/>
        /// <param name="active">The optional active-scope filter; null retains the server default, and false is sent explicitly.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<VatScopesResponse> GetVatScopesAsync(bool? active, CancellationToken cancellationToken)
        {
            return (await GetVatScopesAsyncWithHttpInfo(active, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetVatScopesWithHttpInfo"/>
        public Task<ApiResponse<VatScopesResponse>> GetVatScopesAsyncWithHttpInfo(bool? active = null)
        {
            return GetVatScopesAsyncWithHttpInfo(active, CancellationToken.None);
        }

        /// <inheritdoc cref="GetVatScopesWithHttpInfo"/>
        /// <param name="active">The optional active-scope filter; null retains the server default, and false is sent explicitly.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<VatScopesResponse>> GetVatScopesAsyncWithHttpInfo(bool? active, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/vatscopes";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetVatScopes", response, ExceptionFactory), data => new VatScopesResponse(data));
        }

        /// <summary>Searches statistics accounts and retains the complete JSON result.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken SearchStatisticsAccounts(SearchRequest query = null)
        {
            return SearchStatisticsAccountsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchStatisticsAccounts"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> SearchStatisticsAccountsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/statisticsaccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchStatisticsAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchStatisticsAccounts"/>
        public Task<JToken> SearchStatisticsAccountsAsync(SearchRequest query = null)
        {
            return SearchStatisticsAccountsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchStatisticsAccounts"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchStatisticsAccountsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchStatisticsAccountsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchStatisticsAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchStatisticsAccountsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchStatisticsAccountsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchStatisticsAccountsWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchStatisticsAccountsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/statisticsaccounts";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchStatisticsAccounts", response, ExceptionFactory);
        }

        /// <summary>Searches statistics postings and retains the complete JSON result.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken SearchStatisticsPostings(SearchRequest query = null)
        {
            return SearchStatisticsPostingsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchStatisticsPostings"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> SearchStatisticsPostingsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/statisticspostings";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchStatisticsPostings", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchStatisticsPostings"/>
        public Task<JToken> SearchStatisticsPostingsAsync(SearchRequest query = null)
        {
            return SearchStatisticsPostingsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchStatisticsPostings"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchStatisticsPostingsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchStatisticsPostingsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchStatisticsPostingsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchStatisticsPostingsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchStatisticsPostingsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchStatisticsPostingsWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchStatisticsPostingsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/statisticspostings";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchStatisticsPostings", response, ExceptionFactory);
        }

        /// <summary>Searches available travel entries and retains the complete JSON result.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>Required account profiles and resource visibility are enforced by the server. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps remain unchanged; no date or timezone reinterpretation is performed. The manufacturer declares no fixed success schema for this operation.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON, or a fractional value exceeds the decimal range.</exception>
        public JToken SearchTravelEntries(SearchRequest query = null)
        {
            return SearchTravelEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchTravelEntries"/>
        /// <returns>The status, headers, unchanged raw response and parsed result.</returns>
        public ApiResponse<JToken> SearchTravelEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/travelentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchTravelEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchTravelEntries"/>
        public Task<JToken> SearchTravelEntriesAsync(SearchRequest query = null)
        {
            return SearchTravelEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTravelEntries"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchTravelEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchTravelEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchTravelEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchTravelEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchTravelEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchTravelEntriesWithHttpInfo"/>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchTravelEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/travelentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query?.ToString(Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchTravelEntries", response, ExceptionFactory);
        }

        private static void ValidateAccountingBalanceDate(string balanceDate)
        {
            if (balanceDate == null || !DateTime.TryParseExact(balanceDate, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
                throw new ArgumentException("The balance date must be a valid date in dd.MM.yyyy format.", nameof(balanceDate));
        }

        private static ApiResponse<T> WrapAccountingReadResponse<T>(ApiResponse<JToken> response, Func<JObject, T> create) where T : JObject
        {
            T data = null;
            if (response.Data != null && response.Data.Type != JTokenType.Null)
            {
                if (!(response.Data is JObject json)) throw new JsonSerializationException("The response must be a JSON object.");
                data = create(json);
            }
            return new ApiResponse<T>(response.StatusCode, response.Headers, response.RawJsonContent, data);
        }
    }
}
