﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CompuMaster.Scopevisio.OpenScopeApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class DimensionEntryValueForm
    {
        /// <summary>
        /// Initializes a new instance of the DimensionEntryValueForm class.
        /// </summary>
        public DimensionEntryValueForm() { }

        /// <summary>
        /// Initializes a new instance of the DimensionEntryValueForm class.
        /// </summary>
        public DimensionEntryValueForm(long dimensionNumber, long dimensionEntryNumber, string dimensionEntryName = default(string))
        {
            DimensionNumber = dimensionNumber;
            DimensionEntryNumber = dimensionEntryNumber;
            DimensionEntryName = dimensionEntryName;
        }

        /// <summary>
        /// dimensionNumber (e.g. 1 (Kostenstelle), 2 (Kostenträger), etc.
        /// </summary>
        [JsonProperty(PropertyName = "dimensionNumber")]
        public long DimensionNumber { get; set; }

        /// <summary>
        /// DimensionEntryNumber
        /// </summary>
        [JsonProperty(PropertyName = "dimensionEntryNumber")]
        public long DimensionEntryNumber { get; set; }

        /// <summary>
        /// DimensionEntryName, if the dimensionEntryNumber does not exist, it
        /// will be created. If dimensionEntryName is not provided then it is
        /// set to the dimensionNumber.
        /// </summary>
        [JsonProperty(PropertyName = "dimensionEntryName")]
        public string DimensionEntryName { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
