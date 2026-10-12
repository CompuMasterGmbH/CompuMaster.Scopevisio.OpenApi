using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original XML and explicit options for correcting existing postings.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public class PostingCorrectionXmlRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingCorrectionXmlRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingCorrectionXmlRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original generateVat value.</summary>
        /// <value>The optional server VAT-generation choice.</value>
        public bool? GenerateVat
        {
            get { var token = this["generateVat"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["generateVat"] = new JValue(value); else Remove("generateVat"); }
        }

        /// <summary>Gets or sets the original postingBatch value.</summary>
        /// <value>The optional server batch choice.</value>
        public bool? PostingBatch
        {
            get { var token = this["postingBatch"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["postingBatch"] = new JValue(value); else Remove("postingBatch"); }
        }

        /// <summary>Gets or sets the original postingXml value.</summary>
        /// <value>The required original XML when a body is supplied; it is not parsed or rewritten.</value>
        public string PostingXml
        {
            get { var token = this["postingXml"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["postingXml"] = new JValue(value); else Remove("postingXml"); }
        }

        /// <summary>Gets or sets the original importMasterDataEnabled value.</summary>
        /// <value>The optional explicit master-data import choice.</value>
        public bool? ImportMasterDataEnabled
        {
            get { var token = this["importMasterDataEnabled"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["importMasterDataEnabled"] = new JValue(value); else Remove("importMasterDataEnabled"); }
        }

        /// <summary>Gets or sets the original importMasterDataMode value.</summary>
        /// <value>The original CREATEORUPDATE or CREATEONLY server mode; no client default is inserted.</value>
        public string ImportMasterDataMode
        {
            get { var token = this["importMasterDataMode"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["importMasterDataMode"] = new JValue(value); else Remove("importMasterDataMode"); }
        }

        /// <summary>Gets or sets the original importMasterDataAccountSearchOnly value.</summary>
        /// <value>The optional account-search-only choice.</value>
        public bool? ImportMasterDataAccountSearchOnly
        {
            get { var token = this["importMasterDataAccountSearchOnly"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["importMasterDataAccountSearchOnly"] = new JValue(value); else Remove("importMasterDataAccountSearchOnly"); }
        }

        /// <summary>Gets or sets the original importFilesEnabled value.</summary>
        /// <value>The optional explicit file-import choice.</value>
        public bool? ImportFilesEnabled
        {
            get { var token = this["importFilesEnabled"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["importFilesEnabled"] = new JValue(value); else Remove("importFilesEnabled"); }
        }

        /// <summary>Gets or sets the original generateVatCreatorInfos value.</summary>
        /// <value>The optional VAT creator-information choice.</value>
        public bool? GenerateVatCreatorInfos
        {
            get { var token = this["generateVatCreatorInfos"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["generateVatCreatorInfos"] = new JValue(value); else Remove("generateVatCreatorInfos"); }
        }

        /// <summary>Gets or sets the original refreshPaymentTerm value.</summary>
        /// <value>The optional payment-term refresh choice.</value>
        public bool? RefreshPaymentTerm
        {
            get { var token = this["refreshPaymentTerm"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["refreshPaymentTerm"] = new JValue(value); else Remove("refreshPaymentTerm"); }
        }

        /// <summary>Gets or sets the original importReferenceAttributes value.</summary>
        /// <value>The optional reference-attribute import choice.</value>
        public bool? ImportReferenceAttributes
        {
            get { var token = this["importReferenceAttributes"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["importReferenceAttributes"] = new JValue(value); else Remove("importReferenceAttributes"); }
        }

        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The original optional cancellation document number.</value>
        public string DocumentNumber
        {
            get { var token = this["documentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original pdeRowNumber value.</summary>
        /// <value>The optional Int64 posting-document row number, used by the server only without a document number.</value>
        public long? PdeRowNumber
        {
            get { var token = this["pdeRowNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["pdeRowNumber"] = new JValue(value); else Remove("pdeRowNumber"); }
        }

        /// <summary>Gets or sets the original cancellationDate value.</summary>
        /// <value>The original optional dd.mm.yyyy cancellation date; omission requests real cancellation on the original date.</value>
        public string CancellationDate
        {
            get { var token = this["cancellationDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["cancellationDate"] = new JValue(value); else Remove("cancellationDate"); }
        }
    }
}
