using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
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
    public class MissingReadOperationProtocolTests
    {
        private static readonly int[] Routes = { 0, 1, 2, 3, 4 };
        private static readonly string[] Names = { "SearchTimeEntryRuns", "SearchTimeEntryRunEntries", "GetChildOrganisationContacts", "GetFilters", "GetStatisticsUnits" };
        private static readonly string[] Paths = { "/rest/timeentryruns", "/rest/timeentryrunentries", "/rest/contact/9223372036854775807/childOrganisations",
            "/rest/filters/invoice%20%2F%20alpha%3F%23", "/rest/catalog/accounting/statisticsunit" };
        private const string Records = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null}],\"more\":true}";

        [Test]
        public async Task DocumentedMethodPathAndOptionalParametersReachTheWire([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            {
                var query = new SearchRequest { Page = 3, PageSize = 200, CountOnly = false };
                var result = await Invoke(route, mode, ConfigurationFor(transport), query, "id,name & custom", "english", CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(route == 2 || route == 4 ? HttpMethod.Get : HttpMethod.Post));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-fixture-token"));
                if (route <= 1)
                    Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None)));
                else Assert.That(string.IsNullOrEmpty(handler.Body), Is.True);
                var decodedQuery = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(decodedQuery, Is.EqualTo(route == 2 ? "?fields=id,name & custom" : route == 4 ? "?language=english" : ""));
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.RawJsonContent, Is.EqualTo(Records));
                Assert.That(result.Data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(result.Data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(result.Data["records"][0]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(result.Data.Value<bool>("more"), Is.True);
            }
        }

        [Test]
        public async Task OmittedOptionalParametersKeepTheServerDefaults([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var transport = new HttpClient(handler))
            {
                await Invoke(route, 1, ConfigurationFor(transport), null, null, null, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(string.IsNullOrEmpty(handler.Body), Is.True);
                if (route == 2 || route == 4)
                {
                    await Invoke(route, 2, ConfigurationFor(transport), null, "", "", CancellationToken.None);
                    Assert.That(handler.Uri.Query, Is.EqualTo(route == 2 ? "?fields=" : "?language="));
                }
            }
        }

        [Test]
        public async Task ServerErrorsAndCustomHooksPreserveTheirExceptions([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.NotFound, "not found")));
            using (var transport = new HttpClient(handler))
            {
                var config = ConfigurationFor(transport);
                Exception actual = null;
                try { await Invoke(route, mode, config, null, null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ApiException>());
                Assert.That(((ApiException)actual).ErrorCode, Is.EqualTo(404));
                var expected = new InvalidOperationException("isolated custom hook");
                actual = null;
                try
                {
                    await Invoke(route, mode, config, null, null, null, CancellationToken.None, (name, response) => {
                        Assert.That(name, Is.EqualTo(Names[route]));
                        return expected;
                    });
                }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsHttpForEveryReadOperation([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => {
                    await Invoke(route, 2, ConfigurationFor(transport), null, null, null, cancellation.Token);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesEveryReadOperation([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => {
                entered.SetResult(true);
                await Task.Delay(Timeout.Infinite, token);
                return Response(HttpStatusCode.OK, Records);
            });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var config = ConfigurationFor(transport);
                var pending = Invoke(route, 2, config, null, null, null, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, pending, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(pending, Task.Delay(5000)), Is.SameAs(pending));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await pending; }));
                    Assert.That(config.AccessToken, Is.EqualTo("isolated-fixture-token"));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public async Task ConvenienceMethodsExposeJsonData([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            {
                var config = ConfigurationFor(transport);
                var api = Api(route, config);
                var types = ParameterTypes(route, false);
                var name = Names[route] + (asynchronous ? "Async" : "");
                var parameters = Parameters(route, null, null, null, false, CancellationToken.None);
                var invoked = InvokeMethod(api, name, types, parameters);
                var data = asynchronous ? await (Task<JToken>)invoked : (JToken)invoked;
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }

        [Test]
        public void FilterTypeIsRequiredAndIsRejectedBeforeHttp()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            {
                var api = new TaskApi(ConfigurationFor(transport));
                Assert.Throws<ArgumentException>((Action)(() => { api.GetFilters(null); }));
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await api.GetFiltersAsyncWithHttpInfo("", CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        private static async Task<ApiResponse<JToken>> Invoke(int route, int mode, Configuration config, SearchRequest query, string fields, string language,
            CancellationToken token, ExceptionFactory factory = null)
        {
            var api = Api(route, config);
            if (factory != null) api.GetType().GetProperty("ExceptionFactory").SetValue(api, factory);
            var name = Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo");
            var invoked = InvokeMethod(api, name, ParameterTypes(route, mode == 2), Parameters(route, query, fields, language, mode == 2, token));
            return mode == 0 ? (ApiResponse<JToken>)invoked : await (Task<ApiResponse<JToken>>)invoked;
        }

        private static object InvokeMethod(object api, string name, Type[] types, object[] parameters)
        {
            try { return api.GetType().GetMethod(name, types).Invoke(api, parameters); }
            catch (TargetInvocationException error)
            {
                ExceptionDispatchInfo.Capture(error.InnerException).Throw();
                throw;
            }
        }

        private static object Api(int route, Configuration config)
        {
            if (route <= 1) return new ProjectApi(config);
            if (route == 2) return new ContactApi(config);
            if (route == 3) return new TaskApi(config);
            return new AdditionalApi(config);
        }

        private static Type[] ParameterTypes(int route, bool cancellation)
        {
            if (route <= 1) return cancellation ? new[] { typeof(SearchRequest), typeof(CancellationToken) } : new[] { typeof(SearchRequest) };
            if (route == 2) return cancellation ? new[] { typeof(long), typeof(string), typeof(CancellationToken) } : new[] { typeof(long), typeof(string) };
            return cancellation ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) };
        }

        private static object[] Parameters(int route, SearchRequest query, string fields, string language, bool cancellation, CancellationToken token)
        {
            if (route <= 1) return cancellation ? new object[] { query, token } : new object[] { query };
            if (route == 2) return cancellation ? new object[] { long.MaxValue, fields, token } : new object[] { long.MaxValue, fields };
            string pathOrLanguage = route == 3 ? "invoice / alpha?#" : language;
            return cancellation ? new object[] { pathOrLanguage, token } : new object[] { pathOrLanguage };
        }

        private static Configuration ConfigurationFor(HttpClient transport)
        {
            return new Configuration { HttpClient = transport, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-fixture-token" };
        }

        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        }

        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++;
                Uri = request.RequestUri;
                Method = request.Method;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Authorization = request.Headers.Authorization?.ToString();
                return await send(token);
            }
        }
    }
}
