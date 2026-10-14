using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    public partial class User
    {
        /// <summary>Gets or sets the optional license metadata returned for the account user.</summary>
        /// <value>The original ordered list, including duplicates or an empty list, or null when absent. License types and read-only signed Int64 expiry values remain server-supplied metadata.</value>
        /// <remarks>No license purchase, expiry calculation, permission decision or account selection is performed. Existing generated equality, hash and diagnostic formatting retain their original fields.</remarks>
        [DataMember(Name = "licenses", EmitDefaultValue = false)]
        public List<License> Licenses { get; set; }

        /// <summary>Gets or sets the optional profile metadata returned for the account user.</summary>
        /// <value>The original ordered list, including duplicates or an empty list, or null when absent. Profile and organisation names remain original strings; instance-wide assignments are distinct from numeric organisation and Teamwork tenant identities.</value>
        /// <remarks>No administrator or profile grant, organisation lookup, permission inference or tenant selection is performed. Existing generated equality, hash and diagnostic formatting retain their original fields.</remarks>
        [DataMember(Name = "profiles", EmitDefaultValue = false)]
        public List<Profile> Profiles { get; set; }
    }
}
