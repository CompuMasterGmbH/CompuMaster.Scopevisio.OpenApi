using CompuMaster.Scopevisio.OpenApi;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter customer no.: ");
            string customerno = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            string password = Console.ReadLine();

            try
            {
                Request1AuthorizeAndGetAccessToken(username, customerno, password);
                Request2GetInterfaceVersion();
                Request3HelloWorld();
            }
            catch (ApiException e)
            {
                System.Diagnostics.Debug.Print("Exception when calling AccountingApi.AddDimensionEntry: " + e.Message);
                System.Diagnostics.Debug.Print("Status Code: " + e.ErrorCode);
                System.Diagnostics.Debug.Print(e.StackTrace);
            }
        }

        static void Request1AuthorizeAndGetAccessToken(string username, string customerNo, string password)
        {
            var AuthApi = new CompuMaster.Scopevisio.OpenApi.Api.AuthorizationApi();
            CompuMaster.Scopevisio.OpenApi.Model.TokenResponse TokenResult = AuthApi.TokenWithHttpInfo("password", customerNo, null, null, username, null, null, password, null, null, null, null).Data;
            string AccessToken = TokenResult.AccessToken;
            System.Console.WriteLine("Authorization successful, future requests can be executed with access token:\n" + AccessToken);
            System.Console.WriteLine();
        }

        static void Request2GetInterfaceVersion()
        {
            var AdditionalApi = new CompuMaster.Scopevisio.OpenApi.Api.AdditionalApi();
            CompuMaster.Scopevisio.OpenApi.Model.Version result = AdditionalApi.GetVersionWithHttpInfo().Data;
            System.Console.WriteLine("Interface version=" + result.ToString());
        }

        static void Request3HelloWorld()
        {
            var AdditionalApi = new CompuMaster.Scopevisio.OpenApi.Api.AdditionalApi();
            CompuMaster.Scopevisio.OpenApi.Model.Hello result = AdditionalApi.HelloJsonWithHttpInfo().Data;
            System.Console.WriteLine("Hello world=" + result.HelloInfo);
        }

    }
}
