# Org.OpenAPITools.Api.DispatchApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetDispatch**](DispatchApi.md#getdispatch) | **GET** /dispatch/{number} | get dispatch with number (interne Nummer)
[**GetDispatchParent**](DispatchApi.md#getdispatchparent) | **GET** /dispatch/{number}/parent | the parent of the dispatch with the given document number
[**GetDispatchPositions**](DispatchApi.md#getdispatchpositions) | **GET** /dispatch/{number}/positions | get the positions for the dispatch with the given number (interne Nummer)
[**GetDispatches**](DispatchApi.md#getdispatches) | **POST** /dispatches | Dispatches



## GetDispatch

> void GetDispatch (string number, string fields = null)

get dispatch with number (interne Nummer)

Returns the Scopevisio dispatch with the given number (interne Nummer).  Required profile(s) : <i>Lieferscheine (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DispatchApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio dispatch
            var fields = id,documentNumber,name;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // get dispatch with number (interne Nummer)
                apiInstance.GetDispatch(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DispatchApi.GetDispatch: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **number** | **string**| number (interne Nummer) of a Scopevisio dispatch | 
 **fields** | **string**| comma separated list of fields to show in response  id, documentNumber | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the Scopevisio dispatch with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetDispatchParent

> void GetDispatchParent (string number, string fields = null)

the parent of the dispatch with the given document number

Returns the Scopevisio outgoing invoice document which is parent of the dispatch with the given document number.  Required profile(s): <i>Lieferscheine (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDispatchParentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DispatchApi(Configuration.Default);
            var number = number_example;  // string | document number of a Scopevisio dispatch
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // the parent of the dispatch with the given document number
                apiInstance.GetDispatchParent(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DispatchApi.GetDispatchParent: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **number** | **string**| document number of a Scopevisio dispatch | 
 **fields** | **string**| comma separated list of fields to show in response  id, documentNumber | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the Scopevisio outgoing invoice document which is parent of the dispatch with the given document number. |  -  |
| **404** | The dispatch was not found or authorization missing.  No parent for the dispatch found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetDispatchPositions

> void GetDispatchPositions (string number, string fields = null)

get the positions for the dispatch with the given number (interne Nummer)

Returns the positions of the dispatch with the given number (interne Nummer).  Required profile(s): <i>Lieferscheine (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDispatchPositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DispatchApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio dispatch
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get the positions for the dispatch with the given number (interne Nummer)
                apiInstance.GetDispatchPositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DispatchApi.GetDispatchPositions: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **number** | **string**| number (interne Nummer) of a Scopevisio dispatch | 
 **fields** | **string**| comma separated list of fields to show in response  id,name | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the positions of the dispatch with the given number (interne Nummer).  |  -  |
| **404** | No dispatch with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetDispatches

> void GetDispatches (string body = null)

Dispatches

Returns all Scopevisio dispatches.  Required profile(s): <i>Lieferscheine (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDispatchesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DispatchApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Dispatches
                apiInstance.GetDispatches(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DispatchApi.GetDispatches: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns all Scopevisio dispatches. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

