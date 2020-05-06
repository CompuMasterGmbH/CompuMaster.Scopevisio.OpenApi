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

    public partial class DimensionForm
    {
        /// <summary>
        /// Initializes a new instance of the DimensionForm class.
        /// </summary>
        public DimensionForm() { }

        /// <summary>
        /// Initializes a new instance of the DimensionForm class.
        /// </summary>
        public DimensionForm(string dimensionId = default(string), string dimensionAccountNumber = default(string))
        {
            DimensionId = dimensionId;
            DimensionAccountNumber = dimensionAccountNumber;
        }

        /// <summary>
        /// The id of the dimension. "dimension_1" for Kostenstellen,
        /// "dimension_2" for Kostenträger, "dimension_3" for Projekte, etc.
        /// </summary>
        [JsonProperty(PropertyName = "dimensionId")]
        public string DimensionId { get; set; }

        /// <summary>
        /// The account number for this dimension.
        /// </summary>
        [JsonProperty(PropertyName = "dimensionAccountNumber")]
        public string DimensionAccountNumber { get; set; }

    }
}