using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains a complete human resource creation result and reported errors.</summary>
    /// <remarks>HTTP 201 alone does not establish successful workday generation or absence of errors. Unknown fields remain available through the inherited JSON object.</remarks>
    public class HumanResourceCreationResponse : JObject
    {
        /// <summary>Initializes a result by deeply copying the complete response object.</summary>
        /// <param name="other">The response object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public HumanResourceCreationResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the response status value.</summary>
        /// <value>The original status, or null when omitted or JSON null.</value>
        public int? Status { get { return Value<int?>("status"); } }

        /// <summary>Gets the created human resource identifier.</summary>
        /// <value>The original Int64 identifier, or null when omitted or JSON null.</value>
        public long? HumanResourceId { get { return Value<long?>("humanResourceId"); } }

        /// <summary>Gets the original returned location.</summary>
        /// <value>The location string, or null when omitted or JSON null. No URI resolution or catalog interpretation is added.</value>
        public string Location { get { return Value<string>("location"); } }

        /// <summary>Gets the returned error details requiring caller inspection.</summary>
        /// <value>The original mutable JSON details, or null when omitted.</value>
        public JToken Errors { get { return this["errors"]; } }
    }
}
