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
    public partial class IncomingInvoiceApi
    {
        /// <summary>Assigns or explicitly removes the responsible user contact of a selected incoming invoice.</summary>
        /// <param name="idOrNumber">The required original invoice master-ID or number string, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; contact ID zero or negative explicitly selects removal.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Rechnungseingangsbuch (Bearbeiten), a valid user contact and invoice/workflow prerequisites. HTTP 404 covers several missing or invalid workflow/contact states; original diagnostics are preserved without existence/permission inference or automatic workflow repair. Omitted or JSON-null contact fields do not trigger client-side removal. No new mutation retry or follow-up is added; cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The invoice selection is blank or a relative dot segment, or contactId has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken SetIncomingInvoiceResponsibleContact(string idOrNumber, IncomingInvoiceResponsibleContactRequest request = null)
        {
            return SetIncomingInvoiceResponsibleContactWithHttpInfo(idOrNumber, request).Data;
        }

        /// <inheritdoc cref="SetIncomingInvoiceResponsibleContact"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> SetIncomingInvoiceResponsibleContactWithHttpInfo(string idOrNumber, IncomingInvoiceResponsibleContactRequest request = null)
        {
            var selection = ValidateResponsibleInvoiceSelection(idOrNumber);
            var body = SnapshotResponsibleContact(request);
            var localVarPath = "/incominginvoice/{idOrNumber}/responsiblecontact";
            localVarPath = localVarPath.Replace("{idOrNumber}", selection);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SetIncomingInvoiceResponsibleContact", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SetIncomingInvoiceResponsibleContact"/>
        public Task<JToken> SetIncomingInvoiceResponsibleContactAsync(string idOrNumber, IncomingInvoiceResponsibleContactRequest request = null)
        {
            return SetIncomingInvoiceResponsibleContactAsync(idOrNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="SetIncomingInvoiceResponsibleContact"/>
        /// <param name="idOrNumber">The required original invoice master-ID or number string, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; contact ID zero or negative explicitly selects removal.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the assignment.</exception>
        public async Task<JToken> SetIncomingInvoiceResponsibleContactAsync(string idOrNumber, IncomingInvoiceResponsibleContactRequest request, CancellationToken cancellationToken)
        {
            return (await SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(idOrNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SetIncomingInvoiceResponsibleContactWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(string idOrNumber, IncomingInvoiceResponsibleContactRequest request = null)
        {
            return SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(idOrNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="SetIncomingInvoiceResponsibleContactWithHttpInfo"/>
        /// <param name="idOrNumber">The required original invoice master-ID or number string, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request, copied before HTTP I/O. Null omits the body; contact ID zero or negative explicitly selects removal.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed the assignment.</exception>
        public async Task<ApiResponse<JToken>> SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(string idOrNumber, IncomingInvoiceResponsibleContactRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var selection = ValidateResponsibleInvoiceSelection(idOrNumber);
            var body = SnapshotResponsibleContact(request);
            var localVarPath = "/incominginvoice/{idOrNumber}/responsiblecontact";
            localVarPath = localVarPath.Replace("{idOrNumber}", selection);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SetIncomingInvoiceResponsibleContact", response, ExceptionFactory);
        }

        private static string ValidateResponsibleInvoiceSelection(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == "..")
                throw new ArgumentException("An original invoice identifier or number is required.", "idOrNumber");
            return Uri.EscapeDataString(value);
        }

        private static string SnapshotResponsibleContact(IncomingInvoiceResponsibleContactRequest request)
        {
            if (request == null) return null;
            var snapshot = new IncomingInvoiceResponsibleContactRequest(request);
            var contact = snapshot["contactId"];
            long identifier;
            if (contact != null && contact.Type != JTokenType.Null && (contact.Type != JTokenType.Integer ||
                !long.TryParse(contact.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out identifier)))
                throw new ArgumentException("The contactId field must be an Int64 JSON integer or null.", nameof(request));
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
