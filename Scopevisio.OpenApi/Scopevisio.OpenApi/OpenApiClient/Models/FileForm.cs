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

    public partial class FileForm
    {
        /// <summary>
        /// Initializes a new instance of the FileForm class.
        /// </summary>
        public FileForm() { }

        /// <summary>
        /// Initializes a new instance of the FileForm class.
        /// </summary>
        public FileForm(string data, string filename = default(string))
        {
            Filename = filename;
            Data = data;
        }

        /// <summary>
        /// filename. If no filename is given, it is assumed a pdf file is
        /// uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        /// <summary>
        /// file data in Base64
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Data == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Data");
            }
        }
    }
}
