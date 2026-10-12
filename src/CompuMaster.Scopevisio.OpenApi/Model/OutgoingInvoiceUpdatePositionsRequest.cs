using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original outgoing-invoice position selections values.</summary>
    /// <remarks>Typed null setters omit fields. Raw indexers preserve unknown fields and optional explicit JSON null. Source objects and nested values are defensive copies. Known schema requirements are validated before transport; actual invoice behavior remains server-authoritative.</remarks>
    public class OutgoingInvoiceUpdatePositionsRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing server defaults.</summary>
        public OutgoingInvoiceUpdatePositionsRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public OutgoingInvoiceUpdatePositionsRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original copyProductsToPositions value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? CopyProductsToPositions
        {
            get { var token = this["copyProductsToPositions"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["copyProductsToPositions"] = new JValue(value); else Remove("copyProductsToPositions"); }
        }
        /// <summary>Gets or sets the original positions value.</summary>
        /// <value>The original required complete copied array. No schema minimum count is inserted.</value>
        public JArray Positions
        {
            get { return (JArray)(this["positions"] as JArray )?.DeepClone(); }
            set { if (value != null) this["positions"] = value.DeepClone(); else Remove("positions"); }
        }
    }
}
