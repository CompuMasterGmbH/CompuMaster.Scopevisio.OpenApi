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
        /// <summary>Retrieves the complete buffered response of an explicitly selected Teamwork bridge resource.</summary>
        /// <param name="request">The copied one-to-three-segment resource selection and original ordered query pairs.</param>
        /// <returns>The complete buffered entity bytes, an empty array for an empty entity, or null if the transport supplies no bytes.</returns>
        /// <remarks>The existing Scopevisio OAuth token authorizes the bridge; CenterDevice resource permissions remain server-authoritative. The manufacturer declares no fixed success representation for these routes. Content is buffered by the existing transport without JSON, text, URI or file parsing. The caller owns any later interpretation and storage. Transport content decoding can apply; these bytes do not represent literal network packets. No account selection, child navigation, retry or token installation is added.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public byte[] GetTeamworkResource(TeamworkBridgeReadRequest request)
            => GetTeamworkResourceWithHttpInfo(request).Data;

        /// <inheritdoc cref="GetTeamworkResource"/>
        /// <returns>The buffered entity bytes with status and combined original response headers; RawJsonContent is null.</returns>
        public ApiResponse<byte[]> GetTeamworkResourceWithHttpInfo(TeamworkBridgeReadRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = Configuration.ApiClient.CallApi(BridgeReadPath("/teamworkbridge/{arg1}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream");
                    break;
                case 2:
                    response = Configuration.ApiClient.CallApi(BridgeReadPath("/teamworkbridge/{arg1}/{arg2}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream");
                    break;
                case 3:
                    response = Configuration.ApiClient.CallApi(BridgeReadPath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream");
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("GetTeamworkResource", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTeamworkResource"/>
        public Task<byte[]> GetTeamworkResourceAsync(TeamworkBridgeReadRequest request)
            => GetTeamworkResourceAsync(request, CancellationToken.None);

        /// <inheritdoc cref="GetTeamworkResource"/>
        /// <param name="request">The copied resource selection and original query pairs.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<byte[]> GetTeamworkResourceAsync(TeamworkBridgeReadRequest request, CancellationToken cancellationToken)
            => (await GetTeamworkResourceAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;

        /// <inheritdoc cref="GetTeamworkResourceWithHttpInfo"/>
        public Task<ApiResponse<byte[]>> GetTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeReadRequest request)
            => GetTeamworkResourceAsyncWithHttpInfo(request, CancellationToken.None);

        /// <inheritdoc cref="GetTeamworkResourceWithHttpInfo"/>
        /// <param name="request">The copied resource selection and original query pairs.</param>
        /// <param name="cancellationToken">Cancels active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<byte[]>> GetTeamworkResourceAsyncWithHttpInfo(TeamworkBridgeReadRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request == null) throw new ArgumentNullException(nameof(request));
            var query = new List<KeyValuePair<string, string>>(request.Query);
            var headers = BinaryFileResponse.Headers(Configuration);
            var paths = new Dictionary<string, string>();
            var forms = new Dictionary<string, string>();
            var files = new Dictionary<string, FileParameter>();
            RestResponse response;
            switch (request.PathSegments.Count)
            {
                case 1:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeReadPath("/teamworkbridge/{arg1}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 2:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeReadPath("/teamworkbridge/{arg1}/{arg2}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                case 3:
                    response = await Configuration.ApiClient.CallApiAsync(BridgeReadPath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Get,
                        query, null, headers, forms, files, paths, "application/octet-stream", cancellationToken).ConfigureAwait(false);
                    break;
                default: throw new ArgumentException("An unsupported resource selection was supplied.", nameof(request));
            }
            return BinaryFileResponse.Parse("GetTeamworkResource", response, ExceptionFactory);
        }

        private static string BridgeReadPath(string path, TeamworkBridgeReadRequest request)
        {
            for (var index = 0; index < request.PathSegments.Count; index++)
                path = path.Replace("{arg" + (index + 1) + "}", Uri.EscapeDataString(request.PathSegments[index]));
            return path;
        }
    }
}
