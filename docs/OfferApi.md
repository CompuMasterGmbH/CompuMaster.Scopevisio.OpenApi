# CompuMaster.Scopevisio.OpenApi.Api.OfferApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConvertOfferToDispatch**](OfferApi.md#convertoffertodispatch) | **POST** /offer/{number}/convertToDispatch | Convert offer to dispatch
[**ConvertOfferToOpportunity**](OfferApi.md#convertoffertoopportunity) | **POST** /offer/{number}/convertToOpportunity | Convert offer to opportunity
[**ConvertOfferToOrder**](OfferApi.md#convertoffertoorder) | **POST** /offer/{number}/convertToOrder | Convert offer to order
[**ConvertOfferToOutgoingInvoice**](OfferApi.md#convertoffertooutgoinginvoice) | **POST** /offer/{number}/convertToOutgoingInvoice | Convert offer to outgoing invoice
[**ConvertOfferToRecurringInvoice**](OfferApi.md#convertoffertorecurringinvoice) | **POST** /offer/{number}/convertToRecurringInvoice | Convert offer to recurring invoice
[**GetOffer**](OfferApi.md#getoffer) | **GET** /offer/{number} | get offer with number (interne Nummer)
[**GetOfferParent**](OfferApi.md#getofferparent) | **GET** /offer/{number}/parent | the parent of an offer with the given document number
[**GetOfferPositions**](OfferApi.md#getofferpositions) | **GET** /offer/{number}/positions | get the positions for the offer with the given number (interne Nummer)
[**GetOffers**](OfferApi.md#getoffers) | **POST** /offers | Offers



## ConvertOfferToDispatch

> void ConvertOfferToDispatch (string number)

Convert offer to dispatch

Convert the Scopevisio offer with the given number (interne Nummer) to an dispatch.  Required profile(s): <i>Angebote (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertOfferToDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer

            try
            {
                // Convert offer to dispatch
                apiInstance.ConvertOfferToDispatch(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.ConvertOfferToDispatch: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 

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
| **200** | The offer was successfully converted to dispatch. |  -  |
| **404** | The offer was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOfferToOpportunity

> void ConvertOfferToOpportunity (string number)

Convert offer to opportunity

Convert the Scopevisio offer with the given number (interne Nummer) to an opportunity.  Required profile(s): <i>Angebote (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertOfferToOpportunityExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer

            try
            {
                // Convert offer to opportunity
                apiInstance.ConvertOfferToOpportunity(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.ConvertOfferToOpportunity: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 

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
| **200** | The offer was successfully converted to opportunity. |  -  |
| **404** | The offer was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOfferToOrder

> void ConvertOfferToOrder (string number)

Convert offer to order

Convert the Scopevisio offer with the given number (interne Nummer) to an order.  Required profile(s): <i>Angebote (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertOfferToOrderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer

            try
            {
                // Convert offer to order
                apiInstance.ConvertOfferToOrder(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.ConvertOfferToOrder: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 

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
| **200** | The offer was successfully converted to order. |  -  |
| **404** | The offer was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOfferToOutgoingInvoice

> void ConvertOfferToOutgoingInvoice (string number)

Convert offer to outgoing invoice

Convert the Scopevisio offer with the given number (interne Nummer) to an outgoing invoice.  Required profile(s): <i>Angebote (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertOfferToOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer

            try
            {
                // Convert offer to outgoing invoice
                apiInstance.ConvertOfferToOutgoingInvoice(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.ConvertOfferToOutgoingInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 

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
| **200** | The offer was successfully converted to outgoing invoice. |  -  |
| **404** | The offer was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertOfferToRecurringInvoice

> void ConvertOfferToRecurringInvoice (string number, ConvertToRecurringInvoiceForm body = null)

Convert offer to recurring invoice

Convert the Scopevisio offer with the given number (interne Nummer) to a recurring invoice.  Required profile(s): <i>Angebote (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertOfferToRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer
            var body = new ConvertToRecurringInvoiceForm(); // ConvertToRecurringInvoiceForm |  (optional) 

            try
            {
                // Convert offer to recurring invoice
                apiInstance.ConvertOfferToRecurringInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.ConvertOfferToRecurringInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 
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
| **200** | The offer was successfully converted to recurring invoice. |  -  |
| **404** | The offer was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOffer

> void GetOffer (string number, string fields = null)

get offer with number (interne Nummer)

Returns the Scopevisio offer with the given number (interne Nummer).  Required profile(s) : <i>Angebote (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetOfferExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer
            var fields = id,documentNumber,name;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // get offer with number (interne Nummer)
                apiInstance.GetOffer(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.GetOffer: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 
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
| **200** | Returns the Scopevisio offer with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOfferParent

> void GetOfferParent (string number, string fields = null)

the parent of an offer with the given document number

Returns the Scopevisio outgoing invoice document which is parent of the outgoing invoice with the given document number.  Required profile(s): <i>Angebote (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetOfferParentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | document number of a Scopevisio offer
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // the parent of an offer with the given document number
                apiInstance.GetOfferParent(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.GetOfferParent: " + e.Message );
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
| **200** | Returns the Scopevisio outgoing invoice document which is parent of the offer with the given document number. |  -  |
| **404** | The offer was not found or authorization missing.  No parent for the offer found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOfferPositions

> void GetOfferPositions (string number, string fields = null)

get the positions for the offer with the given number (interne Nummer)

Returns the positions of the offer with the given number (interne Nummer).  Required profile(s): <i>Angebote (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetOfferPositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio offer
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get the positions for the offer with the given number (interne Nummer)
                apiInstance.GetOfferPositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.GetOfferPositions: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio offer | 
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
| **200** | Returns the positions of the offer with the given number (interne Nummer).  |  -  |
| **404** | No offer with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOffers

> void GetOffers (string body = null)

Offers

Returns all Scopevisio offers.  Required profile(s): <i>Angebote (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetOffersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OfferApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Offers
                apiInstance.GetOffers(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OfferApi.GetOffers: " + e.Message );
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
| **200** | Returns all Scopevisio offers. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

