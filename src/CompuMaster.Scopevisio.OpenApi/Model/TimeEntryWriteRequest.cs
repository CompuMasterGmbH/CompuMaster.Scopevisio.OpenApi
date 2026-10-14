using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current time-entry creation or update fields.</summary>
    /// <remarks>Creation requires an employee ID or email, rather than both. Typed null setters omit fields; raw update null remains available. Supplied JSON is deeply copied before sending without inserting timestamps, permissions or boolean defaults. Server rules determine references, full-day timestamps, clearing and time-entry-run restrictions. Legacy TimeEntryForm behavior is preserved.</remarks>
    public class TimeEntryWriteRequest : JObject
    {
        /// <summary>Initializes an empty time-entry request without inserting defaults.</summary>
        public TimeEntryWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete time-entry JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public TimeEntryWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original employeeId value.</summary>
        /// <value>The optional original employee contact ID. Creation requires this or EmployeeMail; do not supply both.</value>
        public long? EmployeeId { get { return Value<long?>("employeeId"); } set { SetOptional("employeeId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original employeeMail value.</summary>
        /// <value>The optional original employee email or login, replacing EmployeeId. Creation requires this or EmployeeId; do not supply both. The server validates the original string.</value>
        public string EmployeeMail { get { return Value<string>("employeeMail"); } set { SetOptional("employeeMail", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original startTs value.</summary>
        /// <value>The optional original start timestamp in epoch milliseconds. Omission leaves server defaults or the existing value intact; no current time is inserted.</value>
        public long? StartTs { get { return Value<long?>("startTs"); } set { SetOptional("startTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original endTs value.</summary>
        /// <value>The optional original end timestamp in epoch milliseconds. Omission leaves server defaults or the existing value intact; no current time is inserted.</value>
        public long? EndTs { get { return Value<long?>("endTs"); } set { SetOptional("endTs", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original fullDay value.</summary>
        /// <value>The optional explicit boolean. True lets the server use configured full-day boundaries instead of supplied timestamps; null omits it without inserting false.</value>
        public bool? FullDay { get { return Value<bool?>("fullDay"); } set { SetOptional("fullDay", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original contactId value.</summary>
        /// <value>The optional original contact ID. The server derives omitted contacts from project, task and order in that priority order.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } set { SetOptional("contactId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original projectId value.</summary>
        /// <value>The optional original project ID. The server derives an omitted project from task or order in that priority order.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } set { SetOptional("projectId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original taskId value.</summary>
        /// <value>The optional original signed Int64 identifier, or null to omit it without inserting a default ID.</value>
        public long? TaskId { get { return Value<long?>("taskId"); } set { SetOptional("taskId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original orderId value.</summary>
        /// <value>The optional original order ID. Do not supply it together with OrderNumber.</value>
        public long? OrderId { get { return Value<long?>("orderId"); } set { SetOptional("orderId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original orderNumber value.</summary>
        /// <value>The optional original order document number, replacing OrderId. Do not supply both; no number template is populated or resolved locally.</value>
        public string OrderNumber { get { return Value<string>("orderNumber"); } set { SetOptional("orderNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original productIdOrNumber value.</summary>
        /// <value>The optional original product ID or product number string, without numeric conversion or lookup.</value>
        public string ProductIdOrNumber { get { return Value<string>("productIdOrNumber"); } set { SetOptional("productIdOrNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original legacynumber value.</summary>
        /// <value>The optional original legacy number. The wire name is legacynumber, preserving the manufacturer casing.</value>
        public string LegacyNumber { get { return Value<string>("legacynumber"); } set { SetOptional("legacynumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original billable value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? Billable { get { return Value<bool?>("billable"); } set { SetOptional("billable", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original confirmed value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? Confirmed { get { return Value<bool?>("confirmed"); } set { SetOptional("confirmed", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original stateDone value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? StateDone { get { return Value<bool?>("stateDone"); } set { SetOptional("stateDone", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original internalNote value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string InternalNote { get { return Value<string>("internalNote"); } set { SetOptional("internalNote", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original note value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string Note { get { return Value<string>("note"); } set { SetOptional("note", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original workLocation value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string WorkLocation { get { return Value<string>("workLocation"); } set { SetOptional("workLocation", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original customFields value.</summary>
        /// <value>The optional deeply copied custom-field array. Entries require a string customFieldName and explicit mixed JSON customFieldValue; update null values remain available. The server validates configured names and types.</value>
        public JArray CustomFields { get { return this["customFields"] as JArray; } set { SetOptional("customFields", value?.DeepClone()); } }

        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The optional original Int32 permission value. The documented semantic range is 0 through 2 and remains server-authoritative; no zero default is inserted.</value>
        public int? PermissionType { get { return Value<int?>("permissionType"); } set { SetOptional("permissionType", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string ReadPermissionProfiles { get { return Value<string>("readPermissionProfiles"); } set { SetOptional("readPermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string ReadPermissionLogins { get { return Value<string>("readPermissionLogins"); } set { SetOptional("readPermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string WritePermissionProfiles { get { return Value<string>("writePermissionProfiles"); } set { SetOptional("writePermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates reference and permission semantics.</value>
        public string WritePermissionLogins { get { return Value<string>("writePermissionLogins"); } set { SetOptional("writePermissionLogins", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "employeeMail", "orderNumber", "productIdOrNumber", "legacynumber", "internalNote", "note", "workLocation", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins" })
                ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "fullDay", "billable", "confirmed", "stateDone" })
                ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            foreach (var name in new[] { "customFields" })
                ValidateKind(snapshot[name], name, JTokenType.Array, creating);
            foreach (var name in new[] { "employeeId", "startTs", "endTs", "contactId", "projectId", "taskId", "orderId" })
                ValidateInteger(snapshot[name], name, true, creating);
            foreach (var name in new[] { "permissionType" })
                ValidateInteger(snapshot[name], name, false, creating);
            var employeeId = HasValue(snapshot["employeeId"]);
            var employeeMail = HasValue(snapshot["employeeMail"]);
            if ((creating && !employeeId && !employeeMail) || (employeeId && employeeMail))
                throw new ArgumentException("Creation requires an employeeId or employeeMail; do not supply both selectors.", "request");
            if (HasValue(snapshot["orderId"]) && HasValue(snapshot["orderNumber"]))
                throw new ArgumentException("Do not supply both orderId and orderNumber.", "request");
            if (snapshot["customFields"] is JArray items)
                foreach (var item in items)
                {
                    if (!(item is JObject row) || row["customFieldName"]?.Type != JTokenType.String || row.Property("customFieldValue") == null || (creating && row["customFieldValue"].Type == JTokenType.Null))
                        throw new ArgumentException("A custom field requires its original string name and explicit value.", "request");
                    // General documentation permits scalar values despite the vendor object schema.
                    // Preserve original mixed JSON values; the server validates configured custom types.
                }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static bool HasValue(JToken value) => value != null && value.Type != JTokenType.Null;

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
    }
}
