using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using CompuMaster.Scopevisio.OpenApi.Client;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AdditionalApi
    {
        /// <summary>Retrieves the authenticated account context using cancellable HTTP I/O.</summary>
        /// <inheritdoc cref="GetApplicationContextAsyncWithHttpInfo()"/>
        /// <param name="cancellationToken">Cancels the active HTTP request.</param>
        /// <returns>The HTTP response and its deserialized result.</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.AccountInfo>> GetApplicationContextAsyncWithHttpInfo(System.Threading.CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var localVarPath = "/myaccount";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);


            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.Get, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType, cancellationToken).ConfigureAwait(false);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetApplicationContext", localVarResponse);
                if (exception != null) throw exception;
            }

            CompuMaster.Scopevisio.OpenApi.Model.AccountInfo result = Newtonsoft.Json.JsonConvert.DeserializeObject<CompuMaster.Scopevisio.OpenApi.Model.AccountInfo>(localVarResponse.Content);

            return new ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.AccountInfo>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                result);
        }
    }
}
