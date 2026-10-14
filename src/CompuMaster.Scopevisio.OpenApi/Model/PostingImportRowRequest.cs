using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an original posting import row without chart-specific defaults or balancing.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public sealed class PostingImportRowRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingImportRowRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingImportRowRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original postingDate value.</summary>
        /// <value>The required original Int64 Unix-millisecond posting date.</value>
        public long? PostingDate
        {
            get { var token = this["postingDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["postingDate"] = new JValue(value); else Remove("postingDate"); }
        }

        /// <summary>Gets or sets the original documentDate value.</summary>
        /// <value>The optional original Int64 Unix-millisecond document date.</value>
        public long? DocumentDate
        {
            get { var token = this["documentDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["documentDate"] = new JValue(value); else Remove("documentDate"); }
        }

        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The required original document number.</value>
        public string DocumentNumber
        {
            get { var token = this["documentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original externalDocumentNumber value.</summary>
        /// <value>The optional original external document number.</value>
        public string ExternalDocumentNumber
        {
            get { var token = this["externalDocumentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalDocumentNumber"] = new JValue(value); else Remove("externalDocumentNumber"); }
        }

        /// <summary>Gets or sets the original account value.</summary>
        /// <value>The required original personal or impersonal account number.</value>
        public string Account
        {
            get { var token = this["account"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["account"] = new JValue(value); else Remove("account"); }
        }

        /// <summary>Gets or sets the original summaryAccount value.</summary>
        /// <value>The optional original summary account, required by the server for personal accounts.</value>
        public string SummaryAccount
        {
            get { var token = this["summaryAccount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["summaryAccount"] = new JValue(value); else Remove("summaryAccount"); }
        }

        /// <summary>Gets or sets the original amount value.</summary>
        /// <value>The required finite original amount; no rounding or balancing is imposed.</value>
        public decimal? Amount
        {
            get { var token = this["amount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["amount"] = new JValue(value); else Remove("amount"); }
        }

        /// <summary>Gets or sets the original cancelDocument value.</summary>
        /// <value>The original optional cancellation document number.</value>
        public string CancelDocument
        {
            get { var token = this["cancelDocument"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["cancelDocument"] = new JValue(value); else Remove("cancelDocument"); }
        }

        /// <summary>Gets or sets the original foreignCurrencyAmount value.</summary>
        /// <value>The optional finite foreign-currency amount.</value>
        public decimal? ForeignCurrencyAmount
        {
            get { var token = this["foreignCurrencyAmount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["foreignCurrencyAmount"] = new JValue(value); else Remove("foreignCurrencyAmount"); }
        }

        /// <summary>Gets or sets the original foreignCurrencyCode value.</summary>
        /// <value>The original optional foreign-currency code.</value>
        public string ForeignCurrencyCode
        {
            get { var token = this["foreignCurrencyCode"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["foreignCurrencyCode"] = new JValue(value); else Remove("foreignCurrencyCode"); }
        }

        /// <summary>Gets or sets the original foreignCurrencyRate value.</summary>
        /// <value>The optional finite foreign-currency rate.</value>
        public decimal? ForeignCurrencyRate
        {
            get { var token = this["foreignCurrencyRate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["foreignCurrencyRate"] = new JValue(value); else Remove("foreignCurrencyRate"); }
        }

        /// <summary>Gets or sets the original documentText value.</summary>
        /// <value>The original optional document text.</value>
        public string DocumentText
        {
            get { var token = this["documentText"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentText"] = new JValue(value); else Remove("documentText"); }
        }

        /// <summary>Gets or sets the original rowText value.</summary>
        /// <value>The original optional row text.</value>
        public string RowText
        {
            get { var token = this["rowText"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["rowText"] = new JValue(value); else Remove("rowText"); }
        }

        /// <summary>Gets or sets the original vatKey value.</summary>
        /// <value>The original optional VAT key; no chart-specific sample is inserted.</value>
        public string VatKey
        {
            get { var token = this["vatKey"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["vatKey"] = new JValue(value); else Remove("vatKey"); }
        }

        /// <summary>Gets or sets the original dimensions value.</summary>
        /// <value>The complete copied optional dimension selections.</value>
        public JArray Dimensions
        {
            get { return (JArray)(this["dimensions"] as JArray)?.DeepClone(); }
            set { if (value != null) this["dimensions"] = value.DeepClone(); else Remove("dimensions"); }
        }

        /// <summary>Gets or sets the original discountPercent1 value.</summary>
        /// <value>The optional finite first discount percent, evaluated by the server for personal accounts.</value>
        public decimal? DiscountPercent1
        {
            get { var token = this["discountPercent1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountPercent1"] = new JValue(value); else Remove("discountPercent1"); }
        }

        /// <summary>Gets or sets the original discountPeriod1 value.</summary>
        /// <value>The optional Int64 first discount period.</value>
        public long? DiscountPeriod1
        {
            get { var token = this["discountPeriod1"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["discountPeriod1"] = new JValue(value); else Remove("discountPeriod1"); }
        }

        /// <summary>Gets or sets the original discountPercent2 value.</summary>
        /// <value>The optional finite second discount percent, evaluated by the server for personal accounts.</value>
        public decimal? DiscountPercent2
        {
            get { var token = this["discountPercent2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<decimal?>(); }
            set { if (value != null) this["discountPercent2"] = new JValue(value); else Remove("discountPercent2"); }
        }

        /// <summary>Gets or sets the original discountPeriod2 value.</summary>
        /// <value>The optional Int64 second discount period.</value>
        public long? DiscountPeriod2
        {
            get { var token = this["discountPeriod2"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["discountPeriod2"] = new JValue(value); else Remove("discountPeriod2"); }
        }

        /// <summary>Gets or sets the original netTimeLimit value.</summary>
        /// <value>The optional Int64 net time limit.</value>
        public long? NetTimeLimit
        {
            get { var token = this["netTimeLimit"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["netTimeLimit"] = new JValue(value); else Remove("netTimeLimit"); }
        }

        /// <summary>Gets or sets the original discountAccount value.</summary>
        /// <value>The optional original discount account.</value>
        public string DiscountAccount
        {
            get { var token = this["discountAccount"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["discountAccount"] = new JValue(value); else Remove("discountAccount"); }
        }

        /// <summary>Gets or sets the original paymentType value.</summary>
        /// <value>The optional original payment type.</value>
        public string PaymentType
        {
            get { var token = this["paymentType"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["paymentType"] = new JValue(value); else Remove("paymentType"); }
        }

        /// <summary>Gets or sets the original internalDocumentNumber value.</summary>
        /// <value>The original optional internal document number.</value>
        public string InternalDocumentNumber
        {
            get { var token = this["internalDocumentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["internalDocumentNumber"] = new JValue(value); else Remove("internalDocumentNumber"); }
        }

        /// <summary>Gets or sets the original ustId value.</summary>
        /// <value>The original optional personal-account VAT ID.</value>
        public string UstId
        {
            get { var token = this["ustId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["ustId"] = new JValue(value); else Remove("ustId"); }
        }
    }
}
