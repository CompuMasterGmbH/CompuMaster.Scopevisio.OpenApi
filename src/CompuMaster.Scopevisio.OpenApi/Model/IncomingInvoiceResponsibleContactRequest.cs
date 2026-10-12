using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicit responsible user-contact selection for an incoming invoice.</summary>
    /// <remarks>Unknown fields and explicit JSON null remain available through the inherited object. The schema requires no individual property; omitted or null values never imply removal in the client.</remarks>
    public class IncomingInvoiceResponsibleContactRequest : JObject
    {
        /// <summary>Initializes an empty request with no inferred contact assignment or removal.</summary>
        public IncomingInvoiceResponsibleContactRequest() { }

        /// <summary>Initializes a request by deeply copying complete JSON.</summary>
        /// <param name="other">The complete object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source request is null.</exception>
        public IncomingInvoiceResponsibleContactRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original responsible user-contact identifier.</summary>
        /// <value>The Int64 user-contact master ID. Zero or negative values explicitly request removal according to the manufacturer. Null removes the field from this request and does not select removal.</value>
        public long? ContactId { get { return Value<long?>("contactId"); } set { if (value.HasValue) this["contactId"] = new JValue(value.Value); else Remove("contactId"); } }
    }
}
