using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>
    /// Represents an extensible JSON search object with optional paging and field selection.
    /// </summary>
    /// <remarks>
    /// Unset properties are omitted so that server defaults apply. Additional endpoint-specific
    /// filters can be assigned through the inherited JSON indexer. Criteria keep their original
    /// field names, operator strings and value types, including explicit JSON null values.
    /// </remarks>
    public class SearchRequest : JObject
    {
        /// <summary>Initializes an empty search object without imposing server defaults.</summary>
        public SearchRequest() { }

        /// <summary>Initializes a search object by copying all fields from another JSON object.</summary>
        /// <param name="other">The JSON object to copy, including unknown filters.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public SearchRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the zero-based page number.</summary>
        /// <value>The requested page, or null to omit it and use the server default of zero.</value>
        /// <remarks>Negative values are sent unchanged; the server applies its documented lower bound.</remarks>
        public int? Page { get { return Value<int?>("page"); } set { SetOptional("page", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the requested number of records per page.</summary>
        /// <value>The requested size, or null to use the server default of 100.</value>
        /// <remarks>The documented server maximum is 1000. The client does not clamp values.</remarks>
        public int? PageSize { get { return Value<int?>("pageSize"); } set { SetOptional("pageSize", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the fields selected for the response.</summary>
        /// <value>A copied array of field names, or null to omit field selection.</value>
        public string[] Fields { get { return this["fields"]?.ToObject<string[]>(); } set { SetOptional("fields", value == null ? null : new JArray(value)); } }

        /// <summary>Gets or sets server-side value formatting for selected fields.</summary>
        /// <value>The formatting choice, or null to use server defaults.</value>
        /// <remarks>The server applies this setting only when at least one response field is selected.</remarks>
        public bool? FormatValues { get { return Value<bool?>("formatValues"); } set { SetOptional("formatValues", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the response sort expressions.</summary>
        /// <value>A copied array of expressions such as "lastname = desc", or null to omit ordering.</value>
        public string[] Order { get { return this["order"]?.ToObject<string[]>(); } set { SetOptional("order", value == null ? null : new JArray(value)); } }

        /// <summary>Gets or sets whether the search requests only the number of matching records.</summary>
        /// <value>The count-only choice, or null to omit the JSON count field.</value>
        /// <remarks>The server ignores paging, field selection and ordering when count is true.</remarks>
        public bool? CountOnly { get { return Value<bool?>("count"); } set { SetOptional("count", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the JSON search criteria combined by the server.</summary>
        /// <value>The mutable criteria array, or null to omit it. Assigned arrays are copied.</value>
        /// <remarks>
        /// Criteria may use field/fields, value/values and operator keys. Operator strings and
        /// comma-separated OR expressions are sent unchanged. Separate criteria are combined with AND.
        /// </remarks>
        public JArray Search { get { return this["search"] as JArray; } set { SetOptional("search", value?.DeepClone()); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
