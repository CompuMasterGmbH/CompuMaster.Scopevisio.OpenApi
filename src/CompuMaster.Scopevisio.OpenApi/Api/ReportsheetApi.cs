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
    /// <summary>Provides reportsheet reads and structured searches.</summary>
    /// <remarks>Uses the shared configuration and existing transport/exception contracts. Profiles, visibility, paging and current server acceptance remain server-enforced.</remarks>
    public partial class ReportsheetApi
    {
        private ExceptionFactory exceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;

        /// <summary>Initializes the API group with the selected shared configuration.</summary>
        /// <param name="configuration">The configuration to use; null selects Configuration.Default without altering it.</param>
        public ReportsheetApi(Configuration configuration = null)
        {
            Configuration = configuration ?? CompuMaster.Scopevisio.OpenApi.Client.Configuration.Default;
        }

        /// <summary>Initializes the API group with a separate configuration for the selected REST origin.</summary>
        /// <param name="basePath">The REST origin used by the new configuration.</param>
        public ReportsheetApi(string basePath) : this(new Configuration { BasePath = basePath }) { }

        /// <summary>Gets or sets the configuration used by the API group.</summary>
        /// <value>The configuration shared by requests, including caller-owned HTTP transport and authorization.</value>
        public Configuration Configuration { get; set; }

        /// <summary>Gets or sets the response exception factory.</summary>
        /// <value>The factory used before response parsing; null disables this hook.</value>
        /// <exception cref="InvalidOperationException">The selected factory is a multicast delegate.</exception>
        public ExceptionFactory ExceptionFactory
        {
            get
            {
                if (exceptionFactory != null && exceptionFactory.GetInvocationList().Length > 1)
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                return exceptionFactory;
            }
            set { exceptionFactory = value; }
        }

        /// <summary>Retrieves the reportsheet selected by its internal number.</summary>
        /// <param name="number">The required nonblank internal number, retained as text including leading zeros; relative path segments are invalid.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits it and an empty string is sent.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The vendor declares no fixed success schema. The internal number is not converted to an integer. Unknown fields, date-like strings and exact raw responses remain available. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Profiles, visibility and server-specific formats remain server-enforced; no completeness or extra paging is inferred.</remarks>
        /// <exception cref="ArgumentException">The required number is null, empty, blank or a relative path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible typed object/record shape or a fraction exceeding the decimal range.</exception>
        public JToken GetReportsheetByNumber(string number, string fields = null)
        {
            return GetReportsheetByNumberWithHttpInfo(number, fields).Data;
        }

        /// <inheritdoc cref="GetReportsheetByNumber"/>
        /// <returns>The status, headers, exact raw response and complete JSON.</returns>
        public ApiResponse<JToken> GetReportsheetByNumberWithHttpInfo(string number, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..") throw new ArgumentException("A nonblank internal number without relative path segments is required.", nameof(number));
            var localVarPath = "/reportsheet/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetReportsheetByNumber", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetReportsheetByNumber"/>
        public Task<JToken> GetReportsheetByNumberAsync(string number, string fields = null)
        {
            return GetReportsheetByNumberAsync(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetReportsheetByNumber"/>
        /// <param name="number">The required nonblank internal number, retained as text including leading zeros; relative path segments are invalid.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits it and an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> GetReportsheetByNumberAsync(string number, string fields, CancellationToken cancellationToken)
        {
            return (await GetReportsheetByNumberAsyncWithHttpInfo(number, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetReportsheetByNumberWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetReportsheetByNumberAsyncWithHttpInfo(string number, string fields = null)
        {
            return GetReportsheetByNumberAsyncWithHttpInfo(number, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetReportsheetByNumberWithHttpInfo"/>
        /// <param name="number">The required nonblank internal number, retained as text including leading zeros; relative path segments are invalid.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits it and an empty string is sent.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetReportsheetByNumberAsyncWithHttpInfo(string number, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..") throw new ArgumentException("A nonblank internal number without relative path segments is required.", nameof(number));
            var localVarPath = "/reportsheet/{number}";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetReportsheetByNumber", response, ExceptionFactory);
        }

        /// <summary>Searches reportsheets using a structured JSON object.</summary>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The vendor's search-body schema says string while its search documentation describes an object; this additive method sends one JSON object. Unset search properties retain server defaults, and caller-selected filters/nulls/paging/future fields are unchanged. Unknown fields, date-like strings and exact raw responses remain available. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Profiles, visibility and server-specific formats remain server-enforced; no completeness or extra paging is inferred.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible typed object/record shape or a fraction exceeding the decimal range.</exception>
        public JToken SearchReportsheets(SearchRequest query = null)
        {
            return SearchReportsheetsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchReportsheets"/>
        /// <returns>The status, headers, exact raw response and complete JSON.</returns>
        public ApiResponse<JToken> SearchReportsheetsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/reportsheets";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchReportsheets", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchReportsheets"/>
        public Task<JToken> SearchReportsheetsAsync(SearchRequest query = null)
        {
            return SearchReportsheetsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchReportsheets"/>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchReportsheetsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchReportsheetsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchReportsheetsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchReportsheetsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchReportsheetsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchReportsheetsWithHttpInfo"/>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchReportsheetsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/reportsheets";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchReportsheets", response, ExceptionFactory);
        }

    }
}
