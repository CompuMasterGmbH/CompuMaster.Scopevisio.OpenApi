using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies overnight or vehicle position values using their shared manufacturer schema.</summary>
    /// <remarks>The shared schema describes vehicle catalog choices even for overnight creation. The SDK does not infer overnight-specific alternatives or calculate reimbursement from distance.</remarks>
    public class TravelDistancePositionCreationRequest : TravelPositionCreationRequest
    {
        /// <summary>Initializes an empty position request with no inferred distance.</summary>
        public TravelDistancePositionCreationRequest() { }

        /// <summary>Initializes a position request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete request object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public TravelDistancePositionCreationRequest(JObject other) : base(other) { }

        /// <summary>Gets or sets the original distance.</summary>
        /// <value>The optional numeric kilometres, or null to omit the field. No unit conversion, range rule or mileage reimbursement is inferred.</value>
        public decimal? Distance { get { return Value<decimal?>("distance"); } set { if (value.HasValue) this["distance"] = new JValue(value.Value); else Remove("distance"); } }
    }
}
