using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains a revenue-account record with typed access and unknown JSON fields.</summary>
    public class RevenueAccountRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The source object, including unknown fields and omitted/null values.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public RevenueAccountRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the revenue-account record identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? Id => this.Value<long?>("id");

        /// <summary>Gets the accounting area identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? AccountingArea => this.Value<long?>("accountingArea");

        /// <summary>Gets the unchanged creation timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? CreatedTs => this.Value<long?>("createdTs");

        /// <summary>Gets the creator identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CreatorUid => this.Value<string>("creatorUid");

        /// <summary>Gets the creator name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CreatorName => this.Value<string>("creatorName");

        /// <summary>Gets the unchanged modification timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ModifiedTs => this.Value<long?>("modifiedTs");

        /// <summary>Gets the modifier identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string ModifierUid => this.Value<string>("modifierUid");

        /// <summary>Gets the modifier name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string ModifierName => this.Value<string>("modifierName");

        /// <summary>Gets the account identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? AccountId => this.Value<long?>("accountId");

        /// <summary>Gets the account number, including leading zeros.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AccountNumber => this.Value<string>("accountNumber");

        /// <summary>Gets the VAT key.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string VatKey => this.Value<string>("vatKey");

        /// <summary>Gets whether the record uses the VAT matrix.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public bool? Matrix => this.Value<bool?>("matrix");

        /// <summary>Gets the tax case identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? TaxCaseId => this.Value<long?>("taxCaseId");

        /// <summary>Gets the tax case name.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string TaxCaseName => this.Value<string>("taxCaseName");

        /// <summary>Gets the tax type identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? TaxTypeId => this.Value<long?>("taxTypeId");

        /// <summary>Gets the tax type display text.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string TaxTypeDisplay => this.Value<string>("taxTypeDisplay");

        /// <summary>Gets the tax key.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string TaxKey => this.Value<string>("taxKey");

        /// <summary>Gets the account description.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string Description => this.Value<string>("description");

        /// <summary>Gets the country code.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CountryCode => this.Value<string>("countryCode");

        /// <summary>Gets the country ISO code.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string CountryIso => this.Value<string>("countryIso");

        /// <summary>Gets the original country code.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string OriginalCountryCode => this.Value<string>("originalCountryCode");

        /// <summary>Gets the original country ISO code.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string OriginalCountryIso => this.Value<string>("originalCountryIso");

        /// <summary>Gets the product identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ProductId => this.Value<long?>("productId");

        /// <summary>Gets the unchanged product type code, including future codes.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ProductType => this.Value<long?>("productType");

        /// <summary>Gets the product type display text.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string ProductTypeDisplay => this.Value<string>("productTypeDisplay");

        /// <summary>Gets the merchandise group identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? MerchandiseGroupId => this.Value<long?>("merchandiseGroupId");

        /// <summary>Gets the unchanged valid-from timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ValidFrom => this.Value<long?>("validFrom");

        /// <summary>Gets the unchanged valid-until timestamp.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? ValidTill => this.Value<long?>("validTill");

        /// <summary>Gets whether reverse-charge treatment applies.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public bool? ReverseCharge => this.Value<bool?>("reverseCharge");

        /// <summary>Gets whether advance-payment treatment applies.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public bool? Advance => this.Value<bool?>("advance");

        /// <summary>Gets the advance account identifier.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public long? AdvanceAccountId => this.Value<long?>("advanceAccountId");

        /// <summary>Gets the advance account number, including leading zeros.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AdvanceAccountNumber => this.Value<string>("advanceAccountNumber");

        /// <summary>Gets the advance VAT key.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string AdvanceVatKey => this.Value<string>("advanceVatKey");

        /// <summary>Gets the VAT key description.</summary>
        /// <value>The value, or null when omitted or JSON null.</value>
        public string VatKeyDescription => this.Value<string>("vatKeyDescription");
    }
}
