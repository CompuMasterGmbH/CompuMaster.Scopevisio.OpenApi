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
    public partial class TeamworkToken :  IEquatable<TeamworkToken>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamworkToken" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected TeamworkToken() { }


        /// <summary>
        /// Gets or Sets Organisation
        /// </summary>
        [DataMember(Name= "organisation", EmitDefaultValue=false)]
        public int Organisation { get; set; }

        /// <summary>
        /// Teamwork uid (User ID)
        /// </summary>
        [DataMember(Name = "uid", EmitDefaultValue = false)]
        public string Uid { get; set; }

        /// <summary>
        /// Teamwork teamworkTenantId
        /// </summary>
        [DataMember(Name = "teamworkTenantId", EmitDefaultValue = false)]
        public string TeamworkTenantId { get; set; }

        /// <summary>
        /// Teamwork accessToken
        /// </summary>
        [DataMember(Name = "accessToken", EmitDefaultValue = false)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Teamwork expiresIn
        /// </summary>
        [DataMember(Name = "expiresIn", EmitDefaultValue = false)]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Teamwork refreshToken
        /// </summary>
        [DataMember(Name = "refreshToken", EmitDefaultValue = false)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TeamworkToken {\n");
            sb.Append("  Organisation: ").Append(Organisation.ToString()).Append("\n");
            sb.Append("  UserID: ").Append(Uid).Append("\n");
            sb.Append("  TeamworkTenantId: ").Append(TeamworkTenantId).Append("\n");
            sb.Append("  AccessToken: ").Append(AccessToken).Append("\n");
            sb.Append("  ExpiresIn: ").Append(ExpiresIn.ToString()).Append("\n");
            sb.Append("  RefreshToken: ").Append(RefreshToken).Append("\n");
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
        public bool Equals(TeamworkToken input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Organisation == input.Organisation ||
                    (this.Organisation != null &&
                    this.Organisation.Equals(input.Organisation))
                ) &&
                (
                    this.Uid == input.Uid ||
                    (this.Uid != null &&
                    this.Uid.Equals(input.Uid))
                ) &&
                (
                    this.TeamworkTenantId == input.TeamworkTenantId ||
                    (this.TeamworkTenantId != null &&
                    this.TeamworkTenantId.Equals(input.TeamworkTenantId))
                ) &&
                (
                    this.AccessToken == input.AccessToken ||
                    (this.AccessToken != null &&
                    this.AccessToken.Equals(input.AccessToken))
                ) &&
                (
                    this.ExpiresIn == input.ExpiresIn ||
                    (this.ExpiresIn != null &&
                    this.ExpiresIn.Equals(input.ExpiresIn))
                ) &&
                (
                    this.RefreshToken == input.RefreshToken ||
                    (this.RefreshToken != null &&
                    this.RefreshToken.Equals(input.RefreshToken))
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
                if (this.Organisation != null)
                    hashCode = hashCode * 59 + this.Organisation.GetHashCode();
                if (this.Uid != null)
                    hashCode = hashCode * 59 + this.Uid.GetHashCode();
                if (this.TeamworkTenantId != null)
                    hashCode = hashCode * 59 + this.TeamworkTenantId.GetHashCode();
                if (this.AccessToken != null)
                    hashCode = hashCode * 59 + this.AccessToken.GetHashCode();
                if (this.ExpiresIn != null)
                    hashCode = hashCode * 59 + this.ExpiresIn.GetHashCode();
                if (this.RefreshToken != null)
                    hashCode = hashCode * 59 + this.RefreshToken.GetHashCode();
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