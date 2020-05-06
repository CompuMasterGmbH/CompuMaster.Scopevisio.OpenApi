# CompuMaster.Scopevisio.OpenApi.Api.ProjectApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddProjectFile**](ProjectApi.md#addprojectfile) | **POST** /project/{projectId}/file/new | Add a file to a Scopevisio project
[**AssignMailProject**](ProjectApi.md#assignmailproject) | **POST** /project/{projectId}/assignMail | Assign mail to a project.
[**CreateIncomingMail**](ProjectApi.md#createincomingmail) | **POST** /incomingmail/new | Import an incoming email. 
[**CreateProject**](ProjectApi.md#createproject) | **POST** /project/new | create a new project
[**CreateProjectComment**](ProjectApi.md#createprojectcomment) | **POST** /project/{projectId}/comment/new | Create a new comment.
[**CreateProjectEmployee**](ProjectApi.md#createprojectemployee) | **POST** /project/{projectId}/projectemployee/new | Create a project employee (Projektteam-Eintrag)
[**CreateSalesProject**](ProjectApi.md#createsalesproject) | **POST** /salesproject/new | Create a new sales project (Vertriebsprojekt)
[**CreateTimeEntry**](ProjectApi.md#createtimeentry) | **POST** /timeentry/new | Create a new timeentry
[**GetAllTimEntries**](ProjectApi.md#getalltimentries) | **POST** /timeentries | Get all timeentries available
[**GetBlogEntriesByProjectId**](ProjectApi.md#getblogentriesbyprojectid) | **GET** /project/{projectId}/comments | Comments by projectId.
[**GetProject**](ProjectApi.md#getproject) | **GET** /project/{id} | project by id
[**GetProjectEmployee**](ProjectApi.md#getprojectemployee) | **GET** /project/{projectId}/projectemployee/{employeeId} | Get project employee (Projektteam-Eintrag) by id
[**GetProjectEmployees**](ProjectApi.md#getprojectemployees) | **POST** /project/{projectId}/projectemployees | project employees (Projektteam) of a given project
[**GetProjectResources**](ProjectApi.md#getprojectresources) | **POST** /projectresources | Get project resources.
[**GetProjectRevenue**](ProjectApi.md#getprojectrevenue) | **POST** /projectrevenue | Get project revenue
[**GetProjects**](ProjectApi.md#getprojects) | **POST** /projects | projects
[**GetTimeEntry**](ProjectApi.md#gettimeentry) | **GET** /timeentry/{id} | timeEntry for id
[**UpdateProject**](ProjectApi.md#updateproject) | **POST** /project/{id} | update project by id
[**UpdateProjectEmployee**](ProjectApi.md#updateprojectemployee) | **POST** /project/{projectId}/projectemployee/{employeeId} | update project employee (Projektteam-Eintrag) by employeeId (not contactId).
[**UpdateSalesProject**](ProjectApi.md#updatesalesproject) | **POST** /salesproject/{id} | update sales project (Vertriebsprojekt) by id
[**UpdateTimeEntry**](ProjectApi.md#updatetimeentry) | **POST** /timeentry/{id} | Update timeEntry



## AddProjectFile

> void AddProjectFile (long projectId, TeamworkFileForm body = null)

Add a file to a Scopevisio project

Adds a file to a Scopevisio project.  Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AddProjectFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 10942;  // long | projectId (MID) of a Scopevisio project.
            var body = new TeamworkFileForm(); // TeamworkFileForm |  (optional) 

            try
            {
                // Add a file to a Scopevisio project
                apiInstance.AddProjectFile(projectId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.AddProjectFile: " + e.Message );
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
 **projectId** | **long**| projectId (MID) of a Scopevisio project. | 
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
| **404** | No project for the given projectId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## AssignMailProject

> void AssignMailProject (long projectId, AssignMailForm body)

Assign mail to a project.

Assigns the mail with the given mailId to the project with the given projectId.  Required profile(s): <i>Projekte, Mailbot, Mailagent (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AssignMailProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 789;  // long | 
            var body = new AssignMailForm(); // AssignMailForm | content in JSON format

            try
            {
                // Assign mail to a project.
                apiInstance.AssignMailProject(projectId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.AssignMailProject: " + e.Message );
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
 **projectId** | **long**|  | 
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
| **404** | No mail with the given mailId found. No project with the given contactIdOrLegacyNumber found. |  -  |
| **500** | Mail not assigned. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateIncomingMail

> void CreateIncomingMail (IncomingMailForm body = null)

Import an incoming email. 

Required profiles: <i>Mailbot, Mailagent (Bearbeiten)</i>. The service tries to assign the imported mail to the sender and receiver contacts if found and if they are not the current user or a vaild sender (see Scopevisio client, Systemadministration -> Persönliche E-Mail-Adresse verwalten -> Absender 1-5).The email should be provided as eml content.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateIncomingMailExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = new IncomingMailForm(); // IncomingMailForm |  (optional) 

            try
            {
                // Import an incoming email. 
                apiInstance.CreateIncomingMail(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateIncomingMail: " + e.Message );
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
 **body** | [**IncomingMailForm**](IncomingMailForm.md)|  | [optional] 

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

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateProject

> void CreateProject (ProjectForm body = null)

create a new project

Creates a new Project Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = new ProjectForm(); // ProjectForm |  (optional) 

            try
            {
                // create a new project
                apiInstance.CreateProject(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateProject: " + e.Message );
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
 **body** | [**ProjectForm**](ProjectForm.md)|  | [optional] 

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
| **201** | Project created |  -  |
| **400** | Project name missing.  Contact id missing. Personal contactId for project employee needed. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateProjectComment

> void CreateProjectComment (long projectId, BlogEntryForm body = null)

Create a new comment.

Creates a new comment. Required profile(s): <i>Projekte, Pinnwand (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateProjectCommentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 10942;  // long | projectId of a Scopevisio project
            var body = new BlogEntryForm(); // BlogEntryForm |  (optional) 

            try
            {
                // Create a new comment.
                apiInstance.CreateProjectComment(projectId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateProjectComment: " + e.Message );
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
 **projectId** | **long**| projectId of a Scopevisio project | 
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
| **400** | Missing projectId. |  -  |
| **404** | No project with the given projectId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateProjectEmployee

> void CreateProjectEmployee (long projectId, ProjectEmployeeForm body)

Create a project employee (Projektteam-Eintrag)

Creates a new project employee (Projektteam-Eintrag) for a Scopevisio project.  Required profile(s): <i>Kontakte, Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateProjectEmployeeExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 789;  // long | 
            var body = new ProjectEmployeeForm(); // ProjectEmployeeForm | content in JSON format

            try
            {
                // Create a project employee (Projektteam-Eintrag)
                apiInstance.CreateProjectEmployee(projectId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateProjectEmployee: " + e.Message );
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
 **projectId** | **long**|  | 
 **body** | [**ProjectEmployeeForm**](ProjectEmployeeForm.md)| content in JSON format | 

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
| **404** | No project with the given id or authorization missing.  No project employee with the given employeeId found. No personal contact with the given contactId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateSalesProject

> void CreateSalesProject (SalesProjectForm body = null)

Create a new sales project (Vertriebsprojekt)

Creates a new sales project (Vertriebsprojekt). The created project has \"sphereLead\" set. It is not possible to convert a  Lead (project with \"sphereLead\" set) to Interessent (\"sphereInterested\" set) via webservice. The conversion must be done via the Scopevisio client. To retrieve a sales project use the endpoint project/{id}.  Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateSalesProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = new SalesProjectForm(); // SalesProjectForm |  (optional) 

            try
            {
                // Create a new sales project (Vertriebsprojekt)
                apiInstance.CreateSalesProject(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateSalesProject: " + e.Message );
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
 **body** | [**SalesProjectForm**](SalesProjectForm.md)|  | [optional] 

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
| **200** | Creates a new sales project. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateTimeEntry

> void CreateTimeEntry (TimeEntryForm body = null)

Create a new timeentry

Creates a new timeentry. Required profile(s): <i>Projekte, Zeiterfassung (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateTimeEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = new TimeEntryForm(); // TimeEntryForm |  (optional) 

            try
            {
                // Create a new timeentry
                apiInstance.CreateTimeEntry(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.CreateTimeEntry: " + e.Message );
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
 **body** | [**TimeEntryForm**](TimeEntryForm.md)|  | [optional] 

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
| **201** | Time entry created |  -  |
| **400** | Employee id missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetAllTimEntries

> Object GetAllTimEntries (string body = null)

Get all timeentries available

Shows all available time entries.  Required profile(s): <i>Zeiterfassung (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetAllTimEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // Get all timeentries available
                Object result = apiInstance.GetAllTimEntries(body);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetAllTimEntries: " + e.Message );
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

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetBlogEntriesByProjectId

> void GetBlogEntriesByProjectId (long projectId, string fields = null)

Comments by projectId.

Returns the Scopevisio comments with the given projectId.  Required profile(s): <i>Projekte, Pinnwand (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetBlogEntriesByProjectIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 789;  // long | Id of a Scopevisio project
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,blogId,body (optional) 

            try
            {
                // Comments by projectId.
                apiInstance.GetBlogEntriesByProjectId(projectId, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetBlogEntriesByProjectId: " + e.Message );
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
 **projectId** | **long**| Id of a Scopevisio project | 
 **fields** | **string**| comma separated list of fields to show in response  id,blogId,body | [optional] 

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
| **200** | Returns the Scopevisio comments with the given projectId |  -  |
| **400** | ProjectId not provided. |  -  |
| **404** | No Project with the given ProjectId found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProject

> void GetProject (long id, string fields = null)

project by id

Returns the Scopevisio project with the given id.  Required profile(s): <i>Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var id = 789;  // long | 
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,number,name (optional) 

            try
            {
                // project by id
                apiInstance.GetProject(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProject: " + e.Message );
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
 **id** | **long**|  | 
 **fields** | **string**| comma separated list of fields to show in response  id,number,name | [optional] 

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
| **200** | Returns the Scopevisio project with the given id. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProjectEmployee

> void GetProjectEmployee (long employeeId, long projectId, string fields = null)

Get project employee (Projektteam-Eintrag) by id

Returns project employee with the given employee id (not contact id).  Required profile(s): <i>Kontakte, Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectEmployeeExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var employeeId = 789;  // long | Id of a Scopevisio project employee
            var projectId = 789;  // long | 
            var fields = fields_example;  // string | Comma separated list of fields to show in response  id,type,subtype (optional) 

            try
            {
                // Get project employee (Projektteam-Eintrag) by id
                apiInstance.GetProjectEmployee(employeeId, projectId, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProjectEmployee: " + e.Message );
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
 **employeeId** | **long**| Id of a Scopevisio project employee | 
 **projectId** | **long**|  | 
 **fields** | **string**| Comma separated list of fields to show in response  id,type,subtype | [optional] 

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

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProjectEmployees

> void GetProjectEmployees (long projectId, string body = null)

project employees (Projektteam) of a given project

Returns the project employees (Projektteam) of a Scopevisio project with the given projectId.  Required profile(s): <i>Kontakte, Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectEmployeesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 789;  // long | id of a Scopevisio project
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // project employees (Projektteam) of a given project
                apiInstance.GetProjectEmployees(projectId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProjectEmployees: " + e.Message );
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
 **projectId** | **long**| id of a Scopevisio project | 
 **body** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | [optional] 

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
| **200** | Returns the project employees (Projektteam) of a  Scopevisio project given projectId. |  -  |
| **404** | No project with the given projectId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProjectResources

> Object GetProjectResources (long projectStartFrom, long projectStartTo, string searchSpecification = null)

Get project resources.

Returns the project resources.  Required profile(s): <i>Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectResourcesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectStartFrom = 789;  // long | After which date the project start date should be. It must be provided as an epoch timestamp in milliseconds.
            var projectStartTo = 789;  // long | Before which date the project start date should be. It must be provided as an epoch timestamp in milliseconds.
            var searchSpecification = searchSpecification_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>.  <b>Please consider: the fields \\\"month\\\",\\\"monthDays\\\",\\\"workDays\\\", \\\"availableMonthDays\\\" are excluded from the search.</b> (optional) 

            try
            {
                // Get project resources.
                Object result = apiInstance.GetProjectResources(projectStartFrom, projectStartTo, searchSpecification);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProjectResources: " + e.Message );
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
 **projectStartFrom** | **long**| After which date the project start date should be. It must be provided as an epoch timestamp in milliseconds. | 
 **projectStartTo** | **long**| Before which date the project start date should be. It must be provided as an epoch timestamp in milliseconds. | 
 **searchSpecification** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;.  &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;monthDays\\\&quot;,\\\&quot;workDays\\\&quot;, \\\&quot;availableMonthDays\\\&quot; are excluded from the search.&lt;/b&gt; | [optional] 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: application/x-www-form-urlencoded
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProjectRevenue

> Object GetProjectRevenue (long projectStartFrom, long projectStartTo, long revenueType = null, string searchSpecification = null)

Get project revenue

Get project revenue.  Required profile(s): <i>Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectRevenueExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectStartFrom = 789;  // long | After which date the project start date should be. It must be provided as an epoch timestamp in milliseconds.
            var projectStartTo = 789;  // long | Before which date the project start date should be. It must be provided as an epoch timestamp in milliseconds.
            var revenueType = 789;  // long | revenueType(Umsatzart), the wished revenue Type. (Umsatzart), 0 = Planung, 1 = Realisierung, 2 = Abweichung, if not provided revenueType is set to \\\"Planung\\\" (optional) 
            var searchSpecification = searchSpecification_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. <b>Please consider: the fields \\\"month\\\",\\\"monthRevenue\\\",\\\"id\\\" are excluded from the search.</b> (optional) 

            try
            {
                // Get project revenue
                Object result = apiInstance.GetProjectRevenue(projectStartFrom, projectStartTo, revenueType, searchSpecification);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProjectRevenue: " + e.Message );
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
 **projectStartFrom** | **long**| After which date the project start date should be. It must be provided as an epoch timestamp in milliseconds. | 
 **projectStartTo** | **long**| Before which date the project start date should be. It must be provided as an epoch timestamp in milliseconds. | 
 **revenueType** | **long**| revenueType(Umsatzart), the wished revenue Type. (Umsatzart), 0 &#x3D; Planung, 1 &#x3D; Realisierung, 2 &#x3D; Abweichung, if not provided revenueType is set to \\\&quot;Planung\\\&quot; | [optional] 
 **searchSpecification** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. &lt;b&gt;Please consider: the fields \\\&quot;month\\\&quot;,\\\&quot;monthRevenue\\\&quot;,\\\&quot;id\\\&quot; are excluded from the search.&lt;/b&gt; | [optional] 

### Return type

**Object**

### Authorization

[oauth](../README.md#oauth)

### HTTP request headers

- **Content-Type**: application/x-www-form-urlencoded
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProjects

> void GetProjects (string body = null)

projects

Returns Scopevisio projects.  Required profile(s): <i>Projekte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetProjectsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // projects
                apiInstance.GetProjects(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetProjects: " + e.Message );
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
| **200** | Returns Scopevisio projects. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTimeEntry

> void GetTimeEntry (long id, string fields = null)

timeEntry for id

Returns the Scopevisio timeEntry with the given id.  Required profile(s): <i>Projekte (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetTimeEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio timeEntry
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,topic (optional) 

            try
            {
                // timeEntry for id
                apiInstance.GetTimeEntry(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.GetTimeEntry: " + e.Message );
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
 **id** | **long**| id of a Scopevisio timeEntry | 
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
| **200** | Returns the Scopevisio timeEntry with the given id. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateProject

> void UpdateProject (long id, ProjectForm body = null)

update project by id

Updates the Scopevisio project with the given id.  Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio Project
            var body = new ProjectForm(); // ProjectForm |  (optional) 

            try
            {
                // update project by id
                apiInstance.UpdateProject(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.UpdateProject: " + e.Message );
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
 **id** | **long**| id of a Scopevisio Project | 
 **body** | [**ProjectForm**](ProjectForm.md)|  | [optional] 

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
| **200** | Updates the Scopevisio project with the given id. |  -  |
| **400** | Project name missing.  Contact id missing. Personal contactId for project employee needed. |  -  |
| **404** | Project not found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateProjectEmployee

> void UpdateProjectEmployee (long projectId, long employeeId, ProjectEmployeeForm body)

update project employee (Projektteam-Eintrag) by employeeId (not contactId).

Updates the employee of a Scopevisio project with the given employeeId.  Required profile(s): <i>Kontakte, Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateProjectEmployeeExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var projectId = 789;  // long | 
            var employeeId = 789;  // long | 
            var body = new ProjectEmployeeForm(); // ProjectEmployeeForm | content in JSON format

            try
            {
                // update project employee (Projektteam-Eintrag) by employeeId (not contactId).
                apiInstance.UpdateProjectEmployee(projectId, employeeId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.UpdateProjectEmployee: " + e.Message );
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
 **projectId** | **long**|  | 
 **employeeId** | **long**|  | 
 **body** | [**ProjectEmployeeForm**](ProjectEmployeeForm.md)| content in JSON format | 

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
| **200** | Updates the employee of a Scopevisio project with the given employeeId. |  -  |
| **404** | No project with the given id or authorization missing.  No project employee with the given employeeId found. No personal contact with the given contactId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateSalesProject

> void UpdateSalesProject (long id, SalesProjectForm body = null)

update sales project (Vertriebsprojekt) by id

Updates the Scopevisio sales project (Vertriebsprojekt) with the given id. It is not possible to convert a  Lead (project \"sphereLead\" set) to Interessent (\"sphereInterested\" set) via webservice. The conversion must be done via the Scopevisio client. Setting the interested fields like intrestedBudget, intrestedAuthority, etc. is only possible if the projectis already Interessent (\"sphereInterested\" set). Otherwise they are overridden by the conversion process. To retrieve a sales project use the endpoint project/{id}.  Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateSalesProjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio Project
            var body = new SalesProjectForm(); // SalesProjectForm |  (optional) 

            try
            {
                // update sales project (Vertriebsprojekt) by id
                apiInstance.UpdateSalesProject(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.UpdateSalesProject: " + e.Message );
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
 **id** | **long**| id of a Scopevisio Project | 
 **body** | [**SalesProjectForm**](SalesProjectForm.md)|  | [optional] 

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
| **200** | Updates the Scopevisio sales project (Vertriebsprojekt) with the given id. |  -  |
| **400** | Project name missing.  Contact id missing. Personal contactId for project employee needed. |  -  |
| **404** | Project not found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateTimeEntry

> void UpdateTimeEntry (long id, TimeEntryForm body = null)

Update timeEntry

Updates the Scopevisio timeEntry with the given timeEntry id. Update may be refused if a time entry run exists for this time entry. In this case the time entry is updated only if the setting in the time entry run settings (see Scopevisio client application) is \"Änderungen zulassen\".  Required profile(s): <i>Projekte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateTimeEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ProjectApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio timeEntry
            var body = new TimeEntryForm(); // TimeEntryForm | Content in JSON format. Please refer to TimeentryForm for example values and to our  <a href='../browser/index.html#!/documentation'>general documentation</a> for update information. (optional) 

            try
            {
                // Update timeEntry
                apiInstance.UpdateTimeEntry(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ProjectApi.UpdateTimeEntry: " + e.Message );
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
 **id** | **long**| id of a Scopevisio timeEntry | 
 **body** | [**TimeEntryForm**](TimeEntryForm.md)| Content in JSON format. Please refer to TimeentryForm for example values and to our  &lt;a href&#x3D;&#39;../browser/index.html#!/documentation&#39;&gt;general documentation&lt;/a&gt; for update information. | [optional] 

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
| **200** | Updates the Scopevisio timeEntry with the given timeEntryId. |  -  |
| **400** | There is a time entry run for the time Entry with the given id |  -  |
| **404** | TimeEntry not found or authorization missing |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

