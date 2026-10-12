using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase position values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchasePositionRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchasePositionRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchasePositionRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original product value.</summary>
        /// <value>The original complete copied object. This field is required when its containing object is supplied.</value>
        public JObject Product
        {
            get { return (JObject)(this["product"] as JObject)?.DeepClone(); }
            set { if (value != null) this["product"] = value.DeepClone(); else Remove("product"); }
        }
        /// <summary>Gets or sets the original quantity value.</summary>
        /// <value>The original finite numeric value. This field is required when its containing object is supplied. The declared minimum is zero; negative nonzero quantities or prices are rejected without rounding.</value>
        public decimal? Quantity
        {
            get { var token = this["quantity"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["quantity"] = new JValue(value); else Remove("quantity"); }
        }
        /// <summary>Gets or sets the original unit value.</summary>
        /// <value>The original complete copied object.</value>
        public JObject Unit
        {
            get { return (JObject)(this["unit"] as JObject)?.DeepClone(); }
            set { if (value != null) this["unit"] = value.DeepClone(); else Remove("unit"); }
        }
        /// <summary>Gets or sets the original price value.</summary>
        /// <value>The original finite numeric value. This field is required when its containing object is supplied. The declared minimum is zero; negative nonzero quantities or prices are rejected without rounding. Original unit and price resolution remain server decisions.</value>
        public decimal? Price
        {
            get { var token = this["price"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["price"] = new JValue(value); else Remove("price"); }
        }
    }
}
