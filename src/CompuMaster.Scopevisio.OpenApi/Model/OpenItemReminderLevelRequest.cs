using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicitly selected reminder level for an original document number.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemReminderLevelRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemReminderLevelRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemReminderLevelRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber selection.</summary>
        /// <value>The required original document-number string, retaining leading zeros and empty text for server validation.</value>
        public string DocumentNumber
        {
            get { return Value<string>("documentNumber"); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original reminderLevel selection.</summary>
        /// <value>The required original Int32 reminder-level selection. It must exist in the server's accounting area; no local range or level lookup is inferred.</value>
        public int? ReminderLevel
        {
            get { return Value<int?>("reminderLevel"); }
            set { if (value.HasValue) this["reminderLevel"] = new JValue(value.Value); else Remove("reminderLevel"); }
        }

    }
}
