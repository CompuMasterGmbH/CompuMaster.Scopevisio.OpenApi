using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains the complete project resource response with typed access to its records.</summary>
    public class ProjectResourcesResponse : JObject
    {
        /// <summary>Initializes a response by deeply copying a JSON object.</summary>
        /// <param name="other">The response object to copy, including unknown metadata.</param>
        /// <exception cref="ArgumentNullException">The source response is null.</exception>
        /// <exception cref="JsonSerializationException">The records field has a non-array value or contains a non-object item.</exception>
        public ProjectResourcesResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
            var records = this["records"];
            if (records != null && records.Type != JTokenType.Null &&
                (!(records is JArray array) || array.Any(record => !(record is JObject))))
                throw new JsonSerializationException("The records field must be an array of JSON objects.");
        }

        /// <summary>Gets typed copies of the response records.</summary>
        /// <value>A read-only list of copied records; null when the records field is absent or JSON null.</value>
        public IReadOnlyList<ProjectResourceRecord> Records
        {
            get
            {
                var records = this["records"] as JArray;
                return records == null ? null : Array.AsReadOnly(records.Select(record => new ProjectResourceRecord((JObject)record)).ToArray());
            }
        }
    }
}
