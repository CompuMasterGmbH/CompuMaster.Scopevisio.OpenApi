using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class CashbookApi
    {
        /// <summary>Creates an explicitly selected cashbook entry and retains the complete JSON result.</summary>
        /// <param name="request">The required explicit entry selection, deeply copied and validated before HTTP admission.</param>
        /// <returns>The complete original JSON result, or null for an empty response.</returns>
        /// <remarks>The server requires Kassenbewegung eingeben (Bearbeiten). Required cashbookId and documentDate are original Int64 values; timestamps remain Unix epoch milliseconds without month conversion. Optional amounts and account/dimension choices are not defaulted, normalized or looked up. The server can create missing dimension entries even when no name is supplied; omission does not prevent creation, and the documented name default is the dimension number. The copied request prose mentions an undefined topic field; the SDK follows the declared required fields instead of inventing topic. HTTP 201 has no fixed response schema. No write is automatically retried; cancellation after sending does not establish server outcome. Parsed fractions use bounded .NET decimal precision; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required field is missing or has an invalid width/kind, a known optional kind is invalid, a number is nonfinite or the read-only internal number is supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken CreateCashbookEntryWithResult(CashbookEntryCreationRequest request)
        {
            return CreateCashbookEntryWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateCashbookEntryWithResult"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        public ApiResponse<JToken> CreateCashbookEntryWithResultWithHttpInfo(CashbookEntryCreationRequest request)
        {
            var localVarPath = "/cashbookentry/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotCashbookEntryCreation(request),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateCashbookEntryWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateCashbookEntryWithResult"/>
        public Task<JToken> CreateCashbookEntryWithResultAsync(CashbookEntryCreationRequest request)
        {
            return CreateCashbookEntryWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateCashbookEntryWithResult"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="request">The required explicit entry selection, deeply copied and validated before HTTP admission.</param>
        public async Task<JToken> CreateCashbookEntryWithResultAsync(CashbookEntryCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateCashbookEntryWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateCashbookEntryWithResultWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateCashbookEntryWithResultAsyncWithHttpInfo(CashbookEntryCreationRequest request)
        {
            return CreateCashbookEntryWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateCashbookEntryWithResultWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <param name="request">The required explicit entry selection, deeply copied and validated before HTTP admission.</param>
        public async Task<ApiResponse<JToken>> CreateCashbookEntryWithResultAsyncWithHttpInfo(CashbookEntryCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/cashbookentry/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotCashbookEntryCreation(request),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateCashbookEntryWithResult", response, ExceptionFactory);
        }


        private static string SnapshotCashbookEntryCreation(CashbookEntryCreationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            RequireCashbookInt64(snapshot, "cashbookId");
            RequireCashbookInt64(snapshot, "documentDate");
            if (snapshot.Property("internalDocumentNumber") != null)
                throw new ArgumentException("The internal document number is assigned by the server.", nameof(request));
            foreach (var name in new[] { "externalDocumentNumber", "freeText", "impersonalAccountNumber", "personalAccountNumber", "notice" })
                ValidateCashbookString(snapshot, name);
            foreach (var name in new[] { "debitAmount", "creditAmount" })
            {
                var value = snapshot[name];
                if (value != null && value.Type != JTokenType.Null && value.Type != JTokenType.Integer && value.Type != JTokenType.Float)
                    throw new ArgumentException("An explicit amount must be a JSON number.", nameof(request));
            }
            var dimensions = snapshot["dimensions"];
            if (dimensions != null && dimensions.Type != JTokenType.Null)
            {
                if (!(dimensions is JArray entries)) throw new ArgumentException("Dimensions must be an array.", nameof(request));
                foreach (var token in entries)
                {
                    if (!(token is JObject entry)) throw new ArgumentException("A dimension entry must be an object.", nameof(request));
                    RequireCashbookInt64(entry, "dimensionNumber");
                    RequireCashbookInt64(entry, "dimensionEntryNumber");
                    ValidateCashbookString(entry, "dimensionEntryName");
                }
            }
            foreach (var value in snapshot.DescendantsAndSelf().OfType<JValue>())
            {
                if ((value.Value is double d && (double.IsNaN(d) || double.IsInfinity(d))) ||
                    (value.Value is float f && (float.IsNaN(f) || float.IsInfinity(f))))
                    throw new ArgumentException("JSON numbers must be finite.", nameof(request));
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void RequireCashbookInt64(JObject value, string name)
        {
            var token = value[name];
            if (token == null || token.Type != JTokenType.Integer ||
                !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture, out _))
                throw new ArgumentException("The required " + name + " field must be an Int64 JSON integer.", "request");
        }

        private static void ValidateCashbookString(JObject value, string name)
        {
            var token = value[name];
            if (token != null && token.Type != JTokenType.Null && token.Type != JTokenType.String)
                throw new ArgumentException("The optional " + name + " field must be original JSON string text.", "request");
        }
    }
}
