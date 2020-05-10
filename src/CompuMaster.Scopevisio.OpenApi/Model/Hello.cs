using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = CompuMaster.Scopevisio.OpenApi.Client.OpenAPIDateConverter;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>
    /// Hello
    /// </summary>
    [DataContract]
    public partial class Hello :  IEquatable<Hello>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hello" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Hello() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hello" /> class.
        /// </summary>
        /// <param name="hello">Hello</param>
        public Hello(string hello = default(string))
        {
            this.HelloInfo = hello;
        }


        /// <summary>
        /// Hello
        /// </summary>
        /// <value>Hello</value>
        [DataMember(Name= "hello", EmitDefaultValue=false)]
        public string HelloInfo { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Hello {\n");
            sb.Append("  HelloInfo:   ").Append(HelloInfo).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Version);
        }

        /// <summary>
        /// Returns true if Hello instances are equal
        /// </summary>
        /// <param name="input">Instance of Hello to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Hello input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.HelloInfo == input.HelloInfo ||
                    (this.HelloInfo != null &&
                    this.HelloInfo.Equals(input.HelloInfo))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.HelloInfo != null)
                    hashCode = hashCode * 59 + this.HelloInfo.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
