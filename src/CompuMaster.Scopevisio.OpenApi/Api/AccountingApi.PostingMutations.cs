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
        /// <summary>Corrects existing postings using explicitly supplied XML.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Buchen (Anzeigen), XML import options and cancellation targets. Original XML is sent unchanged; no local XML parser or posting reconstruction is used. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CorrectPostingXml(PostingCorrectionXmlRequest request = null)
        {
            return CorrectPostingXmlWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CorrectPostingXml"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CorrectPostingXmlWithHttpInfo(PostingCorrectionXmlRequest request = null)
        {
            var body = SnapshotPostingMutation(request, 0);
            var localVarPath = "/correctpostings";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CorrectPostingXml", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CorrectPostingXml"/>
        public Task<JToken> CorrectPostingXmlAsync(PostingCorrectionXmlRequest request = null)
        {
            return CorrectPostingXmlAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CorrectPostingXml"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> CorrectPostingXmlAsync(PostingCorrectionXmlRequest request, CancellationToken cancellationToken)
        {
            return (await CorrectPostingXmlAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CorrectPostingXmlWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CorrectPostingXmlAsyncWithHttpInfo(PostingCorrectionXmlRequest request = null)
        {
            return CorrectPostingXmlAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CorrectPostingXmlWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> CorrectPostingXmlAsyncWithHttpInfo(PostingCorrectionXmlRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPostingMutation(request, 0);
            var localVarPath = "/correctpostings";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CorrectPostingXml", response, ExceptionFactory);
        }

        /// <summary>Cancels an explicitly selected posting.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Bearbeiten). Original documentNumber takes server precedence over pdeRowNumber. An omitted cancellationDate requests the documented real cancellation on the original date. HTTP 200 can contain errors or an ignored cancellation; callers inspect the complete result. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CancelPosting(PostingCancellationRequest request = null)
        {
            return CancelPostingWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CancelPosting"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CancelPostingWithHttpInfo(PostingCancellationRequest request = null)
        {
            var body = SnapshotPostingMutation(request, 1);
            var localVarPath = "/posting/cancel";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CancelPosting", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CancelPosting"/>
        public Task<JToken> CancelPostingAsync(PostingCancellationRequest request = null)
        {
            return CancelPostingAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CancelPosting"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> CancelPostingAsync(PostingCancellationRequest request, CancellationToken cancellationToken)
        {
            return (await CancelPostingAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CancelPostingWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CancelPostingAsyncWithHttpInfo(PostingCancellationRequest request = null)
        {
            return CancelPostingAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CancelPostingWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> CancelPostingAsyncWithHttpInfo(PostingCancellationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPostingMutation(request, 1);
            var localVarPath = "/posting/cancel";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CancelPosting", response, ExceptionFactory);
        }

        /// <summary>Imports explicitly supplied posting correction rows.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with original status and string insertCount/updateCount fields; callers inspect the complete result and error feedback.</returns>
        /// <remarks>The server enforces Datenimport (Bearbeiten), at least two posting rows, chart-specific accounts, personal-account summary accounts and balancing. No SKR03 sample account, automatic VAT/tax choice, row balancing or timestamp conversion is introduced. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ImportPostingCorrections(PostingCorrectionImportRequest request = null)
        {
            return ImportPostingCorrectionsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ImportPostingCorrections"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> ImportPostingCorrectionsWithHttpInfo(PostingCorrectionImportRequest request = null)
        {
            var body = SnapshotPostingMutation(request, 2);
            var localVarPath = "/postings/correction";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ImportPostingCorrections", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ImportPostingCorrections"/>
        public Task<JToken> ImportPostingCorrectionsAsync(PostingCorrectionImportRequest request = null)
        {
            return ImportPostingCorrectionsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportPostingCorrections"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> ImportPostingCorrectionsAsync(PostingCorrectionImportRequest request, CancellationToken cancellationToken)
        {
            return (await ImportPostingCorrectionsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ImportPostingCorrectionsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ImportPostingCorrectionsAsyncWithHttpInfo(PostingCorrectionImportRequest request = null)
        {
            return ImportPostingCorrectionsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ImportPostingCorrectionsWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> ImportPostingCorrectionsAsyncWithHttpInfo(PostingCorrectionImportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPostingMutation(request, 2);
            var localVarPath = "/postings/correction";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ImportPostingCorrections", response, ExceptionFactory);
        }

        /// <summary>Updates explicitly selected posting document numbers.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Buchen (Anzeigen) and existing posting identity. Original internal/external document numbers remain distinct; no year/increment expansion or replacement number is generated. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdatePostingDocumentNumbers(PostingDocumentNumbersUpdateRequest request = null)
        {
            return UpdatePostingDocumentNumbersWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="UpdatePostingDocumentNumbers"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> UpdatePostingDocumentNumbersWithHttpInfo(PostingDocumentNumbersUpdateRequest request = null)
        {
            var body = SnapshotPostingMutation(request, 3);
            var localVarPath = "/postings/update";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdatePostingDocumentNumbers", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdatePostingDocumentNumbers"/>
        public Task<JToken> UpdatePostingDocumentNumbersAsync(PostingDocumentNumbersUpdateRequest request = null)
        {
            return UpdatePostingDocumentNumbersAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdatePostingDocumentNumbers"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> UpdatePostingDocumentNumbersAsync(PostingDocumentNumbersUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdatePostingDocumentNumbersAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdatePostingDocumentNumbersWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdatePostingDocumentNumbersAsyncWithHttpInfo(PostingDocumentNumbersUpdateRequest request = null)
        {
            return UpdatePostingDocumentNumbersAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdatePostingDocumentNumbersWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> UpdatePostingDocumentNumbersAsyncWithHttpInfo(PostingDocumentNumbersUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotPostingMutation(request, 3);
            var localVarPath = "/postings/update";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdatePostingDocumentNumbers", response, ExceptionFactory);
        }

        private static string SnapshotPostingMutation(JObject request, int kind)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            if (kind == 0)
            {
                ValidatePostingFields(snapshot,
                    new[] { "postingXml", "importMasterDataMode", "documentNumber", "cancellationDate" },
                    new[] { "generateVat", "postingBatch", "importMasterDataEnabled", "importMasterDataAccountSearchOnly", "importFilesEnabled", "generateVatCreatorInfos", "refreshPaymentTerm", "importReferenceAttributes" },
                    new[] { "pdeRowNumber" }, new string[0], new[] { "postingXml" });
            }
            else if (kind == 1)
            {
                ValidatePostingFields(snapshot, new[] { "documentNumber", "cancellationDate" }, new string[0], new[] { "pdeRowNumber" }, new string[0], new string[0]);
            }
            else if (kind == 3)
            {
                ValidatePostingFields(snapshot, new[] { "documentNumber", "externalDocumentNumber", "internalDocumentNumber" }, new string[0], new string[0], new string[0], new[] { "documentNumber" });
            }
            else
            {
                ValidatePostingFields(snapshot, new string[0], new[] { "adjustVatKey", "autoCreateTax" }, new string[0], new string[0], new string[0]);
                var rows = PostingArray(snapshot["rows"], "rows");
                if (rows != null) foreach (var token in rows)
                {
                    if (!(token is JObject row)) throw new ArgumentException("Posting rows must be JSON objects.", nameof(request));
                    ValidatePostingFields(row,
                        new[] { "documentNumber", "externalDocumentNumber", "account", "summaryAccount", "cancelDocument", "foreignCurrencyCode", "documentText", "rowText", "vatKey", "discountAccount", "paymentType", "internalDocumentNumber", "ustId" },
                        new string[0], new[] { "postingDate", "documentDate", "discountPeriod1", "discountPeriod2", "netTimeLimit" },
                        new[] { "amount", "foreignCurrencyAmount", "foreignCurrencyRate", "discountPercent1", "discountPercent2" },
                        new[] { "postingDate", "documentNumber", "account", "amount" });
                    var dimensions = PostingArray(row["dimensions"], "dimensions");
                    if (dimensions != null) foreach (var item in dimensions)
                    {
                        if (!(item is JObject dimension)) throw new ArgumentException("Posting dimensions must be JSON objects.", nameof(request));
                        ValidatePostingFields(dimension, new[] { "dimensionId", "dimensionAccountNumber" }, new string[0], new string[0], new string[0], new string[0]);
                    }
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static JArray PostingArray(JToken value, string name)
        {
            if (value == null || value.Type == JTokenType.Null) return null;
            if (!(value is JArray array)) throw new ArgumentException("The " + name + " field must be an array or null.", "request");
            return array;
        }

        private static void ValidatePostingFields(JObject value, string[] textFields, string[] flags, string[] integers, string[] numbers, string[] required)
        {
            foreach (var name in required)
                if (value[name] == null || value[name].Type == JTokenType.Null)
                    throw new ArgumentException("The " + name + " field is required.", "request");
            foreach (var name in textFields)
                if (value[name] != null && value[name].Type != JTokenType.Null && value[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be a string or null.", "request");
            foreach (var name in flags)
                if (value[name] != null && value[name].Type != JTokenType.Null && value[name].Type != JTokenType.Boolean)
                    throw new ArgumentException("The " + name + " field must be a boolean or null.", "request");
            foreach (var name in integers)
            {
                var token = value[name];
                long parsed;
                if (token != null && token.Type != JTokenType.Null && (token.Type != JTokenType.Integer ||
                    !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed)))
                    throw new ArgumentException("The " + name + " field must be an Int64 integer or null.", "request");
            }
            foreach (var name in numbers)
            {
                var token = value[name];
                if (token != null && token.Type != JTokenType.Null && ((token.Type != JTokenType.Integer && token.Type != JTokenType.Float) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(token.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                    throw new ArgumentException("The " + name + " field must be a finite JSON number or null.", "request");
            }
        }
    }
}
