using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material purchase delivery creation values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialPurchaseDeliveryCreationRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialPurchaseDeliveryCreationRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialPurchaseDeliveryCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original vendor value.</summary>
        /// <value>The original complete copied object. This field is required when its containing object is supplied.</value>
        public JObject Vendor
        {
            get { return (JObject)(this["vendor"] as JObject)?.DeepClone(); }
            set { if (value != null) this["vendor"] = value.DeepClone(); else Remove("vendor"); }
        }
        /// <summary>Gets or sets the original purchaseDeliveryDate value.</summary>
        /// <value>The original string. This field is required when its containing object is supplied. The schema says yyyy-MM-dd while prose also says dd.MM.yyyy; original text is retained for server validation.</value>
        public string PurchaseDeliveryDate
        {
            get { var token = this["purchaseDeliveryDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["purchaseDeliveryDate"] = new JValue(value); else Remove("purchaseDeliveryDate"); }
        }
        /// <summary>Gets or sets the original externalPurchaseDeliveryNumber value.</summary>
        /// <value>The original string.</value>
        public string ExternalPurchaseDeliveryNumber
        {
            get { var token = this["externalPurchaseDeliveryNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalPurchaseDeliveryNumber"] = new JValue(value); else Remove("externalPurchaseDeliveryNumber"); }
        }
        /// <summary>Gets or sets the original enableAutomapping value.</summary>
        /// <value>The original explicit server flag. Omission retains server false and no orders are mapped in the client.</value>
        public bool? EnableAutomapping
        {
            get { var token = this["enableAutomapping"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["enableAutomapping"] = new JValue(value); else Remove("enableAutomapping"); }
        }
        /// <summary>Gets or sets the original referencedPurchaseOrderNumbers value.</summary>
        /// <value>The original complete copied array.</value>
        public JArray ReferencedPurchaseOrderNumbers
        {
            get { return (JArray)(this["referencedPurchaseOrderNumbers"] as JArray)?.DeepClone(); }
            set { if (value != null) this["referencedPurchaseOrderNumbers"] = value.DeepClone(); else Remove("referencedPurchaseOrderNumbers"); }
        }
        /// <summary>Gets or sets the original generatePurchaseOrderForRemainingValue value.</summary>
        /// <value>The original explicit server flag. The server requires automapping; the client never enables it implicitly.</value>
        public bool? GeneratePurchaseOrderForRemainingValue
        {
            get { var token = this["generatePurchaseOrderForRemainingValue"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["generatePurchaseOrderForRemainingValue"] = new JValue(value); else Remove("generatePurchaseOrderForRemainingValue"); }
        }
        /// <summary>Gets or sets the original postingDate value.</summary>
        /// <value>The original string. The schema says yyyy-MM-dd while prose also says dd.MM.yyyy; original text is retained for server validation.</value>
        public string PostingDate
        {
            get { var token = this["postingDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["postingDate"] = new JValue(value); else Remove("postingDate"); }
        }
        /// <summary>Gets or sets the original postDelivery value.</summary>
        /// <value>The original explicit server flag. An explicit true asks the server to post using the supplied posting date; omission never enables posting in the client.</value>
        public bool? PostDelivery
        {
            get { var token = this["postDelivery"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["postDelivery"] = new JValue(value); else Remove("postDelivery"); }
        }
        /// <summary>Gets or sets the original materialPurchaseDeliveryPositions value.</summary>
        /// <value>The original complete copied array. This field is required when its containing object is supplied.</value>
        public JArray MaterialPurchaseDeliveryPositions
        {
            get { return (JArray)(this["materialPurchaseDeliveryPositions"] as JArray)?.DeepClone(); }
            set { if (value != null) this["materialPurchaseDeliveryPositions"] = value.DeepClone(); else Remove("materialPurchaseDeliveryPositions"); }
        }
    }
}
