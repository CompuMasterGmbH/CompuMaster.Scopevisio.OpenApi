using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ReportApi
    {
        /// <summary>Downloads the complete buffered accounting-report export with explicit choices.</summary>
        /// <param name="request">The copied report selection, original required dates and optional format, flags and dimension splitting.</param>
        /// <returns>The original buffered bytes, an empty array for empty output, or null if the transport supplies no bytes.</returns>
        /// <remarks>The server requires Berichte, Debitoren, Kreditoren (Anzeigen) profiles and validates report/catalog/date/dimension choices. Omitted outputFormat uses the server default of csv. No defaults, date conversion, report lookup, parsing or destination files are introduced. The existing transport buffers complete output in memory. Historical generated methods and account/token defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The request or a required date string is null.</exception>
        /// <exception cref="ArgumentException">The report type is blank or a relative path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] DownloadAccountingReport(ReportExportRequest request)
        {
            return DownloadAccountingReportWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="DownloadAccountingReport"/>
        /// <returns>The buffered bytes, status and combined response/content headers; RawJsonContent is null for export content.</returns>
        public ApiResponse<byte[]> DownloadAccountingReportWithHttpInfo(ReportExportRequest request)
        {
            var selected = SnapshotReportExportRequest(request);
            var localVarPath = "/reports/{type}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(selected.Type));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                ReportExportQuery(selected), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream");
            return BinaryFileResponse.Parse("DownloadAccountingReport", response, ExceptionFactory);
        }

        /// <inheritdoc cref="DownloadAccountingReport"/>
        public Task<byte[]> DownloadAccountingReportAsync(ReportExportRequest request)
        {
            return DownloadAccountingReportAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadAccountingReport"/>
        /// <param name="request">The copied report selection, original required dates and optional format, flags and dimension splitting.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<byte[]> DownloadAccountingReportAsync(ReportExportRequest request, CancellationToken cancellationToken)
        {
            return (await DownloadAccountingReportAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="DownloadAccountingReportWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> DownloadAccountingReportAsyncWithHttpInfo(ReportExportRequest request)
        {
            return DownloadAccountingReportAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="DownloadAccountingReportWithHttpInfo"/>
        /// <param name="request">The copied report selection, original required dates and optional format, flags and dimension splitting.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> DownloadAccountingReportAsyncWithHttpInfo(ReportExportRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var selected = SnapshotReportExportRequest(request);
            var localVarPath = "/reports/{type}";
            localVarPath = localVarPath.Replace("{type}", Uri.EscapeDataString(selected.Type));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                ReportExportQuery(selected), null, BinaryFileResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/octet-stream", cancellationToken).ConfigureAwait(false);
            return BinaryFileResponse.Parse("DownloadAccountingReport", response, ExceptionFactory);
        }

        private static ReportExportRequest SnapshotReportExportRequest(ReportExportRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var selected = new ReportExportRequest(request);
            if (string.IsNullOrWhiteSpace(selected.Type) || selected.Type == "." || selected.Type == "..")
                throw new ArgumentException("An explicit report type without relative path segments is required.", nameof(request));
            if (selected.StartDate == null) throw new ArgumentNullException(nameof(request), "The original startDate string is required.");
            if (selected.EndDate == null) throw new ArgumentNullException(nameof(request), "The original endDate string is required.");
            return selected;
        }

        private static List<KeyValuePair<string, string>> ReportExportQuery(ReportExportRequest selected)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("startDate", selected.StartDate),
                new KeyValuePair<string, string>("endDate", selected.EndDate)
            };
            if (selected.ReportName != null) query.Add(new KeyValuePair<string, string>("reportName", selected.ReportName));
            if (selected.OutputFormat != null) query.Add(new KeyValuePair<string, string>("outputFormat", selected.OutputFormat));
            if (selected.IncludeHeader.HasValue) query.Add(new KeyValuePair<string, string>("includeHeader", selected.IncludeHeader.Value ? "true" : "false"));
            if (selected.IncludeZeroValued.HasValue) query.Add(new KeyValuePair<string, string>("includeZeroValued", selected.IncludeZeroValued.Value ? "true" : "false"));
            if (selected.SplitDimensions != null) query.Add(new KeyValuePair<string, string>("splitDimensions", selected.SplitDimensions));
            return query;
        }
    }
}
