using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current personal-account bank-connection fields.</summary>
    /// <remarks>All individual fields are optional in the schema. Complete JSON is copied without account, IBAN, bank, holder or standard defaults. Actual identification requirements and standard-payment implications are server-authoritative; legacy BankConnectionForm behavior remains unchanged.</remarks>
    public class BankConnectionWriteRequest : JObject
    {
        /// <summary>Initializes an empty bank-connection request without inserting defaults.</summary>
        public BankConnectionWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete original JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public BankConnectionWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original number string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string Number { get { return Value<string>("number"); } set { SetOptional("number", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original bankCode string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string BankCode { get { return Value<string>("bankCode"); } set { SetOptional("bankCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original iban string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string Iban { get { return Value<string>("iban"); } set { SetOptional("iban", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original swiftBic string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string SwiftBic { get { return Value<string>("swiftBic"); } set { SetOptional("swiftBic", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original bankName string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string BankName { get { return Value<string>("bankName"); } set { SetOptional("bankName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original accountHolder string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string AccountHolder { get { return Value<string>("accountHolder"); } set { SetOptional("accountHolder", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the explicit standard-bank-connection choice.</summary>
        /// <value>The optional boolean. Omission remains distinct from false or true without inserting false; the server determines the default connection for transfers or withdrawals. No payment action is initiated by the client.</value>
        public bool? Standard { get { return Value<bool?>("standard"); } set { SetOptional("standard", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original description string.</summary>
        /// <value>The optional original text, including empty strings, or null to omit it. The client does not trim, normalize, remove leading zeros, infer checksums or look up bank/account data; the server validates content.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "number", "bankCode", "iban", "swiftBic", "bankName", "accountHolder", "description" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be an original JSON string.", "request");
            if (snapshot["standard"] != null && snapshot["standard"].Type != JTokenType.Boolean)
                throw new ArgumentException("The standard field must be an explicitly supplied JSON boolean.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
