using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied travel-position values and complete receipt JSON.</summary>
    /// <remarks>Typed null setters omit fields; raw indexers preserve unknown fields and optional JSON null. Required travelEntryId and fileform must be supplied before I/O. Catalogs, tax rates and Base64/file acceptance remain server-authoritative.</remarks>
    public class TravelPositionCreationRequest : JObject
    {
        /// <summary>Initializes an empty request whose travel identity and receipt array must be assigned before sending.</summary>
        public TravelPositionCreationRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete request object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public TravelPositionCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the travel entry id.</summary>
        /// <value>The required original Int64 travel entry identifier. Zero/negative values remain server validation choices.</value>
        public long? TravelEntryId
        {
            get { return Value<long?>("travelEntryId"); }
            set { SetOptional("travelEntryId", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the type display.</summary>
        /// <value>The original optional expense-type catalog text, or null to omit it. No enum rejects tenant-specific or future choices.</value>
        public string TypeDisplay
        {
            get { return Value<string>("typeDisplay"); }
            set { SetOptional("typeDisplay", value != null ? new JValue(value) : null); }
        }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The optional original amount, or null to omit it. No currency/range/scale conversion is added.</value>
        public decimal? Amount
        {
            get { return Value<decimal?>("amount"); }
            set { SetOptional("amount", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the tax rate.</summary>
        /// <value>The optional original numeric tax rate, or null to omit it. Catalog/range membership is validated by the server.</value>
        public decimal? TaxRate
        {
            get { return Value<decimal?>("taxRate"); }
            set { SetOptional("taxRate", value.HasValue ? new JValue(value.Value) : null); }
        }

        /// <summary>Gets or sets the fileform.</summary>
        /// <value>A defensive copy of the required receipt object array, including explicit empty arrays. Receipt data must be a JSON string; the schema supplies no minimum receipt count.</value>
        public JArray Fileform
        {
            get { return (JArray)(this["fileform"] as JArray)?.DeepClone(); }
            set { SetOptional("fileform", value != null ? value.DeepClone() : null); }
        }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }
    }
}
