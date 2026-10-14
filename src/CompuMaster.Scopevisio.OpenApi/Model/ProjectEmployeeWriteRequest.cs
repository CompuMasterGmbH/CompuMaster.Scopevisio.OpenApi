using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current project-team-entry creation or update fields.</summary>
    /// <remarks>Creation requires a supplied contactId. Typed null setters omit fields; raw update null remains available. Complete JSON is copied before sending without contact, role or rate defaults. Legacy ProjectEmployeeForm behavior is preserved.</remarks>
    public class ProjectEmployeeWriteRequest : JObject
    {
        /// <summary>Initializes an empty project-team-entry request without inserting defaults.</summary>
        public ProjectEmployeeWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete project-team-entry JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProjectEmployeeWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the explicitly selected personal-contact identifier.</summary>
        /// <value>The original nullable Int64 contactId. Creation requires a supplied value; typed null omits it on updates. It is distinct from the project-team-entry employeeId and is never resolved or converted by the client.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } set { SetOptional("contactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original contact sphere.</summary>
        /// <value>The optional original sphere string, or null to omit it without choosing a default. The server validates allowed selections.</value>
        public string ContactSphere { get { return Value<string>("contactSphere"); } set { SetOptional("contactSphere", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original configured project role.</summary>
        /// <value>The optional original role catalogue string, or null to omit it. Configured roles are not restricted to the historical generated enum; the server validates membership.</value>
        public string Role { get { return Value<string>("role"); } set { SetOptional("role", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original hourly rate.</summary>
        /// <value>The optional decimal rate with bounded .NET decimal precision, or null to omit it without inserting zero. Complete raw numeric tokens can be supplied through the JSON indexer without rate calculation or rounding.</value>
        public decimal? HourlyRate { get { return Value<decimal?>("hourlyRate"); } set { SetOptional("hourlyRate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original daily rate.</summary>
        /// <value>The optional decimal rate with bounded .NET decimal precision, or null to omit it without inserting zero. Complete raw numeric tokens can be supplied through the JSON indexer without rate calculation or rounding.</value>
        public decimal? DailyRate { get { return Value<decimal?>("dailyRate"); } set { SetOptional("dailyRate", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "contactSphere", "role" }) ValidateKind(snapshot[name], name, JTokenType.String, creating);
            var contact = snapshot["contactId"];
            if (contact != null && (creating || contact.Type != JTokenType.Null))
            {
                long number;
                if (contact.Type != JTokenType.Integer || !long.TryParse(contact.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number))
                    throw new ArgumentException("The contactId field must be a JSON integer within its declared Int64 width.", "request");
            }
            foreach (var name in new[] { "hourlyRate", "dailyRate" }) ValidateNumber(snapshot[name], name, creating);
            if (creating && contact?.Type != JTokenType.Integer)
                throw new ArgumentException("Project-team-entry creation requires an explicit personal-contact ID.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
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
