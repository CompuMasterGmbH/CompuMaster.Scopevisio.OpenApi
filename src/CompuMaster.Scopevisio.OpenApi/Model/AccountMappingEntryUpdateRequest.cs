using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied account-mapping entry update values while retaining complete JSON.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and explicit JSON null. Response-only entry identifiers/timestamps are not populated automatically.</remarks>
    public class AccountMappingEntryUpdateRequest : JObject
    {
        /// <summary>Initializes an empty request whose schema-required fields must be set before sending a body.</summary>
        public AccountMappingEntryUpdateRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public AccountMappingEntryUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the account scope.</summary>
        /// <value>The required original accountScope string when a body is supplied, retaining leading zeros and empty text. The SDK does not infer mapping direction from this name.</value>
        public string AccountScope
        {
            get { return Value<string>("accountScope"); }
            set { SetOptional("accountScope", value != null ? new JValue(value) : null); }
        }

        /// <summary>Gets or sets the account external.</summary>
        /// <value>The required original accountExternal string when a body is supplied, retaining leading zeros and empty text. The server determines chart membership and mapping semantics.</value>
        public string AccountExternal
        {
            get { return Value<string>("accountExternal"); }
            set { SetOptional("accountExternal", value != null ? new JValue(value) : null); }
        }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
