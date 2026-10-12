using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class JsonContentHeaderTests
    {
        private const string Json = "{\"id\":1,\"future\":\"Ω\"}";

        [Test]
        public async Task PublicRawAndTypedResultsRetainBothHeaderSets([Values(0, 1, 2)] int workflow, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(Json)))
            {
                var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest" };
                object api = workflow == 2 ? (object)new HumanResourcesApi(config) : new AccountingApi(config);
                var name = workflow == 0 ? "GetFiscalYears" : workflow == 1 ? "SearchCreditorOpenItems" : "GetHumanResourceDetails";
                object[] args = workflow == 0 ? new object[0] : workflow == 1 ? new object[] { null, null, null } : new object[] { 1L, null, null };
                Type[] types = workflow == 0 ? new Type[0] : workflow == 1 ? new[] { typeof(CompuMaster.Scopevisio.OpenApi.Model.SearchRequest), typeof(long?), typeof(string) } : new[] { typeof(long), typeof(string), typeof(bool?) };
                if (mode == 2) { var argumentList = new List<object>(args) { CancellationToken.None }; args = argumentList.ToArray(); var typeList = new List<Type>(types) { typeof(CancellationToken) }; types = typeList.ToArray(); }
                var response = await Invoke(api, name + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, types);
                var responseType = response.GetType();
                var headers = (IDictionary<string, string>)responseType.GetProperty("Headers").GetValue(response);
                Assert.That(headers["Content-Type"], Is.EqualTo("application/json; charset=utf-8"));
                Assert.That(headers["content-length"], Is.EqualTo(Encoding.UTF8.GetByteCount(Json).ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Assert.That(headers["CONTENT-LANGUAGE"], Is.EqualTo("de-DE"));
                Assert.That(headers["x-fixture"], Is.EqualTo("first,second,third"));
                Assert.That(responseType.GetProperty("StatusCode").GetValue(response), Is.EqualTo(200));
                Assert.That(responseType.GetProperty("RawJsonContent").GetValue(response), Is.EqualTo(Json));
                Assert.That(((JToken)responseType.GetProperty("Data").GetValue(response)).Value<string>("future"), Is.EqualTo("Ω"));
            }
        }

        [Test]
        public void FactoryStillRejectsBeforeJsonParsing([Values(false, true)] bool asynchronous)
        {
            using (var http = new HttpClient(new Handler("not JSON")))
            {
                var api = new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest" });
                var expected = new InvalidOperationException("original header fixture hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("GetFiscalYears")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, "GetFiscalYears" + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[0], new Type[0]);
                })), Is.SameAs(expected));
            }
        }

        private static async Task<object> Invoke(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null);
            object response;
            try { response = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(response is Task task)) return response;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly string json;
            internal Handler(string json) { this.json = json; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
                response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
                response.Content.Headers.TryAddWithoutValidation("x-fixture", "third");
                response.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(json);
                response.Content.Headers.ContentLanguage.Add("de-DE");
                return Task.FromResult(response);
            }
        }
    }
}
