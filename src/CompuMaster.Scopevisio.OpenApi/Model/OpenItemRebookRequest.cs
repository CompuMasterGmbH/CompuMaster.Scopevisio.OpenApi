using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original rebooking document, date, period and source/target accounts.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class OpenItemRebookRequest : JObject
    {
        /// <summary>Initializes an empty request without choosing defaults or existing items.</summary>
        public OpenItemRebookRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OpenItemRebookRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string DocumentNumber
        {
            get { return Value<string>("documentNumber"); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original postingDate selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string PostingDate
        {
            get { return Value<string>("postingDate"); }
            set { if (value != null) this["postingDate"] = new JValue(value); else Remove("postingDate"); }
        }

        /// <summary>Gets or sets the original postingPeriod selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string PostingPeriod
        {
            get { return Value<string>("postingPeriod"); }
            set { if (value != null) this["postingPeriod"] = new JValue(value); else Remove("postingPeriod"); }
        }

        /// <summary>Gets or sets the original sourceNumber selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string SourceNumber
        {
            get { return Value<string>("sourceNumber"); }
            set { if (value != null) this["sourceNumber"] = new JValue(value); else Remove("sourceNumber"); }
        }

        /// <summary>Gets or sets the original targetNumber selection.</summary>
        /// <value>The original optional string, including empty text, or null to omit it. No concrete schema default or account/date/period choice is inserted.</value>
        public string TargetNumber
        {
            get { return Value<string>("targetNumber"); }
            set { if (value != null) this["targetNumber"] = new JValue(value); else Remove("targetNumber"); }
        }

    }
}
