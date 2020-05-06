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

    public partial class DatevPostings
    {
        /// <summary>
        /// Initializes a new instance of the DatevPostings class.
        /// </summary>
        public DatevPostings() { }

        /// <summary>
        /// Initializes a new instance of the DatevPostings class.
        /// </summary>
        public DatevPostings(string data, string zone = default(string))
        {
            Zone = zone;
            Data = data;
        }

        /// <summary>
        /// Zone (Rechnungskreis) If this parameter is mandatory depends on
        /// the zone settings in the Scopevisio desktop application. If set
        /// to mandatory and not given, the standard zone is used.
        /// </summary>
        [JsonProperty(PropertyName = "zone")]
        public string Zone { get; set; }

        /// <summary>
        /// A DATEV-Importfile (EXTF) in Base64 format.
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