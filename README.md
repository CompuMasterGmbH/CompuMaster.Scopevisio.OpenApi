# CompuMaster.Scopevisio.OpenApi - the C# library for the Scopevisio OpenScope REST API

[![NuGet CompuMaster.Scopevisio.OpenApi](https://img.shields.io/nuget/v/CompuMaster.Scopevisio.OpenApi.svg?label=NuGet%20CompuMaster.Scopevisio.OpenApi)](https://www.nuget.org/packages/CompuMaster.Scopevisio.OpenApi/) 
[![Travis](https://img.shields.io/travis/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi.svg?label=Build%20with%20Mono)](https://travis-ci.org/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/)

This C# SDK is automatically generated by the [OpenAPI Generator](https://openapi-generator.tech) project.
For more information, please visit 
* [https://www.scopevisio.com](https://www.scopevisio.com)
* https://www.openscope.de/
* https://appload.scopevisio.com/rest/swagger.json
* [Interactive documenation generated by OpenApi Generator](InteractiveDocumentation%20(OpenApiGenerator).md)

## Frameworks supported

* .NET 4.8 or later

## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;

namespace Example
{
    public class Example
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

## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 105.1.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 7.0.0 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.2.0 or later

## Documentation for Authorization

### oauth

- **Type**: OAuth
- **Flow**: accessCode

## Licensing

This library is dual licensed:
* Community edition is licensed under AGPL 3.0
* For commercial licensing, please contact info@compumaster.de
