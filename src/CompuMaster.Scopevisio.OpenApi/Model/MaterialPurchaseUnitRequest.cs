using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase unit values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchaseUnitRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchaseUnitRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchaseUnitRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original measurementUnitId value.</summary>
        /// <value>The original Int64 value.</value>
        public long? MeasurementUnitId
        {
            get { var token = this["measurementUnitId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["measurementUnitId"] = new JValue(value); else Remove("measurementUnitId"); }
        }
        /// <summary>Gets or sets the original deliveryUnitId value.</summary>
        /// <value>The original Int64 value.</value>
        public long? DeliveryUnitId
        {
            get { var token = this["deliveryUnitId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["deliveryUnitId"] = new JValue(value); else Remove("deliveryUnitId"); }
        }
        /// <summary>Gets or sets the original externalSystemUnitId value.</summary>
        /// <value>The original string.</value>
        public string ExternalSystemUnitId
        {
            get { var token = this["externalSystemUnitId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalSystemUnitId"] = new JValue(value); else Remove("externalSystemUnitId"); }
        }
        /// <summary>Gets or sets the original unitName value.</summary>
        /// <value>The original string.</value>
        public string UnitName
        {
            get { var token = this["unitName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["unitName"] = new JValue(value); else Remove("unitName"); }
        }
        /// <summary>Gets or sets the original thirdPartyUnitId value.</summary>
        /// <value>The original string.</value>
        public string ThirdPartyUnitId
        {
            get { var token = this["thirdPartyUnitId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["thirdPartyUnitId"] = new JValue(value); else Remove("thirdPartyUnitId"); }
        }
        /// <summary>Gets or sets the original thirdPartyUnitName value.</summary>
        /// <value>The original string.</value>
        public string ThirdPartyUnitName
        {
            get { var token = this["thirdPartyUnitName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["thirdPartyUnitName"] = new JValue(value); else Remove("thirdPartyUnitName"); }
        }
    }
}
