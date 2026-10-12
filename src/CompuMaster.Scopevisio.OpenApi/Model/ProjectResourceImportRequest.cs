using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original CSV or JSON records for a project-resource planning import.</summary>
    /// <remarks>The schema supplies no required fields or minimum record count. The server validates usable import content and gives data precedence when alternatives are both supplied. Unknown fields and explicit JSON null remain representable through the inherited object; typed null setters omit fields.</remarks>
    public class ProjectResourceImportRequest : JObject
    {
        /// <summary>Initializes an empty import request without selecting an import representation.</summary>
        public ProjectResourceImportRequest() { }

        /// <summary>Initializes an import request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete request object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public ProjectResourceImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original Base64 CSV data.</summary>
        /// <value>The original string, including empty text, or null to omit it. The server validates Base64 encoding, CSV columns and precedence; the SDK does not read files or re-encode content.</value>
        public string Data { get { return Value<string>("data"); } set { if (value == null) Remove("data"); else this["data"] = value; } }

        /// <summary>Gets or sets the complete original resources array.</summary>
        /// <value>A defensive copy of all JSON records, an explicit empty array, or null to omit it. Known record kinds are validated before I/O; no month/date normalization or amount adjustment is applied.</value>
        public JArray Resources
        {
            get { return (JArray)(this["resources"] as JArray)?.DeepClone(); }
            set { if (value == null) Remove("resources"); else this["resources"] = value.DeepClone(); }
        }
    }
}
