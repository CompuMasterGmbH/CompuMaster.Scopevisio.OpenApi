using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit revenue-account choices for an original product number.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers retain unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class ProductRevenueAccountEntryRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults or automatic selections.</summary>
        public ProductRevenueAccountEntryRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProductRevenueAccountEntryRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original productNumber selection.</summary>
        /// <value>The required original product-number string, retaining leading zeros and empty text for server validation.</value>
        public string ProductNumber
        {
            get { return Value<string>("productNumber"); }
            set { if (value != null) this["productNumber"] = new JValue(value); else Remove("productNumber"); }
        }

        /// <summary>Gets or sets the original useDefaultAccounts selection.</summary>
        /// <value>The optional original true/false/inherit string, or null to omit it. The prose also says INHERIT; no case conversion is imposed. Enabling defaults does not delete non-default assignments according to the manufacturer.</value>
        public string UseDefaultAccounts
        {
            get { return Value<string>("useDefaultAccounts"); }
            set { if (value != null) this["useDefaultAccounts"] = new JValue(value); else Remove("useDefaultAccounts"); }
        }

        /// <summary>Gets or sets the original accounts selection.</summary>
        /// <value>The complete copied optional non-default account assignments, including empty arrays, or null to omit them.</value>
        public JArray Accounts
        {
            get { return (JArray)(this["accounts"] as JArray)?.DeepClone(); }
            set { if (value != null) this["accounts"] = value.DeepClone(); else Remove("accounts"); }
        }

    }
}
