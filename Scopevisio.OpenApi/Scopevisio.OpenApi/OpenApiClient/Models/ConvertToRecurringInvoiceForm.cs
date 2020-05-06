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

    public partial class ConvertToRecurringInvoiceForm
    {
        /// <summary>
        /// Initializes a new instance of the ConvertToRecurringInvoiceForm
        /// class.
        /// </summary>
        public ConvertToRecurringInvoiceForm() { }

        /// <summary>
        /// Initializes a new instance of the ConvertToRecurringInvoiceForm
        /// class.
        /// </summary>
        public ConvertToRecurringInvoiceForm(long validFrom, long validTo, string frequency, long nextInvoiceDate)
        {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Frequency = frequency;
            NextInvoiceDate = nextInvoiceDate;
        }

        /// <summary>
        /// validFrom
        /// </summary>
        [JsonProperty(PropertyName = "validFrom")]
        public long ValidFrom { get; set; }

        /// <summary>
        /// validTo
        /// </summary>
        [JsonProperty(PropertyName = "validTo")]
        public long ValidTo { get; set; }

        /// <summary>
        /// frequency. Possible values include: 'daily', 'weekly', 'monthly',
        /// 'bimonthly', 'quarterly', 'half-yearly', 'yearly'
        /// </summary>
        [JsonProperty(PropertyName = "frequency")]
        public string Frequency { get; set; }

        /// <summary>
        /// nextInvoiceDate
        /// </summary>
        [JsonProperty(PropertyName = "nextInvoiceDate")]
        public long NextInvoiceDate { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Frequency == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Frequency");
            }
        }
    }
}
