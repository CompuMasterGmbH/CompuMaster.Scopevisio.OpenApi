using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase party values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchasePartyRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchasePartyRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchasePartyRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original vendorId value.</summary>
        /// <value>The original Int64 value.</value>
        public long? VendorId
        {
            get { var token = this["vendorId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["vendorId"] = new JValue(value); else Remove("vendorId"); }
        }
        /// <summary>Gets or sets the original externalSystemVendorId value.</summary>
        /// <value>The original string.</value>
        public string ExternalSystemVendorId
        {
            get { var token = this["externalSystemVendorId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalSystemVendorId"] = new JValue(value); else Remove("externalSystemVendorId"); }
        }
    }
}
