using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an original XML filter import with an explicit existing-filter update choice.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers retain unknown fields and explicit optional JSON null. The body snapshots before HTTP I/O and server business constraints remain authoritative.</remarks>
    public class FilterImportRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting default selections.</summary>
        public FilterImportRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public FilterImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original data selection.</summary>
        /// <value>The required original XML string when a body is supplied, including empty text. The server validates XML and filter/import semantics; the SDK does not read files or parse/re-encode content.</value>
        public string Data { get { return Value<string>("data"); } set { if (value != null) this["data"] = new JValue(value); else Remove("data"); } }

        /// <summary>Gets or sets the original updateExistingFilters selection.</summary>
        /// <value>The explicit name-matching update choice, or null to omit it and retain the documented server default false.</value>
        public bool? UpdateExistingFilters { get { return Value<bool?>("updateExistingFilters"); } set { if (value.HasValue) this["updateExistingFilters"] = new JValue(value.Value); else Remove("updateExistingFilters"); } }

    }
}
