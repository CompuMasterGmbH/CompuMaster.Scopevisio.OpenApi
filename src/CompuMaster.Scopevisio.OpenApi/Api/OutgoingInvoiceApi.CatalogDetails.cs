using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Retrieves the complete JSON sales-document export-template catalog.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires Faktura Stammdaten, Opportunities (Anzeigen) and declares no fixed success schema or exhaustive error-status list. The request has no query or body; no catalog defaults, format/paging choices, local associations or document generation are inferred. Existing generated methods, null-data defaults, authorization and selected account remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetOutgoingInvoiceExportTemplatesDetails()
        {
            return GetOutgoingInvoiceExportTemplatesDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceExportTemplatesDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetOutgoingInvoiceExportTemplatesDetailsWithHttpInfo()
        {
            var localVarPath = "/outgoinginvoice/exporttemplates";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetOutgoingInvoiceExportTemplatesDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceExportTemplatesDetails"/>
        public Task<JToken> GetOutgoingInvoiceExportTemplatesDetailsAsync()
        {
            return GetOutgoingInvoiceExportTemplatesDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceExportTemplatesDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetOutgoingInvoiceExportTemplatesDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetOutgoingInvoiceExportTemplatesDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceExportTemplatesDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetOutgoingInvoiceExportTemplatesDetailsAsyncWithHttpInfo()
        {
            return GetOutgoingInvoiceExportTemplatesDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceExportTemplatesDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetOutgoingInvoiceExportTemplatesDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/outgoinginvoice/exporttemplates";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetOutgoingInvoiceExportTemplatesDetails", response, ExceptionFactory);
        }

    }
}
