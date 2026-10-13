using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class IncomingInvoiceApi
    {
        /// <summary>Retrieves the complete decoded Teamwork file-link response for a selected incoming invoice.</summary>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming or conversion.</param>
        /// <returns>The complete original response text decoded by the transport, or null if the transport provides no content.</returns>
        /// <remarks>The manufacturer describes a link to the main file in the Teamwork web application and requires a separately logged-in web-app session and the Rechnungseingangsbuch (Anzeigen) profile. No success schema or content representation is defined. The text is not parsed as JSON, normalized as a URI or opened; no file is downloaded. Decoding follows the existing transport and does not promise byte identity. HTTP 404 can mean a missing invoice, permission or file. Original generated methods, null-data defaults, authorization and selected account remain unchanged.</remarks>
        /// <exception cref="ArgumentException">The invoice selector is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public string GetIncomingInvoiceTeamworkFileLinkResponse(string idOrNumber)
        {
            return GetIncomingInvoiceTeamworkFileLinkResponseWithHttpInfo(idOrNumber).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoiceTeamworkFileLinkResponse"/>
        /// <returns>The complete decoded text, status and original combined headers; RawJsonContent is null because a JSON representation is not established.</returns>
        public ApiResponse<string> GetIncomingInvoiceTeamworkFileLinkResponseWithHttpInfo(string idOrNumber)
        {
            if (string.IsNullOrWhiteSpace(idOrNumber) || idOrNumber == "." || idOrNumber == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(idOrNumber));
            var localVarPath = "/incominginvoice/{idOrNumber}/teamworkFileLink";
            localVarPath = localVarPath.Replace("{idOrNumber}", Uri.EscapeDataString(idOrNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, RawTextResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return RawTextResponse.Parse("GetIncomingInvoiceTeamworkFileLinkResponse", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetIncomingInvoiceTeamworkFileLinkResponse"/>
        public Task<string> GetIncomingInvoiceTeamworkFileLinkResponseAsync(string idOrNumber)
        {
            return GetIncomingInvoiceTeamworkFileLinkResponseAsync(idOrNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoiceTeamworkFileLinkResponse"/>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<string> GetIncomingInvoiceTeamworkFileLinkResponseAsync(string idOrNumber, CancellationToken cancellationToken)
        {
            return (await GetIncomingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(idOrNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetIncomingInvoiceTeamworkFileLinkResponseWithHttpInfo"/>
        public Task<ApiResponse<string>> GetIncomingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(string idOrNumber)
        {
            return GetIncomingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(idOrNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="GetIncomingInvoiceTeamworkFileLinkResponseWithHttpInfo"/>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<string>> GetIncomingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(string idOrNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(idOrNumber) || idOrNumber == "." || idOrNumber == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(idOrNumber));
            var localVarPath = "/incominginvoice/{idOrNumber}/teamworkFileLink";
            localVarPath = localVarPath.Replace("{idOrNumber}", Uri.EscapeDataString(idOrNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, RawTextResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return RawTextResponse.Parse("GetIncomingInvoiceTeamworkFileLinkResponse", response, ExceptionFactory);
        }

    }
}
