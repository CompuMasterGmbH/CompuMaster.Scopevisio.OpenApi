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
    /// ImpersonalAccountRow
    /// </summary>
    [DataContract]
    public partial class ImpersonalAccountRow :  IEquatable<ImpersonalAccountRow>, IValidatableObject
    {
        /// <summary>
        /// The account type (Kontoart).
        /// </summary>
        /// <value>The account type (Kontoart).</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum Aktiv for value: Aktiv
            /// </summary>
            [EnumMember(Value = "Aktiv")]
            Aktiv = 1,

            /// <summary>
            /// Enum Passiv for value: Passiv
            /// </summary>
            [EnumMember(Value = "Passiv")]
            Passiv = 2,

            /// <summary>
            /// Enum Kosten for value: Kosten
            /// </summary>
            [EnumMember(Value = "Kosten")]
            Kosten = 3,

            /// <summary>
            /// Enum Erlse for value: Erlöse
            /// </summary>
            [EnumMember(Value = "Erlöse")]
            Erlse = 4,

            /// <summary>
            /// Enum AktivPassiv for value: Aktiv/Passiv
            /// </summary>
            [EnumMember(Value = "Aktiv/Passiv")]
            AktivPassiv = 5,

            /// <summary>
            /// Enum SonstigeKonten for value: Sonstige Konten
            /// </summary>
            [EnumMember(Value = "Sonstige Konten")]
            SonstigeKonten = 6

        }

        /// <summary>
        /// The account type (Kontoart).
        /// </summary>
        /// <value>The account type (Kontoart).</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public TypeEnum Type { get; set; }
        /// <summary>
        /// The account category (Kontokennzeichen).
        /// </summary>
        /// <value>The account category (Kontokennzeichen).</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CategoryEnum
        {
            /// <summary>
            /// Enum AndereKonten for value: Andere Konten
            /// </summary>
            [EnumMember(Value = "Andere Konten")]
            AndereKonten = 1,

            /// <summary>
            /// Enum ForderungenausLieferungenundLeistungen for value: Forderungen aus Lieferungen und Leistungen
            /// </summary>
            [EnumMember(Value = "Forderungen aus Lieferungen und Leistungen")]
            ForderungenausLieferungenundLeistungen = 2,

            /// <summary>
            /// Enum Umsatzsteuer for value: Umsatzsteuer
            /// </summary>
            [EnumMember(Value = "Umsatzsteuer")]
            Umsatzsteuer = 3,

            /// <summary>
            /// Enum VerbindlichkeitenausLieferungenundLeistungen for value: Verbindlichkeiten aus Lieferungen und Leistungen
            /// </summary>
            [EnumMember(Value = "Verbindlichkeiten aus Lieferungen und Leistungen")]
            VerbindlichkeitenausLieferungenundLeistungen = 4,

            /// <summary>
            /// Enum Vorsteuer for value: Vorsteuer
            /// </summary>
            [EnumMember(Value = "Vorsteuer")]
            Vorsteuer = 5

        }

        /// <summary>
        /// The account category (Kontokennzeichen).
        /// </summary>
        /// <value>The account category (Kontokennzeichen).</value>
        [DataMember(Name="category", EmitDefaultValue=false)]
        public CategoryEnum? Category { get; set; }
        /// <summary>
        /// vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.
        /// </summary>
        /// <value>vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum VatCodeEnum
        {
            /// <summary>
            /// Enum INL for value: INL
            /// </summary>
            [EnumMember(Value = "INL")]
            INL = 1,

            /// <summary>
            /// Enum EUmID for value: EUmID
            /// </summary>
            [EnumMember(Value = "EUmID")]
            EUmID = 2,

            /// <summary>
            /// Enum EUoID for value: EUoID
            /// </summary>
            [EnumMember(Value = "EUoID")]
            EUoID = 3,

            /// <summary>
            /// Enum Drittland for value: Drittland
            /// </summary>
            [EnumMember(Value = "Drittland")]
            Drittland = 4

        }

        /// <summary>
        /// vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.
        /// </summary>
        /// <value>vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.</value>
        [DataMember(Name="vatCode", EmitDefaultValue=false)]
        public VatCodeEnum? VatCode { get; set; }
        /// <summary>
        /// vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.
        /// </summary>
        /// <value>vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum VatKeyEnum
        {
            /// <summary>
            /// Enum B19 for value: B19
            /// </summary>
            [EnumMember(Value = "B19")]
            B19 = 1,

            /// <summary>
            /// Enum B7 for value: B7
            /// </summary>
            [EnumMember(Value = "B7")]
            B7 = 2,

            /// <summary>
            /// Enum BoV19 for value: BoV19
            /// </summary>
            [EnumMember(Value = "BoV19")]
            BoV19 = 3,

            /// <summary>
            /// Enum E19 for value: E19
            /// </summary>
            [EnumMember(Value = "E19")]
            E19 = 4,

            /// <summary>
            /// Enum E7 for value: E7
            /// </summary>
            [EnumMember(Value = "E7")]
            E7 = 5,

            /// <summary>
            /// Enum EoV7 for value: EoV7
            /// </summary>
            [EnumMember(Value = "EoV7")]
            EoV7 = 6,

            /// <summary>
            /// Enum L19 for value: L19
            /// </summary>
            [EnumMember(Value = "L19")]
            L19 = 7,

            /// <summary>
            /// Enum LoV19 for value: LoV19
            /// </summary>
            [EnumMember(Value = "LoV19")]
            LoV19 = 8,

            /// <summary>
            /// Enum LoV7 for value: LoV7
            /// </summary>
            [EnumMember(Value = "LoV7")]
            LoV7 = 9,

            /// <summary>
            /// Enum SL19 for value: SL19
            /// </summary>
            [EnumMember(Value = "SL19")]
            SL19 = 10,

            /// <summary>
            /// Enum SLoV19 for value: SLoV19
            /// </summary>
            [EnumMember(Value = "SLoV19")]
            SLoV19 = 11,

            /// <summary>
            /// Enum U19 for value: U19
            /// </summary>
            [EnumMember(Value = "U19")]
            U19 = 12,

            /// <summary>
            /// Enum U7 for value: U7
            /// </summary>
            [EnumMember(Value = "U7")]
            U7 = 13,

            /// <summary>
            /// Enum V19 for value: V19
            /// </summary>
            [EnumMember(Value = "V19")]
            V19 = 14,

            /// <summary>
            /// Enum V7 for value: V7
            /// </summary>
            [EnumMember(Value = "V7")]
            V7 = 15

        }

        /// <summary>
        /// vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.
        /// </summary>
        /// <value>vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts.</value>
        [DataMember(Name="vatKey", EmitDefaultValue=false)]
        public VatKeyEnum? VatKey { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpersonalAccountRow" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ImpersonalAccountRow() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpersonalAccountRow" /> class.
        /// </summary>
        /// <param name="number">impersonal account number (required).</param>
        /// <param name="name">impersonal account name (required).</param>
        /// <param name="type">The account type (Kontoart). (required).</param>
        /// <param name="category">The account category (Kontokennzeichen)..</param>
        /// <param name="vatCode">vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts..</param>
        /// <param name="vatKey">vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts..</param>
        /// <param name="fixVatKey">is the vat key fixed or can another vat key be used when post to this account (default to false).</param>
        /// <param name="deactivated">it is not possible to post to this account (default to false).</param>
        /// <param name="directPosting">is direct posting possible (default to false).</param>
        /// <param name="discountAccount">discount account.</param>
        /// <param name="isFinanceAccount">is finance account (default to false).</param>
        /// <param name="subjectToClearing">is it possible to use the account for clearing (default to false).</param>
        /// <param name="dimensions">dimensions.</param>
        public ImpersonalAccountRow(string number = default(string), string name = default(string), TypeEnum type = default(TypeEnum), CategoryEnum? category = default(CategoryEnum?), VatCodeEnum? vatCode = default(VatCodeEnum?), VatKeyEnum? vatKey = default(VatKeyEnum?), bool fixVatKey = false, bool deactivated = false, bool directPosting = false, string discountAccount = default(string), bool isFinanceAccount = false, bool subjectToClearing = false, List<DimensionForm> dimensions = default(List<DimensionForm>))
        {
            // to ensure "number" is required (not null)
            if (number == null)
            {
                throw new InvalidDataException("number is a required property for ImpersonalAccountRow and cannot be null");
            }
            else
            {
                this.Number = number;
            }
            
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for ImpersonalAccountRow and cannot be null");
            }
            else
            {
                this.Name = name;
            }
            
            // to ensure "type" is required (not null)
            if (type == null)
            {
                throw new InvalidDataException("type is a required property for ImpersonalAccountRow and cannot be null");
            }
            else
            {
                this.Type = type;
            }
            
            this.Category = category;
            this.VatCode = vatCode;
            this.VatKey = vatKey;
            // use default value if no "fixVatKey" provided
            if (fixVatKey == null)
            {
                this.FixVatKey = false;
            }
            else
            {
                this.FixVatKey = fixVatKey;
            }
            // use default value if no "deactivated" provided
            if (deactivated == null)
            {
                this.Deactivated = false;
            }
            else
            {
                this.Deactivated = deactivated;
            }
            // use default value if no "directPosting" provided
            if (directPosting == null)
            {
                this.DirectPosting = false;
            }
            else
            {
                this.DirectPosting = directPosting;
            }
            this.DiscountAccount = discountAccount;
            // use default value if no "isFinanceAccount" provided
            if (isFinanceAccount == null)
            {
                this.IsFinanceAccount = false;
            }
            else
            {
                this.IsFinanceAccount = isFinanceAccount;
            }
            // use default value if no "subjectToClearing" provided
            if (subjectToClearing == null)
            {
                this.SubjectToClearing = false;
            }
            else
            {
                this.SubjectToClearing = subjectToClearing;
            }
            this.Dimensions = dimensions;
        }
        
        /// <summary>
        /// impersonal account number
        /// </summary>
        /// <value>impersonal account number</value>
        [DataMember(Name="number", EmitDefaultValue=false)]
        public string Number { get; set; }

        /// <summary>
        /// impersonal account name
        /// </summary>
        /// <value>impersonal account name</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }





        /// <summary>
        /// is the vat key fixed or can another vat key be used when post to this account
        /// </summary>
        /// <value>is the vat key fixed or can another vat key be used when post to this account</value>
        [DataMember(Name="fixVatKey", EmitDefaultValue=false)]
        public bool FixVatKey { get; set; }

        /// <summary>
        /// it is not possible to post to this account
        /// </summary>
        /// <value>it is not possible to post to this account</value>
        [DataMember(Name="deactivated", EmitDefaultValue=false)]
        public bool Deactivated { get; set; }

        /// <summary>
        /// is direct posting possible
        /// </summary>
        /// <value>is direct posting possible</value>
        [DataMember(Name="directPosting", EmitDefaultValue=false)]
        public bool DirectPosting { get; set; }

        /// <summary>
        /// discount account
        /// </summary>
        /// <value>discount account</value>
        [DataMember(Name="discountAccount", EmitDefaultValue=false)]
        public string DiscountAccount { get; set; }

        /// <summary>
        /// is finance account
        /// </summary>
        /// <value>is finance account</value>
        [DataMember(Name="isFinanceAccount", EmitDefaultValue=false)]
        public bool IsFinanceAccount { get; set; }

        /// <summary>
        /// is it possible to use the account for clearing
        /// </summary>
        /// <value>is it possible to use the account for clearing</value>
        [DataMember(Name="subjectToClearing", EmitDefaultValue=false)]
        public bool SubjectToClearing { get; set; }

        /// <summary>
        /// Gets or Sets Dimensions
        /// </summary>
        [DataMember(Name="dimensions", EmitDefaultValue=false)]
        public List<DimensionForm> Dimensions { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ImpersonalAccountRow {\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  VatCode: ").Append(VatCode).Append("\n");
            sb.Append("  VatKey: ").Append(VatKey).Append("\n");
            sb.Append("  FixVatKey: ").Append(FixVatKey).Append("\n");
            sb.Append("  Deactivated: ").Append(Deactivated).Append("\n");
            sb.Append("  DirectPosting: ").Append(DirectPosting).Append("\n");
            sb.Append("  DiscountAccount: ").Append(DiscountAccount).Append("\n");
            sb.Append("  IsFinanceAccount: ").Append(IsFinanceAccount).Append("\n");
            sb.Append("  SubjectToClearing: ").Append(SubjectToClearing).Append("\n");
            sb.Append("  Dimensions: ").Append(Dimensions).Append("\n");
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
            return this.Equals(input as ImpersonalAccountRow);
        }

        /// <summary>
        /// Returns true if ImpersonalAccountRow instances are equal
        /// </summary>
        /// <param name="input">Instance of ImpersonalAccountRow to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ImpersonalAccountRow input)
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
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Category == input.Category ||
                    (this.Category != null &&
                    this.Category.Equals(input.Category))
                ) && 
                (
                    this.VatCode == input.VatCode ||
                    (this.VatCode != null &&
                    this.VatCode.Equals(input.VatCode))
                ) && 
                (
                    this.VatKey == input.VatKey ||
                    (this.VatKey != null &&
                    this.VatKey.Equals(input.VatKey))
                ) && 
                (
                    this.FixVatKey == input.FixVatKey ||
                    (this.FixVatKey != null &&
                    this.FixVatKey.Equals(input.FixVatKey))
                ) && 
                (
                    this.Deactivated == input.Deactivated ||
                    (this.Deactivated != null &&
                    this.Deactivated.Equals(input.Deactivated))
                ) && 
                (
                    this.DirectPosting == input.DirectPosting ||
                    (this.DirectPosting != null &&
                    this.DirectPosting.Equals(input.DirectPosting))
                ) && 
                (
                    this.DiscountAccount == input.DiscountAccount ||
                    (this.DiscountAccount != null &&
                    this.DiscountAccount.Equals(input.DiscountAccount))
                ) && 
                (
                    this.IsFinanceAccount == input.IsFinanceAccount ||
                    (this.IsFinanceAccount != null &&
                    this.IsFinanceAccount.Equals(input.IsFinanceAccount))
                ) && 
                (
                    this.SubjectToClearing == input.SubjectToClearing ||
                    (this.SubjectToClearing != null &&
                    this.SubjectToClearing.Equals(input.SubjectToClearing))
                ) && 
                (
                    this.Dimensions == input.Dimensions ||
                    this.Dimensions != null &&
                    input.Dimensions != null &&
                    this.Dimensions.SequenceEqual(input.Dimensions)
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Category != null)
                    hashCode = hashCode * 59 + this.Category.GetHashCode();
                if (this.VatCode != null)
                    hashCode = hashCode * 59 + this.VatCode.GetHashCode();
                if (this.VatKey != null)
                    hashCode = hashCode * 59 + this.VatKey.GetHashCode();
                if (this.FixVatKey != null)
                    hashCode = hashCode * 59 + this.FixVatKey.GetHashCode();
                if (this.Deactivated != null)
                    hashCode = hashCode * 59 + this.Deactivated.GetHashCode();
                if (this.DirectPosting != null)
                    hashCode = hashCode * 59 + this.DirectPosting.GetHashCode();
                if (this.DiscountAccount != null)
                    hashCode = hashCode * 59 + this.DiscountAccount.GetHashCode();
                if (this.IsFinanceAccount != null)
                    hashCode = hashCode * 59 + this.IsFinanceAccount.GetHashCode();
                if (this.SubjectToClearing != null)
                    hashCode = hashCode * 59 + this.SubjectToClearing.GetHashCode();
                if (this.Dimensions != null)
                    hashCode = hashCode * 59 + this.Dimensions.GetHashCode();
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
