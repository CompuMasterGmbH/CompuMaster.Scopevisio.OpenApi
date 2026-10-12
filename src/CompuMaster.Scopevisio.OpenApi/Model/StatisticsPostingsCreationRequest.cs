using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly selected statistics posting creation and optional account creation.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers retain unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class StatisticsPostingsCreationRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults or automatic selections.</summary>
        public StatisticsPostingsCreationRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public StatisticsPostingsCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original autoCreateAccount selection.</summary>
        /// <value>The explicit automatic account-creation choice, or null to omit it. The SDK never enables creation automatically.</value>
        public bool? AutoCreateAccount
        {
            get { return Value<bool?>("autoCreateAccount"); }
            set { if (value.HasValue) this["autoCreateAccount"] = new JValue(value.Value); else Remove("autoCreateAccount"); }
        }

        /// <summary>Gets or sets the original autoAccountDecimal selection.</summary>
        /// <value>The optional original Int64 decimal-digit selection for automatic accounts, or null to omit it. The server validates usable ranges.</value>
        public long? AutoAccountDecimal
        {
            get { return Value<long?>("autoAccountDecimal"); }
            set { if (value.HasValue) this["autoAccountDecimal"] = new JValue(value.Value); else Remove("autoAccountDecimal"); }
        }

        /// <summary>Gets or sets the original statisticsPostings selection.</summary>
        /// <value>The complete copied optional statistics-posting array, including explicit empty arrays. No schema minimum is imposed.</value>
        public JArray StatisticsPostings
        {
            get { return (JArray)(this["statisticsPostings"] as JArray)?.DeepClone(); }
            set { if (value != null) this["statisticsPostings"] = value.DeepClone(); else Remove("statisticsPostings"); }
        }

    }
}
