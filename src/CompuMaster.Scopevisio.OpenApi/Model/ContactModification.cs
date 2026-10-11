using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Represents explicitly selected Contact fields to modify.</summary>
    public class ContactModification : JObject
    {
        /// <summary>Initializes an empty modification without changing existing field values.</summary>
        public ContactModification() { }

        /// <summary>Initializes a modification by deeply copying a JSON object.</summary>
        /// <param name="other">The field map to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source field map is null.</exception>
        public ContactModification(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the Contact birth date to assign or clear.</summary>
        /// <value>A date in dd.MM.yyyy format; an empty string clears it. Setting null omits the field and retains its current value.</value>
        /// <remarks>The HTTP methods validate the raw field as a valid date string or explicit empty string; JSON null is rejected.</remarks>
        public string BirthDate
        {
            get { return Value<string>("birthDate"); }
            set { if (value == null) Remove("birthDate"); else this["birthDate"] = value; }
        }
    }
}
