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
        /// <summary>Searches creditor open items, including unmatched payments and retains the complete JSON result.</summary>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. Server profiles, visibility, historical availability and filtering remain authoritative. No fixed success schema, automatic paging, complete-balance assertion or aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchCreditorOpenItems(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchCreditorOpenItemsWithHttpInfo(query, historicTs, accountNumber).Data;
        }

        /// <inheritdoc cref="SearchCreditorOpenItems"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchCreditorOpenItemsWithHttpInfo(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            var localVarPath = "/openitems/creditors";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (historicTs.HasValue) queryParams.Add(new KeyValuePair<string, string>("historicTs", historicTs.Value.ToString(CultureInfo.InvariantCulture)));
            if (accountNumber != null) queryParams.Add(new KeyValuePair<string, string>("accountNumber", accountNumber));
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCreditorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCreditorOpenItems"/>
        public Task<JToken> SearchCreditorOpenItemsAsync(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchCreditorOpenItemsAsync(query, historicTs, accountNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCreditorOpenItems"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchCreditorOpenItemsAsync(SearchRequest query, long? historicTs, string accountNumber, CancellationToken cancellationToken)
        {
            return (await SearchCreditorOpenItemsAsyncWithHttpInfo(query, historicTs, accountNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCreditorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCreditorOpenItemsAsyncWithHttpInfo(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchCreditorOpenItemsAsyncWithHttpInfo(query, historicTs, accountNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCreditorOpenItemsWithHttpInfo"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchCreditorOpenItemsAsyncWithHttpInfo(SearchRequest query, long? historicTs, string accountNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/openitems/creditors";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (historicTs.HasValue) queryParams.Add(new KeyValuePair<string, string>("historicTs", historicTs.Value.ToString(CultureInfo.InvariantCulture)));
            if (accountNumber != null) queryParams.Add(new KeyValuePair<string, string>("accountNumber", accountNumber));
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCreditorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Searches debtor open items, including unmatched payments and retains the complete JSON result.</summary>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. Server profiles, visibility, historical availability and filtering remain authoritative. No fixed success schema, automatic paging, complete-balance assertion or aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchDebtorOpenItems(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchDebtorOpenItemsWithHttpInfo(query, historicTs, accountNumber).Data;
        }

        /// <inheritdoc cref="SearchDebtorOpenItems"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchDebtorOpenItemsWithHttpInfo(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            var localVarPath = "/openitems/debtors";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (historicTs.HasValue) queryParams.Add(new KeyValuePair<string, string>("historicTs", historicTs.Value.ToString(CultureInfo.InvariantCulture)));
            if (accountNumber != null) queryParams.Add(new KeyValuePair<string, string>("accountNumber", accountNumber));
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchDebtorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchDebtorOpenItems"/>
        public Task<JToken> SearchDebtorOpenItemsAsync(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchDebtorOpenItemsAsync(query, historicTs, accountNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebtorOpenItems"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchDebtorOpenItemsAsync(SearchRequest query, long? historicTs, string accountNumber, CancellationToken cancellationToken)
        {
            return (await SearchDebtorOpenItemsAsyncWithHttpInfo(query, historicTs, accountNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchDebtorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchDebtorOpenItemsAsyncWithHttpInfo(SearchRequest query = null, long? historicTs = null, string accountNumber = null)
        {
            return SearchDebtorOpenItemsAsyncWithHttpInfo(query, historicTs, accountNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebtorOpenItemsWithHttpInfo"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="historicTs">The optional historical instant in epoch milliseconds, without date or timezone conversion; null omits it.</param>
        /// <param name="accountNumber">The optional original account-number string, retaining leading zeros and explicit empty text; null omits it.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchDebtorOpenItemsAsyncWithHttpInfo(SearchRequest query, long? historicTs, string accountNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/openitems/debtors";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (historicTs.HasValue) queryParams.Add(new KeyValuePair<string, string>("historicTs", historicTs.Value.ToString(CultureInfo.InvariantCulture)));
            if (accountNumber != null) queryParams.Add(new KeyValuePair<string, string>("accountNumber", accountNumber));
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchDebtorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Searches creditor personal journal entries and retains the complete JSON result.</summary>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. Server profiles, visibility, historical availability and filtering remain authoritative. No fixed success schema, automatic paging, complete-balance assertion or aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchCreditorPersonalJournalEntries(SearchRequest query = null)
        {
            return SearchCreditorPersonalJournalEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchCreditorPersonalJournalEntries"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchCreditorPersonalJournalEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/openitems/creditor/list";
            var queryParams = new List<KeyValuePair<string, string>>();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCreditorPersonalJournalEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCreditorPersonalJournalEntries"/>
        public Task<JToken> SearchCreditorPersonalJournalEntriesAsync(SearchRequest query = null)
        {
            return SearchCreditorPersonalJournalEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCreditorPersonalJournalEntries"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchCreditorPersonalJournalEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCreditorPersonalJournalEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCreditorPersonalJournalEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCreditorPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchCreditorPersonalJournalEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCreditorPersonalJournalEntriesWithHttpInfo"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchCreditorPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/openitems/creditor/list";
            var queryParams = new List<KeyValuePair<string, string>>();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCreditorPersonalJournalEntries", response, ExceptionFactory);
        }

        /// <summary>Searches debtor personal journal entries and retains the complete JSON result.</summary>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. Server profiles, visibility, historical availability and filtering remain authoritative. No fixed success schema, automatic paging, complete-balance assertion or aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchDebtorPersonalJournalEntries(SearchRequest query = null)
        {
            return SearchDebtorPersonalJournalEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchDebtorPersonalJournalEntries"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchDebtorPersonalJournalEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/openitems/debitor/list";
            var queryParams = new List<KeyValuePair<string, string>>();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchDebtorPersonalJournalEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchDebtorPersonalJournalEntries"/>
        public Task<JToken> SearchDebtorPersonalJournalEntriesAsync(SearchRequest query = null)
        {
            return SearchDebtorPersonalJournalEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebtorPersonalJournalEntries"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchDebtorPersonalJournalEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchDebtorPersonalJournalEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchDebtorPersonalJournalEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchDebtorPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchDebtorPersonalJournalEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebtorPersonalJournalEntriesWithHttpInfo"/>
        /// <param name="query">The optional structured search object, snapshotted as JSON before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchDebtorPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/openitems/debitor/list";
            var queryParams = new List<KeyValuePair<string, string>>();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchDebtorPersonalJournalEntries", response, ExceptionFactory);
        }

    }
}
