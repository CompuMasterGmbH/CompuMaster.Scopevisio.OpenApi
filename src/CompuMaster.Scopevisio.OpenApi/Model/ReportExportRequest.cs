using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit accounting-report export choices without server defaults.</summary>
    /// <remarks>The request is copied before HTTP admission. Original date/catalog/dimension strings remain server-validated; no format conversion, report lookup or dimension lookup is performed.</remarks>
    public sealed class ReportExportRequest
    {
        /// <summary>Initializes an empty export selection without imposing defaults.</summary>
        public ReportExportRequest() { }

        /// <summary>Initializes an export selection by copying every original choice.</summary>
        /// <param name="other">The explicit report selection to copy.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public ReportExportRequest(ReportExportRequest other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Type = other.Type; StartDate = other.StartDate; EndDate = other.EndDate;
            ReportName = other.ReportName; OutputFormat = other.OutputFormat;
            IncludeHeader = other.IncludeHeader; IncludeZeroValued = other.IncludeZeroValued;
            SplitDimensions = other.SplitDimensions;
        }

        /// <summary>Gets or sets the required original report-type string.</summary>
        /// <remarks>The documented catalog contains susa_account, susa_debitor, susa_creditor, bilanz, guv and bwa; actual availability remains server-authoritative.</remarks>
        public string Type { get; set; }
        /// <summary>Gets or sets the required original start-date string.</summary>
        /// <remarks>The documented format is dd.MM.yyyy; calendar validity is checked by the server without client conversion.</remarks>
        public string StartDate { get; set; }
        /// <summary>Gets or sets the required original end-date string.</summary>
        /// <remarks>The documented format is dd.MM.yyyy; calendar validity is checked by the server without client conversion.</remarks>
        public string EndDate { get; set; }
        /// <summary>Gets or sets the original optional report name, or null to omit it.</summary>
        /// <remarks>The server applies this choice only to bilanz, guv and bwa.</remarks>
        public string ReportName { get; set; }
        /// <summary>Gets or sets the original optional output format, or null to use the server default of csv.</summary>
        /// <remarks>The documented catalog contains pdf, csv, xml and json. The client does not parse or convert the selected output.</remarks>
        public string OutputFormat { get; set; }
        /// <summary>Gets or sets the explicit CSV-header choice, or null to omit it.</summary>
        public bool? IncludeHeader { get; set; }
        /// <summary>Gets or sets the explicit zero-value inclusion choice, or null to omit it.</summary>
        public bool? IncludeZeroValued { get; set; }
        /// <summary>Gets or sets original comma-separated dimension indices, or null to omit balance splitting.</summary>
        /// <remarks>Indices and ordering are not parsed, converted or looked up by the client.</remarks>
        public string SplitDimensions { get; set; }
    }
}
