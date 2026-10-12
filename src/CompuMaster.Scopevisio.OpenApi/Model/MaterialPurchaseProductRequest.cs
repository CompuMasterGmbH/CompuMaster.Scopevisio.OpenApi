using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase product values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchaseProductRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchaseProductRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchaseProductRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original productId value.</summary>
        /// <value>The original Int64 value.</value>
        public long? ProductId
        {
            get { var token = this["productId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["productId"] = new JValue(value); else Remove("productId"); }
        }
        /// <summary>Gets or sets the original externalSystemProductId value.</summary>
        /// <value>The original string.</value>
        public string ExternalSystemProductId
        {
            get { var token = this["externalSystemProductId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalSystemProductId"] = new JValue(value); else Remove("externalSystemProductId"); }
        }
        /// <summary>Gets or sets the original productName value.</summary>
        /// <value>The original string.</value>
        public string ProductName
        {
            get { var token = this["productName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["productName"] = new JValue(value); else Remove("productName"); }
        }
        /// <summary>Gets or sets the original productNumber value.</summary>
        /// <value>The original string.</value>
        public string ProductNumber
        {
            get { var token = this["productNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["productNumber"] = new JValue(value); else Remove("productNumber"); }
        }
        /// <summary>Gets or sets the original vendorProductNumber value.</summary>
        /// <value>The original string.</value>
        public string VendorProductNumber
        {
            get { var token = this["vendorProductNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["vendorProductNumber"] = new JValue(value); else Remove("vendorProductNumber"); }
        }
        /// <summary>Gets or sets the original thirdPartyProductNumber value.</summary>
        /// <value>The original string.</value>
        public string ThirdPartyProductNumber
        {
            get { var token = this["thirdPartyProductNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["thirdPartyProductNumber"] = new JValue(value); else Remove("thirdPartyProductNumber"); }
        }
        /// <summary>Gets or sets the original productGtin value.</summary>
        /// <value>The original string.</value>
        public string ProductGtin
        {
            get { var token = this["productGtin"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["productGtin"] = new JValue(value); else Remove("productGtin"); }
        }
        /// <summary>Gets or sets the original vendorProductGtin value.</summary>
        /// <value>The original string.</value>
        public string VendorProductGtin
        {
            get { var token = this["vendorProductGtin"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["vendorProductGtin"] = new JValue(value); else Remove("vendorProductGtin"); }
        }
    }
}
