using CompuMaster.Scopevisio.OpenApi.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Searches projects with native cancellation and preserves the legacy response contract.</summary>
        /// <param name="body">The already serialized JSON search object, or null to omit the optional body; text is sent unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <returns>The status, headers and exact raw response with null legacy Data.</returns>
        /// <remarks>Uses the existing configuration, authorization and exception hook. Use SearchProjects for complete parsed JSON data. Cancellation does not change authorization state or add retries.</remarks>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public async Task<ApiResponse<Object>> GetProjectsAsyncWithHttpInfo(string body, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/projects";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            localVarPostBody = body; // Already serialized JSON search text.

            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType, cancellationToken).ConfigureAwait(false);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetProjects", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                null);
        }
        /// <summary>Searches projects asynchronously with native cancellation while discarding the legacy result.</summary>
        /// <param name="body">The already serialized JSON search object, or null to omit the optional body; text is sent unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <returns>A task completing when the request finishes.</returns>
        /// <remarks>Preserves the existing void/Task behavior. Use SearchProjects for complete parsed JSON results.</remarks>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        public async Task GetProjectsAsync(string body, CancellationToken cancellationToken)
        {
            await GetProjectsAsyncWithHttpInfo(body, cancellationToken).ConfigureAwait(false);
        }
    }
}
