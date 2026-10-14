using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains a copied current impersonal-account import request.</summary>
    /// <remarks>The optional body and rows are preserved without inserting chart, account, tax, posting or dimension defaults. Each supplied row requires number, name and type. The server may create or update accounts; complete actual acceptance and reversible ownership remain unverified.</remarks>
    public sealed class ImpersonalAccountImportRequest : JObject
    {
        /// <summary>Initializes an empty copied import request.</summary>
        public ImpersonalAccountImportRequest() { }

        /// <summary>Initializes an import request by deeply copying original JSON.</summary>
        /// <param name="other">The original object, including future fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ImpersonalAccountImportRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the copied optional import rows.</summary>
        /// <value>The optional deeply copied original row array, including an empty array, or null to omit it. Rows preserve account and chart selections without lookup or sorting.</value>
        public JArray Rows
        {
            get { return (JArray)(this["rows"] as JArray)?.DeepClone(); }
            set { if (value == null) Remove("rows"); else this["rows"] = value.DeepClone(); }
        }

        internal string SnapshotForSend()
        {
            var snapshot = (JObject)DeepClone();
            if (snapshot["rows"] != null && !(snapshot["rows"] is JArray))
                throw new ArgumentException("The rows field must be an original JSON array.", "request");
            if (snapshot["rows"] is JArray rows)
                foreach (var item in rows)
                {
                    if (!(item is JObject row)) throw new ArgumentException("Each import row must be an original JSON object.", "request");
                    ImpersonalAccountImportRowRequest.Validate(row);
                }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
