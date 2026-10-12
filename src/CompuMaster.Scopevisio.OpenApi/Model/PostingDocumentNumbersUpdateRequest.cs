using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an existing posting and explicit document-number replacements.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public class PostingDocumentNumbersUpdateRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingDocumentNumbersUpdateRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingDocumentNumbersUpdateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The required original posting document number when a body is supplied.</value>
        public string DocumentNumber
        {
            get { var token = this["documentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original externalDocumentNumber value.</summary>
        /// <value>The optional original external replacement number.</value>
        public string ExternalDocumentNumber
        {
            get { var token = this["externalDocumentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["externalDocumentNumber"] = new JValue(value); else Remove("externalDocumentNumber"); }
        }

        /// <summary>Gets or sets the original internalDocumentNumber value.</summary>
        /// <value>The optional original internal replacement number.</value>
        public string InternalDocumentNumber
        {
            get { var token = this["internalDocumentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["internalDocumentNumber"] = new JValue(value); else Remove("internalDocumentNumber"); }
        }
    }
}
