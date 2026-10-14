using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains copied current outgoing-document XML import data and options.</summary>
    /// <remarks>The route body is optional; a supplied object requires original XML data. Omission remains distinct from false and true without numbering, posting, PDF, duplicate, copy, overwrite or tax defaults. Original XML is neither parsed nor converted; actual format and option effects remain server-authoritative. Historical models/defaults remain unchanged.</remarks>
    public sealed class OutgoingInvoiceImportRequest : JObject
    {
        /// <summary>Initializes an empty copied import request.</summary>
        public OutgoingInvoiceImportRequest() { }

        /// <summary>Initializes a request by deeply copying original JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OutgoingInvoiceImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original generateDocumentNumbers value.</summary>
        /// <value>The optional original server document-number generation choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? GenerateDocumentNumbers { get { return Value<bool?>("generateDocumentNumbers"); } set { SetOptional("generateDocumentNumbers", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original doPost value.</summary>
        /// <value>The optional original server posting choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? DoPost { get { return Value<bool?>("doPost"); } set { SetOptional("doPost", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original skipDuplicates value.</summary>
        /// <value>The optional original server duplicate suppression choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? SkipDuplicates { get { return Value<bool?>("skipDuplicates"); } set { SetOptional("skipDuplicates", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original createPdf value.</summary>
        /// <value>The optional original server PDF generation choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? CreatePdf { get { return Value<bool?>("createPdf"); } set { SetOptional("createPdf", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original template value.</summary>
        /// <value>The optional original template string, including empty text, or null to omit it. No template lookup, selection, trimming or conversion occurs.</value>
        public string Template { get { return Value<string>("template"); } set { SetOptional("template", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original copyProductToPosition value.</summary>
        /// <value>The optional original server product-to-position copying choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? CopyProductToPosition { get { return Value<bool?>("copyProductToPosition"); } set { SetOptional("copyProductToPosition", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original copyProductToPositionOverwriteMode value.</summary>
        /// <value>The optional original server product-to-position overwrite mode choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? CopyProductToPositionOverwriteMode { get { return Value<bool?>("copyProductToPositionOverwriteMode"); } set { SetOptional("copyProductToPositionOverwriteMode", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original copyImpersonalAccountFieldsToPosition value.</summary>
        /// <value>The optional original server impersonal-account-field copying choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? CopyImpersonalAccountFieldsToPosition { get { return Value<bool?>("copyImpersonalAccountFieldsToPosition"); } set { SetOptional("copyImpersonalAccountFieldsToPosition", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original copyVatKeyAndTaxRateToPosition value.</summary>
        /// <value>The optional original server VAT-key and tax-rate copying choice. Omission remains distinct from false or true without inserting defaults or performing this operation in the client.</value>
        public bool? CopyVatKeyAndTaxRateToPosition { get { return Value<bool?>("copyVatKeyAndTaxRateToPosition"); } set { SetOptional("copyVatKeyAndTaxRateToPosition", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original data value.</summary>
        /// <value>The original required XML import string, or null to omit it while constructing a request. Text is not Base64-encoded, parsed, repaired, normalized or used to generate documents in the client; the server validates its format.</value>
        public string Data { get { return Value<string>("data"); } set { SetOptional("data", value != null ? new JValue(value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "data", "template" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be an original JSON string.", "request");
            foreach (var name in new[] { "generateDocumentNumbers", "doPost", "skipDuplicates", "createPdf", "copyProductToPosition", "copyProductToPositionOverwriteMode", "copyImpersonalAccountFieldsToPosition", "copyVatKeyAndTaxRateToPosition" })
                if (snapshot[name] != null && snapshot[name].Type != JTokenType.Boolean)
                    throw new ArgumentException("The " + name + " field must be an explicitly supplied JSON boolean.", "request");
            if (snapshot["data"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied XML import object requires its original data string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
