using System.Runtime.Serialization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    public partial class Customer
    {
        /// <summary>Gets or sets the optional original customer origin metadata returned in the account context.</summary>
        /// <value>The original string, including empty text, or null when absent. The manufacturer does not define further semantics or a fixed set of values.</value>
        /// <remarks>This value does not select or establish a server address, authentication endpoint, organisation, user or Teamwork tenant. Existing generated equality, hash and diagnostic formatting retain their original fields.</remarks>
        [DataMember(Name = "origin", EmitDefaultValue = false)]
        public string Origin { get; set; }
    }
}
