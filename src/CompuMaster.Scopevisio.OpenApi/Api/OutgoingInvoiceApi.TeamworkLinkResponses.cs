using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class OutgoingInvoiceApi
    {
        /// <summary>Retrieves the complete decoded Teamwork file-link response for a selected outgoing invoice.</summary>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming or conversion.</param>
        /// <returns>The complete original response text decoded by the transport, or null if the transport provides no content.</returns>
        /// <remarks>The manufacturer describes a link to the last file in the Teamwork web application and requires a separately logged-in web-app session and the Rechnungen (Anzeigen) profile. No success schema or content representation is defined. The text is not parsed as JSON, normalized as a URI or opened; no file is downloaded. Decoding follows the existing transport and does not promise byte identity. HTTP 404 can mean a missing invoice, permission or file. Original generated methods, null-data defaults, authorization and selected account remain unchanged.</remarks>
        /// <exception cref="ArgumentException">The invoice selector is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public string GetOutgoingInvoiceTeamworkFileLinkResponse(string number)
        {
            return GetOutgoingInvoiceTeamworkFileLinkResponseWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceTeamworkFileLinkResponse"/>
        /// <returns>The complete decoded text, status and original combined headers; RawJsonContent is null because a JSON representation is not established.</returns>
        public ApiResponse<string> GetOutgoingInvoiceTeamworkFileLinkResponseWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/teamworkFileLink";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, RawTextResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return RawTextResponse.Parse("GetOutgoingInvoiceTeamworkFileLinkResponse", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceTeamworkFileLinkResponse"/>
        public Task<string> GetOutgoingInvoiceTeamworkFileLinkResponseAsync(string number)
        {
            return GetOutgoingInvoiceTeamworkFileLinkResponseAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceTeamworkFileLinkResponse"/>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<string> GetOutgoingInvoiceTeamworkFileLinkResponseAsync(string number, CancellationToken cancellationToken)
        {
            return (await GetOutgoingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetOutgoingInvoiceTeamworkFileLinkResponseWithHttpInfo"/>
        public Task<ApiResponse<string>> GetOutgoingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(string number)
        {
            return GetOutgoingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="GetOutgoingInvoiceTeamworkFileLinkResponseWithHttpInfo"/>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming or conversion.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<string>> GetOutgoingInvoiceTeamworkFileLinkResponseAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/teamworkFileLink";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, RawTextResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return RawTextResponse.Parse("GetOutgoingInvoiceTeamworkFileLinkResponse", response, ExceptionFactory);
        }

    }
}
