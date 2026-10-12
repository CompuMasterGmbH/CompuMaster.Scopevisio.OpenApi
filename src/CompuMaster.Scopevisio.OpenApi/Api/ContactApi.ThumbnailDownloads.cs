using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ContactApi
    {
        /// <summary>Downloads the complete buffered thumbnail of a selected contact.</summary>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <returns>The original buffered response bytes, an empty array for an empty file, or null if the transport supplies no bytes.</returns>
        /// <remarks>The manufacturer specifies no MIME, filename, size or fixed success schema. The server requires the Kontakte (Anzeigen) profile; a 404 can indicate a missing resource, missing permission or missing file/thumbnail. The complete file is buffered in memory by the existing transport. This method does not parse JSON, use the legacy file deserializer or write temporary/destination files; the caller owns any later storage.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadContactThumbnail(long id)
        {
            return DownloadContactThumbnailWithHttpInfo(id).Data;
        }

        /// <inheritdoc cref="DownloadContactThumbnail"/>
        /// <returns>The buffered original bytes with status and original combined headers; RawJsonContent is null because this is binary content.</returns>
        public ApiResponse<byte[]> DownloadContactThumbnailWithHttpInfo(long id)
        {
            var localVarPath = "/contact/{id}/thumb";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return BinaryFileResponse.Parse("DownloadContactThumbnail", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DownloadContactThumbnail"/>
        public Task<byte[]> DownloadContactThumbnailAsync(long id)
        {
            return DownloadContactThumbnailAsync(id, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadContactThumbnail"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> DownloadContactThumbnailAsync(long id, CancellationToken cancellationToken)
        {
            return (await DownloadContactThumbnailAsyncWithHttpInfo(id, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadContactThumbnailWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadContactThumbnailAsyncWithHttpInfo(long id)
        {
            return DownloadContactThumbnailAsyncWithHttpInfo(id, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadContactThumbnailWithHttpInfo"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadContactThumbnailAsyncWithHttpInfo(long id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/thumb";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                new List<KeyValuePair<string, string>>(), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return BinaryFileResponse.Parse("DownloadContactThumbnail", response, ExceptionFactory);
        }

    }
}
