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
    /// Version
    /// </summary>
    [DataContract]
    public partial class Version :  IEquatable<Version>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Version" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Version() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version" /> class.
        /// </summary>
        /// <param name="buildDate">Build date</param>
        /// <param name="buildNumber">Build number</param>
        /// <param name="commitHash">Commit hash</param>
        /// <param name="commitDate">Commit date</param>
        public Version(string buildDate = default(string), long buildNumber = default(long), string commitHash = default(string), string commitDate = default(string))
        {
            this.BuildDateString = buildDate;
            this.BuildNumber = buildNumber;
            this.CommitHash = commitHash;
            this.CommitDateString = commitDate;
        }


        /// <summary>
        /// Build date
        /// </summary>
        /// <value>Build date</value>
        [DataMember(Name= "buildDate", EmitDefaultValue=false)]
        private string BuildDateString { get; set; }

        /// <summary>
        /// Build date
        /// </summary>
        /// <value>Build date</value>
        public DateTime BuildDate {
            get 
            {
                return DateTime.ParseExact(this.BuildDateString, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
            }            
            set
            {
                this.BuildDateString = value.ToString("yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Build number
        /// </summary>
        /// <value>Build number</value>
        [DataMember(Name= "buildNumber", EmitDefaultValue=false)]
        public long BuildNumber { get; set; }

        /// <summary>
        /// Commit hash
        /// </summary>
        /// <value>Commit hash</value>
        [DataMember(Name= "commitHash", EmitDefaultValue=false)]
        public string CommitHash { get; set; }

        /// <summary>
        /// Commit date
        /// </summary>
        /// <value>Commit date</value>
        [DataMember(Name= "commitDate", EmitDefaultValue=false)]
        private string CommitDateString { get; set; }

        /// <summary>
        /// Commit date
        /// </summary>
        public DateTime CommitDate
        {
            get
            {
                return Client.ScopevisioDateTimeConverter.ToDate(this.CommitDateString);
            }
            set
            {
                this.CommitDateString = Client.ScopevisioDateTimeConverter.ToString(value);
            }
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Version {\n");
            sb.Append("  BuildDate:   ").Append(BuildDate).Append("\n");
            sb.Append("  BuildNumber: ").Append(BuildNumber).Append("\n");
            sb.Append("  CommitHash:  ").Append(CommitHash).Append("\n");
            sb.Append("  CommitDate:  ").Append(CommitDate).Append("\n");
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
            return this.Equals(input as Version);
        }

        /// <summary>
        /// Returns true if Version instances are equal
        /// </summary>
        /// <param name="input">Instance of Version to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Version input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.BuildDate == input.BuildDate ||
                    (this.BuildDate != DateTime.MinValue &&
                    this.BuildDate.Equals(input.BuildDate))
                ) && 
                (
                    this.BuildNumber == input.BuildNumber ||
                    this.BuildNumber.Equals(input.BuildNumber)
                ) && 
                (
                    this.CommitHash == input.CommitHash ||
                    (this.CommitHash != null &&
                    this.CommitHash.Equals(input.CommitHash))
                ) && 
                (
                    this.CommitDate == input.CommitDate ||
                    (this.CommitDate != DateTime.MinValue &&
                    this.CommitDate.Equals(input.CommitDate))
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
                if (this.BuildDate != DateTime.MinValue)
                    hashCode = hashCode * 59 + this.BuildDate.GetHashCode();
                hashCode = hashCode * 59 + this.BuildNumber.GetHashCode();
                if (this.CommitHash != null)
                    hashCode = hashCode * 59 + this.CommitHash.GetHashCode();
                if (this.CommitDate != DateTime.MinValue)
                    hashCode = hashCode * 59 + this.CommitDate.GetHashCode();
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
