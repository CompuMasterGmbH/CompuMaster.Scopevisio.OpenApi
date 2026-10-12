using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly selected clearing groups for creditor or debtor open items.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemClearingRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemClearingRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemClearingRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original clearings selection.</summary>
        /// <value>The complete copied optional clearing-group array, including explicit empty arrays. No schema minimum is imposed.</value>
        public JArray Clearings
        {
            get { return (JArray)(this["clearings"] as JArray)?.DeepClone(); }
            set { if (value != null) this["clearings"] = value.DeepClone(); else Remove("clearings"); }
        }

    }
}
