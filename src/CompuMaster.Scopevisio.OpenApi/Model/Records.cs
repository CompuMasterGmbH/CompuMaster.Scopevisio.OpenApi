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
    /// A list of records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class Records<T>
    {
        /// <summary>
        /// Create an empty record list 
        /// </summary>
        public Records()
        {
        }

        /// <summary>
        /// The records
        /// </summary>
        [DataMember(Name = "records", EmitDefaultValue = false)]
        public IList<T> Items { get; set; }
    }
}
