using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace CompuMaster.Scopevisio.OpenApi.Test
{
    internal class HttpClient
    {
        [Test]
        public void AssignUserAgent()
        {
            var httpClient = new System.Net.Http.HttpClient();

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OpenApiTest/1.0");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OpenApiTest/1.0 (test)");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CompuMaster OpenScope/0.1.0 (.Net)");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (.Net)");
            System.Console.WriteLine("User agent=" + httpClient.DefaultRequestHeaders.UserAgent.ToString());

            ProductHeaderValue header = new ProductHeaderValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            ProductInfoHeaderValue userAgent = new ProductInfoHeaderValue(header);
            System.Console.WriteLine("Dynamic user agent test assembly=" + userAgent.ToString());
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);

            var conf = new CompuMaster.Scopevisio.OpenApi.Client.Configuration();
            System.Console.WriteLine("Dynamic user agent OpenScope assembly=" + conf.UserAgent);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(conf.UserAgent);
        }
    }
}
