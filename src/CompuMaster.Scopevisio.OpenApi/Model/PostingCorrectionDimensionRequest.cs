using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an original posting dimension and account selection.</summary>
    /// <remarks>Typed null setters omit fields; inherited indexers preserve unknown fields and optional explicit JSON null. Source objects and arrays are copied; known required fields are validated before transport.</remarks>
    public class PostingCorrectionDimensionRequest : JObject
    {
        /// <summary>Initializes an empty request without selecting defaults.</summary>
        public PostingCorrectionDimensionRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source is null.</exception>
        public PostingCorrectionDimensionRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original dimensionId value.</summary>
        /// <value>The original optional dimension identifier.</value>
        public string DimensionId
        {
            get { var token = this["dimensionId"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimensionId"] = new JValue(value); else Remove("dimensionId"); }
        }

        /// <summary>Gets or sets the original dimensionAccountNumber value.</summary>
        /// <value>The original optional dimension account number.</value>
        public string DimensionAccountNumber
        {
            get { var token = this["dimensionAccountNumber"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimensionAccountNumber"] = new JValue(value); else Remove("dimensionAccountNumber"); }
        }
    }
}
