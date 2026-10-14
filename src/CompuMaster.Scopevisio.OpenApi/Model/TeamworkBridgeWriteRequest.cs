using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies a copied Teamwork bridge mutation resource, query and optional opaque payload.</summary>
    /// <remarks>The existing Scopevisio OAuth context authorizes the bridge. The target endpoint defines body, query, access and mutation semantics. This request does not select or establish tenant identity.</remarks>
    public sealed class TeamworkBridgeWriteRequest
    {
        private readonly byte[] body;

        /// <summary>Initializes a mutation selection by copying its segments, query pairs and optional payload.</summary>
        /// <param name="pathSegments">One to four nonblank original resource segments without dot segments. POST workflows accept at most three.</param>
        /// <param name="query">Original ordered query pairs, including repeated keys and empty values, or null to omit the query. Keys must be nonblank and values nonnull.</param>
        /// <param name="body">Original entity bytes, including an explicitly empty array, or null to omit the entity. Bytes are copied without serialization or format inference.</param>
        /// <param name="contentType">The explicit valid media type when bytes are supplied, including any caller-owned multipart boundary. Must be null when body is null; original spelling is retained.</param>
        /// <exception cref="ArgumentNullException">The path selection is null.</exception>
        /// <exception cref="ArgumentException">Segments or query pairs are invalid, or body and media type are inconsistent, or the media type is invalid or contains a line break.</exception>
        public TeamworkBridgeWriteRequest(IEnumerable<string> pathSegments, IEnumerable<KeyValuePair<string, string>> query = null, byte[] body = null, string contentType = null)
        {
            if (pathSegments == null) throw new ArgumentNullException(nameof(pathSegments));
            var paths = pathSegments.ToArray();
            if (paths.Length < 1 || paths.Length > 4 || paths.Any(x => string.IsNullOrWhiteSpace(x) || x == "." || x == ".."))
                throw new ArgumentException("One to four nonblank resource segments without dot segments are required.", nameof(pathSegments));
            var pairs = query == null ? new KeyValuePair<string, string>[0] : query.ToArray();
            if (pairs.Any(x => string.IsNullOrWhiteSpace(x.Key) || x.Value == null))
                throw new ArgumentException("Query keys must be nonblank and values must not be null.", nameof(query));
            if (body == null && contentType != null)
                throw new ArgumentException("A content type requires an explicitly supplied body.", nameof(contentType));
            if (body != null && (string.IsNullOrWhiteSpace(contentType) || contentType.IndexOfAny(new[] { '\r', '\n' }) >= 0 || !MediaTypeHeaderValue.TryParse(contentType, out var parsedType)))
                throw new ArgumentException("An explicit valid content type without line breaks is required for a supplied body.", nameof(contentType));
            PathSegments = Array.AsReadOnly(paths);
            Query = Array.AsReadOnly(pairs);
            this.body = body == null ? null : (byte[])body.Clone();
            ContentType = contentType;
        }

        /// <summary>Gets the copied original resource segments in their original order.</summary>
        /// <remarks>The transport escapes each segment independently; embedded slashes do not add resource segments.</remarks>
        public IReadOnlyList<string> PathSegments { get; }

        /// <summary>Gets copied original ordered query pairs, preserving repeated keys and empty values.</summary>
        public IReadOnlyList<KeyValuePair<string, string>> Query { get; }

        /// <summary>Gets a fresh copy of the original entity bytes, or null when the entity is omitted.</summary>
        public byte[] Body => body == null ? null : (byte[])body.Clone();

        /// <summary>Gets the explicitly supplied original media type, or null when the entity is omitted.</summary>
        public string ContentType { get; }
    }
}
