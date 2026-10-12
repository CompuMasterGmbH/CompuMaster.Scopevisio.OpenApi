using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an original non-default revenue-account assignment.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers retain unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class ProductRevenueAccountAssignmentRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults or automatic selections.</summary>
        public ProductRevenueAccountAssignmentRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProductRevenueAccountAssignmentRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original accountNumber selection.</summary>
        /// <value>The required original account-number string, retaining leading zeros and empty text for server validation.</value>
        public string AccountNumber
        {
            get { return Value<string>("accountNumber"); }
            set { if (value != null) this["accountNumber"] = new JValue(value); else Remove("accountNumber"); }
        }

        /// <summary>Gets or sets the original taxCase selection.</summary>
        /// <value>The optional original tax-case name, or null to omit it.</value>
        public string TaxCase
        {
            get { return Value<string>("taxCase"); }
            set { if (value != null) this["taxCase"] = new JValue(value); else Remove("taxCase"); }
        }

        /// <summary>Gets or sets the original taxCaseId selection.</summary>
        /// <value>The original optional Int64 tax-case selection, or null to omit it; server identity rules remain authoritative.</value>
        public long? TaxCaseId
        {
            get { return Value<long?>("taxCaseId"); }
            set { if (value.HasValue) this["taxCaseId"] = new JValue(value.Value); else Remove("taxCaseId"); }
        }

        /// <summary>Gets or sets the original countryIsoCode selection.</summary>
        /// <value>The optional original country-code string, or null to omit it. No catalog or case conversion is added.</value>
        public string CountryIsoCode
        {
            get { return Value<string>("countryIsoCode"); }
            set { if (value != null) this["countryIsoCode"] = new JValue(value); else Remove("countryIsoCode"); }
        }

        /// <summary>Gets or sets the original validFrom selection.</summary>
        /// <value>The optional original date string, or null to omit it. The contract conflicts between yyyy-MM-dd and dd.MM.yyyy; server acceptance remains authoritative.</value>
        public string ValidFrom
        {
            get { return Value<string>("validFrom"); }
            set { if (value != null) this["validFrom"] = new JValue(value); else Remove("validFrom"); }
        }

        /// <summary>Gets or sets the original validTo selection.</summary>
        /// <value>The optional original date string, or null to omit it. No date ordering, format conversion or year boundary is inferred.</value>
        public string ValidTo
        {
            get { return Value<string>("validTo"); }
            set { if (value != null) this["validTo"] = new JValue(value); else Remove("validTo"); }
        }

    }
}
