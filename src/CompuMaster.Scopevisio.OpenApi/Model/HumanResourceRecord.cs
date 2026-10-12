using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Provides typed access to human resource details while retaining unknown JSON fields.</summary>
    /// <remarks>Unknown fields and explicit nulls remain available through the JSON object. Numeric getters follow the unformatted schema; formatted server text may require using the raw field. Decimal fractions use bounded .NET precision.</remarks>
    public class HumanResourceRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The record to copy, including unknown fields and explicit nulls.</param>
        /// <exception cref="ArgumentNullException">The source record is null.</exception>
        public HumanResourceRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the human resource identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the organisation identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Organisation { get { return Value<long?>("organisation"); } }

        /// <summary>Gets the contact identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } }

        /// <summary>Gets the contact name.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ContactName { get { return Value<string>("contactName"); } }

        /// <summary>Gets the employment role.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Role { get { return Value<string>("role"); } }

        /// <summary>Gets the position.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Position { get { return Value<string>("position"); } }

        /// <summary>Gets the department.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Department { get { return Value<string>("department"); } }

        /// <summary>Gets the project box.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ProjectBox { get { return Value<string>("projectBox"); } }

        /// <summary>Gets the topic.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Topic { get { return Value<string>("topic"); } }

        /// <summary>Gets the location.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Location { get { return Value<string>("location"); } }

        /// <summary>Gets the total work days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? TotalWorkDays { get { return Value<long?>("totalWorkDays"); } }

        /// <summary>Gets the total booked days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? TotalBookedDays { get { return Value<long?>("totalBookedDays"); } }

        /// <summary>Gets the total available days.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? TotalAvailableDays { get { return Value<long?>("totalAvailableDays"); } }

        /// <summary>Gets the total booked ratio.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? TotalBookedRatio { get { return Value<decimal?>("totalBookedRatio"); } }

        /// <summary>Gets the fee.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public decimal? Fee { get { return Value<decimal?>("fee"); } }

        /// <summary>Gets the raw validity-start timestamp.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ValidFrom { get { return Value<long?>("validFrom"); } }

        /// <summary>Gets the raw validity-end timestamp.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ValidTill { get { return Value<long?>("validTill"); } }

    }
}
