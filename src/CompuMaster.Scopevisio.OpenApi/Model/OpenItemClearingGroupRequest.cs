using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original clearing document selections without inferring matching items.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemClearingGroupRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemClearingGroupRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemClearingGroupRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string DocumentNumber
        {
            get { return Value<string>("documentNumber"); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original documents selection.</summary>
        /// <value>The complete copied optional document array, including empty arrays, or null to omit it.</value>
        public JArray Documents
        {
            get { return (JArray)(this["documents"] as JArray)?.DeepClone(); }
            set { if (value != null) this["documents"] = value.DeepClone(); else Remove("documents"); }
        }

    }
}
