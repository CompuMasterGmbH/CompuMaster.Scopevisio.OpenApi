# CompuMaster.Scopevisio.OpenApi.Api.OrganisationApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateOrganisation**](OrganisationApi.md#createorganisation) | **POST** /organisation/new | Creates a Scopevisio organisation.



## CreateOrganisation

> void CreateOrganisation (OrganisationForm body = null)

Creates a Scopevisio organisation.

Creates a Scopevisio organisation with the given name (if not existing) and the given scoa (standard chart of accounts). <strong>The scoa set up takes some time. Please make sure you set your timeout to at least 5 min.</strong> Required profiles: <i>Organisation (Bearbeiten)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateOrganisationExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrganisationApi(Configuration.Default);
            var body = new OrganisationForm(); // OrganisationForm |  (optional) 

            try
            {
                // Creates a Scopevisio organisation.
                apiInstance.CreateOrganisation(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling OrganisationApi.CreateOrganisation: " + e.Message );
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
 **body** | [**OrganisationForm**](OrganisationForm.md)|  | [optional] 

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
| **201** | Organisation created. |  -  |
| **400** | A Scopevisio organisation with the given organisationName already exists.  No standard chart of accounts for the given scoa found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

