using System.Runtime.Serialization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    public partial class TokenResponse
    {
        /// <summary>Gets or sets the optional original customer identifier returned with the token.</summary>
        /// <value>The customer/tenant identifier string, or null when absent. It is distinct from numeric Scope organisation, user and opaque Teamwork tenant identifiers.</value>
        /// <remarks>The value is metadata and does not establish selected organisation, user or Teamwork tenant scope. Existing generated equality, hash and diagnostic formatting remain unchanged.</remarks>
        [DataMember(Name = "customerId", EmitDefaultValue = false)]
        public string CustomerId { get; set; }

        /// <summary>Gets or sets the optional original public customer number returned with the token.</summary>
        /// <value>The original string, including leading zeros or empty text, or null when absent. No numeric conversion or account selection is performed.</value>
        /// <remarks>The value is metadata and does not replace any organisation, user or Teamwork tenant identity proof. Existing generated equality, hash and diagnostic formatting remain unchanged.</remarks>
        [DataMember(Name = "publicId", EmitDefaultValue = false)]
        public string PublicId { get; set; }
    }
}
