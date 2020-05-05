# Org.OpenAPITools.Api.OpportunityApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConvertOpportunityToDispatch**](OpportunityApi.md#convertopportunitytodispatch) | **POST** /opportunity/{number}/convertToDispatch | Convert opportunity to dispatch
[**ConvertOpportunityToOffer**](OpportunityApi.md#convertopportunitytooffer) | **POST** /opportunity/{number}/convertToOffer | Convert opportunity to offer
[**ConvertOpportunityToOrder**](OpportunityApi.md#convertopportunitytoorder) | **POST** /opportunity/{number}/convertToOrder | Convert opportunity to order
[**ConvertOpportunityToOutgoingInvoice**](OpportunityApi.md#convertopportunitytooutgoinginvoice) | **POST** /opportunity/{number}/convertToOutgoingInvoice | Convert opportunity to outgoing invoice
[**ConvertOpportunityToRecurringInvoice**](OpportunityApi.md#convertopportunitytorecurringinvoice) | **POST** /opportunity/{number}/convertToRecurringInvoice | Convert opportunity to recurring invoice
[**GetOpportunities**](OpportunityApi.md#getopportunities) | **POST** /opportunities | Opportunities
[**GetOpportunity**](OpportunityApi.md#getopportunity) | **GET** /opportunity/{number} | get opportunity with number (interne Nummer)
[**GetOpportunityPositions**](OpportunityApi.md#getopportunitypositions) | **GET** /opportunity/{number}/positions | get opportunity positions for the given number (interne Nummer)



## ConvertOpportunityToDispatch

> void ConvertOpportunityToDispatch (string number)

Convert opportunity to dispatch

Convert the Scopevisio opportunity with the given number (interne Nummer) to an dispatch.  Required profile(s): <i>Rechnungen, Potenziale (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOpportunityToDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio opportunity

            try
            {
                // Convert opportunity to dispatch
                apiInstance.ConvertOpportunityToDispatch(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.ConvertOpportunityToDispatch: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio opportunity | 

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
| **200** | The opportunity was successfully converted to dispatch. |  -  |
| **404** | The opportunity was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOpportunityToOffer

> void ConvertOpportunityToOffer (string number)

Convert opportunity to offer

Convert the Scopevisio opportunity with the given number (interne Nummer) to an offer.  Required profile(s): <i>Rechnungen, Potenziale (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOpportunityToOfferExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio opportunity

            try
            {
                // Convert opportunity to offer
                apiInstance.ConvertOpportunityToOffer(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.ConvertOpportunityToOffer: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio opportunity | 

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
| **200** | The opportunity was successfully converted to offer. |  -  |
| **404** | The opportunity was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOpportunityToOrder

> void ConvertOpportunityToOrder (string number)

Convert opportunity to order

Convert the Scopevisio opportunity with the given number (interne Nummer) to an order.  Required profile(s): <i>Rechnungen, Potenziale (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOpportunityToOrderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio opportunity

            try
            {
                // Convert opportunity to order
                apiInstance.ConvertOpportunityToOrder(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.ConvertOpportunityToOrder: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio opportunity | 

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
| **200** | The opportunity was successfully converted to order. |  -  |
| **404** | The opportunity was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOpportunityToOutgoingInvoice

> void ConvertOpportunityToOutgoingInvoice (string number)

Convert opportunity to outgoing invoice

Convert the Scopevisio opportunity with the given number (interne Nummer) to an outgoing invoice.  Required profile(s): <i>Rechnungen, Potenziale (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOpportunityToOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio opportunity

            try
            {
                // Convert opportunity to outgoing invoice
                apiInstance.ConvertOpportunityToOutgoingInvoice(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.ConvertOpportunityToOutgoingInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio opportunity | 

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
| **200** | The opportunity was successfully converted to outgoing invoice. |  -  |
| **404** | The opportunity was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOpportunityToRecurringInvoice

> void ConvertOpportunityToRecurringInvoice (string number, ConvertToRecurringInvoiceForm body = null)

Convert opportunity to recurring invoice

Convert the Scopevisio opportunity with the given number (interne Nummer) to a recurring invoice.  Required profile(s): <i>Rechnungen, Potenziale (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOpportunityToRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio opportunity
            var body = new ConvertToRecurringInvoiceForm(); // ConvertToRecurringInvoiceForm |  (optional) 

            try
            {
                // Convert opportunity to recurring invoice
                apiInstance.ConvertOpportunityToRecurringInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.ConvertOpportunityToRecurringInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio opportunity | 
 **body** | [**ConvertToRecurringInvoiceForm**](ConvertToRecurringInvoiceForm.md)|  | [optional] 

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
| **200** | The opportunity was successfully converted to recurring invoice. |  -  |
| **404** | The opportunity was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOpportunities

> void GetOpportunities (string body = null)

Opportunities

Returns all Scopevisio opportunites.  Required profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOpportunitiesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Opportunities
                apiInstance.GetOpportunities(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.GetOpportunities: " + e.Message );
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
| **200** | Returns all Scopevisio opportunities. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOpportunity

> void GetOpportunity (string number, string fields = null)

get opportunity with number (interne Nummer)

Returns the Scopevisio sales potential with the given number (interne Nummer).  Required profile(s): <i>Rechnungen, Potenziale (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOpportunityExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // get opportunity with number (interne Nummer)
                apiInstance.GetOpportunity(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.GetOpportunity: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio outgoing invoice | 
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
| **200** | Returns the Scopevisio opportunity with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOpportunityPositions

> void GetOpportunityPositions (string number, string fields = null)

get opportunity positions for the given number (interne Nummer)

Returns the positions for the opportunity with the given number (interne Nummer).  Required Profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOpportunityPositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OpportunityApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio potential
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get opportunity positions for the given number (interne Nummer)
                apiInstance.GetOpportunityPositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OpportunityApi.GetOpportunityPositions: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio potential | 
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
| **200** | Returns the positions for the opportunity with the given number (interne Nummer).  |  -  |
| **404** | No offer with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

