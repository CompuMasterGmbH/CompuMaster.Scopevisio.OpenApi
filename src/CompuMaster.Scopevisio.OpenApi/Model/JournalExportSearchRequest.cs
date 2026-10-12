using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an extensible journal export search with explicit optional Int64 selections.</summary>
    /// <remarks>Rows are ordered by ascending journal row number by the server. Bounds are epoch milliseconds without date/timezone conversion or local range ordering. Cursor progression and further paging remain caller-controlled. Unset properties omit fields and retain server defaults; explicit JSON null remains representable through the inherited indexer. Unknown search fields remain unchanged.</remarks>
    public class JournalExportSearchRequest : SearchRequest
    {
        /// <summary>Initializes an empty export search without imposed cursor or bounds.</summary>
        public JournalExportSearchRequest() { }

        /// <summary>Initializes an export search by deeply copying another JSON object.</summary>
        /// <param name="other">The complete search object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public JournalExportSearchRequest(JObject other) : base(other) { }

        /// <summary>Gets or sets the last fetched journal row number.</summary>
        /// <value>The explicit Int64 selection, or null to omit the field; zero and negative values are retained for server validation.</value>
        /// <exception cref="ArgumentException">The inherited JSON field is neither an Int64 integer nor JSON null.</exception>
        public long? AfterRowNumber { get { return ExportSearchIntegers.Read(this, "afterRowNumber"); } set { ExportSearchIntegers.Set(this, "afterRowNumber", value); } }

        /// <summary>Gets or sets the inclusive posting timestamp lower bound in epoch milliseconds.</summary>
        /// <value>The explicit Int64 selection, or null to omit the field; zero and negative values are retained for server validation.</value>
        /// <exception cref="ArgumentException">The inherited JSON field is neither an Int64 integer nor JSON null.</exception>
        public long? From { get { return ExportSearchIntegers.Read(this, "from"); } set { ExportSearchIntegers.Set(this, "from", value); } }

        /// <summary>Gets or sets the inclusive posting timestamp upper bound in epoch milliseconds.</summary>
        /// <value>The explicit Int64 selection, or null to omit the field; zero and negative values are retained for server validation.</value>
        /// <exception cref="ArgumentException">The inherited JSON field is neither an Int64 integer nor JSON null.</exception>
        public long? To { get { return ExportSearchIntegers.Read(this, "to"); } set { ExportSearchIntegers.Set(this, "to", value); } }

        internal void ValidateForSend()
        {
            ExportSearchIntegers.Read(this, "afterRowNumber");
            ExportSearchIntegers.Read(this, "from");
            ExportSearchIntegers.Read(this, "to");
        }
    }
}
