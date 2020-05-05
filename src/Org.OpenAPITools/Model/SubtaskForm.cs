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
    /// SubtaskForm
    /// </summary>
    [DataContract]
    public partial class SubtaskForm :  IEquatable<SubtaskForm>, IValidatableObject
    {
        /// <summary>
        /// status
        /// </summary>
        /// <value>status</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Anstehend for value: Anstehend
            /// </summary>
            [EnumMember(Value = "Anstehend")]
            Anstehend = 1,

            /// <summary>
            /// Enum Begonnen for value: Begonnen
            /// </summary>
            [EnumMember(Value = "Begonnen")]
            Begonnen = 2,

            /// <summary>
            /// Enum Erledigt for value: Erledigt
            /// </summary>
            [EnumMember(Value = "Erledigt")]
            Erledigt = 3

        }

        /// <summary>
        /// status
        /// </summary>
        /// <value>status</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtaskForm" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected SubtaskForm() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtaskForm" /> class.
        /// </summary>
        /// <param name="taskId">Id der Aufgabe für die die Tätigkeit erstellt wird (required).</param>
        /// <param name="topic">topic.</param>
        /// <param name="dueDateTS">dueDateTS.</param>
        /// <param name="responsibleContactId">responsibleContactId.</param>
        /// <param name="status">status.</param>
        /// <param name="estimatedHours">geschätzte Anzahl Stunden.</param>
        public SubtaskForm(long taskId = default(long), string topic = default(string), long dueDateTS = default(long), long responsibleContactId = default(long), StatusEnum? status = default(StatusEnum?), decimal estimatedHours = default(decimal))
        {
            // to ensure "taskId" is required (not null)
            if (taskId == null)
            {
                throw new InvalidDataException("taskId is a required property for SubtaskForm and cannot be null");
            }
            else
            {
                this.TaskId = taskId;
            }
            
            this.Topic = topic;
            this.DueDateTS = dueDateTS;
            this.ResponsibleContactId = responsibleContactId;
            this.Status = status;
            this.EstimatedHours = estimatedHours;
        }
        
        /// <summary>
        /// Id der Aufgabe für die die Tätigkeit erstellt wird
        /// </summary>
        /// <value>Id der Aufgabe für die die Tätigkeit erstellt wird</value>
        [DataMember(Name="taskId", EmitDefaultValue=false)]
        public long TaskId { get; set; }

        /// <summary>
        /// topic
        /// </summary>
        /// <value>topic</value>
        [DataMember(Name="topic", EmitDefaultValue=false)]
        public string Topic { get; set; }

        /// <summary>
        /// dueDateTS
        /// </summary>
        /// <value>dueDateTS</value>
        [DataMember(Name="dueDateTS", EmitDefaultValue=false)]
        public long DueDateTS { get; set; }

        /// <summary>
        /// responsibleContactId
        /// </summary>
        /// <value>responsibleContactId</value>
        [DataMember(Name="responsibleContactId", EmitDefaultValue=false)]
        public long ResponsibleContactId { get; set; }


        /// <summary>
        /// geschätzte Anzahl Stunden
        /// </summary>
        /// <value>geschätzte Anzahl Stunden</value>
        [DataMember(Name="estimatedHours", EmitDefaultValue=false)]
        public decimal EstimatedHours { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubtaskForm {\n");
            sb.Append("  TaskId: ").Append(TaskId).Append("\n");
            sb.Append("  Topic: ").Append(Topic).Append("\n");
            sb.Append("  DueDateTS: ").Append(DueDateTS).Append("\n");
            sb.Append("  ResponsibleContactId: ").Append(ResponsibleContactId).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  EstimatedHours: ").Append(EstimatedHours).Append("\n");
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
            return this.Equals(input as SubtaskForm);
        }

        /// <summary>
        /// Returns true if SubtaskForm instances are equal
        /// </summary>
        /// <param name="input">Instance of SubtaskForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SubtaskForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.TaskId == input.TaskId ||
                    (this.TaskId != null &&
                    this.TaskId.Equals(input.TaskId))
                ) && 
                (
                    this.Topic == input.Topic ||
                    (this.Topic != null &&
                    this.Topic.Equals(input.Topic))
                ) && 
                (
                    this.DueDateTS == input.DueDateTS ||
                    (this.DueDateTS != null &&
                    this.DueDateTS.Equals(input.DueDateTS))
                ) && 
                (
                    this.ResponsibleContactId == input.ResponsibleContactId ||
                    (this.ResponsibleContactId != null &&
                    this.ResponsibleContactId.Equals(input.ResponsibleContactId))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.EstimatedHours == input.EstimatedHours ||
                    (this.EstimatedHours != null &&
                    this.EstimatedHours.Equals(input.EstimatedHours))
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
                if (this.TaskId != null)
                    hashCode = hashCode * 59 + this.TaskId.GetHashCode();
                if (this.Topic != null)
                    hashCode = hashCode * 59 + this.Topic.GetHashCode();
                if (this.DueDateTS != null)
                    hashCode = hashCode * 59 + this.DueDateTS.GetHashCode();
                if (this.ResponsibleContactId != null)
                    hashCode = hashCode * 59 + this.ResponsibleContactId.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.EstimatedHours != null)
                    hashCode = hashCode * 59 + this.EstimatedHours.GetHashCode();
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
