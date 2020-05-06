# CompuMaster.Scopevisio.OpenApi.Api.ContactApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddContactFile**](ContactApi.md#addcontactfile) | **POST** /contact/{contactIdOrLegacyNumber}/file/new | Add a file to a Scopevisio contact
[**AssignMailContact**](ContactApi.md#assignmailcontact) | **POST** /contact/{contactIdOrLegacyNumber}/assignMail | Assign mail to a contact.
[**CreateContact**](ContactApi.md#createcontact) | **POST** /contact/new | create a new contact
[**CreateContactComment**](ContactApi.md#createcontactcomment) | **POST** /contact/{contactId}/comment/new | Create a new comment.
[**CreateProperty**](ContactApi.md#createproperty) | **POST** /contact/{contactIdOrLegacyNumber}/contactProperty/new | Create a contact property (allgemeine Information)
[**DeleteContactById**](ContactApi.md#deletecontactbyid) | **DELETE** /contact/{contactIdOrLegacyNumber} | Delete a contact by contactId or legacyNumber
[**GetBlogEntriesByContactId**](ContactApi.md#getblogentriesbycontactid) | **GET** /contact/{contactIdOrLegacyNumber}/comments | Comments by contactIdOrLegacyNumber.
[**GetContactById**](ContactApi.md#getcontactbyid) | **GET** /contact/{id} | Get contact by id
[**GetContactByKeyIdentifierId**](ContactApi.md#getcontactbykeyidentifierid) | **GET** /contact/{keyIdentifier}/{id} | Get contact by id matching the field stated as key identifier
[**GetContactEmployees**](ContactApi.md#getcontactemployees) | **GET** /contact/{id}/employees | employees of an organisation contact
[**GetContactEmployers**](ContactApi.md#getcontactemployers) | **GET** /contact/{id}/employers | employers of a personal contact
[**GetContactProperties**](ContactApi.md#getcontactproperties) | **GET** /contact/{id}/properties | contact propeties for the given contact
[**GetContacts**](ContactApi.md#getcontacts) | **POST** /contacts | contacts
[**GetImage**](ContactApi.md#getimage) | **GET** /contact/{id}/thumb | thumbnail image of contact
[**GetProperty**](ContactApi.md#getproperty) | **GET** /contact/contactProperty/{propertyId} | Get contact property (allgemeine Information) by id
[**UpdateContactById**](ContactApi.md#updatecontactbyid) | **POST** /contact/{id} | update contact by id
[**UpdateProperty**](ContactApi.md#updateproperty) | **POST** /contact/contactProperty/{propertyId} | Update contact property (allgemeine Information) by id



## AddContactFile

> void AddContactFile (string contactIdOrLegacyNumber, TeamworkFileForm body = null)

Add a file to a Scopevisio contact

Adds a file to a Scopevisio contact entry.  Required profile(s): <i>Kontakte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AddContactFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactIdOrLegacyNumber = 10942;  // string | contactId or legacyNumber of a Scopevisio contact
            var body = new TeamworkFileForm(); // TeamworkFileForm |  (optional) 

            try
            {
                // Add a file to a Scopevisio contact
                apiInstance.AddContactFile(contactIdOrLegacyNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.AddContactFile: " + e.Message );
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
 **contactIdOrLegacyNumber** | **string**| contactId or legacyNumber of a Scopevisio contact | 
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
| **404** | No contact for the given contactIdOrLegacyNumber found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## AssignMailContact

> void AssignMailContact (string contactIdOrLegacyNumber, AssignMailForm body)

Assign mail to a contact.

Assigns the mail with the given mailId to the contact with the given contactIdOrLegacyNumber.  Required profile(s): <i>Kontakte, Mailbot, Mailagent (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AssignMailContactExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactIdOrLegacyNumber = contactIdOrLegacyNumber_example;  // string | 
            var body = new AssignMailForm(); // AssignMailForm | content in JSON format

            try
            {
                // Assign mail to a contact.
                apiInstance.AssignMailContact(contactIdOrLegacyNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.AssignMailContact: " + e.Message );
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
 **contactIdOrLegacyNumber** | **string**|  | 
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
| **404** | No mail with the given mailId found. No contact with the given contactIdOrLegacyNumber found. |  -  |
| **500** | Mail not assigned. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateContact

> void CreateContact (KontaktForm body = null)

create a new contact

Creates a new Scopevisio Contact.  Required profile(s): <i>Kontakte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateContactExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var body = new KontaktForm(); // KontaktForm |  (optional) 

            try
            {
                // create a new contact
                apiInstance.CreateContact(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.CreateContact: " + e.Message );
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
 **body** | [**KontaktForm**](KontaktForm.md)|  | [optional] 

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
| **201** | Contact created |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateContactComment

> void CreateContactComment (long contactId, BlogEntryForm body = null)

Create a new comment.

Creates a new comment. Required profile(s): <i>Kontakte, Pinnwand (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateContactCommentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactId = 10942;  // long | contactId of a Scopevisio contact
            var body = new BlogEntryForm(); // BlogEntryForm |  (optional) 

            try
            {
                // Create a new comment.
                apiInstance.CreateContactComment(contactId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.CreateContactComment: " + e.Message );
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
 **contactId** | **long**| contactId of a Scopevisio contact | 
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
| **400** | Missing contactId. |  -  |
| **404** | No contact with the given contactId found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateProperty

> void CreateProperty (string contactIdOrLegacyNumber, ContactPropertyForm body)

Create a contact property (allgemeine Information)

Creates a new property (allgemeine Information) for a Scopevisio contact.  Required profile(s): <i>Kontakte bearbeiten (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreatePropertyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactIdOrLegacyNumber = contactIdOrLegacyNumber_example;  // string | 
            var body = new ContactPropertyForm(); // ContactPropertyForm | content in JSON format

            try
            {
                // Create a contact property (allgemeine Information)
                apiInstance.CreateProperty(contactIdOrLegacyNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.CreateProperty: " + e.Message );
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
 **contactIdOrLegacyNumber** | **string**|  | 
 **body** | [**ContactPropertyForm**](ContactPropertyForm.md)| content in JSON format | 

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
| **201** | Contact property created |  -  |
| **403** | Contact not found or the user does not have read permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DeleteContactById

> void DeleteContactById (string contactIdOrLegacyNumber)

Delete a contact by contactId or legacyNumber

Delets a Scopevisio contact.  Required profile(s): <i>Kontakte (Bearbeiten)</i>.   <strong>If the contact has the sphereEmployee set then you needthe additional user profile <em>Personal</em></strong>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class DeleteContactByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactIdOrLegacyNumber = 10942;  // string | contactId or legacyNumber of a Scopevisio contact

            try
            {
                // Delete a contact by contactId or legacyNumber
                apiInstance.DeleteContactById(contactIdOrLegacyNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.DeleteContactById: " + e.Message );
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
 **contactIdOrLegacyNumber** | **string**| contactId or legacyNumber of a Scopevisio contact | 

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
| **403** | Access denied. |  -  |
| **404** | No contact for the given contactIdOrLegacyNumber found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetBlogEntriesByContactId

> void GetBlogEntriesByContactId (string contactIdOrLegacyNumber, string fields = null)

Comments by contactIdOrLegacyNumber.

Returns the Scopevisio comments with the given contactIdOrLegacyNumber.  Required profile(s): <i>Kontakte, Pinnwand (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetBlogEntriesByContactIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var contactIdOrLegacyNumber = contactIdOrLegacyNumber_example;  // string | id (Master ID) or legacyNumber (ID Vorsystem) of a Scopevisio contact
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,blogId,body (optional) 

            try
            {
                // Comments by contactIdOrLegacyNumber.
                apiInstance.GetBlogEntriesByContactId(contactIdOrLegacyNumber, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetBlogEntriesByContactId: " + e.Message );
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
 **contactIdOrLegacyNumber** | **string**| id (Master ID) or legacyNumber (ID Vorsystem) of a Scopevisio contact | 
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
| **200** | Returns the Scopevisio comments with the given contactIdOrLegacyNumber. |  -  |
| **400** | contactIdOrLegacyNumber not provided. |  -  |
| **404** | No contact with the given contactIdOrLegacyNumber found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContactById

> void GetContactById (long id, string fields = null)

Get contact by id

Returns the Scopevisio contact with the given id.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio contact
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, lastname (optional) 

            try
            {
                // Get contact by id
                apiInstance.GetContactById(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContactById: " + e.Message );
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
 **id** | **long**| id of a Scopevisio contact | 
 **fields** | **string**| comma separated list of fields to show in response  id, lastname | [optional] 

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
| **200** | The contact with the given id was found. |  -  |
| **400** | The contact to update was not found or the user has no read permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContactByKeyIdentifierId

> void GetContactByKeyIdentifierId (string keyIdentifier, string id, string fields = null)

Get contact by id matching the field stated as key identifier

Returns the Scopevisio contact with the given id matching the field stated as key identifier. If more than one contact is found for the given legacyNumber, then a HTTP 404 error will be returned. Please use the endpoint /contacts if you do not have distinct values for your key identfiers. Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactByKeyIdentifierIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var keyIdentifier = keyIdentifier_example;  // string | key identifier [ID, LEGACYNUMBER], default key identifier: ID (default to ID)
            var id = id_example;  // string | id or legacyNumber of a Scopevisio contact, depending on the key identifier context
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, lastname (optional) 

            try
            {
                // Get contact by id matching the field stated as key identifier
                apiInstance.GetContactByKeyIdentifierId(keyIdentifier, id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContactByKeyIdentifierId: " + e.Message );
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
 **keyIdentifier** | **string**| key identifier [ID, LEGACYNUMBER], default key identifier: ID | [default to ID]
 **id** | **string**| id or legacyNumber of a Scopevisio contact, depending on the key identifier context | 
 **fields** | **string**| comma separated list of fields to show in response  id, lastname | [optional] 

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
| **200** | The contact with the given id matching the field stated as key identifier was found. |  -  |
| **400** | The contact was not found or the user has no read permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContactEmployees

> void GetContactEmployees (long id, string fields = null)

employees of an organisation contact

Returns the employees of the Scopevisio organisation contact with the given id.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactEmployeesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio organisation contact
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,contactId,lastname (optional) 

            try
            {
                // employees of an organisation contact
                apiInstance.GetContactEmployees(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContactEmployees: " + e.Message );
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
 **id** | **long**| id of a Scopevisio organisation contact | 
 **fields** | **string**| comma separated list of fields to show in response  id,contactId,lastname | [optional] 

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
| **200** | Returns the contact employees. |  -  |
| **400** | The contact with the given id was not found or the user does not have readpermissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContactEmployers

> void GetContactEmployers (long id, string fields = null)

employers of a personal contact

Returns the employers of the Scopevisio personal contact with the given id.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactEmployersExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio personal contact
            var fields = fields_example;  // string | comma separated list of fields to show in response  id,contactId,company (optional) 

            try
            {
                // employers of a personal contact
                apiInstance.GetContactEmployers(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContactEmployers: " + e.Message );
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
 **id** | **long**| id of a Scopevisio personal contact | 
 **fields** | **string**| comma separated list of fields to show in response  id,contactId,company | [optional] 

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
| **200** | Returns the employers fo the contact with the given id. |  -  |
| **400** | The contact with the given id was not found or the user does not have readpermissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContactProperties

> void GetContactProperties (long id, string fields = null)

contact propeties for the given contact

Returns the Scopevisio contact custom property fields with the given id.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactPropertiesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio contact
            var fields = fields_example;  // string | comma separated list of fields to show in response  id, description (optional) 

            try
            {
                // contact propeties for the given contact
                apiInstance.GetContactProperties(id, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContactProperties: " + e.Message );
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
 **id** | **long**| id of a Scopevisio contact | 
 **fields** | **string**| comma separated list of fields to show in response  id, description | [optional] 

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
| **200** | Returns the contact properties. |  -  |
| **403** | The user does not have read rights for the contact. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetContacts

> void GetContacts (string body = null)

contacts

Returns Scopevisio contacts.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetContactsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // contacts
                apiInstance.GetContacts(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetContacts: " + e.Message );
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
| **200** | Returns the Scopevisio contacts. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetImage

> void GetImage (long id)

thumbnail image of contact

Returns the thumb of a Scopevisio contact with the given id.  Required profile(s): <i>Kontakte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetImageExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio contact

            try
            {
                // thumbnail image of contact
                apiInstance.GetImage(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetImage: " + e.Message );
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
 **id** | **long**| id of a Scopevisio contact | 

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
| **200** | Returns the contact image |  -  |
| **404** | Contact not found or the user does not have read permissions for it. Contact thumb not found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetProperty

> void GetProperty (long propertyId, string fields = null)

Get contact property (allgemeine Information) by id

Returns the contact property (allgemeine Information) with the given propertyId of the given contactId.  Required profile(s): <i>Kontakte bearbeiten (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetPropertyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var propertyId = 789;  // long | id of a Scopevisio property
            var fields = fields_example;  // string | Comma separated list of fields to show in response  id,type,subType (optional) 

            try
            {
                // Get contact property (allgemeine Information) by id
                apiInstance.GetProperty(propertyId, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.GetProperty: " + e.Message );
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
 **propertyId** | **long**| id of a Scopevisio property | 
 **fields** | **string**| Comma separated list of fields to show in response  id,type,subType | [optional] 

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


## UpdateContactById

> void UpdateContactById (long id, KontaktForm body = null)

update contact by id

Updates the Scopevisio Contact with the given id.  Required profile(s): <i>Kontakte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateContactByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var id = 789;  // long | id of a Contact
            var body = new KontaktForm(); // KontaktForm | Content in JSON format. Please refer to KontaktForm for example values and to our  <a href='../browser/index.html#!/documentation'>general documentation</a> for update information. (optional) 

            try
            {
                // update contact by id
                apiInstance.UpdateContactById(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.UpdateContactById: " + e.Message );
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
 **id** | **long**| id of a Contact | 
 **body** | [**KontaktForm**](KontaktForm.md)| Content in JSON format. Please refer to KontaktForm for example values and to our  &lt;a href&#x3D;&#39;../browser/index.html#!/documentation&#39;&gt;general documentation&lt;/a&gt; for update information. | [optional] 

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
| **200** | The contact was successfully updated. |  -  |
| **404** | The contact to update was not found or the user has no write permissions for it. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateProperty

> void UpdateProperty (long propertyId, ContactPropertyForm body)

Update contact property (allgemeine Information) by id

Updates the property (allgemeine Information) of a Scopevisio contact with the given propertyId.  Required profile(s): <i>Kontakte bearbeiten (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdatePropertyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ContactApi(Configuration.Default);
            var propertyId = 789;  // long | 
            var body = new ContactPropertyForm(); // ContactPropertyForm | content in JSON format

            try
            {
                // Update contact property (allgemeine Information) by id
                apiInstance.UpdateProperty(propertyId, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ContactApi.UpdateProperty: " + e.Message );
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
 **propertyId** | **long**|  | 
 **body** | [**ContactPropertyForm**](ContactPropertyForm.md)| content in JSON format | 

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
| **200** | Contact updated |  -  |
| **403** | The user does not have write permissions for the contact property.   The user does not have wrie permissions for the contact. |  -  |
| **404** | Contact property not found |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

