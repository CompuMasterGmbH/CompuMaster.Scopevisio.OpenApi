using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current subtask write fields.</summary>
    /// <remarks>Creation requires an explicitly supplied taskId. Typed null setters omit fields; known explicit creation nulls are rejected before I/O. Complete JSON is copied before sending without ID, timestamp, priority, status, hour, permission or boolean defaults. Legacy SubtaskForm behavior is preserved.</remarks>
    public class SubtaskWriteRequest : JObject
    {
        /// <summary>Initializes an empty subtask request without inserting defaults.</summary>
        public SubtaskWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete subtask JSON.</summary>
        /// <param name="other">The original object, including nested arrays and unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public SubtaskWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original taskId value.</summary>
        /// <value>The original Int64 parent-task identifier, required for creation. Typed null omits it; no value is chosen or inserted.</value>
        public long? TaskId { get { return Value<long?>("taskId"); } set { SetOptional("taskId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original topic value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string Topic { get { return Value<string>("topic"); } set { SetOptional("topic", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original dueDateTS value.</summary>
        /// <value>The optional original timestamp in epoch milliseconds, or null to omit it. No current date, month boundary, reminder or zero timestamp is inserted.</value>
        public long? DueDateTS { get { return Value<long?>("dueDateTS"); } set { SetOptional("dueDateTS", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original responsibleContactId value.</summary>
        /// <value>The optional original Int64 identifier, or null to omit it without choosing an ID or inserting zero.</value>
        public long? ResponsibleContactId { get { return Value<long?>("responsibleContactId"); } set { SetOptional("responsibleContactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original status value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string Status { get { return Value<string>("status"); } set { SetOptional("status", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original estimatedHours value.</summary>
        /// <value>The optional original decimal hours with bounded .NET precision. Raw numeric tokens may be supplied through the JSON indexer without conversion, calculation or rounding.</value>
        public decimal? EstimatedHours { get { return Value<decimal?>("estimatedHours"); } set { SetOptional("estimatedHours", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "topic", "status" }) ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "taskId", "dueDateTS", "responsibleContactId" }) ValidateInteger(snapshot[name], name, true, creating);
            ValidateNumber(snapshot["estimatedHours"], "estimatedHours", creating);
            if (creating && snapshot["taskId"]?.Type != JTokenType.Integer)
                throw new ArgumentException("Subtask creation requires an explicitly supplied taskId.", "request");
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
