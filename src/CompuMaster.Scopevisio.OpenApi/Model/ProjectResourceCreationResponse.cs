using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains a project resource creation result, including non-critical errors and hints.</summary>
    public class ProjectResourceCreationResponse : JObject
    {
        /// <summary>Initializes a result by deeply copying the complete response object.</summary>
        /// <param name="other">The response to copy, including unknown metadata.</param>
        /// <exception cref="ArgumentNullException">The source response is null.</exception>
        public ProjectResourceCreationResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the response status value.</summary>
        /// <value>The status, or null when omitted or JSON null.</value>
        public int? Status { get { return Value<int?>("status"); } }

        /// <summary>Gets the created project resource identifier.</summary>
        /// <value>The identifier, or null when omitted or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the associated project identifier.</summary>
        /// <value>The identifier, or null when omitted or JSON null.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } }

        /// <summary>Gets non-critical errors reported even when the HTTP response indicates creation.</summary>
        /// <value>The original mutable JSON error details, or null when omitted.</value>
        public JToken Errors { get { return this["errors"]; } }

        /// <summary>Gets hints requiring the caller's attention.</summary>
        /// <value>The original mutable JSON hint details, or null when omitted.</value>
        /// <remarks>The SDK never automatically retries creation or enables recreation in response to hints.</remarks>
        public JToken Hints { get { return this["hints"]; } }
    }
}
