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
        /// <summary>Updates an explicitly selected outgoing invoice using the current beta contract.</summary>
        /// <param name="number">The required original outgoing-invoice document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied bodies require the schema's customerContactId and documentDate fields.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>This manufacturer endpoint is BETA. The server enforces Rechnungen (Bearbeiten), rejects already posted invoices with HTTP 403 and may use HTTP 404 for either missing invoices or missing write permission. Creation-oriented required/default prose conflicts with schema requirements; no current dates, customers, products, permissions, currency or discount state is inferred. Complete results and original errors remain available. No new mutation retry or follow-up is added; cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The invoice selection is blank or a relative dot segment, or known supplied fields have incompatible required kinds or widths.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken UpdateOutgoingInvoice(string number, OutgoingInvoiceUpdateRequest request = null)
        {
            return UpdateOutgoingInvoiceWithHttpInfo(number, request).Data;
        }

        /// <inheritdoc cref="UpdateOutgoingInvoice"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> UpdateOutgoingInvoiceWithHttpInfo(string number, OutgoingInvoiceUpdateRequest request = null)
        {
            var selection = ValidateOutgoingInvoiceUpdateSelection(number);
            var body = SnapshotOutgoingInvoiceUpdate(request);
            var localVarPath = "/outgoinginvoice/{number}";
            localVarPath = localVarPath.Replace("{number}", selection);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateOutgoingInvoice", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoice"/>
        public Task<JToken> UpdateOutgoingInvoiceAsync(string number, OutgoingInvoiceUpdateRequest request = null)
        {
            return UpdateOutgoingInvoiceAsync(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoice"/>
        /// <param name="number">The required original outgoing-invoice document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied bodies require the schema's customerContactId and documentDate fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the invoice.</exception>
        public async Task<JToken> UpdateOutgoingInvoiceAsync(string number, OutgoingInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateOutgoingInvoiceAsyncWithHttpInfo(number, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateOutgoingInvoiceWithHttpInfo"/>
        public Task<ApiResponse<JToken>> UpdateOutgoingInvoiceAsyncWithHttpInfo(string number, OutgoingInvoiceUpdateRequest request = null)
        {
            return UpdateOutgoingInvoiceAsyncWithHttpInfo(number, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateOutgoingInvoiceWithHttpInfo"/>
        /// <param name="number">The required original outgoing-invoice document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; supplied bodies require the schema's customerContactId and documentDate fields.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the invoice.</exception>
        public async Task<ApiResponse<JToken>> UpdateOutgoingInvoiceAsyncWithHttpInfo(string number, OutgoingInvoiceUpdateRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var selection = ValidateOutgoingInvoiceUpdateSelection(number);
            var body = SnapshotOutgoingInvoiceUpdate(request);
            var localVarPath = "/outgoinginvoice/{number}";
            localVarPath = localVarPath.Replace("{number}", selection);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateOutgoingInvoice", response, ExceptionFactory);
        }

        private static string ValidateOutgoingInvoiceUpdateSelection(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == "..")
                throw new ArgumentException("An original invoice document number is required.", "number");
            return Uri.EscapeDataString(value);
        }

        private static string SnapshotOutgoingInvoiceUpdate(OutgoingInvoiceUpdateRequest request)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            ValidateOutgoingInvoiceUpdateFields(snapshot, 0);
            var positions = snapshot["positionsForm"];
            if (positions != null && positions.Type != JTokenType.Null)
            {
                if (!(positions is JObject form)) throw new ArgumentException("The positionsForm field must be an object or null.", nameof(request));
                ValidateOutgoingInvoiceUpdateFields(form, 1);
                foreach (var token in (JArray)form["positions"])
                {
                    if (!(token is JObject row)) throw new ArgumentException("Positions must be objects.", nameof(request));
                    ValidateOutgoingInvoiceUpdateFields(row, 2);
                }
            }
            var fields = snapshot["customFields"];
            if (fields != null && fields.Type != JTokenType.Null)
            {
                foreach (var token in (JArray)fields)
                {
                    if (!(token is JObject field)) throw new ArgumentException("Custom fields must be objects.", nameof(request));
                    var name = field["customFieldName"];
                    if (name == null || name.Type != JTokenType.String || field.Property("customFieldValue") == null)
                        throw new ArgumentException("Custom fields require original customFieldName and customFieldValue.", nameof(request));
                }
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateOutgoingInvoiceUpdateFields(JObject value, int kind)
        {
            if (kind == 0)
            {
                ValidateOutgoingInvoiceUpdateValues(value,
                    new[] { "customerContactName","customerContactCustomerNumber","customerPersonalAccountNumber","customerPersonalAccountName","customerPersonalAccountExternalNumber","customerName","customerNumber","customerNameExtra","name","text","documentNumber","ourContactPerson","deliveryType","textTitle","textSubTitle","textIntro","textExtro","otherAddress","readPermissionProfiles","readPermissionLogins","writePermissionProfiles","writePermissionLogins","paymentTypeName","orderNumber","zoneNumber","documentText","locale","currency","reference","taxCountryCodeIso2" },
                    new[] { "gross","isEndDiscountAbsolute","isPaid","explicitDiscount","isIntercompany" },
                    new[] { "projectId","customerContactId","customerPersonalAccountId","customerContactPerson","documentDate","selectedAddress","permissionType","deliveryDate","deliveryDateTo","discountDate1","discountDate2","dueDate","postingDate" },
                    new[] { "deliveryCosts","cashOnDelivery","endDiscount","discountPercent1","discountPercent2","discountAmount1","discountAmount2","exchangeRate" },
                    new[] { "customFields" },
                    new[] { "customerContactId","documentDate" });
            }
            else if (kind == 1)
            {
                ValidateOutgoingInvoiceUpdateValues(value,
                    new string[0],
                    new[] { "copyProductsToPositions" },
                    new string[0],
                    new string[0],
                    new[] { "positions" },
                    new[] { "positions" });
            }
            else if (kind == 2)
            {
                ValidateOutgoingInvoiceUpdateValues(value,
                    new[] { "typeDisplay","number","name","description","unit","account","vatKey","freeText1","freeText2","dimension_1","dimension_2","dimension_3","dimension_4","dimension_5","dimension_6","dimension_7","dimension_8","dimension_9","dimension_10" },
                    new[] { "asIfSold" },
                    new[] { "valutaDate","dateFrom","dateTo" },
                    new[] { "quantity","singleAmount","discount","singleCostAmount" },
                    new string[0],
                    new[] { "name" });
            }
        }

        private static void ValidateOutgoingInvoiceUpdateValues(JObject value, string[] texts, string[] flags, string[] integers, string[] numbers, string[] arrays, string[] required)
        {
            foreach (var name in required)
                if (value[name] == null || value[name].Type == JTokenType.Null)
                    throw new ArgumentException("The " + name + " field is required.", "request");
            foreach (var name in texts)
                if (value[name] != null && value[name].Type != JTokenType.Null && value[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be a string or null.", "request");
            foreach (var name in flags)
                if (value[name] != null && value[name].Type != JTokenType.Null && value[name].Type != JTokenType.Boolean)
                    throw new ArgumentException("The " + name + " field must be a boolean or null.", "request");
            foreach (var name in integers)
            {
                var token = value[name]; long parsed;
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
            foreach (var name in arrays)
                if (value[name] != null && value[name].Type != JTokenType.Null && !(value[name] is JArray))
                    throw new ArgumentException("The " + name + " field must be an array or null.", "request");
        }
    }
}
