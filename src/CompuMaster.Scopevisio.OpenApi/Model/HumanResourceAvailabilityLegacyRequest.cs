using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit epoch range for the deprecated personnel availability operation.</summary>
    /// <remarks>Typed null setters omit fields. Inherited indexers retain explicit JSON null and future fields. Dates remain original Unix epoch milliseconds; only the server applies year/month semantics. The modern v2 workflow requires separately selected month values.</remarks>
    [Obsolete("Use HumanResourceAvailabilityRequest with GetHumanResourceAvailability for the modern v2 month workflow.")]
    public class HumanResourceAvailabilityLegacyRequest : JObject
    {
        /// <summary>Initializes an empty request without range or search defaults.</summary>
        public HumanResourceAvailabilityLegacyRequest() { }

        /// <summary>Initializes a request by deeply copying the complete source object.</summary>
        /// <param name="other">The original JSON object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public HumanResourceAvailabilityLegacyRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the required original range start in Unix epoch milliseconds.</summary>
        public long? ValidFrom
        {
            get { return Value<long?>("validFrom"); }
            set { if (value.HasValue) this["validFrom"] = new JValue(value.Value); else Remove("validFrom"); }
        }

        /// <summary>Gets or sets the required original range end in Unix epoch milliseconds.</summary>
        public long? ValidTill
        {
            get { return Value<long?>("validTill"); }
            set { if (value.HasValue) this["validTill"] = new JValue(value.Value); else Remove("validTill"); }
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
