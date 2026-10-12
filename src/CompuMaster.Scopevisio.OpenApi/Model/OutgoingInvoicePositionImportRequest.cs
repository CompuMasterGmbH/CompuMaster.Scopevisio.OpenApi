using Newtonsoft.Json.Linq;
using System;
namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original outgoing-invoice position XML and explicit import options.</summary>
    /// <remarks>Typed null setters omit fields. Raw indexers preserve unknown fields and optional explicit null. Sources are deeply copied and the complete body snapshots before transport.</remarks>
    public class OutgoingInvoicePositionImportRequest : JObject
    {
        /// <summary>Initializes an empty import without choosing server options.</summary>
        public OutgoingInvoicePositionImportRequest() { }
        /// <summary>Initializes an import by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public OutgoingInvoicePositionImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }
        /// <summary>Gets or sets the original generateDocumentNumbers value.</summary>
        /// <value>The optional explicit server document-number-generation choice.</value>
        public bool? GenerateDocumentNumbers
        {
            get { var token = this["generateDocumentNumbers"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["generateDocumentNumbers"] = new JValue(value); else Remove("generateDocumentNumbers"); }
        }
        /// <summary>Gets or sets the original doPost value.</summary>
        /// <value>The optional explicit server posting choice; omission never enables posting in the client.</value>
        public bool? DoPost
        {
            get { var token = this["doPost"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["doPost"] = new JValue(value); else Remove("doPost"); }
        }
        /// <summary>Gets or sets the original skipDuplicates value.</summary>
        /// <value>The optional explicit server duplicate-handling choice.</value>
        public bool? SkipDuplicates
        {
            get { var token = this["skipDuplicates"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["skipDuplicates"] = new JValue(value); else Remove("skipDuplicates"); }
        }
        /// <summary>Gets or sets the original createPdf value.</summary>
        /// <value>The optional explicit server PDF-creation choice.</value>
        public bool? CreatePdf
        {
            get { var token = this["createPdf"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["createPdf"] = new JValue(value); else Remove("createPdf"); }
        }
        /// <summary>Gets or sets the original template value.</summary>
        /// <value>The original optional server template selection.</value>
        public string Template
        {
            get { var token = this["template"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["template"] = new JValue(value); else Remove("template"); }
        }
        /// <summary>Gets or sets the original copyProductToPosition value.</summary>
        /// <value>The optional explicit product-copy choice.</value>
        public bool? CopyProductToPosition
        {
            get { var token = this["copyProductToPosition"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["copyProductToPosition"] = new JValue(value); else Remove("copyProductToPosition"); }
        }
        /// <summary>Gets or sets the original copyProductToPositionOverwriteMode value.</summary>
        /// <value>The optional explicit product-copy overwrite choice.</value>
        public bool? CopyProductToPositionOverwriteMode
        {
            get { var token = this["copyProductToPositionOverwriteMode"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["copyProductToPositionOverwriteMode"] = new JValue(value); else Remove("copyProductToPositionOverwriteMode"); }
        }
        /// <summary>Gets or sets the original copyImpersonalAccountFieldsToPosition value.</summary>
        /// <value>The optional explicit impersonal-account-field-copy choice.</value>
        public bool? CopyImpersonalAccountFieldsToPosition
        {
            get { var token = this["copyImpersonalAccountFieldsToPosition"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["copyImpersonalAccountFieldsToPosition"] = new JValue(value); else Remove("copyImpersonalAccountFieldsToPosition"); }
        }
        /// <summary>Gets or sets the original copyVatKeyAndTaxRateToPosition value.</summary>
        /// <value>The optional explicit VAT-key and tax-rate-copy choice.</value>
        public bool? CopyVatKeyAndTaxRateToPosition
        {
            get { var token = this["copyVatKeyAndTaxRateToPosition"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<bool?>(); }
            set { if (value != null) this["copyVatKeyAndTaxRateToPosition"] = new JValue(value); else Remove("copyVatKeyAndTaxRateToPosition"); }
        }
        /// <summary>Gets or sets the original data value.</summary>
        /// <value>The required original import XML when a body is supplied; no parsing, normalization or rewriting occurs.</value>
        public string Data
        {
            get { var token = this["data"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["data"] = new JValue(value); else Remove("data"); }
        }
    }
}
