using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains the complete VAT scope response with typed access and unknown JSON fields.</summary>
    public class VatScopesResponse : JObject
    {
        /// <summary>Initializes the result by deeply copying a JSON object.</summary>
        /// <param name="other">The object to copy, including unknown fields and omitted or null values.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        /// <exception cref="JsonSerializationException">The records field is not an array of JSON objects.</exception>
        public VatScopesResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
            var values = this["records"];
            if (values != null && values.Type != JTokenType.Null &&
                (!(values is JArray array) || array.Any(value => !(value is JObject))))
                throw new JsonSerializationException("The records field must be an array of JSON objects.");
        }

        /// <summary>Gets typed copies of the records.</summary>
        /// <value>A read-only list of copied records, or null when the records field is omitted or JSON null.</value>
        public IReadOnlyList<VatScopeRecord> Records
        {
            get
            {
                var values = this["records"] as JArray;
                return values == null ? null : Array.AsReadOnly(values.Select(value => new VatScopeRecord((JObject)value)).ToArray());
            }
        }
    }
}
