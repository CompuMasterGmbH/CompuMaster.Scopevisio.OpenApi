using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies a copied Teamwork bridge resource and its original query pairs.</summary>
    /// <remarks>The bridge uses the existing Scopevisio OAuth context. Endpoint names and query semantics remain server-authoritative.</remarks>
    public sealed class TeamworkBridgeReadRequest
    {
        /// <summary>Initializes a resource selection by copying every segment and query pair.</summary>
        /// <param name="pathSegments">One to three nonblank resource segments in original order and spelling; dot segments are invalid.</param>
        /// <param name="query">Original ordered query pairs, including repeated keys and empty strings, or null to omit the query. Keys must be nonblank; values must not be null.</param>
        /// <exception cref="ArgumentNullException">The path selection is null.</exception>
        /// <exception cref="ArgumentException">The selection has an invalid count or segment, or a query key is blank or null, or a value is null.</exception>
        public TeamworkBridgeReadRequest(IEnumerable<string> pathSegments, IEnumerable<KeyValuePair<string, string>> query = null)
        {
            if (pathSegments == null) throw new ArgumentNullException(nameof(pathSegments));
            var paths = pathSegments.ToArray();
            if (paths.Length < 1 || paths.Length > 3 || paths.Any(x => string.IsNullOrWhiteSpace(x) || x == "." || x == ".."))
                throw new ArgumentException("One to three nonblank resource segments without dot segments are required.", nameof(pathSegments));
            var pairs = query == null ? new KeyValuePair<string, string>[0] : query.ToArray();
            if (pairs.Any(x => string.IsNullOrWhiteSpace(x.Key) || x.Value == null))
                throw new ArgumentException("Query keys must be nonblank and values must not be null.", nameof(query));
            PathSegments = Array.AsReadOnly(paths);
            Query = Array.AsReadOnly(pairs);
        }

        /// <summary>Gets the copied resource segments in their original order and spelling.</summary>
        /// <remarks>The transport escapes each segment independently; embedded slashes are not additional resource segments.</remarks>
        public IReadOnlyList<string> PathSegments { get; }

        /// <summary>Gets the copied query pairs in original order, preserving repeated keys and empty strings.</summary>
        public IReadOnlyList<KeyValuePair<string, string>> Query { get; }
    }
}
