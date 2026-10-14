using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies an explicitly selected existing mail for assignment.</summary>
    /// <remarks>A supplied original mailId is required and is distinct from contact, project or task target IDs. Complete JSON is copied without mail selection, lookup or a zero default. Legacy AssignMailForm behavior is preserved.</remarks>
    public class MailAssignmentWriteRequest : JObject
    {
        /// <summary>Initializes an empty assignment request without choosing a mail.</summary>
        public MailAssignmentWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete assignment JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public MailAssignmentWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the explicitly selected existing mail identifier.</summary>
        /// <value>The original nullable Int64 mailId. Creation of the assignment requires a supplied value; null omits it while constructing the request. The client never selects a default mail or derives it from the target.</value>
        public long? MailId { get { return Value<long?>("mailId"); } set { if (!value.HasValue) Remove("mailId"); else this["mailId"] = new JValue(value.Value); } }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            var id = snapshot["mailId"]; long value;
            if (id?.Type != JTokenType.Integer || !long.TryParse(id.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
                throw new ArgumentException("Mail assignment requires an explicit JSON Int64 mailId.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
