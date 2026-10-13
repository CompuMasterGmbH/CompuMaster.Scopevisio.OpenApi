using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit cashbook entry without historical monetary defaults.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve optional explicit JSON null and future fields. Source objects and arrays are copied. Required identifiers/timestamps and nested dimension numbers are validated before transport. No month/date conversion, cashbook selection, zero amounts, account lookup or dimension name is inferred. The server assigns the read-only internal document number.</remarks>
    public class CashbookEntryCreationRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults.</summary>
        public CashbookEntryCreationRequest() { }

        /// <summary>Initializes a request by deeply copying all original JSON fields.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public CashbookEntryCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the required original cashbook identifier, or null to leave it unspecified.</summary>
        public long? CashbookId
        {
            get { return Value<long?>("cashbookId"); }
            set { if (value.HasValue) this["cashbookId"] = new JValue(value.Value); else Remove("cashbookId"); }
        }

        /// <summary>Gets or sets the required original document timestamp in Unix epoch milliseconds.</summary>
        public long? DocumentDate
        {
            get { return Value<long?>("documentDate"); }
            set { if (value.HasValue) this["documentDate"] = new JValue(value.Value); else Remove("documentDate"); }
        }

        /// <summary>Gets or sets the optional original external document number.</summary>
        public string ExternalDocumentNumber
        {
            get { var token = this["externalDocumentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalDocumentNumber"] = value; else Remove("externalDocumentNumber"); }
        }

        /// <summary>Gets or sets the optional original document text.</summary>
        public string FreeText
        {
            get { var token = this["freeText"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["freeText"] = value; else Remove("freeText"); }
        }

        /// <summary>Gets or sets the optional original receipt amount without inserting zero or a sign rule.</summary>
        public decimal? DebitAmount
        {
            get { return Value<decimal?>("debitAmount"); }
            set { if (value.HasValue) this["debitAmount"] = new JValue(value.Value); else Remove("debitAmount"); }
        }

        /// <summary>Gets or sets the optional original expense amount without inserting zero or a sign rule.</summary>
        public decimal? CreditAmount
        {
            get { return Value<decimal?>("creditAmount"); }
            set { if (value.HasValue) this["creditAmount"] = new JValue(value.Value); else Remove("creditAmount"); }
        }

        /// <summary>Gets or sets the optional original impersonal account number without lookup or numeric conversion.</summary>
        public string ImpersonalAccountNumber
        {
            get { var token = this["impersonalAccountNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["impersonalAccountNumber"] = value; else Remove("impersonalAccountNumber"); }
        }

        /// <summary>Gets or sets the optional original customer or supplier account number without lookup or numeric conversion.</summary>
        public string PersonalAccountNumber
        {
            get { var token = this["personalAccountNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["personalAccountNumber"] = value; else Remove("personalAccountNumber"); }
        }

        /// <summary>Gets or sets the optional original notice.</summary>
        public string Notice
        {
            get { var token = this["notice"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["notice"] = value; else Remove("notice"); }
        }

        /// <summary>Gets or sets the copied optional dimension array, including explicit empty arrays.</summary>
        public JArray Dimensions
        {
            get { return (JArray)(this["dimensions"] as JArray)?.DeepClone(); }
            set { if (value != null) this["dimensions"] = value.DeepClone(); else Remove("dimensions"); }
        }
    }
}
