using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains a VAT scope record with typed access and unknown JSON fields.</summary>
    public class VatScopeRecord : JObject
    {
        /// <summary>Initializes the result by deeply copying a JSON object.</summary>
        /// <param name="other">The object to copy, including unknown fields and omitted or null values.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public VatScopeRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
        }

        /// <summary>Gets the scope identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? Id => this.Value<long?>("id");

        /// <summary>Gets the accounting area identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? Area => this.Value<long?>("area");

        /// <summary>Gets the unchanged creation timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? CreatedTs => this.Value<long?>("createdTs");

        /// <summary>Gets the creator identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CreatorUid => this.Value<string>("creatorUid");

        /// <summary>Gets the creator name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CreatorName => this.Value<string>("creatorName");

        /// <summary>Gets the modifier identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string ModifierUid => this.Value<string>("modifierUid");

        /// <summary>Gets the modifier name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string ModifierName => this.Value<string>("modifierName");

        /// <summary>Gets the unchanged modification timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ModifiedTs => this.Value<long?>("modifiedTs");

        /// <summary>Gets whether the scope is active.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public bool? Active => this.Value<bool?>("active");

        /// <summary>Gets the VAT case identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? CaseId => this.Value<long?>("caseId");

        /// <summary>Gets the VAT case name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CaseName => this.Value<string>("caseName");

        /// <summary>Gets the VAT case description.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CaseDescription => this.Value<string>("caseDescription");
    }
}
