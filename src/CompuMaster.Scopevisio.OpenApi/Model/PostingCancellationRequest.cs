using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit posting cancellation target and optional original date.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public class PostingCancellationRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingCancellationRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingCancellationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original documentNumber value.</summary>
        /// <value>The original optional document number, taking server precedence over the row number.</value>
        public string DocumentNumber
        {
            get { var token = this["documentNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["documentNumber"] = new JValue(value); else Remove("documentNumber"); }
        }

        /// <summary>Gets or sets the original pdeRowNumber value.</summary>
        /// <value>The optional Int64 posting-document row number used only without a document number.</value>
        public long? PdeRowNumber
        {
            get { var token = this["pdeRowNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<long?>(); }
            set { if (value != null) this["pdeRowNumber"] = new JValue(value); else Remove("pdeRowNumber"); }
        }

        /// <summary>Gets or sets the original cancellationDate value.</summary>
        /// <value>The original optional dd.mm.yyyy date; omission requests real cancellation on the original date.</value>
        public string CancellationDate
        {
            get { var token = this["cancellationDate"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["cancellationDate"] = new JValue(value); else Remove("cancellationDate"); }
        }
    }
}
