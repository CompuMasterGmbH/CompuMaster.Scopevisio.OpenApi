# Org.OpenAPITools.Api.ReportApi

All URIs are relative to *https://appload.scopevisio.com/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetReport**](ReportApi.md#getreport) | **GET** /reports/{type} | Exports accounting reports



## GetReport

> void GetReport (string type, string startDate, string endDate, string reportName = null, string outputFormat = null, bool includeHeader = null, bool includeZeroValued = null)

Exports accounting reports

Required profile(s): <code>Berichte, Debitoren, Kreditoren (Anzeigen)</code>.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetReportExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "https://appload.scopevisio.com/rest";
            // Configure OAuth2 access token for authorization: oauth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ReportApi(Configuration.Default);
            var type = type_example;  // string | The report type to export:<ul><li>susa_account = SuSa Sachkonten</li><li>susa_debitor = SuSa Debitorkonten</li><li>susa_creditor = SuSa Kreditorkonten</li><li>bilanz = Bilanz</li><li>guv = Gewinn- und Verlustrechnung</li><li>bwa = Betriebswirtschaftliche Auswertung</li></ul>
            var startDate = startDate_example;  // string | Start date. Format: <code>dd.MM.yyyy</code>
            var endDate = endDate_example;  // string | End date. Format: <code>dd.MM.yyyy</code>
            var reportName = reportName_example;  // string | Report name, applies only for type bilanz, guv, bwa (optional) 
            var outputFormat = outputFormat_example;  // string | Output format (optional)  (default to csv)
            var includeHeader = true;  // bool | Trigger: Include header in csv export (optional) 
            var includeZeroValued = true;  // bool | Trigger: Include zero values in export (optional) 

            try
            {
                // Exports accounting reports
                apiInstance.GetReport(type, startDate, endDate, reportName, outputFormat, includeHeader, includeZeroValued);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ReportApi.GetReport: " + e.Message );
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
 **type** | **string**| The report type to export:&lt;ul&gt;&lt;li&gt;susa_account &#x3D; SuSa Sachkonten&lt;/li&gt;&lt;li&gt;susa_debitor &#x3D; SuSa Debitorkonten&lt;/li&gt;&lt;li&gt;susa_creditor &#x3D; SuSa Kreditorkonten&lt;/li&gt;&lt;li&gt;bilanz &#x3D; Bilanz&lt;/li&gt;&lt;li&gt;guv &#x3D; Gewinn- und Verlustrechnung&lt;/li&gt;&lt;li&gt;bwa &#x3D; Betriebswirtschaftliche Auswertung&lt;/li&gt;&lt;/ul&gt; | 
 **startDate** | **string**| Start date. Format: &lt;code&gt;dd.MM.yyyy&lt;/code&gt; | 
 **endDate** | **string**| End date. Format: &lt;code&gt;dd.MM.yyyy&lt;/code&gt; | 
 **reportName** | **string**| Report name, applies only for type bilanz, guv, bwa | [optional] 
 **outputFormat** | **string**| Output format | [optional] [default to csv]
 **includeHeader** | **bool**| Trigger: Include header in csv export | [optional] 
 **includeZeroValued** | **bool**| Trigger: Include zero values in export | [optional] 

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
| **200** | Exports accounting reports |  -  |
| **404** | The report with the given reportName was not found. |  -  |
| **500** | Something went wrong |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

