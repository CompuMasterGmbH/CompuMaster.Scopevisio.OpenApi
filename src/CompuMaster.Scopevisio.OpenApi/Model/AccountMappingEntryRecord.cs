using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains an account mapping entry with typed access and unknown JSON fields.</summary>
    public class AccountMappingEntryRecord : JObject
    {
        /// <summary>Initializes the result by deeply copying a JSON object.</summary>
        /// <param name="other">The object to copy, including unknown fields and omitted or null values.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public AccountMappingEntryRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
        }

        /// <summary>Gets the entry identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? Id => this.Value<long?>("id");

        /// <summary>Gets the source account number, including leading zeros.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AccountScope => this.Value<string>("accountScope");

        /// <summary>Gets the target account number, including leading zeros.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AccountExternal => this.Value<string>("accountExternal");

        /// <summary>Gets the unchanged last-modification timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ModifiedTs => this.Value<long?>("modifiedTs");
    }
}
