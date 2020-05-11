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
            System.Console.WriteLine("PLEASE NOTE: Following user input will be buffered in directory " + System.IO.Path.GetTempPath());
            string username = InputLine("username");
            string customerno = InputLine("customer no.");
            string password = InputLine("password");

            OpenScopeApiClient OpenScopeClient = new OpenScopeApiClient();
            try
            {
                //Authorize initially with user credentials -> recieved access token will be saved for following requests
                OpenScopeClient.AuthorizeWithUserCredentials(username, customerno, password);
                System.Console.WriteLine("Authorization successful, future requests can be executed with access token:\n" + OpenScopeClient.Token.AccessToken);
                System.Console.WriteLine();

                //A 1st API method call for getting API version info (authorized by access token from initial request)
                CompuMaster.Scopevisio.OpenApi.Model.Version VersionResult = OpenScopeClient.AdditionalApi.GetVersionWithHttpInfo().Data;
                System.Console.WriteLine("Interface version=" + VersionResult.ToString());
                System.Console.WriteLine();

                //A 2nd API method call for getting HelloWorld data (authorized by access token from initial request)
                CompuMaster.Scopevisio.OpenApi.Model.Hello HelloResult = OpenScopeClient.AdditionalApi.HelloJsonWithHttpInfo().Data;
                System.Console.WriteLine("Hello world=" + HelloResult.HelloMessage);
                System.Console.WriteLine();

                //A 3rd API method call for getting HelloWorld data (authorized by access token from initial request)
                //Demonstration of async requests
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

        /// <summary>
        /// Ask user for field data and buffer them in local files
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static string InputLine(string fieldName)
        {
            string BufferFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + fieldName.GetHashCode().ToString() + ".tmp");
            string DefaultValue;
            if (System.IO.File.Exists(BufferFile))
                DefaultValue = System.IO.File.ReadAllText(BufferFile);
            else
                DefaultValue = "";

            Console.WriteLine("Please enter " + fieldName + " [" + DefaultValue + "]: ");
            string UserInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(UserInput))
                return DefaultValue;
            else
            {
                System.IO.File.WriteAllText(BufferFile, UserInput);
                return UserInput;
            }
        }
    }
}
