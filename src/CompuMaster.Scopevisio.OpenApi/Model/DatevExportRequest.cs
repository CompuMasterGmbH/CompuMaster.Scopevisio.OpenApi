using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains a copied current DATEV export settings request.</summary>
    /// <remarks>Preserves original selections and future JSON without date, chart, zone, mapping or flag defaults, lookups, conversion, calculation or replay. Actual format, profile and server acceptance remain unverified. Historical models and defaults remain unchanged.</remarks>
    public sealed class DatevExportRequest : JObject
    {
        /// <summary>Initializes an empty copied DATEV request.</summary>
        public DatevExportRequest() { }

        /// <summary>Initializes a request by deeply copying original JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public DatevExportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original fromPostingDateTs value.</summary>
        /// <value>The original optional signed Int64 UNIX epoch milliseconds. No date conversion, current-year calculation, timezone adjustment, minimum or ordering rule is inserted.</value>
        public long? FromPostingDateTs { get { return Value<long?>("fromPostingDateTs"); } set { SetOptional("fromPostingDateTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original toPostingDateTs value.</summary>
        /// <value>The original optional signed Int64 UNIX epoch milliseconds. No date conversion, current-year calculation, timezone adjustment, minimum or ordering rule is inserted.</value>
        public long? ToPostingDateTs { get { return Value<long?>("toPostingDateTs"); } set { SetOptional("toPostingDateTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original fromCreationDateTs value.</summary>
        /// <value>The original optional signed Int64 UNIX epoch milliseconds. No date conversion, current-year calculation, timezone adjustment, minimum or ordering rule is inserted. Both creation-date bounds must be supplied together for creation-date filtering.</value>
        public long? FromCreationDateTs { get { return Value<long?>("fromCreationDateTs"); } set { SetOptional("fromCreationDateTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original toCreationDateTs value.</summary>
        /// <value>The original optional signed Int64 UNIX epoch milliseconds. No date conversion, current-year calculation, timezone adjustment, minimum or ordering rule is inserted. Both creation-date bounds must be supplied together for creation-date filtering.</value>
        public long? ToCreationDateTs { get { return Value<long?>("toCreationDateTs"); } set { SetOptional("toCreationDateTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original dimension1 value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative.</value>
        public string Dimension1 { get { return Value<string>("dimension1"); } set { SetOptional("dimension1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dimension2 value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative.</value>
        public string Dimension2 { get { return Value<string>("dimension2"); } set { SetOptional("dimension2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original textConfiguration value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative.</value>
        public string TextConfiguration { get { return Value<string>("textConfiguration"); } set { SetOptional("textConfiguration", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original field1Configuration value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative.</value>
        public string Field1Configuration { get { return Value<string>("field1Configuration"); } set { SetOptional("field1Configuration", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original exportStrategy value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative.</value>
        public string ExportStrategy { get { return Value<string>("exportStrategy"); } set { SetOptional("exportStrategy", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original skipCancellations value.</summary>
        /// <value>The optional explicit boolean. Omission remains distinct from false or true; server defaults are not inserted by the client.</value>
        public bool? SkipCancellations { get { return Value<bool?>("skipCancellations"); } set { SetOptional("skipCancellations", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original vatMatrixDatevKeys value.</summary>
        /// <value>The optional explicit boolean. Omission remains distinct from false or true; server defaults are not inserted by the client.</value>
        public bool? VatMatrixDatevKeys { get { return Value<bool?>("vatMatrixDatevKeys"); } set { SetOptional("vatMatrixDatevKeys", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original preferRowTexts value.</summary>
        /// <value>The optional explicit boolean. Omission remains distinct from false or true; server defaults are not inserted by the client.</value>
        public bool? PreferRowTexts { get { return Value<bool?>("preferRowTexts"); } set { SetOptional("preferRowTexts", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original mapping value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative. The original legacy selector or numeric record-ID text is retained without parsing or resolving an account mapping.</value>
        public string Mapping { get { return Value<string>("mapping"); } set { SetOptional("mapping", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "dimension1", "dimension2", "textConfiguration", "field1Configuration", "exportStrategy", "mapping" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
            foreach (var name in new[] { "skipCancellations", "vatMatrixDatevKeys", "preferRowTexts" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.Boolean)
                    throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
            foreach (var name in new[] { "fromPostingDateTs", "toPostingDateTs", "fromCreationDateTs", "toCreationDateTs" })
            {
                var value = snapshot[name];
                long number;
                if (value != null && (value.Type != JTokenType.Integer || !long.TryParse(value.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number)))
                    throw new ArgumentException("The " + name + " field must be an original JSON integer within Int64.", "request");
            }
            if ((snapshot.Property("fromCreationDateTs") != null) != (snapshot.Property("toCreationDateTs") != null))
                throw new ArgumentException("Creation-date filtering requires both original creation-date bounds.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
