using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current product creation or update fields.</summary>
    /// <remarks>Creation requires an explicit unit. Original catalogue strings are not restricted to historical enum lists. Typed null setters omit fields; raw update null remains available. Read-only type is rejected in writes. JSON is deeply copied before sending without price, permission or boolean defaults. Legacy ProductForm behavior is preserved.</remarks>
    public class ProductWriteRequest : JObject
    {
        /// <summary>Initializes an empty product request without inserting defaults.</summary>
        public ProductWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete product JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProductWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original number value.</summary>
        /// <value>The optional original product number. When omitted during creation the server can generate it; omission during update retains the existing number.</value>
        public string Number { get { return Value<string>("number"); } set { SetOptional("number", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string Name { get { return Value<string>("name"); } set { SetOptional("name", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original typeDisplay value.</summary>
        /// <value>The optional original product kind string. The server derives read-only numeric type from this field and applies its default when omitted during creation.</value>
        public string TypeDisplay { get { return Value<string>("typeDisplay"); } set { SetOptional("typeDisplay", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original invoicePostingMode value.</summary>
        /// <value>The optional original invoice posting mode string. Server catalogue values and defaults remain server-authoritative.</value>
        public string InvoicePostingMode { get { return Value<string>("invoicePostingMode"); } set { SetOptional("invoicePostingMode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original taxRate value.</summary>
        /// <value>The optional original decimal tax rate. The server validates supported rates and applies creation defaults; no zero or tax rate is inserted.</value>
        public decimal? TaxRate { get { return Value<decimal?>("taxRate"); } set { SetOptional("taxRate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original revenueAccountMode value.</summary>
        /// <value>The optional original Int32 account mode. The server validates the documented 0 through 1 range and may derive manual mode from provided accounts; no mode is inserted.</value>
        public int? RevenueAccountMode { get { return Value<int?>("revenueAccountMode"); } set { SetOptional("revenueAccountMode", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original revenueAccount0 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string RevenueAccount0 { get { return Value<string>("revenueAccount0"); } set { SetOptional("revenueAccount0", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original revenueAccount1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string RevenueAccount1 { get { return Value<string>("revenueAccount1"); } set { SetOptional("revenueAccount1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original revenueAccount2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string RevenueAccount2 { get { return Value<string>("revenueAccount2"); } set { SetOptional("revenueAccount2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original revenueAccount3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string RevenueAccount3 { get { return Value<string>("revenueAccount3"); } set { SetOptional("revenueAccount3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original unit value.</summary>
        /// <value>The original selected unit string, required for creation. Account catalogues may define additional units beyond the documented examples; no unit is chosen automatically.</value>
        public string Unit { get { return Value<string>("unit"); } set { SetOptional("unit", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original asIfSold value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? AsIfSold { get { return Value<bool?>("asIfSold"); } set { SetOptional("asIfSold", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original billable value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? Billable { get { return Value<bool?>("billable"); } set { SetOptional("billable", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original locked value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? Locked { get { return Value<bool?>("locked"); } set { SetOptional("locked", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original planValue value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? PlanValue { get { return Value<decimal?>("planValue"); } set { SetOptional("planValue", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original costPrice value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? CostPrice { get { return Value<decimal?>("costPrice"); } set { SetOptional("costPrice", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmount value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmount { get { return Value<decimal?>("singleAmount"); } set { SetOptional("singleAmount", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmount2 value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmount2 { get { return Value<decimal?>("singleAmount2"); } set { SetOptional("singleAmount2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmount3 value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmount3 { get { return Value<decimal?>("singleAmount3"); } set { SetOptional("singleAmount3", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmountGross value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmountGross { get { return Value<decimal?>("singleAmountGross"); } set { SetOptional("singleAmountGross", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmountGross2 value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmountGross2 { get { return Value<decimal?>("singleAmountGross2"); } set { SetOptional("singleAmountGross2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original singleAmountGross3 value.</summary>
        /// <value>The optional original decimal amount with bounded .NET decimal precision, or null to omit it without inserting zero. No price calculation or rounding is performed.</value>
        public decimal? SingleAmountGross3 { get { return Value<decimal?>("singleAmountGross3"); } set { SetOptional("singleAmountGross3", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original nameLng1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string NameLng1 { get { return Value<string>("nameLng1"); } set { SetOptional("nameLng1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original descriptionLng1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue and reference semantics.</value>
        public string DescriptionLng1 { get { return Value<string>("descriptionLng1"); } set { SetOptional("descriptionLng1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original productGroupName value.</summary>
        /// <value>The optional original selected product-group name. The client does not create or resolve a group implicitly.</value>
        public string ProductGroupName { get { return Value<string>("productGroupName"); } set { SetOptional("productGroupName", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            if (snapshot.Property("type") != null)
                throw new ArgumentException("The type field is read-only and derived by the server from typeDisplay.", "request");
            foreach (var name in new[] { "number", "name", "description", "typeDisplay", "invoicePostingMode", "revenueAccount0", "revenueAccount1", "revenueAccount2", "revenueAccount3", "unit", "nameLng1", "descriptionLng1", "productGroupName" })
                ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "asIfSold", "billable", "locked" })
                ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            var mode = snapshot["revenueAccountMode"];
            if (mode != null && (creating || mode.Type != JTokenType.Null))
            {
                int value;
                if (mode.Type != JTokenType.Integer || !int.TryParse(mode.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
                    throw new ArgumentException("The revenueAccountMode field must be a JSON integer within its declared Int32 width.", "request");
            }
            foreach (var name in new[] { "taxRate", "planValue", "costPrice", "singleAmount", "singleAmount2", "singleAmount3", "singleAmountGross", "singleAmountGross2", "singleAmountGross3" })
                ValidateNumber(snapshot[name], name, creating);
            if (creating && snapshot["unit"]?.Type != JTokenType.String)
                throw new ArgumentException("Product creation requires an explicit unit string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateNumber(JToken value, string name, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != JTokenType.Integer && value.Type != JTokenType.Float)
                throw new ArgumentException("The " + name + " field must be an original JSON number.", "request");
            var scalar = ((JValue)value).Value;
            if ((scalar is double number && (double.IsNaN(number) || double.IsInfinity(number))) ||
                (scalar is float single && (float.IsNaN(single) || float.IsInfinity(single))))
                throw new ArgumentException("The " + name + " field must be a finite JSON number.", "request");
        }
    }
}
