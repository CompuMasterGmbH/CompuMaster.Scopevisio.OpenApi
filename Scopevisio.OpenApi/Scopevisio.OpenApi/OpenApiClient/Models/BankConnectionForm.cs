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

    public partial class BankConnectionForm
    {
        /// <summary>
        /// Initializes a new instance of the BankConnectionForm class.
        /// </summary>
        public BankConnectionForm() { }

        /// <summary>
        /// Initializes a new instance of the BankConnectionForm class.
        /// </summary>
        public BankConnectionForm(string number = default(string), string bankCode = default(string), string iban = default(string), string swiftBic = default(string), string bankName = default(string), string accountHolder = default(string), bool? standard = default(bool?))
        {
            Number = number;
            BankCode = bankCode;
            Iban = iban;
            SwiftBic = swiftBic;
            BankName = bankName;
            AccountHolder = accountHolder;
            Standard = standard;
        }

        /// <summary>
        /// number (Kontonummer)
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        /// <summary>
        /// bankCode (BLZ)
        /// </summary>
        [JsonProperty(PropertyName = "bankCode")]
        public string BankCode { get; set; }

        /// <summary>
        /// iban
        /// </summary>
        [JsonProperty(PropertyName = "iban")]
        public string Iban { get; set; }

        /// <summary>
        /// swiftBic
        /// </summary>
        [JsonProperty(PropertyName = "swiftBic")]
        public string SwiftBic { get; set; }

        /// <summary>
        /// bankName (Kreditinstitut)
        /// </summary>
        [JsonProperty(PropertyName = "bankName")]
        public string BankName { get; set; }

        /// <summary>
        /// accountHolder (Kontoinhaber)
        /// </summary>
        [JsonProperty(PropertyName = "accountHolder")]
        public string AccountHolder { get; set; }

        /// <summary>
        /// standard, whether or not this should be the standard bank
        /// connection for transfers or withdrawals.
        /// </summary>
        [JsonProperty(PropertyName = "standard")]
        public bool? Standard { get; set; }

    }
}
