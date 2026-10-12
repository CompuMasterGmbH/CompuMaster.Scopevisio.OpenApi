using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied account-mapping creation values while retaining complete JSON.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and explicit JSON null. Response-only entry identifiers/timestamps are not populated automatically.</remarks>
    public class AccountMappingCreationRequest : JObject
    {
        /// <summary>Initializes an empty request whose schema-required fields must be set before sending a body.</summary>
        public AccountMappingCreationRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public AccountMappingCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The required original description string when a body is supplied. Empty text is retained for server validation.</value>
        public string Description
        {
            get { return Value<string>("description"); }
            set { SetOptional("description", value != null ? new JValue(value) : null); }
        }

        /// <summary>Gets or sets the accounting area.</summary>
        /// <value>The optional Int64 area identifier, or null to retain the server's active-area choice.</value>
        public long? AccountingArea
        {
            get { return Value<long?>("accountingArea"); }
            set { SetOptional("accountingArea", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the include personal accounts.</summary>
        /// <value>The explicit personal-account selection, or null to omit it; no undocumented default is imposed.</value>
        public bool? IncludePersonalAccounts
        {
            get { return Value<bool?>("includePersonalAccounts"); }
            set { SetOptional("includePersonalAccounts", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the valid from.</summary>
        /// <value>The original optional millisecond timestamp, or null to omit it. No range or timezone conversion is added.</value>
        public long? ValidFrom
        {
            get { return Value<long?>("validFrom"); }
            set { SetOptional("validFrom", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the valid till.</summary>
        /// <value>The original optional millisecond timestamp, or null to omit it. No range or timezone conversion is added.</value>
        public long? ValidTill
        {
            get { return Value<long?>("validTill"); }
            set { SetOptional("validTill", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the gl account length from.</summary>
        /// <value>The original optional Int64 length selection, or null to omit it. The server validates ranges.</value>
        public long? GlAccountLengthFrom
        {
            get { return Value<long?>("glAccountLengthFrom"); }
            set { SetOptional("glAccountLengthFrom", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the gl account length to.</summary>
        /// <value>The original optional Int64 length selection, or null to omit it. The server validates ranges.</value>
        public long? GlAccountLengthTo
        {
            get { return Value<long?>("glAccountLengthTo"); }
            set { SetOptional("glAccountLengthTo", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the personal account length from.</summary>
        /// <value>The original optional Int64 length selection, or null to omit it. The server validates ranges.</value>
        public long? PersonalAccountLengthFrom
        {
            get { return Value<long?>("personalAccountLengthFrom"); }
            set { SetOptional("personalAccountLengthFrom", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the personal account length to.</summary>
        /// <value>The original optional Int64 length selection, or null to omit it. The server validates ranges.</value>
        public long? PersonalAccountLengthTo
        {
            get { return Value<long?>("personalAccountLengthTo"); }
            set { SetOptional("personalAccountLengthTo", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the account format.</summary>
        /// <value>The original optional format string, or null to omit it.</value>
        public string AccountFormat
        {
            get { return Value<string>("accountFormat"); }
            set { SetOptional("accountFormat", value != null ? new JValue(value) : null); }
        }

        /// <summary>Gets or sets the account type.</summary>
        /// <value>The original optional account type, or null to omit it.</value>
        public string AccountType
        {
            get { return Value<string>("accountType"); }
            set { SetOptional("accountType", value != null ? new JValue(value) : null); }
        }

        /// <summary>Gets or sets the entries.</summary>
        /// <value>The complete copied entry array, an explicit empty array, or null to omit it. Supplied entries require original accountScope/accountExternal strings.</value>
        public JArray Entries
        {
            get { return (JArray)(this["entries"] as JArray)?.DeepClone(); }
            set { SetOptional("entries", value != null ? value.DeepClone() : null); }
        }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
