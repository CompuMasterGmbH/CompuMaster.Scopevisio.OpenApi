# CompuMaster.Scopevisio.OpenApi.Api.RecurringInvoiceApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConvertRecurringInvoiceToDispatch**](RecurringInvoiceApi.md#convertrecurringinvoicetodispatch) | **POST** /recurringinvoice/{number}/convertToDispatch | Convert recurring invoice to dispatch
[**ConvertRecurringInvoiceToOffer**](RecurringInvoiceApi.md#convertrecurringinvoicetooffer) | **POST** /recurringinvoice/{number}/convertToOffer | Convert recurring invoice to offer
[**ConvertRecurringInvoiceToOpportunity**](RecurringInvoiceApi.md#convertrecurringinvoicetoopportunity) | **POST** /recurringinvoice/{number}/convertToOpportunity | Convert recurring invoice to opportunity
[**ConvertRecurringInvoiceToOrder**](RecurringInvoiceApi.md#convertrecurringinvoicetoorder) | **POST** /recurringinvoice/{number}/convertToOrder | Convert recurring invoice to order
[**ConvertRecurringInvoiceToOutgoingInvoice**](RecurringInvoiceApi.md#convertrecurringinvoicetooutgoinginvoice) | **POST** /recurringinvoice/{number}/convertToOutgoingInvoice | Convert recurring invoice to recurring invoice
[**GetRecurringInvoice**](RecurringInvoiceApi.md#getrecurringinvoice) | **GET** /recurringinvoice/{number} | recurring invoice for number (interne Nummer)
[**GetRecurringInvoicePositions**](RecurringInvoiceApi.md#getrecurringinvoicepositions) | **GET** /recurringinvoice/{number}/positions | get recurring invoice positions for the given number (interne Nummer)
[**GetRecurringInvoices**](RecurringInvoiceApi.md#getrecurringinvoices) | **POST** /recurringinvoices | recurring invoices
[**UpdateRecurringInvoice**](RecurringInvoiceApi.md#updaterecurringinvoice) | **POST** /recurringinvoice/{number} | Update the recurrence data of an invoice.



## ConvertRecurringInvoiceToDispatch

> void ConvertRecurringInvoiceToDispatch (string number)

Convert recurring invoice to dispatch

Convert the Scopevisio recurring invoice with the given number (interne Nummer) to an dispatch.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertRecurringInvoiceToDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice

            try
            {
                // Convert recurring invoice to dispatch
                apiInstance.ConvertRecurringInvoiceToDispatch(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.ConvertRecurringInvoiceToDispatch: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 

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
| **200** | The recurring invoice was successfully converted to dispatch. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertRecurringInvoiceToOffer

> void ConvertRecurringInvoiceToOffer (string number)

Convert recurring invoice to offer

Convert the Scopevisio recurring invoice with the given number (interne Nummer) to an offer.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertRecurringInvoiceToOfferExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice

            try
            {
                // Convert recurring invoice to offer
                apiInstance.ConvertRecurringInvoiceToOffer(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.ConvertRecurringInvoiceToOffer: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 

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
| **200** | The recurring invoice was successfully converted to offer. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertRecurringInvoiceToOpportunity

> void ConvertRecurringInvoiceToOpportunity (string number)

Convert recurring invoice to opportunity

Convert the Scopevisio recurring invoice with the given number (interne Nummer) to an opportunity.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertRecurringInvoiceToOpportunityExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice

            try
            {
                // Convert recurring invoice to opportunity
                apiInstance.ConvertRecurringInvoiceToOpportunity(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.ConvertRecurringInvoiceToOpportunity: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 

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
| **200** | The recurring invoice was successfully converted to opportunity. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertRecurringInvoiceToOrder

> void ConvertRecurringInvoiceToOrder (string number)

Convert recurring invoice to order

Convert the Scopevisio recurring invoice with the given number (interne Nummer) to an order.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertRecurringInvoiceToOrderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice

            try
            {
                // Convert recurring invoice to order
                apiInstance.ConvertRecurringInvoiceToOrder(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.ConvertRecurringInvoiceToOrder: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 

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
| **200** | The recurring invoice was successfully converted to order. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertRecurringInvoiceToOutgoingInvoice

> void ConvertRecurringInvoiceToOutgoingInvoice (string number)

Convert recurring invoice to recurring invoice

Convert the Scopevisio recurring invoice with the given number (interne Nummer) to a recurring invoice.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ConvertRecurringInvoiceToOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice

            try
            {
                // Convert recurring invoice to recurring invoice
                apiInstance.ConvertRecurringInvoiceToOutgoingInvoice(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.ConvertRecurringInvoiceToOutgoingInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 

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
| **200** | The recurring invoice was successfully converted to recurring invoice. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetRecurringInvoice

> void GetRecurringInvoice (string number, string fields = null)

recurring invoice for number (interne Nummer)

Returns the Scopevisio recurring invoice with the given number (interne Nummer).  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // recurring invoice for number (interne Nummer)
                apiInstance.GetRecurringInvoice(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.GetRecurringInvoice: " + e.Message );
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
| **200** | Returns the Scopevisio recurring invoice with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetRecurringInvoicePositions

> void GetRecurringInvoicePositions (string number, string fields = null)

get recurring invoice positions for the given number (interne Nummer)

Returns the positions for the recurring invoice with the given number (interne Nummer).  Required Profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetRecurringInvoicePositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get recurring invoice positions for the given number (interne Nummer)
                apiInstance.GetRecurringInvoicePositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.GetRecurringInvoicePositions: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 
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
| **200** | Returns the positions for the recurring invoice with the given number (interne Nummer).  |  -  |
| **404** | No recurring invoice with with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetRecurringInvoices

> void GetRecurringInvoices (string body = null)

recurring invoices

Returns the Scopevisio recurring invoices. <i> Required profile(s): Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetRecurringInvoicesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // recurring invoices
                apiInstance.GetRecurringInvoices(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.GetRecurringInvoices: " + e.Message );
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
| **200** | Returns the Scopevisio outgoing invoices. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateRecurringInvoice

> void UpdateRecurringInvoice (string number, RecurringInvoiceForm body = null)

Update the recurrence data of an invoice.

Update the recurrence data of the invoice with the given number(interne Nummer).  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RecurringInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio recurring invoice
            var body = new RecurringInvoiceForm(); // RecurringInvoiceForm |  (optional) 

            try
            {
                // Update the recurrence data of an invoice.
                apiInstance.UpdateRecurringInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling RecurringInvoiceApi.UpdateRecurringInvoice: " + e.Message );
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
 **number** | **string**| number (interne Nummer) of a Scopevisio recurring invoice | 
 **body** | [**RecurringInvoiceForm**](RecurringInvoiceForm.md)|  | [optional] 

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
| **200** | The recurring data was successfully updated. |  -  |
| **404** | The recurring invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

