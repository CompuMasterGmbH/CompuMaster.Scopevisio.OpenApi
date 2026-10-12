using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase order creation values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchaseOrderCreationRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchaseOrderCreationRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchaseOrderCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original vendor value.</summary>
        /// <value>The original complete copied object. This field is required when its containing object is supplied.</value>
        public JObject Vendor
        {
            get { return (JObject)(this["vendor"] as JObject)?.DeepClone(); }
            set { if (value != null) this["vendor"] = value.DeepClone(); else Remove("vendor"); }
        }
        /// <summary>Gets or sets the original purchaseOrderDate value.</summary>
        /// <value>The original string. This field is required when its containing object is supplied. The schema says yyyy-MM-dd while prose also says dd.MM.yyyy; original text is retained for server validation.</value>
        public string PurchaseOrderDate
        {
            get { var token = this["purchaseOrderDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["purchaseOrderDate"] = new JValue(value); else Remove("purchaseOrderDate"); }
        }
        /// <summary>Gets or sets the original externalPurchaseOrderNumber value.</summary>
        /// <value>The original string.</value>
        public string ExternalPurchaseOrderNumber
        {
            get { var token = this["externalPurchaseOrderNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalPurchaseOrderNumber"] = new JValue(value); else Remove("externalPurchaseOrderNumber"); }
        }
        /// <summary>Gets or sets the original materialPurchaseOrderPositions value.</summary>
        /// <value>The original complete copied array. This field is required when its containing object is supplied.</value>
        public JArray MaterialPurchaseOrderPositions
        {
            get { return (JArray)(this["materialPurchaseOrderPositions"] as JArray)?.DeepClone(); }
            set { if (value != null) this["materialPurchaseOrderPositions"] = value.DeepClone(); else Remove("materialPurchaseOrderPositions"); }
        }
    }
}
