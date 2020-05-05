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
    /// TravelEntryForm
    /// </summary>
    [DataContract]
    public partial class TravelEntryForm :  IEquatable<TravelEntryForm>, IValidatableObject
    {
        /// <summary>
        /// accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user. 
        /// </summary>
        /// <value>accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user. </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AccountTypeEnum
        {
            /// <summary>
            /// Enum Mitarbeiter for value: Mitarbeiter
            /// </summary>
            [EnumMember(Value = "Mitarbeiter")]
            Mitarbeiter = 1,

            /// <summary>
            /// Enum Unternehmer for value: Unternehmer
            /// </summary>
            [EnumMember(Value = "Unternehmer")]
            Unternehmer = 2,

            /// <summary>
            /// Enum Extern for value: Extern
            /// </summary>
            [EnumMember(Value = "Extern")]
            Extern = 3

        }

        /// <summary>
        /// accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user. 
        /// </summary>
        /// <value>accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user. </value>
        [DataMember(Name="accountType", EmitDefaultValue=false)]
        public AccountTypeEnum? AccountType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TravelEntryForm" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected TravelEntryForm() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TravelEntryForm" /> class.
        /// </summary>
        /// <param name="employeeId">Id of contact (Reisender) (required).</param>
        /// <param name="accountType">accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user. .</param>
        /// <param name="documentnumber">documentNumber(Reisenummer). \&quot;documentNumber\&quot; if not provided a document number is generated. .</param>
        /// <param name="orderId">orderId(Auftrag).</param>
        /// <param name="dimensionNumber">dimensionNumber(Kostenstelle).</param>
        /// <param name="projectId">projectId(Projekt).</param>
        /// <param name="note">note(Anlass der Reise)..</param>
        /// <param name="startTs">start timestamp(Reisebeginn).\&quot;startTs\&quot; is a mandatory field. If not given it is not work. (required).</param>
        /// <param name="endTs">end timestamp(Reiseende).\&quot;endTs\&quot; is a mandatory field. If not given it is not work. (required).</param>
        /// <param name="startContactId">contactId(kontakt).</param>
        /// <param name="startStreet">startStreet and startNumber(straße und hausnummer).</param>
        /// <param name="startCity">startCity(stadt).</param>
        /// <param name="startPostcode">startPostcode(postleitzahl).</param>
        /// <param name="startCountry">startCountry(land).</param>
        /// <param name="startFederalState">startFederalState(bundesland).</param>
        /// <param name="street">street and number(straße und Hausnummer).</param>
        /// <param name="city">city(stadt).</param>
        /// <param name="postcode">postcode(postleitzahl).</param>
        /// <param name="country">county(land).</param>
        /// <param name="federalState">federalstate(bundesland).</param>
        /// <param name="internalNote">internal Note(Interne Notiz).</param>
        /// <param name="stateDone">stateDone(Durchgeführt) (default to false).</param>
        /// <param name="billable">billable(Fakturierbar) (default to false).</param>
        /// <param name="permissionType">0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich.</param>
        /// <param name="readPermissionProfiles">comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1..</param>
        /// <param name="readPermissionLogins">comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1..</param>
        /// <param name="writePermissionProfiles">comma separated list of profiles for full access, evaluated only if permissionType equals 1..</param>
        /// <param name="writePermissionLogins">comma separated list of user login emails, evaluated only if permissionType equals 1..</param>
        public TravelEntryForm(long employeeId = default(long), AccountTypeEnum? accountType = default(AccountTypeEnum?), string documentnumber = default(string), long orderId = default(long), long dimensionNumber = default(long), long projectId = default(long), string note = default(string), long startTs = default(long), long endTs = default(long), long startContactId = default(long), string startStreet = default(string), string startCity = default(string), string startPostcode = default(string), string startCountry = default(string), string startFederalState = default(string), string street = default(string), string city = default(string), string postcode = default(string), string country = default(string), string federalState = default(string), string internalNote = default(string), bool stateDone = false, bool billable = false, long permissionType = default(long), string readPermissionProfiles = default(string), string readPermissionLogins = default(string), string writePermissionProfiles = default(string), string writePermissionLogins = default(string))
        {
            // to ensure "employeeId" is required (not null)
            if (employeeId == null)
            {
                throw new InvalidDataException("employeeId is a required property for TravelEntryForm and cannot be null");
            }
            else
            {
                this.EmployeeId = employeeId;
            }
            
            // to ensure "startTs" is required (not null)
            if (startTs == null)
            {
                throw new InvalidDataException("startTs is a required property for TravelEntryForm and cannot be null");
            }
            else
            {
                this.StartTs = startTs;
            }
            
            // to ensure "endTs" is required (not null)
            if (endTs == null)
            {
                throw new InvalidDataException("endTs is a required property for TravelEntryForm and cannot be null");
            }
            else
            {
                this.EndTs = endTs;
            }
            
            this.AccountType = accountType;
            this.Documentnumber = documentnumber;
            this.OrderId = orderId;
            this.DimensionNumber = dimensionNumber;
            this.ProjectId = projectId;
            this.Note = note;
            this.StartContactId = startContactId;
            this.StartStreet = startStreet;
            this.StartCity = startCity;
            this.StartPostcode = startPostcode;
            this.StartCountry = startCountry;
            this.StartFederalState = startFederalState;
            this.Street = street;
            this.City = city;
            this.Postcode = postcode;
            this.Country = country;
            this.FederalState = federalState;
            this.InternalNote = internalNote;
            // use default value if no "stateDone" provided
            if (stateDone == null)
            {
                this.StateDone = false;
            }
            else
            {
                this.StateDone = stateDone;
            }
            // use default value if no "billable" provided
            if (billable == null)
            {
                this.Billable = false;
            }
            else
            {
                this.Billable = billable;
            }
            this.PermissionType = permissionType;
            this.ReadPermissionProfiles = readPermissionProfiles;
            this.ReadPermissionLogins = readPermissionLogins;
            this.WritePermissionProfiles = writePermissionProfiles;
            this.WritePermissionLogins = writePermissionLogins;
        }
        
        /// <summary>
        /// Id of contact (Reisender)
        /// </summary>
        /// <value>Id of contact (Reisender)</value>
        [DataMember(Name="employeeId", EmitDefaultValue=false)]
        public long EmployeeId { get; set; }


        /// <summary>
        /// documentNumber(Reisenummer). \&quot;documentNumber\&quot; if not provided a document number is generated. 
        /// </summary>
        /// <value>documentNumber(Reisenummer). \&quot;documentNumber\&quot; if not provided a document number is generated. </value>
        [DataMember(Name="documentnumber", EmitDefaultValue=false)]
        public string Documentnumber { get; set; }

        /// <summary>
        /// orderId(Auftrag)
        /// </summary>
        /// <value>orderId(Auftrag)</value>
        [DataMember(Name="orderId", EmitDefaultValue=false)]
        public long OrderId { get; set; }

        /// <summary>
        /// dimensionNumber(Kostenstelle)
        /// </summary>
        /// <value>dimensionNumber(Kostenstelle)</value>
        [DataMember(Name="dimensionNumber", EmitDefaultValue=false)]
        public long DimensionNumber { get; set; }

        /// <summary>
        /// projectId(Projekt)
        /// </summary>
        /// <value>projectId(Projekt)</value>
        [DataMember(Name="projectId", EmitDefaultValue=false)]
        public long ProjectId { get; set; }

        /// <summary>
        /// note(Anlass der Reise).
        /// </summary>
        /// <value>note(Anlass der Reise).</value>
        [DataMember(Name="note", EmitDefaultValue=false)]
        public string Note { get; set; }

        /// <summary>
        /// start timestamp(Reisebeginn).\&quot;startTs\&quot; is a mandatory field. If not given it is not work.
        /// </summary>
        /// <value>start timestamp(Reisebeginn).\&quot;startTs\&quot; is a mandatory field. If not given it is not work.</value>
        [DataMember(Name="startTs", EmitDefaultValue=false)]
        public long StartTs { get; set; }

        /// <summary>
        /// end timestamp(Reiseende).\&quot;endTs\&quot; is a mandatory field. If not given it is not work.
        /// </summary>
        /// <value>end timestamp(Reiseende).\&quot;endTs\&quot; is a mandatory field. If not given it is not work.</value>
        [DataMember(Name="endTs", EmitDefaultValue=false)]
        public long EndTs { get; set; }

        /// <summary>
        /// contactId(kontakt)
        /// </summary>
        /// <value>contactId(kontakt)</value>
        [DataMember(Name="startContactId", EmitDefaultValue=false)]
        public long StartContactId { get; set; }

        /// <summary>
        /// startStreet and startNumber(straße und hausnummer)
        /// </summary>
        /// <value>startStreet and startNumber(straße und hausnummer)</value>
        [DataMember(Name="startStreet", EmitDefaultValue=false)]
        public string StartStreet { get; set; }

        /// <summary>
        /// startCity(stadt)
        /// </summary>
        /// <value>startCity(stadt)</value>
        [DataMember(Name="startCity", EmitDefaultValue=false)]
        public string StartCity { get; set; }

        /// <summary>
        /// startPostcode(postleitzahl)
        /// </summary>
        /// <value>startPostcode(postleitzahl)</value>
        [DataMember(Name="startPostcode", EmitDefaultValue=false)]
        public string StartPostcode { get; set; }

        /// <summary>
        /// startCountry(land)
        /// </summary>
        /// <value>startCountry(land)</value>
        [DataMember(Name="startCountry", EmitDefaultValue=false)]
        public string StartCountry { get; set; }

        /// <summary>
        /// startFederalState(bundesland)
        /// </summary>
        /// <value>startFederalState(bundesland)</value>
        [DataMember(Name="startFederalState", EmitDefaultValue=false)]
        public string StartFederalState { get; set; }

        /// <summary>
        /// street and number(straße und Hausnummer)
        /// </summary>
        /// <value>street and number(straße und Hausnummer)</value>
        [DataMember(Name="street", EmitDefaultValue=false)]
        public string Street { get; set; }

        /// <summary>
        /// city(stadt)
        /// </summary>
        /// <value>city(stadt)</value>
        [DataMember(Name="city", EmitDefaultValue=false)]
        public string City { get; set; }

        /// <summary>
        /// postcode(postleitzahl)
        /// </summary>
        /// <value>postcode(postleitzahl)</value>
        [DataMember(Name="postcode", EmitDefaultValue=false)]
        public string Postcode { get; set; }

        /// <summary>
        /// county(land)
        /// </summary>
        /// <value>county(land)</value>
        [DataMember(Name="country", EmitDefaultValue=false)]
        public string Country { get; set; }

        /// <summary>
        /// federalstate(bundesland)
        /// </summary>
        /// <value>federalstate(bundesland)</value>
        [DataMember(Name="federalState", EmitDefaultValue=false)]
        public string FederalState { get; set; }

        /// <summary>
        /// internal Note(Interne Notiz)
        /// </summary>
        /// <value>internal Note(Interne Notiz)</value>
        [DataMember(Name="internalNote", EmitDefaultValue=false)]
        public string InternalNote { get; set; }

        /// <summary>
        /// stateDone(Durchgeführt)
        /// </summary>
        /// <value>stateDone(Durchgeführt)</value>
        [DataMember(Name="stateDone", EmitDefaultValue=false)]
        public bool StateDone { get; set; }

        /// <summary>
        /// billable(Fakturierbar)
        /// </summary>
        /// <value>billable(Fakturierbar)</value>
        [DataMember(Name="billable", EmitDefaultValue=false)]
        public bool Billable { get; set; }

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
            sb.Append("class TravelEntryForm {\n");
            sb.Append("  EmployeeId: ").Append(EmployeeId).Append("\n");
            sb.Append("  AccountType: ").Append(AccountType).Append("\n");
            sb.Append("  Documentnumber: ").Append(Documentnumber).Append("\n");
            sb.Append("  OrderId: ").Append(OrderId).Append("\n");
            sb.Append("  DimensionNumber: ").Append(DimensionNumber).Append("\n");
            sb.Append("  ProjectId: ").Append(ProjectId).Append("\n");
            sb.Append("  Note: ").Append(Note).Append("\n");
            sb.Append("  StartTs: ").Append(StartTs).Append("\n");
            sb.Append("  EndTs: ").Append(EndTs).Append("\n");
            sb.Append("  StartContactId: ").Append(StartContactId).Append("\n");
            sb.Append("  StartStreet: ").Append(StartStreet).Append("\n");
            sb.Append("  StartCity: ").Append(StartCity).Append("\n");
            sb.Append("  StartPostcode: ").Append(StartPostcode).Append("\n");
            sb.Append("  StartCountry: ").Append(StartCountry).Append("\n");
            sb.Append("  StartFederalState: ").Append(StartFederalState).Append("\n");
            sb.Append("  Street: ").Append(Street).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Postcode: ").Append(Postcode).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  FederalState: ").Append(FederalState).Append("\n");
            sb.Append("  InternalNote: ").Append(InternalNote).Append("\n");
            sb.Append("  StateDone: ").Append(StateDone).Append("\n");
            sb.Append("  Billable: ").Append(Billable).Append("\n");
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
            return this.Equals(input as TravelEntryForm);
        }

        /// <summary>
        /// Returns true if TravelEntryForm instances are equal
        /// </summary>
        /// <param name="input">Instance of TravelEntryForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TravelEntryForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.EmployeeId == input.EmployeeId ||
                    (this.EmployeeId != null &&
                    this.EmployeeId.Equals(input.EmployeeId))
                ) && 
                (
                    this.AccountType == input.AccountType ||
                    (this.AccountType != null &&
                    this.AccountType.Equals(input.AccountType))
                ) && 
                (
                    this.Documentnumber == input.Documentnumber ||
                    (this.Documentnumber != null &&
                    this.Documentnumber.Equals(input.Documentnumber))
                ) && 
                (
                    this.OrderId == input.OrderId ||
                    (this.OrderId != null &&
                    this.OrderId.Equals(input.OrderId))
                ) && 
                (
                    this.DimensionNumber == input.DimensionNumber ||
                    (this.DimensionNumber != null &&
                    this.DimensionNumber.Equals(input.DimensionNumber))
                ) && 
                (
                    this.ProjectId == input.ProjectId ||
                    (this.ProjectId != null &&
                    this.ProjectId.Equals(input.ProjectId))
                ) && 
                (
                    this.Note == input.Note ||
                    (this.Note != null &&
                    this.Note.Equals(input.Note))
                ) && 
                (
                    this.StartTs == input.StartTs ||
                    (this.StartTs != null &&
                    this.StartTs.Equals(input.StartTs))
                ) && 
                (
                    this.EndTs == input.EndTs ||
                    (this.EndTs != null &&
                    this.EndTs.Equals(input.EndTs))
                ) && 
                (
                    this.StartContactId == input.StartContactId ||
                    (this.StartContactId != null &&
                    this.StartContactId.Equals(input.StartContactId))
                ) && 
                (
                    this.StartStreet == input.StartStreet ||
                    (this.StartStreet != null &&
                    this.StartStreet.Equals(input.StartStreet))
                ) && 
                (
                    this.StartCity == input.StartCity ||
                    (this.StartCity != null &&
                    this.StartCity.Equals(input.StartCity))
                ) && 
                (
                    this.StartPostcode == input.StartPostcode ||
                    (this.StartPostcode != null &&
                    this.StartPostcode.Equals(input.StartPostcode))
                ) && 
                (
                    this.StartCountry == input.StartCountry ||
                    (this.StartCountry != null &&
                    this.StartCountry.Equals(input.StartCountry))
                ) && 
                (
                    this.StartFederalState == input.StartFederalState ||
                    (this.StartFederalState != null &&
                    this.StartFederalState.Equals(input.StartFederalState))
                ) && 
                (
                    this.Street == input.Street ||
                    (this.Street != null &&
                    this.Street.Equals(input.Street))
                ) && 
                (
                    this.City == input.City ||
                    (this.City != null &&
                    this.City.Equals(input.City))
                ) && 
                (
                    this.Postcode == input.Postcode ||
                    (this.Postcode != null &&
                    this.Postcode.Equals(input.Postcode))
                ) && 
                (
                    this.Country == input.Country ||
                    (this.Country != null &&
                    this.Country.Equals(input.Country))
                ) && 
                (
                    this.FederalState == input.FederalState ||
                    (this.FederalState != null &&
                    this.FederalState.Equals(input.FederalState))
                ) && 
                (
                    this.InternalNote == input.InternalNote ||
                    (this.InternalNote != null &&
                    this.InternalNote.Equals(input.InternalNote))
                ) && 
                (
                    this.StateDone == input.StateDone ||
                    (this.StateDone != null &&
                    this.StateDone.Equals(input.StateDone))
                ) && 
                (
                    this.Billable == input.Billable ||
                    (this.Billable != null &&
                    this.Billable.Equals(input.Billable))
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
                if (this.EmployeeId != null)
                    hashCode = hashCode * 59 + this.EmployeeId.GetHashCode();
                if (this.AccountType != null)
                    hashCode = hashCode * 59 + this.AccountType.GetHashCode();
                if (this.Documentnumber != null)
                    hashCode = hashCode * 59 + this.Documentnumber.GetHashCode();
                if (this.OrderId != null)
                    hashCode = hashCode * 59 + this.OrderId.GetHashCode();
                if (this.DimensionNumber != null)
                    hashCode = hashCode * 59 + this.DimensionNumber.GetHashCode();
                if (this.ProjectId != null)
                    hashCode = hashCode * 59 + this.ProjectId.GetHashCode();
                if (this.Note != null)
                    hashCode = hashCode * 59 + this.Note.GetHashCode();
                if (this.StartTs != null)
                    hashCode = hashCode * 59 + this.StartTs.GetHashCode();
                if (this.EndTs != null)
                    hashCode = hashCode * 59 + this.EndTs.GetHashCode();
                if (this.StartContactId != null)
                    hashCode = hashCode * 59 + this.StartContactId.GetHashCode();
                if (this.StartStreet != null)
                    hashCode = hashCode * 59 + this.StartStreet.GetHashCode();
                if (this.StartCity != null)
                    hashCode = hashCode * 59 + this.StartCity.GetHashCode();
                if (this.StartPostcode != null)
                    hashCode = hashCode * 59 + this.StartPostcode.GetHashCode();
                if (this.StartCountry != null)
                    hashCode = hashCode * 59 + this.StartCountry.GetHashCode();
                if (this.StartFederalState != null)
                    hashCode = hashCode * 59 + this.StartFederalState.GetHashCode();
                if (this.Street != null)
                    hashCode = hashCode * 59 + this.Street.GetHashCode();
                if (this.City != null)
                    hashCode = hashCode * 59 + this.City.GetHashCode();
                if (this.Postcode != null)
                    hashCode = hashCode * 59 + this.Postcode.GetHashCode();
                if (this.Country != null)
                    hashCode = hashCode * 59 + this.Country.GetHashCode();
                if (this.FederalState != null)
                    hashCode = hashCode * 59 + this.FederalState.GetHashCode();
                if (this.InternalNote != null)
                    hashCode = hashCode * 59 + this.InternalNote.GetHashCode();
                if (this.StateDone != null)
                    hashCode = hashCode * 59 + this.StateDone.GetHashCode();
                if (this.Billable != null)
                    hashCode = hashCode * 59 + this.Billable.GetHashCode();
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
