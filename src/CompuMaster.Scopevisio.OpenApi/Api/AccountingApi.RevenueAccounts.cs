using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
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
        /// <summary>Retrieves the revenue accounts for the selected product.</summary>
        /// <param name="productId">The selected Int64 product identifier.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="preferred">The optional preferred-account filter; null retains the server default of false. True implies effective active filtering and forbids a negative VAT scope.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <returns>The typed response retaining unknown fields, or null for an empty body or JSON null.</returns>
        /// <remarks>The server returns at most 1000 records and provides no paging parameter for this endpoint. The server enforces required accounting profiles and visibility. Optional values are sent unchanged; no client-side filter or date default is imposed. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps and type codes remain Int64 values.</remarks>
        /// <exception cref="ArgumentException">A supplied date is invalid, or preferred filtering is combined with a negative VAT scope.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON or an incompatible root/records shape, or a fraction exceeds the decimal range.</exception>
        public RevenueAccountsResponse GetProductRevenueAccounts(long productId, string fields = null, bool? preferred = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductRevenueAccountsWithHttpInfo(productId, fields, preferred, country, vatScope, active, servicesRenderedDate).Data;
        }

        /// <inheritdoc cref="GetProductRevenueAccounts"/>
        /// <returns>The status, headers, exact raw response and typed data.</returns>
        public ApiResponse<RevenueAccountsResponse> GetProductRevenueAccountsWithHttpInfo(long productId, string fields = null, bool? preferred = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, preferred);
            var localVarPath = "/revenueaccounts/product/{id}";
            localVarPath = localVarPath.Replace("{id}", productId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (preferred.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("preferred", preferred.Value ? "true" : "false"));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetProductRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        /// <inheritdoc cref="GetProductRevenueAccounts"/>
        public Task<RevenueAccountsResponse> GetProductRevenueAccountsAsync(long productId, string fields = null, bool? preferred = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductRevenueAccountsAsync(productId, fields, preferred, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductRevenueAccounts"/>
        /// <param name="productId">The selected Int64 product identifier.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="preferred">The optional preferred-account filter; null retains the server default of false. True implies effective active filtering and forbids a negative VAT scope.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<RevenueAccountsResponse> GetProductRevenueAccountsAsync(long productId, string fields, bool? preferred, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            return (await GetProductRevenueAccountsAsyncWithHttpInfo(productId, fields, preferred, country, vatScope, active, servicesRenderedDate, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProductRevenueAccountsWithHttpInfo"/>
        public Task<ApiResponse<RevenueAccountsResponse>> GetProductRevenueAccountsAsyncWithHttpInfo(long productId, string fields = null, bool? preferred = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductRevenueAccountsAsyncWithHttpInfo(productId, fields, preferred, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductRevenueAccountsWithHttpInfo"/>
        /// <param name="productId">The selected Int64 product identifier.</param>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="preferred">The optional preferred-account filter; null retains the server default of false. True implies effective active filtering and forbids a negative VAT scope.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<RevenueAccountsResponse>> GetProductRevenueAccountsAsyncWithHttpInfo(long productId, string fields, bool? preferred, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, preferred);
            var localVarPath = "/revenueaccounts/product/{id}";
            localVarPath = localVarPath.Replace("{id}", productId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (preferred.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("preferred", preferred.Value ? "true" : "false"));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetProductRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        /// <summary>Retrieves paged revenue accounts configured explicitly for products.</summary>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null retains the server default.</param>
        /// <param name="pageSize">The optional requested page size, sent unchanged; the server silently enforces a maximum of 1000.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <returns>The typed response retaining unknown fields, or null for an empty body or JSON null.</returns>
        /// <remarks>Products using organisation-standard revenue accounts are omitted. No paging requests are performed automatically. The server enforces required accounting profiles and visibility. Optional values are sent unchanged; no client-side filter or date default is imposed. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps and type codes remain Int64 values.</remarks>
        /// <exception cref="ArgumentException">A supplied date is invalid, or preferred filtering is combined with a negative VAT scope.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON or an incompatible root/records shape, or a fraction exceeds the decimal range.</exception>
        public RevenueAccountsResponse GetProductsRevenueAccounts(string fields = null, int? page = null, int? pageSize = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductsRevenueAccountsWithHttpInfo(fields, page, pageSize, country, vatScope, active, servicesRenderedDate).Data;
        }

        /// <inheritdoc cref="GetProductsRevenueAccounts"/>
        /// <returns>The status, headers, exact raw response and typed data.</returns>
        public ApiResponse<RevenueAccountsResponse> GetProductsRevenueAccountsWithHttpInfo(string fields = null, int? page = null, int? pageSize = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, null);
            var localVarPath = "/revenueaccounts/products";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (page.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetProductsRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        /// <inheritdoc cref="GetProductsRevenueAccounts"/>
        public Task<RevenueAccountsResponse> GetProductsRevenueAccountsAsync(string fields = null, int? page = null, int? pageSize = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductsRevenueAccountsAsync(fields, page, pageSize, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductsRevenueAccounts"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null retains the server default.</param>
        /// <param name="pageSize">The optional requested page size, sent unchanged; the server silently enforces a maximum of 1000.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<RevenueAccountsResponse> GetProductsRevenueAccountsAsync(string fields, int? page, int? pageSize, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            return (await GetProductsRevenueAccountsAsyncWithHttpInfo(fields, page, pageSize, country, vatScope, active, servicesRenderedDate, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetProductsRevenueAccountsWithHttpInfo"/>
        public Task<ApiResponse<RevenueAccountsResponse>> GetProductsRevenueAccountsAsyncWithHttpInfo(string fields = null, int? page = null, int? pageSize = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetProductsRevenueAccountsAsyncWithHttpInfo(fields, page, pageSize, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetProductsRevenueAccountsWithHttpInfo"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="page">The optional zero-based page number; null retains the server default.</param>
        /// <param name="pageSize">The optional requested page size, sent unchanged; the server silently enforces a maximum of 1000.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<RevenueAccountsResponse>> GetProductsRevenueAccountsAsyncWithHttpInfo(string fields, int? page, int? pageSize, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, null);
            var localVarPath = "/revenueaccounts/products";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (page.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetProductsRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        /// <summary>Retrieves standard revenue accounts for the active organisation.</summary>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <returns>The typed response retaining unknown fields, or null for an empty body or JSON null.</returns>
        /// <remarks>These standard accounts do not enumerate every product using them. The server enforces required accounting profiles and visibility. Optional values are sent unchanged; no client-side filter or date default is imposed. Fractional values use .NET decimal precision and may round; raw JSON remains exact. Numeric timestamps and type codes remain Int64 values.</remarks>
        /// <exception cref="ArgumentException">A supplied date is invalid, or preferred filtering is combined with a negative VAT scope.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON or an incompatible root/records shape, or a fraction exceeds the decimal range.</exception>
        public RevenueAccountsResponse GetStandardRevenueAccounts(string fields = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetStandardRevenueAccountsWithHttpInfo(fields, country, vatScope, active, servicesRenderedDate).Data;
        }

        /// <inheritdoc cref="GetStandardRevenueAccounts"/>
        /// <returns>The status, headers, exact raw response and typed data.</returns>
        public ApiResponse<RevenueAccountsResponse> GetStandardRevenueAccountsWithHttpInfo(string fields = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, null);
            var localVarPath = "/revenueaccounts/standard";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetStandardRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        /// <inheritdoc cref="GetStandardRevenueAccounts"/>
        public Task<RevenueAccountsResponse> GetStandardRevenueAccountsAsync(string fields = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetStandardRevenueAccountsAsync(fields, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStandardRevenueAccounts"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<RevenueAccountsResponse> GetStandardRevenueAccountsAsync(string fields, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            return (await GetStandardRevenueAccountsAsyncWithHttpInfo(fields, country, vatScope, active, servicesRenderedDate, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetStandardRevenueAccountsWithHttpInfo"/>
        public Task<ApiResponse<RevenueAccountsResponse>> GetStandardRevenueAccountsAsyncWithHttpInfo(string fields = null, string country = null, long? vatScope = null, bool? active = null, string servicesRenderedDate = null)
        {
            return GetStandardRevenueAccountsAsyncWithHttpInfo(fields, country, vatScope, active, servicesRenderedDate, CancellationToken.None);
        }

        /// <inheritdoc cref="GetStandardRevenueAccountsWithHttpInfo"/>
        /// <param name="fields">The optional comma-separated selected fields; null omits the parameter, while an empty string is sent.</param>
        /// <param name="country">The optional two-letter country ISO code; null uses the organisation's taxation country.</param>
        /// <param name="vatScope">The optional VAT case identifier; null uses the server default of 1. Negative values select all scopes except with preferred filtering; zero has undefined server behavior.</param>
        /// <param name="active">The optional active filter; null retains the server default of false. Preferred product filtering is effectively active regardless of this value.</param>
        /// <param name="servicesRenderedDate">The optional date in dd.MM.yyyy format; null uses today's server date. It affects filtering when active is effective.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<RevenueAccountsResponse>> GetStandardRevenueAccountsAsyncWithHttpInfo(string fields, string country, long? vatScope, bool? active, string servicesRenderedDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateRevenueAccountParameters(servicesRenderedDate, vatScope, null);
            var localVarPath = "/revenueaccounts/standard";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null)
                queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (country != null)
                queryParams.Add(new KeyValuePair<string, string>("country", country));
            if (vatScope.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("vatScope", vatScope.Value.ToString(CultureInfo.InvariantCulture)));
            if (active.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("active", active.Value ? "true" : "false"));
            if (servicesRenderedDate != null)
                queryParams.Add(new KeyValuePair<string, string>("servicesRenderedDate", servicesRenderedDate));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapAccountingReadResponse(SearchJsonResponse.Parse("GetStandardRevenueAccounts", response, ExceptionFactory),
                data => new RevenueAccountsResponse(data));
        }

        private static void ValidateRevenueAccountParameters(string servicesRenderedDate, long? vatScope, bool? preferred)
        {
            if (servicesRenderedDate != null && !DateTime.TryParseExact(servicesRenderedDate, "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                throw new ArgumentException("The services-rendered date must be valid in dd.MM.yyyy format.", nameof(servicesRenderedDate));
            if (preferred == true && vatScope < 0)
                throw new ArgumentException("Preferred revenue-account filtering does not allow a negative VAT scope.", nameof(vatScope));
        }
    }
}
