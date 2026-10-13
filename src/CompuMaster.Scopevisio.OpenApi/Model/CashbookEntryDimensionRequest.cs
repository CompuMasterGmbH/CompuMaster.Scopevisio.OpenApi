using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit cashbook entry dimension and optional name.</summary>
    /// <remarks>The server may create a missing entry when its name is supplied; when the name is omitted the documented server default is the dimension number. The SDK does not infer a name, look up entries or enable creation itself. Typed null setters omit fields; inherited indexers preserve optional explicit null and future fields.</remarks>
    public class CashbookEntryDimensionRequest : JObject
    {
        /// <summary>Initializes an empty request without inserting defaults.</summary>
        public CashbookEntryDimensionRequest() { }

        /// <summary>Initializes a request by deeply copying all original JSON fields.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public CashbookEntryDimensionRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the required original dimension number, or null to leave it unspecified.</summary>
        public long? DimensionNumber
        {
            get { return Value<long?>("dimensionNumber"); }
            set { if (value.HasValue) this["dimensionNumber"] = new JValue(value.Value); else Remove("dimensionNumber"); }
        }

        /// <summary>Gets or sets the required original dimension entry number, or null to leave it unspecified.</summary>
        public long? DimensionEntryNumber
        {
            get { return Value<long?>("dimensionEntryNumber"); }
            set { if (value.HasValue) this["dimensionEntryNumber"] = new JValue(value.Value); else Remove("dimensionEntryNumber"); }
        }

        /// <summary>Gets or sets the optional original name for a missing dimension entry.</summary>
        public string DimensionEntryName
        {
            get { var token = this["dimensionEntryName"]; return token == null || token.Type == JTokenType.Null ? null : token.Value<string>(); }
            set { if (value != null) this["dimensionEntryName"] = value; else Remove("dimensionEntryName"); }
        }
    }
}
