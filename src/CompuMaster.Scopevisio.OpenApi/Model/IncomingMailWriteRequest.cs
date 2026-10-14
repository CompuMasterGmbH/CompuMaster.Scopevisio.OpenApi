using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied incoming EML import fields.</summary>
    /// <remarks>A supplied object requires original eml text. Typed null setters omit fields without inserting UTF-8 or false. The server may automatically assign matching contacts unless the caller explicitly skips that behavior. Complete JSON is copied; legacy IncomingMailForm behavior is preserved.</remarks>
    public class IncomingMailWriteRequest : JObject
    {
        /// <summary>Initializes an empty incoming-mail request without inserting defaults.</summary>
        public IncomingMailWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete incoming-mail JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public IncomingMailWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original encoding selection.</summary>
        /// <value>The optional original encoding string, including UTF-8 or base64, or null to omit it. The server defaults omission to UTF-8 and validates the selection; the client does not re-encode the EML.</value>
        public string Encoding { get { return Value<string>("encoding"); } set { SetOptional("encoding", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original EML content string.</summary>
        /// <value>The required supplied-object string. With explicit base64 encoding it contains original Base64 EML; otherwise it contains the caller's original text. The client does not parse, normalize, decode or deliver it.</value>
        public string Eml { get { return Value<string>("eml"); } set { SetOptional("eml", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the explicit standard-contact-assignment choice.</summary>
        /// <value>The optional boolean. True asks the server to skip sender/recipient contact matching; false permits standard matching. Null omits the field, preserving the server's false default without local contact lookup or assignment.</value>
        public bool? SkipStandardContactAssignment { get { return Value<bool?>("skipStandardContactAssignment"); } set { SetOptional("skipStandardContactAssignment", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name); else this[name] = value;
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            if (snapshot["eml"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied incoming-mail object requires original EML text.", "request");
            if (snapshot["encoding"] != null && snapshot["encoding"].Type != JTokenType.String)
                throw new ArgumentException("The supplied encoding must be a JSON string.", "request");
            if (snapshot["skipStandardContactAssignment"] != null && snapshot["skipStandardContactAssignment"].Type != JTokenType.Boolean)
                throw new ArgumentException("The supplied contact-assignment flag must be a JSON boolean.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
