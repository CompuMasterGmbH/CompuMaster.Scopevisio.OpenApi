using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original outgoing-invoice custom-field values.</summary>
    /// <remarks>Typed null setters omit fields. Raw indexers preserve unknown fields and optional explicit JSON null. Source objects and nested values are defensive copies. Known schema requirements are validated before transport; actual invoice behavior remains server-authoritative.</remarks>
    public class OutgoingInvoiceUpdateCustomFieldRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing server defaults.</summary>
        public OutgoingInvoiceUpdateCustomFieldRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public OutgoingInvoiceUpdateCustomFieldRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original customFieldName value.</summary>
        /// <value>The original required string.</value>
        public string CustomFieldName
        {
            get { var token = this["customFieldName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["customFieldName"] = new JValue(value); else Remove("customFieldName"); }
        }
        /// <summary>Gets or sets the original customFieldValue value.</summary>
        /// <value>The original required complete copied JSON value. The schema declares object while examples use strings; complete original JSON is retained without coercion.</value>
        public JToken CustomFieldValue
        {
            get { return (JToken)(this["customFieldValue"] )?.DeepClone(); }
            set { if (value != null) this["customFieldValue"] = value.DeepClone(); else Remove("customFieldValue"); }
        }
    }
}
