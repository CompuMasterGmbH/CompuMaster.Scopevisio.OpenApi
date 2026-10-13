using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied values for creating a contact, project or task comment.</summary>
    /// <remarks>The complete JSON object is deeply copied before sending. Unknown fields remain available. Typed null setters omit known fields; explicitly supplied known fields must be strings. Omission does not insert server defaults or example text.</remarks>
    public class CommentCreationRequest : JObject
    {
        /// <summary>Initializes an empty comment request without category or text defaults.</summary>
        public CommentCreationRequest() { }

        /// <summary>Initializes a request by deeply copying a complete JSON object.</summary>
        /// <param name="other">The source object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public CommentCreationRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original comment category.</summary>
        /// <value>Notiz, Anruf, Besprechung, Vertrag or E-mail, with exact spelling; null omits the field and retains the server default Notiz.</value>
        public string Category { get { return Value<string>("category"); } set { SetOptional("category", value); } }

        /// <summary>Gets or sets the original comment text.</summary>
        /// <value>The optional text, including empty or whitespace text, or null to omit the field. Text and markup are sent without conversion.</value>
        public string Body { get { return Value<string>("body"); } set { SetOptional("body", value); } }

        private void SetOptional(string name, string value)
        {
            if (value == null) Remove(name);
            else this[name] = new JValue(value);
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "category", "body" })
            {
                var value = snapshot[name];
                if (value != null && value.Type != JTokenType.String)
                    throw new ArgumentException("The " + name + " field must be a JSON string when supplied.", "request");
            }
            var category = snapshot["category"]?.Value<string>();
            if (category != null && category != "Notiz" && category != "Anruf" && category != "Besprechung" && category != "Vertrag" && category != "E-mail")
                throw new ArgumentException("The category field must match a documented comment category exactly.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
