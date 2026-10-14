using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current task write fields.</summary>
    /// <remarks>Creation requires an explicitly supplied topic. Typed null setters omit fields; raw update null remains available. Complete JSON is copied before sending without ID, timestamp, priority, status, hour, permission or boolean defaults. Legacy TaskForm behavior is preserved.</remarks>
    public class TaskWriteRequest : JObject
    {
        /// <summary>Initializes an empty task request without inserting defaults.</summary>
        public TaskWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete task JSON.</summary>
        /// <param name="other">The original object, including nested arrays and unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public TaskWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original topic value.</summary>
        /// <value>The original topic string, required for creation. Typed null omits it; no value is chosen or inserted.</value>
        public string Topic { get { return Value<string>("topic"); } set { SetOptional("topic", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original priority value.</summary>
        /// <value>The optional original priority string. The documented normal default is not inserted by the client.</value>
        public string Priority { get { return Value<string>("priority"); } set { SetOptional("priority", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original contactId value.</summary>
        /// <value>The optional original Int64 identifier, or null to omit it without choosing an ID or inserting zero.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } set { SetOptional("contactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original contactLegacyId value.</summary>
        /// <value>The optional original contact legacy-ID string, or null to omit it. The server ignores it if the supplied contactId resolves; the client does not choose between or look up the selectors.</value>
        public string ContactLegacyId { get { return Value<string>("contactLegacyId"); } set { SetOptional("contactLegacyId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original contactSphere value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string ContactSphere { get { return Value<string>("contactSphere"); } set { SetOptional("contactSphere", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original startDateTS value.</summary>
        /// <value>The optional original timestamp in epoch milliseconds, or null to omit it. No current date, month boundary, reminder or zero timestamp is inserted.</value>
        public long? StartDateTS { get { return Value<long?>("startDateTS"); } set { SetOptional("startDateTS", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original dueDateTS value.</summary>
        /// <value>The optional original timestamp in epoch milliseconds, or null to omit it. No current date, month boundary, reminder or zero timestamp is inserted.</value>
        public long? DueDateTS { get { return Value<long?>("dueDateTS"); } set { SetOptional("dueDateTS", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original responsibleContactId value.</summary>
        /// <value>The optional original Int64 identifier, or null to omit it without choosing an ID or inserting zero.</value>
        public long? ResponsibleContactId { get { return Value<long?>("responsibleContactId"); } set { SetOptional("responsibleContactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original reminderDateTS value.</summary>
        /// <value>The optional original timestamp in epoch milliseconds, or null to omit it. No current date, month boundary, reminder or zero timestamp is inserted.</value>
        public long? ReminderDateTS { get { return Value<long?>("reminderDateTS"); } set { SetOptional("reminderDateTS", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original status value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string Status { get { return Value<string>("status"); } set { SetOptional("status", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original projectId value.</summary>
        /// <value>The optional original Int64 identifier, or null to omit it without choosing an ID or inserting zero.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } set { SetOptional("projectId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original outgoingInvoiceId value.</summary>
        /// <value>The optional original Int64 identifier, or null to omit it without choosing an ID or inserting zero.</value>
        public long? OutgoingInvoiceId { get { return Value<long?>("outgoingInvoiceId"); } set { SetOptional("outgoingInvoiceId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original tags value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates allowed values; text and spelling are not repaired.</value>
        public string Tags { get { return Value<string>("tags"); } set { SetOptional("tags", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original employeeDomain value.</summary>
        /// <value>The optional explicitly supplied boolean, or null to omit it without inserting false or inferring a mode.</value>
        public bool? EmployeeDomain { get { return Value<bool?>("employeeDomain"); } set { SetOptional("employeeDomain", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original setEstimatedHoursManually value.</summary>
        /// <value>The optional explicitly supplied boolean, or null to omit it without inserting false or inferring a mode.</value>
        public bool? SetEstimatedHoursManually { get { return Value<bool?>("setEstimatedHoursManually"); } set { SetOptional("setEstimatedHoursManually", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original estimatedHours value.</summary>
        /// <value>The optional original decimal hours with bounded .NET precision. Raw numeric tokens may be supplied through the JSON indexer without conversion, calculation or rounding. The server evaluates it only when setEstimatedHoursManually is explicitly true.</value>
        public decimal? EstimatedHours { get { return Value<decimal?>("estimatedHours"); } set { SetOptional("estimatedHours", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original customFields value.</summary>
        /// <value>The optional deeply copied custom-field array. Entries require an original string customFieldName and explicit mixed JSON customFieldValue; update null remains available. The server validates configured names and types.</value>
        public JArray CustomFields { get { return this["customFields"] as JArray; } set { SetOptional("customFields", value?.DeepClone()); } }

        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The optional original Int32 permission value. Its documented 0 through 2 semantic range remains server-authoritative; no zero default is inserted.</value>
        public int? PermissionType { get { return Value<int?>("permissionType"); } set { SetOptional("permissionType", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The optional original comma-separated permission string, including empty text, or null to omit it. The server evaluates it only when permissionType equals 1.</value>
        public string ReadPermissionProfiles { get { return Value<string>("readPermissionProfiles"); } set { SetOptional("readPermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The optional original comma-separated permission string, including empty text, or null to omit it. The server evaluates it only when permissionType equals 1.</value>
        public string ReadPermissionLogins { get { return Value<string>("readPermissionLogins"); } set { SetOptional("readPermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The optional original comma-separated permission string, including empty text, or null to omit it. The server evaluates it only when permissionType equals 1.</value>
        public string WritePermissionProfiles { get { return Value<string>("writePermissionProfiles"); } set { SetOptional("writePermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The optional original comma-separated permission string, including empty text, or null to omit it. The server evaluates it only when permissionType equals 1.</value>
        public string WritePermissionLogins { get { return Value<string>("writePermissionLogins"); } set { SetOptional("writePermissionLogins", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "topic", "priority", "contactLegacyId", "contactSphere", "description", "status", "tags", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins" }) ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "employeeDomain", "setEstimatedHoursManually" }) ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            foreach (var name in new[] { "customFields" }) ValidateKind(snapshot[name], name, JTokenType.Array, creating);
            foreach (var name in new[] { "contactId", "startDateTS", "dueDateTS", "responsibleContactId", "reminderDateTS", "projectId", "outgoingInvoiceId" }) ValidateInteger(snapshot[name], name, true, creating);
            foreach (var name in new[] { "permissionType" }) ValidateInteger(snapshot[name], name, false, creating);
            ValidateNumber(snapshot["estimatedHours"], "estimatedHours", creating);
            if (creating && snapshot["topic"]?.Type != JTokenType.String)
                throw new ArgumentException("Task creation requires an explicitly supplied topic.", "request");
            if (snapshot["customFields"] is JArray custom)
                foreach (var item in custom)
                    if (!(item is JObject row) || row["customFieldName"]?.Type != JTokenType.String || row.Property("customFieldValue") == null || (creating && row["customFieldValue"].Type == JTokenType.Null))
                        throw new ArgumentException("A custom field requires its original string name and explicit value.", "request");
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
