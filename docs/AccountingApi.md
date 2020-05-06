# CompuMaster.Scopevisio.OpenApi.Api.AccountingApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddDimensionEntry**](AccountingApi.md#adddimensionentry) | **POST** /dimensions/{dimensionNameOrNumber}/dimensionentry/new | Add a dimensionEntry to a dimension
[**AddFile**](AccountingApi.md#addfile) | **POST** /journal/{documentNumber}/file/new | Add a file to a Scopevisio journal entry
[**CancelPostingDocument**](AccountingApi.md#cancelpostingdocument) | **POST** /journal/{documentNumber}/cancel | Cancels the posting with the given documentNumber.
[**CreateDebitor**](AccountingApi.md#createdebitor) | **POST** /createdebitor | Create debitor for a given contact, if the contact is not already a debitor.
[**CreateDebitorBankConnection**](AccountingApi.md#createdebitorbankconnection) | **POST** /debitoraccounts/{accountNumber}/bankConnection/new | Create a new bank connection for debitor.
[**CreateImpersonalAcocunts**](AccountingApi.md#createimpersonalacocunts) | **POST** /createimpersonalaccounts | import impersonalaccounts
[**CreateKebitorBankConnection**](AccountingApi.md#createkebitorbankconnection) | **POST** /kreditoraccounts/{accountNumber}/bankConnection/new | Create a new bank connection for kreditor.
[**CreateKreditor**](AccountingApi.md#createkreditor) | **POST** /createkreditor | Create vendor for a given contact, if the contact is not already a vendor
[**CreatePaymentTerm**](AccountingApi.md#createpaymentterm) | **POST** /paymentterm/new | Create a new payment term.
[**CreatePostings**](AccountingApi.md#createpostings) | **POST** /postings/new | import postings
[**ExportDatev**](AccountingApi.md#exportdatev) | **POST** /datevexport | export postings in DATEV EXTF format
[**GetAccountingInfos**](AccountingApi.md#getaccountinginfos) | **GET** /accountinginfo | Returns accounting info details.
[**GetAllDimensionEntries**](AccountingApi.md#getalldimensionentries) | **POST** /dimensions | dimensions
[**GetBankConnectionsForDebitor**](AccountingApi.md#getbankconnectionsfordebitor) | **GET** /debitoraccounts/{accountNumber}/bankConnections | Bank connections for a Scopevisio debitor account.
[**GetBankConnectionsForKreditor**](AccountingApi.md#getbankconnectionsforkreditor) | **GET** /kreditoraccounts/{accountNumber}/bankConnections | Bank connections for a Scopevisio kreditor account.
[**GetDebitorAccounts**](AccountingApi.md#getdebitoraccounts) | **POST** /debitoraccounts | debitor accounts
[**GetDimensionReportEntries**](AccountingApi.md#getdimensionreportentries) | **GET** /dimensions/{dimensionNameOrNumber}/dimensionentries | dimension entries
[**GetGeneralJournalEntryFile**](AccountingApi.md#getgeneraljournalentryfile) | **GET** /journal/{documentNumber}/file | Returns file for a journal entry if existing
[**GetImpersonalAccounts**](AccountingApi.md#getimpersonalaccounts) | **POST** /impersonalaccounts | impersonal accounts
[**GetJournalEntries**](AccountingApi.md#getjournalentries) | **POST** /journal | general journal entries
[**GetJournalEntryByDocumentNumber**](AccountingApi.md#getjournalentrybydocumentnumber) | **GET** /journal/{documentNumber} | Posting by documentNumber
[**GetKreditorAccounts**](AccountingApi.md#getkreditoraccounts) | **POST** /kreditoraccounts | kreditor accounts
[**GetPaymentTerm**](AccountingApi.md#getpaymentterm) | **GET** /paymentterm/{id} | Gets payment term for id.
[**GetPaymentTerms**](AccountingApi.md#getpaymentterms) | **GET** /paymentterms | Get payment terms..
[**GetPersonalJournalEntries**](AccountingApi.md#getpersonaljournalentries) | **POST** /personaljournal | personal journal entries
[**GetTextTemplates**](AccountingApi.md#gettexttemplates) | **GET** /texttemplates | Get text templates (Textbausteine)
[**GetVatMatrixEntries**](AccountingApi.md#getvatmatrixentries) | **GET** /vatmatrixentries | vat matrix
[**GetZones**](AccountingApi.md#getzones) | **POST** /zones | zones
[**ImportDatevData**](AccountingApi.md#importdatevdata) | **POST** /datevpostings/new | import postings in DATEV-Format
[**UpdateDebitor**](AccountingApi.md#updatedebitor) | **POST** /debitoraccounts/{accountNumber} | Update a debitor by accountNumber (Kontonummer).
[**UpdateDimensionEntry**](AccountingApi.md#updatedimensionentry) | **POST** /dimensions/{dimensionNameOrNumber}/dimensionentry | Updates the name of a dimension entry.
[**UpdateKreditor**](AccountingApi.md#updatekreditor) | **POST** /kreditoraccounts/{accountNumber} | Update a kreditor by accountNumber (Kontonummer).
[**UpdatePaymentTerm**](AccountingApi.md#updatepaymentterm) | **POST** /paymentterm/{id} | Update payment term.



## AddDimensionEntry

> void AddDimensionEntry (string dimensionNameOrNumber, DimensionEntryForm body)

Add a dimensionEntry to a dimension

Adds a new dimension entry to a dimension.  Required profile(s): <i>Berichte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AddDimensionEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var dimensionNameOrNumber = 1;  // string | the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries
            var body = new DimensionEntryForm(); // DimensionEntryForm | 

            try
            {
                // Add a dimensionEntry to a dimension
                apiInstance.AddDimensionEntry(dimensionNameOrNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.AddDimensionEntry: " + e.Message );
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
 **dimensionNameOrNumber** | **string**| the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries | 
 **body** | [**DimensionEntryForm**](DimensionEntryForm.md)|  | 

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
| **201** | Dimension entry created |  -  |
| **400** | \&quot;number\&quot; not given, \&quot;name\&quot; not given or dimension entry with the given number already exists. |  -  |
| **404** | No dimension for the given dimensionNameOrNumber found or authorization missing |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## AddFile

> void AddFile (string documentNumber, FileForm body = null)

Add a file to a Scopevisio journal entry

Adds a file to a Scopevisio journal entry.  Required profile(s): <i>Journal (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class AddFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var documentNumber = 2018-00942;  // string | document number of a Scopevisio general journal entry
            var body = new FileForm(); // FileForm |  (optional) 

            try
            {
                // Add a file to a Scopevisio journal entry
                apiInstance.AddFile(documentNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.AddFile: " + e.Message );
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
 **documentNumber** | **string**| document number of a Scopevisio general journal entry | 
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
| **404** | No journal entry for the given number found or authorization missing |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CancelPostingDocument

> void CancelPostingDocument (string documentNumber)

Cancels the posting with the given documentNumber.

Cancels the posting with the given documentNumber. If the posting is already cancelled no error is thrown but the cancellation posting document number is returned.  Required profiles: <i>Journal (Bearbeiten)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CancelPostingDocumentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var documentNumber = 2018-00942;  // string | Document number (Buchungsnummer) of a Scopevisio general journal entry.

            try
            {
                // Cancels the posting with the given documentNumber.
                apiInstance.CancelPostingDocument(documentNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CancelPostingDocument: " + e.Message );
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
 **documentNumber** | **string**| Document number (Buchungsnummer) of a Scopevisio general journal entry. | 

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
| **200** | The posting with the given documentNumber has already been or was just successfully cancelled. The posting with the given documentNumber could not be cancelled because of validation errors: The posting period is closed,  the accounts are locked, etc. In this case please consider the errors in the response. |  -  |
| **400** | The posting with the given documentNumber is a cancellation posting and cannot be cancelled. |  -  |
| **404** | The posting with the given documentNumber was not found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateDebitor

> void CreateDebitor (PersonalAccountForm body = null)

Create debitor for a given contact, if the contact is not already a debitor.

Required profiles: <i>Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten)</i>.  At least one of the parameters in the body description below is needed.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateDebitorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new PersonalAccountForm(); // PersonalAccountForm |  (optional) 

            try
            {
                // Create debitor for a given contact, if the contact is not already a debitor.
                apiInstance.CreateDebitor(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreateDebitor: " + e.Message );
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
 **body** | [**PersonalAccountForm**](PersonalAccountForm.md)|  | [optional] 

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
| **200** | Customer not created. A customer for the given contactId already exists. The existing personal account number for the given contactId is returned. |  -  |
| **201** | Customer created. |  -  |
| **400** | Account already exists. |  -  |
| **404** | Contact not found or authorization missing. PersonalAccountNumber does not have the correct format or is not within the allowed number range. |  -  |
| **500** | No  number ranges found in accounting area. Data is broken or accounting is not set up for the organisation. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateDebitorBankConnection

> Object CreateDebitorBankConnection (string accountNumber, BankConnectionForm body = null)

Create a new bank connection for debitor.

Creates a new bank connection for the debitor with the given accountNumber  Required profile(s): <i>Stammdaten, Debitorenstamm (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateDebitorBankConnectionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = 10002;  // string | accountNumber (Kontonummer)of a Scopevisio debitor
            var body = new BankConnectionForm(); // BankConnectionForm |  (optional) 

            try
            {
                // Create a new bank connection for debitor.
                Object result = apiInstance.CreateDebitorBankConnection(accountNumber, body);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreateDebitorBankConnection: " + e.Message );
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
 **accountNumber** | **string**| accountNumber (Kontonummer)of a Scopevisio debitor | 
 **body** | [**BankConnectionForm**](BankConnectionForm.md)|  | [optional] 

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
| **201** | Bank connection created. |  -  |
| **400** | Missing debitorAccount.   Missing iban or bank account number and bank code. |  -  |
| **404** | No debitor with the given accountNumber found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateImpersonalAcocunts

> void CreateImpersonalAcocunts (ImpersonalAccountsForm body = null)

import impersonalaccounts

Required profiles: <i>Datenimport (Bearbeiten)</i>. The examples and allowable values refer to chart of accounts SKR03. If you use another chart of accounts you should check your settings in the Scopevisio desktop application.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateImpersonalAcocuntsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new ImpersonalAccountsForm(); // ImpersonalAccountsForm |  (optional) 

            try
            {
                // import impersonalaccounts
                apiInstance.CreateImpersonalAcocunts(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreateImpersonalAcocunts: " + e.Message );
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
 **body** | [**ImpersonalAccountsForm**](ImpersonalAccountsForm.md)|  | [optional] 

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
| **200** | Impersonal account created or updated. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateKebitorBankConnection

> Object CreateKebitorBankConnection (string accountNumber, BankConnectionForm body = null)

Create a new bank connection for kreditor.

Creates a new bank connection for the kreditor with the given accountNumber  Required profile(s): <i>Stammdaten, Kreditorenstamm (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateKebitorBankConnectionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = 70008;  // string | accountNumber (Kontonummer)of a Scopevisio kreditor
            var body = new BankConnectionForm(); // BankConnectionForm |  (optional) 

            try
            {
                // Create a new bank connection for kreditor.
                Object result = apiInstance.CreateKebitorBankConnection(accountNumber, body);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreateKebitorBankConnection: " + e.Message );
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
 **accountNumber** | **string**| accountNumber (Kontonummer)of a Scopevisio kreditor | 
 **body** | [**BankConnectionForm**](BankConnectionForm.md)|  | [optional] 

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
| **201** | Bank connection created. |  -  |
| **400** | Missing kreditorAccount.   Missing iban or bank account number and bank code. |  -  |
| **404** | No kreditor with the given accountNumber found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreateKreditor

> void CreateKreditor (PersonalAccountForm body = null)

Create vendor for a given contact, if the contact is not already a vendor

Required profile(s): <i>Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten)</i>.   At least one of the parameters in the body description below is needed.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreateKreditorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new PersonalAccountForm(); // PersonalAccountForm |  (optional) 

            try
            {
                // Create vendor for a given contact, if the contact is not already a vendor
                apiInstance.CreateKreditor(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreateKreditor: " + e.Message );
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
 **body** | [**PersonalAccountForm**](PersonalAccountForm.md)|  | [optional] 

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
| **200** | Kreditor was not created, because the contact is already a kreditor. The kreditor was not created because the number range has probably no free numbers anymore. |  -  |
| **201** | Kreditor created. |  -  |
| **400** | Account already exists. |  -  |
| **404** | Contact not found or authorization missing. PersonalAccountNumber does not have the correct format or is not within the allowed number range. |  -  |
| **500** | No  number ranges found in accounting area. Data is broken or accounting is not set up for the organisation. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreatePaymentTerm

> Object CreatePaymentTerm (PaymentTermForm body = null)

Create a new payment term.

Creates a new payment term.  Required profile(s): <i>Stammdaten, Zahlungsbedingungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreatePaymentTermExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new PaymentTermForm(); // PaymentTermForm |  (optional) 

            try
            {
                // Create a new payment term.
                Object result = apiInstance.CreatePaymentTerm(body);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreatePaymentTerm: " + e.Message );
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
 **body** | [**PaymentTermForm**](PaymentTermForm.md)|  | [optional] 

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
| **201** | Payment term created. |  -  |
| **400** | Payment term name missing.  A payment term with the given name already exists.   Payment term nettimelimit missing. netTimeLimit is less or equal cashDiscountDays1 and/or cashDiscountDays1. percentage2 is greater than percentage1.   percentage2 or percentage1 are more than 100. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CreatePostings

> void CreatePostings (Postings body = null)

import postings

Required profiles: <i>Datenimport (Bearbeiten)</i>. The examples and allowable values refer to chart of accounts SKR03. If you use another chart of accounts you should check your settings in the Scopevisio desktop application.  To import a posting two or more postings rows are needed. More information and data structure can be found inside the Scopevisio client

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class CreatePostingsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new Postings(); // Postings |  (optional) 

            try
            {
                // import postings
                apiInstance.CreatePostings(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.CreatePostings: " + e.Message );
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
 **body** | [**Postings**](Postings.md)|  | [optional] 

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
| **200** | Create a single posting. See response for error messages. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ExportDatev

> void ExportDatev (DatevExportSettingsForm body = null)

export postings in DATEV EXTF format

Required profiles: <i>Datenexport (Anzeigen)</i>. Export postings in DATEV EXTF format.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ExportDatevExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new DatevExportSettingsForm(); // DatevExportSettingsForm |  (optional) 

            try
            {
                // export postings in DATEV EXTF format
                apiInstance.ExportDatev(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.ExportDatev: " + e.Message );
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
 **body** | [**DatevExportSettingsForm**](DatevExportSettingsForm.md)|  | [optional] 

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
| **200** | Export Postings in DATEV Format. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetAccountingInfos

> void GetAccountingInfos ()

Returns accounting info details.

Required profiles: <i>Stammdaten (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetAccountingInfosExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);

            try
            {
                // Returns accounting info details.
                apiInstance.GetAccountingInfos();
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetAccountingInfos: " + e.Message );
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
| **200** | Successfully returns accounting info details. |  -  |
| **500** | Accounting is not set up for the organisation. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetAllDimensionEntries

> void GetAllDimensionEntries (string body = null)

dimensions

Returns all of the dimensions.  Required profile(s): <i>Journal (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetAllDimensionEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // dimensions
                apiInstance.GetAllDimensionEntries(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetAllDimensionEntries: " + e.Message );
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
| **200** | Returns the dimensions. |  -  |
| **404** | No dimensions found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetBankConnectionsForDebitor

> void GetBankConnectionsForDebitor (string accountNumber, string fields = null)

Bank connections for a Scopevisio debitor account.

Returns the bank connections for the debitor account with the given accountNumber.  Required profile(s): <i>Stammdaten, Debitorenstamm (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetBankConnectionsForDebitorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = accountNumber_example;  // string | accountNumber (Kontonummer) of a Scopevisio debitor.
            var fields = fields_example;  // string | comma separated list of fields to show in response  documentNumber, postingDate (optional) 

            try
            {
                // Bank connections for a Scopevisio debitor account.
                apiInstance.GetBankConnectionsForDebitor(accountNumber, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetBankConnectionsForDebitor: " + e.Message );
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
 **accountNumber** | **string**| accountNumber (Kontonummer) of a Scopevisio debitor. | 
 **fields** | **string**| comma separated list of fields to show in response  documentNumber, postingDate | [optional] 

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
| **200** | Returns the bank connections for the debitor with the given accountNumber. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetBankConnectionsForKreditor

> void GetBankConnectionsForKreditor (string accountNumber, string fields = null)

Bank connections for a Scopevisio kreditor account.

Returns the bank connections for the kreditor account with the given accountNumber.  Required profile(s): <i>Stammdaten, Kreditorenstamm (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetBankConnectionsForKreditorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = accountNumber_example;  // string | accountNumber (Kontonummer) of a Scopevisio kreditor.
            var fields = fields_example;  // string | comma separated list of fields to show in response  documentNumber, postingDate (optional) 

            try
            {
                // Bank connections for a Scopevisio kreditor account.
                apiInstance.GetBankConnectionsForKreditor(accountNumber, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetBankConnectionsForKreditor: " + e.Message );
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
 **accountNumber** | **string**| accountNumber (Kontonummer) of a Scopevisio kreditor. | 
 **fields** | **string**| comma separated list of fields to show in response  documentNumber, postingDate | [optional] 

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
| **200** | Returns the bank connections for the kreditor with the given accountNumber. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetDebitorAccounts

> void GetDebitorAccounts (string body)

debitor accounts

Returns the Debitor-Accounts  Required profile(s): <i>Stammdaten, Debitorenstamm (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetDebitorAccountsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>.

            try
            {
                // debitor accounts
                apiInstance.GetDebitorAccounts(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetDebitorAccounts: " + e.Message );
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
 **body** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | 

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
| **200** | Returns the debitor accounts |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetDimensionReportEntries

> void GetDimensionReportEntries (string dimensionNameOrNumber)

dimension entries

Returns the entries for a given dimension.  Required profile(s): <i>Berichte (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetDimensionReportEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var dimensionNameOrNumber = Kostenstellen;  // string | the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries

            try
            {
                // dimension entries
                apiInstance.GetDimensionReportEntries(dimensionNameOrNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetDimensionReportEntries: " + e.Message );
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
 **dimensionNameOrNumber** | **string**| the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries | 

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
| **200** | Returns the dimension entries for the given dimension |  -  |
| **404** | Dimension name not given. The dimension with the given name was not found. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetGeneralJournalEntryFile

> void GetGeneralJournalEntryFile (string documentNumber)

Returns file for a journal entry if existing

Returns the file of the general journal entry with the given document number.  Required profile(s): <i>Journal (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetGeneralJournalEntryFileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var documentNumber = 2018-00942;  // string | document number of a Scopevisio general journal entry

            try
            {
                // Returns file for a journal entry if existing
                apiInstance.GetGeneralJournalEntryFile(documentNumber);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetGeneralJournalEntryFile: " + e.Message );
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
 **documentNumber** | **string**| document number of a Scopevisio general journal entry | 

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
| **200** | Returns the file for the journal entry with the given documentNumber. |  -  |
| **404** | No journal entry for the given number found or authorization missing. No file for the given documentNumber found. Not outgoing or incoming invoice found or authorization missing. No cashbook entry for the given documentNumber found. |  -  |
| **500** | There is a file name in the journal entry, but no reference to the file itself. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetImpersonalAccounts

> void GetImpersonalAccounts (string body)

impersonal accounts

Returns the impersonalaccounts  Required profile(s): <i>Stammdaten, Kontenplan (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetImpersonalAccountsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>.

            try
            {
                // impersonal accounts
                apiInstance.GetImpersonalAccounts(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetImpersonalAccounts: " + e.Message );
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
 **body** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | 

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
| **200** | Returns the impersonal accounts |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetJournalEntries

> void GetJournalEntries (string body = null)

general journal entries

Returns the Scopevisio journal entries.  Required profile(s): <i>Journal (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetJournalEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // general journal entries
                apiInstance.GetJournalEntries(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetJournalEntries: " + e.Message );
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
| **200** | Returns the Scopevisio journal entries. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetJournalEntryByDocumentNumber

> void GetJournalEntryByDocumentNumber (string documentNumber, string fields = null)

Posting by documentNumber

Returns the Scopevisio posting rows with the given documentNumber.  Required profile(s): <i>Journal (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetJournalEntryByDocumentNumberExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var documentNumber = documentNumber_example;  // string | documentNumber (Buchungsnummer) of a scopevisio posting.
            var fields = fields_example;  // string | comma separated list of fields to show in response  documentNumber, postingDate (optional) 

            try
            {
                // Posting by documentNumber
                apiInstance.GetJournalEntryByDocumentNumber(documentNumber, fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetJournalEntryByDocumentNumber: " + e.Message );
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
 **documentNumber** | **string**| documentNumber (Buchungsnummer) of a scopevisio posting. | 
 **fields** | **string**| comma separated list of fields to show in response  documentNumber, postingDate | [optional] 

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
| **200** | Returns the Scopevisio posting rows with the given documentNumber. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetKreditorAccounts

> void GetKreditorAccounts (string body)

kreditor accounts

Returns the Kreditor-Accounts  Required profile(s): <i>Stammdaten, Kreditorenstamm (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetKreditorAccountsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>.

            try
            {
                // kreditor accounts
                apiInstance.GetKreditorAccounts(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetKreditorAccounts: " + e.Message );
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
 **body** | **string**| search filter in JSON format. See also &lt;a href&#x3D;&#39;../browser/index.html#!/searchscope&#39;&gt;our search documentation&lt;/a&gt;. | 

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
| **200** | Returns the kreditor accounts |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetPaymentTerm

> void GetPaymentTerm (long id)

Gets payment term for id.

Returns the Scopevisio payment term with the given id.  Required profile(s): <i>Stammdaten, Zahlungsbedingungen (Anzeigen)</i>

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetPaymentTermExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var id = 789;  // long | id of a Scopevisio payment term

            try
            {
                // Gets payment term for id.
                apiInstance.GetPaymentTerm(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetPaymentTerm: " + e.Message );
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
 **id** | **long**| id of a Scopevisio payment term | 

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
| **200** | Returns the Scopevisio payment term with the given id. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetPaymentTerms

> void GetPaymentTerms (string fields = null)

Get payment terms..

Returns all payment terms for the current Scopeviso organisation.  Required profile(s): <i>Stammdaten, Zahlungsbedingungen (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetPaymentTermsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var fields = fields_example;  // string | comma separated list of fields to show in response  name, netTimeLimit (optional) 

            try
            {
                // Get payment terms..
                apiInstance.GetPaymentTerms(fields);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetPaymentTerms: " + e.Message );
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
 **fields** | **string**| comma separated list of fields to show in response  name, netTimeLimit | [optional] 

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
| **200** | Returns the payment terms for the current Scopevisio organisation. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetPersonalJournalEntries

> void GetPersonalJournalEntries (string body = null)

personal journal entries

Returns the Scopevisio journal entries.  Required profile(s): <i>Journal (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetPersonalJournalEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // personal journal entries
                apiInstance.GetPersonalJournalEntries(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetPersonalJournalEntries: " + e.Message );
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
| **200** | Returns the Scopevisio journal entries. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTextTemplates

> void GetTextTemplates ()

Get text templates (Textbausteine)

Returns the Scopevisio text templates (Textbausteine).  Required profile(s): <i>Angebote, Aufträge, Lieferscheine, Rechnungen, Rechnungseingangsbuch (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetTextTemplatesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);

            try
            {
                // Get text templates (Textbausteine)
                apiInstance.GetTextTemplates();
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetTextTemplates: " + e.Message );
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
| **200** | Returns the Scopevisio text templates. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetVatMatrixEntries

> void GetVatMatrixEntries ()

vat matrix

Returns the vat matrix for the current organisation.  Required profile(s): <i>Stammdaten, Steuermatrix (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetVatMatrixEntriesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);

            try
            {
                // vat matrix
                apiInstance.GetVatMatrixEntries();
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetVatMatrixEntries: " + e.Message );
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
| **200** | Returns the vat matrix for the current organisation. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetZones

> void GetZones (string body = null)

zones

Returns all Scopevisio zones (Rechnungskreise).  Required profile(s): <i>Stammdaten, Rechnungskreis (Anzeigen)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class GetZonesExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = body_example;  // string | search filter in JSON format. See also <a href='../browser/index.html#!/searchscope'>our search documentation</a>. (optional) 

            try
            {
                // zones
                apiInstance.GetZones(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.GetZones: " + e.Message );
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
| **200** | Returns all Scopevisio zones. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ImportDatevData

> void ImportDatevData (DatevPostings body = null)

import postings in DATEV-Format

Required profiles: <i>Datenimport (Bearbeiten)</i>. Import postings in DATEV-Format.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class ImportDatevDataExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var body = new DatevPostings(); // DatevPostings |  (optional) 

            try
            {
                // import postings in DATEV-Format
                apiInstance.ImportDatevData(body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.ImportDatevData: " + e.Message );
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
 **body** | [**DatevPostings**](DatevPostings.md)|  | [optional] 

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
| **200** | Create a single posting. See response for error messages. |  -  |
| **400** | Mandatory parameter not given. |  -  |
| **404** | \&quot;zone\&quot; not found. |  -  |
| **500** | Posting error occured or import data is not valid. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateDebitor

> void UpdateDebitor (string accountNumber, UpdatePersonalAccountForm body)

Update a debitor by accountNumber (Kontonummer).

Updates the debitor with the given accountNumber.  Required profile(s): <i>Stammdaten, Debitorenstamm (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateDebitorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = accountNumber_example;  // string | 
            var body = new UpdatePersonalAccountForm(); // UpdatePersonalAccountForm | content in JSON format

            try
            {
                // Update a debitor by accountNumber (Kontonummer).
                apiInstance.UpdateDebitor(accountNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.UpdateDebitor: " + e.Message );
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
 **accountNumber** | **string**|  | 
 **body** | [**UpdatePersonalAccountForm**](UpdatePersonalAccountForm.md)| content in JSON format | 

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
| **200** | Personal account updated. |  -  |
| **400** | No personal account given. |  -  |
| **404** | The personal account with the given accountNumber was not found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateDimensionEntry

> void UpdateDimensionEntry (string dimensionNameOrNumber, DimensionEntryForm body)

Updates the name of a dimension entry.

Updates the name of the Scopevisio dimension entry with the given number.  Required profile(s): <i>Berichte (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateDimensionEntryExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var dimensionNameOrNumber = Kostenstellen;  // string | the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries
            var body = new DimensionEntryForm(); // DimensionEntryForm | 

            try
            {
                // Updates the name of a dimension entry.
                apiInstance.UpdateDimensionEntry(dimensionNameOrNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.UpdateDimensionEntry: " + e.Message );
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
 **dimensionNameOrNumber** | **string**| the name or the number of of the dimension. Example: /dimensions/Kostenstellen/dimensionentries, /dimesions/1/dimensionentries | 
 **body** | [**DimensionEntryForm**](DimensionEntryForm.md)|  | 

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
| **200** | Dimension entry name updated. |  -  |
| **400** | \&quot;number\&quot; or \&quot;name\&quot; not given. |  -  |
| **404** | Dimension not found or authorization missing, dimensionEntry not found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdateKreditor

> void UpdateKreditor (string accountNumber, UpdatePersonalAccountForm body)

Update a kreditor by accountNumber (Kontonummer).

Updates the kreditor with the given accountNumber.  Required profile(s): <i>Stammdaten, Kreditorenstamm (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdateKreditorExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var accountNumber = accountNumber_example;  // string | 
            var body = new UpdatePersonalAccountForm(); // UpdatePersonalAccountForm | content in JSON format

            try
            {
                // Update a kreditor by accountNumber (Kontonummer).
                apiInstance.UpdateKreditor(accountNumber, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.UpdateKreditor: " + e.Message );
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
 **accountNumber** | **string**|  | 
 **body** | [**UpdatePersonalAccountForm**](UpdatePersonalAccountForm.md)| content in JSON format | 

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
| **200** | Personal account updated. |  -  |
| **400** | No personal account given. |  -  |
| **404** | The personal account with the given accountNumber was not found or authorization missing. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## UpdatePaymentTerm

> void UpdatePaymentTerm (long id, PaymentTermForm body = null)

Update payment term.

Update the payment term with the given id.  Required profile(s): <i>Stammdaten, Zahlungsbedingungen (Bearbeiten)</i>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class UpdatePaymentTermExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AccountingApi(Configuration.Default);
            var id = 789;  // long | 
            var body = new PaymentTermForm(); // PaymentTermForm |  (optional) 

            try
            {
                // Update payment term.
                apiInstance.UpdatePaymentTerm(id, body);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountingApi.UpdatePaymentTerm: " + e.Message );
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
 **body** | [**PaymentTermForm**](PaymentTermForm.md)|  | [optional] 

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
| **200** | Payment term changed. |  -  |
| **400** | Payment term name missing.  A payment term with the given name already exists.   Payment term nettimelimit missing. netTimeLimit is less or equal cashDiscountDays1 and/or cashDiscountDays1. percentage2 is greater than percentage1.   percentage2 or percentage1 are more than 100. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

