using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies human resource availability bounds while retaining unknown JSON fields.</summary>
    /// <remarks>The vendor declares yyyy-MM patterns/examples and conflicting dd.MM.yyyy descriptions for sv-year-month. Bounds are sent as caller-selected strings without date conversion, local range ordering or paging defaults. Server validation determines accepted formats and search semantics.</remarks>
    public class HumanResourceAvailabilityRequest : JObject
    {
        /// <summary>Initializes the required availability month bounds.</summary>
        /// <param name="validFrom">The required nonblank month-bound string, sent unchanged.</param>
        /// <param name="validTill">The required nonblank month-bound string, sent unchanged.</param>
        public HumanResourceAvailabilityRequest(string validFrom, string validTill)
        {
            ValidFrom = validFrom;
            ValidTill = validTill;
        }

        /// <summary>Initializes a request by deeply copying a JSON object.</summary>
        /// <param name="other">The request to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public HumanResourceAvailabilityRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the caller-selected availability start month.</summary>
        /// <value>The required nonblank string; no date-format conversion is performed.</value>
        public string ValidFrom { get { return Value<string>("validFrom"); } set { this["validFrom"] = value == null ? JValue.CreateNull() : new JValue(value); } }

        /// <summary>Gets or sets the caller-selected availability end month.</summary>
        /// <value>The required nonblank string; no date-format conversion is performed.</value>
        public string ValidTill { get { return Value<string>("validTill"); } set { this["validTill"] = value == null ? JValue.CreateNull() : new JValue(value); } }

        /// <summary>Gets or sets a deeply copied search specification.</summary>
        /// <value>A copied JSON object; assigning null omits the optional field.</value>
        public JObject SearchSpecification
        {
            get { return (this["searchSpecification"] as JObject)?.DeepClone() as JObject; }
            set { if (value == null) Remove("searchSpecification"); else this["searchSpecification"] = value.DeepClone(); }
        }

        internal void ValidateForSend()
        {
            foreach (var name in new[] { "validFrom", "validTill" })
            {
                var token = this[name];
                if (token == null || token.Type != JTokenType.String || string.IsNullOrWhiteSpace(token.Value<string>()))
                    throw new ArgumentException("Both availability bounds must be nonblank JSON strings.", "query");
            }
            var search = this["searchSpecification"];
            if (search != null && search.Type != JTokenType.Null && !(search is JObject))
                throw new ArgumentException("The search specification must be a JSON object or null.", "query");
        }
    }
}
