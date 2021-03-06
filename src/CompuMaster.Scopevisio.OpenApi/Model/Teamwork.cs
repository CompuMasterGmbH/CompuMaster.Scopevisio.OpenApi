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
    /// Teamwork
    /// </summary>
    [DataContract]
    public partial class Teamwork :  IEquatable<Teamwork>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Teamwork" /> class.
        /// </summary>
        /// <param name="teamworkAppRootUrl">teamworkAppRootUrl.</param>
        public Teamwork(string teamworkAppRootUrl = default(string))
        {
            this.TeamworkAppRootUrl = teamworkAppRootUrl;
        }
        
        /// <summary>
        /// Gets or Sets TeamworkAppRootUrl
        /// </summary>
        [DataMember(Name="teamworkAppRootUrl", EmitDefaultValue=false)]
        public string TeamworkAppRootUrl { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Teamwork {\n");
            sb.Append("  TeamworkAppRootUrl: ").Append(TeamworkAppRootUrl).Append("\n");
            sb.Append("}");
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
            return this.Equals(input as Teamwork);
        }

        /// <summary>
        /// Returns true if Teamwork instances are equal
        /// </summary>
        /// <param name="input">Instance of Teamwork to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Teamwork input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.TeamworkAppRootUrl == input.TeamworkAppRootUrl ||
                    (this.TeamworkAppRootUrl != null &&
                    this.TeamworkAppRootUrl.Equals(input.TeamworkAppRootUrl))
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
                if (this.TeamworkAppRootUrl != null)
                    hashCode = hashCode * 59 + this.TeamworkAppRootUrl.GetHashCode();
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
