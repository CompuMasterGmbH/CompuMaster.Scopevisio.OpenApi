using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class TeamworkApi
    {
        /// <summary>Sends an explicitly selected Teamwork bridge DELETE mutation and retains its complete buffered response.</summary>
        /// <param name="request">The copied one-to-4-segment resource selection, original query pairs and optional explicit entity bytes and media type.</param>
        /// <returns>The complete buffered entity bytes, an empty array for an empty entity, or null if the transport supplies no bytes.</returns>
        /// <remarks>The existing Scopevisio OAuth context is used without account selection, lookup, token installation or replay. OpenScope declares no request body or fixed success representation for these bridge routes; explicit optional payload forwarding follows the delegated endpoint contract and remains unverified on actual servers. The caller supplies any JSON, form or multipart encoding and boundary. Response content decoding can apply; no JSON, text, URI or file interpretation is added. Target permissions, mutation semantics and cleanup remain server-authoritative. Cancellation can leave an already applied mutation; tenant scope and owned cleanup must be established separately.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">The resource segment count is unsupported by this workflow.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] RemoveTeamworkResource(TeamworkBridgeWriteRequest request)
            => RemoveTeamworkResourceWithHttpInfo(request).Data;

        /// <inheritdoc cref="RemoveTeamworkResource"/>
        /// <returns>The complete buffered bytes with status and combined original response headers; RawJsonContent is null.</returns>
        public ApiResponse<byte[]> RemoveTeamworkResourceWithHttpInfo(TeamworkBridgeWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 4) throw new ArgumentException("This workflow supports at most 4 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 2:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 3:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 4:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("RemoveTeamworkResource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="RemoveTeamworkResource"/>
        public Task<byte[]> RemoveTeamworkResourceAsync(TeamworkBridgeWriteRequest request)
            => RemoveTeamworkResourceAsync(request, CancellationToken.None);

        /// <inheritdoc cref="RemoveTeamworkResource"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> RemoveTeamworkResourceAsync(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
            => (await RemoveTeamworkResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;

        /// <inheritdoc cref="RemoveTeamworkResourceWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> RemoveTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request)
            => RemoveTeamworkResourceAsyncWithHttpInfo(request, CancellationToken.None);

        /// <inheritdoc cref="RemoveTeamworkResourceWithHttpInfo"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> RemoveTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 4) throw new ArgumentException("This workflow supports at most 4 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 2:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 3:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 4:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4}", request), Method.Delete,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("RemoveTeamworkResource", response, ExceptionFactory);
        }

        /// <summary>Sends an explicitly selected Teamwork bridge PUT mutation and retains its complete buffered response.</summary>
        /// <param name="request">The copied one-to-4-segment resource selection, original query pairs and optional explicit entity bytes and media type.</param>
        /// <returns>The complete buffered entity bytes, an empty array for an empty entity, or null if the transport supplies no bytes.</returns>
        /// <remarks>The existing Scopevisio OAuth context is used without account selection, lookup, token installation or replay. OpenScope declares no request body or fixed success representation for these bridge routes; explicit optional payload forwarding follows the delegated endpoint contract and remains unverified on actual servers. The caller supplies any JSON, form or multipart encoding and boundary. Response content decoding can apply; no JSON, text, URI or file interpretation is added. Target permissions, mutation semantics and cleanup remain server-authoritative. Cancellation can leave an already applied mutation; tenant scope and owned cleanup must be established separately.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">The resource segment count is unsupported by this workflow.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] PutTeamworkResource(TeamworkBridgeWriteRequest request)
            => PutTeamworkResourceWithHttpInfo(request).Data;

        /// <inheritdoc cref="PutTeamworkResource"/>
        /// <returns>The complete buffered bytes with status and combined original response headers; RawJsonContent is null.</returns>
        public ApiResponse<byte[]> PutTeamworkResourceWithHttpInfo(TeamworkBridgeWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 4) throw new ArgumentException("This workflow supports at most 4 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 2:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 3:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 4:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("PutTeamworkResource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="PutTeamworkResource"/>
        public Task<byte[]> PutTeamworkResourceAsync(TeamworkBridgeWriteRequest request)
            => PutTeamworkResourceAsync(request, CancellationToken.None);

        /// <inheritdoc cref="PutTeamworkResource"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> PutTeamworkResourceAsync(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
            => (await PutTeamworkResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;

        /// <inheritdoc cref="PutTeamworkResourceWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> PutTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request)
            => PutTeamworkResourceAsyncWithHttpInfo(request, CancellationToken.None);

        /// <inheritdoc cref="PutTeamworkResourceWithHttpInfo"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> PutTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 4) throw new ArgumentException("This workflow supports at most 4 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 2:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 3:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 4:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4}", request), Method.Put,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("PutTeamworkResource", response, ExceptionFactory);
        }

        /// <summary>Sends an explicitly selected Teamwork bridge POST mutation and retains its complete buffered response.</summary>
        /// <param name="request">The copied one-to-3-segment resource selection, original query pairs and optional explicit entity bytes and media type.</param>
        /// <returns>The complete buffered entity bytes, an empty array for an empty entity, or null if the transport supplies no bytes.</returns>
        /// <remarks>The existing Scopevisio OAuth context is used without account selection, lookup, token installation or replay. OpenScope declares no request body or fixed success representation for these bridge routes; explicit optional payload forwarding follows the delegated endpoint contract and remains unverified on actual servers. The caller supplies any JSON, form or multipart encoding and boundary. Response content decoding can apply; no JSON, text, URI or file interpretation is added. Target permissions, mutation semantics and cleanup remain server-authoritative. Cancellation can leave an already applied mutation; tenant scope and owned cleanup must be established separately.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">The resource segment count is unsupported by this workflow.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] PostTeamworkResource(TeamworkBridgeWriteRequest request)
            => PostTeamworkResourceWithHttpInfo(request).Data;

        /// <inheritdoc cref="PostTeamworkResource"/>
        /// <returns>The complete buffered bytes with status and combined original response headers; RawJsonContent is null.</returns>
        public ApiResponse<byte[]> PostTeamworkResourceWithHttpInfo(TeamworkBridgeWriteRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 3) throw new ArgumentException("This workflow supports at most 3 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 2:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                case 3:
                    response = Configuration.ApiClient.CallApi(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream");
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("PostTeamworkResource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="PostTeamworkResource"/>
        public Task<byte[]> PostTeamworkResourceAsync(TeamworkBridgeWriteRequest request)
            => PostTeamworkResourceAsync(request, CancellationToken.None);

        /// <inheritdoc cref="PostTeamworkResource"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> PostTeamworkResourceAsync(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
            => (await PostTeamworkResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;

        /// <inheritdoc cref="PostTeamworkResourceWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> PostTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request)
            => PostTeamworkResourceAsyncWithHttpInfo(request, CancellationToken.None);

        /// <inheritdoc cref="PostTeamworkResourceWithHttpInfo"/>
        /// <param name="request">The copied resource, query and optional payload.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O; an already applied mutation may remain.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> PostTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PathSegments.Count > 3) throw new ArgumentException("This workflow supports at most 3 resource segments.", nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var body = request.Body;
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 2:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 3:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Post,
                        query, body, headers, forms, files, paths, request.ContentType ?? "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("PostTeamworkResource", response, ExceptionFactory);
        }

        private static string BridgeWritePath(string path, TeamworkBridgeWriteRequest request)
        {
            for (var index = 0; index < request.PathSegments.Count; index++)
                path = path.Replace("{arg" + (index + 1) + "}", Uri.EscapeDataString(request.PathSegments[index]));
            return path;
        }
    }
}
