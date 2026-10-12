using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AccountingApi
    {
        /// <summary>Downloads the complete buffered original file for a journal entry.</summary>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <returns>The original buffered response bytes, an empty array for an empty file, or null if the transport supplies no bytes.</returns>
        /// <remarks>The manufacturer specifies no MIME, filename, size or fixed success schema. The server enforces Journal profiles and file visibility; a 404 does not establish whether the entry, permission or file is absent. The complete file is buffered in memory by the existing transport. This method does not parse JSON, use the legacy file deserializer or write temporary/destination files; the caller owns any later storage.</remarks>
        /// <exception cref="ArgumentException">The document number is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadJournalEntryFile(string documentNumber)
        {
            return DownloadJournalEntryFileWithHttpInfo(documentNumber).Data;
        }

        /// <inheritdoc cref="DownloadJournalEntryFile"/>
        /// <returns>The buffered original bytes with status and original combined headers; RawJsonContent is null because this is binary content.</returns>
        public ApiResponse<byte[]> DownloadJournalEntryFileWithHttpInfo(string documentNumber)
        {
            ValidateJournalDownloadNumber(documentNumber);
            var localVarPath = "/journal/{documentNumber}/file";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, JournalDownloadHeaders(),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return WrapJournalDownload("DownloadJournalEntryFile", response);
        }

        /// <inheritdoc cref="DownloadJournalEntryFile"/>
        public Task<byte[]> DownloadJournalEntryFileAsync(string documentNumber)
        {
            return DownloadJournalEntryFileAsync(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadJournalEntryFile"/>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> DownloadJournalEntryFileAsync(string documentNumber, CancellationToken cancellationToken)
        {
            return (await DownloadJournalEntryFileAsyncWithHttpInfo(documentNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadJournalEntryFileWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadJournalEntryFileAsyncWithHttpInfo(string documentNumber)
        {
            return DownloadJournalEntryFileAsyncWithHttpInfo(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadJournalEntryFileWithHttpInfo"/>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadJournalEntryFileAsyncWithHttpInfo(string documentNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateJournalDownloadNumber(documentNumber);
            var localVarPath = "/journal/{documentNumber}/file";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, JournalDownloadHeaders(),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return WrapJournalDownload("DownloadJournalEntryFile", response);
        }

        /// <summary>Downloads the complete buffered server-stamped file for a journal entry.</summary>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <returns>The original buffered response bytes, an empty array for an empty file, or null if the transport supplies no bytes.</returns>
        /// <remarks>The manufacturer specifies no MIME, filename, size or fixed success schema. The server enforces Journal profiles and file visibility; a 404 does not establish whether the entry, permission or file is absent. Stamp contents depend on the server's invoice settings. The complete file is buffered in memory by the existing transport. This method does not parse JSON, use the legacy file deserializer or write temporary/destination files; the caller owns any later storage.</remarks>
        /// <exception cref="ArgumentException">The document number is missing, blank or a relative-dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadStampedJournalEntryFile(string documentNumber)
        {
            return DownloadStampedJournalEntryFileWithHttpInfo(documentNumber).Data;
        }

        /// <inheritdoc cref="DownloadStampedJournalEntryFile"/>
        /// <returns>The buffered original bytes with status and original combined headers; RawJsonContent is null because this is binary content.</returns>
        public ApiResponse<byte[]> DownloadStampedJournalEntryFileWithHttpInfo(string documentNumber)
        {
            ValidateJournalDownloadNumber(documentNumber);
            var localVarPath = "/journal/{documentNumber}/filewithstamp";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, JournalDownloadHeaders(),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return WrapJournalDownload("DownloadStampedJournalEntryFile", response);
        }

        /// <inheritdoc cref="DownloadStampedJournalEntryFile"/>
        public Task<byte[]> DownloadStampedJournalEntryFileAsync(string documentNumber)
        {
            return DownloadStampedJournalEntryFileAsync(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadStampedJournalEntryFile"/>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> DownloadStampedJournalEntryFileAsync(string documentNumber, CancellationToken cancellationToken)
        {
            return (await DownloadStampedJournalEntryFileAsyncWithHttpInfo(documentNumber, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadStampedJournalEntryFileWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadStampedJournalEntryFileAsyncWithHttpInfo(string documentNumber)
        {
            return DownloadStampedJournalEntryFileAsyncWithHttpInfo(documentNumber, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadStampedJournalEntryFileWithHttpInfo"/>
        /// <param name="documentNumber">The required nonblank original document-number string, escaped without trimming.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadStampedJournalEntryFileAsyncWithHttpInfo(string documentNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateJournalDownloadNumber(documentNumber);
            var localVarPath = "/journal/{documentNumber}/filewithstamp";
            localVarPath = localVarPath.Replace("{documentNumber}", Uri.EscapeDataString(documentNumber));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, JournalDownloadHeaders(),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return WrapJournalDownload("DownloadStampedJournalEntryFile", response);
        }

        private static void ValidateJournalDownloadNumber(string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentNumber) || documentNumber == "." || documentNumber == "..")
                throw new ArgumentException("A nonblank document number without relative path segments is required.", nameof(documentNumber));
        }

        private Dictionary<string, string> JournalDownloadHeaders()
        {
            var headers = SearchJsonResponse.Headers(Configuration);
            headers["Accept"] = "*/*";
            return headers;
        }

        private static void AppendJournalDownloadHeader(Dictionary<string, string> headers, string name, string value)
        {
            if (headers.TryGetValue(name, out var previous)) headers[name] = previous + "," + value;
            else headers.Add(name, value);
        }

        private ApiResponse<byte[]> WrapJournalDownload(string methodName, RestResponse response)
        {
            var error = ExceptionFactory?.Invoke(methodName, response);
            if (error != null) throw error;
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (response.Headers != null)
                foreach (var header in response.Headers)
                    AppendJournalDownloadHeader(headers, header.Name, Convert.ToString(header.Value));
            if (response.ContentHeaders != null)
                foreach (var header in response.ContentHeaders)
                    AppendJournalDownloadHeader(headers, header.Name, Convert.ToString(header.Value));
            return new ApiResponse<byte[]>((int)response.StatusCode, headers, null, response.RawBytes);
        }
    }
}
