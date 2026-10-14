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
        /// <summary>Creates a bank connection for an explicitly selected debitor account.</summary>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied bank-connection JSON; null sends no body and no standard or bank defaults are inserted.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces Stammdaten, Debitorenstamm (Bearbeiten), bank identification and selected-account semantics. The route body is optional and no individual fields are schema-required, although server 400 describes missing IBAN or bank account number plus bank code. Original strings and omitted/false/true standard are sent unchanged without validation repair, checksum or bank lookup, defaults, transfer/withdrawal, account-context selection or mutation replay. Setting standard may affect the server default connection; actual effects remain unverified. The complete request is copied before HTTP. Cancellation after sending cannot establish resulting connection state. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact. Legacy BankConnectionForm APIs and the historical misspelled kreditor method remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The required account selector is null.</exception>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or the account-number selector is a dot segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateDebitorBankConnectionWithResult(string accountNumber, BankConnectionWriteRequest request = null)
        {
            return CreateDebitorBankConnectionWithResultWithHttpInfo(accountNumber, request).Data;
        }

        /// <inheritdoc cref="CreateDebitorBankConnectionWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateDebitorBankConnectionWithResultWithHttpInfo(string accountNumber, BankConnectionWriteRequest request = null)
        {
            if (accountNumber == null) throw new ArgumentNullException(nameof(accountNumber));
            if (accountNumber == "." || accountNumber == "..") throw new ArgumentException("The account-number selector cannot be a path dot segment.", nameof(accountNumber));
            var body = request?.SnapshotForSend();
            var localVarPath = "/debitoraccounts/{accountNumber}/bankConnection/new";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateDebitorBankConnectionWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateDebitorBankConnectionWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateDebitorBankConnectionWithResultAsync(string accountNumber, BankConnectionWriteRequest request = null)
        {
            return CreateDebitorBankConnectionWithResultAsync(accountNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateDebitorBankConnectionWithResult"/>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied bank-connection JSON; null sends no body and no standard or bank defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added or selected the bank connection.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateDebitorBankConnectionWithResultAsync(string accountNumber, BankConnectionWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(accountNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateDebitorBankConnectionWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(string accountNumber, BankConnectionWriteRequest request = null)
        {
            return CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(accountNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateDebitorBankConnectionWithResultWithHttpInfo"/>
        /// <param name="accountNumber">The original personal-account-number string, escaped as one segment without trimming, numeric conversion, zero removal, contact-ID translation or lookup. Null and dot segments are rejected.</param>
        /// <param name="request">The optional complete copied bank-connection JSON; null sends no body and no standard or bank defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have added or selected the bank connection.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(string accountNumber, BankConnectionWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (accountNumber == null) throw new ArgumentNullException(nameof(accountNumber));
            if (accountNumber == "." || accountNumber == "..") throw new ArgumentException("The account-number selector cannot be a path dot segment.", nameof(accountNumber));
            var body = request?.SnapshotForSend();
            var localVarPath = "/debitoraccounts/{accountNumber}/bankConnection/new";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateDebitorBankConnectionWithResult", response, ExceptionFactory);
        }
    }
}
