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
        /// <summary>Downloads the complete buffered main file of a selected incoming invoice.</summary>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming.</param>
        /// <returns>The original buffered response bytes, an empty array for an empty file, or null if the transport supplies no bytes.</returns>
        /// <remarks>The manufacturer specifies no MIME, filename, size or fixed success schema. The server requires the Rechnungseingangsbuch (Anzeigen) profile; a 404 can indicate a missing resource, missing permission or missing file/thumbnail. The complete file is buffered in memory by the existing transport. This method does not parse JSON, use the legacy file deserializer or write temporary/destination files; the caller owns any later storage.</remarks>
        /// <exception cref="ArgumentException">The invoice selector is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadIncomingInvoiceFile(string idOrNumber)
        {
            return DownloadIncomingInvoiceFileWithHttpInfo(idOrNumber).Data;
        }

        /// <inheritdoc cref="DownloadIncomingInvoiceFile"/>
        /// <returns>The buffered original bytes with status and original combined headers; RawJsonContent is null because this is binary content.</returns>
        public ApiResponse<byte[]> DownloadIncomingInvoiceFileWithHttpInfo(string idOrNumber)
        {
            if (string.IsNullOrWhiteSpace(idOrNumber) || idOrNumber == "." || idOrNumber == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(idOrNumber));
            var localVarPath = "/incominginvoice/{idOrNumber}/file";
            localVarPath = localVarPath.Replace("{idOrNumber}", Uri.EscapeDataString(idOrNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return BinaryFileResponse.Parse("DownloadIncomingInvoiceFile", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DownloadIncomingInvoiceFile"/>
        public Task<byte[]> DownloadIncomingInvoiceFileAsync(string idOrNumber)
        {
            return DownloadIncomingInvoiceFileAsync(idOrNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadIncomingInvoiceFile"/>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> DownloadIncomingInvoiceFileAsync(string idOrNumber, CancellationToken cancellationToken)
        {
            return (await DownloadIncomingInvoiceFileAsyncWithHttpInfo(idOrNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadIncomingInvoiceFileWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadIncomingInvoiceFileAsyncWithHttpInfo(string idOrNumber)
        {
            return DownloadIncomingInvoiceFileAsyncWithHttpInfo(idOrNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadIncomingInvoiceFileWithHttpInfo"/>
        /// <param name="idOrNumber">The explicitly selected original incoming invoice identifier or internal number, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadIncomingInvoiceFileAsyncWithHttpInfo(string idOrNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(idOrNumber) || idOrNumber == "." || idOrNumber == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(idOrNumber));
            var localVarPath = "/incominginvoice/{idOrNumber}/file";
            localVarPath = localVarPath.Replace("{idOrNumber}", Uri.EscapeDataString(idOrNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return BinaryFileResponse.Parse("DownloadIncomingInvoiceFile", response, ExceptionFactory);
        }

    }
}
