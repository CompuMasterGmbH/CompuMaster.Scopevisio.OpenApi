using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class StructuredSearchProtocolTests
    {
        private const string JsonResult = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"epoch\":1600852271000,\"date\":\"2026-10-11T22:00:00Z\",\"unknown\":null}],\"page\":2,\"extra\":{\"hasNext\":true}}";
        private static readonly int[] Routes = { 0, 1, 2, 3, 4, 5 };
        private static readonly int[] Modes = { 0, 1, 2, 3 };
        private static readonly string[] Paths = { "/rest/contacts", "/rest/projects", "/rest/tasks",
            "/rest/contact/legacy%20number/comments", "/rest/project/9223372036854775807/comments", "/rest/task/9223372036854775807/comments" };

        [Test]
        public void OptionalModelFieldsRemainOmittedAndCopiedFiltersPreserveJsonValues()
        {
            var query = new SearchRequest();
            Assert.That(query.ToString(Formatting.None), Is.EqualTo("{}"));
            Assert.That(query.Page, Is.Null);
            Assert.That(query.CountOnly, Is.Null);
            query.Page = -1;
            query.PageSize = 2000;
            query.FormatValues = false;
            query.CountOnly = false;
            query.Fields = new[] { "id", "amount" };
            query.Order = new[] { "id = desc" };
            query.Search = new JArray(new JObject { ["field"] = "amount", ["value"] = 123456789.123456789m, ["operator"] = "greater" });
            var copy = new SearchRequest(query);
            query.Search[0]["value"] = JValue.CreateNull();
            Assert.That(copy.Search[0].Value<decimal>("value"), Is.EqualTo(123456789.123456789m));
            Assert.That(copy.Page, Is.EqualTo(-1));
            Assert.That(copy.PageSize, Is.EqualTo(2000));
            var fields = copy.Fields;
            fields[0] = "changed";
            Assert.That(copy.Fields[0], Is.EqualTo("id"));
            copy.Page = null; copy.PageSize = null; copy.FormatValues = null; copy.CountOnly = null;
            copy.Fields = null; copy.Order = null; copy.Search = null;
            Assert.That(copy.ToString(Formatting.None), Is.EqualTo("{}"));
            Assert.Throws<ArgumentNullException>((Action)(() => { new SearchRequest((JObject)null); }));
        }

        [Test]
        public void CriteriaCopyPreservesExplicitNullOmissionAndEndpointSpecificFilters()
        {
            var criteria = new JArray(new JObject { ["field"] = "name", ["operator"] = "is null" },
                new JObject { ["field"] = "name", ["value"] = JValue.CreateNull(), ["operator"] = "equal" });
            var query = new SearchRequest { Search = criteria };
            query["createdSince"] = 1600852271000L;
            criteria[0]["field"] = "changed";
            Assert.That(query.Search[0].Value<string>("field"), Is.EqualTo("name"));
            Assert.That(((JObject)query.Search[0]).Property("value"), Is.Null);
            Assert.That(query.Search[1]["value"].Type, Is.EqualTo(JTokenType.Null));
            Assert.That(query.Value<long>("createdSince"), Is.EqualTo(1600852271000L));
        }

        [Test]
        public async Task EveryRouteSendsStructuredFiltersAndRetainsFullResults([ValueSource(nameof(Routes))] int route, [ValueSource(nameof(Modes))] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, JsonResult)));
            using (var transport = new HttpClient(handler))
            {
                var query = Query();
                var result = await Invoke(route, mode, transport, query, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Path, Is.EqualTo(Paths[route]));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-fixture-token"));
                Assert.That(handler.Accept, Does.Contain("application/json"));
                Assert.That(handler.ContentType, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None)));
                Assert.That(JToken.Parse(handler.Body).Type, Is.EqualTo(JTokenType.Object));
                Assert.That(result.RawJsonContent, Is.EqualTo(JsonResult));
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Headers["X-Fixture"], Is.EqualTo("response-marker"));
                Assert.That(result.Data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(result.Data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(((JValue)result.Data["records"][0]["amount"]).Value, Is.TypeOf<decimal>());
                Assert.That(result.Data["records"][0].Value<long>("epoch"), Is.EqualTo(1600852271000L));
                Assert.That(result.Data["records"][0]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(result.Data["records"][0]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(result.Data["extra"].Value<bool>("hasNext"), Is.True);
            }
        }

        [Test]
        public async Task MissingFiltersRemainEmptyAndCountResponsesAreNotForcedIntoRecords([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "4294967296")));
            using (var transport = new HttpClient(handler))
            {
                var result = await Invoke(route, 2, transport, null, CancellationToken.None);
                Assert.That(string.IsNullOrEmpty(handler.Body), Is.True);
                Assert.That(result.Data.Value<long>(), Is.EqualTo(4294967296L));
                await Invoke(route, 0, transport, new SearchRequest(), CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo("{}"));
                var query = new SearchRequest { CountOnly = true };
                await Invoke(route, 3, transport, query, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo("{\"count\":true}"));
            }
        }

        [TestCase("", false)]
        [TestCase("null", true)]
        [TestCase("[]", true)]
        public async Task EmptyNullAndArrayResultsRetainTheirJsonShape(string response, bool hasToken)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, response)));
            using (var transport = new HttpClient(handler))
            {
                var result = await Invoke(0, 0, transport, null, CancellationToken.None);
                Assert.That(result.RawJsonContent, Is.EqualTo(response));
                Assert.That(result.Data != null, Is.EqualTo(hasToken));
            }
        }

        [Test]
        public async Task ErrorStatusAndCustomExceptionFactoryPrecedeParsing([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "permission denied")));
            using (var transport = new HttpClient(handler))
            {
                Exception actual = null;
                try { await Invoke(route, asynchronous ? 3 : 0, transport, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ApiException>());
                Assert.That(((ApiException)actual).ErrorCode, Is.EqualTo(403));

                var expected = new InvalidOperationException("custom fixture error");
                var config = ConfigurationFor(transport);
                var api = new ProjectApi(config) { ExceptionFactory = (name, response) => {
                    Assert.That(name, Is.EqualTo("SearchProjects"));
                    return expected;
                }};
                actual = null;
                try
                {
                    if (asynchronous) await api.SearchProjectsAsyncWithHttpInfo(null, CancellationToken.None);
                    else api.SearchProjectsWithHttpInfo();
                }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.SameAs(expected));
            }
        }

        [Test]
        public async Task InvalidSuccessJsonRaisesTheOriginalParserException([Values(0, 3)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "not-json")));
            using (var transport = new HttpClient(handler))
            {
                Exception actual = null;
                try { await Invoke(0, mode, transport, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.InstanceOf<JsonException>());
            }
        }

        [Test]
        public void PreCancellationStopsEverySearchBeforeHttp([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, JsonResult)));
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(route, 3, transport, Query(), cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesEverySearchTransport([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => {
                entered.SetResult(true);
                await Task.Delay(Timeout.Infinite, token);
                return Response(HttpStatusCode.OK, JsonResult);
            });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var pending = Invoke(route, 3, transport, Query(), cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, pending, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(pending, Task.Delay(5000)), Is.SameAs(pending));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await pending; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public void LegacySearchSignaturesAndDefaultsRemainAvailable()
        {
            foreach (var entry in new[] { new { Type = typeof(ProjectApi), Name = "GetProjects" }, new { Type = typeof(TaskApi), Name = "GetTasks" } })
            {
                var sync = entry.Type.GetMethod(entry.Name, new[] { typeof(string) });
                Assert.That(sync.ReturnType, Is.EqualTo(typeof(void)));
                Assert.That(sync.GetParameters()[0].IsOptional, Is.True);
                Assert.That(sync.GetParameters()[0].DefaultValue, Is.Null);
                Assert.That(entry.Type.GetMethod(entry.Name + "Async", new[] { typeof(string) }).ReturnType, Is.EqualTo(typeof(Task)));
                Assert.That(entry.Type.GetMethod(entry.Name + "WithHttpInfo", new[] { typeof(string) }).ReturnType, Is.EqualTo(typeof(ApiResponse<object>)));
            }
        }

        [Test]
        public void MissingContactIdentifierFailsBeforeHttp()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, JsonResult)));
            using (var transport = new HttpClient(handler))
            {
                var api = new ContactApi(ConfigurationFor(transport));
                Assert.Throws<ArgumentException>((Action)(() => { api.SearchContactComments(null); }));
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await api.SearchContactCommentsAsync("", null, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [TestCase("0.123456789012345678901234567891", "0.1234567890123456789012345679")]
        [TestCase("1e-40", "0")]
        public async Task DecimalPrecisionIsBoundedButRawJsonRetainsTheOriginalNumber(string json, string rounded)
        {
            var handler = new FakePrecisionHandler(json);
            using (var transport = new HttpClient(handler))
            {
                var api = new ContactApi(ConfigurationFor(transport));
                var result = await api.SearchContactsAsyncWithHttpInfo();
                Assert.That(result.RawJsonContent, Is.EqualTo(json));
                Assert.That(result.Data.Value<decimal>(), Is.EqualTo(decimal.Parse(rounded, System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

        private sealed class FakePrecisionHandler : HttpMessageHandler
        {
            private readonly string body;
            internal FakePrecisionHandler(string body) { this.body = body; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                return Task.FromResult(Response(HttpStatusCode.OK, body));
            }
        }

        private static SearchRequest Query()
        {
            return new SearchRequest {
                Page = 2, PageSize = 250, Fields = new[] { "id", "amount" }, FormatValues = false,
                Order = new[] { "id = desc" },
                Search = new JArray(new JObject { ["field"] = "amount", ["value"] = 123456789.123456789m, ["operator"] = "greater" },
                    new JObject { ["field"] = "name", ["value"] = JValue.CreateNull(), ["operator"] = "is null" })
            };
        }

        private static Configuration ConfigurationFor(HttpClient transport)
        {
            return new Configuration { HttpClient = transport, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-fixture-token" };
        }

        private static async Task<ApiResponse<JToken>> Invoke(int route, int mode, HttpClient transport, SearchRequest query, CancellationToken token)
        {
            var config = ConfigurationFor(transport);
            if (route == 0)
            {
                var api = new ContactApi(config);
                if (mode == 0) return api.SearchContactsWithHttpInfo(query);
                if (mode == 1) { await api.SearchContactsAsync(query, token); return await api.SearchContactsAsyncWithHttpInfo(query, token); }
                if (mode == 2) return await api.SearchContactsAsyncWithHttpInfo(query);
                return await api.SearchContactsAsyncWithHttpInfo(query, token);
            }
            if (route == 1)
            {
                var api = new ProjectApi(config);
                if (mode == 0) return api.SearchProjectsWithHttpInfo(query);
                if (mode == 1) { Assert.That(api.SearchProjects(query), Is.Not.Null); return api.SearchProjectsWithHttpInfo(query); }
                if (mode == 2) return await api.SearchProjectsAsyncWithHttpInfo(query);
                return await api.SearchProjectsAsyncWithHttpInfo(query, token);
            }
            if (route == 2)
            {
                var api = new TaskApi(config);
                if (mode == 0) return api.SearchTasksWithHttpInfo(query);
                if (mode == 1) { await api.SearchTasksAsync(query); return await api.SearchTasksAsyncWithHttpInfo(query); }
                if (mode == 2) return await api.SearchTasksAsyncWithHttpInfo(query);
                return await api.SearchTasksAsyncWithHttpInfo(query, token);
            }
            if (route == 3)
            {
                var api = new ContactApi(config);
                if (mode == 0) return api.SearchContactCommentsWithHttpInfo("legacy number", query);
                if (mode == 1) { await api.SearchContactCommentsAsync("legacy number", query); return await api.SearchContactCommentsAsyncWithHttpInfo("legacy number", query); }
                if (mode == 2) return await api.SearchContactCommentsAsyncWithHttpInfo("legacy number", query);
                return await api.SearchContactCommentsAsyncWithHttpInfo("legacy number", query, token);
            }
            if (route == 4)
            {
                var api = new ProjectApi(config);
                if (mode == 0) return api.SearchProjectCommentsWithHttpInfo(long.MaxValue, query);
                if (mode == 1) { await api.SearchProjectCommentsAsync(long.MaxValue, query, token); return await api.SearchProjectCommentsAsyncWithHttpInfo(long.MaxValue, query, token); }
                if (mode == 2) return await api.SearchProjectCommentsAsyncWithHttpInfo(long.MaxValue, query);
                return await api.SearchProjectCommentsAsyncWithHttpInfo(long.MaxValue, query, token);
            }
            var task = new TaskApi(config);
            if (mode == 0) return task.SearchTaskCommentsWithHttpInfo(long.MaxValue, query);
            if (mode == 1) { Assert.That(task.SearchTaskComments(long.MaxValue, query), Is.Not.Null); return task.SearchTaskCommentsWithHttpInfo(long.MaxValue, query); }
            if (mode == 2) return await task.SearchTaskCommentsAsyncWithHttpInfo(long.MaxValue, query);
            return await task.SearchTaskCommentsAsyncWithHttpInfo(long.MaxValue, query, token);
        }

        private static HttpResponseMessage Response(HttpStatusCode status, string json)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            response.Headers.Add("X-Fixture", "response-marker");
            return response;
        }

        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal string Body, Path, Authorization, Accept, ContentType;
            internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Path = request.RequestUri.AbsolutePath;
                Method = request.Method;
                Authorization = request.Headers.Authorization?.ToString();
                Accept = request.Headers.Accept.ToString();
                ContentType = request.Content?.Headers.ContentType.MediaType;
                return await send(token);
            }
        }
    }
}
