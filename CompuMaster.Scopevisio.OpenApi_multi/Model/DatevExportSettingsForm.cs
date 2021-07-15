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
    /// DatevExportSettingsForm
    /// </summary>
    [DataContract]
    public partial class DatevExportSettingsForm :  IEquatable<DatevExportSettingsForm>, IValidatableObject
    {
        /// <summary>
        /// textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used.
        /// </summary>
        /// <value>textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TextConfigurationEnum
        {
            /// <summary>
            /// Enum Belegtext for value: Belegtext
            /// </summary>
            [EnumMember(Value = "Belegtext")]
            Belegtext = 1,

            /// <summary>
            /// Enum ErsteBuchungszeile for value: Erste Buchungszeile
            /// </summary>
            [EnumMember(Value = "Erste Buchungszeile")]
            ErsteBuchungszeile = 2

        }

        /// <summary>
        /// textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used.
        /// </summary>
        /// <value>textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used.</value>
        [DataMember(Name="textConfiguration", EmitDefaultValue=false)]
        public TextConfigurationEnum? TextConfiguration { get; set; }
        /// <summary>
        /// field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used.
        /// </summary>
        /// <value>field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Field1ConfigurationEnum
        {
            /// <summary>
            /// Enum Belegnummer for value: Belegnummer
            /// </summary>
            [EnumMember(Value = "Belegnummer")]
            Belegnummer = 1,

            /// <summary>
            /// Enum ExterneBelegnummer for value: Externe Belegnummer
            /// </summary>
            [EnumMember(Value = "Externe Belegnummer")]
            ExterneBelegnummer = 2

        }

        /// <summary>
        /// field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used.
        /// </summary>
        /// <value>field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used.</value>
        [DataMember(Name="field1Configuration", EmitDefaultValue=false)]
        public Field1ConfigurationEnum? Field1Configuration { get; set; }
        /// <summary>
        /// exportStrategy. The way split entries are exported. If not set standard is used.
        /// </summary>
        /// <value>exportStrategy. The way split entries are exported. If not set standard is used.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ExportStrategyEnum
        {
            /// <summary>
            /// Enum Standard for value: Standard
            /// </summary>
            [EnumMember(Value = "Standard")]
            Standard = 1,

            /// <summary>
            /// Enum Journalreihenfolgebeachten for value: Journalreihenfolge beachten
            /// </summary>
            [EnumMember(Value = "Journalreihenfolge beachten")]
            Journalreihenfolgebeachten = 2

        }

        /// <summary>
        /// exportStrategy. The way split entries are exported. If not set standard is used.
        /// </summary>
        /// <value>exportStrategy. The way split entries are exported. If not set standard is used.</value>
        [DataMember(Name="exportStrategy", EmitDefaultValue=false)]
        public ExportStrategyEnum? ExportStrategy { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatevExportSettingsForm" /> class.
        /// </summary>
        /// <param name="fromPostingDateTs">fromPostingDateTs. Timestamp in milliseconds to set the posting date from. If not set the first day of the current year is set..</param>
        /// <param name="toPostingDateTs">toPostingDateTs. Timestamp in milliseconds to set the posting date to. If not set the last day of the current year is set..</param>
        /// <param name="fromCreationDateTs">fromCreationDateTs. Timestamp in milliseconds to set the creation date from. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed..</param>
        /// <param name="toCreationDateTs">toCreationDateTs. Timestamp in milliseconds to set the creation date to. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed..</param>
        /// <param name="dimension1">dimension1. The name of the dimension which should be exported to Kost1 in DATEV..</param>
        /// <param name="dimension2">dimension2. The name of the dimension which should be exported to Kost2 in DATEV..</param>
        /// <param name="textConfiguration">textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used..</param>
        /// <param name="field1Configuration">field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used..</param>
        /// <param name="exportStrategy">exportStrategy. The way split entries are exported. If not set standard is used..</param>
        /// <param name="skipCancellations">skipCancellations. Do you want to export cancellation postings? (default to false).</param>
        /// <param name="vatMatrixDatevKeys">vatMatrixDatevKeys. Do you want to use the DATEV posting keys (BU-Schlüssel) from Scopevisios vat matrix? Standard value is true. (default to false).</param>
        public DatevExportSettingsForm(long fromPostingDateTs = default(long), long toPostingDateTs = default(long), long fromCreationDateTs = default(long), long toCreationDateTs = default(long), string dimension1 = default(string), string dimension2 = default(string), TextConfigurationEnum? textConfiguration = default(TextConfigurationEnum?), Field1ConfigurationEnum? field1Configuration = default(Field1ConfigurationEnum?), ExportStrategyEnum? exportStrategy = default(ExportStrategyEnum?), bool skipCancellations = false, bool vatMatrixDatevKeys = false)
        {
            this.FromPostingDateTs = fromPostingDateTs;
            this.ToPostingDateTs = toPostingDateTs;
            this.FromCreationDateTs = fromCreationDateTs;
            this.ToCreationDateTs = toCreationDateTs;
            this.Dimension1 = dimension1;
            this.Dimension2 = dimension2;
            this.TextConfiguration = textConfiguration;
            this.Field1Configuration = field1Configuration;
            this.ExportStrategy = exportStrategy;
            // use default value if no "skipCancellations" provided
            if (skipCancellations == null)
            {
                this.SkipCancellations = false;
            }
            else
            {
                this.SkipCancellations = skipCancellations;
            }
            // use default value if no "vatMatrixDatevKeys" provided
            if (vatMatrixDatevKeys == null)
            {
                this.VatMatrixDatevKeys = false;
            }
            else
            {
                this.VatMatrixDatevKeys = vatMatrixDatevKeys;
            }
        }
        
        /// <summary>
        /// fromPostingDateTs. Timestamp in milliseconds to set the posting date from. If not set the first day of the current year is set.
        /// </summary>
        /// <value>fromPostingDateTs. Timestamp in milliseconds to set the posting date from. If not set the first day of the current year is set.</value>
        [DataMember(Name="fromPostingDateTs", EmitDefaultValue=false)]
        public long FromPostingDateTs { get; set; }

        /// <summary>
        /// toPostingDateTs. Timestamp in milliseconds to set the posting date to. If not set the last day of the current year is set.
        /// </summary>
        /// <value>toPostingDateTs. Timestamp in milliseconds to set the posting date to. If not set the last day of the current year is set.</value>
        [DataMember(Name="toPostingDateTs", EmitDefaultValue=false)]
        public long ToPostingDateTs { get; set; }

        /// <summary>
        /// fromCreationDateTs. Timestamp in milliseconds to set the creation date from. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed.
        /// </summary>
        /// <value>fromCreationDateTs. Timestamp in milliseconds to set the creation date from. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed.</value>
        [DataMember(Name="fromCreationDateTs", EmitDefaultValue=false)]
        public long FromCreationDateTs { get; set; }

        /// <summary>
        /// toCreationDateTs. Timestamp in milliseconds to set the creation date to. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed.
        /// </summary>
        /// <value>toCreationDateTs. Timestamp in milliseconds to set the creation date to. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed.</value>
        [DataMember(Name="toCreationDateTs", EmitDefaultValue=false)]
        public long ToCreationDateTs { get; set; }

        /// <summary>
        /// dimension1. The name of the dimension which should be exported to Kost1 in DATEV.
        /// </summary>
        /// <value>dimension1. The name of the dimension which should be exported to Kost1 in DATEV.</value>
        [DataMember(Name="dimension1", EmitDefaultValue=false)]
        public string Dimension1 { get; set; }

        /// <summary>
        /// dimension2. The name of the dimension which should be exported to Kost2 in DATEV.
        /// </summary>
        /// <value>dimension2. The name of the dimension which should be exported to Kost2 in DATEV.</value>
        [DataMember(Name="dimension2", EmitDefaultValue=false)]
        public string Dimension2 { get; set; }




        /// <summary>
        /// skipCancellations. Do you want to export cancellation postings?
        /// </summary>
        /// <value>skipCancellations. Do you want to export cancellation postings?</value>
        [DataMember(Name="skipCancellations", EmitDefaultValue=false)]
        public bool SkipCancellations { get; set; }

        /// <summary>
        /// vatMatrixDatevKeys. Do you want to use the DATEV posting keys (BU-Schlüssel) from Scopevisios vat matrix? Standard value is true.
        /// </summary>
        /// <value>vatMatrixDatevKeys. Do you want to use the DATEV posting keys (BU-Schlüssel) from Scopevisios vat matrix? Standard value is true.</value>
        [DataMember(Name="vatMatrixDatevKeys", EmitDefaultValue=false)]
        public bool VatMatrixDatevKeys { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DatevExportSettingsForm {\n");
            sb.Append("  FromPostingDateTs: ").Append(FromPostingDateTs).Append("\n");
            sb.Append("  ToPostingDateTs: ").Append(ToPostingDateTs).Append("\n");
            sb.Append("  FromCreationDateTs: ").Append(FromCreationDateTs).Append("\n");
            sb.Append("  ToCreationDateTs: ").Append(ToCreationDateTs).Append("\n");
            sb.Append("  Dimension1: ").Append(Dimension1).Append("\n");
            sb.Append("  Dimension2: ").Append(Dimension2).Append("\n");
            sb.Append("  TextConfiguration: ").Append(TextConfiguration).Append("\n");
            sb.Append("  Field1Configuration: ").Append(Field1Configuration).Append("\n");
            sb.Append("  ExportStrategy: ").Append(ExportStrategy).Append("\n");
            sb.Append("  SkipCancellations: ").Append(SkipCancellations).Append("\n");
            sb.Append("  VatMatrixDatevKeys: ").Append(VatMatrixDatevKeys).Append("\n");
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
            return this.Equals(input as DatevExportSettingsForm);
        }

        /// <summary>
        /// Returns true if DatevExportSettingsForm instances are equal
        /// </summary>
        /// <param name="input">Instance of DatevExportSettingsForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DatevExportSettingsForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.FromPostingDateTs == input.FromPostingDateTs ||
                    (this.FromPostingDateTs != null &&
                    this.FromPostingDateTs.Equals(input.FromPostingDateTs))
                ) && 
                (
                    this.ToPostingDateTs == input.ToPostingDateTs ||
                    (this.ToPostingDateTs != null &&
                    this.ToPostingDateTs.Equals(input.ToPostingDateTs))
                ) && 
                (
                    this.FromCreationDateTs == input.FromCreationDateTs ||
                    (this.FromCreationDateTs != null &&
                    this.FromCreationDateTs.Equals(input.FromCreationDateTs))
                ) && 
                (
                    this.ToCreationDateTs == input.ToCreationDateTs ||
                    (this.ToCreationDateTs != null &&
                    this.ToCreationDateTs.Equals(input.ToCreationDateTs))
                ) && 
                (
                    this.Dimension1 == input.Dimension1 ||
                    (this.Dimension1 != null &&
                    this.Dimension1.Equals(input.Dimension1))
                ) && 
                (
                    this.Dimension2 == input.Dimension2 ||
                    (this.Dimension2 != null &&
                    this.Dimension2.Equals(input.Dimension2))
                ) && 
                (
                    this.TextConfiguration == input.TextConfiguration ||
                    (this.TextConfiguration != null &&
                    this.TextConfiguration.Equals(input.TextConfiguration))
                ) && 
                (
                    this.Field1Configuration == input.Field1Configuration ||
                    (this.Field1Configuration != null &&
                    this.Field1Configuration.Equals(input.Field1Configuration))
                ) && 
                (
                    this.ExportStrategy == input.ExportStrategy ||
                    (this.ExportStrategy != null &&
                    this.ExportStrategy.Equals(input.ExportStrategy))
                ) && 
                (
                    this.SkipCancellations == input.SkipCancellations ||
                    (this.SkipCancellations != null &&
                    this.SkipCancellations.Equals(input.SkipCancellations))
                ) && 
                (
                    this.VatMatrixDatevKeys == input.VatMatrixDatevKeys ||
                    (this.VatMatrixDatevKeys != null &&
                    this.VatMatrixDatevKeys.Equals(input.VatMatrixDatevKeys))
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
                if (this.FromPostingDateTs != null)
                    hashCode = hashCode * 59 + this.FromPostingDateTs.GetHashCode();
                if (this.ToPostingDateTs != null)
                    hashCode = hashCode * 59 + this.ToPostingDateTs.GetHashCode();
                if (this.FromCreationDateTs != null)
                    hashCode = hashCode * 59 + this.FromCreationDateTs.GetHashCode();
                if (this.ToCreationDateTs != null)
                    hashCode = hashCode * 59 + this.ToCreationDateTs.GetHashCode();
                if (this.Dimension1 != null)
                    hashCode = hashCode * 59 + this.Dimension1.GetHashCode();
                if (this.Dimension2 != null)
                    hashCode = hashCode * 59 + this.Dimension2.GetHashCode();
                if (this.TextConfiguration != null)
                    hashCode = hashCode * 59 + this.TextConfiguration.GetHashCode();
                if (this.Field1Configuration != null)
                    hashCode = hashCode * 59 + this.Field1Configuration.GetHashCode();
                if (this.ExportStrategy != null)
                    hashCode = hashCode * 59 + this.ExportStrategy.GetHashCode();
                if (this.SkipCancellations != null)
                    hashCode = hashCode * 59 + this.SkipCancellations.GetHashCode();
                if (this.VatMatrixDatevKeys != null)
                    hashCode = hashCode * 59 + this.VatMatrixDatevKeys.GetHashCode();
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
