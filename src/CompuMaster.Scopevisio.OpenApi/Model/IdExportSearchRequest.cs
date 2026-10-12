using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an extensible identity-based export search with explicit optional Int64 selections.</summary>
    /// <remarks>Rows are ordered by ascending identity by the server. The caller selects the cursor from an actual returned row; the client does not infer completion, advance the cursor or fetch further pages. Unset properties omit fields and retain server defaults; explicit JSON null remains representable through the inherited indexer. Unknown search fields remain unchanged.</remarks>
    public class IdExportSearchRequest : SearchRequest
    {
        /// <summary>Initializes an empty export search without imposed cursor or bounds.</summary>
        public IdExportSearchRequest() { }

        /// <summary>Initializes an export search by deeply copying another JSON object.</summary>
        /// <param name="other">The complete search object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public IdExportSearchRequest(JObject other) : base(other) { }

        /// <summary>Gets or sets the last fetched row identity.</summary>
        /// <value>The explicit Int64 selection, or null to omit the field; zero and negative values are retained for server validation.</value>
        /// <exception cref="ArgumentException">The inherited JSON field is neither an Int64 integer nor JSON null.</exception>
        public long? AfterId { get { return ExportSearchIntegers.Read(this, "afterId"); } set { ExportSearchIntegers.Set(this, "afterId", value); } }

        internal void ValidateForSend()
        {
            ExportSearchIntegers.Read(this, "afterId");
        }
    }
}
