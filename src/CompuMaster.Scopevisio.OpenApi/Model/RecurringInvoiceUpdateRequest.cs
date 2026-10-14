using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains copied current complete recurring-invoice recurrence data.</summary>
    /// <remarks>The route body is optional, but its dedicated schema requires all four fields in a supplied object. No calendar, date, timezone, frequency or invoice defaults are inserted. The server validates actual dates/frequency and ignores time of day as documented; original timestamps remain unchanged. Historical models/defaults remain unchanged.</remarks>
    public sealed class RecurringInvoiceUpdateRequest : JObject
    {
        /// <summary>Initializes an empty copied recurrence request.</summary>
        public RecurringInvoiceUpdateRequest() { }

        /// <summary>Initializes a request by deeply copying original recurrence JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public RecurringInvoiceUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original validFrom value.</summary>
        /// <value>The original required signed Int64 UNIX epoch milliseconds, or null to omit it while constructing the request. No date/timezone conversion, day truncation, current-year selection, range ordering or next-invoice calculation occurs.</value>
        public long? ValidFrom { get { return Value<long?>("validFrom"); } set { SetOptional("validFrom", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original validTo value.</summary>
        /// <value>The original required signed Int64 UNIX epoch milliseconds, or null to omit it while constructing the request. No date/timezone conversion, day truncation, current-year selection, range ordering or next-invoice calculation occurs.</value>
        public long? ValidTo { get { return Value<long?>("validTo"); } set { SetOptional("validTo", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original frequency value.</summary>
        /// <value>The original required frequency string, or null to omit it while constructing the request. The server validates frequency choices; no trimming, fallback, enum conversion or schedule expansion occurs.</value>
        public string Frequency { get { return Value<string>("frequency"); } set { SetOptional("frequency", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original nextInvoiceDate value.</summary>
        /// <value>The original required signed Int64 UNIX epoch milliseconds, or null to omit it while constructing the request. No date/timezone conversion, day truncation, current-year selection, range ordering or next-invoice calculation occurs.</value>
        public long? NextInvoiceDate { get { return Value<long?>("nextInvoiceDate"); } set { SetOptional("nextInvoiceDate", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "validFrom", "validTo", "nextInvoiceDate" })
            {
                var value = snapshot[name];
                long number;
                if (value?.Type != JTokenType.Integer || !long.TryParse(value.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number))
                    throw new ArgumentException("The " + name + " field requires an explicitly supplied original Int64 integer.", "request");
            }
            if (snapshot["frequency"]?.Type != JTokenType.String)
                throw new ArgumentException("The frequency field requires an explicitly supplied original string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
