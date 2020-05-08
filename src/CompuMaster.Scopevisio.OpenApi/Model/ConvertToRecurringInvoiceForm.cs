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
    /// ConvertToRecurringInvoiceForm
    /// </summary>
    [DataContract]
    public partial class ConvertToRecurringInvoiceForm :  IEquatable<ConvertToRecurringInvoiceForm>, IValidatableObject
    {
        /// <summary>
        /// frequency
        /// </summary>
        /// <value>frequency</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FrequencyEnum
        {
            /// <summary>
            /// Enum Daily for value: daily
            /// </summary>
            [EnumMember(Value = "daily")]
            Daily = 1,

            /// <summary>
            /// Enum Weekly for value: weekly
            /// </summary>
            [EnumMember(Value = "weekly")]
            Weekly = 2,

            /// <summary>
            /// Enum Monthly for value: monthly
            /// </summary>
            [EnumMember(Value = "monthly")]
            Monthly = 3,

            /// <summary>
            /// Enum Bimonthly for value: bimonthly
            /// </summary>
            [EnumMember(Value = "bimonthly")]
            Bimonthly = 4,

            /// <summary>
            /// Enum Quarterly for value: quarterly
            /// </summary>
            [EnumMember(Value = "quarterly")]
            Quarterly = 5,

            /// <summary>
            /// Enum HalfYearly for value: half-yearly
            /// </summary>
            [EnumMember(Value = "half-yearly")]
            HalfYearly = 6,

            /// <summary>
            /// Enum Yearly for value: yearly
            /// </summary>
            [EnumMember(Value = "yearly")]
            Yearly = 7

        }

        /// <summary>
        /// frequency
        /// </summary>
        /// <value>frequency</value>
        [DataMember(Name="frequency", EmitDefaultValue=true)]
        public FrequencyEnum Frequency { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertToRecurringInvoiceForm" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ConvertToRecurringInvoiceForm() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertToRecurringInvoiceForm" /> class.
        /// </summary>
        /// <param name="validFrom">validFrom (required).</param>
        /// <param name="validTo">validTo (required).</param>
        /// <param name="frequency">frequency (required).</param>
        /// <param name="nextInvoiceDate">nextInvoiceDate (required).</param>
        public ConvertToRecurringInvoiceForm(long validFrom = default(long), long validTo = default(long), FrequencyEnum frequency = default(FrequencyEnum), long nextInvoiceDate = default(long))
        {
            // to ensure "validFrom" is required (not null)
            if (validFrom == null)
            {
                throw new InvalidDataException("validFrom is a required property for ConvertToRecurringInvoiceForm and cannot be null");
            }
            else
            {
                this.ValidFrom = validFrom;
            }
            
            // to ensure "validTo" is required (not null)
            if (validTo == null)
            {
                throw new InvalidDataException("validTo is a required property for ConvertToRecurringInvoiceForm and cannot be null");
            }
            else
            {
                this.ValidTo = validTo;
            }
            
            // to ensure "frequency" is required (not null)
            if (frequency == null)
            {
                throw new InvalidDataException("frequency is a required property for ConvertToRecurringInvoiceForm and cannot be null");
            }
            else
            {
                this.Frequency = frequency;
            }
            
            // to ensure "nextInvoiceDate" is required (not null)
            if (nextInvoiceDate == null)
            {
                throw new InvalidDataException("nextInvoiceDate is a required property for ConvertToRecurringInvoiceForm and cannot be null");
            }
            else
            {
                this.NextInvoiceDate = nextInvoiceDate;
            }
            
        }
        
        /// <summary>
        /// validFrom
        /// </summary>
        /// <value>validFrom</value>
        [DataMember(Name="validFrom", EmitDefaultValue=true)]
        public long ValidFrom { get; set; }

        /// <summary>
        /// validTo
        /// </summary>
        /// <value>validTo</value>
        [DataMember(Name="validTo", EmitDefaultValue=true)]
        public long ValidTo { get; set; }


        /// <summary>
        /// nextInvoiceDate
        /// </summary>
        /// <value>nextInvoiceDate</value>
        [DataMember(Name="nextInvoiceDate", EmitDefaultValue=true)]
        public long NextInvoiceDate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ConvertToRecurringInvoiceForm {\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
            sb.Append("  Frequency: ").Append(Frequency).Append("\n");
            sb.Append("  NextInvoiceDate: ").Append(NextInvoiceDate).Append("\n");
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
            return this.Equals(input as ConvertToRecurringInvoiceForm);
        }

        /// <summary>
        /// Returns true if ConvertToRecurringInvoiceForm instances are equal
        /// </summary>
        /// <param name="input">Instance of ConvertToRecurringInvoiceForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConvertToRecurringInvoiceForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ValidFrom == input.ValidFrom ||
                    (this.ValidFrom != null &&
                    this.ValidFrom.Equals(input.ValidFrom))
                ) && 
                (
                    this.ValidTo == input.ValidTo ||
                    (this.ValidTo != null &&
                    this.ValidTo.Equals(input.ValidTo))
                ) && 
                (
                    this.Frequency == input.Frequency ||
                    (this.Frequency != null &&
                    this.Frequency.Equals(input.Frequency))
                ) && 
                (
                    this.NextInvoiceDate == input.NextInvoiceDate ||
                    (this.NextInvoiceDate != null &&
                    this.NextInvoiceDate.Equals(input.NextInvoiceDate))
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
                if (this.ValidFrom != null)
                    hashCode = hashCode * 59 + this.ValidFrom.GetHashCode();
                if (this.ValidTo != null)
                    hashCode = hashCode * 59 + this.ValidTo.GetHashCode();
                if (this.Frequency != null)
                    hashCode = hashCode * 59 + this.Frequency.GetHashCode();
                if (this.NextInvoiceDate != null)
                    hashCode = hashCode * 59 + this.NextInvoiceDate.GetHashCode();
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
