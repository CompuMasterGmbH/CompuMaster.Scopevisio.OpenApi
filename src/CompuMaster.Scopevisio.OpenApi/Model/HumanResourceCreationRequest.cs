using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied values for creating a human resource.</summary>
    /// <remarks>Typed null setters omit fields. The inherited JSON object retains unknown fields and permits explicit optional JSON null. Sending validates known JSON kinds and required values before I/O, without converting dates or catalog strings.</remarks>
    public class HumanResourceCreationRequest : JObject
    {
        /// <summary>Initializes an empty request whose contact identifier and planning dates must be assigned before sending.</summary>
        public HumanResourceCreationRequest() { }

        /// <summary>Initializes a request by deeply copying a complete JSON object.</summary>
        /// <param name="other">The JSON object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public HumanResourceCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the contact id.</summary>
        /// <value>The required contact identifier. Omitted or JSON-null identifiers are rejected before I/O; server identity constraints remain authoritative.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } set { SetOptional("contactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the valid from.</summary>
        /// <value>The required nonblank original planning start string. The contract contradicts yyyy-MM-dd and dd.MM.yyyy; the server validates the format and first-day-of-month semantics.</value>
        public string ValidFrom { get { return Value<string>("validFrom"); } set { SetOptional("validFrom", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the valid till.</summary>
        /// <value>The required nonblank original planning end string. The server validates the format, range and last-day-of-month semantics.</value>
        public string ValidTill { get { return Value<string>("validTill"); } set { SetOptional("validTill", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string Role { get { return Value<string>("role"); } set { SetOptional("role", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string Position { get { return Value<string>("position"); } set { SetOptional("position", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the department.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string Department { get { return Value<string>("department"); } set { SetOptional("department", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the project box.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string ProjectBox { get { return Value<string>("projectBox"); } set { SetOptional("projectBox", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the topic.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string Topic { get { return Value<string>("topic"); } set { SetOptional("topic", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The optional original catalog string, or null to omit it. The server validates catalog membership.</value>
        public string Location { get { return Value<string>("location"); } set { SetOptional("location", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the fee.</summary>
        /// <value>The optional fee, or null to omit it. Negative values remain unchanged for server validation; raw JSON numbers are not rounded by the request serializer.</value>
        public decimal? Fee { get { return Value<decimal?>("fee"); } set { SetOptional("fee", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the allow multiple.</summary>
        /// <value>The explicit multiple-resource choice, or null to omit it and retain the server default false. True requests multiple resources for the same contact.</value>
        public bool? AllowMultiple { get { return Value<bool?>("allowMultiple"); } set { SetOptional("allowMultiple", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = new HumanResourceCreationRequest(this);
            long identifier;
            var contact = snapshot["contactId"];
            if (contact == null || contact.Type != JTokenType.Integer ||
                !long.TryParse(contact.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out identifier))
                throw new ArgumentException("The contactId field must be an Int64 JSON integer.", "request");
            foreach (var name in new[] { "validFrom", "validTill" })
            {
                var value = snapshot[name];
                if (value == null || value.Type != JTokenType.String || string.IsNullOrWhiteSpace(value.Value<string>()))
                    throw new ArgumentException("The " + name + " field must be a nonblank JSON string.", "request");
            }
            foreach (var name in new[] { "role", "position", "department", "projectBox", "topic", "location" })
            {
                var value = snapshot[name];
                if (value != null && value.Type != JTokenType.Null && value.Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be a JSON string or null.", "request");
            }
            var multiple = snapshot["allowMultiple"];
            if (multiple != null && multiple.Type != JTokenType.Null && multiple.Type != JTokenType.Boolean)
                throw new ArgumentException("The allowMultiple field must be a JSON boolean or null.", "request");
            var fee = snapshot["fee"];
            if (fee != null && fee.Type != JTokenType.Null &&
                ((fee.Type != JTokenType.Integer && fee.Type != JTokenType.Float) ||
                !System.Text.RegularExpressions.Regex.IsMatch(fee.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                throw new ArgumentException("The fee field must be a finite JSON number or null.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
