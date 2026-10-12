using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains the complete e-commerce SKU response with typed record access.</summary>
    public class ECommerceSkusResponse : JObject
    {
        /// <summary>Initializes a response by deeply copying a JSON object.</summary>
        /// <param name="other">The response object to copy, including unknown metadata.</param>
        /// <exception cref="ArgumentNullException">The source response is null.</exception>
        /// <exception cref="JsonSerializationException">The records field is not an array of JSON objects.</exception>
        public ECommerceSkusResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
            var records = this["records"];
            if (records != null && records.Type != JTokenType.Null &&
                (!(records is JArray array) || array.Any(record => !(record is JObject))))
                throw new JsonSerializationException("The records field must be an array of JSON objects.");
        }

        /// <summary>Gets typed copies of the response records.</summary>
        /// <value>A read-only list of copied records; null when the records field is absent or JSON null.</value>
        public IReadOnlyList<ECommerceSkuRecord> Records
        {
            get
            {
                var records = this["records"] as JArray;
                return records == null ? null : Array.AsReadOnly(records.Select(record => new ECommerceSkuRecord((JObject)record)).ToArray());
            }
        }
    }
}
