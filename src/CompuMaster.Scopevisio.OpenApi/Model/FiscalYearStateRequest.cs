using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicit fiscal-year or period state selections without inferring current state.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers retain unknown fields and explicit optional JSON null. The body snapshots before HTTP I/O and server business constraints remain authoritative.</remarks>
    public class FiscalYearStateRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting default selections.</summary>
        public FiscalYearStateRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public FiscalYearStateRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original fiscalYearName selection.</summary>
        /// <value>The optional original fiscal-year name, including empty text, or null to omit it.</value>
        public string FiscalYearName { get { return Value<string>("fiscalYearName"); } set { if (value != null) this["fiscalYearName"] = new JValue(value); else Remove("fiscalYearName"); } }

        /// <summary>Gets or sets the original periodId selection.</summary>
        /// <value>The optional original Int32 period identifier, or null to omit it. The server validates year/period selection and constraints; the SDK does not infer the active period or identifier ranges.</value>
        public int? PeriodId { get { return Value<int?>("periodId"); } set { if (value.HasValue) this["periodId"] = new JValue(value.Value); else Remove("periodId"); } }

        /// <summary>Gets or sets the original open selection.</summary>
        /// <value>The explicit open/close choice, or null to omit it. No unspecified default or existing state is inferred.</value>
        public bool? Open { get { return Value<bool?>("open"); } set { if (value.HasValue) this["open"] = new JValue(value.Value); else Remove("open"); } }

    }
}
