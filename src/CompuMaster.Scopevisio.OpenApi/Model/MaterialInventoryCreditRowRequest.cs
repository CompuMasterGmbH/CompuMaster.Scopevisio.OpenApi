using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material inventory credit row values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialInventoryCreditRowRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialInventoryCreditRowRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialInventoryCreditRowRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original productId value.</summary>
        /// <value>The original Int64 value. At least one product ID or number is supplied; when both are supplied the server gives the ID precedence.</value>
        public long? ProductId
        {
            get { var token = this["productId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["productId"] = new JValue(value); else Remove("productId"); }
        }
        /// <summary>Gets or sets the original productNumber value.</summary>
        /// <value>The original string. The server uses this when no product ID is supplied, despite the contradictory schema requiring both.</value>
        public string ProductNumber
        {
            get { var token = this["productNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["productNumber"] = new JValue(value); else Remove("productNumber"); }
        }
        /// <summary>Gets or sets the original quantity value.</summary>
        /// <value>The original finite numeric value. This field is required when its containing object is supplied. The declared minimum is zero; negative nonzero quantities or prices are rejected without rounding.</value>
        public decimal? Quantity
        {
            get { var token = this["quantity"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["quantity"] = new JValue(value); else Remove("quantity"); }
        }
        /// <summary>Gets or sets the original warehouseId value.</summary>
        /// <value>The original Int64 value. Omission retains the server's default warehouse.</value>
        public long? WarehouseId
        {
            get { var token = this["warehouseId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["warehouseId"] = new JValue(value); else Remove("warehouseId"); }
        }
        /// <summary>Gets or sets the original contactId value.</summary>
        /// <value>The original Int64 value. The manufacturer marks this field deprecated; it remains available for explicit callers.</value>
        public long? ContactId
        {
            get { var token = this["contactId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["contactId"] = new JValue(value); else Remove("contactId"); }
        }
        /// <summary>Gets or sets the original amount value.</summary>
        /// <value>The original finite numeric value. The server resolves amount/price consistency and original quantity; no client multiplication or delivery-price default is imposed.</value>
        public decimal? Amount
        {
            get { var token = this["amount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["amount"] = new JValue(value); else Remove("amount"); }
        }
        /// <summary>Gets or sets the original price value.</summary>
        /// <value>The original finite numeric value. The server resolves amount/price consistency and original quantity; no client multiplication or delivery-price default is imposed.</value>
        public decimal? Price
        {
            get { var token = this["price"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["price"] = new JValue(value); else Remove("price"); }
        }
        /// <summary>Gets or sets the original projectId value.</summary>
        /// <value>The original Int64 value.</value>
        public long? ProjectId
        {
            get { var token = this["projectId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["projectId"] = new JValue(value); else Remove("projectId"); }
        }
        /// <summary>Gets or sets the original dimension1 value.</summary>
        /// <value>The original Int64 value.</value>
        public long? Dimension1
        {
            get { var token = this["dimension1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dimension1"] = new JValue(value); else Remove("dimension1"); }
        }
        /// <summary>Gets or sets the original dimension2 value.</summary>
        /// <value>The original Int64 value.</value>
        public long? Dimension2
        {
            get { var token = this["dimension2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dimension2"] = new JValue(value); else Remove("dimension2"); }
        }
        /// <summary>Gets or sets the original dimension3 value.</summary>
        /// <value>The original Int64 value.</value>
        public long? Dimension3
        {
            get { var token = this["dimension3"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dimension3"] = new JValue(value); else Remove("dimension3"); }
        }
        /// <summary>Gets or sets the original postingTs value.</summary>
        /// <value>The original Int64 value.</value>
        public long? PostingTs
        {
            get { var token = this["postingTs"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["postingTs"] = new JValue(value); else Remove("postingTs"); }
        }
    }
}
