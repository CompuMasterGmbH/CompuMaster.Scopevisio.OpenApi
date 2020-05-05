# Org.OpenAPITools.Api.IncomingInvoiceApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateIncomingInvoice**](IncomingInvoiceApi.md#createincominginvoice) | **POST** /incominginvoice/new | Import an incoming invoice.
[**GetIncomingInvoiceTeamworkFileLink**](IncomingInvoiceApi.md#getincominginvoiceteamworkfilelink) | **GET** /incominginvoice/{idOrNumber}/teamworkFileLink | Link to the main file for the incoming invoice in the teamwork web application
[**GetIncomingInvoices**](IncomingInvoiceApi.md#getincominginvoices) | **POST** /incominginvoices | incoming invoices
[**GetInvoice**](IncomingInvoiceApi.md#getinvoice) | **GET** /incominginvoice/{idOrNumber} | incoming invoice for the given idOrNumber (id or interne Nummer)
[**GetInvoiceFile**](IncomingInvoiceApi.md#getinvoicefile) | **GET** /incominginvoice/{idOrNumber}/file | The file of incoming invoice with the given idOrNumber (id or Interne Nummer)



## CreateIncomingInvoice

> void CreateIncomingInvoice (IncomingInvoiceForm body = null)

Import an incoming invoice.

Required profiles: <i>Rechnungseingangsbuch (Bearbeiten)</i>.  The invoice should be provided as a base64 encoded pdf file. The maximum size allowed is 20 MB.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateIncomingInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IncomingInvoiceApi(Configuration.Default);
            var body = new IncomingInvoiceForm(); // IncomingInvoiceForm |  (optional) 

            try
            {
                // Import an incoming invoice.
                apiInstance.CreateIncomingInvoice(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling IncomingInvoiceApi.CreateIncomingInvoice: " + e.Message );
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
 **body** | [**IncomingInvoiceForm**](IncomingInvoiceForm.md)|  | [optional] 

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
| **200** | Import successfull. |  -  |
| **404** | Filename missing. Data missing. The data size exceeds 20 MB. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetIncomingInvoiceTeamworkFileLink

> void GetIncomingInvoiceTeamworkFileLink (string idOrNumber)

Link to the main file for the incoming invoice in the teamwork web application

Returns a link to the main file for the incoming invoice with the given idOrNumber (id or Interne Nummer) in the teamwork web application.You need to be logged in to the teamwork webapp.  Required profile(s): <i>Rechnungseingangsbuch (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetIncomingInvoiceTeamworkFileLinkExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IncomingInvoiceApi(Configuration.Default);
            var idOrNumber = 2018-2;  // string | idOrNumber (id or Interne Nummer) of a Scopevisio incoming invoice.

            try
            {
                // Link to the main file for the incoming invoice in the teamwork web application
                apiInstance.GetIncomingInvoiceTeamworkFileLink(idOrNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling IncomingInvoiceApi.GetIncomingInvoiceTeamworkFileLink: " + e.Message );
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
 **idOrNumber** | **string**| idOrNumber (id or Interne Nummer) of a Scopevisio incoming invoice. | 

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
| **200** | Returns a link to the main file for the incoming invoice with the given idOrNumber (id or Interne Nummer) in the teamwork web application. |  -  |
| **404** | No outgoingInvoice for the given idOrMumber found or authorization missing. No teamwork file for the given idOrNumber. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetIncomingInvoices

> void GetIncomingInvoices (string body = null)

incoming invoices

Returns Scopevisio incoming invoices.  Required profile(s): <i>Rechnungseingangsbuch (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetIncomingInvoicesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IncomingInvoiceApi(Configuration.Default);
            var body = body_example;  // string | (optional) Apply a search filter in JSON. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // incoming invoices
                apiInstance.GetIncomingInvoices(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling IncomingInvoiceApi.GetIncomingInvoices: " + e.Message );
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
 **body** | **string**| (optional) Apply a search filter in JSON. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | [optional] 

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
| **200** | Returns Scopevisio incoming invoices. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetInvoice

> void GetInvoice (string idOrNumber, string fields = null)

incoming invoice for the given idOrNumber (id or interne Nummer)

Returns the Scopevisio incoming invoices with the given idOrNumber (id or interne Nummer).  Required profile(s):<i> Rechnungseingangsbuch (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInvoiceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IncomingInvoiceApi(Configuration.Default);
            var idOrNumber = idOrNumber_example;  // string | document idOrNumber (id or interne Nummer) of a Scopevisio invcoming invoice
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,documentNumber (optional) 

            try
            {
                // incoming invoice for the given idOrNumber (id or interne Nummer)
                apiInstance.GetInvoice(idOrNumber, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling IncomingInvoiceApi.GetInvoice: " + e.Message );
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
 **idOrNumber** | **string**| document idOrNumber (id or interne Nummer) of a Scopevisio invcoming invoice | 
 **fields** | **string**| comma separated list of fields to show in response  id,documentNumber | [optional] 

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
| **200** | Returns the scopevisio incoming invoices |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetInvoiceFile

> void GetInvoiceFile (string idOrNumber)

The file of incoming invoice with the given idOrNumber (id or Interne Nummer)

Returns the main file of the Scopevisio incoming invoice with the given idOrNumber (id or Interne Nummer).  Required profile(s):<i> Rechnungseingangsbuch (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInvoiceFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IncomingInvoiceApi(Configuration.Default);
            var idOrNumber = 2018-2;  // string | id or number(Interne Nummer) of a Scopevisio invcoming invoice.

            try
            {
                // The file of incoming invoice with the given idOrNumber (id or Interne Nummer)
                apiInstance.GetInvoiceFile(idOrNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling IncomingInvoiceApi.GetInvoiceFile: " + e.Message );
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
 **idOrNumber** | **string**| id or number(Interne Nummer) of a Scopevisio invcoming invoice. | 

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
| **200** | Returns the main file of the Scopevisio incoming invoice with the given idOrNumber (id or Interne Nummer). |  -  |
| **404** | No incoming invoice for the given idOrNumber found or authorization missing. No file for the given invoice idOrNumber found |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

