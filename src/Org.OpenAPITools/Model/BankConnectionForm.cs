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
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// BankConnectionForm
    /// </summary>
    [DataContract]
    public partial class BankConnectionForm :  IEquatable<BankConnectionForm>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankConnectionForm" /> class.
        /// </summary>
        /// <param name="number">number (Kontonummer).</param>
        /// <param name="bankCode">bankCode (BLZ).</param>
        /// <param name="iban">iban.</param>
        /// <param name="swiftBic">swiftBic.</param>
        /// <param name="bankName">bankName (Kreditinstitut).</param>
        /// <param name="accountHolder">accountHolder (Kontoinhaber).</param>
        /// <param name="standard">standard, whether or not this should be the standard bank connection for transfers or withdrawals. (default to false).</param>
        public BankConnectionForm(string number = default(string), string bankCode = default(string), string iban = default(string), string swiftBic = default(string), string bankName = default(string), string accountHolder = default(string), bool standard = false)
        {
            this.Number = number;
            this.BankCode = bankCode;
            this.Iban = iban;
            this.SwiftBic = swiftBic;
            this.BankName = bankName;
            this.AccountHolder = accountHolder;
            // use default value if no "standard" provided
            if (standard == null)
            {
                this.Standard = false;
            }
            else
            {
                this.Standard = standard;
            }
        }
        
        /// <summary>
        /// number (Kontonummer)
        /// </summary>
        /// <value>number (Kontonummer)</value>
        [DataMember(Name="number", EmitDefaultValue=false)]
        public string Number { get; set; }

        /// <summary>
        /// bankCode (BLZ)
        /// </summary>
        /// <value>bankCode (BLZ)</value>
        [DataMember(Name="bankCode", EmitDefaultValue=false)]
        public string BankCode { get; set; }

        /// <summary>
        /// iban
        /// </summary>
        /// <value>iban</value>
        [DataMember(Name="iban", EmitDefaultValue=false)]
        public string Iban { get; set; }

        /// <summary>
        /// swiftBic
        /// </summary>
        /// <value>swiftBic</value>
        [DataMember(Name="swiftBic", EmitDefaultValue=false)]
        public string SwiftBic { get; set; }

        /// <summary>
        /// bankName (Kreditinstitut)
        /// </summary>
        /// <value>bankName (Kreditinstitut)</value>
        [DataMember(Name="bankName", EmitDefaultValue=false)]
        public string BankName { get; set; }

        /// <summary>
        /// accountHolder (Kontoinhaber)
        /// </summary>
        /// <value>accountHolder (Kontoinhaber)</value>
        [DataMember(Name="accountHolder", EmitDefaultValue=false)]
        public string AccountHolder { get; set; }

        /// <summary>
        /// standard, whether or not this should be the standard bank connection for transfers or withdrawals.
        /// </summary>
        /// <value>standard, whether or not this should be the standard bank connection for transfers or withdrawals.</value>
        [DataMember(Name="standard", EmitDefaultValue=false)]
        public bool Standard { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BankConnectionForm {\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  BankCode: ").Append(BankCode).Append("\n");
            sb.Append("  Iban: ").Append(Iban).Append("\n");
            sb.Append("  SwiftBic: ").Append(SwiftBic).Append("\n");
            sb.Append("  BankName: ").Append(BankName).Append("\n");
            sb.Append("  AccountHolder: ").Append(AccountHolder).Append("\n");
            sb.Append("  Standard: ").Append(Standard).Append("\n");
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
            return this.Equals(input as BankConnectionForm);
        }

        /// <summary>
        /// Returns true if BankConnectionForm instances are equal
        /// </summary>
        /// <param name="input">Instance of BankConnectionForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BankConnectionForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Number == input.Number ||
                    (this.Number != null &&
                    this.Number.Equals(input.Number))
                ) && 
                (
                    this.BankCode == input.BankCode ||
                    (this.BankCode != null &&
                    this.BankCode.Equals(input.BankCode))
                ) && 
                (
                    this.Iban == input.Iban ||
                    (this.Iban != null &&
                    this.Iban.Equals(input.Iban))
                ) && 
                (
                    this.SwiftBic == input.SwiftBic ||
                    (this.SwiftBic != null &&
                    this.SwiftBic.Equals(input.SwiftBic))
                ) && 
                (
                    this.BankName == input.BankName ||
                    (this.BankName != null &&
                    this.BankName.Equals(input.BankName))
                ) && 
                (
                    this.AccountHolder == input.AccountHolder ||
                    (this.AccountHolder != null &&
                    this.AccountHolder.Equals(input.AccountHolder))
                ) && 
                (
                    this.Standard == input.Standard ||
                    (this.Standard != null &&
                    this.Standard.Equals(input.Standard))
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
                if (this.Number != null)
                    hashCode = hashCode * 59 + this.Number.GetHashCode();
                if (this.BankCode != null)
                    hashCode = hashCode * 59 + this.BankCode.GetHashCode();
                if (this.Iban != null)
                    hashCode = hashCode * 59 + this.Iban.GetHashCode();
                if (this.SwiftBic != null)
                    hashCode = hashCode * 59 + this.SwiftBic.GetHashCode();
                if (this.BankName != null)
                    hashCode = hashCode * 59 + this.BankName.GetHashCode();
                if (this.AccountHolder != null)
                    hashCode = hashCode * 59 + this.AccountHolder.GetHashCode();
                if (this.Standard != null)
                    hashCode = hashCode * 59 + this.Standard.GetHashCode();
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
