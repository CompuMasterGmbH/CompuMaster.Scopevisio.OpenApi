using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current journal or outgoing-invoice file data.</summary>
    /// <remarks>A supplied object requires an original Base64 data string. Typed null setters omit fields; known explicit nulls are rejected before sending. Complete JSON is copied without filename, PDF, content-type or document defaults. Legacy FileForm APIs remain unchanged.</remarks>
    public class FileWriteRequest : JObject
    {
        /// <summary>Initializes an empty file request without inserting defaults.</summary>
        public FileWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete original file JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public FileWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original filename.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server assumes PDF when omitted; the client does not choose a filename or extension.</value>
        public string Filename { get { return Value<string>("filename"); } set { SetOptional("filename", value); } }

        /// <summary>Gets or sets the original Base64 data string.</summary>
        /// <value>The required supplied-object string, or null to omit it while constructing the request. Text, whitespace and empty strings are not repaired, decoded or re-encoded; the server validates content.</value>
        public string Data { get { return Value<string>("data"); } set { SetOptional("data", value); } }

        /// <summary>Creates a file request by copying and encoding the original bytes as Base64.</summary>
        /// <param name="data">The original byte array, including an explicitly empty array.</param>
        /// <param name="filename">The optional original filename, or null to omit it.</param>
        /// <returns>A new request with an immutable encoded byte snapshot, without file-system access, text conversion or content inference.</returns>
        /// <exception cref="ArgumentNullException">The byte array is null.</exception>
        public static FileWriteRequest FromBytes(byte[] data, string filename = null)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            return new FileWriteRequest { Data = Convert.ToBase64String(data), Filename = filename };
        }

        private void SetOptional(string name, string value)
        {
            if (value == null) Remove(name);
            else this[name] = new JValue(value);
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            if (snapshot["data"]?.Type != JTokenType.String)
                throw new ArgumentException("A supplied file object requires an original Base64 data string.", "request");
            if (snapshot["filename"] != null && snapshot["filename"].Type != JTokenType.String)
                throw new ArgumentException("The supplied filename must be a JSON string.", "request");
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
