using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current personal-account update fields.</summary>
    /// <remarks>Sparse updates retain omitted fields and preserve explicit raw null without assuming all fields can be cleared. Complete JSON is copied without number-range, summary-account, currency, group, payment or boolean defaults. Nested payment forms retain the caller selection and server fallback; legacy UpdatePersonalAccountForm behavior is preserved.</remarks>
    public class PersonalAccountUpdateRequest : JObject
    {
        /// <summary>Initializes an empty personal-account request without inserting defaults.</summary>
        public PersonalAccountUpdateRequest() { }

        /// <summary>Initializes a request by deeply copying complete personal-account JSON.</summary>
        /// <param name="other">The original object, including nested payment data and unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public PersonalAccountUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original externalNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue, format and reference semantics without client normalization or lookup.</value>
        public string ExternalNumber { get { return Value<string>("externalNumber"); } set { SetOptional("externalNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original sumAccountNumber value.</summary>
        /// <value>The optional original summary-account-number string. Omission leaves the documented server default and does not insert a client account number.</value>
        public string SumAccountNumber { get { return Value<string>("sumAccountNumber"); } set { SetOptional("sumAccountNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original numberRangeNumber value.</summary>
        /// <value>The optional original Int64 number-range selection. The server may otherwise choose its first range and generate an account number; the client does not choose the documented debitor/kreditor range defaults.</value>
        public long? NumberRangeNumber { get { return Value<long?>("numberRangeNumber"); } set { SetOptional("numberRangeNumber", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original group value.</summary>
        /// <value>The optional original group name. The server may create a missing group and assign it; the client performs no group lookup or additional request.</value>
        public string Group { get { return Value<string>("group"); } set { SetOptional("group", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatCode value.</summary>
        /// <value>The optional original VAT catalogue string, including additional configured matrix values. It is not restricted to historical enum examples or calculated by the client.</value>
        public string VatCode { get { return Value<string>("vatCode"); } set { SetOptional("vatCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original paymentType value.</summary>
        /// <value>The optional original payment-type string. Text, capitalization and commas are not split, repaired or selected by the client; allowed values remain server-authoritative.</value>
        public string PaymentType { get { return Value<string>("paymentType"); } set { SetOptional("paymentType", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue, format and reference semantics without client normalization or lookup.</value>
        public string VatNumber { get { return Value<string>("vatNumber"); } set { SetOptional("vatNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatId value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates catalogue, format and reference semantics without client normalization or lookup.</value>
        public string VatId { get { return Value<string>("vatId"); } set { SetOptional("vatId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original currency value.</summary>
        /// <value>The optional original currency string. The server validates the ISO 4217 selection; no currency conversion, case normalization or default is introduced.</value>
        public string Currency { get { return Value<string>("currency"); } set { SetOptional("currency", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original language value.</summary>
        /// <value>The optional original configured language string. The client does not choose a language, culture or historical enum default.</value>
        public string Language { get { return Value<string>("language"); } set { SetOptional("language", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original paymentTermId value.</summary>
        /// <value>The optional original Int64 payment-term selection. When a form is also supplied, the server first looks for this ID and may then use the form; no local lookup, XOR rule or selection is added.</value>
        public long? PaymentTermId { get { return Value<long?>("paymentTermId"); } set { SetOptional("paymentTermId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original paymentTermForm value.</summary>
        /// <value>The optional deeply copied original payment-term JSON. A supplied form requires name using the current payment contract. The server may match a name or create a payment term; the client preserves the form alongside paymentTermId without a second request.</value>
        public JObject PaymentTermForm { get { return this["paymentTermForm"] as JObject; } set { SetOptional("paymentTermForm", value?.DeepClone()); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var field in new[] { "externalNumber", "sumAccountNumber", "group", "vatCode", "paymentType", "vatNumber", "vatId", "currency", "language" }) ValidateKind(snapshot[field], field, JTokenType.String);
            foreach (var field in new[] { "paymentTermForm" }) ValidateKind(snapshot[field], field, JTokenType.Object);
            ValidateInteger(snapshot["numberRangeNumber"], "numberRangeNumber");
            ValidateInteger(snapshot["paymentTermId"], "paymentTermId");
            if (snapshot["paymentTermForm"] is JObject payment)
                new PaymentTermWriteRequest(payment).SnapshotForSend(true);
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind)
        {
            if (value == null || value.Type == JTokenType.Null) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateInteger(JToken value, string name)
        {
            if (value == null || value.Type == JTokenType.Null) return;
            long number;
            if (value.Type != JTokenType.Integer || !long.TryParse(value.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number))
                throw new ArgumentException("The " + name + " field must be a JSON integer within Int64 width.", "request");
        }
    }
}
