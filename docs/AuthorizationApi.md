# CompuMaster.Scopevisio.OpenApi.Api.AuthorizationApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ChangeOrganisationById**](AuthorizationApi.md#changeorganisationbyid) | **PUT** /token/organisation/{organisationId} | Changes the organisation by ID
[**ChangeOrganisationByName**](AuthorizationApi.md#changeorganisationbyname) | **PUT** /token/organisation | Changes the organisation by name
[**RevokeToken**](AuthorizationApi.md#revoketoken) | **DELETE** /token | Revokes the given token
[**Token**](AuthorizationApi.md#token) | **POST** /token | Access token



## ChangeOrganisationById

> void ChangeOrganisationById (long organisationId)

Changes the organisation by ID

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ChangeOrganisationByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AuthorizationApi(Configuration.Default);
            var organisationId = 789;  // long | ID of the organisation to change to

            try
            {
                // Changes the organisation by ID
                apiInstance.ChangeOrganisationById(organisationId);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AuthorizationApi.ChangeOrganisationById: " + e.Message );
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
 **organisationId** | **long**| ID of the organisation to change to | 

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
| **200** | OK |  -  |
| **400** | Invalid organisation ID |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ChangeOrganisationByName

> void ChangeOrganisationByName (string name)

Changes the organisation by name

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ChangeOrganisationByNameExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AuthorizationApi(Configuration.Default);
            var name = name_example;  // string | Name of the organisation to change to

            try
            {
                // Changes the organisation by name
                apiInstance.ChangeOrganisationByName(name);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AuthorizationApi.ChangeOrganisationByName: " + e.Message );
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
 **name** | **string**| Name of the organisation to change to | 

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
| **200** | OK |  -  |
| **400** | Invalid organisation name |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## RevokeToken

> void RevokeToken ()

Revokes the given token

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class RevokeTokenExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AuthorizationApi(Configuration.Default);

            try
            {
                // Revokes the given token
                apiInstance.RevokeToken();
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AuthorizationApi.RevokeToken: " + e.Message );
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
| **204** | OK |  -  |
| **403** | Access denied. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Token

> void Token (string grantType, string customer, string clientId = null, string clientSecret = null, string username = null, long? organisationId = null, string organisation = null, string password = null, string totpResponse = null, string refreshToken = null, string code = null, string requestcookie = null)

Access token

Retrieves a Scopevisio access token. No profiles required.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class TokenExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            var apiInstance = new AuthorizationApi(Configuration.Default);
            var grantType = grantType_example;  // string | Grant type
            var customer = customer_example;  // string | Scopevisio 7 digit customer number
            var clientId = clientId_example;  // string | Client ID, if not set, will revert to \\\"sv\\\" (optional) 
            var clientSecret = clientSecret_example;  // string | Client secret (optional) 
            var username = username_example;  // string | Scopevisio user login, required if grant_type=password (optional) 
            var organisationId = 789;  // long? | Scopevisio organisation id (optional) 
            var organisation = organisation_example;  // string | Scopevisio organisation name (optional) 
            var password = password_example;  // string | Scopevisio user password, required if grant_type=password (optional) 
            var totpResponse = totpResponse_example;  // string | Scopeviso user one time password, required only if activated (optional) 
            var refreshToken = refreshToken_example;  // string | Refresh token, required if grant_type=refresh_token (optional) 
            var code = code_example;  // string | Authorization code, required if grant_type=authorization_code (optional) 
            var requestcookie = requestcookie_example;  // string | Whether to set access token in cookie (optional) 

            try
            {
                // Access token
                apiInstance.Token(grantType, customer, clientId, clientSecret, username, organisationId, organisation, password, totpResponse, refreshToken, code, requestcookie);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AuthorizationApi.Token: " + e.Message );
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
 **grantType** | **string**| Grant type | 
 **customer** | **string**| Scopevisio 7 digit customer number | 
 **clientId** | **string**| Client ID, if not set, will revert to \\\&quot;sv\\\&quot; | [optional] 
 **clientSecret** | **string**| Client secret | [optional] 
 **username** | **string**| Scopevisio user login, required if grant_type&#x3D;password | [optional] 
 **organisationId** | **long?**| Scopevisio organisation id | [optional] 
 **organisation** | **string**| Scopevisio organisation name | [optional] 
 **password** | **string**| Scopevisio user password, required if grant_type&#x3D;password | [optional] 
 **totpResponse** | **string**| Scopeviso user one time password, required only if activated | [optional] 
 **refreshToken** | **string**| Refresh token, required if grant_type&#x3D;refresh_token | [optional] 
 **code** | **string**| Authorization code, required if grant_type&#x3D;authorization_code | [optional] 
 **requestcookie** | **string**| Whether to set access token in cookie | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/x-www-form-urlencoded
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Retrieves a Scopevisio access token. |  -  |
| **400** | Unknown client id. Missing grant type. Unsupported grant type. |  -  |
| **403** | Bad client credentials. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

