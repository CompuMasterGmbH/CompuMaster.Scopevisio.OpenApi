using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ContactApi
    {
        /// <summary>
        /// Retrieves contacts using a JSON search filter with cancellation support.
        /// </summary>
        /// <param name="body">The complete JSON search object, or null to omit the optional filter. The supplied JSON is sent unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <returns>The HTTP response and typed contact records.</returns>
        /// <remarks>
        /// Paging, selected fields and formatting are specified in the search object.
        /// The account requires permission to view contacts. Cancellation does not change the client's authorization state.
        /// </remarks>
        /// <exception cref="OperationCanceledException">The cancellation token is canceled before or during the request.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the server response.</exception>
        public async Task<ApiResponse<RecordCollection<Contact>>> GetContactsAsyncWithHttpInfo(string body, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contacts";
            var localVarHeaderParams = new Dictionary<string, string>(Configuration.DefaultHeader);
            if (!string.IsNullOrEmpty(Configuration.AccessToken))
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;

            // This API accepts a pre-serialized search object, as does the synchronous entry point.
            // Serializing body again would turn the object into a quoted JSON string.
            RestResponse response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, localVarHeaderParams,
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetContacts", response);
                if (exception != null) throw exception;
            }

            var records = Newtonsoft.Json.JsonConvert.DeserializeObject<Records<JObject>>(response.Content);
            var contacts = new RecordCollection<Contact>(records);
            return new ApiResponse<RecordCollection<Contact>>((int)response.StatusCode,
                SearchJsonResponse.ResponseHeaders(response),
                response.Content, contacts);
        }
    }
}
