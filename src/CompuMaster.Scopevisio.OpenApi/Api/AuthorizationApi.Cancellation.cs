using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using CompuMaster.Scopevisio.OpenApi.Client;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AuthorizationApi
    {
        /// <summary>Obtains authorization tokens using cancellable HTTP I/O.</summary>
        /// <inheritdoc cref="TokenAsyncWithHttpInfo(string, string, string, string, string, long?, string, string, string, string, string, string)"/>
        /// <param name="cancellationToken">Cancels the active HTTP request.</param>
        /// <returns>The HTTP response and its deserialized result.</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Model.TokenResponse>> TokenAsyncWithHttpInfo(string grantType, string customer, System.Threading.CancellationToken cancellationToken, string clientId = default(string), string clientSecret = default(string), string username = default(string), long? organisationId = default(long?), string organisation = default(string), string password = default(string), string totpResponse = default(string), string refreshToken = default(string), string code = default(string), string requestcookie = default(string))
        {
            cancellationToken.ThrowIfCancellationRequested();
            // verify the required parameter 'grantType' is set
            if (grantType == null)
                throw new ApiException(400, "Missing required parameter 'grantType' when calling AuthorizationApi->Token");
            // verify the required parameter 'customer' is set
            if (customer == null)
                throw new ApiException(400, "Missing required parameter 'customer' when calling AuthorizationApi->Token");

            var localVarPath = "/token";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/x-www-form-urlencoded"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (clientId != null) localVarFormParams.Add("client_id", this.Configuration.ApiClient.ParameterToString(clientId)); // form parameter
            if (clientSecret != null) localVarFormParams.Add("client_secret", this.Configuration.ApiClient.ParameterToString(clientSecret)); // form parameter
            if (grantType != null) localVarFormParams.Add("grant_type", this.Configuration.ApiClient.ParameterToString(grantType)); // form parameter
            if (customer != null) localVarFormParams.Add("customer", this.Configuration.ApiClient.ParameterToString(customer)); // form parameter
            if (username != null) localVarFormParams.Add("username", this.Configuration.ApiClient.ParameterToString(username)); // form parameter
            if (organisationId != null) localVarFormParams.Add("organisation_id", this.Configuration.ApiClient.ParameterToString(organisationId)); // form parameter
            if (organisation != null) localVarFormParams.Add("organisation", this.Configuration.ApiClient.ParameterToString(organisation)); // form parameter
            if (password != null) localVarFormParams.Add("password", this.Configuration.ApiClient.ParameterToString(password)); // form parameter
            if (totpResponse != null) localVarFormParams.Add("totpResponse", this.Configuration.ApiClient.ParameterToString(totpResponse)); // form parameter
            if (refreshToken != null) localVarFormParams.Add("refresh_token", this.Configuration.ApiClient.ParameterToString(refreshToken)); // form parameter
            if (code != null) localVarFormParams.Add("code", this.Configuration.ApiClient.ParameterToString(code)); // form parameter
            if (requestcookie != null) localVarFormParams.Add("requestcookie", this.Configuration.ApiClient.ParameterToString(requestcookie)); // form parameter


            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType, cancellationToken).ConfigureAwait(false);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("Token", localVarResponse);
                if (exception != null) throw exception;
            }

            Model.TokenResponse TokenResult = null;
            if (localVarStatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                //apply token to current configuration instance
                TokenResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.TokenResponse>(localVarResponse.Content);
                this.Configuration.AccessToken = TokenResult.AccessToken;
            }

            return new ApiResponse<Model.TokenResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                TokenResult);
        }
    }
}
