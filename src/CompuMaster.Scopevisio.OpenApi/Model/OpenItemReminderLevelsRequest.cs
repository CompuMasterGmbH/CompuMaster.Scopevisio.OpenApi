using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit per-document reminder-level updates.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemReminderLevelsRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemReminderLevelsRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemReminderLevelsRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original openItems selection.</summary>
        /// <value>The complete copied required item array, including explicit empty arrays. No schema minimum is imposed; each item requires original documentNumber and Int32 reminderLevel.</value>
        public JArray OpenItems
        {
            get { return (JArray)(this["openItems"] as JArray)?.DeepClone(); }
            set { if (value != null) this["openItems"] = value.DeepClone(); else Remove("openItems"); }
        }

    }
}
