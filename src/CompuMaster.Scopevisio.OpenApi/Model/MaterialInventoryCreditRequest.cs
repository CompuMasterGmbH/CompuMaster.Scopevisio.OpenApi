using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original material inventory credit values.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve future fields and optional explicit JSON null. Source objects and nested values are defensive copies. Actual catalog identities, dates and material outcomes remain server-authoritative.</remarks>
    public class MaterialInventoryCreditRequest : JObject
    {
        /// <summary>Initializes an empty request without server defaults.</summary>
        public MaterialInventoryCreditRequest() { }
        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public MaterialInventoryCreditRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original materialPostingRows value.</summary>
        /// <value>The original complete copied array.</value>
        public JArray MaterialPostingRows
        {
            get { return (JArray)(this["materialPostingRows"] as JArray)?.DeepClone(); }
            set { if (value != null) this["materialPostingRows"] = value.DeepClone(); else Remove("materialPostingRows"); }
        }
    }
}
