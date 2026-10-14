using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current payment-term write fields.</summary>
    /// <remarks>A supplied creation object requires name. Typed null setters omit fields; raw update null remains available. Complete JSON is copied before sending without day, percentage or schedule defaults; server errors additionally mention netTimeLimit. Legacy PaymentTermForm behavior is preserved.</remarks>
    public class PaymentTermWriteRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults.</summary>
        public PaymentTermWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete original JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public PaymentTermWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The original name string, required when a payment creation object is supplied. Typed null omits it on updates; uniqueness and missing-name error semantics remain server-authoritative.</value>
        public string Name { get { return Value<string>("name"); } set { SetOptional("name", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original cashDiscountDays1 value.</summary>
        /// <value>The optional original Int64 day count, or null to omit it without inserting zero. The server validates payment schedule relationships.</value>
        public long? CashDiscountDays1 { get { return Value<long?>("cashDiscountDays1"); } set { SetOptional("cashDiscountDays1", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original percentage1 value.</summary>
        /// <value>The optional original decimal percentage with bounded .NET precision. Raw numeric tokens may be supplied through the JSON indexer without conversion, rounding or discount calculation; the server validates percentage bounds and relationships.</value>
        public decimal? Percentage1 { get { return Value<decimal?>("percentage1"); } set { SetOptional("percentage1", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original cashDiscountDays2 value.</summary>
        /// <value>The optional original Int64 day count, or null to omit it without inserting zero. The server validates payment schedule relationships.</value>
        public long? CashDiscountDays2 { get { return Value<long?>("cashDiscountDays2"); } set { SetOptional("cashDiscountDays2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original percentage2 value.</summary>
        /// <value>The optional original decimal percentage with bounded .NET precision. Raw numeric tokens may be supplied through the JSON indexer without conversion, rounding or discount calculation; the server validates percentage bounds and relationships.</value>
        public decimal? Percentage2 { get { return Value<decimal?>("percentage2"); } set { SetOptional("percentage2", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original netTimeLimit value.</summary>
        /// <value>The optional original Int64 day count, or null to omit it without inserting zero. The server validates payment schedule relationships.</value>
        public long? NetTimeLimit { get { return Value<long?>("netTimeLimit"); } set { SetOptional("netTimeLimit", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates names, references and update semantics.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "name", "description" }) ValidateKind(snapshot[name], name, JTokenType.String, creating);
            ValidateInteger(snapshot["cashDiscountDays1"], "cashDiscountDays1", true, creating);
            ValidateNumber(snapshot["percentage1"], "percentage1", creating);
            ValidateInteger(snapshot["cashDiscountDays2"], "cashDiscountDays2", true, creating);
            ValidateNumber(snapshot["percentage2"], "percentage2", creating);
            ValidateInteger(snapshot["netTimeLimit"], "netTimeLimit", true, creating);
            if (creating && snapshot["name"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied payment-term creation object requires an explicit name.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateInteger(JToken value, string name, bool wide, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            long number;
            if (value.Type != JTokenType.Integer || !long.TryParse(value.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number) ||
                (!wide && (number < int.MinValue || number > int.MaxValue)))
                throw new ArgumentException("The " + name + " field must be a JSON integer within its declared width.", "request");
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
