# CompuMaster.Scopevisio.OpenApi.Api.HumanResourcesApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetHumanResources**](HumanResourcesApi.md#gethumanresources) | **POST** /humanresources | Get human resources
[**GetHumanResourcesYear**](HumanResourcesApi.md#gethumanresourcesyear) | **POST** /humanresourcesAvailability | Get availability of human resources.



## GetHumanResources

> Object GetHumanResources (string body = null)

Get human resources

Get human resources.  Required profile(s): <i>Human-Ressourcen Verwaltung (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetHumanResourcesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new HumanResourcesApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Get human resources
                Object result = apiInstance.GetHumanResources(body);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling HumanResourcesApi.GetHumanResources: " + e.Message );
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

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetHumanResourcesYear

> Object GetHumanResourcesYear (long validFrom, long validTill, string searchSpecification = null)

Get availability of human resources.

Return availability of human resources for the given time range. The both parameters validFrom and validTill specify the start and end time for which the availability of human resources should be retrieved. Required profile(s): <i>Human-Ressourcen Verwaltung (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetHumanResourcesYearExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new HumanResourcesApi(Configuration.Default);
            var validFrom = 789;  // long | validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.
            var validTill = 789;  // long | validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds.
            var searchSpecification = searchSpecification_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. <b>Please consider: the fields \\\"month\\\",\\\"available\\\",\\\"id\\\" are excluded from the search.</b> (optional) 

            try
            {
                // Get availability of human resources.
                Object result = apiInstance.GetHumanResourcesYear(validFrom, validTill, searchSpecification);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling HumanResourcesApi.GetHumanResourcesYear: " + e.Message );
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
 **validFrom** | **long**| validFrom (Planungszeit von). The start of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds. | 
 **validTill** | **long**| validTill (Planungszeit bis). The end of the time range to show the availability of the human resources. It must be provided as an epoch timestamp in milliseconds. | 
 **searchSpecification** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;available\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; | [optional] 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: application/x-www-form-urlencoded
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

