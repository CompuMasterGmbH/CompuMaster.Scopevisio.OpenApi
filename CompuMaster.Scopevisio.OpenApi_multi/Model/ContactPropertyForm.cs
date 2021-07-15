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
    /// ContactPropertyForm
    /// </summary>
    [DataContract]
    public partial class ContactPropertyForm :  IEquatable<ContactPropertyForm>, IValidatableObject
    {
        /// <summary>
        /// spheres. Not set means visible in all spheres. 
        /// </summary>
        /// <value>spheres. Not set means visible in all spheres. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SpheresDisplayEnum
        {
            /// <summary>
            /// Enum Kontakt for value: Kontakt
            /// </summary>
            [EnumMember(Value = "Kontakt")]
            Kontakt = 1,

            /// <summary>
            /// Enum Lead for value: Lead
            /// </summary>
            [EnumMember(Value = "Lead")]
            Lead = 2,

            /// <summary>
            /// Enum Interessent for value: Interessent
            /// </summary>
            [EnumMember(Value = "Interessent")]
            Interessent = 3,

            /// <summary>
            /// Enum Kunde for value: Kunde
            /// </summary>
            [EnumMember(Value = "Kunde")]
            Kunde = 4,

            /// <summary>
            /// Enum Lieferant for value: Lieferant
            /// </summary>
            [EnumMember(Value = "Lieferant")]
            Lieferant = 5,

            /// <summary>
            /// Enum Debitor for value: Debitor
            /// </summary>
            [EnumMember(Value = "Debitor")]
            Debitor = 6,

            /// <summary>
            /// Enum Kreditor for value: Kreditor
            /// </summary>
            [EnumMember(Value = "Kreditor")]
            Kreditor = 7,

            /// <summary>
            /// Enum Mitarbeiter for value: Mitarbeiter
            /// </summary>
            [EnumMember(Value = "Mitarbeiter")]
            Mitarbeiter = 8,

            /// <summary>
            /// Enum Partner for value: Partner
            /// </summary>
            [EnumMember(Value = "Partner")]
            Partner = 9,

            /// <summary>
            /// Enum Arbeitnehmer for value: Arbeitnehmer
            /// </summary>
            [EnumMember(Value = "Arbeitnehmer")]
            Arbeitnehmer = 10

        }

        /// <summary>
        /// spheres. Not set means visible in all spheres. 
        /// </summary>
        /// <value>spheres. Not set means visible in all spheres. </value>
        [DataMember(Name="spheresDisplay", EmitDefaultValue=false)]
        public SpheresDisplayEnum? SpheresDisplay { get; set; }
        /// <summary>
        /// In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot;
        /// </summary>
        /// <value>In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot;</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EmployeeFilterDisplayEnum
        {
            /// <summary>
            /// Enum BeiallenAG for value: Bei allen AG
            /// </summary>
            [EnumMember(Value = "Bei allen AG")]
            BeiallenAG = 1,

            /// <summary>
            /// Enum BeikeinemAG for value: Bei keinem AG
            /// </summary>
            [EnumMember(Value = "Bei keinem AG")]
            BeikeinemAG = 2,

            /// <summary>
            /// Enum Commaseparatedlistoftheemployercontactnames for value: comma separated list of the employer contact names
            /// </summary>
            [EnumMember(Value = "comma separated list of the employer contact names")]
            Commaseparatedlistoftheemployercontactnames = 3

        }

        /// <summary>
        /// In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot;
        /// </summary>
        /// <value>In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot;</value>
        [DataMember(Name="employeeFilterDisplay", EmitDefaultValue=false)]
        public EmployeeFilterDisplayEnum? EmployeeFilterDisplay { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPropertyForm" /> class.
        /// </summary>
        /// <param name="type">Sets the values in column \&quot;Art der Information\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Art der Information.</param>
        /// <param name="subtype">Sets the values in column \&quot;Untergruppierung\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Untergruppierung.</param>
        /// <param name="description">Description.</param>
        /// <param name="validFrom">validFrom.</param>
        /// <param name="validTo">validTo.</param>
        /// <param name="employeeDomain">employeeDomain (default to false).</param>
        /// <param name="spheresDisplay">spheres. Not set means visible in all spheres. .</param>
        /// <param name="employeeFilterDisplay">In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot;.</param>
        /// <param name="permissionType">0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich.</param>
        /// <param name="readPermissionProfiles">comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1..</param>
        /// <param name="readPermissionLogins">comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1..</param>
        /// <param name="writePermissionProfiles">comma separated list of profiles for full access, evaluated only if permissionType equals 1..</param>
        /// <param name="writePermissionLogins">comma separated list of user login emails, evaluated only if permissionType equals 1..</param>
        public ContactPropertyForm(string type = default(string), string subtype = default(string), string description = default(string), long validFrom = default(long), long validTo = default(long), bool employeeDomain = false, SpheresDisplayEnum? spheresDisplay = default(SpheresDisplayEnum?), EmployeeFilterDisplayEnum? employeeFilterDisplay = default(EmployeeFilterDisplayEnum?), long permissionType = default(long), string readPermissionProfiles = default(string), string readPermissionLogins = default(string), string writePermissionProfiles = default(string), string writePermissionLogins = default(string))
        {
            this.Type = type;
            this.Subtype = subtype;
            this.Description = description;
            this.ValidFrom = validFrom;
            this.ValidTo = validTo;
            // use default value if no "employeeDomain" provided
            if (employeeDomain == null)
            {
                this.EmployeeDomain = false;
            }
            else
            {
                this.EmployeeDomain = employeeDomain;
            }
            this.SpheresDisplay = spheresDisplay;
            this.EmployeeFilterDisplay = employeeFilterDisplay;
            this.PermissionType = permissionType;
            this.ReadPermissionProfiles = readPermissionProfiles;
            this.ReadPermissionLogins = readPermissionLogins;
            this.WritePermissionProfiles = writePermissionProfiles;
            this.WritePermissionLogins = writePermissionLogins;
        }
        
        /// <summary>
        /// Sets the values in column \&quot;Art der Information\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Art der Information
        /// </summary>
        /// <value>Sets the values in column \&quot;Art der Information\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Art der Information</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public string Type { get; set; }

        /// <summary>
        /// Sets the values in column \&quot;Untergruppierung\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Untergruppierung
        /// </summary>
        /// <value>Sets the values in column \&quot;Untergruppierung\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Untergruppierung</value>
        [DataMember(Name="subtype", EmitDefaultValue=false)]
        public string Subtype { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <value>Description</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// validFrom
        /// </summary>
        /// <value>validFrom</value>
        [DataMember(Name="validFrom", EmitDefaultValue=false)]
        public long ValidFrom { get; set; }

        /// <summary>
        /// validTo
        /// </summary>
        /// <value>validTo</value>
        [DataMember(Name="validTo", EmitDefaultValue=false)]
        public long ValidTo { get; set; }

        /// <summary>
        /// employeeDomain
        /// </summary>
        /// <value>employeeDomain</value>
        [DataMember(Name="employeeDomain", EmitDefaultValue=false)]
        public bool EmployeeDomain { get; set; }



        /// <summary>
        /// 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich
        /// </summary>
        /// <value>0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich</value>
        [DataMember(Name="permissionType", EmitDefaultValue=false)]
        public long PermissionType { get; set; }

        /// <summary>
        /// comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1.
        /// </summary>
        /// <value>comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1.</value>
        [DataMember(Name="readPermissionProfiles", EmitDefaultValue=false)]
        public string ReadPermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1.
        /// </summary>
        /// <value>comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1.</value>
        [DataMember(Name="readPermissionLogins", EmitDefaultValue=false)]
        public string ReadPermissionLogins { get; set; }

        /// <summary>
        /// comma separated list of profiles for full access, evaluated only if permissionType equals 1.
        /// </summary>
        /// <value>comma separated list of profiles for full access, evaluated only if permissionType equals 1.</value>
        [DataMember(Name="writePermissionProfiles", EmitDefaultValue=false)]
        public string WritePermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails, evaluated only if permissionType equals 1.
        /// </summary>
        /// <value>comma separated list of user login emails, evaluated only if permissionType equals 1.</value>
        [DataMember(Name="writePermissionLogins", EmitDefaultValue=false)]
        public string WritePermissionLogins { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContactPropertyForm {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Subtype: ").Append(Subtype).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
            sb.Append("  EmployeeDomain: ").Append(EmployeeDomain).Append("\n");
            sb.Append("  SpheresDisplay: ").Append(SpheresDisplay).Append("\n");
            sb.Append("  EmployeeFilterDisplay: ").Append(EmployeeFilterDisplay).Append("\n");
            sb.Append("  PermissionType: ").Append(PermissionType).Append("\n");
            sb.Append("  ReadPermissionProfiles: ").Append(ReadPermissionProfiles).Append("\n");
            sb.Append("  ReadPermissionLogins: ").Append(ReadPermissionLogins).Append("\n");
            sb.Append("  WritePermissionProfiles: ").Append(WritePermissionProfiles).Append("\n");
            sb.Append("  WritePermissionLogins: ").Append(WritePermissionLogins).Append("\n");
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
            return this.Equals(input as ContactPropertyForm);
        }

        /// <summary>
        /// Returns true if ContactPropertyForm instances are equal
        /// </summary>
        /// <param name="input">Instance of ContactPropertyForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContactPropertyForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Subtype == input.Subtype ||
                    (this.Subtype != null &&
                    this.Subtype.Equals(input.Subtype))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
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
                    this.EmployeeDomain == input.EmployeeDomain ||
                    (this.EmployeeDomain != null &&
                    this.EmployeeDomain.Equals(input.EmployeeDomain))
                ) && 
                (
                    this.SpheresDisplay == input.SpheresDisplay ||
                    (this.SpheresDisplay != null &&
                    this.SpheresDisplay.Equals(input.SpheresDisplay))
                ) && 
                (
                    this.EmployeeFilterDisplay == input.EmployeeFilterDisplay ||
                    (this.EmployeeFilterDisplay != null &&
                    this.EmployeeFilterDisplay.Equals(input.EmployeeFilterDisplay))
                ) && 
                (
                    this.PermissionType == input.PermissionType ||
                    (this.PermissionType != null &&
                    this.PermissionType.Equals(input.PermissionType))
                ) && 
                (
                    this.ReadPermissionProfiles == input.ReadPermissionProfiles ||
                    (this.ReadPermissionProfiles != null &&
                    this.ReadPermissionProfiles.Equals(input.ReadPermissionProfiles))
                ) && 
                (
                    this.ReadPermissionLogins == input.ReadPermissionLogins ||
                    (this.ReadPermissionLogins != null &&
                    this.ReadPermissionLogins.Equals(input.ReadPermissionLogins))
                ) && 
                (
                    this.WritePermissionProfiles == input.WritePermissionProfiles ||
                    (this.WritePermissionProfiles != null &&
                    this.WritePermissionProfiles.Equals(input.WritePermissionProfiles))
                ) && 
                (
                    this.WritePermissionLogins == input.WritePermissionLogins ||
                    (this.WritePermissionLogins != null &&
                    this.WritePermissionLogins.Equals(input.WritePermissionLogins))
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
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Subtype != null)
                    hashCode = hashCode * 59 + this.Subtype.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.ValidFrom != null)
                    hashCode = hashCode * 59 + this.ValidFrom.GetHashCode();
                if (this.ValidTo != null)
                    hashCode = hashCode * 59 + this.ValidTo.GetHashCode();
                if (this.EmployeeDomain != null)
                    hashCode = hashCode * 59 + this.EmployeeDomain.GetHashCode();
                if (this.SpheresDisplay != null)
                    hashCode = hashCode * 59 + this.SpheresDisplay.GetHashCode();
                if (this.EmployeeFilterDisplay != null)
                    hashCode = hashCode * 59 + this.EmployeeFilterDisplay.GetHashCode();
                if (this.PermissionType != null)
                    hashCode = hashCode * 59 + this.PermissionType.GetHashCode();
                if (this.ReadPermissionProfiles != null)
                    hashCode = hashCode * 59 + this.ReadPermissionProfiles.GetHashCode();
                if (this.ReadPermissionLogins != null)
                    hashCode = hashCode * 59 + this.ReadPermissionLogins.GetHashCode();
                if (this.WritePermissionProfiles != null)
                    hashCode = hashCode * 59 + this.WritePermissionProfiles.GetHashCode();
                if (this.WritePermissionLogins != null)
                    hashCode = hashCode * 59 + this.WritePermissionLogins.GetHashCode();
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
