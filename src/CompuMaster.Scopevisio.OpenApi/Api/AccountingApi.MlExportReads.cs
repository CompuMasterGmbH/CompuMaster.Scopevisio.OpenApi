using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
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
        /// <summary>Searches Hypatos OCR export rows and retains the complete JSON result.</summary>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. The server selects the calling organisation and orders rows by ascending identity; the caller selects afterId from an actual returned row. Inline OCR JSON remains complete. The required ML Engine Export profile, server visibility and filtering remain authoritative. No fixed success schema, automatic cursor progression/paging or tenant completeness/aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A raw cursor or bound is not an Int64 integer or JSON null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchHypatosExport(IdExportSearchRequest query = null)
        {
            return SearchHypatosExportWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchHypatosExport"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchHypatosExportWithHttpInfo(IdExportSearchRequest query = null)
        {
            var localVarPath = "/mlexport/hypatos";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchHypatosExport", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchHypatosExport"/>
        public Task<JToken> SearchHypatosExportAsync(IdExportSearchRequest query = null)
        {
            return SearchHypatosExportAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchHypatosExport"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchHypatosExportAsync(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchHypatosExportAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchHypatosExportWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchHypatosExportAsyncWithHttpInfo(IdExportSearchRequest query = null)
        {
            return SearchHypatosExportAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchHypatosExportWithHttpInfo"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchHypatosExportAsyncWithHttpInfo(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/mlexport/hypatos";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchHypatosExport", response, ExceptionFactory);
        }

        /// <summary>Searches incoming-invoice booking-label export rows and retains the complete JSON result.</summary>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. The server selects the calling organisation and invoices with nonempty posting XML, ordered by ascending identity; the caller selects afterId from an actual returned row. Raw posting XML strings remain unchanged. The required ML Engine Export profile, server visibility and filtering remain authoritative. No fixed success schema, automatic cursor progression/paging or tenant completeness/aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A raw cursor or bound is not an Int64 integer or JSON null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchInvoiceExport(IdExportSearchRequest query = null)
        {
            return SearchInvoiceExportWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchInvoiceExport"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchInvoiceExportWithHttpInfo(IdExportSearchRequest query = null)
        {
            var localVarPath = "/mlexport/invoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchInvoiceExport", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchInvoiceExport"/>
        public Task<JToken> SearchInvoiceExportAsync(IdExportSearchRequest query = null)
        {
            return SearchInvoiceExportAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchInvoiceExport"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchInvoiceExportAsync(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchInvoiceExportAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchInvoiceExportWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchInvoiceExportAsyncWithHttpInfo(IdExportSearchRequest query = null)
        {
            return SearchInvoiceExportAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchInvoiceExportWithHttpInfo"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchInvoiceExportAsyncWithHttpInfo(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/mlexport/invoice";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchInvoiceExport", response, ExceptionFactory);
        }

        /// <summary>Searches general-journal export rows and retains the complete JSON result.</summary>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. The server selects the calling organisation and orders rows by ascending row number; the caller selects afterRowNumber from an actual returned row. Optional from/to bounds are inclusive epoch milliseconds; the client performs no date/timezone conversion or range ordering. Decimal and timestamp strings remain strings. The required ML Engine Export profile, server visibility and filtering remain authoritative. No fixed success schema, automatic cursor progression/paging or tenant completeness/aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A raw cursor or bound is not an Int64 integer or JSON null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchJournalExport(JournalExportSearchRequest query = null)
        {
            return SearchJournalExportWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchJournalExport"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchJournalExportWithHttpInfo(JournalExportSearchRequest query = null)
        {
            var localVarPath = "/mlexport/journal";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchJournalExport", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchJournalExport"/>
        public Task<JToken> SearchJournalExportAsync(JournalExportSearchRequest query = null)
        {
            return SearchJournalExportAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchJournalExport"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchJournalExportAsync(JournalExportSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchJournalExportAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchJournalExportWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchJournalExportAsyncWithHttpInfo(JournalExportSearchRequest query = null)
        {
            return SearchJournalExportAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchJournalExportWithHttpInfo"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchJournalExportAsyncWithHttpInfo(JournalExportSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/mlexport/journal";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchJournalExport", response, ExceptionFactory);
        }

        /// <summary>Searches creditor and debtor master export rows and retains the complete JSON result.</summary>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <returns>The complete JSON result, or null for an empty response body.</returns>
        /// <remarks>The manufacturer declares the search body as a string schema but links structured-object search documentation; the client sends one JSON object without quoting it twice. The server selects the calling organisation and orders rows by ascending identity; the caller selects afterId from an actual returned row. Account type codes and future projection values remain unchanged. The required ML Engine Export profile, server visibility and filtering remain authoritative. No fixed success schema, automatic cursor progression/paging or tenant completeness/aggregate totals are inferred. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Date-like strings and numeric timestamps remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A raw cursor or bound is not an Int64 integer or JSON null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        public JToken SearchPersonalAccountExport(IdExportSearchRequest query = null)
        {
            return SearchPersonalAccountExportWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchPersonalAccountExport"/>
        /// <returns>The status, headers, exact raw response and parsed JSON.</returns>
        public ApiResponse<JToken> SearchPersonalAccountExportWithHttpInfo(IdExportSearchRequest query = null)
        {
            var localVarPath = "/mlexport/personal-account";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchPersonalAccountExport", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchPersonalAccountExport"/>
        public Task<JToken> SearchPersonalAccountExportAsync(IdExportSearchRequest query = null)
        {
            return SearchPersonalAccountExportAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchPersonalAccountExport"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchPersonalAccountExportAsync(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchPersonalAccountExportAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchPersonalAccountExportWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchPersonalAccountExportAsyncWithHttpInfo(IdExportSearchRequest query = null)
        {
            return SearchPersonalAccountExportAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchPersonalAccountExportWithHttpInfo"/>
        /// <param name="query">The optional extensible export search with caller-selected cursor/bounds, snapshotted before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchPersonalAccountExportAsyncWithHttpInfo(IdExportSearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/mlexport/personal-account";
            var queryParams = new List<KeyValuePair<string, string>>();
            query?.ValidateForSend();
            var body = query?.ToString(Formatting.None);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchPersonalAccountExport", response, ExceptionFactory);
        }

    }
}
