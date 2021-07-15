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
    /// BlogEntryForm
    /// </summary>
    [DataContract]
    public partial class BlogEntryForm :  IEquatable<BlogEntryForm>, IValidatableObject
    {
        /// <summary>
        /// category (Kommentar-Typ/Komentarart), default value is \&quot;Notiz\&quot; 
        /// </summary>
        /// <value>category (Kommentar-Typ/Komentarart), default value is \&quot;Notiz\&quot; </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CategoryEnum
        {
            /// <summary>
            /// Enum Notiz for value: Notiz
            /// </summary>
            [EnumMember(Value = "Notiz")]
            Notiz = 1,

            /// <summary>
            /// Enum Anruf for value: Anruf
            /// </summary>
            [EnumMember(Value = "Anruf")]
            Anruf = 2,

            /// <summary>
            /// Enum Besprechung for value: Besprechung
            /// </summary>
            [EnumMember(Value = "Besprechung")]
            Besprechung = 3,

            /// <summary>
            /// Enum Vertrag for value: Vertrag
            /// </summary>
            [EnumMember(Value = "Vertrag")]
            Vertrag = 4,

            /// <summary>
            /// Enum EMail for value: E-mail
            /// </summary>
            [EnumMember(Value = "E-mail")]
            EMail = 5

        }

        /// <summary>
        /// category (Kommentar-Typ/Komentarart), default value is \&quot;Notiz\&quot; 
        /// </summary>
        /// <value>category (Kommentar-Typ/Komentarart), default value is \&quot;Notiz\&quot; </value>
        [DataMember(Name="category", EmitDefaultValue=false)]
        public CategoryEnum? Category { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogEntryForm" /> class.
        /// </summary>
        /// <param name="category">category (Kommentar-Typ/Komentarart), default value is \&quot;Notiz\&quot; .</param>
        /// <param name="body">body (Kommentar).</param>
        public BlogEntryForm(CategoryEnum? category = default(CategoryEnum?), string body = default(string))
        {
            this.Category = category;
            this.Body = body;
        }
        

        /// <summary>
        /// body (Kommentar)
        /// </summary>
        /// <value>body (Kommentar)</value>
        [DataMember(Name="body", EmitDefaultValue=false)]
        public string Body { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BlogEntryForm {\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  Body: ").Append(Body).Append("\n");
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
            return this.Equals(input as BlogEntryForm);
        }

        /// <summary>
        /// Returns true if BlogEntryForm instances are equal
        /// </summary>
        /// <param name="input">Instance of BlogEntryForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BlogEntryForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Category == input.Category ||
                    (this.Category != null &&
                    this.Category.Equals(input.Category))
                ) && 
                (
                    this.Body == input.Body ||
                    (this.Body != null &&
                    this.Body.Equals(input.Body))
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
                if (this.Category != null)
                    hashCode = hashCode * 59 + this.Category.GetHashCode();
                if (this.Body != null)
                    hashCode = hashCode * 59 + this.Body.GetHashCode();
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
