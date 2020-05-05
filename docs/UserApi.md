# Org.OpenAPITools.Api.UserApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateUsers**](UserApi.md#createusers) | **POST** /users | Creates the given users
[**DeleteUser**](UserApi.md#deleteuser) | **DELETE** /users/{uidOrEmail} | Deletes the user by UID or email. Please note, the users are only marked as deleted.
[**GetUser**](UserApi.md#getuser) | **GET** /users/{uidOrEmail} | Returns the user by UID or email
[**GetUsers**](UserApi.md#getusers) | **GET** /users | Returns all users
[**UpdateUsers**](UserApi.md#updateusers) | **PUT** /users | Updates the given users



## CreateUsers

> void CreateUsers (List<UserForm> body = null)

Creates the given users

Required profile(s): Benutzer (Bearbeiten).

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateUsersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi(Configuration.Default);
            var body = new List<UserForm>(); // List<UserForm> |  (optional) 

            try
            {
                // Creates the given users
                apiInstance.CreateUsers(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling UserApi.CreateUsers: " + e.Message );
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
 **body** | [**List&lt;UserForm&gt;**](UserForm.md)|  | [optional] 

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
| **200** | Creates the given users. |  -  |
| **500** | Something went wrong. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DeleteUser

> void DeleteUser (string uidOrEmail)

Deletes the user by UID or email. Please note, the users are only marked as deleted.

Required profile(s): Benutzer (Bearbeiten).

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteUserExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi(Configuration.Default);
            var uidOrEmail = uidOrEmail_example;  // string | User ID or email

            try
            {
                // Deletes the user by UID or email. Please note, the users are only marked as deleted.
                apiInstance.DeleteUser(uidOrEmail);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling UserApi.DeleteUser: " + e.Message );
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
 **uidOrEmail** | **string**| User ID or email | 

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
| **204** | Successful delete. |  -  |
| **500** | Something went wrong |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetUser

> void GetUser (string uidOrEmail)

Returns the user by UID or email

Required profile(s): Benutzer (Anzeigen).

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetUserExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi(Configuration.Default);
            var uidOrEmail = uidOrEmail_example;  // string | User ID or email

            try
            {
                // Returns the user by UID or email
                apiInstance.GetUser(uidOrEmail);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling UserApi.GetUser: " + e.Message );
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
 **uidOrEmail** | **string**| User ID or email | 

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
| **200** | Returns the user by UID or email. |  -  |
| **500** | Something went wrong |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetUsers

> void GetUsers (bool onlyActive = null)

Returns all users

Required profile(s): Benutzer (Anzeigen).

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetUsersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi(Configuration.Default);
            var onlyActive = true;  // bool | If set to true, deleted users are not returned. (optional) 

            try
            {
                // Returns all users
                apiInstance.GetUsers(onlyActive);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling UserApi.GetUsers: " + e.Message );
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
 **onlyActive** | **bool**| If set to true, deleted users are not returned. | [optional] 

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
| **200** | Returns all users. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateUsers

> void UpdateUsers (List<UserForm> body = null)

Updates the given users

Required profile(s): Benutzer (Bearbeiten).

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateUsersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi(Configuration.Default);
            var body = new List<UserForm>(); // List<UserForm> |  (optional) 

            try
            {
                // Updates the given users
                apiInstance.UpdateUsers(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling UserApi.UpdateUsers: " + e.Message );
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
 **body** | [**List&lt;UserForm&gt;**](UserForm.md)|  | [optional] 

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
| **200** | Updates the given users |  -  |
| **500** | Somethin went wrong. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

