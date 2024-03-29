/* 
 * Scopevisio OpenScope REST API
 *
 * <p>This is an interactive API reference for the Scopevisio REST API.</p><p>Below you will see the main sections of the API. Click each section in order to see the endpoints that are available in that category and use the 'Try it out' button to make API calls.</p><p>For endpoints that require authentication you can click the lock symbol in the top right of the endpoint description, which will take you to a login form.</p><h3>Authentication</h3><p>In order to use the \"Try it out\" function in Swagger you need your own Scopevisio account. If you have not set up a Scopevisio user account already you can do so <a href='https://www.scopevisio.com' target='_blank'>here</a>.</p><h3>Example Application</h3><p>We also have an example application you can reach <a href='../browser/index.html'> here </a>. Usage requires a Scopevisio account.</p><h3>Search Documentation</h3><p>Our documentation regarding <a href='../browser/index.html#!/searchscope'>search specification and paging  </a>.</p><h3>General Documentation</h3><p>Our general documentation can be found <a href='../browser/index.html#!/documentation'> here </a>.</p>
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: support@scopevisio.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using CompuMaster.Scopevisio.OpenApi.Client;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    [Obsolete("Unstable or incomplete implementation", false)]
    public partial class HumanResourcesApi 
    {
        private CompuMaster.Scopevisio.OpenApi.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanResourcesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public HumanResourcesApi(String basePath)
        {
            this.Configuration = new CompuMaster.Scopevisio.OpenApi.Client.Configuration { BasePath = basePath };

            ExceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanResourcesApi"/> class
        /// </summary>
        /// <returns></returns>
        public HumanResourcesApi()
        {
            this.Configuration = CompuMaster.Scopevisio.OpenApi.Client.Configuration.Default;

            ExceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanResourcesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public HumanResourcesApi(CompuMaster.Scopevisio.OpenApi.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = CompuMaster.Scopevisio.OpenApi.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public CompuMaster.Scopevisio.OpenApi.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public CompuMaster.Scopevisio.OpenApi.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        internal IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        internal void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Get human resources Get human resources.  Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. (optional)</param>
        /// <returns>Object</returns>
        public Object GetHumanResources (string body = default(string))
        {
             ApiResponse<Object> localVarResponse = GetHumanResourcesWithHttpInfo(body);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get human resources Get human resources.  Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        public ApiResponse<Object> GetHumanResourcesWithHttpInfo (string body = default(string))
        {

            var localVarPath = "/humanresources";
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
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }

            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetHumanResources", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                (Object) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(Object)));
        }

        /// <summary>
        /// Get human resources Get human resources.  Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. (optional)</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> GetHumanResourcesAsync (string body = default(string))
        {
             ApiResponse<Object> localVarResponse = await GetHumanResourcesAsyncWithHttpInfo(body);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get human resources Get human resources.  Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. (optional)</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> GetHumanResourcesAsyncWithHttpInfo (string body = default(string))
        {

            var localVarPath = "/humanresources";
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
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }

            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetHumanResources", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                (Object) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(Object)));
        }

        /// <summary>
        /// Get availability of human resources. Return availability of human resources for the given time range. The both parameters validFrom and validTill specify the start and end time for which the availability of human resources should be retrieved. Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="validFrom">validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="validTill">validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="searchSpecification">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;available\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; (optional)</param>
        /// <returns>Object</returns>
        public Object GetHumanResourcesYear (long validFrom, long validTill, string searchSpecification = default(string))
        {
             ApiResponse<Object> localVarResponse = GetHumanResourcesYearWithHttpInfo(validFrom, validTill, searchSpecification);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get availability of human resources. Return availability of human resources for the given time range. The both parameters validFrom and validTill specify the start and end time for which the availability of human resources should be retrieved. Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="validFrom">validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="validTill">validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="searchSpecification">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;available\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        public ApiResponse<Object> GetHumanResourcesYearWithHttpInfo (long validFrom, long validTill, string searchSpecification = default(string))
        {
            // verify the required parameter 'validFrom' is set
            if (validFrom == null)
                throw new ApiException(400, "Missing required parameter 'validFrom' when calling HumanResourcesApi->GetHumanResourcesYear");
            // verify the required parameter 'validTill' is set
            if (validTill == null)
                throw new ApiException(400, "Missing required parameter 'validTill' when calling HumanResourcesApi->GetHumanResourcesYear");

            var localVarPath = "/humanresourcesAvailability";
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
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (validFrom != null) localVarFormParams.Add("validFrom", this.Configuration.ApiClient.ParameterToString(validFrom)); // form parameter
            if (validTill != null) localVarFormParams.Add("validTill", this.Configuration.ApiClient.ParameterToString(validTill)); // form parameter
            if (searchSpecification != null) localVarFormParams.Add("searchSpecification", this.Configuration.ApiClient.ParameterToString(searchSpecification)); // form parameter

            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetHumanResourcesYear", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                (Object) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(Object)));
        }

        /// <summary>
        /// Get availability of human resources. Return availability of human resources for the given time range. The both parameters validFrom and validTill specify the start and end time for which the availability of human resources should be retrieved. Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="validFrom">validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="validTill">validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="searchSpecification">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;available\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; (optional)</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> GetHumanResourcesYearAsync (long validFrom, long validTill, string searchSpecification = default(string))
        {
             ApiResponse<Object> localVarResponse = await GetHumanResourcesYearAsyncWithHttpInfo(validFrom, validTill, searchSpecification);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get availability of human resources. Return availability of human resources for the given time range. The both parameters validFrom and validTill specify the start and end time for which the availability of human resources should be retrieved. Required profile(s): &lt;i&gt;Human-Ressourcen Verwaltung (Anzeigen)&lt;/i&gt;.
        /// </summary>
        /// <exception cref="CompuMaster.Scopevisio.OpenApi.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="validFrom">validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="validTill">validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.</param>
        /// <param name="searchSpecification">search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;available\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; (optional)</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> GetHumanResourcesYearAsyncWithHttpInfo (long validFrom, long validTill, string searchSpecification = default(string))
        {
            // verify the required parameter 'validFrom' is set
            if (validFrom == null)
                throw new ApiException(400, "Missing required parameter 'validFrom' when calling HumanResourcesApi->GetHumanResourcesYear");
            // verify the required parameter 'validTill' is set
            if (validTill == null)
                throw new ApiException(400, "Missing required parameter 'validTill' when calling HumanResourcesApi->GetHumanResourcesYear");

            var localVarPath = "/humanresourcesAvailability";
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
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (validFrom != null) localVarFormParams.Add("validFrom", this.Configuration.ApiClient.ParameterToString(validFrom)); // form parameter
            if (validTill != null) localVarFormParams.Add("validTill", this.Configuration.ApiClient.ParameterToString(validTill)); // form parameter
            if (searchSpecification != null) localVarFormParams.Add("searchSpecification", this.Configuration.ApiClient.ParameterToString(searchSpecification)); // form parameter

            // authentication (oauth) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            RestResponse localVarResponse = (RestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.Post, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetHumanResourcesYear", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                localVarResponse.Content,
                (Object) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(Object)));
        }

    }
}
