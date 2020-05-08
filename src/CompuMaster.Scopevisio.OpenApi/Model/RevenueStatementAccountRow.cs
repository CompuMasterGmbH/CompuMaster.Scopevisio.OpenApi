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
    /// Revenue statement accounts
    /// </summary>
    [DataContract]
    public partial class RevenueStatementAccountRow :  IEquatable<RevenueStatementAccountRow>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevenueStatementAccountRow" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RevenueStatementAccountRow() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RevenueStatementAccountRow" /> class.
        /// </summary>
        /// <param name="allocationAccountNumber">allocationAccountNumber (Verrechnunskonto Nummer) (required).</param>
        /// <param name="settlementAccountNumber">settlementAccountNumber (Erlöskonto Nummer) (required).</param>
        public RevenueStatementAccountRow(string allocationAccountNumber = default(string), string settlementAccountNumber = default(string))
        {
            // to ensure "allocationAccountNumber" is required (not null)
            if (allocationAccountNumber == null)
            {
                throw new InvalidDataException("allocationAccountNumber is a required property for RevenueStatementAccountRow and cannot be null");
            }
            else
            {
                this.AllocationAccountNumber = allocationAccountNumber;
            }
            
            // to ensure "settlementAccountNumber" is required (not null)
            if (settlementAccountNumber == null)
            {
                throw new InvalidDataException("settlementAccountNumber is a required property for RevenueStatementAccountRow and cannot be null");
            }
            else
            {
                this.SettlementAccountNumber = settlementAccountNumber;
            }
            
        }
        
        /// <summary>
        /// allocationAccountNumber (Verrechnunskonto Nummer)
        /// </summary>
        /// <value>allocationAccountNumber (Verrechnunskonto Nummer)</value>
        [DataMember(Name="allocationAccountNumber", EmitDefaultValue=true)]
        public string AllocationAccountNumber { get; set; }

        /// <summary>
        /// settlementAccountNumber (Erlöskonto Nummer)
        /// </summary>
        /// <value>settlementAccountNumber (Erlöskonto Nummer)</value>
        [DataMember(Name="settlementAccountNumber", EmitDefaultValue=true)]
        public string SettlementAccountNumber { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RevenueStatementAccountRow {\n");
            sb.Append("  AllocationAccountNumber: ").Append(AllocationAccountNumber).Append("\n");
            sb.Append("  SettlementAccountNumber: ").Append(SettlementAccountNumber).Append("\n");
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
            return this.Equals(input as RevenueStatementAccountRow);
        }

        /// <summary>
        /// Returns true if RevenueStatementAccountRow instances are equal
        /// </summary>
        /// <param name="input">Instance of RevenueStatementAccountRow to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RevenueStatementAccountRow input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AllocationAccountNumber == input.AllocationAccountNumber ||
                    (this.AllocationAccountNumber != null &&
                    this.AllocationAccountNumber.Equals(input.AllocationAccountNumber))
                ) && 
                (
                    this.SettlementAccountNumber == input.SettlementAccountNumber ||
                    (this.SettlementAccountNumber != null &&
                    this.SettlementAccountNumber.Equals(input.SettlementAccountNumber))
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
                if (this.AllocationAccountNumber != null)
                    hashCode = hashCode * 59 + this.AllocationAccountNumber.GetHashCode();
                if (this.SettlementAccountNumber != null)
                    hashCode = hashCode * 59 + this.SettlementAccountNumber.GetHashCode();
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
