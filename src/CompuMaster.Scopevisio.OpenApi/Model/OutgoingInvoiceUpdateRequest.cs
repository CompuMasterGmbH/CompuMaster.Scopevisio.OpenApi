using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original outgoing-invoice update values.</summary>
    /// <remarks>Typed null setters omit fields. Raw indexers preserve unknown fields and optional explicit JSON null. Source objects and nested values are defensive copies. Known schema requirements are validated before transport; actual invoice behavior remains server-authoritative.</remarks>
    public class OutgoingInvoiceUpdateRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing server defaults.</summary>
        public OutgoingInvoiceUpdateRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public OutgoingInvoiceUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original projectId value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? ProjectId
        {
            get { var token = this["projectId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["projectId"] = new JValue(value); else Remove("projectId"); }
        }
        /// <summary>Gets or sets the original customerContactId value.</summary>
        /// <value>The original required Int64 value. The schema requires this even though the prose makes it conditional on project settings.</value>
        public long? CustomerContactId
        {
            get { var token = this["customerContactId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["customerContactId"] = new JValue(value); else Remove("customerContactId"); }
        }
        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The original optional string.</value>
        public string Name
        {
            get { var token = this["name"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["name"] = new JValue(value); else Remove("name"); }
        }
        /// <summary>Gets or sets the original text value.</summary>
        /// <value>The original optional string.</value>
        public string Text
        {
            get { var token = this["text"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["text"] = new JValue(value); else Remove("text"); }
        }
        /// <summary>Gets or sets the original customerContactPerson value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? CustomerContactPerson
        {
            get { var token = this["customerContactPerson"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["customerContactPerson"] = new JValue(value); else Remove("customerContactPerson"); }
        }
        /// <summary>Gets or sets the original documentDate value.</summary>
        /// <value>The original required Int64 value. Timestamps are Unix milliseconds and are not converted. The schema requires this even though creation-oriented prose describes an omitted current-date default.</value>
        public long? DocumentDate
        {
            get { var token = this["documentDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["documentDate"] = new JValue(value); else Remove("documentDate"); }
        }
        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The original optional string.</value>
        public string DocumentNumber
        {
            get { var token = this["documentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }
        /// <summary>Gets or sets the original ourContactPerson value.</summary>
        /// <value>The original optional string.</value>
        public string OurContactPerson
        {
            get { var token = this["ourContactPerson"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["ourContactPerson"] = new JValue(value); else Remove("ourContactPerson"); }
        }
        /// <summary>Gets or sets the original deliveryType value.</summary>
        /// <value>The original optional string.</value>
        public string DeliveryType
        {
            get { var token = this["deliveryType"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["deliveryType"] = new JValue(value); else Remove("deliveryType"); }
        }
        /// <summary>Gets or sets the original deliveryCosts value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? DeliveryCosts
        {
            get { var token = this["deliveryCosts"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["deliveryCosts"] = new JValue(value); else Remove("deliveryCosts"); }
        }
        /// <summary>Gets or sets the original cashOnDelivery value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? CashOnDelivery
        {
            get { var token = this["cashOnDelivery"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["cashOnDelivery"] = new JValue(value); else Remove("cashOnDelivery"); }
        }
        /// <summary>Gets or sets the original textTitle value.</summary>
        /// <value>The original optional string.</value>
        public string TextTitle
        {
            get { var token = this["textTitle"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["textTitle"] = new JValue(value); else Remove("textTitle"); }
        }
        /// <summary>Gets or sets the original textSubTitle value.</summary>
        /// <value>The original optional string.</value>
        public string TextSubTitle
        {
            get { var token = this["textSubTitle"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["textSubTitle"] = new JValue(value); else Remove("textSubTitle"); }
        }
        /// <summary>Gets or sets the original textIntro value.</summary>
        /// <value>The original optional string.</value>
        public string TextIntro
        {
            get { var token = this["textIntro"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["textIntro"] = new JValue(value); else Remove("textIntro"); }
        }
        /// <summary>Gets or sets the original textExtro value.</summary>
        /// <value>The original optional string.</value>
        public string TextExtro
        {
            get { var token = this["textExtro"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["textExtro"] = new JValue(value); else Remove("textExtro"); }
        }
        /// <summary>Gets or sets the original selectedAddress value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? SelectedAddress
        {
            get { var token = this["selectedAddress"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["selectedAddress"] = new JValue(value); else Remove("selectedAddress"); }
        }
        /// <summary>Gets or sets the original otherAddress value.</summary>
        /// <value>The original optional string.</value>
        public string OtherAddress
        {
            get { var token = this["otherAddress"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["otherAddress"] = new JValue(value); else Remove("otherAddress"); }
        }
        /// <summary>Gets or sets the original gross value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? Gross
        {
            get { var token = this["gross"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["gross"] = new JValue(value); else Remove("gross"); }
        }
        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? PermissionType
        {
            get { var token = this["permissionType"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["permissionType"] = new JValue(value); else Remove("permissionType"); }
        }
        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The original optional string.</value>
        public string ReadPermissionProfiles
        {
            get { var token = this["readPermissionProfiles"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["readPermissionProfiles"] = new JValue(value); else Remove("readPermissionProfiles"); }
        }
        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The original optional string.</value>
        public string ReadPermissionLogins
        {
            get { var token = this["readPermissionLogins"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["readPermissionLogins"] = new JValue(value); else Remove("readPermissionLogins"); }
        }
        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The original optional string.</value>
        public string WritePermissionProfiles
        {
            get { var token = this["writePermissionProfiles"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["writePermissionProfiles"] = new JValue(value); else Remove("writePermissionProfiles"); }
        }
        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The original optional string.</value>
        public string WritePermissionLogins
        {
            get { var token = this["writePermissionLogins"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["writePermissionLogins"] = new JValue(value); else Remove("writePermissionLogins"); }
        }
        /// <summary>Gets or sets the original customFields value.</summary>
        /// <value>The original optional complete copied array.</value>
        public JArray CustomFields
        {
            get { return (JArray)(this["customFields"] as JArray )?.DeepClone(); }
            set { if (value != null) this["customFields"] = value.DeepClone(); else Remove("customFields"); }
        }
        /// <summary>Gets or sets the original positionsForm value.</summary>
        /// <value>The original optional complete copied object.</value>
        public JObject PositionsForm
        {
            get { return (JObject)(this["positionsForm"] as JObject )?.DeepClone(); }
            set { if (value != null) this["positionsForm"] = value.DeepClone(); else Remove("positionsForm"); }
        }
        /// <summary>Gets or sets the original deliveryDate value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? DeliveryDate
        {
            get { var token = this["deliveryDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["deliveryDate"] = new JValue(value); else Remove("deliveryDate"); }
        }
        /// <summary>Gets or sets the original deliveryDateTo value.</summary>
        /// <value>The original optional Int64 value.</value>
        public long? DeliveryDateTo
        {
            get { var token = this["deliveryDateTo"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["deliveryDateTo"] = new JValue(value); else Remove("deliveryDateTo"); }
        }
        /// <summary>Gets or sets the original isEndDiscountAbsolute value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? IsEndDiscountAbsolute
        {
            get { var token = this["isEndDiscountAbsolute"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["isEndDiscountAbsolute"] = new JValue(value); else Remove("isEndDiscountAbsolute"); }
        }
        /// <summary>Gets or sets the original endDiscount value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? EndDiscount
        {
            get { var token = this["endDiscount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["endDiscount"] = new JValue(value); else Remove("endDiscount"); }
        }
        /// <summary>Gets or sets the original paymentTypeName value.</summary>
        /// <value>The original optional string. Original spelling and future values remain server-validated.</value>
        public string PaymentTypeName
        {
            get { var token = this["paymentTypeName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["paymentTypeName"] = new JValue(value); else Remove("paymentTypeName"); }
        }
        /// <summary>Gets or sets the original isPaid value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? IsPaid
        {
            get { var token = this["isPaid"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["isPaid"] = new JValue(value); else Remove("isPaid"); }
        }
        /// <summary>Gets or sets the original discountDate1 value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? DiscountDate1
        {
            get { var token = this["discountDate1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["discountDate1"] = new JValue(value); else Remove("discountDate1"); }
        }
        /// <summary>Gets or sets the original discountDate2 value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? DiscountDate2
        {
            get { var token = this["discountDate2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["discountDate2"] = new JValue(value); else Remove("discountDate2"); }
        }
        /// <summary>Gets or sets the original dueDate value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? DueDate
        {
            get { var token = this["dueDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["dueDate"] = new JValue(value); else Remove("dueDate"); }
        }
        /// <summary>Gets or sets the original discountPercent1 value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? DiscountPercent1
        {
            get { var token = this["discountPercent1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountPercent1"] = new JValue(value); else Remove("discountPercent1"); }
        }
        /// <summary>Gets or sets the original discountPercent2 value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? DiscountPercent2
        {
            get { var token = this["discountPercent2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountPercent2"] = new JValue(value); else Remove("discountPercent2"); }
        }
        /// <summary>Gets or sets the original explicitDiscount value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? ExplicitDiscount
        {
            get { var token = this["explicitDiscount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["explicitDiscount"] = new JValue(value); else Remove("explicitDiscount"); }
        }
        /// <summary>Gets or sets the original discountAmount1 value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? DiscountAmount1
        {
            get { var token = this["discountAmount1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountAmount1"] = new JValue(value); else Remove("discountAmount1"); }
        }
        /// <summary>Gets or sets the original discountAmount2 value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? DiscountAmount2
        {
            get { var token = this["discountAmount2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountAmount2"] = new JValue(value); else Remove("discountAmount2"); }
        }
        /// <summary>Gets or sets the original orderNumber value.</summary>
        /// <value>The original optional string.</value>
        public string OrderNumber
        {
            get { var token = this["orderNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["orderNumber"] = new JValue(value); else Remove("orderNumber"); }
        }
        /// <summary>Gets or sets the original zoneNumber value.</summary>
        /// <value>The original optional string.</value>
        public string ZoneNumber
        {
            get { var token = this["zoneNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["zoneNumber"] = new JValue(value); else Remove("zoneNumber"); }
        }
        /// <summary>Gets or sets the original postingDate value.</summary>
        /// <value>The original optional Int64 value. Timestamps are Unix milliseconds and are not converted.</value>
        public long? PostingDate
        {
            get { var token = this["postingDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["postingDate"] = new JValue(value); else Remove("postingDate"); }
        }
        /// <summary>Gets or sets the original documentText value.</summary>
        /// <value>The original optional string.</value>
        public string DocumentText
        {
            get { var token = this["documentText"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentText"] = new JValue(value); else Remove("documentText"); }
        }
        /// <summary>Gets or sets the original locale value.</summary>
        /// <value>The original optional string.</value>
        public string Locale
        {
            get { var token = this["locale"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["locale"] = new JValue(value); else Remove("locale"); }
        }
        /// <summary>Gets or sets the original currency value.</summary>
        /// <value>The original optional string.</value>
        public string Currency
        {
            get { var token = this["currency"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["currency"] = new JValue(value); else Remove("currency"); }
        }
        /// <summary>Gets or sets the original exchangeRate value.</summary>
        /// <value>The original optional finite numeric value.</value>
        public decimal? ExchangeRate
        {
            get { var token = this["exchangeRate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["exchangeRate"] = new JValue(value); else Remove("exchangeRate"); }
        }
        /// <summary>Gets or sets the original reference value.</summary>
        /// <value>The original optional string.</value>
        public string Reference
        {
            get { var token = this["reference"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["reference"] = new JValue(value); else Remove("reference"); }
        }
        /// <summary>Gets or sets the original taxCountryCodeIso2 value.</summary>
        /// <value>The original optional string.</value>
        public string TaxCountryCodeIso2
        {
            get { var token = this["taxCountryCodeIso2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["taxCountryCodeIso2"] = new JValue(value); else Remove("taxCountryCodeIso2"); }
        }
        /// <summary>Gets or sets the original isIntercompany value.</summary>
        /// <value>The original optional server flag.</value>
        public bool? IsIntercompany
        {
            get { var token = this["isIntercompany"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["isIntercompany"] = new JValue(value); else Remove("isIntercompany"); }
        }
    }
}
