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

    public partial class CustomField
    {
        /// <summary>
        /// Initializes a new instance of the CustomField class.
        /// </summary>
        public CustomField() { }

        /// <summary>
        /// Initializes a new instance of the CustomField class.
        /// </summary>
        public CustomField(string customFieldName, object customFieldValue)
        {
            CustomFieldName = customFieldName;
            CustomFieldValue = customFieldValue;
        }

        /// <summary>
        /// See our &lt;a
        /// href='../browser/index.html#!/documentation#cf'&gt;documentation
        /// &lt;/a&gt; for how the names are put together.
        /// </summary>
        [JsonProperty(PropertyName = "customFieldName")]
        public string CustomFieldName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customFieldValue")]
        public object CustomFieldValue { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (CustomFieldName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "CustomFieldName");
            }
            if (CustomFieldValue == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "CustomFieldValue");
            }
        }
    }
}
