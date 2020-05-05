# Org.OpenAPITools.Api.TaskApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddTaskFile**](TaskApi.md#addtaskfile) | **POST** /task/{taskId}/file/new | Add a file to a Scopevisio task
[**AssignMailTask**](TaskApi.md#assignmailtask) | **POST** /task/{taskId}/assignMail | Assign mail to a task.
[**CreateSubtask**](TaskApi.md#createsubtask) | **POST** /subtask/new | Create a new subtask (Tätigkeit)
[**CreateTask**](TaskApi.md#createtask) | **POST** /task/new | create a new task
[**CreateTaskComment**](TaskApi.md#createtaskcomment) | **POST** /task/{taskId}/comment/new | Create a new comment.
[**GetSubtasks**](TaskApi.md#getsubtasks) | **GET** /task/{taskId}/subtasks | Subtasks (Tätigkeiten) for task id
[**GetTask**](TaskApi.md#gettask) | **GET** /task/{id} | task for id
[**GetTaskComments**](TaskApi.md#gettaskcomments) | **GET** /task/{taskId}/comments | Comments by taskId.
[**GetTasks**](TaskApi.md#gettasks) | **POST** /tasks | tasks
[**UpdateTask**](TaskApi.md#updatetask) | **POST** /task/{id} | Update task



## AddTaskFile

> void AddTaskFile (long taskId, TeamworkFileForm body = null)

Add a file to a Scopevisio task

Adds a file to a Scopevisio task.  Required profile(s): <i>Aufgaben (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AddTaskFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var taskId = 1094;  // long | taskId (Master ID) of a Scopevisio taskId.
            var body = new TeamworkFileForm(); // TeamworkFileForm |  (optional) 

            try
            {
                // Add a file to a Scopevisio task
                apiInstance.AddTaskFile(taskId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.AddTaskFile: " + e.Message );
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
 **taskId** | **long**| taskId (Master ID) of a Scopevisio taskId. | 
 **body** | [**TeamworkFileForm**](TeamworkFileForm.md)|  | [optional] 

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
| **200** | File added. |  -  |
| **400** | Data was not provided.   The user does&#39;t have teamwork access. |  -  |
| **404** | No task for the given taskId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## AssignMailTask

> void AssignMailTask (long taskId, AssignMailForm body)

Assign mail to a task.

Assigns the mail with the given mailId to the task with the given taskId.  Required profile(s): <i>Aufgaben,Mailbot,Mailagent (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AssignMailTaskExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var taskId = 789;  // long | 
            var body = new AssignMailForm(); // AssignMailForm | content in JSON format

            try
            {
                // Assign mail to a task.
                apiInstance.AssignMailTask(taskId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.AssignMailTask: " + e.Message );
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
 **taskId** | **long**|  | 
 **body** | [**AssignMailForm**](AssignMailForm.md)| content in JSON format | 

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
| **200** | Mail assigned. |  -  |
| **400** | MailId not provided. |  -  |
| **404** | No mail with the given mailId found. No task with the given taskId found. |  -  |
| **500** | Mail not assigned. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateSubtask

> void CreateSubtask (SubtaskForm body)

Create a new subtask (Tätigkeit)

Creates a new subtask (Tätigkeit) for a Scopevisio Task  Required profile(s): <i>Aufgabe, Kontakte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateSubtaskExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var body = new SubtaskForm(); // SubtaskForm | content in JSON format, taskId is required

            try
            {
                // Create a new subtask (Tätigkeit)
                apiInstance.CreateSubtask(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.CreateSubtask: " + e.Message );
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
 **body** | [**SubtaskForm**](SubtaskForm.md)| content in JSON format, taskId is required | 

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
| **201** | Created |  -  |
| **404** | No task with the given taskId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateTask

> void CreateTask (TaskForm body)

create a new task

Creates a new Scopevisio Task  Required profile(s): <i>Aufgabe, Kontakte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateTaskExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var body = new TaskForm(); // TaskForm | content in JSON format, topic is required

            try
            {
                // create a new task
                apiInstance.CreateTask(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.CreateTask: " + e.Message );
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
 **body** | [**TaskForm**](TaskForm.md)| content in JSON format, topic is required | 

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
| **201** | Created |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateTaskComment

> void CreateTaskComment (long taskId, BlogEntryForm body = null)

Create a new comment.

Creates a new comment. Required profile(s): <i>Aufgaben, Pinnwand (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateTaskCommentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var taskId = 10942;  // long | taskId of a Scopevisio task
            var body = new BlogEntryForm(); // BlogEntryForm |  (optional) 

            try
            {
                // Create a new comment.
                apiInstance.CreateTaskComment(taskId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.CreateTaskComment: " + e.Message );
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
 **taskId** | **long**| taskId of a Scopevisio task | 
 **body** | [**BlogEntryForm**](BlogEntryForm.md)|  | [optional] 

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
| **201** | Comment created. |  -  |
| **400** | Missing taskId. |  -  |
| **404** | No task with the given taskId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetSubtasks

> void GetSubtasks (long taskId)

Subtasks (Tätigkeiten) for task id

Returns all subtasks (Tätigkeiten) for a Scopevisio task with the given id.  Required profile(s): <i>Aufgabe (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetSubtasksExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var taskId = 789;  // long | 

            try
            {
                // Subtasks (Tätigkeiten) for task id
                apiInstance.GetSubtasks(taskId);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.GetSubtasks: " + e.Message );
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
 **taskId** | **long**|  | 

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
| **200** | Returns all subtasks for a Scopevisio task with the given id.  |  -  |
| **404** | A task with the given id was not found or authorization missing.  |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTask

> void GetTask (long id, string fields = null)

task for id

Returns the Scopevisio task with the given id.  Required profile(s): <i>Aufgabe (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetTaskExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio task
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,topic (optional) 

            try
            {
                // task for id
                apiInstance.GetTask(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.GetTask: " + e.Message );
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
 **id** | **long**| id of a Scopevisio task | 
 **fields** | **string**| comma separated list of fields to show in response  id,topic | [optional] 

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
| **200** | Returns the Scopevisio task with the given id. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTaskComments

> void GetTaskComments (long taskId, string fields = null)

Comments by taskId.

Returns the Scopevisio comments with the given taskId.  Required profile(s): <i>Aufgaben, Pinnwand (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetTaskCommentsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var taskId = 789;  // long | id (Master ID) of a Scopevisio task
            var fields = fields_example;  // string | Comma separated list of fields to show in response.  id,blogId,body (optional) 

            try
            {
                // Comments by taskId.
                apiInstance.GetTaskComments(taskId, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.GetTaskComments: " + e.Message );
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
 **taskId** | **long**| id (Master ID) of a Scopevisio task | 
 **fields** | **string**| Comma separated list of fields to show in response.  id,blogId,body | [optional] 

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
| **200** | Returns the Scopevisio comments with the given taskId. |  -  |
| **400** | TaskId not provided. |  -  |
| **404** | No Task with the given taskId found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTasks

> void GetTasks (string body = null)

tasks

Returns Scopevisio tasks.  Required profile(s): <i>Aufgabe (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetTasksExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // tasks
                apiInstance.GetTasks(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.GetTasks: " + e.Message );
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
| **200** | Returns Scopevisio tasks. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateTask

> void UpdateTask (long id, TaskForm body = null)

Update task

Updates the Scopevisio task with the given task id.  Required profile(s): <i>Aufgabe (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateTaskExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TaskApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio task
            var body = new TaskForm(); // TaskForm | Content in JSON format. Please refer to TaskForm for example values and to our  <a href='../browser/index.html#!/documentation'>general documentation</a> for update information. (optional) 

            try
            {
                // Update task
                apiInstance.UpdateTask(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TaskApi.UpdateTask: " + e.Message );
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
 **id** | **long**| id of a Scopevisio task | 
 **body** | [**TaskForm**](TaskForm.md)| Content in JSON format. Please refer to TaskForm for example values and to our  &lt;a href&#x3D;&#39;../browser/index.html#!/documentation&#39;&gt;general documentation&lt;/a&gt; for update information. | [optional] 

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
| **200** | Updates the Scopevisio task with the given task id. |  -  |
| **404** | Task not found or authorization missing |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

