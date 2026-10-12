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
        /// <summary>Downloads the complete buffered file of a selected outgoing invoice.</summary>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming.</param>
        /// <returns>The original buffered response bytes, an empty array for an empty file, or null if the transport supplies no bytes.</returns>
        /// <remarks>The manufacturer describes the last file in the operation and the main file in its response; selection remains server-authoritative. No MIME, filename, size or fixed success schema is specified. The server requires the Rechnungen (Anzeigen) profile; a 404 can indicate a missing resource, missing permission or missing file/thumbnail. The complete file is buffered in memory by the existing transport. This method does not parse JSON, use the legacy file deserializer or write temporary/destination files; the caller owns any later storage.</remarks>
        /// <exception cref="ArgumentException">The invoice selector is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadOutgoingInvoiceFile(string number)
        {
            return DownloadOutgoingInvoiceFileWithHttpInfo(number).Data;
        }

        /// <inheritdoc cref="DownloadOutgoingInvoiceFile"/>
        /// <returns>The buffered original bytes with status and original combined headers; RawJsonContent is null because this is binary content.</returns>
        public ApiResponse<byte[]> DownloadOutgoingInvoiceFileWithHttpInfo(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/file";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return BinaryFileResponse.Parse("DownloadOutgoingInvoiceFile", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DownloadOutgoingInvoiceFile"/>
        public Task<byte[]> DownloadOutgoingInvoiceFileAsync(string number)
        {
            return DownloadOutgoingInvoiceFileAsync(number, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadOutgoingInvoiceFile"/>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> DownloadOutgoingInvoiceFileAsync(string number, CancellationToken cancellationToken)
        {
            return (await DownloadOutgoingInvoiceFileAsyncWithHttpInfo(number, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadOutgoingInvoiceFileWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadOutgoingInvoiceFileAsyncWithHttpInfo(string number)
        {
            return DownloadOutgoingInvoiceFileAsyncWithHttpInfo(number, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadOutgoingInvoiceFileWithHttpInfo"/>
        /// <param name="number">The explicitly selected original outgoing invoice document number, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadOutgoingInvoiceFileAsyncWithHttpInfo(string number, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(number) || number == "." || number == "..")
                throw new ArgumentException("An explicit invoice selector without relative path segments is required.", nameof(number));
            var localVarPath = "/outgoinginvoice/{number}/file";
            localVarPath = localVarPath.Replace("{number}", Uri.EscapeDataString(number));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return BinaryFileResponse.Parse("DownloadOutgoingInvoiceFile", response, ExceptionFactory);
        }

    }
}
