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
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Deletes explicitly selected outgoing-invoice positions using original import XML.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten) and interprets the original XML/import options. All three vendor descriptions say positions are updated and define no success schema; exact delete/update-all/selected semantics require live acceptance. HTTP 200 may contain errors; callers inspect the complete result. No XML rewriting, automatic posting, PDF or number generation is enabled. No current invoice, position selection, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken DeleteOutgoingInvoicePositions(OutgoingInvoicePositionImportRequest request = null)
        {
            return DeleteOutgoingInvoicePositionsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="DeleteOutgoingInvoicePositions"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> DeleteOutgoingInvoicePositionsWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/deletepositions";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("DeleteOutgoingInvoicePositions", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DeleteOutgoingInvoicePositions"/>
        public Task<JToken> DeleteOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request = null)
        {
            return DeleteOutgoingInvoicePositionsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteOutgoingInvoicePositions"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> DeleteOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            return (await DeleteOutgoingInvoicePositionsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DeleteOutgoingInvoicePositionsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> DeleteOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            return DeleteOutgoingInvoicePositionsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteOutgoingInvoicePositionsWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> DeleteOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/deletepositions";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("DeleteOutgoingInvoicePositions", response, ExceptionFactory);
        }

        /// <summary>Updates all outgoing-invoice positions selected by original import XML.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten) and interprets the original XML/import options. All three vendor descriptions say positions are updated and define no success schema; exact delete/update-all/selected semantics require live acceptance. HTTP 200 may contain errors; callers inspect the complete result. No XML rewriting, automatic posting, PDF or number generation is enabled. No current invoice, position selection, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdateAllOutgoingInvoicePositions(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateAllOutgoingInvoicePositionsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="UpdateAllOutgoingInvoicePositions"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> UpdateAllOutgoingInvoicePositionsWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/updateallpositions";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateAllOutgoingInvoicePositions", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateAllOutgoingInvoicePositions"/>
        public Task<JToken> UpdateAllOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateAllOutgoingInvoicePositionsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateAllOutgoingInvoicePositions"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> UpdateAllOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateAllOutgoingInvoicePositionsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateAllOutgoingInvoicePositionsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdateAllOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateAllOutgoingInvoicePositionsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateAllOutgoingInvoicePositionsWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> UpdateAllOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/updateallpositions";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateAllOutgoingInvoicePositions", response, ExceptionFactory);
        }

        /// <summary>Updates explicitly selected outgoing-invoice positions using original import XML.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten) and interprets the original XML/import options. All three vendor descriptions say positions are updated and define no success schema; exact delete/update-all/selected semantics require live acceptance. HTTP 200 may contain errors; callers inspect the complete result. No XML rewriting, automatic posting, PDF or number generation is enabled. No current invoice, position selection, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdateOutgoingInvoicePositions(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateOutgoingInvoicePositionsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="UpdateOutgoingInvoicePositions"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> UpdateOutgoingInvoicePositionsWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/updatepositions";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateOutgoingInvoicePositions", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoicePositions"/>
        public Task<JToken> UpdateOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateOutgoingInvoicePositionsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoicePositions"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> UpdateOutgoingInvoicePositionsAsync(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateOutgoingInvoicePositionsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateOutgoingInvoicePositionsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdateOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request = null)
        {
            return UpdateOutgoingInvoicePositionsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoicePositionsWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> UpdateOutgoingInvoicePositionsAsyncWithHttpInfo(OutgoingInvoicePositionImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOutgoingPositionImport(request);
            var localVarPath = "/outgoinginvoices/updatepositions";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateOutgoingInvoicePositions", response, ExceptionFactory);
        }

        private static string SnapshotOutgoingPositionImport(OutgoingInvoicePositionImportRequest request)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            if (snapshot["data"] == null || snapshot["data"].Type != JTokenType.String)
                throw new ArgumentException("The data field is required and must be an original XML string.", nameof(request));
            var template = snapshot["template"];
            if (template != null && template.Type != JTokenType.Null && template.Type != JTokenType.String)
                throw new ArgumentException("The template field must be a string or null.", nameof(request));
            foreach (var name in new[] { "generateDocumentNumbers", "doPost", "skipDuplicates", "createPdf", "copyProductToPosition", "copyProductToPositionOverwriteMode", "copyImpersonalAccountFieldsToPosition", "copyVatKeyAndTaxRateToPosition" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.Null && snapshot[name].Type != JTokenType.Boolean)
                    throw new ArgumentException("The " + name + " field must be a boolean or null.", nameof(request));
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
