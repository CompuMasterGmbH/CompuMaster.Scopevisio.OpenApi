using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Provides typed access to a human resource availability record while retaining unknown JSON fields.</summary>
    /// <remarks>Unknown fields and explicit nulls remain available through the JSON object. Numeric getters follow the unformatted schema; formatted server text may require using the raw field. Decimal fractions use bounded .NET precision.</remarks>
    public class HumanResourceAvailabilityRecord : JObject
    {
        /// <summary>Initializes a record by deeply copying a JSON object.</summary>
        /// <param name="other">The record to copy, including unknown fields and explicit nulls.</param>
        /// <exception cref="ArgumentNullException">The source record is null.</exception>
        public HumanResourceAvailabilityRecord(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets the contact identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } }

        /// <summary>Gets the contact name.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string ContactName { get { return Value<string>("contactName"); } }

        /// <summary>Gets the record identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Id { get { return Value<long?>("id"); } }

        /// <summary>Gets the organisation identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? Organisation { get { return Value<long?>("organisation"); } }

        /// <summary>Gets the human resource identifier.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public long? HumanResourceId { get { return Value<long?>("humanResourceId"); } }

        /// <summary>Gets the month string.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public string Month { get { return Value<string>("month"); } }

        /// <summary>Gets the available amount.</summary>
        /// <value>The value, or null when absent or JSON null.</value>
        public int? Available { get { return Value<int?>("available"); } }

    }
}
