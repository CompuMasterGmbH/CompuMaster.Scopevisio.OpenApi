# Org.OpenAPITools.Api.TeamworkApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteHandler1**](TeamworkApi.md#deletehandler1) | **DELETE** /teamworkbridge/{arg1} | 
[**DeleteHandler2**](TeamworkApi.md#deletehandler2) | **DELETE** /teamworkbridge/{arg1}/{arg2} | 
[**DeleteHandler3**](TeamworkApi.md#deletehandler3) | **DELETE** /teamworkbridge/{arg1}/{arg2}/{arg3} | 
[**DeleteHandler4**](TeamworkApi.md#deletehandler4) | **DELETE** /teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4} | 
[**GetHandler1**](TeamworkApi.md#gethandler1) | **GET** /teamworkbridge/{arg1} | 
[**GetHandler2**](TeamworkApi.md#gethandler2) | **GET** /teamworkbridge/{arg1}/{arg2} | 
[**GetHandler3**](TeamworkApi.md#gethandler3) | **GET** /teamworkbridge/{arg1}/{arg2}/{arg3} | 
[**PostHandler1**](TeamworkApi.md#posthandler1) | **POST** /teamworkbridge/{arg1} | 
[**PostHandler2**](TeamworkApi.md#posthandler2) | **POST** /teamworkbridge/{arg1}/{arg2} | 
[**PostHandler3**](TeamworkApi.md#posthandler3) | **POST** /teamworkbridge/{arg1}/{arg2}/{arg3} | 
[**PutHandler1**](TeamworkApi.md#puthandler1) | **PUT** /teamworkbridge/{arg1} | 
[**PutHandler2**](TeamworkApi.md#puthandler2) | **PUT** /teamworkbridge/{arg1}/{arg2} | 
[**PutHandler3**](TeamworkApi.md#puthandler3) | **PUT** /teamworkbridge/{arg1}/{arg2}/{arg3} | 
[**PutHandler4**](TeamworkApi.md#puthandler4) | **PUT** /teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4} | 



## DeleteHandler1

> Object DeleteHandler1 (string arg1)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteHandler1Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 

            try
            {
                Object result = apiInstance.DeleteHandler1(arg1);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.DeleteHandler1: " + e.Message );
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
 **arg1** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DeleteHandler2

> Object DeleteHandler2 (string arg1, string arg2)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteHandler2Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 

            try
            {
                Object result = apiInstance.DeleteHandler2(arg1, arg2);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.DeleteHandler2: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DeleteHandler3

> Object DeleteHandler3 (string arg1, string arg2, string arg3)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteHandler3Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 

            try
            {
                Object result = apiInstance.DeleteHandler3(arg1, arg2, arg3);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.DeleteHandler3: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DeleteHandler4

> Object DeleteHandler4 (string arg1, string arg2, string arg3, string arg4)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteHandler4Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 
            var arg4 = arg4_example;  // string | 

            try
            {
                Object result = apiInstance.DeleteHandler4(arg1, arg2, arg3, arg4);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.DeleteHandler4: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 
 **arg4** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetHandler1

> Object GetHandler1 (string arg1)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetHandler1Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 

            try
            {
                Object result = apiInstance.GetHandler1(arg1);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.GetHandler1: " + e.Message );
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
 **arg1** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetHandler2

> Object GetHandler2 (string arg1, string arg2)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetHandler2Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 

            try
            {
                Object result = apiInstance.GetHandler2(arg1, arg2);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.GetHandler2: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetHandler3

> Object GetHandler3 (string arg1, string arg2, string arg3)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetHandler3Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 

            try
            {
                Object result = apiInstance.GetHandler3(arg1, arg2, arg3);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.GetHandler3: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PostHandler1

> Object PostHandler1 (string arg1)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostHandler1Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 

            try
            {
                Object result = apiInstance.PostHandler1(arg1);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PostHandler1: " + e.Message );
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
 **arg1** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PostHandler2

> Object PostHandler2 (string arg1, string arg2)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostHandler2Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 

            try
            {
                Object result = apiInstance.PostHandler2(arg1, arg2);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PostHandler2: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PostHandler3

> Object PostHandler3 (string arg1, string arg2, string arg3)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostHandler3Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 

            try
            {
                Object result = apiInstance.PostHandler3(arg1, arg2, arg3);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PostHandler3: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PutHandler1

> Object PutHandler1 (string arg1)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutHandler1Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 

            try
            {
                Object result = apiInstance.PutHandler1(arg1);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PutHandler1: " + e.Message );
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
 **arg1** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PutHandler2

> Object PutHandler2 (string arg1, string arg2)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutHandler2Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 

            try
            {
                Object result = apiInstance.PutHandler2(arg1, arg2);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PutHandler2: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PutHandler3

> Object PutHandler3 (string arg1, string arg2, string arg3)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutHandler3Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 

            try
            {
                Object result = apiInstance.PutHandler3(arg1, arg2, arg3);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PutHandler3: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PutHandler4

> Object PutHandler4 (string arg1, string arg2, string arg3, string arg4)



See our <a href='../browser/index.html#!/documentation#teamwork'>documentation</a> for usage.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutHandler4Example
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TeamworkApi(Configuration.Default);
            var arg1 = arg1_example;  // string | 
            var arg2 = arg2_example;  // string | 
            var arg3 = arg3_example;  // string | 
            var arg4 = arg4_example;  // string | 

            try
            {
                Object result = apiInstance.PutHandler4(arg1, arg2, arg3, arg4);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TeamworkApi.PutHandler4: " + e.Message );
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
 **arg1** | **string**|  | 
 **arg2** | **string**|  | 
 **arg3** | **string**|  | 
 **arg4** | **string**|  | 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

