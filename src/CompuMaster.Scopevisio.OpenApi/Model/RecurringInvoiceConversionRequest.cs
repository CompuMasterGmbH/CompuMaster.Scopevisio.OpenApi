using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit recurring-invoice conversion scheduling without historical defaults.</summary>
    /// <remarks>The whole request is optional at the operation level. When supplied, all four scheduling fields are required. Typed null setters omit fields; original source and future JSON fields are copied. Epoch timestamps are not converted or defaulted.</remarks>
    public class RecurringInvoiceConversionRequest : JObject
    {
        /// <summary>Initializes an empty request without dates or a frequency.</summary>
        public RecurringInvoiceConversionRequest() { }

        /// <summary>Initializes a request by deeply copying the complete source object.</summary>
        /// <param name="other">The original JSON object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public RecurringInvoiceConversionRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the required original validity start in Unix epoch milliseconds.</summary>
        public long? ValidFrom
        {
            get { return Value<long?>("validFrom"); }
            set { if (value.HasValue) this["validFrom"] = new JValue(value.Value); else Remove("validFrom"); }
        }

        /// <summary>Gets or sets the required original validity end in Unix epoch milliseconds.</summary>
        public long? ValidTo
        {
            get { return Value<long?>("validTo"); }
            set { if (value.HasValue) this["validTo"] = new JValue(value.Value); else Remove("validTo"); }
        }

        /// <summary>Gets or sets the required original next-invoice timestamp in Unix epoch milliseconds.</summary>
        public long? NextInvoiceDate
        {
            get { return Value<long?>("nextInvoiceDate"); }
            set { if (value.HasValue) this["nextInvoiceDate"] = new JValue(value.Value); else Remove("nextInvoiceDate"); }
        }

        /// <summary>Gets or sets the required explicit frequency using the existing frequency enumeration.</summary>
        /// <remarks>The serialized choices are daily, weekly, monthly, bimonthly, quarterly, half-yearly and yearly. A null setter omits the field; the API rejects missing or undefined choices when a body is supplied.</remarks>
        public ConvertToRecurringInvoiceForm.FrequencyEnum? Frequency
        {
            get { return this["frequency"]?.ToObject<ConvertToRecurringInvoiceForm.FrequencyEnum?>(); }
            set { if (value.HasValue) this["frequency"] = JToken.FromObject(value.Value); else Remove("frequency"); }
        }
    }
}

