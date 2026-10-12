using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit VAT matrix entry while retaining unknown JSON fields.</summary>
    /// <remarks>The schema requires no individual fields. Typed null setters omit fields; explicit JSON null remains representable through the inherited indexer. The server validates account/rate/catalog/identity semantics.</remarks>
    public class VatMatrixEntryRequest : JObject
    {
        /// <summary>Initializes an empty entry without inferring a tax rate or account.</summary>
        public VatMatrixEntryRequest() { }

        /// <summary>Initializes an entry by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source entry is null.</exception>
        public VatMatrixEntryRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The optional original Int64 entry identity used for PATCH matching, or null to omit it. No current identity is inferred.</value>
        public long? Id { get { return Value<long?>("id"); } set { SetOptional("id", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the vat rate.</summary>
        /// <value>The optional original percentage rate, or null to omit it. No local range, scale or rounding is imposed; raw numbers remain available.</value>
        public decimal? VatRate { get { return Value<decimal?>("vatRate"); } set { SetOptional("vatRate", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the datev key.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string DatevKey { get { return Value<string>("datevKey"); } set { SetOptional("datevKey", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the datev key reduced.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string DatevKeyReduced { get { return Value<string>("datevKeyReduced"); } set { SetOptional("datevKeyReduced", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the vat key.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string VatKey { get { return Value<string>("vatKey"); } set { SetOptional("vatKey", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the vat key description.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string VatKeyDescription { get { return Value<string>("vatKeyDescription"); } set { SetOptional("vatKeyDescription", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the vat code.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string VatCode { get { return Value<string>("vatCode"); } set { SetOptional("vatCode", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the purchase account.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string PurchaseAccount { get { return Value<string>("purchaseAccount"); } set { SetOptional("purchaseAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the sales account.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string SalesAccount { get { return Value<string>("salesAccount"); } set { SetOptional("salesAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the sales not due account.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string SalesNotDueAccount { get { return Value<string>("salesNotDueAccount"); } set { SetOptional("salesNotDueAccount", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the cash discount account.</summary>
        /// <value>The optional original string, retaining empty text and leading zeros, or null to omit it. Server account/catalog rules remain authoritative.</value>
        public string CashDiscountAccount { get { return Value<string>("cashDiscountAccount"); } set { SetOptional("cashDiscountAccount", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
