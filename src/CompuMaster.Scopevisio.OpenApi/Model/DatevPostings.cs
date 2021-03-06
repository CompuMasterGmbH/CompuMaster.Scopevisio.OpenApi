/* 
 * Scopevisio OpenScope REST API
 *
 * <p>This is an interactive API reference for the Scopevisio REST API.</p><p>Below you will see the main sections of the API. Click each section in order to see the endpoints that are available in that category and use the 'Try it out' button to make API calls.</p><p>For endpoints that require authentication you can click the lock symbol in the top right of the endpoint description, which will take you to a login form.</p><h3>Authentication</h3><p>In order to use the \"Try it out\" function in Swagger you need your own Scopevisio account. If you have not set up a Scopevisio user account already you can do so <a href='https://www.scopevisio.com' target='_blank'>here</a>.</p><h3>Example Application</h3><p>We also have an example application you can reach <a href='../browser/index.html'> here </a>. Usage requires a Scopevisio account.</p><h3>Search Documentation</h3><p>Our documentation regarding <a href='../browser/index.html#!/searchscope'>search specification and paging  </a>.</p><h3>General Documentation</h3><p>Our general documentation can be found <a href='../browser/index.html#!/documentation'> here </a>.</p>
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: support@scopevisio.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

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
    /// DatevPostings
    /// </summary>
    [DataContract]
    public partial class DatevPostings :  IEquatable<DatevPostings>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatevPostings" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DatevPostings() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatevPostings" /> class.
        /// </summary>
        /// <param name="zone">Zone (Rechnungskreis) If this parameter is mandatory depends on the zone settings in the Scopevisio desktop application. If set to mandatory and not given, the standard zone is used..</param>
        /// <param name="data">A DATEV-Importfile (EXTF) in Base64 format. (required).</param>
        public DatevPostings(string zone = default(string), string data = default(string))
        {
            // to ensure "data" is required (not null)
            if (data == null)
            {
                throw new InvalidDataException("data is a required property for DatevPostings and cannot be null");
            }
            else
            {
                this.Data = data;
            }
            
            this.Zone = zone;
        }
        
        /// <summary>
        /// Zone (Rechnungskreis) If this parameter is mandatory depends on the zone settings in the Scopevisio desktop application. If set to mandatory and not given, the standard zone is used.
        /// </summary>
        /// <value>Zone (Rechnungskreis) If this parameter is mandatory depends on the zone settings in the Scopevisio desktop application. If set to mandatory and not given, the standard zone is used.</value>
        [DataMember(Name="zone", EmitDefaultValue=false)]
        public string Zone { get; set; }

        /// <summary>
        /// A DATEV-Importfile (EXTF) in Base64 format.
        /// </summary>
        /// <value>A DATEV-Importfile (EXTF) in Base64 format.</value>
        [DataMember(Name="data", EmitDefaultValue=true)]
        public string Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DatevPostings {\n");
            sb.Append("  Zone: ").Append(Zone).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
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
            return this.Equals(input as DatevPostings);
        }

        /// <summary>
        /// Returns true if DatevPostings instances are equal
        /// </summary>
        /// <param name="input">Instance of DatevPostings to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DatevPostings input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Zone == input.Zone ||
                    (this.Zone != null &&
                    this.Zone.Equals(input.Zone))
                ) && 
                (
                    this.Data == input.Data ||
                    (this.Data != null &&
                    this.Data.Equals(input.Data))
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
                if (this.Zone != null)
                    hashCode = hashCode * 59 + this.Zone.GetHashCode();
                if (this.Data != null)
                    hashCode = hashCode * 59 + this.Data.GetHashCode();
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
