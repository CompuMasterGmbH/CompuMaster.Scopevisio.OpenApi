using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies outgoing-invoice search choices and explicit individual number-range inclusion.</summary>
    /// <remarks>All inherited search and unknown fields remain available. No number-range, account or paging default is selected.</remarks>
    public class OutgoingInvoiceSearchRequest : SearchRequest
    {
        /// <summary>Initializes an empty search without inserting defaults.</summary>
        public OutgoingInvoiceSearchRequest() { }
        /// <summary>Initializes a search by deeply copying complete original JSON.</summary>
        /// <param name="other">The complete source object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public OutgoingInvoiceSearchRequest(JObject other) : base(other) { }
        /// <summary>Gets or sets whether invoices from individual number ranges are included.</summary>
        /// <value>The explicit choice, or null to omit it and retain the server default of false.</value>
        public bool? IncludeIndividualNumberRanges
        {
            get { return Value<bool?>("includeIndividualNumberRanges"); }
            set { if (value.HasValue) this["includeIndividualNumberRanges"] = value.Value; else Remove("includeIndividualNumberRanges"); }
        }
    }
}
