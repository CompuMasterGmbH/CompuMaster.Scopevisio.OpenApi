using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains copied current organisation creation fields.</summary>
    /// <remarks>The route body is optional; a supplied object requires its original organisationName. Optional scoa remains omitted without chart setup or a default. Original strings and future JSON are copied without lookup, upsert/reuse, context selection or timeout changes. Actual global organisation/chart/profile behaviour and owned cleanup remain unverified; historical models/defaults remain unchanged.</remarks>
    public sealed class OrganisationCreationRequest : JObject
    {
        /// <summary>Initializes an empty copied organisation request.</summary>
        public OrganisationCreationRequest() { }

        /// <summary>Initializes a request by deeply copying original JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OrganisationCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original organisation name.</summary>
        /// <value>The required supplied-object string, or null to omit it while constructing a request. Empty text, whitespace and case remain server-authoritative without trimming, lookup, default naming or reuse of an existing organisation.</value>
        public string OrganisationName { get { return Value<string>("organisationName"); } set { SetOptional("organisationName", value); } }

        /// <summary>Gets or sets the original standard chart-of-accounts selection.</summary>
        /// <value>The optional original scoa string, including empty text, or null to omit it. Omission leaves chart setup to the user on the server; the client neither selects a chart nor normalizes or resolves its name.</value>
        public string Scoa { get { return Value<string>("scoa"); } set { SetOptional("scoa", value); } }

        private void SetOptional(string name, string value)
        {
            if (value == null) Remove(name); else this[name] = new JValue(value);
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "organisationName", "scoa" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be an original JSON string.", "request");
            if (snapshot["organisationName"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied organisation object requires an original organisationName string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
