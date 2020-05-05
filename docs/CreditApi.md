# Org.OpenAPITools.Api.CreditApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetCredit**](CreditApi.md#getcredit) | **GET** /credit/{number} | credit for number (interne Nummer)
[**GetCreditParent**](CreditApi.md#getcreditparent) | **GET** /credit/{number}/parent | the parent of a credit with the given document number
[**GetCreditPositions**](CreditApi.md#getcreditpositions) | **GET** /credit/{number}/positions | get outgoing invoice positions for the given number(interne Nummer)
[**GetCredits**](CreditApi.md#getcredits) | **POST** /credits | credits
[**PostCredit**](CreditApi.md#postcredit) | **POST** /credit/{number}/post | Post a credit.



## GetCredit

> void GetCredit (string number, string fields = null)

credit for number (interne Nummer)

Returns the Scopevisio credit with the given number (interne Nummer).  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCreditExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CreditApi(Configuration.Default);
            var number = GS-2019-1;  // string | number (interne Nummer) of a Scopevisio credit
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // credit for number (interne Nummer)
                apiInstance.GetCredit(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CreditApi.GetCredit: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio credit | 
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
| **200** | Returns the Scopevisio credit with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetCreditParent

> void GetCreditParent (string number, string fields = null)

the parent of a credit with the given document number

Returns the Scopevisio outgoing invoice document which is parent of the credit with the given document number.  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCreditParentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CreditApi(Configuration.Default);
            var number = number_example;  // string | document number of a Scopevisio credit
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // the parent of a credit with the given document number
                apiInstance.GetCreditParent(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CreditApi.GetCreditParent: " + e.Message );
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
 **number** | **string**| document number of a Scopevisio credit | 
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
| **200** | Returns the Scopevisio outgoing invoice document which is parent of the credit with the given document number. |  -  |
| **404** | The outgoing invoice was not found or authorization missing.  No parent for the credit found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetCreditPositions

> void GetCreditPositions (string number, string fields = null)

get outgoing invoice positions for the given number(interne Nummer)

Returns the positions for the outgoing invoice with the given number(interne Nummer).  Required Profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCreditPositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CreditApi(Configuration.Default);
            var number = number_example;  // string | number(interne Nummer) of a Scopevisio credit
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get outgoing invoice positions for the given number(interne Nummer)
                apiInstance.GetCreditPositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CreditApi.GetCreditPositions: " + e.Message );
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
 **number** | **string**| number(interne Nummer) of a Scopevisio credit | 
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
| **200** | Returns the positions for the credit with the given number(interne Nummer).  |  -  |
| **404** | No credit with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetCredits

> void GetCredits (string body = null)

credits

Returns the Scopevisio credits. <i> Required profile(s): Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetCreditsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CreditApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // credits
                apiInstance.GetCredits(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CreditApi.GetCredits: " + e.Message );
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
| **200** | Returns the Scopevisio credits. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PostCredit

> void PostCredit (string number)

Post a credit.

Post the credit with the given number(interne Nummer).  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostCreditExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CreditApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio credit.

            try
            {
                // Post a credit.
                apiInstance.PostCredit(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CreditApi.PostCredit: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio credit. | 

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
| **200** | The credit was successfully posted. |  -  |
| **400** | The credit was already posted. |  -  |
| **404** | The credit was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

