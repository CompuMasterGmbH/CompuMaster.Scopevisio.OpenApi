# Org.OpenAPITools.Api.OrderApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConvertOrderToDispatch**](OrderApi.md#convertordertodispatch) | **POST** /order/{number}/convertToDispatch | Convert order to Dispatch
[**ConvertOrderToOffer**](OrderApi.md#convertordertooffer) | **POST** /order/{number}/convertToOffer | Convert order to offer
[**ConvertOrderToOpportunity**](OrderApi.md#convertordertoopportunity) | **POST** /order/{number}/convertToOpportunity | Convert order to opportunity
[**ConvertOrderToOutgoingInvoice**](OrderApi.md#convertordertooutgoinginvoice) | **POST** /order/{number}/convertToOutgoingInvoice | Convert order to outgoing invoice
[**ConvertOrderToRecurringInvoice**](OrderApi.md#convertordertorecurringinvoice) | **POST** /order/{number}/convertToRecurringInvoice | Convert order to recurring invoice
[**GetOrder**](OrderApi.md#getorder) | **GET** /order/{number} | get order with number (interne Nummer)
[**GetOrderParent**](OrderApi.md#getorderparent) | **GET** /order/{number}/parent | the parent of an order with the given document number
[**GetOrderPositions**](OrderApi.md#getorderpositions) | **GET** /order/{number}/positions | get the positions for the order with the given number (interne Nummer)
[**GetOrders**](OrderApi.md#getorders) | **POST** /orders | Orders



## ConvertOrderToDispatch

> void ConvertOrderToDispatch (string number)

Convert order to Dispatch

Convert the Scopevisio order with the given number (interne Nummer) to an dispatch.  Required profile(s): <i>Aufträge (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOrderToDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order

            try
            {
                // Convert order to Dispatch
                apiInstance.ConvertOrderToDispatch(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.ConvertOrderToDispatch: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 

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
| **200** | The order was successfully converted to Dispatch. |  -  |
| **404** | The order was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOrderToOffer

> void ConvertOrderToOffer (string number)

Convert order to offer

Convert the Scopevisio order with the given number (interne Nummer) to an offer.  Required profile(s): <i>Aufträge (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOrderToOfferExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order

            try
            {
                // Convert order to offer
                apiInstance.ConvertOrderToOffer(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.ConvertOrderToOffer: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 

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
| **200** | The order was successfully converted to offer. |  -  |
| **404** | The order was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOrderToOpportunity

> void ConvertOrderToOpportunity (string number)

Convert order to opportunity

Convert the Scopevisio order with the given number (interne Nummer) to an opportunity.  Required profile(s): <i>Aufträge (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOrderToOpportunityExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order

            try
            {
                // Convert order to opportunity
                apiInstance.ConvertOrderToOpportunity(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.ConvertOrderToOpportunity: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 

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
| **200** | The order was successfully converted to opportunity. |  -  |
| **404** | The order was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOrderToOutgoingInvoice

> void ConvertOrderToOutgoingInvoice (string number)

Convert order to outgoing invoice

Convert the Scopevisio order with the given number (interne Nummer) to an outgoing invoice.  Required profile(s): <i>Aufträge (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOrderToOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order

            try
            {
                // Convert order to outgoing invoice
                apiInstance.ConvertOrderToOutgoingInvoice(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.ConvertOrderToOutgoingInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 

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
| **200** | The order was successfully converted to outgoing invoice. |  -  |
| **404** | The order was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOrderToRecurringInvoice

> void ConvertOrderToRecurringInvoice (string number, ConvertToRecurringInvoiceForm body = null)

Convert order to recurring invoice

Convert the Scopevisio order with the given number (interne Nummer) to a recurring invoice.  Required profile(s): <i>Aufträge (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertOrderToRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order
            var body = new ConvertToRecurringInvoiceForm(); // ConvertToRecurringInvoiceForm |  (optional) 

            try
            {
                // Convert order to recurring invoice
                apiInstance.ConvertOrderToRecurringInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.ConvertOrderToRecurringInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 
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
| **200** | The order was successfully converted to recurring invoice. |  -  |
| **404** | The order was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOrder

> void GetOrder (string number, string fields = null)

get order with number (interne Nummer)

Returns the Scopevisio order with the given number (interne Nummer).  Required profile(s) : <i>Aufträge (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOrderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio order
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // get order with number (interne Nummer)
                apiInstance.GetOrder(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.GetOrder: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio order | 
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
| **200** | Returns the Scopevisio order with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOrderParent

> void GetOrderParent (string number, string fields = null)

the parent of an order with the given document number

Returns the Scopevisio outgoing invoice document which is parent of the order with the given document number.  Required profile(s): <i>Aufträge (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOrderParentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | document number of a Scopevisio offer
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // the parent of an order with the given document number
                apiInstance.GetOrderParent(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.GetOrderParent: " + e.Message );
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
 **number** | **string**| document number of a Scopevisio offer | 
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
| **200** | Returns the Scopevisio outgoing invoice document which is parent of the order with the given document number. |  -  |
| **404** | The order was not found or authorization missing.  No parent for the offer found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOrderPositions

> void GetOrderPositions (string number, string fields = null)

get the positions for the order with the given number (interne Nummer)

Returns the positions of the order with the given number (interne Nummer).  Required profile(s): <i>Aufträge (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOrderPositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio OfferPositions
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get the positions for the order with the given number (interne Nummer)
                apiInstance.GetOrderPositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.GetOrderPositions: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio OfferPositions | 
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
| **200** | Returns the positions of the order with the given number (interne Nummer).  |  -  |
| **404** | No order with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOrders

> void GetOrders (string body = null)

Orders

Returns all Scopevisio orders.  Required profile(s): <i>Aufträge (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOrdersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrderApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Orders
                apiInstance.GetOrders(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrderApi.GetOrders: " + e.Message );
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
| **200** | Returns all Scopevisio orders. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

