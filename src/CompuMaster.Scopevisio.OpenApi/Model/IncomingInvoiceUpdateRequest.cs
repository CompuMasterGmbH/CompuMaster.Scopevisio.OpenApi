using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit unverified incoming-invoice updates while retaining complete JSON.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve unknown fields and optional JSON null. The server determines omitted-field effects, contact-derived values, payment/discount precedence, permission defaults and deferral behavior from actual invoice state. The SDK does not infer retention or repair existing state.</remarks>
    public class IncomingInvoiceUpdateRequest : JObject
    {
        /// <summary>Initializes an empty update without inserting field or permission defaults.</summary>
        public IncomingInvoiceUpdateRequest() { }

        /// <summary>Initializes an update by deeply copying complete JSON.</summary>
        /// <param name="other">The complete request object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public IncomingInvoiceUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original incomingTs value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? IncomingTs { get { return Value<long?>("incomingTs"); } set { SetOptional("incomingTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original documentDate value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DocumentDate { get { return Value<long?>("documentDate"); } set { SetOptional("documentDate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DocumentNumber { get { return Value<string>("documentNumber"); } set { SetOptional("documentNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original orderNumber value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string OrderNumber { get { return Value<string>("orderNumber"); } set { SetOptional("orderNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerContactId value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? BuyerContactId { get { return Value<long?>("buyerContactId"); } set { SetOptional("buyerContactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deliveryDateFrom value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DeliveryDateFrom { get { return Value<long?>("deliveryDateFrom"); } set { SetOptional("deliveryDateFrom", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deliveryDateTo value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DeliveryDateTo { get { return Value<long?>("deliveryDateTo"); } set { SetOptional("deliveryDateTo", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original text1 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string Text1 { get { return Value<string>("text1"); } set { SetOptional("text1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original taxCountryIsoCode2 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string TaxCountryIsoCode2 { get { return Value<string>("taxCountryIsoCode2"); } set { SetOptional("taxCountryIsoCode2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerName value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerName { get { return Value<string>("buyerName"); } set { SetOptional("buyerName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerId value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerId { get { return Value<string>("buyerId"); } set { SetOptional("buyerId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerContactPersonId value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? BuyerContactPersonId { get { return Value<long?>("buyerContactPersonId"); } set { SetOptional("buyerContactPersonId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original buyerGTIN value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerGTIN { get { return Value<string>("buyerGTIN"); } set { SetOptional("buyerGTIN", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerDepartment value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerDepartment { get { return Value<string>("buyerDepartment"); } set { SetOptional("buyerDepartment", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerZipCode value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerZipCode { get { return Value<string>("buyerZipCode"); } set { SetOptional("buyerZipCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerCity value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerCity { get { return Value<string>("buyerCity"); } set { SetOptional("buyerCity", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerStreet value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerStreet { get { return Value<string>("buyerStreet"); } set { SetOptional("buyerStreet", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerAddressExtra value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerAddressExtra { get { return Value<string>("buyerAddressExtra"); } set { SetOptional("buyerAddressExtra", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original buyerCountry value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string BuyerCountry { get { return Value<string>("buyerCountry"); } set { SetOptional("buyerCountry", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoName value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoName { get { return Value<string>("shiptoName"); } set { SetOptional("shiptoName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoId value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoId { get { return Value<string>("shiptoId"); } set { SetOptional("shiptoId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoGtin value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoGtin { get { return Value<string>("shiptoGtin"); } set { SetOptional("shiptoGtin", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoDepartment value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoDepartment { get { return Value<string>("shiptoDepartment"); } set { SetOptional("shiptoDepartment", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoZipCode value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoZipCode { get { return Value<string>("shiptoZipCode"); } set { SetOptional("shiptoZipCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoCity value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoCity { get { return Value<string>("shiptoCity"); } set { SetOptional("shiptoCity", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoStreet value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoStreet { get { return Value<string>("shiptoStreet"); } set { SetOptional("shiptoStreet", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoAddressExtra value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoAddressExtra { get { return Value<string>("shiptoAddressExtra"); } set { SetOptional("shiptoAddressExtra", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoCountry value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ShiptoCountry { get { return Value<string>("shiptoCountry"); } set { SetOptional("shiptoCountry", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shiptoContactPersonId value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? ShiptoContactPersonId { get { return Value<long?>("shiptoContactPersonId"); } set { SetOptional("shiptoContactPersonId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original invoiceeName value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeName { get { return Value<string>("invoiceeName"); } set { SetOptional("invoiceeName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeId value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeId { get { return Value<string>("invoiceeId"); } set { SetOptional("invoiceeId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeGtin value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeGtin { get { return Value<string>("invoiceeGtin"); } set { SetOptional("invoiceeGtin", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeDepartment value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeDepartment { get { return Value<string>("invoiceeDepartment"); } set { SetOptional("invoiceeDepartment", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeZipCode value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeZipCode { get { return Value<string>("invoiceeZipCode"); } set { SetOptional("invoiceeZipCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeCity value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeCity { get { return Value<string>("invoiceeCity"); } set { SetOptional("invoiceeCity", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeStreet value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeStreet { get { return Value<string>("invoiceeStreet"); } set { SetOptional("invoiceeStreet", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeAddressExtra value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeAddressExtra { get { return Value<string>("invoiceeAddressExtra"); } set { SetOptional("invoiceeAddressExtra", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeCountry value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string InvoiceeCountry { get { return Value<string>("invoiceeCountry"); } set { SetOptional("invoiceeCountry", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoiceeContactPersonId value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? InvoiceeContactPersonId { get { return Value<long?>("invoiceeContactPersonId"); } set { SetOptional("invoiceeContactPersonId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original vendorContactId value.</summary>
        /// <value>The original optional vendor contact identity, or null to omit it. Other vendor values may be copied by the server; no local contact lookup occurs.</value>
        public long? VendorContactId { get { return Value<long?>("vendorContactId"); } set { SetOptional("vendorContactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original paymentTermName value.</summary>
        /// <value>The original payment-term name, or null to omit it. The server may use creditor defaults or ignore this name when explicit discount fields are supplied.</value>
        public string PaymentTermName { get { return Value<string>("paymentTermName"); } set { SetOptional("paymentTermName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original discountDate1 value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DiscountDate1 { get { return Value<long?>("discountDate1"); } set { SetOptional("discountDate1", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountDate2 value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DiscountDate2 { get { return Value<long?>("discountDate2"); } set { SetOptional("discountDate2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original dueDate value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DueDate { get { return Value<long?>("dueDate"); } set { SetOptional("dueDate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountPercent1 value.</summary>
        /// <value>The original optional numeric discount value, or null to omit it. The server validates amount/percentage/date dependencies and precedence.</value>
        public decimal? DiscountPercent1 { get { return Value<decimal?>("discountPercent1"); } set { SetOptional("discountPercent1", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountPercent2 value.</summary>
        /// <value>The original optional numeric discount value, or null to omit it. The server validates amount/percentage/date dependencies and precedence.</value>
        public decimal? DiscountPercent2 { get { return Value<decimal?>("discountPercent2"); } set { SetOptional("discountPercent2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original explicitDiscount value.</summary>
        /// <value>The original manual-discount choice, or null to omit it. The server evaluates supplied amounts and actual invoice state.</value>
        public bool? ExplicitDiscount { get { return Value<bool?>("explicitDiscount"); } set { SetOptional("explicitDiscount", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountAmount1 value.</summary>
        /// <value>The original optional numeric discount value, or null to omit it. The server validates amount/percentage/date dependencies and precedence.</value>
        public decimal? DiscountAmount1 { get { return Value<decimal?>("discountAmount1"); } set { SetOptional("discountAmount1", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountAmount2 value.</summary>
        /// <value>The original optional numeric discount value, or null to omit it. The server validates amount/percentage/date dependencies and precedence.</value>
        public decimal? DiscountAmount2 { get { return Value<decimal?>("discountAmount2"); } set { SetOptional("discountAmount2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The explicit 0 public, 1 custom or 2 personal permission choice, or null to omit it. The schema declares default 0; the client does not insert this default or infer that omission preserves old permissions.</value>
        public int? PermissionType { get { return Value<int?>("permissionType"); } set { SetOptional("permissionType", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ReadPermissionProfiles { get { return Value<string>("readPermissionProfiles"); } set { SetOptional("readPermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string ReadPermissionLogins { get { return Value<string>("readPermissionLogins"); } set { SetOptional("readPermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string WritePermissionProfiles { get { return Value<string>("writePermissionProfiles"); } set { SetOptional("writePermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string WritePermissionLogins { get { return Value<string>("writePermissionLogins"); } set { SetOptional("writePermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postingDate value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? PostingDate { get { return Value<long?>("postingDate"); } set { SetOptional("postingDate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deliveryNumber value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DeliveryNumber { get { return Value<string>("deliveryNumber"); } set { SetOptional("deliveryNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel1 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel1 { get { return Value<string>("dimensionLabel1"); } set { SetOptional("dimensionLabel1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel2 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel2 { get { return Value<string>("dimensionLabel2"); } set { SetOptional("dimensionLabel2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel3 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel3 { get { return Value<string>("dimensionLabel3"); } set { SetOptional("dimensionLabel3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel4 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel4 { get { return Value<string>("dimensionLabel4"); } set { SetOptional("dimensionLabel4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel5 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel5 { get { return Value<string>("dimensionLabel5"); } set { SetOptional("dimensionLabel5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel6 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel6 { get { return Value<string>("dimensionLabel6"); } set { SetOptional("dimensionLabel6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel7 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel7 { get { return Value<string>("dimensionLabel7"); } set { SetOptional("dimensionLabel7", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel8 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel8 { get { return Value<string>("dimensionLabel8"); } set { SetOptional("dimensionLabel8", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel9 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel9 { get { return Value<string>("dimensionLabel9"); } set { SetOptional("dimensionLabel9", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimensionLabel10 value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DimensionLabel10 { get { return Value<string>("dimensionLabel10"); } set { SetOptional("dimensionLabel10", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original deferredDate value.</summary>
        /// <value>The original optional epoch-millisecond value, or null to omit it. No date, timezone or month-boundary conversion is applied.</value>
        public long? DeferredDate { get { return Value<long?>("deferredDate"); } set { SetOptional("deferredDate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deferredSteps value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? DeferredSteps { get { return Value<long?>("deferredSteps"); } set { SetOptional("deferredSteps", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deferredPeriod value.</summary>
        /// <value>The explicit 0 month, 1 quarter or 2 year deferral period, or null to omit it.</value>
        public int? DeferredPeriod { get { return Value<int?>("deferredPeriod"); } set { SetOptional("deferredPeriod", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deferredCostAccount value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DeferredCostAccount { get { return Value<string>("deferredCostAccount"); } set { SetOptional("deferredCostAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original deferredCreditAccount value.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. Account numbers, CSV permissions, catalog names and identifiers are not normalized.</value>
        public string DeferredCreditAccount { get { return Value<string>("deferredCreditAccount"); } set { SetOptional("deferredCreditAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original deferredAccountSelection value.</summary>
        /// <value>The explicit 0 automatic or 1 manual deferral-account mode, or null to omit it.</value>
        public int? DeferredAccountSelection { get { return Value<int?>("deferredAccountSelection"); } set { SetOptional("deferredAccountSelection", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deferredPostingPlan value.</summary>
        /// <value>The original optional Int64 selection, or null to omit it. The server validates identity, range and state-dependent behavior.</value>
        public long? DeferredPostingPlan { get { return Value<long?>("deferredPostingPlan"); } set { SetOptional("deferredPostingPlan", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original lastComment value.</summary>
        /// <value>The original optional new invoice comment, including empty text, or null to omit it.</value>
        public string LastComment { get { return Value<string>("lastComment"); } set { SetOptional("lastComment", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = new IncomingInvoiceUpdateRequest(this);
            foreach (var name in new[] { "documentNumber", "orderNumber", "text1", "taxCountryIsoCode2", "buyerName", "buyerId", "buyerGTIN", "buyerDepartment", "buyerZipCode", "buyerCity", "buyerStreet", "buyerAddressExtra", "buyerCountry", "shiptoName", "shiptoId", "shiptoGtin", "shiptoDepartment", "shiptoZipCode", "shiptoCity", "shiptoStreet", "shiptoAddressExtra", "shiptoCountry", "invoiceeName", "invoiceeId", "invoiceeGtin", "invoiceeDepartment", "invoiceeZipCode", "invoiceeCity", "invoiceeStreet", "invoiceeAddressExtra", "invoiceeCountry", "paymentTermName", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins", "deliveryNumber", "dimensionLabel1", "dimensionLabel2", "dimensionLabel3", "dimensionLabel4", "dimensionLabel5", "dimensionLabel6", "dimensionLabel7", "dimensionLabel8", "dimensionLabel9", "dimensionLabel10", "deferredCostAccount", "deferredCreditAccount", "lastComment" })
                ValidateKind(snapshot[name], name, JTokenType.String);
            ValidateKind(snapshot["explicitDiscount"], "explicitDiscount", JTokenType.Boolean);
            foreach (var name in new[] { "incomingTs", "documentDate", "buyerContactId", "deliveryDateFrom", "deliveryDateTo", "buyerContactPersonId", "shiptoContactPersonId", "invoiceeContactPersonId", "vendorContactId", "discountDate1", "discountDate2", "dueDate", "postingDate", "deferredDate", "deferredSteps", "deferredPostingPlan" })
                ValidateInteger(snapshot[name], name, long.MinValue, long.MaxValue);
            ValidateInteger(snapshot["permissionType"], "permissionType", 0, 2);
            ValidateInteger(snapshot["deferredPeriod"], "deferredPeriod", 0, 2);
            ValidateInteger(snapshot["deferredAccountSelection"], "deferredAccountSelection", 0, 1);
            foreach (var name in new[] { "discountPercent1", "discountPercent2", "discountAmount1", "discountAmount2" })
            {
                var value = snapshot[name];
                if (value != null && value.Type != JTokenType.Null && ((value.Type != JTokenType.Integer && value.Type != JTokenType.Float) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(value.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                    throw new ArgumentException("The " + name + " field must be a finite JSON number or null.", "request");
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken token, string name, JTokenType kind)
        {
            if (token != null && token.Type != JTokenType.Null && token.Type != kind)
                throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateInteger(JToken token, string name, long minimum, long maximum)
        {
            if (token == null || token.Type == JTokenType.Null) return;
            long value;
            if (token.Type != JTokenType.Integer || !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out value) || value < minimum || value > maximum)
                throw new ArgumentException("The " + name + " field must be a JSON integer within its declared range.", "request");
        }
    }
}
