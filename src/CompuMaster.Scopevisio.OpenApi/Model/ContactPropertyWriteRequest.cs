using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current contact-property creation or update fields.</summary>
    /// <remarks>All individual fields are optional. Original catalogue and comma-separated strings remain unchanged. Typed null setters omit fields; explicit raw update null remains available. JSON is deeply copied before sending without timestamps, permissions or boolean defaults. Legacy ContactPropertyForm behavior is preserved.</remarks>
    public class ContactPropertyWriteRequest : JObject
    {
        /// <summary>Initializes an empty contact-property request without inserting defaults.</summary>
        public ContactPropertyWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete contact-property JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ContactPropertyWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original type value.</summary>
        /// <value>The optional original information-type catalogue string, or null to omit it. The server validates configured catalogue membership.</value>
        public new string Type { get { return Value<string>("type"); } set { SetOptional("type", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original subtype value.</summary>
        /// <value>The optional original subgroup catalogue string, or null to omit it. The server validates configured catalogue membership.</value>
        public string Subtype { get { return Value<string>("subtype"); } set { SetOptional("subtype", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The optional original description text, including empty text, or null to omit it.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original validFrom value.</summary>
        /// <value>The optional original validity-start timestamp in epoch milliseconds, or null to omit it. No current date, zero or year-boundary normalization is inserted.</value>
        public long? ValidFrom { get { return Value<long?>("validFrom"); } set { SetOptional("validFrom", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original validTo value.</summary>
        /// <value>The optional original validity-end timestamp in epoch milliseconds, or null to omit it. No current date, zero or year-boundary normalization is inserted.</value>
        public long? ValidTo { get { return Value<long?>("validTo"); } set { SetOptional("validTo", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original employeeDomain value.</summary>
        /// <value>The optional explicit employee-domain boolean, or null to omit it without inserting false.</value>
        public bool? EmployeeDomain { get { return Value<bool?>("employeeDomain"); } set { SetOptional("employeeDomain", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original spheresDisplay value.</summary>
        /// <value>The optional original sphere-selection string, including documented comma-separated selections. Omission lets the server retain existing visibility or apply creation defaults; the client does not restrict it to a single sphere enum.</value>
        public string SpheresDisplay { get { return Value<string>("spheresDisplay"); } set { SetOptional("spheresDisplay", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original employeeFilterDisplay value.</summary>
        /// <value>The optional original employer visibility string, including comma-separated employer contact names. The client performs no employer lookup or literal-enum restriction; the server validates visibility rules.</value>
        public string EmployeeFilterDisplay { get { return Value<string>("employeeFilterDisplay"); } set { SetOptional("employeeFilterDisplay", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The optional original Int32 permission value. The documented 0 through 2 semantic range remains server-authoritative; no zero default is inserted.</value>
        public int? PermissionType { get { return Value<int?>("permissionType"); } set { SetOptional("permissionType", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server evaluates permission lists only when permissionType equals 1.</value>
        public string ReadPermissionProfiles { get { return Value<string>("readPermissionProfiles"); } set { SetOptional("readPermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server evaluates permission lists only when permissionType equals 1.</value>
        public string ReadPermissionLogins { get { return Value<string>("readPermissionLogins"); } set { SetOptional("readPermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server evaluates permission lists only when permissionType equals 1.</value>
        public string WritePermissionProfiles { get { return Value<string>("writePermissionProfiles"); } set { SetOptional("writePermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server evaluates permission lists only when permissionType equals 1.</value>
        public string WritePermissionLogins { get { return Value<string>("writePermissionLogins"); } set { SetOptional("writePermissionLogins", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "type", "subtype", "description", "spheresDisplay", "employeeFilterDisplay", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins" })
                ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "employeeDomain" })
                ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            foreach (var name in new[] { "validFrom", "validTo" })
                ValidateInteger(snapshot[name], name, true, creating);
            foreach (var name in new[] { "permissionType" })
                ValidateInteger(snapshot[name], name, false, creating);
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
    }
}
