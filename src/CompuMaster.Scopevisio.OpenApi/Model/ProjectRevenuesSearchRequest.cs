using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Represents a date-bounded search for project revenues.</summary>
    /// <remarks>Required date bounds are checked before HTTP I/O. Optional fields remain omitted unless assigned.</remarks>
    public class ProjectRevenuesSearchRequest : JObject
    {
        /// <summary>Initializes an empty request whose required date bounds must be assigned before sending.</summary>
        public ProjectRevenuesSearchRequest() { }

        /// <summary>Initializes a request by copying an existing JSON object and its unknown filters.</summary>
        /// <param name="other">The JSON object to copy.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ProjectRevenuesSearchRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the earliest project start date included in the search.</summary>
        /// <value>The required date string, sent unchanged; null omits the field.</value>
        /// <remarks>The vendor pattern/examples use yyyy-MM-dd, while descriptions also mention dd.MM.yyyy. The client does not reinterpret date strings.</remarks>
        public string ProjectStartFrom { get { return Value<string>("projectStartFrom"); } set { SetOptional("projectStartFrom", value == null ? null : new JValue(value)); } }

        /// <summary>Gets or sets the latest project start date included in the search.</summary>
        /// <value>The required date string, sent unchanged; null omits the field.</value>
        /// <remarks>The vendor pattern/examples use yyyy-MM-dd, while descriptions also mention dd.MM.yyyy. The client does not reinterpret date strings.</remarks>
        public string ProjectStartTo { get { return Value<string>("projectStartTo"); } set { SetOptional("projectStartTo", value == null ? null : new JValue(value)); } }

        /// <summary>Gets or sets the nested search specification.</summary>
        /// <value>A copied search object, or null to omit it. Getter results are independent copies.</value>
        /// <remarks>The vendor excludes month, monthRevenue and id from the nested search. Explicit JSON null can be assigned through the inherited indexer.</remarks>
        public SearchRequest SearchSpecification
        {
            get { return this["searchSpecification"] is JObject value ? new SearchRequest(value) : null; }
            set { SetOptional("searchSpecification", value?.DeepClone()); }
        }

        /// <summary>Gets or sets the requested project revenue type.</summary>
        /// <value>The optional type, or null to retain the server's planning default.</value>
        public ProjectRevenueType? RevenueType
        {
            get { int? value = Value<int?>("revenueType"); return value.HasValue ? (ProjectRevenueType?)value.Value : null; }
            set { SetOptional("revenueType", value.HasValue ? new JValue((int)value.Value) : null); }
        }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
