using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Retains the complete revenue-account response with typed copies of its records.</summary>
    public class RevenueAccountsResponse : JObject
    {
        /// <summary>Initializes a response by deeply copying a JSON object.</summary>
        /// <param name="other">The source object, including unknown response metadata.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        /// <exception cref="JsonSerializationException">The records field is not an array of JSON objects.</exception>
        public RevenueAccountsResponse(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other)))
        {
            var records = this["records"];
            if (records != null && records.Type != JTokenType.Null &&
                (!(records is JArray array) || array.Any(record => !(record is JObject))))
                throw new JsonSerializationException("The records field must be an array of JSON objects.");
        }

        /// <summary>Gets typed copies of the response records.</summary>
        /// <value>A read-only list of copied records, or null when records are omitted or JSON null.</value>
        public IReadOnlyList<RevenueAccountRecord> Records
        {
            get
            {
                var records = this["records"] as JArray;
                return records == null ? null : Array.AsReadOnly(records.Select(record => new RevenueAccountRecord((JObject)record)).ToArray());
            }
        }
    }
}
