using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit epoch range and optional type for the deprecated project revenue search.</summary>
    /// <remarks>Dates and nullable copied search objects follow the inherited legacy range contract. An absent revenue type preserves the server planning default without inserting a value. Modern v2 uses separately selected date strings.</remarks>
    [Obsolete("Use ProjectRevenuesSearchRequest with SearchProjectRevenues for the modern v2 date-string workflow.")]
    public class ProjectRevenuesLegacySearchRequest : ProjectResourcesLegacySearchRequest
    {
        /// <summary>Initializes an empty request without range, search or revenue defaults.</summary>
        public ProjectRevenuesLegacySearchRequest() { }

        /// <summary>Initializes a request by deeply copying the complete source object.</summary>
        /// <param name="other">The original JSON object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProjectRevenuesLegacySearchRequest(JObject other) : base(other) { }

        /// <summary>Gets or sets the optional explicit revenue type, or null to omit it.</summary>
        /// <remarks>The declared values are planning, realization and deviation. The API validates explicitly supplied JSON integer choices; explicit JSON null is not a declared revenue type.</remarks>
        public ProjectRevenueType? RevenueType
        {
            get { var value = Value<int?>("revenueType"); return value.HasValue ? (ProjectRevenueType?)value.Value : null; }
            set { if (value.HasValue) this["revenueType"] = new JValue((int)value.Value); else Remove("revenueType"); }
        }
    }
}
