using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Provides typed e-commerce SKU fields while retaining complete unknown JSON data.</summary>
    public class ECommerceSkuRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The record to copy, including unknown fields and explicit nulls.</param>
        /// <exception cref="ArgumentNullException">The source record is null.</exception>
        public ECommerceSkuRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the SKU identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the organisation identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Organisation { get { return Value<long?>("organisation"); } }

        /// <summary>Gets the product number without integer conversion.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProductNumber { get { return Value<string>("productNumber"); } }

        /// <summary>Gets the e-commerce account identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ECommerceAccount { get { return Value<long?>("ecommerceAccount"); } }

        /// <summary>Gets the e-commerce account display text.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ECommerceAccountDisplay { get { return Value<string>("ecommerceAccountDisplay"); } }

        /// <summary>Gets the platform number without integer conversion.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string PlatformNumber { get { return Value<string>("platformNumber"); } }
    }
}
