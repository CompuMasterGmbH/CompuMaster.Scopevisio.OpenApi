using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AccountingApi
    {
        /// <summary>Creates explicitly supplied product revenue-account assignments.</summary>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <returns>The complete JSON result, or null for an empty body; inspect actual feedback rather than inferring creation from an absent result field.</returns>
        /// <remarks>The server enforces Produktverwaltung (Bearbeiten), products/accounts and date/catalog rules. Default-account enabling does not delete specific assignments. The response references MaterialResponse and copied purchase-order prose; no purchase order or aggregate success is inferred. Original conflicting date and inherit casing strings remain server-authoritative. No new mutation retry or follow-up is added; cancellation after sending cannot establish server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required nested field is absent, a known kind/width is invalid or a statistics amount is zero/nonfinite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateProductRevenueAccounts(ProductRevenueAccountsCreationRequest request)
        {
            return CreateProductRevenueAccountsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateProductRevenueAccounts"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CreateProductRevenueAccountsWithHttpInfo(ProductRevenueAccountsCreationRequest request)
        {
            var body = SnapshotRevenueStatisticsCreation(request, true);
            var localVarPath = "/revenueaccounts/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateProductRevenueAccounts", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateProductRevenueAccounts"/>
        public Task<JToken> CreateProductRevenueAccountsAsync(ProductRevenueAccountsCreationRequest request)
        {
            return CreateProductRevenueAccountsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductRevenueAccounts"/>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all selected records.</exception>
        public async Task<JToken> CreateProductRevenueAccountsAsync(ProductRevenueAccountsCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateProductRevenueAccountsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateProductRevenueAccountsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateProductRevenueAccountsAsyncWithHttpInfo(ProductRevenueAccountsCreationRequest request)
        {
            return CreateProductRevenueAccountsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateProductRevenueAccountsWithHttpInfo"/>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all selected records.</exception>
        public async Task<ApiResponse<JToken>> CreateProductRevenueAccountsAsyncWithHttpInfo(ProductRevenueAccountsCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotRevenueStatisticsCreation(request, true);
            var localVarPath = "/revenueaccounts/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateProductRevenueAccounts", response, ExceptionFactory);
        }

        /// <summary>Creates explicitly supplied statistics postings with caller-selected account options.</summary>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <returns>The complete JSON result, or null for an empty body; inspect actual feedback rather than inferring creation from an absent result field.</returns>
        /// <remarks>The server enforces Statistikbuchungen (Bearbeiten), accounts, zone-dependent requirements and dimension validity. Automatic account creation is never enabled by the SDK. The manufacturer declares HTTP 200 for all postings and 201 for a posting; original status/content remain distinct without a fabricated completeness count. No new mutation retry or follow-up is added; cancellation after sending cannot establish server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required nested field is absent, a known kind/width is invalid or a statistics amount is zero/nonfinite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateStatisticsPostings(StatisticsPostingsCreationRequest request)
        {
            return CreateStatisticsPostingsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateStatisticsPostings"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CreateStatisticsPostingsWithHttpInfo(StatisticsPostingsCreationRequest request)
        {
            var body = SnapshotRevenueStatisticsCreation(request, false);
            var localVarPath = "/statisticspostings/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateStatisticsPostings", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateStatisticsPostings"/>
        public Task<JToken> CreateStatisticsPostingsAsync(StatisticsPostingsCreationRequest request)
        {
            return CreateStatisticsPostingsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateStatisticsPostings"/>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all selected records.</exception>
        public async Task<JToken> CreateStatisticsPostingsAsync(StatisticsPostingsCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateStatisticsPostingsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateStatisticsPostingsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateStatisticsPostingsAsyncWithHttpInfo(StatisticsPostingsCreationRequest request)
        {
            return CreateStatisticsPostingsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateStatisticsPostingsWithHttpInfo"/>
        /// <param name="request">The required complete creation body, deeply copied before HTTP I/O. The root schemas require no individual properties or minimum entry count.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all selected records.</exception>
        public async Task<ApiResponse<JToken>> CreateStatisticsPostingsAsyncWithHttpInfo(StatisticsPostingsCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotRevenueStatisticsCreation(request, false);
            var localVarPath = "/statisticspostings/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateStatisticsPostings", response, ExceptionFactory);
        }

        private static string SnapshotRevenueStatisticsCreation(JObject request, bool revenue)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            if (!revenue)
            {
                ValidateRevenueStatisticsKind(snapshot["autoCreateAccount"], "autoCreateAccount", JTokenType.Boolean);
                ValidateRevenueStatisticsInteger(snapshot["autoAccountDecimal"], "autoAccountDecimal", false);
            }
            var key = revenue ? "entries" : "statisticsPostings";
            var rows = ReadRevenueStatisticsArray(snapshot[key], key);
            if (rows != null) foreach (var token in rows)
            {
                if (!(token is JObject row)) throw new ArgumentException("Creation entries must be JSON objects.", nameof(request));
                if (revenue)
                {
                    RequireRevenueStatisticsString(row["productNumber"], "productNumber");
                    ValidateRevenueStatisticsKind(row["useDefaultAccounts"], "useDefaultAccounts", JTokenType.String);
                    var accounts = ReadRevenueStatisticsArray(row["accounts"], "accounts");
                    if (accounts != null) foreach (var accountToken in accounts)
                    {
                        if (!(accountToken is JObject account)) throw new ArgumentException("Revenue account assignments must be JSON objects.", nameof(request));
                        RequireRevenueStatisticsString(account["accountNumber"], "accountNumber");
                        ValidateRevenueStatisticsInteger(account["taxCaseId"], "taxCaseId", false);
                        foreach (var name in new[] { "taxCase", "countryIsoCode", "validFrom", "validTo" })
                            ValidateRevenueStatisticsKind(account[name], name, JTokenType.String);
                    }
                }
                else
                {
                    RequireRevenueStatisticsString(row["accountNumber"], "accountNumber");
                    ValidateRevenueStatisticsInteger(row["date"], "date", true);
                    foreach (var name in new[] { "text", "zoneNumber" }) ValidateRevenueStatisticsKind(row[name], name, JTokenType.String);
                    for (var dimension = 1; dimension <= 10; dimension++)
                    {
                        var name = "dimension_" + dimension.ToString(CultureInfo.InvariantCulture);
                        ValidateRevenueStatisticsInteger(row[name], name, false);
                    }
                    var amount = row["amount"];
                    var number = amount?.ToString(Newtonsoft.Json.Formatting.None);
                    if (amount == null || (amount.Type != JTokenType.Integer && amount.Type != JTokenType.Float) ||
                        !System.Text.RegularExpressions.Regex.IsMatch(number ?? "", @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$") ||
                        !number.Split('e', 'E')[0].Any(character => character >= '1' && character <= '9'))
                        throw new ArgumentException("The statistics amount must be a finite nonzero JSON number.", nameof(request));
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static JArray ReadRevenueStatisticsArray(JToken token, string name)
        {
            if (token == null || token.Type == JTokenType.Null) return null;
            if (!(token is JArray array)) throw new ArgumentException("The " + name + " field must be a JSON array or null.", "request");
            return array;
        }

        private static void RequireRevenueStatisticsString(JToken token, string name)
        {
            if (token == null || token.Type != JTokenType.String) throw new ArgumentException("The " + name + " field is required and must be a JSON string.", "request");
        }

        private static void ValidateRevenueStatisticsKind(JToken token, string name, JTokenType kind)
        {
            if (token != null && token.Type != JTokenType.Null && token.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateRevenueStatisticsInteger(JToken token, string name, bool required)
        {
            if (!required && (token == null || token.Type == JTokenType.Null)) return;
            long value;
            if (token == null || token.Type != JTokenType.Integer || !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                throw new ArgumentException("The " + name + " field must be an Int64 JSON integer.", "request");
        }
    }
}
