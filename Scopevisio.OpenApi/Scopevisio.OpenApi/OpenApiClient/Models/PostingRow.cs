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

    /// <summary>
    /// A posting always requires more than one row.
    /// </summary>
    public partial class PostingRow
    {
        /// <summary>
        /// Initializes a new instance of the PostingRow class.
        /// </summary>
        public PostingRow() { }

        /// <summary>
        /// Initializes a new instance of the PostingRow class.
        /// </summary>
        public PostingRow(long postingDate, string documentNumber, string account, double amount, long? documentDate = default(long?), string externalDocumentNumber = default(string), string summaryAccount = default(string), string cancelDocument = default(string), double? foreignCurrencyAmount = default(double?), string foreignCurrencyCode = default(string), double? foreignCurrencyRate = default(double?), string documentText = default(string), string rowText = default(string), string vatKey = default(string), IList<DimensionForm> dimensions = default(IList<DimensionForm>), double? discountPercent1 = default(double?), long? discountPeriod1 = default(long?), double? discountPercent2 = default(double?), long? discountPeriod2 = default(long?), long? nettimeLimit = default(long?), string disountAccount = default(string), string paymentType = default(string), string internalDocumentNumber = default(string))
        {
            PostingDate = postingDate;
            DocumentDate = documentDate;
            DocumentNumber = documentNumber;
            ExternalDocumentNumber = externalDocumentNumber;
            Account = account;
            SummaryAccount = summaryAccount;
            Amount = amount;
            CancelDocument = cancelDocument;
            ForeignCurrencyAmount = foreignCurrencyAmount;
            ForeignCurrencyCode = foreignCurrencyCode;
            ForeignCurrencyRate = foreignCurrencyRate;
            DocumentText = documentText;
            RowText = rowText;
            VatKey = vatKey;
            Dimensions = dimensions;
            DiscountPercent1 = discountPercent1;
            DiscountPeriod1 = discountPeriod1;
            DiscountPercent2 = discountPercent2;
            DiscountPeriod2 = discountPeriod2;
            NettimeLimit = nettimeLimit;
            DisountAccount = disountAccount;
            PaymentType = paymentType;
            InternalDocumentNumber = internalDocumentNumber;
        }

        /// <summary>
        /// posting date
        /// </summary>
        [JsonProperty(PropertyName = "postingDate")]
        public long PostingDate { get; set; }

        /// <summary>
        /// document date
        /// </summary>
        [JsonProperty(PropertyName = "documentDate")]
        public long? DocumentDate { get; set; }

        /// <summary>
        /// document number
        /// </summary>
        [JsonProperty(PropertyName = "documentNumber")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// external document number
        /// </summary>
        [JsonProperty(PropertyName = "externalDocumentNumber")]
        public string ExternalDocumentNumber { get; set; }

        /// <summary>
        /// account (personal or impersonal account)
        /// </summary>
        [JsonProperty(PropertyName = "account")]
        public string Account { get; set; }

        /// <summary>
        /// summary account (required if a personal account is used)
        /// </summary>
        [JsonProperty(PropertyName = "summaryAccount")]
        public string SummaryAccount { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        /// <summary>
        /// number of documet for cancellation
        /// </summary>
        [JsonProperty(PropertyName = "cancelDocument")]
        public string CancelDocument { get; set; }

        /// <summary>
        /// foreign currency amount
        /// </summary>
        [JsonProperty(PropertyName = "foreignCurrencyAmount")]
        public double? ForeignCurrencyAmount { get; set; }

        /// <summary>
        /// foreign currency code
        /// </summary>
        [JsonProperty(PropertyName = "foreignCurrencyCode")]
        public string ForeignCurrencyCode { get; set; }

        /// <summary>
        /// foreign currency rate
        /// </summary>
        [JsonProperty(PropertyName = "foreignCurrencyRate")]
        public double? ForeignCurrencyRate { get; set; }

        /// <summary>
        /// document text
        /// </summary>
        [JsonProperty(PropertyName = "documentText")]
        public string DocumentText { get; set; }

        /// <summary>
        /// posting row text
        /// </summary>
        [JsonProperty(PropertyName = "rowText")]
        public string RowText { get; set; }

        /// <summary>
        /// vat key
        /// </summary>
        [JsonProperty(PropertyName = "vatKey")]
        public string VatKey { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "dimensions")]
        public IList<DimensionForm> Dimensions { get; set; }

        /// <summary>
        /// discount percent 1, evaluated if "account" is personal account
        /// </summary>
        [JsonProperty(PropertyName = "discountPercent1")]
        public double? DiscountPercent1 { get; set; }

        /// <summary>
        /// discount period 1, evaluated if "account" is personal account
        /// </summary>
        [JsonProperty(PropertyName = "discountPeriod1")]
        public long? DiscountPeriod1 { get; set; }

        /// <summary>
        /// discount percent 2, evaluated if "account" is personal account
        /// </summary>
        [JsonProperty(PropertyName = "discountPercent2")]
        public double? DiscountPercent2 { get; set; }

        /// <summary>
        /// discount period 2, evaluated if "account" is personal account
        /// </summary>
        [JsonProperty(PropertyName = "discountPeriod2")]
        public long? DiscountPeriod2 { get; set; }

        /// <summary>
        /// net time limit, evaluated if "account" is personal account
        /// </summary>
        [JsonProperty(PropertyName = "nettimeLimit")]
        public long? NettimeLimit { get; set; }

        /// <summary>
        /// disountAccount(Skontokonto), evaluated if "account" is personal
        /// account
        /// </summary>
        [JsonProperty(PropertyName = "disountAccount")]
        public string DisountAccount { get; set; }

        /// <summary>
        /// paymentType(Zahlungsart), evaluated if "account" is personal
        /// account
        /// </summary>
        [JsonProperty(PropertyName = "paymentType")]
        public string PaymentType { get; set; }

        /// <summary>
        /// internal document number
        /// </summary>
        [JsonProperty(PropertyName = "internalDocumentNumber")]
        public string InternalDocumentNumber { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (DocumentNumber == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "DocumentNumber");
            }
            if (Account == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Account");
            }
        }
    }
}