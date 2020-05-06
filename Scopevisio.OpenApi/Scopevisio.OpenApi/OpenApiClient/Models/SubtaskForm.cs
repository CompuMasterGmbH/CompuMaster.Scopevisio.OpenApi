﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CompuMaster.Scopevisio.OpenApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class SubtaskForm
    {
        /// <summary>
        /// Initializes a new instance of the SubtaskForm class.
        /// </summary>
        public SubtaskForm() { }

        /// <summary>
        /// Initializes a new instance of the SubtaskForm class.
        /// </summary>
        public SubtaskForm(long taskId, string topic = default(string), long? dueDateTS = default(long?), long? responsibleContactId = default(long?), string status = default(string), double? estimatedHours = default(double?))
        {
            TaskId = taskId;
            Topic = topic;
            DueDateTS = dueDateTS;
            ResponsibleContactId = responsibleContactId;
            Status = status;
            EstimatedHours = estimatedHours;
        }

        /// <summary>
        /// Id der Aufgabe für die die Tätigkeit erstellt wird
        /// </summary>
        [JsonProperty(PropertyName = "taskId")]
        public long TaskId { get; set; }

        /// <summary>
        /// topic
        /// </summary>
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        /// <summary>
        /// dueDateTS
        /// </summary>
        [JsonProperty(PropertyName = "dueDateTS")]
        public long? DueDateTS { get; set; }

        /// <summary>
        /// responsibleContactId
        /// </summary>
        [JsonProperty(PropertyName = "responsibleContactId")]
        public long? ResponsibleContactId { get; set; }

        /// <summary>
        /// status. Possible values include: 'Anstehend', 'Begonnen',
        /// 'Erledigt'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// geschätzte Anzahl Stunden
        /// </summary>
        [JsonProperty(PropertyName = "estimatedHours")]
        public double? EstimatedHours { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}