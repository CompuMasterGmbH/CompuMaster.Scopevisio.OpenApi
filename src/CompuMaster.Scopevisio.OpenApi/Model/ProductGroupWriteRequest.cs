using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current product-group creation fields.</summary>
    /// <remarks>The original name and future JSON fields are copied before sending. No group name or catalogue default is selected automatically. Legacy ProductGroupForm behavior is preserved.</remarks>
    public class ProductGroupWriteRequest : JObject
    {
        /// <summary>Initializes an empty product-group request without selecting a name.</summary>
        public ProductGroupWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete product-group JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProductGroupWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original product-group name.</summary>
        /// <value>The explicit creation name string. Null omits it; the server validates unchanged empty or whitespace text.</value>
        public string Name { get { return Value<string>("name"); } set { if (value == null) Remove("name"); else this["name"] = value; } }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            if (snapshot["name"]?.Type != JTokenType.String)
                throw new ArgumentException("Product-group creation requires an explicit name string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
