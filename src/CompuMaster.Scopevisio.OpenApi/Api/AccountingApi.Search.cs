using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AccountingApi
    {
        /// <summary>Searches debtor accounts and retains the complete JSON response.</summary>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Stammdaten, Debitorenstamm (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The required search object is null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchDebitorAccounts(SearchRequest query)
        {
            return SearchDebitorAccountsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchDebitorAccounts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchDebitorAccountsWithHttpInfo(SearchRequest query)
        {
            var localVarPath = "/debitoraccounts";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchDebitorAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchDebitorAccounts"/>
        public Task<JToken> SearchDebitorAccountsAsync(SearchRequest query)
        {
            return SearchDebitorAccountsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebitorAccounts"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<JToken> SearchDebitorAccountsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchDebitorAccountsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchDebitorAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchDebitorAccountsAsyncWithHttpInfo(SearchRequest query)
        {
            return SearchDebitorAccountsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDebitorAccountsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<ApiResponse<JToken>> SearchDebitorAccountsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/debitoraccounts";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchDebitorAccounts", response, ExceptionFactory);
        }


        /// <summary>Searches creditor accounts and retains the complete JSON response.</summary>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Stammdaten, Kreditorenstamm (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The required search object is null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchKreditorAccounts(SearchRequest query)
        {
            return SearchKreditorAccountsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchKreditorAccounts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchKreditorAccountsWithHttpInfo(SearchRequest query)
        {
            var localVarPath = "/kreditoraccounts";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchKreditorAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchKreditorAccounts"/>
        public Task<JToken> SearchKreditorAccountsAsync(SearchRequest query)
        {
            return SearchKreditorAccountsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchKreditorAccounts"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<JToken> SearchKreditorAccountsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchKreditorAccountsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchKreditorAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchKreditorAccountsAsyncWithHttpInfo(SearchRequest query)
        {
            return SearchKreditorAccountsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchKreditorAccountsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<ApiResponse<JToken>> SearchKreditorAccountsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/kreditoraccounts";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchKreditorAccounts", response, ExceptionFactory);
        }


        /// <summary>Searches impersonal accounts and retains the complete JSON response.</summary>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Stammdaten, Kontenplan (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The required search object is null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchImpersonalAccounts(SearchRequest query)
        {
            return SearchImpersonalAccountsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchImpersonalAccounts"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchImpersonalAccountsWithHttpInfo(SearchRequest query)
        {
            var localVarPath = "/impersonalaccounts";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchImpersonalAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchImpersonalAccounts"/>
        public Task<JToken> SearchImpersonalAccountsAsync(SearchRequest query)
        {
            return SearchImpersonalAccountsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchImpersonalAccounts"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<JToken> SearchImpersonalAccountsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchImpersonalAccountsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchImpersonalAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchImpersonalAccountsAsyncWithHttpInfo(SearchRequest query)
        {
            return SearchImpersonalAccountsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchImpersonalAccountsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The required explicit search object; an empty object sends an explicitly unfiltered search.</param>
        public async Task<ApiResponse<JToken>> SearchImpersonalAccountsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/impersonalaccounts";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), (query ?? throw new ArgumentNullException(nameof(query))).ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchImpersonalAccounts", response, ExceptionFactory);
        }


        /// <summary>Searches general journal entries and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Journal (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Original createdSince, createdBefore, postingDateSince, postingDateBefore and externalDocumentDateSince filters can be assigned through the inherited indexer as Unix epoch milliseconds without conversion or defaults. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchJournalEntries(SearchRequest query = null)
        {
            return SearchJournalEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchJournalEntries"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchJournalEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/journal";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchJournalEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchJournalEntries"/>
        public Task<JToken> SearchJournalEntriesAsync(SearchRequest query = null)
        {
            return SearchJournalEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchJournalEntries"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchJournalEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchJournalEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchJournalEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchJournalEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchJournalEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchJournalEntriesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchJournalEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/journal";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchJournalEntries", response, ExceptionFactory);
        }


        /// <summary>Searches personal journal entries and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Journal (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Original createdSince, createdBefore, postingDateSince, postingDateBefore and externalDocumentDateSince filters can be assigned through the inherited indexer as Unix epoch milliseconds without conversion or defaults. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchPersonalJournalEntries(SearchRequest query = null)
        {
            return SearchPersonalJournalEntriesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchPersonalJournalEntries"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchPersonalJournalEntriesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/personaljournal";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchPersonalJournalEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchPersonalJournalEntries"/>
        public Task<JToken> SearchPersonalJournalEntriesAsync(SearchRequest query = null)
        {
            return SearchPersonalJournalEntriesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchPersonalJournalEntries"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchPersonalJournalEntriesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchPersonalJournalEntriesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchPersonalJournalEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchPersonalJournalEntriesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchPersonalJournalEntriesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchPersonalJournalEntriesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/personaljournal";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchPersonalJournalEntries", response, ExceptionFactory);
        }


        /// <summary>Searches dimensions and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Journal (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. A documented 404 for missing dimensions remains a server error under the original factory. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchDimensions(SearchRequest query = null)
        {
            return SearchDimensionsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchDimensions"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchDimensionsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/dimensions";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchDimensions", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchDimensions"/>
        public Task<JToken> SearchDimensionsAsync(SearchRequest query = null)
        {
            return SearchDimensionsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDimensions"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchDimensionsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchDimensionsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchDimensionsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchDimensionsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchDimensionsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchDimensionsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchDimensionsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/dimensions";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchDimensions", response, ExceptionFactory);
        }


        /// <summary>Searches invoice zones and retains the complete JSON response.</summary>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        /// <returns>The JSON result, including records, metadata or a count scalar; null for an empty response.</returns>
        /// <remarks>Requires the server's Stammdaten, Rechnungskreis (Anzeigen) profiles. Search choices remain server-authoritative without paging defaults, clamping or automatic follow-up requests. The OpenAPI labels the search schema as a string, while its description links the manufacturer's JSON-object search documentation; these structured forms follow that documented object workflow. No fixed response schema is declared by the manufacturer. Fractional values use .NET decimal precision and may round; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken SearchZones(SearchRequest query = null)
        {
            return SearchZonesWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchZones"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> SearchZonesWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/zones";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchZones", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchZones"/>
        public Task<JToken> SearchZonesAsync(SearchRequest query = null)
        {
            return SearchZonesAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchZones"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<JToken> SearchZonesAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchZonesAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchZonesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchZonesAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchZonesAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchZonesWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="query">The optional search object, or null to omit the request body.</param>
        public async Task<ApiResponse<JToken>> SearchZonesAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/zones";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), query?.ToString(Newtonsoft.Json.Formatting.None),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchZones", response, ExceptionFactory);
        }

    }
}
