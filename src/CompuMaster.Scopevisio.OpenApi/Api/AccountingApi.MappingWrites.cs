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
        /// <summary>Creates an explicitly selected account mapping.</summary>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <returns>The complete JSON result, or null for an empty response. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>Creation can report a conflict when a mapping already exists. Omitted accountingArea retains the server's active-area selection. The server enforces Stammdaten (Bearbeiten), account/chart choices and operation validity. No mutation retry or automatic follow-up is added; cancellation after sending cannot establish the server outcome. Unknown fields, raw JSON and response/content headers remain available. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied body lacks a required field or contains an incompatible known JSON kind/integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateAccountMapping(AccountMappingCreationRequest request = null)
        {
            return CreateAccountMappingWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateAccountMapping"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<JToken> CreateAccountMappingWithHttpInfo(AccountMappingCreationRequest request = null)
        {
            var body = SnapshotAccountMappingWrite(request, false);
            var localVarPath = "/accountmapping";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateAccountMapping", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateAccountMapping"/>
        public Task<JToken> CreateAccountMappingAsync(AccountMappingCreationRequest request = null)
        {
            return CreateAccountMappingAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateAccountMapping"/>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<JToken> CreateAccountMappingAsync(AccountMappingCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateAccountMappingAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateAccountMappingWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateAccountMappingAsyncWithHttpInfo(AccountMappingCreationRequest request = null)
        {
            return CreateAccountMappingAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateAccountMappingWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> CreateAccountMappingAsyncWithHttpInfo(AccountMappingCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotAccountMappingWrite(request, false);
            var localVarPath = "/accountmapping";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateAccountMapping", response, ExceptionFactory);
        }

        /// <summary>Updates one explicitly selected account mapping entry.</summary>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <returns>The complete JSON result, or null for an empty response. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The entry identity is selected by the path; no mapping direction, account existence or automatic matching is inferred. The server enforces Stammdaten (Bearbeiten), account/chart choices and operation validity. No mutation retry or automatic follow-up is added; cancellation after sending cannot establish the server outcome. Unknown fields, raw JSON and response/content headers remain available. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied body lacks a required field or contains an incompatible known JSON kind/integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdateAccountMappingEntry(long id, AccountMappingEntryUpdateRequest request = null)
        {
            return UpdateAccountMappingEntryWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateAccountMappingEntry"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<JToken> UpdateAccountMappingEntryWithHttpInfo(long id, AccountMappingEntryUpdateRequest request = null)
        {
            var body = SnapshotAccountMappingWrite(request, true);
            var localVarPath = "/accountmapping/entry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateAccountMappingEntry", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateAccountMappingEntry"/>
        public Task<JToken> UpdateAccountMappingEntryAsync(long id, AccountMappingEntryUpdateRequest request = null)
        {
            return UpdateAccountMappingEntryAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateAccountMappingEntry"/>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<JToken> UpdateAccountMappingEntryAsync(long id, AccountMappingEntryUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateAccountMappingEntryAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateAccountMappingEntryWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdateAccountMappingEntryAsyncWithHttpInfo(long id, AccountMappingEntryUpdateRequest request = null)
        {
            return UpdateAccountMappingEntryAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateAccountMappingEntryWithHttpInfo"/>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <param name="request">The optional complete JSON body, copied before HTTP I/O; null omits the body while a supplied body must satisfy its required fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> UpdateAccountMappingEntryAsyncWithHttpInfo(long id, AccountMappingEntryUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotAccountMappingWrite(request, true);
            var localVarPath = "/accountmapping/entry/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateAccountMappingEntry", response, ExceptionFactory);
        }

        /// <summary>Deletes one explicitly selected account mapping including all its entries.</summary>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <returns>The complete JSON result, or null for an empty response. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>Deletion removes the complete selected mapping and its entries; the SDK never chooses the active mapping or lists/deletes other mappings. The server enforces Stammdaten (Bearbeiten), account/chart choices and operation validity. No mutation retry or automatic follow-up is added; cancellation after sending cannot establish the server outcome. Unknown fields, raw JSON and response/content headers remain available. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken DeleteAccountMapping(long id)
        {
            return DeleteAccountMappingWithHttpInfo(id).Data;
        }

        /// <inheritdoc cref="DeleteAccountMapping"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<JToken> DeleteAccountMappingWithHttpInfo(long id)
        {
            var localVarPath = "/accountmapping/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("DeleteAccountMapping", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DeleteAccountMapping"/>
        public Task<JToken> DeleteAccountMappingAsync(long id)
        {
            return DeleteAccountMappingAsync(id, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteAccountMapping"/>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<JToken> DeleteAccountMappingAsync(long id, CancellationToken cancellationToken)
        {
            return (await DeleteAccountMappingAsyncWithHttpInfo(id, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DeleteAccountMappingWithHttpInfo"/>
        public Task<ApiResponse<JToken>> DeleteAccountMappingAsyncWithHttpInfo(long id)
        {
            return DeleteAccountMappingAsyncWithHttpInfo(id, CancellationToken.None);
        }

        /// <inheritdoc cref="DeleteAccountMappingWithHttpInfo"/>
        /// <param name="id">The explicitly selected Int64 identifier, sent unchanged using invariant text.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the mutation.</exception>
        public async Task<ApiResponse<JToken>> DeleteAccountMappingAsyncWithHttpInfo(long id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/accountmapping/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Delete,
                new List<KeyValuePair<string, string>>(), null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("DeleteAccountMapping", response, ExceptionFactory);
        }

        private static string SnapshotAccountMappingWrite(JObject request, bool entry)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            if (entry) ValidateAccountMappingEntry(snapshot);
            else
            {
                RequireAccountMappingString(snapshot["description"], "description");
                foreach (var name in new[] { "accountFormat", "accountType" }) ValidateAccountMappingKind(snapshot[name], name, JTokenType.String);
                foreach (var name in new[] { "accountingArea", "validFrom", "validTill", "glAccountLengthFrom", "glAccountLengthTo", "personalAccountLengthFrom", "personalAccountLengthTo" }) ValidateAccountMappingInteger(snapshot[name], name);
                ValidateAccountMappingKind(snapshot["includePersonalAccounts"], "includePersonalAccounts", JTokenType.Boolean);
                var entries = snapshot["entries"];
                if (entries != null && entries.Type != JTokenType.Null)
                {
                    if (!(entries is JArray array)) throw new ArgumentException("The entries field must be a JSON array or null.", "request");
                    foreach (var item in array)
                    {
                        if (!(item is JObject value)) throw new ArgumentException("Mapping entries must be JSON objects.", "request");
                        ValidateAccountMappingEntry(value);
                    }
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateAccountMappingEntry(JObject entry)
        {
            RequireAccountMappingString(entry["accountScope"], "accountScope");
            RequireAccountMappingString(entry["accountExternal"], "accountExternal");
            // Response-only fields are never populated by typed input properties.
            // Preserve caller-supplied complete JSON while checking declared integer kinds.
            ValidateAccountMappingInteger(entry["id"], "id");
            ValidateAccountMappingInteger(entry["modifiedTs"], "modifiedTs");
        }

        private static void RequireAccountMappingString(JToken token, string name)
        {
            if (token == null || token.Type != JTokenType.String)
                throw new ArgumentException("The " + name + " field is required and must be a JSON string.", "request");
        }

        private static void ValidateAccountMappingKind(JToken token, string name, JTokenType kind)
        {
            if (token != null && token.Type != JTokenType.Null && token.Type != kind)
                throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateAccountMappingInteger(JToken token, string name)
        {
            if (token == null || token.Type == JTokenType.Null) return;
            long value;
            if (token.Type != JTokenType.Integer || !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                throw new ArgumentException("The " + name + " field must be an Int64 JSON integer.", "request");
        }
    }
}
