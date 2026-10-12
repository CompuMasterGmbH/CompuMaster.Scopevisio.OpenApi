using Newtonsoft.Json.Linq;
using System;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies original Base64 receipt data and an optional filename while retaining complete JSON.</summary>
    /// <remarks>This additive model does not change the existing FileForm. No local file is read, transformed or uploaded during construction.</remarks>
    public class TravelReceiptRequest : JObject
    {
        /// <summary>Initializes an empty receipt whose data string must be assigned before sending.</summary>
        public TravelReceiptRequest() { }

        /// <summary>Initializes a receipt by deeply copying complete JSON.</summary>
        /// <param name="other">The complete receipt object to copy, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source receipt is null.</exception>
        public TravelReceiptRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original optional filename.</summary>
        /// <value>The original string, including empty text, or null to omit it. The manufacturer assumes a PDF when no filename is supplied; the SDK does not fabricate one.</value>
        public string Filename { get { return Value<string>("filename"); } set { if (value == null) Remove("filename"); else this["filename"] = value; } }

        /// <summary>Gets or sets the required original Base64 data string.</summary>
        /// <value>The original string, including empty text, or null to omit it before assignment. The server validates Base64 and file contents; no re-encoding, size limit or MIME type is inferred.</value>
        public string Data { get { return Value<string>("data"); } set { if (value == null) Remove("data"); else this["data"] = value; } }
    }
}
