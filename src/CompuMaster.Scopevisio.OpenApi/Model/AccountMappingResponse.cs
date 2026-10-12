using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains the complete account mapping response with typed access and unknown JSON fields.</summary>
    public class AccountMappingResponse : JObject
    {
        /// <summary>Initializes the result by deeply copying a JSON object.</summary>
        /// <param name="other">The object to copy, including unknown fields and omitted or null values.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        /// <exception cref="JsonSerializationException">The entries field is not an array of JSON objects.</exception>
        public AccountMappingResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
            var values = this["entries"];
            if (values != null && values.Type != JTokenType.Null &&
                (!(values is JArray array) || array.Any(value => !(value is JObject))))
                throw new JsonSerializationException("The entries field must be an array of JSON objects.");
        }

        /// <summary>Gets the mapping identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? Id => this.Value<long?>("id");

        /// <summary>Gets the accounting area identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? AccountingArea => this.Value<long?>("accountingArea");

        /// <summary>Gets the mapping description.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string Description => this.Value<string>("description");

        /// <summary>Gets whether personal accounts are included.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public bool? IncludePersonalAccounts => this.Value<bool?>("includePersonalAccounts");

        /// <summary>Gets the unchanged valid-from timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ValidFrom => this.Value<long?>("validFrom");

        /// <summary>Gets the unchanged valid-until timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ValidTill => this.Value<long?>("validTill");

        /// <summary>Gets the account format.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AccountFormat => this.Value<string>("accountFormat");

        /// <summary>Gets the minimum general ledger account length.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? GlAccountLengthFrom => this.Value<long?>("glAccountLengthFrom");

        /// <summary>Gets the maximum general ledger account length.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? GlAccountLengthTo => this.Value<long?>("glAccountLengthTo");

        /// <summary>Gets the minimum personal account length.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? PersonalAccountLengthFrom => this.Value<long?>("personalAccountLengthFrom");

        /// <summary>Gets the maximum personal account length.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? PersonalAccountLengthTo => this.Value<long?>("personalAccountLengthTo");

        /// <summary>Gets the account type.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AccountType => this.Value<string>("accountType");

        /// <summary>Gets the reported entry count.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public int? EntryCount => this.Value<int?>("entryCount");

        /// <summary>Gets typed copies of the entries.</summary>
        /// <value>A read-only list of copied records, or null when the entries field is omitted or JSON null.</value>
        public IReadOnlyList<AccountMappingEntryRecord> Entries
        {
            get
            {
                var values = this["entries"] as JArray;
                return values == null ? null : Array.AsReadOnly(values.Select(value => new AccountMappingEntryRecord((JObject)value)).ToArray());
            }
        }
    }
}
