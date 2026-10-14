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
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Imports original outgoing-document XML data.</summary>
        /// <param name="request">The optional complete copied XML import JSON; null sends no body without numbering, posting, PDF, duplicate, template, copy, overwrite or tax defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten) and actual XML/template/document/product/account/tax semantics. Body is optional; a supplied object requires original XML data. All eight nullable flags preserve omission, false and true without local defaults. Explicit options may request server document numbering, posting, PDF generation, duplicate handling or product/account/VAT copying and overwrite behavior; the client performs none of these operations. Original XML and template strings are retained without parsing, Base64/text conversion, repair, lookup or XML-domain-format inference. HTTP 200 is unspecified and directs callers to response errors; complete JSON/future errors remain available without invented invoice IDs, per-row/aggregate success or atomicity. Requests and future JSON are copied before HTTP without account-context selection, extra requests or mutation replay. Cancellation after sending cannot establish resulting documents, postings or generated files. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual XML/profile/option behavior and owned reversible cleanup remain unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or a supplied object lacks original XML data.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken ImportOutgoingInvoicesWithResult(OutgoingInvoiceImportRequest request = null)
        {
            return ImportOutgoingInvoicesWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportOutgoingInvoicesWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> ImportOutgoingInvoicesWithResultWithHttpInfo(OutgoingInvoiceImportRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/outgoinginvoices/import";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportOutgoingInvoicesWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportOutgoingInvoicesWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> ImportOutgoingInvoicesWithResultAsync(OutgoingInvoiceImportRequest request = null)
        {
            return ImportOutgoingInvoicesWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportOutgoingInvoicesWithResult"/>
        /// <param name="request">The optional complete copied XML import JSON; null sends no body without numbering, posting, PDF, duplicate, template, copy, overwrite or tax defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported documents or applied selected options.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> ImportOutgoingInvoicesWithResultAsync(OutgoingInvoiceImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportOutgoingInvoicesWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(OutgoingInvoiceImportRequest request = null)
        {
            return ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportOutgoingInvoicesWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied XML import JSON; null sends no body without numbering, posting, PDF, duplicate, template, copy, overwrite or tax defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have imported documents or applied selected options.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(OutgoingInvoiceImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/outgoinginvoices/import";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportOutgoingInvoicesWithResult", response, ExceptionFactory);
        }
    }
}
