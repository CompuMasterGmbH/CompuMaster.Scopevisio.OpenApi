# Org.OpenAPITools.Api.CashbookApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateCashbookEntry**](CashbookApi.md#createcashbookentry) | **POST** /cashbookentry/new | creates a new cashbook entry
[**GetCashbookSheets**](CashbookApi.md#getcashbooksheets) | **POST** /cashbooksheets | cashbook sheets
[**GetCashbooks**](CashbookApi.md#getcashbooks) | **POST** /cashbooks | cashbooks



## CreateCashbookEntry

> void CreateCashbookEntry (CashbookEntryForm body)

creates a new cashbook entry

Creates a new Scopevisio cashbook entry.  Required profile(s): <i>Kassenbewegung eingeben (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateCashbookEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CashbookApi(Configuration.Default);
            var body = new CashbookEntryForm(); // CashbookEntryForm | content in JSON format, topic is required

            try
            {
                // creates a new cashbook entry
                apiInstance.CreateCashbookEntry(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CashbookApi.CreateCashbookEntry: " + e.Message );
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
 **body** | [**CashbookEntryForm**](CashbookEntryForm.md)| content in JSON format, topic is required | 

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
| **201** | Created |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetCashbookSheets

> void GetCashbookSheets (string body = null)

cashbook sheets

Returns Scopevisio cashbook sheets.  Required profile(s): <i>Abgeschlossene Kassenblätter, Kassenbewegung eingeben (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCashbookSheetsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CashbookApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // cashbook sheets
                apiInstance.GetCashbookSheets(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CashbookApi.GetCashbookSheets: " + e.Message );
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
| **200** | Returns Scopevisio tasks. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetCashbooks

> void GetCashbooks (string body = null)

cashbooks

Returns Scopevisio cashbooks settings.  Required profile(s): <i>Kassenbücher Dashboard (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCashbooksExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CashbookApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // cashbooks
                apiInstance.GetCashbooks(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CashbookApi.GetCashbooks: " + e.Message );
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
| **200** | Returns Scopevisio cashbooks settings. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

