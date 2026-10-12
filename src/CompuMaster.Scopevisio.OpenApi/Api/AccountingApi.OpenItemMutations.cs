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
        /// <summary>Clears explicitly selected creditor open items.</summary>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Bearbeiten), selected documents and clearing validity. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ClearCreditorOpenItems(OpenItemClearingRequest request)
        {
            return ClearCreditorOpenItemsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ClearCreditorOpenItems"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> ClearCreditorOpenItemsWithHttpInfo(OpenItemClearingRequest request)
        {
            var body = SnapshotOpenItemMutation(request, "clear");
            var localVarPath = "/openitems/creditor/clearing";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ClearCreditorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ClearCreditorOpenItems"/>
        public Task<JToken> ClearCreditorOpenItemsAsync(OpenItemClearingRequest request)
        {
            return ClearCreditorOpenItemsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ClearCreditorOpenItems"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> ClearCreditorOpenItemsAsync(OpenItemClearingRequest request, CancellationToken cancellationToken)
        {
            return (await ClearCreditorOpenItemsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ClearCreditorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ClearCreditorOpenItemsAsyncWithHttpInfo(OpenItemClearingRequest request)
        {
            return ClearCreditorOpenItemsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ClearCreditorOpenItemsWithHttpInfo"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> ClearCreditorOpenItemsAsyncWithHttpInfo(OpenItemClearingRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOpenItemMutation(request, "clear");
            var localVarPath = "/openitems/creditor/clearing";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ClearCreditorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Clears explicitly selected debtor open items.</summary>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Bearbeiten), selected documents and clearing validity. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken ClearDebtorOpenItems(OpenItemClearingRequest request)
        {
            return ClearDebtorOpenItemsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ClearDebtorOpenItems"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> ClearDebtorOpenItemsWithHttpInfo(OpenItemClearingRequest request)
        {
            var body = SnapshotOpenItemMutation(request, "clear");
            var localVarPath = "/openitems/debitor/clearing";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("ClearDebtorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="ClearDebtorOpenItems"/>
        public Task<JToken> ClearDebtorOpenItemsAsync(OpenItemClearingRequest request)
        {
            return ClearDebtorOpenItemsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ClearDebtorOpenItems"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> ClearDebtorOpenItemsAsync(OpenItemClearingRequest request, CancellationToken cancellationToken)
        {
            return (await ClearDebtorOpenItemsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ClearDebtorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> ClearDebtorOpenItemsAsyncWithHttpInfo(OpenItemClearingRequest request)
        {
            return ClearDebtorOpenItemsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ClearDebtorOpenItemsWithHttpInfo"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> ClearDebtorOpenItemsAsyncWithHttpInfo(OpenItemClearingRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOpenItemMutation(request, "clear");
            var localVarPath = "/openitems/debitor/clearing";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("ClearDebtorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Rebooks explicitly selected creditor open items.</summary>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The manufacturer declares Debitoren (Anzeigen) even for creditor rebooking. Actual privileges and account/date/period semantics remain server-authoritative; concrete schema defaults are never inserted. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken RebookCreditorOpenItems(OpenItemRebookRequest request)
        {
            return RebookCreditorOpenItemsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="RebookCreditorOpenItems"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> RebookCreditorOpenItemsWithHttpInfo(OpenItemRebookRequest request)
        {
            var body = SnapshotOpenItemMutation(request, "rebook");
            var localVarPath = "/openitems/creditor/rebook";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("RebookCreditorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RebookCreditorOpenItems"/>
        public Task<JToken> RebookCreditorOpenItemsAsync(OpenItemRebookRequest request)
        {
            return RebookCreditorOpenItemsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="RebookCreditorOpenItems"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> RebookCreditorOpenItemsAsync(OpenItemRebookRequest request, CancellationToken cancellationToken)
        {
            return (await RebookCreditorOpenItemsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="RebookCreditorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> RebookCreditorOpenItemsAsyncWithHttpInfo(OpenItemRebookRequest request)
        {
            return RebookCreditorOpenItemsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="RebookCreditorOpenItemsWithHttpInfo"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> RebookCreditorOpenItemsAsyncWithHttpInfo(OpenItemRebookRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOpenItemMutation(request, "rebook");
            var localVarPath = "/openitems/creditor/rebook";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("RebookCreditorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Rebooks explicitly selected debtor open items.</summary>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The manufacturer declares Debitoren (Anzeigen) for rebooking. Actual privileges and account/date/period semantics remain server-authoritative; concrete schema defaults are never inserted. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken RebookDebtorOpenItems(OpenItemRebookRequest request)
        {
            return RebookDebtorOpenItemsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="RebookDebtorOpenItems"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> RebookDebtorOpenItemsWithHttpInfo(OpenItemRebookRequest request)
        {
            var body = SnapshotOpenItemMutation(request, "rebook");
            var localVarPath = "/openitems/debitor/rebook";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("RebookDebtorOpenItems", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RebookDebtorOpenItems"/>
        public Task<JToken> RebookDebtorOpenItemsAsync(OpenItemRebookRequest request)
        {
            return RebookDebtorOpenItemsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="RebookDebtorOpenItems"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> RebookDebtorOpenItemsAsync(OpenItemRebookRequest request, CancellationToken cancellationToken)
        {
            return (await RebookDebtorOpenItemsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="RebookDebtorOpenItemsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> RebookDebtorOpenItemsAsyncWithHttpInfo(OpenItemRebookRequest request)
        {
            return RebookDebtorOpenItemsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="RebookDebtorOpenItemsWithHttpInfo"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> RebookDebtorOpenItemsAsyncWithHttpInfo(OpenItemRebookRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOpenItemMutation(request, "rebook");
            var localVarPath = "/openitems/debitor/rebook";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("RebookDebtorOpenItems", response, ExceptionFactory);
        }

        /// <summary>Updates explicitly selected open-item reminder levels by original document number.</summary>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Journal (Anzeigen), active accounting area and valid reminder levels. Each item is processed individually; inspect original per-item results even on HTTP 200 rather than inferring aggregate success. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken SetOpenItemReminderLevels(OpenItemReminderLevelsRequest request)
        {
            return SetOpenItemReminderLevelsWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="SetOpenItemReminderLevels"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> SetOpenItemReminderLevelsWithHttpInfo(OpenItemReminderLevelsRequest request)
        {
            var body = SnapshotOpenItemMutation(request, "reminder");
            var localVarPath = "/openitems/setReminderLevel";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SetOpenItemReminderLevels", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SetOpenItemReminderLevels"/>
        public Task<JToken> SetOpenItemReminderLevelsAsync(OpenItemReminderLevelsRequest request)
        {
            return SetOpenItemReminderLevelsAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SetOpenItemReminderLevels"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> SetOpenItemReminderLevelsAsync(OpenItemReminderLevelsRequest request, CancellationToken cancellationToken)
        {
            return (await SetOpenItemReminderLevelsAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SetOpenItemReminderLevelsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SetOpenItemReminderLevelsAsyncWithHttpInfo(OpenItemReminderLevelsRequest request)
        {
            return SetOpenItemReminderLevelsAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SetOpenItemReminderLevelsWithHttpInfo"/>
        /// <param name="request">The required complete JSON body, deeply copied before HTTP I/O. Only declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> SetOpenItemReminderLevelsAsyncWithHttpInfo(OpenItemReminderLevelsRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotOpenItemMutation(request, "reminder");
            var localVarPath = "/openitems/setReminderLevel";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SetOpenItemReminderLevels", response, ExceptionFactory);
        }

        private static string SnapshotOpenItemMutation(JObject request, string kind)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            if (kind == "rebook")
            {
                foreach (var name in new[] { "documentNumber", "postingDate", "postingPeriod", "sourceNumber", "targetNumber" })
                    ValidateOpenItemText(snapshot[name], name, false);
            }
            else if (kind == "clear")
            {
                var groups = ReadOpenItemArray(snapshot["clearings"], "clearings", false);
                if (groups != null) foreach (var token in groups)
                {
                    if (!(token is JObject group)) throw new ArgumentException("Clearing groups must be JSON objects.", nameof(request));
                    ValidateOpenItemText(group["documentNumber"], "documentNumber", false);
                    var documents = ReadOpenItemArray(group["documents"], "documents", false);
                    if (documents != null) foreach (var documentToken in documents)
                    {
                        if (!(documentToken is JObject document)) throw new ArgumentException("Clearing documents must be JSON objects.", nameof(request));
                        ValidateOpenItemText(document["documentNumber"], "documentNumber", false);
                        var amount = document["clearingAmount"];
                        if (amount != null && amount.Type != JTokenType.Null && ((amount.Type != JTokenType.Integer && amount.Type != JTokenType.Float) ||
                            !System.Text.RegularExpressions.Regex.IsMatch(amount.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                            throw new ArgumentException("The clearingAmount field must be a finite JSON number or null.", nameof(request));
                    }
                }
            }
            else
            {
                var items = ReadOpenItemArray(snapshot["openItems"], "openItems", true);
                foreach (var token in items)
                {
                    if (!(token is JObject item)) throw new ArgumentException("Reminder items must be JSON objects.", nameof(request));
                    ValidateOpenItemText(item["documentNumber"], "documentNumber", true);
                    var level = item["reminderLevel"];
                    int value;
                    if (level == null || level.Type != JTokenType.Integer || !int.TryParse(level.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                        throw new ArgumentException("The reminderLevel field is required and must be an Int32 JSON integer.", nameof(request));
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static JArray ReadOpenItemArray(JToken token, string name, bool required)
        {
            if (!required && (token == null || token.Type == JTokenType.Null)) return null;
            if (!(token is JArray array)) throw new ArgumentException("The " + name + " field must be a JSON array.", "request");
            return array;
        }

        private static void ValidateOpenItemText(JToken token, string name, bool required)
        {
            if (!required && (token == null || token.Type == JTokenType.Null)) return;
            if (token == null || token.Type != JTokenType.String) throw new ArgumentException("The " + name + " field must be a JSON string.", "request");
        }
    }
}
