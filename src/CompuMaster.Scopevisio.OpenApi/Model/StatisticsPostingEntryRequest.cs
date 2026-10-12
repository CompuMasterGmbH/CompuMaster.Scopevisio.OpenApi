using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit statistics posting with required original date, account and nonzero amount.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers retain unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class StatisticsPostingEntryRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults or automatic selections.</summary>
        public StatisticsPostingEntryRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public StatisticsPostingEntryRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original date selection.</summary>
        /// <value>The required original Int64 epoch-millisecond timestamp. No date/month/year boundary or timezone conversion is applied.</value>
        public long? Date
        {
            get { return Value<long?>("date"); }
            set { if (value.HasValue) this["date"] = new JValue(value.Value); else Remove("date"); }
        }

        /// <summary>Gets or sets the original accountNumber selection.</summary>
        /// <value>The required original account-number string, retaining leading zeros and empty text for server validation.</value>
        public string AccountNumber
        {
            get { return Value<string>("accountNumber"); }
            set { if (value != null) this["accountNumber"] = new JValue(value); else Remove("accountNumber"); }
        }

        /// <summary>Gets or sets the original amount selection.</summary>
        /// <value>The required original finite nonzero quantity. Negative values remain allowed; no client rounding or sign rule is imposed.</value>
        public decimal? Amount
        {
            get { return Value<decimal?>("amount"); }
            set { if (value.HasValue) this["amount"] = new JValue(value.Value); else Remove("amount"); }
        }

        /// <summary>Gets or sets the original text selection.</summary>
        /// <value>The optional original posting text, including empty text, or null to omit it.</value>
        public string Text
        {
            get { return Value<string>("text"); }
            set { if (value != null) this["text"] = new JValue(value); else Remove("text"); }
        }

        /// <summary>Gets or sets the original zoneNumber selection.</summary>
        /// <value>The optional original zone string, or null to omit it. Whether the zone is required depends on server settings.</value>
        public string ZoneNumber
        {
            get { return Value<string>("zoneNumber"); }
            set { if (value != null) this["zoneNumber"] = new JValue(value); else Remove("zoneNumber"); }
        }

        /// <summary>Gets or sets the original dimension_1 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension1
        {
            get { return Value<long?>("dimension_1"); }
            set { if (value.HasValue) this["dimension_1"] = new JValue(value.Value); else Remove("dimension_1"); }
        }

        /// <summary>Gets or sets the original dimension_2 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension2
        {
            get { return Value<long?>("dimension_2"); }
            set { if (value.HasValue) this["dimension_2"] = new JValue(value.Value); else Remove("dimension_2"); }
        }

        /// <summary>Gets or sets the original dimension_3 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension3
        {
            get { return Value<long?>("dimension_3"); }
            set { if (value.HasValue) this["dimension_3"] = new JValue(value.Value); else Remove("dimension_3"); }
        }

        /// <summary>Gets or sets the original dimension_4 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension4
        {
            get { return Value<long?>("dimension_4"); }
            set { if (value.HasValue) this["dimension_4"] = new JValue(value.Value); else Remove("dimension_4"); }
        }

        /// <summary>Gets or sets the original dimension_5 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension5
        {
            get { return Value<long?>("dimension_5"); }
            set { if (value.HasValue) this["dimension_5"] = new JValue(value.Value); else Remove("dimension_5"); }
        }

        /// <summary>Gets or sets the original dimension_6 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension6
        {
            get { return Value<long?>("dimension_6"); }
            set { if (value.HasValue) this["dimension_6"] = new JValue(value.Value); else Remove("dimension_6"); }
        }

        /// <summary>Gets or sets the original dimension_7 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension7
        {
            get { return Value<long?>("dimension_7"); }
            set { if (value.HasValue) this["dimension_7"] = new JValue(value.Value); else Remove("dimension_7"); }
        }

        /// <summary>Gets or sets the original dimension_8 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension8
        {
            get { return Value<long?>("dimension_8"); }
            set { if (value.HasValue) this["dimension_8"] = new JValue(value.Value); else Remove("dimension_8"); }
        }

        /// <summary>Gets or sets the original dimension_9 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension9
        {
            get { return Value<long?>("dimension_9"); }
            set { if (value.HasValue) this["dimension_9"] = new JValue(value.Value); else Remove("dimension_9"); }
        }

        /// <summary>Gets or sets the original dimension_10 selection.</summary>
        /// <value>The optional original Int64 dimension selection, or null to omit it. The server validates dimension identity and availability.</value>
        public long? Dimension10
        {
            get { return Value<long?>("dimension_10"); }
            set { if (value.HasValue) this["dimension_10"] = new JValue(value.Value); else Remove("dimension_10"); }
        }

    }
}
