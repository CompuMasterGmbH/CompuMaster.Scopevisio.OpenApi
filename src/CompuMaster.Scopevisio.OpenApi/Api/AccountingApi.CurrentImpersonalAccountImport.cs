using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
        /// <summary>Imports explicitly supplied impersonal accounts.</summary>
        /// <param name="request">The optional complete copied import JSON; null sends no body and no account, chart, tax, posting or dimension defaults are inserted.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten). Body and rows are optional; each supplied row requires original number, name and type strings. Chart, category, VAT and dimension selections remain server-authoritative; examples and allowable values refer to SKR03 and other charts require checking actual client settings. No account lookup, calculation, default selection, posting, extra write, account-context selection or mutation replay occurs. HTTP 200 means created or updated without a fixed schema or guaranteed per-row/aggregate success. Bulk import may change existing accounts and requires owned reversible fixtures. The complete request is copied before HTTP. Cancellation after sending cannot establish resulting account state. Parsed fractions use bounded decimal precision; raw JSON remains exact. Historical CreateImpersonalAcocunts spelling, models and defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or a supplied row lacks number, name or type.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ImportImpersonalAccountsWithResult(ImpersonalAccountImportRequest request = null)
        {
            return ImportImpersonalAccountsWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportImpersonalAccountsWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ImportImpersonalAccountsWithResultWithHttpInfo(ImpersonalAccountImportRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/createimpersonalaccounts";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportImpersonalAccountsWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportImpersonalAccountsWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ImportImpersonalAccountsWithResultAsync(ImpersonalAccountImportRequest request = null)
        {
            return ImportImpersonalAccountsWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportImpersonalAccountsWithResult"/>
        /// <param name="request">The optional complete copied import JSON; null sends no body and no account, chart, tax, posting or dimension defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created or updated one or more accounts.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ImportImpersonalAccountsWithResultAsync(ImpersonalAccountImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportImpersonalAccountsWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportImpersonalAccountsWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportImpersonalAccountsWithResultAsyncWithHttpInfo(ImpersonalAccountImportRequest request = null)
        {
            return ImportImpersonalAccountsWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportImpersonalAccountsWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied import JSON; null sends no body and no account, chart, tax, posting or dimension defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created or updated one or more accounts.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportImpersonalAccountsWithResultAsyncWithHttpInfo(ImpersonalAccountImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/createimpersonalaccounts";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportImpersonalAccountsWithResult", response, ExceptionFactory);
        }
    }
}
