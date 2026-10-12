using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original outgoing-invoice position values.</summary>
    /// <remarks>Typed null setters omit fields. Raw indexers preserve unknown fields and optional explicit JSON null. Source objects and nested values are defensive copies. Known schema requirements are validated before transport; actual invoice behavior remains server-authoritative.</remarks>
    public class OutgoingInvoiceUpdatePositionRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing server defaults.</summary>
        public OutgoingInvoiceUpdatePositionRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public OutgoingInvoiceUpdatePositionRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original typeDisplay value.</summary>
        /// <value>The original optional string. Original spelling and future values remain server-validated.</value>
        public string TypeDisplay
        {
            get { var token = this["typeDisplay"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["typeDisplay"] = new JValue(value); else Remove("typeDisplay"); }
        }
        /// <summary>Gets or sets the original number value.</summary>
        /// <value>The original optional string.</value>
        public string Number
        {
            get { var token = this["number"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["number"] = new JValue(value); else Remove("number"); }
        }
        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The original required string.</value>
        public string Name
        {
            get { var token = this["name"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["name"] = new JValue(value); else Remove("name"); }
        }
        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The original optional string.</value>
        public string Description
        {
            get { var token = this["description"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["description"] = new JValue(value); else Remove("description"); }
        }
        /// <summary>Gets or sets the original quantity value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? Quantity
        {
            get { var token = this["quantity"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["quantity"] = new JValue(value); else Remove("quantity"); }
        }
        /// <summary>Gets or sets the original unit value.</summary>
        /// <value>The original optional string.</value>
        public string Unit
        {
            get { var token = this["unit"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["unit"] = new JValue(value); else Remove("unit"); }
        }
        /// <summary>Gets or sets the original singleAmount value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? SingleAmount
        {
            get { var token = this["singleAmount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["singleAmount"] = new JValue(value); else Remove("singleAmount"); }
        }
        /// <summary>Gets or sets the original account value.</summary>
        /// <value>The original optional string.</value>
        public string Account
        {
            get { var token = this["account"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["account"] = new JValue(value); else Remove("account"); }
        }
        /// <summary>Gets or sets the original vatKey value.</summary>
        /// <value>The original optional string.</value>
        public string VatKey
        {
            get { var token = this["vatKey"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["vatKey"] = new JValue(value); else Remove("vatKey"); }
        }
        /// <summary>Gets or sets the original discount value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? Discount
        {
            get { var token = this["discount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discount"] = new JValue(value); else Remove("discount"); }
        }
        /// <summary>Gets or sets the original freeText1 value.</summary>
        /// <value>The original optional string.</value>
        public string FreeText1
        {
            get { var token = this["freeText1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["freeText1"] = new JValue(value); else Remove("freeText1"); }
        }
        /// <summary>Gets or sets the original freeText2 value.</summary>
        /// <value>The original optional string.</value>
        public string FreeText2
        {
            get { var token = this["freeText2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["freeText2"] = new JValue(value); else Remove("freeText2"); }
        }
        /// <summary>Gets or sets the original singleCostAmount value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? SingleCostAmount
        {
            get { var token = this["singleCostAmount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["singleCostAmount"] = new JValue(value); else Remove("singleCostAmount"); }
        }
        /// <summary>Gets or sets the original valutaDate value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? ValutaDate
        {
            get { var token = this["valutaDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["valutaDate"] = new JValue(value); else Remove("valutaDate"); }
        }
        /// <summary>Gets or sets the original dimension_1 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension1
        {
            get { var token = this["dimension_1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_1"] = new JValue(value); else Remove("dimension_1"); }
        }
        /// <summary>Gets or sets the original dimension_2 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension2
        {
            get { var token = this["dimension_2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_2"] = new JValue(value); else Remove("dimension_2"); }
        }
        /// <summary>Gets or sets the original dimension_3 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension3
        {
            get { var token = this["dimension_3"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_3"] = new JValue(value); else Remove("dimension_3"); }
        }
        /// <summary>Gets or sets the original dimension_4 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension4
        {
            get { var token = this["dimension_4"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_4"] = new JValue(value); else Remove("dimension_4"); }
        }
        /// <summary>Gets or sets the original dimension_5 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension5
        {
            get { var token = this["dimension_5"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_5"] = new JValue(value); else Remove("dimension_5"); }
        }
        /// <summary>Gets or sets the original dimension_6 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension6
        {
            get { var token = this["dimension_6"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_6"] = new JValue(value); else Remove("dimension_6"); }
        }
        /// <summary>Gets or sets the original dimension_7 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension7
        {
            get { var token = this["dimension_7"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_7"] = new JValue(value); else Remove("dimension_7"); }
        }
        /// <summary>Gets or sets the original dimension_8 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension8
        {
            get { var token = this["dimension_8"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_8"] = new JValue(value); else Remove("dimension_8"); }
        }
        /// <summary>Gets or sets the original dimension_9 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension9
        {
            get { var token = this["dimension_9"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_9"] = new JValue(value); else Remove("dimension_9"); }
        }
        /// <summary>Gets or sets the original dimension_10 value.</summary>
        /// <value>The original optional string.</value>
        public string Dimension10
        {
            get { var token = this["dimension_10"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimension_10"] = new JValue(value); else Remove("dimension_10"); }
        }
        /// <summary>Gets or sets the original asIfSold value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? AsIfSold
        {
            get { var token = this["asIfSold"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["asIfSold"] = new JValue(value); else Remove("asIfSold"); }
        }
        /// <summary>Gets or sets the original dateFrom value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? DateFrom
        {
            get { var token = this["dateFrom"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dateFrom"] = new JValue(value); else Remove("dateFrom"); }
        }
        /// <summary>Gets or sets the original dateTo value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? DateTo
        {
            get { var token = this["dateTo"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dateTo"] = new JValue(value); else Remove("dateTo"); }
        }
    }
}
