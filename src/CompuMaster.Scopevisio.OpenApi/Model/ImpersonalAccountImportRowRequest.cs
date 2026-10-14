using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains a copied current impersonal-account import row.</summary>
    /// <remarks>Preserves original account, chart, tax and dimension selections without lookup, calculation, defaults or posting. The manufacturer examples and allowable values refer to SKR03; configured values and actual validity remain server-authoritative. Import may create or update an account. Historical models and defaults remain unchanged.</remarks>
    public sealed class ImpersonalAccountImportRowRequest : JObject
    {
        /// <summary>Initializes an empty copied import row.</summary>
        public ImpersonalAccountImportRowRequest() { }

        /// <summary>Initializes an import row by deeply copying original JSON.</summary>
        /// <param name="other">The original row, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ImpersonalAccountImportRowRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original number value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string Number { get { return Value<string>("number"); } set { SetOptional("number", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string Name { get { return Value<string>("name"); } set { SetOptional("name", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original type value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public new string Type { get { return Value<string>("type"); } set { SetOptional("type", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original category value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string Category { get { return Value<string>("category"); } set { SetOptional("category", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatCode value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string VatCode { get { return Value<string>("vatCode"); } set { SetOptional("vatCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatKey value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string VatKey { get { return Value<string>("vatKey"); } set { SetOptional("vatKey", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fixVatKey value.</summary>
        /// <value>The explicit optional boolean. Omission remains distinct from false or true without inserting a posting, fixed-tax, deactivation, finance or clearing default.</value>
        public bool? FixVatKey { get { return Value<bool?>("fixVatKey"); } set { SetOptional("fixVatKey", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original deactivated value.</summary>
        /// <value>The explicit optional boolean. Omission remains distinct from false or true without inserting a posting, fixed-tax, deactivation, finance or clearing default.</value>
        public bool? Deactivated { get { return Value<bool?>("deactivated"); } set { SetOptional("deactivated", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original directPosting value.</summary>
        /// <value>The explicit optional boolean. Omission remains distinct from false or true without inserting a posting, fixed-tax, deactivation, finance or clearing default.</value>
        public bool? DirectPosting { get { return Value<bool?>("directPosting"); } set { SetOptional("directPosting", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original discountAccount value.</summary>
        /// <value>The original text, including empty text, or null to omit it. Numbers, configured chart/tax values and names are not trimmed, normalized or looked up; the server validates content.</value>
        public string DiscountAccount { get { return Value<string>("discountAccount"); } set { SetOptional("discountAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original isFinanceAccount value.</summary>
        /// <value>The explicit optional boolean. Omission remains distinct from false or true without inserting a posting, fixed-tax, deactivation, finance or clearing default.</value>
        public bool? IsFinanceAccount { get { return Value<bool?>("isFinanceAccount"); } set { SetOptional("isFinanceAccount", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original subjectToClearing value.</summary>
        /// <value>The explicit optional boolean. Omission remains distinct from false or true without inserting a posting, fixed-tax, deactivation, finance or clearing default.</value>
        public bool? SubjectToClearing { get { return Value<bool?>("subjectToClearing"); } set { SetOptional("subjectToClearing", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original dimensions value.</summary>
        /// <value>The optional deeply copied original dimension array, including an empty array, or null to omit it. Dimension identifiers and account numbers remain separate original strings without lookup or default dimensions.</value>
        public JArray Dimensions { get { return (JArray)(this["dimensions"] as JArray)?.DeepClone(); } set { SetOptional("dimensions", value?.DeepClone()); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal static void Validate(JObject row)
        {
            foreach (var name in new[] { "number", "name", "type", "category", "vatCode", "vatKey", "discountAccount" })
                ValidateKind(row[name], name, JTokenType.String);
            foreach (var name in new[] { "fixVatKey", "deactivated", "directPosting", "isFinanceAccount", "subjectToClearing" })
                ValidateKind(row[name], name, JTokenType.Boolean);
            foreach (var name in new[] { "number", "name", "type" })
                if (row[name]?.Type != JTokenType.String)
                    throw new ArgumentException("An import row requires an explicitly supplied " + name + " string.", "request");
            ValidateKind(row["dimensions"], "dimensions", JTokenType.Array);
            if (row["dimensions"] is JArray dimensions)
                foreach (var item in dimensions)
                {
                    if (!(item is JObject dimension)) throw new ArgumentException("Each dimension must be an original JSON object.", "request");
                    ValidateKind(dimension["dimensionId"], "dimensionId", JTokenType.String);
                    ValidateKind(dimension["dimensionAccountNumber"], "dimensionAccountNumber", JTokenType.String);
                }
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind)
        {
            if (value != null && value.Type != kind)
                throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }
    }
}
