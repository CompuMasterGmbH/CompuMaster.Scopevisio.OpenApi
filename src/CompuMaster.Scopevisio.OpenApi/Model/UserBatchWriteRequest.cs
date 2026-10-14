using System;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Contains a copied current JSON array of user write requests.</summary>
    /// <remarks>Preserves row order and future JSON without default users, identities, profiles, licenses, roles or privileges. The route body is optional; an explicit empty batch remains an empty array. Known fields and assignments are validated on a complete deep snapshot before HTTP. Actual server effects and per-user outcomes remain unverified.</remarks>
    public sealed class UserBatchWriteRequest : JArray
    {
        /// <summary>Initializes an explicitly empty copied user batch.</summary>
        public UserBatchWriteRequest() { }

        /// <summary>Initializes a batch by deeply copying original array JSON.</summary>
        /// <param name="other">The original array, including future row data.</param>
        /// <exception cref="ArgumentNullException">The source array is null.</exception>
        public UserBatchWriteRequest(JArray other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JArray)DeepClone();
            foreach (var item in snapshot)
            {
                if (!(item is JObject row)) throw new ArgumentException("Each user write row must be an original JSON object.", "request");
                UserWriteRequest.Validate(row, creating);
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
