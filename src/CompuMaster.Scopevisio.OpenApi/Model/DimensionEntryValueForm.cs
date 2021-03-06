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
    /// DimensionEntryValueForm
    /// </summary>
    [DataContract]
    public partial class DimensionEntryValueForm :  IEquatable<DimensionEntryValueForm>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionEntryValueForm" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DimensionEntryValueForm() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionEntryValueForm" /> class.
        /// </summary>
        /// <param name="dimensionNumber">dimensionNumber (e.g. 1 (Kostenstelle), 2 (Kostenträger), etc. (required).</param>
        /// <param name="dimensionEntryNumber">DimensionEntryNumber (required).</param>
        /// <param name="dimensionEntryName">DimensionEntryName, if the dimensionEntryNumber does not exist, it will be created. If dimensionEntryName is not provided then it is set to the dimensionNumber..</param>
        public DimensionEntryValueForm(long dimensionNumber = default(long), long dimensionEntryNumber = default(long), string dimensionEntryName = default(string))
        {
            // to ensure "dimensionNumber" is required (not null)
            if (dimensionNumber == null)
            {
                throw new InvalidDataException("dimensionNumber is a required property for DimensionEntryValueForm and cannot be null");
            }
            else
            {
                this.DimensionNumber = dimensionNumber;
            }
            
            // to ensure "dimensionEntryNumber" is required (not null)
            if (dimensionEntryNumber == null)
            {
                throw new InvalidDataException("dimensionEntryNumber is a required property for DimensionEntryValueForm and cannot be null");
            }
            else
            {
                this.DimensionEntryNumber = dimensionEntryNumber;
            }
            
            this.DimensionEntryName = dimensionEntryName;
        }
        
        /// <summary>
        /// dimensionNumber (e.g. 1 (Kostenstelle), 2 (Kostenträger), etc.
        /// </summary>
        /// <value>dimensionNumber (e.g. 1 (Kostenstelle), 2 (Kostenträger), etc.</value>
        [DataMember(Name="dimensionNumber", EmitDefaultValue=true)]
        public long DimensionNumber { get; set; }

        /// <summary>
        /// DimensionEntryNumber
        /// </summary>
        /// <value>DimensionEntryNumber</value>
        [DataMember(Name="dimensionEntryNumber", EmitDefaultValue=true)]
        public long DimensionEntryNumber { get; set; }

        /// <summary>
        /// DimensionEntryName, if the dimensionEntryNumber does not exist, it will be created. If dimensionEntryName is not provided then it is set to the dimensionNumber.
        /// </summary>
        /// <value>DimensionEntryName, if the dimensionEntryNumber does not exist, it will be created. If dimensionEntryName is not provided then it is set to the dimensionNumber.</value>
        [DataMember(Name="dimensionEntryName", EmitDefaultValue=false)]
        public string DimensionEntryName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DimensionEntryValueForm {\n");
            sb.Append("  DimensionNumber: ").Append(DimensionNumber).Append("\n");
            sb.Append("  DimensionEntryNumber: ").Append(DimensionEntryNumber).Append("\n");
            sb.Append("  DimensionEntryName: ").Append(DimensionEntryName).Append("\n");
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
            return this.Equals(input as DimensionEntryValueForm);
        }

        /// <summary>
        /// Returns true if DimensionEntryValueForm instances are equal
        /// </summary>
        /// <param name="input">Instance of DimensionEntryValueForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DimensionEntryValueForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DimensionNumber == input.DimensionNumber ||
                    (this.DimensionNumber != null &&
                    this.DimensionNumber.Equals(input.DimensionNumber))
                ) && 
                (
                    this.DimensionEntryNumber == input.DimensionEntryNumber ||
                    (this.DimensionEntryNumber != null &&
                    this.DimensionEntryNumber.Equals(input.DimensionEntryNumber))
                ) && 
                (
                    this.DimensionEntryName == input.DimensionEntryName ||
                    (this.DimensionEntryName != null &&
                    this.DimensionEntryName.Equals(input.DimensionEntryName))
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
                if (this.DimensionNumber != null)
                    hashCode = hashCode * 59 + this.DimensionNumber.GetHashCode();
                if (this.DimensionEntryNumber != null)
                    hashCode = hashCode * 59 + this.DimensionEntryNumber.GetHashCode();
                if (this.DimensionEntryName != null)
                    hashCode = hashCode * 59 + this.DimensionEntryName.GetHashCode();
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
