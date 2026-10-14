using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit posting import rows and server VAT/tax options.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public sealed class PostingImportRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingImportRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original adjustVatKey value.</summary>
        /// <value>The optional explicit server choice to replace supplied VAT keys with chart-of-accounts keys. Omission remains distinct from false or true; the client performs no VAT lookup or replacement.</value>
        public bool? AdjustVatKey
        {
            get { var token = this["adjustVatKey"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["adjustVatKey"] = new JValue(value); else Remove("adjustVatKey"); }
        }

        /// <summary>Gets or sets the original autoCreateTax value.</summary>
        /// <value>The optional explicit server choice to generate tax posting rows. Omission remains distinct from false or true; the client synthesizes no tax or balancing rows.</value>
        public bool? AutoCreateTax
        {
            get { var token = this["autoCreateTax"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["autoCreateTax"] = new JValue(value); else Remove("autoCreateTax"); }
        }

        /// <summary>Gets or sets the original rows value.</summary>
        /// <value>The complete copied optional posting import rows; actual posting validity and the two-row requirement remain server-enforced.</value>
        public JArray Rows
        {
            get { return (JArray)(this["rows"] as JArray)?.DeepClone(); }
            set { if (value != null) this["rows"] = value.DeepClone(); else Remove("rows"); }
        }
    }
}
