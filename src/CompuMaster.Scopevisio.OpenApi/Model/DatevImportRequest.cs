using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains a copied current DATEV import request.</summary>
    /// <remarks>Preserves original selections and future JSON without date, chart, zone, mapping or flag defaults, lookups, conversion, calculation or replay. Actual format, profile and server acceptance remain unverified. Historical models and defaults remain unchanged.</remarks>
    public sealed class DatevImportRequest : JObject
    {
        /// <summary>Initializes an empty copied DATEV request.</summary>
        public DatevImportRequest() { }

        /// <summary>Initializes a request by deeply copying original JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public DatevImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original zone value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative. The server may select its standard zone according to actual zone settings; the client selects none.</value>
        public string Zone { get { return Value<string>("zone"); } set { SetOptional("zone", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original data value.</summary>
        /// <value>The original text, including empty strings, or null to omit it. No decoding, trimming, lookup, normalization or server-default selection occurs; actual content remains server-authoritative. A supplied import object requires this original Base64 data string; its DATEV content is not parsed or repaired.</value>
        public string Data { get { return Value<string>("data"); } set { SetOptional("data", value != null ? new JValue(value) : null); } }

        /// <summary>Creates an import request from the original DATEV file bytes.</summary>
        /// <param name="data">The original bytes, including an empty array, copied into Base64 without text or filesystem conversion.</param>
        /// <returns>A request containing Base64 data and no zone or other defaults.</returns>
        /// <exception cref="ArgumentNullException">The byte array is null.</exception>
        /// <remarks>This factory does not validate DATEV structure or establish server acceptance.</remarks>
        public static DatevImportRequest FromBytes(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            return new DatevImportRequest { Data = Convert.ToBase64String((byte[])data.Clone()) };
        }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "zone", "data" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
            if (snapshot["data"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied DATEV import object requires its original data string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
