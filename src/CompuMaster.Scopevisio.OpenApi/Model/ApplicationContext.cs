using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = CompuMaster.Scopevisio.OpenApi.Client.OpenAPIDateConverter;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>
    /// Application context
    /// </summary>
    [DataContract]
    public class ApplicationContext
    {
        /// <summary>
        /// Create an empty record list 
        /// </summary>
        public ApplicationContext()
        {
        }

        /// <summary>
        /// The customer
        /// </summary>
        [DataMember(Name = "customer", EmitDefaultValue = false)]
        public Model.Customer Customer { get; set; }

        /// <summary>
        /// The organisation
        /// </summary>
        [DataMember(Name = "organisation", EmitDefaultValue = false)]
        public Model.Organisation Organisation { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        public Model.User User { get; set; }

        /// <summary>
        /// The teamwork module
        /// </summary>
        [DataMember(Name = "teamwork", EmitDefaultValue = false)]
        public Model.Teamwork Teamwork { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApplicationContext {\n");
            sb.Append("  Customer:\n").Append(Customer.ToString().Indent()).Append("\n");
            sb.Append("  Organisation:\n").Append(Organisation.ToString().Indent()).Append("\n");
            sb.Append("  User:\n").Append(User.ToString().Indent()).Append("\n");
            sb.Append("  Teamwork:\n").Append(Teamwork.ToString().Indent()).Append("\n");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
