using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit epoch range for the deprecated project resource search.</summary>
    /// <remarks>Typed null setters omit fields. Inherited indexers retain explicit JSON null and future fields. Dates remain original Unix epoch milliseconds; no date conversion or range defaults are inferred. The modern v2 workflow requires separately selected yyyy-MM-dd date strings.</remarks>
    [Obsolete("Use ProjectResourcesSearchRequest with SearchProjectResources for the modern v2 date-string workflow.")]
    public class ProjectResourcesLegacySearchRequest : JObject
    {
        /// <summary>Initializes an empty request without range or search defaults.</summary>
        public ProjectResourcesLegacySearchRequest() { }

        /// <summary>Initializes a request by deeply copying the complete source object.</summary>
        /// <param name="other">The original JSON object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProjectResourcesLegacySearchRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the required original range start in Unix epoch milliseconds.</summary>
        public long? ProjectStartFrom
        {
            get { return Value<long?>("projectStartFrom"); }
            set { if (value.HasValue) this["projectStartFrom"] = new JValue(value.Value); else Remove("projectStartFrom"); }
        }

        /// <summary>Gets or sets the required original range end in Unix epoch milliseconds.</summary>
        public long? ProjectStartTo
        {
            get { return Value<long?>("projectStartTo"); }
            set { if (value.HasValue) this["projectStartTo"] = new JValue(value.Value); else Remove("projectStartTo"); }
        }

        /// <summary>Gets or sets a defensive copy of the optional search object.</summary>
        /// <remarks>A null setter omits the field; inherited indexers can retain explicit JSON null. The server excludes month, monthRevenue and id from search. Caller filters are preserved.</remarks>
        public JObject SearchSpecification
        {
            get { return (JObject)(this["searchSpecification"] as JObject)?.DeepClone(); }
            set { if (value != null) this["searchSpecification"] = value.DeepClone(); else Remove("searchSpecification"); }
        }
    }
}
