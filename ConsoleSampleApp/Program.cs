using CompuMaster.Scopevisio.OpenApi;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using System;
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

            username = "report@bierdeckel-kalkulation.de";
            password = "PWe}VA_6eqX6(W8$~d)PVHe&";
            customerno = "2036092";

            OpenScopeApiClient OpenScopeClient = new OpenScopeApiClient();
            try
            {
                OpenScopeClient.AuthorizeWithUserCredentials(username, customerno, password);
                System.Console.WriteLine("Authorization successful, future requests can be executed with access token:\n" + OpenScopeClient.Token.AccessToken);
                System.Console.WriteLine();

                CompuMaster.Scopevisio.OpenApi.Model.Version VersionResult = OpenScopeClient.AdditionalApi.GetVersionWithHttpInfo().Data;
                System.Console.WriteLine("Interface version=" + VersionResult.ToString());
                System.Console.WriteLine();

                CompuMaster.Scopevisio.OpenApi.Model.Hello HelloResult = OpenScopeClient.AdditionalApi.HelloJsonWithHttpInfo().Data;
                System.Console.WriteLine("Hello world=" + HelloResult.HelloMessage);
                System.Console.WriteLine();

                Task<CompuMaster.Scopevisio.OpenApi.Model.Hello> t = HelloTask(OpenScopeClient);
                t.Wait();
                System.Console.WriteLine("Async hello world=" + t.Result.HelloMessage);
                System.Console.WriteLine();
            }
            catch (ApiException e)
            {
                 System.Console.WriteLine("Exception when calling API: " + e.Message);
                 System.Console.WriteLine("Status Code: " + e.ErrorCode);
                 System.Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception: " + e.ToString());
            }
        }

        /// <summary>
        /// Sample of async call
        /// </summary>
        /// <param name="openScopeClient"></param>
        /// <returns></returns>
        static async Task<CompuMaster.Scopevisio.OpenApi.Model.Hello> HelloTask(OpenScopeApiClient openScopeClient)
        {
            ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.Hello> HelloResponse = await openScopeClient.AdditionalApi.HelloJsonAsyncWithHttpInfo();
            return HelloResponse.Data;
        }
    }
}
