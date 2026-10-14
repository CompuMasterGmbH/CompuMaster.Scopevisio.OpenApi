using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
        /// <summary>Exports original buffered DATEV response bytes.</summary>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <returns>The original buffered response bytes, an empty array for empty content, or null if the transport supplies no bytes.</returns>
        /// <remarks>The server enforces Datenexport (Anzeigen). The route body is optional and settings preserve omission, original signed Int64 epoch milliseconds, mapping/dimension/text/strategy strings and nullable flags. Both creation-date bounds are required together when supplied; actual ranges and choices remain server-authoritative. No current-year/date conversion, mapping lookup, default flag or account-context selection occurs. Omitted posting dates/text/strategy/key choices may use documented server defaults, including current-year posting bounds and vatMatrixDatevKeys true; historical false defaults remain unchanged. The primary HTTP 200 declares no fixed schema, MIME, filename, encoding or format guarantee. Complete buffered bytes are retained without JSON/EXTF parsing, transcoding or inferred success. Requests are copied before HTTP; no extra calls or replay occur. Actual DATEV/mapping/profile acceptance remains unverified; historical APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied field has an incompatible JSON kind or Int64 width, or only one creation-date bound is supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] ExportDatevWithResult(DatevExportRequest request = null)
        {
            return ExportDatevWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="ExportDatevWithResult"/>
        /// <returns>The original buffered bytes with status and combined headers; RawJsonContent is null because this preserves bytes without JSON parsing.</returns>
        public ApiResponse<byte[]> ExportDatevWithResultWithHttpInfo(DatevExportRequest request = null)
        {
            var body = request?.SnapshotForSend();
            var localVarPath = "/datevexport";
            var headers = SearchJsonResponse.Headers(Configuration);
            headers["Accept"] = "*/*";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, headers,
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapJournalDownload("ExportDatevWithResult", response);
        }

        /// <inheritdoc cref="ExportDatevWithResult"/>
        public Task<byte[]> ExportDatevWithResultAsync(DatevExportRequest request = null)
        {
            return ExportDatevWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ExportDatevWithResult"/>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the export may already have been prepared on the server.</exception>
        public async Task<byte[]> ExportDatevWithResultAsync(DatevExportRequest request, CancellationToken cancellationToken)
        {
            return (await ExportDatevWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="ExportDatevWithResultWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> ExportDatevWithResultAsyncWithHttpInfo(DatevExportRequest request = null)
        {
            return ExportDatevWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="ExportDatevWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied DATEV JSON; null sends no body and no date, mapping, zone or flag defaults are inserted.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the export may already have been prepared on the server.</exception>
        public async Task<ApiResponse<byte[]>> ExportDatevWithResultAsyncWithHttpInfo(DatevExportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend();
            var localVarPath = "/datevexport";
            var headers = SearchJsonResponse.Headers(Configuration);
            headers["Accept"] = "*/*";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, headers,
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapJournalDownload("ExportDatevWithResult", response);
        }
    }
}
