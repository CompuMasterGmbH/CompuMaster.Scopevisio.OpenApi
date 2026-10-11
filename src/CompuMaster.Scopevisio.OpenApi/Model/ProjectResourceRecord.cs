using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Provides typed access to a project resource record while retaining unknown JSON fields.</summary>
    public class ProjectResourceRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The record to copy, including unknown fields and explicit nulls.</param>
        /// <exception cref="ArgumentNullException">The source record is null.</exception>
        public ProjectResourceRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the resource identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the organisation identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Organisation { get { return Value<long?>("organisation"); } }

        /// <summary>Gets the project identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ProjectId { get { return Value<long?>("projectId"); } }

        /// <summary>Gets the project name.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectName { get { return Value<string>("projectName"); } }

        /// <summary>Gets the project number.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectNumber { get { return Value<string>("projectNumber"); } }

        /// <summary>Gets the human resource identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? HumanResourceId { get { return Value<long?>("humanResourceId"); } }

        /// <summary>Gets the human resource display name.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string HumanResourceDisplay { get { return Value<string>("humanResourceDisplay"); } }

        /// <summary>Gets the project role.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectRole { get { return Value<string>("projectRole"); } }

        /// <summary>Gets the project position.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectPosition { get { return Value<string>("projectPosition"); } }

        /// <summary>Gets the number of available days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? AvailableDays { get { return Value<long?>("availableDays"); } }

        /// <summary>Gets the number of booked days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? BookedDays { get { return Value<long?>("bookedDays"); } }

        /// <summary>Gets the booked ratio.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? BookedRatio { get { return Value<decimal?>("bookedRatio"); } }

        /// <summary>Gets the allocation status display text.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string AllocationStatusDisplay { get { return Value<string>("allocationStatusDisplay"); } }

        /// <summary>Gets the resource fee.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? Fee { get { return Value<decimal?>("fee"); } }

        /// <summary>Gets the month string.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Month { get { return Value<string>("month"); } }

        /// <summary>Gets the number of calendar days in the month.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? MonthDays { get { return Value<long?>("monthDays"); } }

        /// <summary>Gets the number of work days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public int? WorkDays { get { return Value<int?>("workDays"); } }

        /// <summary>Gets the number of available days in the month.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? AvailableMonthDays { get { return Value<long?>("availableMonthDays"); } }
    }
}
