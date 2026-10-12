using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit product revenue-account creation entries.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers retain unknown fields and optional explicit JSON null. Arrays and source objects are deep copies; required known fields are validated before transport.</remarks>
    public class ProductRevenueAccountsCreationRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults or automatic selections.</summary>
        public ProductRevenueAccountsCreationRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProductRevenueAccountsCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original entries selection.</summary>
        /// <value>The complete copied optional entry array, including explicit empty arrays. No schema minimum is imposed.</value>
        public JArray Entries
        {
            get { return (JArray)(this["entries"] as JArray)?.DeepClone(); }
            set { if (value != null) this["entries"] = value.DeepClone(); else Remove("entries"); }
        }

    }
}
