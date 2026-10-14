using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current dimension-entry write fields.</summary>
    /// <remarks>Creation requires explicit number, name and locked. Name updates require number and name while retaining omitted lock state in the request. Typed null setters omit fields; no lock, identifier or name default is inserted. Actual additional-field update effects remain server-authoritative. Legacy DimensionEntryForm behavior is preserved.</remarks>
    public class DimensionEntryWriteRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults.</summary>
        public DimensionEntryWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete original JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public DimensionEntryWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original number value.</summary>
        /// <value>The explicit original nullable Int64 entry number, required for both creation and name update. It identifies an entry within the separately selected dimension and is never looked up or converted.</value>
        public long? Number { get { return Value<long?>("number"); } set { SetOptional("number", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original name value.</summary>
        /// <value>The original name string, required for creation and name update. Text is not trimmed, synthesized or looked up; empty text remains server-authoritative.</value>
        public string Name { get { return Value<string>("name"); } set { SetOptional("name", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original locked value.</summary>
        /// <value>The optional explicit lock boolean. Creation requires a supplied value; false remains distinct from omission. Name updates do not insert a lock value, and actual lock-field update behavior remains unverified.</value>
        public bool? Locked { get { return Value<bool?>("locked"); } set { SetOptional("locked", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original legacyId value.</summary>
        /// <value>The optional original legacy-ID string, or null to omit it. It is not converted into an entry number or used for lookup; the server determines update support.</value>
        public string LegacyId { get { return Value<string>("legacyId"); } set { SetOptional("legacyId", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "name", "legacyId" }) ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "locked" }) ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            ValidateInteger(snapshot["number"], "number", true, creating);
            if (snapshot["number"]?.Type != JTokenType.Integer || snapshot["name"]?.Type != JTokenType.String || (creating && snapshot["locked"]?.Type != JTokenType.Boolean))
                throw new ArgumentException("A dimension-entry request requires number and name; creation also requires an explicit locked boolean.", "request");
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
