using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an original open-item document and optional clearing amount.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemClearingDocumentRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemClearingDocumentRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemClearingDocumentRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string DocumentNumber
        {
            get { return Value<string>("documentNumber"); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original clearingAmount selection.</summary>
        /// <value>The original optional finite clearing amount, or null to omit it. Zero/negative values are left for server validation; no local matching, summation or rounding occurs.</value>
        public decimal? ClearingAmount
        {
            get { return Value<decimal?>("clearingAmount"); }
            set { if (value.HasValue) this["clearingAmount"] = new JValue(value.Value); else Remove("clearingAmount"); }
        }

    }
}
