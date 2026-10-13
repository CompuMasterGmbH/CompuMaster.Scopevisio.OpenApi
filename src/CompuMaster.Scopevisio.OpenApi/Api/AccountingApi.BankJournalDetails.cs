using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Retrieves the complete JSON bank connections of a selected debtor account.</summary>
        /// <param name="accountNumber">The explicitly selected original debtor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Stammdaten, Debitorenstamm (Anzeigen) profiles. No fixed success schema or exhaustive GET error-status list is declared. Original account/document selections and field choices remain unchanged; no bank, date, identity or permission conversion is performed. Historical methods, configuration and token/account defaults remain intact. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The account or document selector is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetBankConnectionsForDebitorDetails(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForDebitorDetailsWithHttpInfo(accountNumber, fields).Data;
        }

        /// <inheritdoc cref="GetBankConnectionsForDebitorDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetBankConnectionsForDebitorDetailsWithHttpInfo(string accountNumber, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber == "." || accountNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(accountNumber));
            var localVarPath = "/debitoraccounts/{accountNumber}/bankConnections";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetBankConnectionsForDebitorDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetBankConnectionsForDebitorDetails"/>
        public Task<JToken> GetBankConnectionsForDebitorDetailsAsync(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForDebitorDetailsAsync(accountNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBankConnectionsForDebitorDetails"/>
        /// <param name="accountNumber">The explicitly selected original debtor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetBankConnectionsForDebitorDetailsAsync(string accountNumber, string fields, CancellationToken cancellationToken)
        {
            return (await GetBankConnectionsForDebitorDetailsAsyncWithHttpInfo(accountNumber, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetBankConnectionsForDebitorDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetBankConnectionsForDebitorDetailsAsyncWithHttpInfo(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForDebitorDetailsAsyncWithHttpInfo(accountNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBankConnectionsForDebitorDetailsWithHttpInfo"/>
        /// <param name="accountNumber">The explicitly selected original debtor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetBankConnectionsForDebitorDetailsAsyncWithHttpInfo(string accountNumber, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber == "." || accountNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(accountNumber));
            var localVarPath = "/debitoraccounts/{accountNumber}/bankConnections";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetBankConnectionsForDebitorDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON bank connections of a selected creditor account.</summary>
        /// <param name="accountNumber">The explicitly selected original creditor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Stammdaten, Kreditorenstamm (Anzeigen) profiles. No fixed success schema or exhaustive GET error-status list is declared. Original account/document selections and field choices remain unchanged; no bank, date, identity or permission conversion is performed. Historical methods, configuration and token/account defaults remain intact. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The account or document selector is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetBankConnectionsForKreditorDetails(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForKreditorDetailsWithHttpInfo(accountNumber, fields).Data;
        }

        /// <inheritdoc cref="GetBankConnectionsForKreditorDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetBankConnectionsForKreditorDetailsWithHttpInfo(string accountNumber, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber == "." || accountNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(accountNumber));
            var localVarPath = "/kreditoraccounts/{accountNumber}/bankConnections";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetBankConnectionsForKreditorDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetBankConnectionsForKreditorDetails"/>
        public Task<JToken> GetBankConnectionsForKreditorDetailsAsync(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForKreditorDetailsAsync(accountNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBankConnectionsForKreditorDetails"/>
        /// <param name="accountNumber">The explicitly selected original creditor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetBankConnectionsForKreditorDetailsAsync(string accountNumber, string fields, CancellationToken cancellationToken)
        {
            return (await GetBankConnectionsForKreditorDetailsAsyncWithHttpInfo(accountNumber, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetBankConnectionsForKreditorDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetBankConnectionsForKreditorDetailsAsyncWithHttpInfo(string accountNumber, string fields = null)
        {
            return GetBankConnectionsForKreditorDetailsAsyncWithHttpInfo(accountNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetBankConnectionsForKreditorDetailsWithHttpInfo"/>
        /// <param name="accountNumber">The explicitly selected original creditor account number, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetBankConnectionsForKreditorDetailsAsyncWithHttpInfo(string accountNumber, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber == "." || accountNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(accountNumber));
            var localVarPath = "/kreditoraccounts/{accountNumber}/bankConnections";
            localVarPath = localVarPath.Replace("{accountNumber}", Uri.EscapeDataString(accountNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetBankConnectionsForKreditorDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON posting rows for an explicitly selected journal document number.</summary>
        /// <param name="documentNumber">The explicitly selected original posting document-number string, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Journal (Anzeigen) profiles. No fixed success schema or exhaustive GET error-status list is declared. Original account/document selections and field choices remain unchanged; no bank, date, identity or permission conversion is performed. Historical methods, configuration and token/account defaults remain intact. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The account or document selector is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetJournalEntryByDocumentNumberDetails(string documentNumber, string fields = null)
        {
            return GetJournalEntryByDocumentNumberDetailsWithHttpInfo(documentNumber, fields).Data;
        }

        /// <inheritdoc cref="GetJournalEntryByDocumentNumberDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetJournalEntryByDocumentNumberDetailsWithHttpInfo(string documentNumber, string fields = null)
        {
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber == "." || documentNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(documentNumber));
            var localVarPath = "/journal/{documentNumber}";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetJournalEntryByDocumentNumberDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetJournalEntryByDocumentNumberDetails"/>
        public Task<JToken> GetJournalEntryByDocumentNumberDetailsAsync(string documentNumber, string fields = null)
        {
            return GetJournalEntryByDocumentNumberDetailsAsync(documentNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetJournalEntryByDocumentNumberDetails"/>
        /// <param name="documentNumber">The explicitly selected original posting document-number string, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetJournalEntryByDocumentNumberDetailsAsync(string documentNumber, string fields, CancellationToken cancellationToken)
        {
            return (await GetJournalEntryByDocumentNumberDetailsAsyncWithHttpInfo(documentNumber, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetJournalEntryByDocumentNumberDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetJournalEntryByDocumentNumberDetailsAsyncWithHttpInfo(string documentNumber, string fields = null)
        {
            return GetJournalEntryByDocumentNumberDetailsAsyncWithHttpInfo(documentNumber, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetJournalEntryByDocumentNumberDetailsWithHttpInfo"/>
        /// <param name="documentNumber">The explicitly selected original posting document-number string, escaped without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetJournalEntryByDocumentNumberDetailsAsyncWithHttpInfo(string documentNumber, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber == "." || documentNumber == "..")
                throw new ArgumentException("An explicit account or document selector is required.", nameof(documentNumber));
            var localVarPath = "/journal/{documentNumber}";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetJournalEntryByDocumentNumberDetails", response, ExceptionFactory);
        }

    }
}
