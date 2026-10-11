using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Provides typed access to a project revenue record while retaining unknown JSON fields.</summary>
    public class ProjectRevenueRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The record to copy, including unknown fields and explicit nulls.</param>
        /// <exception cref="ArgumentNullException">The source record is null.</exception>
        public ProjectRevenueRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the revenue identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the organisation identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Organisation { get { return Value<long?>("organisation"); } }

        /// <summary>Gets the revenue type.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public ProjectRevenueType? RevenueType { get { long? value = Value<long?>("revenueType"); return value.HasValue ? (ProjectRevenueType?)value.Value : null; } }

        /// <summary>Gets the project identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } }

        /// <summary>Gets the project name.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectName { get { return Value<string>("projectName"); } }

        /// <summary>Gets the project number.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectNumber { get { return Value<string>("projectNumber"); } }

        /// <summary>Gets the revenue group.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string RevenueGroup { get { return Value<string>("revenueGroup"); } }

        /// <summary>Gets the revenue amount.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? Revenue { get { return Value<decimal?>("revenue"); } }

        /// <summary>Gets the extra revenue amount.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? ExtraRevenue { get { return Value<decimal?>("extraRevenue"); } }

        /// <summary>Gets the automatic row indicator.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public bool? AutomaticRow { get { return Value<bool?>("automaticRow"); } }

        /// <summary>Gets the average fee.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? AverageFee { get { return Value<decimal?>("averageFee"); } }

        /// <summary>Gets the revenue status display text.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string RevenueStatusDisplay { get { return Value<string>("revenueStatusDisplay"); } }

        /// <summary>Gets the month string.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Month { get { return Value<string>("month"); } }

        /// <summary>Gets the monthly revenue amount.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? MonthRevenue { get { return Value<decimal?>("monthRevenue"); } }
    }
}
