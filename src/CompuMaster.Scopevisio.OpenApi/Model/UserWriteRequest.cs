using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains copied current writable user fields and assignments.</summary>
    /// <remarks>Creation rows require email, firstName and lastName; updates preserve sparse fields and optional raw nulls under general update contracts. Read-only status, specialRights, creationTs and license validTill cannot be written. Original identifiers, logins, profiles, licenses and roles are copied without lookup, translation, defaults, privilege widening or license purchase. Assignment organisation names and Instanz scopes are distinct from numeric Scope organisation and opaque Teamwork tenant identifiers. Historical models/defaults remain unchanged.</remarks>
    public sealed class UserWriteRequest : JObject
    {
        /// <summary>Initializes an empty copied user request.</summary>
        public UserWriteRequest() { }

        /// <summary>Initializes a request by deeply copying original user JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public UserWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original uid value.</summary>
        /// <value>The optional original user UID, distinct from a Scope organisation, user-contact ID or Teamwork tenant selector. No lookup, UID/email precedence or default user selection occurs.</value>
        public string Uid { get { return Value<string>("uid"); } set { SetOptional("uid", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original firstName value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. Creation requires explicit firstName and lastName; the server validates actual names/gender without normalization or defaults.</value>
        public string FirstName { get { return Value<string>("firstName"); } set { SetOptional("firstName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original lastName value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. Creation requires explicit firstName and lastName; the server validates actual names/gender without normalization or defaults.</value>
        public string LastName { get { return Value<string>("lastName"); } set { SetOptional("lastName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original gender value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. Creation requires explicit firstName and lastName; the server validates actual names/gender without normalization or defaults.</value>
        public string Gender { get { return Value<string>("gender"); } set { SetOptional("gender", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email value.</summary>
        /// <value>The original login email string, required for creation. No validation repair, case conversion, trimming, lookup or default identity occurs; actual content remains server-authoritative.</value>
        public string Email { get { return Value<string>("email"); } set { SetOptional("email", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original pendingLogin value.</summary>
        /// <value>The optional original pending login email string without normalization, identity lookup or automatically confirmed login change.</value>
        public string PendingLogin { get { return Value<string>("pendingLogin"); } set { SetOptional("pendingLogin", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original shortName value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. Creation requires explicit firstName and lastName; the server validates actual names/gender without normalization or defaults.</value>
        public string ShortName { get { return Value<string>("shortName"); } set { SetOptional("shortName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original administrator value.</summary>
        /// <value>The optional explicit server administrator choice. Omission remains distinct from false or true; assigning Firmen und Benutzer requires administrator true on the server, but the client never inserts it or widens privileges.</value>
        public bool? Administrator { get { return Value<bool?>("administrator"); } set { SetOptional("administrator", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original changePassword value.</summary>
        /// <value>The optional explicit server password-change requirement. Omission remains distinct from false or true; the client inserts no password and performs no password change.</value>
        public bool? ChangePassword { get { return Value<bool?>("changePassword"); } set { SetOptional("changePassword", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original profiles value.</summary>
        /// <value>The optional deeply copied profile-assignment array, including empty arrays. Each supplied entry requires original name and organisation strings. Organisation is a name or Instanz for instance-wide scope, not a numeric organisation or opaque tenant selector. No profile lookup, scope translation, merge or default assignment occurs.</value>
        public JArray Profiles { get { return (JArray)(this["profiles"] as JArray)?.DeepClone(); } set { SetOptional("profiles", value?.DeepClone()); } }

        /// <summary>Gets or sets the original licenses value.</summary>
        /// <value>The optional deeply copied license-assignment array, including empty arrays. Each supplied entry requires original type; read-only validTill is rejected. No license purchase, expiry default, lookup, merge or default assignment occurs.</value>
        public JArray Licenses { get { return (JArray)(this["licenses"] as JArray)?.DeepClone(); } set { SetOptional("licenses", value?.DeepClone()); } }

        /// <summary>Gets or sets the original updateScopevisioEMail value.</summary>
        /// <value>The optional explicit server Scopevisio login-renaming choice. Omission remains distinct from false or true; no new email, identity lookup or login default is inserted.</value>
        public bool? UpdateScopevisioEMail { get { return Value<bool?>("updateScopevisioEMail"); } set { SetOptional("updateScopevisioEMail", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original updateTeamworkEMail value.</summary>
        /// <value>The optional explicit server Teamwork login-renaming choice. Omission remains distinct from false or true; the client does not map a tenant/user identity or make a separate Teamwork request.</value>
        public bool? UpdateTeamworkEMail { get { return Value<bool?>("updateTeamworkEMail"); } set { SetOptional("updateTeamworkEMail", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original newEmail value.</summary>
        /// <value>The optional original new email string without normalization or automatically selected Scopevisio/Teamwork rename flags.</value>
        public string NewEmail { get { return Value<string>("newEmail"); } set { SetOptional("newEmail", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original teamworkRoles value.</summary>
        /// <value>The optional deeply copied Teamwork-role-assignment array, including empty arrays. Each supplied entry requires original teamworkRole and organisation strings. No role/organisation/tenant translation, lookup, merge or default assignment occurs.</value>
        public JArray TeamworkRoles { get { return (JArray)(this["teamworkRoles"] as JArray)?.DeepClone(); } set { SetOptional("teamworkRoles", value?.DeepClone()); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal static void Validate(JObject row, bool creating)
        {
            foreach (var name in new[] { "status", "specialRights", "creationTs" })
                if (row.Property(name) != null) throw new ArgumentException("The " + name + " field is read-only and cannot be sent in a user write request.", "request");
            foreach (var name in new[] { "uid", "firstName", "lastName", "gender", "email", "pendingLogin", "shortName", "newEmail" }) ValidateKind(row[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "administrator", "changePassword", "updateScopevisioEMail", "updateTeamworkEMail" }) ValidateKind(row[name], name, JTokenType.Boolean, creating);
            foreach (var name in new[] { "profiles", "licenses", "teamworkRoles" }) ValidateKind(row[name], name, JTokenType.Array, creating);
            if (creating)
                foreach (var name in new[] { "email", "firstName", "lastName" })
                    if (row[name]?.Type != JTokenType.String) throw new ArgumentException("A user creation row requires an original " + name + " string.", "request");
            ValidateAssignments(row["profiles"], new[] { "name", "organisation" }, false);
            ValidateAssignments(row["licenses"], new[] { "type" }, true);
            ValidateAssignments(row["teamworkRoles"], new[] { "teamworkRole", "organisation" }, false);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateAssignments(JToken value, string[] required, bool license)
        {
            if (!(value is JArray entries)) return;
            foreach (var item in entries)
            {
                if (!(item is JObject entry)) throw new ArgumentException("Each user assignment must be an original JSON object.", "request");
                if (license && entry.Property("validTill") != null) throw new ArgumentException("License validTill is read-only and cannot be written.", "request");
                foreach (var name in required)
                    if (entry[name]?.Type != JTokenType.String) throw new ArgumentException("A supplied assignment requires its original " + name + " string.", "request");
            }
        }
    }
}
