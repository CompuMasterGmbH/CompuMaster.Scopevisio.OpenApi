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
    public partial class AccountingApi
    {
        /// <summary>Replaces the entire current organisation VAT matrix with explicitly supplied entries.</summary>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <returns>The complete JSON operation result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed response schema.</returns>
        /// <remarks>This POST replaces all existing entries. An explicit empty array remains empty and may remove the existing matrix. The SDK never chooses or reconstructs a complete matrix automatically. The manufacturer lists Stammdaten, Steuermatrix (Anzeigen) for writes; actual privilege/account/rate semantics remain server-authoritative. No new retry or automatic follow-up is added. Cancellation after sending does not establish server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">An entry or known field has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ReplaceVatMatrixEntries(JArray entries = null)
        {
            return ReplaceVatMatrixEntriesWithHttpInfo(entries).Data;
        }

        /// <inheritdoc cref="ReplaceVatMatrixEntries"/>
        /// <returns>The status, response/content headers, exact raw response and complete JSON result.</returns>
        public ApiResponse<JToken> ReplaceVatMatrixEntriesWithHttpInfo(JArray entries = null)
        {
            var body = SnapshotVatMatrixWrite(entries);
            var localVarPath = "/vatmatrixentries";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ReplaceVatMatrixEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ReplaceVatMatrixEntries"/>
        public Task<JToken> ReplaceVatMatrixEntriesAsync(JArray entries = null)
        {
            return ReplaceVatMatrixEntriesAsync(entries, CancellationToken.None);
        }

        /// <inheritdoc cref="ReplaceVatMatrixEntries"/>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected change.</exception>
        public async Task<JToken> ReplaceVatMatrixEntriesAsync(JArray entries, CancellationToken cancellationToken)
        {
            return (await ReplaceVatMatrixEntriesAsyncWithHttpInfo(entries, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ReplaceVatMatrixEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ReplaceVatMatrixEntriesAsyncWithHttpInfo(JArray entries = null)
        {
            return ReplaceVatMatrixEntriesAsyncWithHttpInfo(entries, CancellationToken.None);
        }

        /// <inheritdoc cref="ReplaceVatMatrixEntriesWithHttpInfo"/>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected change.</exception>
        public async Task<ApiResponse<JToken>> ReplaceVatMatrixEntriesAsyncWithHttpInfo(JArray entries, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotVatMatrixWrite(entries);
            var localVarPath = "/vatmatrixentries";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ReplaceVatMatrixEntries", response, ExceptionFactory);
        }

        /// <summary>Updates explicitly selected VAT matrix entries while retaining existing entries.</summary>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <returns>The complete JSON operation result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed response schema.</returns>
        /// <remarks>This PATCH matches supplied entry identities and maintains existing entries according to the server contract. It is not a client-side merge or JSON Patch document; no IDs are inferred or existing matrix fetched. The manufacturer lists Stammdaten, Steuermatrix (Anzeigen) for writes; actual privilege/account/rate semantics remain server-authoritative. No new retry or automatic follow-up is added. Cancellation after sending does not establish server outcome. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">An entry or known field has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdateVatMatrixEntries(JArray entries = null)
        {
            return UpdateVatMatrixEntriesWithHttpInfo(entries).Data;
        }

        /// <inheritdoc cref="UpdateVatMatrixEntries"/>
        /// <returns>The status, response/content headers, exact raw response and complete JSON result.</returns>
        public ApiResponse<JToken> UpdateVatMatrixEntriesWithHttpInfo(JArray entries = null)
        {
            var body = SnapshotVatMatrixWrite(entries);
            var localVarPath = "/vatmatrixentries";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Patch,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateVatMatrixEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateVatMatrixEntries"/>
        public Task<JToken> UpdateVatMatrixEntriesAsync(JArray entries = null)
        {
            return UpdateVatMatrixEntriesAsync(entries, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateVatMatrixEntries"/>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected change.</exception>
        public async Task<JToken> UpdateVatMatrixEntriesAsync(JArray entries, CancellationToken cancellationToken)
        {
            return (await UpdateVatMatrixEntriesAsyncWithHttpInfo(entries, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateVatMatrixEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdateVatMatrixEntriesAsyncWithHttpInfo(JArray entries = null)
        {
            return UpdateVatMatrixEntriesAsyncWithHttpInfo(entries, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateVatMatrixEntriesWithHttpInfo"/>
        /// <param name="entries">The optional complete JSON array, copied before I/O. Null omits the body; empty arrays remain explicit. Supplied entries must be objects with valid known JSON kinds.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the selected change.</exception>
        public async Task<ApiResponse<JToken>> UpdateVatMatrixEntriesAsyncWithHttpInfo(JArray entries, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotVatMatrixWrite(entries);
            var localVarPath = "/vatmatrixentries";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Patch,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateVatMatrixEntries", response, ExceptionFactory);
        }

        private static string SnapshotVatMatrixWrite(JArray entries)
        {
            if (entries == null) return null;
            var snapshot = (JArray)entries.DeepClone();
            foreach (var token in snapshot)
            {
                if (!(token is JObject entry)) throw new ArgumentException("VAT matrix entries must be JSON objects.", nameof(entries));
                var id = entry["id"];
                long identifier;
                if (id != null && id.Type != JTokenType.Null && (id.Type != JTokenType.Integer ||
                    !long.TryParse(id.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out identifier)))
                    throw new ArgumentException("The entry id must be an Int64 JSON integer or null.", nameof(entries));
                foreach (var name in new[] { "datevKey", "datevKeyReduced", "vatKey", "vatKeyDescription", "vatCode", "purchaseAccount", "salesAccount", "salesNotDueAccount", "cashDiscountAccount" })
                {
                    var value = entry[name];
                    if (value != null && value.Type != JTokenType.Null && value.Type != JTokenType.String)
                        throw new ArgumentException("The " + name + " field must be a JSON string or null.", nameof(entries));
                }
                var rate = entry["vatRate"];
                if (rate != null && rate.Type != JTokenType.Null && ((rate.Type != JTokenType.Integer && rate.Type != JTokenType.Float) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(rate.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                    throw new ArgumentException("The vatRate field must be a finite JSON number or null.", nameof(entries));
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
