using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
        /// <summary>Imports explicitly supplied original XML filter data.</summary>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten), filter-name matching and XML/import validity. Omitted updateExistingFilters retains false. The manufacturer success/error wording refers to postings and zone despite a filter import; the SDK preserves original results/diagnostics without inferring a posting or aggregate success. No new mutation retry or follow-up is added. Cancellation after sending cannot establish the server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; exact raw JSON remains available.</remarks>
        /// <exception cref="ArgumentException">A supplied required field is absent or a known field has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ImportFilters(FilterImportRequest request = null)
        {
            return ImportFiltersWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportFilters"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<JToken> ImportFiltersWithHttpInfo(FilterImportRequest request = null)
        {
            var body = SnapshotAccountingConfiguration(request, true);
            var localVarPath = "/filter/import";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportFilters", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportFilters"/>
        public Task<JToken> ImportFiltersAsync(FilterImportRequest request = null)
        {
            return ImportFiltersAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportFilters"/>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected operation.</exception>
        public async Task<JToken> ImportFiltersAsync(FilterImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportFiltersAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportFiltersWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ImportFiltersAsyncWithHttpInfo(FilterImportRequest request = null)
        {
            return ImportFiltersAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportFiltersWithHttpInfo"/>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected operation.</exception>
        public async Task<ApiResponse<JToken>> ImportFiltersAsyncWithHttpInfo(FilterImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotAccountingConfiguration(request, true);
            var localVarPath = "/filter/import";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportFilters", response, ExceptionFactory);
        }

        /// <summary>Opens or closes explicitly selected fiscal-year periods or a fiscal year.</summary>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Bearbeiten), year/period existence and business constraints, including HTTP 409 conflicts. Omitted fields do not select a client-inferred active year, period or open/closed state. No current state is fetched or repaired. No new mutation retry or follow-up is added. Cancellation after sending cannot establish the server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; exact raw JSON remains available.</remarks>
        /// <exception cref="ArgumentException">A supplied required field is absent or a known field has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken EditFiscalYearState(FiscalYearStateRequest request = null)
        {
            return EditFiscalYearStateWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="EditFiscalYearState"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<JToken> EditFiscalYearStateWithHttpInfo(FiscalYearStateRequest request = null)
        {
            var body = SnapshotAccountingConfiguration(request, false);
            var localVarPath = "/fiscalyears/edit";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("EditFiscalYearState", response, ExceptionFactory);
        }

        /// <inheritdoc cref="EditFiscalYearState"/>
        public Task<JToken> EditFiscalYearStateAsync(FiscalYearStateRequest request = null)
        {
            return EditFiscalYearStateAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="EditFiscalYearState"/>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected operation.</exception>
        public async Task<JToken> EditFiscalYearStateAsync(FiscalYearStateRequest request, CancellationToken cancellationToken)
        {
            return (await EditFiscalYearStateAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="EditFiscalYearStateWithHttpInfo"/>
        public Task<ApiResponse<JToken>> EditFiscalYearStateAsyncWithHttpInfo(FiscalYearStateRequest request = null)
        {
            return EditFiscalYearStateAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="EditFiscalYearStateWithHttpInfo"/>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied fields retain original values and known kinds are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected operation.</exception>
        public async Task<ApiResponse<JToken>> EditFiscalYearStateAsyncWithHttpInfo(FiscalYearStateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotAccountingConfiguration(request, false);
            var localVarPath = "/fiscalyears/edit";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("EditFiscalYearState", response, ExceptionFactory);
        }

        private static string SnapshotAccountingConfiguration(JObject request, bool filters)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            if (filters)
            {
                var data = snapshot["data"];
                if (data == null || data.Type != JTokenType.String) throw new ArgumentException("The data field is required and must be an original XML string.", nameof(request));
                ValidateAccountingConfigurationKind(snapshot["updateExistingFilters"], "updateExistingFilters", JTokenType.Boolean);
            }
            else
            {
                ValidateAccountingConfigurationKind(snapshot["fiscalYearName"], "fiscalYearName", JTokenType.String);
                ValidateAccountingConfigurationKind(snapshot["open"], "open", JTokenType.Boolean);
                var period = snapshot["periodId"];
                int identifier;
                if (period != null && period.Type != JTokenType.Null && (period.Type != JTokenType.Integer ||
                    !int.TryParse(period.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out identifier)))
                    throw new ArgumentException("The periodId field must be an Int32 JSON integer or null.", nameof(request));
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateAccountingConfigurationKind(JToken token, string name, JTokenType kind)
        {
            if (token != null && token.Type != JTokenType.Null && token.Type != kind)
                throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }
    }
}
