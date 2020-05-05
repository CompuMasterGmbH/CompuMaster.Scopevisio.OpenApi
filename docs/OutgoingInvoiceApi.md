# Org.OpenAPITools.Api.OutgoingInvoiceApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddFileToOutgoingInvoice**](OutgoingInvoiceApi.md#addfiletooutgoinginvoice) | **POST** /outgoinginvoice/{number}/file/new | Add a file to a Scopevisio outgoing invoice
[**ConvertInvoiceToDispatch**](OutgoingInvoiceApi.md#convertinvoicetodispatch) | **POST** /outgoinginvoice/{number}/convertToDispatch | Convert outgoing invoice to dispatch
[**ConvertInvoiceToOffer**](OutgoingInvoiceApi.md#convertinvoicetooffer) | **POST** /outgoinginvoice/{number}/convertToOffer | Convert outgoing invoice to offer
[**ConvertInvoiceToOpportunity**](OutgoingInvoiceApi.md#convertinvoicetoopportunity) | **POST** /outgoinginvoice/{number}/convertToOpportunity | Convert outgoing invoice to opportunity
[**ConvertInvoiceToOrder**](OutgoingInvoiceApi.md#convertinvoicetoorder) | **POST** /outgoinginvoice/{number}/convertToOrder | Convert outgoing invoice to order
[**ConvertInvoiceToRecurringInvoice**](OutgoingInvoiceApi.md#convertinvoicetorecurringinvoice) | **POST** /outgoinginvoice/{number}/convertToRecurringInvoice | Convert outgoing invoice to recurring invoice
[**GetOutgoingInvoice**](OutgoingInvoiceApi.md#getoutgoinginvoice) | **GET** /outgoinginvoice/{number} | outgoing invoice for number (interne Nummer)
[**GetOutgoingInvoiceExportTemplates**](OutgoingInvoiceApi.md#getoutgoinginvoiceexporttemplates) | **GET** /outgoinginvoice/exporttemplates | Export templates for offer, opportunity, order, outgoing invoice, credit, packing slip
[**GetOutgoingInvoiceFile**](OutgoingInvoiceApi.md#getoutgoinginvoicefile) | **GET** /outgoinginvoice/{number}/file | The file for the outgoing invoice
[**GetOutgoingInvoiceParent**](OutgoingInvoiceApi.md#getoutgoinginvoiceparent) | **GET** /outgoinginvoice/{number}/parent | the parent of an outgoing invoice with the given document number
[**GetOutgoingInvoicePositions**](OutgoingInvoiceApi.md#getoutgoinginvoicepositions) | **GET** /outgoinginvoice/{number}/positions | get outgoing invoice positions for the given number (interne Nummer)
[**GetOutgoingInvoiceTeamworkFileLink**](OutgoingInvoiceApi.md#getoutgoinginvoiceteamworkfilelink) | **GET** /outgoinginvoice/{number}/teamworkFileLink | Link to the last file for the outgoing invoice in the teamwork web application
[**GetOutgoingInvoices**](OutgoingInvoiceApi.md#getoutgoinginvoices) | **POST** /outgoinginvoices | outgoing invoices
[**ImportOutgoingInvoices**](OutgoingInvoiceApi.md#importoutgoinginvoices) | **POST** /outgoinginvoices/import | Imports outgoing invoices
[**PostOutgoingInvoice**](OutgoingInvoiceApi.md#postoutgoinginvoice) | **POST** /outgoinginvoice/{number}/post | Post an outgoing invoice.



## AddFileToOutgoingInvoice

> void AddFileToOutgoingInvoice (string number, FileForm body = null)

Add a file to a Scopevisio outgoing invoice

Adds a file to a Scopevisio outgoing invoice.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AddFileToOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = 2018-00942;  // string | documentNumber (Belegnummer) of a Scopevisio outgoing invoice
            var body = new FileForm(); // FileForm |  (optional) 

            try
            {
                // Add a file to a Scopevisio outgoing invoice
                apiInstance.AddFileToOutgoingInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.AddFileToOutgoingInvoice: " + e.Message );
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
 **number** | **string**| documentNumber (Belegnummer) of a Scopevisio outgoing invoice | 
 **body** | [**FileForm**](FileForm.md)|  | [optional] 

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
| **200** | File added |  -  |
| **400** | Data was not provided |  -  |
| **404** | No outgoing invoice for the given number found or authorization missing |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertInvoiceToDispatch

> void ConvertInvoiceToDispatch (string number)

Convert outgoing invoice to dispatch

Convert the Scopevisio outgoing invoice with the given number (interne Nummer) to an dispatch.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertInvoiceToDispatchExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice

            try
            {
                // Convert outgoing invoice to dispatch
                apiInstance.ConvertInvoiceToDispatch(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ConvertInvoiceToDispatch: " + e.Message );
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
| **200** | The outgoing invoice was successfully converted to dispatch. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertInvoiceToOffer

> void ConvertInvoiceToOffer (string number)

Convert outgoing invoice to offer

Convert the Scopevisio outgoing invoice with the given number (interne Nummer) to an offer.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertInvoiceToOfferExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice

            try
            {
                // Convert outgoing invoice to offer
                apiInstance.ConvertInvoiceToOffer(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ConvertInvoiceToOffer: " + e.Message );
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
| **200** | The outgoing invoice was successfully converted to offer. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertInvoiceToOpportunity

> void ConvertInvoiceToOpportunity (string number)

Convert outgoing invoice to opportunity

Convert the Scopevisio outgoing invoice with the given number (interne Nummer) to an opportunity.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertInvoiceToOpportunityExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice

            try
            {
                // Convert outgoing invoice to opportunity
                apiInstance.ConvertInvoiceToOpportunity(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ConvertInvoiceToOpportunity: " + e.Message );
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
| **200** | The outgoing invoice was successfully converted to opportunity. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertInvoiceToOrder

> void ConvertInvoiceToOrder (string number)

Convert outgoing invoice to order

Convert the Scopevisio outgoing invoice with the given number (interne Nummer) to an order.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertInvoiceToOrderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice

            try
            {
                // Convert outgoing invoice to order
                apiInstance.ConvertInvoiceToOrder(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ConvertInvoiceToOrder: " + e.Message );
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
| **200** | The outgoing invoice was successfully converted to order. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ConvertInvoiceToRecurringInvoice

> void ConvertInvoiceToRecurringInvoice (string number, ConvertToRecurringInvoiceForm body = null)

Convert outgoing invoice to recurring invoice

Convert the Scopevisio outgoing invoice with the given number (interne Nummer) to a recurring invoice.  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertInvoiceToRecurringInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice
            var body = new ConvertToRecurringInvoiceForm(); // ConvertToRecurringInvoiceForm |  (optional) 

            try
            {
                // Convert outgoing invoice to recurring invoice
                apiInstance.ConvertInvoiceToRecurringInvoice(number, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ConvertInvoiceToRecurringInvoice: " + e.Message );
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
| **200** | The outgoing invoice was successfully converted to recurring invoice. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoice

> void GetOutgoingInvoice (string number, string fields = null)

outgoing invoice for number (interne Nummer)

Returns the Scopevisio outgoing invoices with the given number (interne Nummer).  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // outgoing invoice for number (interne Nummer)
                apiInstance.GetOutgoingInvoice(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoice: " + e.Message );
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
| **200** | Returns the Scopevisio outgoing invoice with the given number (interne Nummer). |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoiceExportTemplates

> void GetOutgoingInvoiceExportTemplates ()

Export templates for offer, opportunity, order, outgoing invoice, credit, packing slip

Returns the Scopevisio expoirt templates for offer, opportunity, order, outgoing invoice, credit, packing slip.  Required profile(s): <i>Faktura Stammdaten, Opportunities (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoiceExportTemplatesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);

            try
            {
                // Export templates for offer, opportunity, order, outgoing invoice, credit, packing slip
                apiInstance.GetOutgoingInvoiceExportTemplates();
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoiceExportTemplates: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

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
| **200** | Returns the Scopevisio export templates. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoiceFile

> void GetOutgoingInvoiceFile (string number)

The file for the outgoing invoice

Returns the last file for the given outgoing invoice number.  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoiceFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = RE-2018-2;  // string | document number of a Scopevisio outgoing invoice

            try
            {
                // The file for the outgoing invoice
                apiInstance.GetOutgoingInvoiceFile(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoiceFile: " + e.Message );
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
 **number** | **string**| document number of a Scopevisio outgoing invoice | 

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
| **200** | Returns the main file of the Scopevisio outgoing invoice with the given document number. |  -  |
| **404** | No outgoing invoice for the given number found or authorization missing. No file for the given document number found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoiceParent

> void GetOutgoingInvoiceParent (string number, string fields = null)

the parent of an outgoing invoice with the given document number

Returns the Scopevisio outgoing invoice document which is parent of the outgoing invoice with the given document number.  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoiceParentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | document number of a Scopevisio outgoing invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, documentNumber (optional) 

            try
            {
                // the parent of an outgoing invoice with the given document number
                apiInstance.GetOutgoingInvoiceParent(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoiceParent: " + e.Message );
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
 **number** | **string**| document number of a Scopevisio outgoing invoice | 
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
| **200** | Returns the Scopevisio outgoing invoice document which is parent of the outgoing invoice with the given document number. |  -  |
| **404** | The outgoing invoice was not found or authorization missing.  No parent for the outgoing invoice found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoicePositions

> void GetOutgoingInvoicePositions (string number, string fields = null)

get outgoing invoice positions for the given number (interne Nummer)

Returns the positions for the outgoing invoice with the given number (interne Nummer).  Required Profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoicePositionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,name (optional) 

            try
            {
                // get outgoing invoice positions for the given number (interne Nummer)
                apiInstance.GetOutgoingInvoicePositions(number, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoicePositions: " + e.Message );
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
| **200** | Returns the positions for the outgoing invoice with the given number (interne Nummer).  |  -  |
| **404** | No outgoing invoice with with the given number found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoiceTeamworkFileLink

> void GetOutgoingInvoiceTeamworkFileLink (string number)

Link to the last file for the outgoing invoice in the teamwork web application

Returns a link to the last file for the given outgoing invoice number in the teamwork web application. You need to be logged in to the teamwork webapp.  Required profile(s): <i>Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoiceTeamworkFileLinkExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = RE-2018-2;  // string | document number of a Scopevisio outgoing invoice

            try
            {
                // Link to the last file for the outgoing invoice in the teamwork web application
                apiInstance.GetOutgoingInvoiceTeamworkFileLink(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoiceTeamworkFileLink: " + e.Message );
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
 **number** | **string**| document number of a Scopevisio outgoing invoice | 

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
| **200** | Returns a link to the last file for the given outgoing invoice number in the teamwork web application. |  -  |
| **404** | No outgoingInvoice for the given number found or authorization missing. No teamwork file for the given number. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetOutgoingInvoices

> void GetOutgoingInvoices (string body = null)

outgoing invoices

Returns the Scopevisio outgoing invoices. <i> Required profile(s): Rechnungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetOutgoingInvoicesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // outgoing invoices
                apiInstance.GetOutgoingInvoices(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.GetOutgoingInvoices: " + e.Message );
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


## ImportOutgoingInvoices

> void ImportOutgoingInvoices (OutgoingInvoiceImportForm body = null)

Imports outgoing invoices

Required profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ImportOutgoingInvoicesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var body = new OutgoingInvoiceImportForm(); // OutgoingInvoiceImportForm |  (optional) 

            try
            {
                // Imports outgoing invoices
                apiInstance.ImportOutgoingInvoices(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.ImportOutgoingInvoices: " + e.Message );
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
 **body** | [**OutgoingInvoiceImportForm**](OutgoingInvoiceImportForm.md)|  | [optional] 

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
| **200** | Imports outgoing invoices. Errors are set in the response. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PostOutgoingInvoice

> void PostOutgoingInvoice (string number)

Post an outgoing invoice.

Post the outgoing invoice with the given number(interne Nummer).  Required profile(s): <i>Rechnungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostOutgoingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OutgoingInvoiceApi(Configuration.Default);
            var number = number_example;  // string | number (interne Nummer) of a Scopevisio outgoing invoice

            try
            {
                // Post an outgoing invoice.
                apiInstance.PostOutgoingInvoice(number);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OutgoingInvoiceApi.PostOutgoingInvoice: " + e.Message );
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
| **200** | The outgoing invoice was successfully posted. |  -  |
| **400** | The outgoing invoice was already posted. |  -  |
| **404** | The outgoing invoice was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

