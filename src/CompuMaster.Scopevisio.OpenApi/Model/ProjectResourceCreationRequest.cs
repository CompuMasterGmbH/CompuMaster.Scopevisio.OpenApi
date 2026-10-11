using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Represents explicitly supplied values for creating a project resource.</summary>
    /// <remarks>Optional null setters omit fields. Known raw field types are validated before HTTP I/O; the SDK never enables recreation automatically.</remarks>
    public class ProjectResourceCreationRequest : JObject
    {
        /// <summary>Initializes an empty request whose required project ID must be assigned before sending.</summary>
        public ProjectResourceCreationRequest() { }

        /// <summary>Initializes a request by deeply copying a JSON object.</summary>
        /// <param name="other">The request to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public ProjectResourceCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the project id.</summary>
        /// <value>The required project master ID. An omitted or JSON-null ID is rejected before transport.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } set { SetOptional("projectId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the human resource id.</summary>
        /// <value>The optional human resource master ID, or null to omit it.</value>
        public long? HumanResourceId { get { return Value<long?>("humanResourceId"); } set { SetOptional("humanResourceId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the project role.</summary>
        /// <value>The optional catalog role string, or null to omit it. The server reports unknown roles in its creation warnings.</value>
        public string ProjectRole { get { return Value<string>("projectRole"); } set { SetOptional("projectRole", value == null ? null : new JValue(value)); } }

        /// <summary>Gets or sets the project position.</summary>
        /// <value>The optional catalog position string, or null to omit it. The server reports unknown positions in its creation warnings.</value>
        public string ProjectPosition { get { return Value<string>("projectPosition"); } set { SetOptional("projectPosition", value == null ? null : new JValue(value)); } }

        /// <summary>Gets or sets the allocation status.</summary>
        /// <value>The optional catalog allocation status string, or null to omit it.</value>
        public string AllocationStatus { get { return Value<string>("allocationStatus"); } set { SetOptional("allocationStatus", value == null ? null : new JValue(value)); } }

        /// <summary>Gets or sets the standard fee.</summary>
        /// <value>The optional non-negative fee, or null to omit it.</value>
        public decimal? StandardFee { get { return Value<decimal?>("standardFee"); } set { SetOptional("standardFee", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the monthly allocations.</summary>
        /// <value>A copied array of non-negative days per project month, or null to omit it. The server validates project duration and month capacity.</value>
        public long[] MonthlyAllocations { get { return this["monthlyAllocations"]?.ToObject<long[]>(); } set { SetOptional("monthlyAllocations", value == null ? null : new JArray(value)); } }

        /// <summary>Gets or sets the product id.</summary>
        /// <value>The optional service product master ID, or null to omit it.</value>
        public long? ProductId { get { return Value<long?>("productId"); } set { SetOptional("productId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the recreate.</summary>
        /// <value>An explicit recreation choice, or null to omit it and retain the server default of false. True may replace all related project revenues.</value>
        public bool? Recreate { get { return Value<bool?>("recreate"); } set { SetOptional("recreate", value.HasValue ? new JValue(value.Value) : null); } }
        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
