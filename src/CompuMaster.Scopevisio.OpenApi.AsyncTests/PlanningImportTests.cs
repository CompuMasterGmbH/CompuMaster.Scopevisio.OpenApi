using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class PlanningImportTests
    {
        private const string ResultJson = "{\"imported\":1,\"errors\":[{\"record\":2,\"message\":\"original row failure\"}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("ImportProjectResources")]
        [TestCase("ImportProjectResourcesWithHttpInfo")]
        [TestCase("ImportProjectResourcesAsyncWithHttpInfo")]
        [TestCase("ImportProjectRevenues")]
        [TestCase("ImportProjectRevenuesWithHttpInfo")]
        [TestCase("ImportProjectRevenuesAsyncWithHttpInfo")]
        public void ExplicitImportWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(HumanResourcesApi).GetMethod(name, new[] { name.Contains("Resources") ? typeof(ProjectResourceImportRequest) : typeof(ProjectRevenueImportRequest) }), Is.Not.Null, "The explicit planning import is absent.");
        }

        [Test]
        public async Task JsonImportPreservesOriginalRowsAndPartialResult([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, ResultJson)));
            using (var http = new HttpClient(handler))
            {
                var request = Request(route);
                var records = new JArray(new JObject { ["projectNumber"] = "001 / project", ["date"] = "15.10.2026", ["comment"] = "original\r\ncomment", ["future"] = new JObject { ["nested"] = JValue.CreateNull() } });
                if (route == 0) {
                    records[0]["humanResourceId"] = long.MaxValue;
                    records[0]["days"] = int.MinValue;
                    records[0]["extraDays"] = int.MaxValue;
                    records[0]["fee"] = -123456789.123456789m;
                }
                else { records[0]["revenueGroup"] = "unknown manual group"; records[0]["amount"] = -123456789.123456789m; }
                SetRecords(request, route, records);
                request["future"] = new JArray();
                var info = await Invoke(Api(http), request, route, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(route == 0 ? "/rest/projectresource/import" : "/rest/projectrevenue/import"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-planning-import-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(ResultJson));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Headers["Content-Length"], Is.EqualTo(Encoding.UTF8.GetByteCount(ResultJson).ToString()));
                Assert.That(info.Data["errors"][0].Value<string>("message"), Is.EqualTo("original row failure"));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CsvAndBothRepresentationsStayExplicit([Values(0, 1)] int route, [Values(0, 2)] int mode, [Values("", "not base64", "cHJvamVjdDthbW91bnQNCjAwMTsxLjIzNDU2Nw==")] string data)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Request(route);
                SetData(request, data);
                SetRecords(request, route, new JArray(new JObject()));
                await Invoke(Api(http), request, route, mode, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<string>("data"), Is.EqualTo(data));
                Assert.That(body[Key(route)], Is.TypeOf<JArray>());
                Assert.That(((JArray)body[Key(route)]).Count, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task EmptyOmittedAndExplicitNullHaveNoInventedMinimum([Values(0, 1)] int route, [Values(0, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Request(route);
                if (selection == 1) SetRecords(request, route, new JArray());
                if (selection == 2) { request["data"] = JValue.CreateNull(); request[Key(route)] = JValue.CreateNull(); }
                await Invoke(Api(http), request, route, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task TypedNullSettersOmitAndArraysAreDefensive([Values(0, 1)] int route)
        {
            var source = new JObject { ["data"] = "original", [Key(route)] = new JArray(new JObject { ["date"] = "original" }) };
            var request = Request(route, source);
            source[Key(route)][0]["date"] = "source changed";
            Assert.That(request[Key(route)][0].Value<string>("date"), Is.EqualTo("original"));
            var records = new JArray(new JObject { ["date"] = "set original" });
            SetRecords(request, route, records);
            records[0]["date"] = "setter source changed";
            var copy = route == 0 ? ((ProjectResourceImportRequest)request).Resources : ((ProjectRevenueImportRequest)request).Revenues;
            copy[0]["date"] = "getter copy changed";
            Assert.That(request[Key(route)][0].Value<string>("date"), Is.EqualTo("set original"));
            SetData(request, null);
            SetRecords(request, route, null);
            Assert.That(request.Properties(), Is.Empty);
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), request, route, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo("{}"));
        }

        private static readonly string[] InvalidCommon = { "{\"data\":1}", "{\"data\":[]}", "{\"rows\":{}}", "{\"rows\":[null]}", "{\"rows\":[1]}", "{\"rows\":[{\"date\":1}]}", "{\"rows\":[{\"projectNumber\":false}]}", "{\"rows\":[{\"comment\":[]}]}", "{\"rows\":[{\"numeric\":\"1.23\"}]}", "{\"rows\":[{\"numeric\":true}]}" };

        [Test]
        public void InvalidKnownKindsPreventHttp([Values(0, 1)] int route, [ValueSource(nameof(InvalidCommon))] string json, [Values(0, 2)] int mode)
        {
            var source = JObject.Parse(json);
            if (source["rows"] != null) { source[Key(route)] = source["rows"]; source.Remove("rows"); }
            if (source[Key(route)] is JArray records && records.Count > 0 && records[0] is JObject item && item["numeric"] != null) {
                item[route == 0 ? "fee" : "amount"] = item["numeric"]; item.Remove("numeric");
            }
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), Request(route, source), route, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [TestCase("humanResourceId", "\"1\"")]
        [TestCase("humanResourceId", "9223372036854775808")]
        [TestCase("days", "2147483648")]
        [TestCase("days", "1.0")]
        [TestCase("extraDays", "-2147483649")]
        [TestCase("extraDays", "false")]
        public void ResourceIntegerWidthsAreCheckedWithoutCoercion(string field, string value)
        {
            var request = new ProjectResourceImportRequest { Resources = new JArray(JObject.Parse("{\"" + field + "\":" + value + "}")) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), request, 0, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void NullRequestAndNonfiniteNumbersPreventHttp([Values(0, 1)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), null, route, mode, CancellationToken.None); }));
                var request = Request(route);
                SetRecords(request, route, new JArray(new JObject { [route == 0 ? "fee" : "amount"] = double.PositiveInfinity }));
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), request, route, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CallerMutationAfterTransportStartsCannotAlterSnapshot([Values(0, 1)] int route)
        {
            var request = Request(route);
            SetRecords(request, route, new JArray(new JObject { ["date"] = "original", ["future"] = new JObject { ["value"] = "original" } }));
            var handler = new Handler(token => { request[Key(route)][0]["date"] = "changed"; request[Key(route)][0]["future"]["value"] = "changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), request, route, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            Assert.That(body[Key(route)][0].Value<string>("date"), Is.EqualTo("original"));
            Assert.That(body[Key(route)][0]["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public async Task ConvenienceFormsRetainCompleteImportJson([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, ResultJson)))))
            {
                var api = Api(http);
                JToken data;
                if (route == 0) { var request = new ProjectResourceImportRequest(); data = mode == 0 ? api.ImportProjectResources(request) : mode == 1 ? await api.ImportProjectResourcesAsync(request) : await api.ImportProjectResourcesAsync(request, CancellationToken.None); }
                else { var request = new ProjectRevenueImportRequest(); data = mode == 0 ? api.ImportProjectRevenues(request) : mode == 1 ? await api.ImportProjectRevenuesAsync(request) : await api.ImportProjectRevenuesAsync(request, CancellationToken.None); }
                Assert.That(data["errors"][0].Value<string>("message"), Is.EqualTo("original row failure"));
            }
        }

        [Test]
        public async Task UnspecifiedResultShapesStayExact([Values(0, 1)] int route, [Values("", "null", "[]", "{}", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), Request(route), route, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void ErrorFactoryRunsBeforeJsonParsingWithoutReplay([Values(0, 1)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.InternalServerError, "original invalid import")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, Request(route), route, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(500));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original invalid import"));
                var expected = new InvalidOperationException("custom import hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(route == 0 ? "ImportProjectResources" : "ImportProjectRevenues")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, Request(route), route, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsImport([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), null, route, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesImportTransportWithoutRetry([Values(0, 1)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), Request(route), route, 2, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        private static string Key(int route) => route == 0 ? "resources" : "revenues";
        private static JObject Request(int route, JObject source = null) => route == 0 ? (JObject)(source == null ? new ProjectResourceImportRequest() : new ProjectResourceImportRequest(source)) : source == null ? new ProjectRevenueImportRequest() : new ProjectRevenueImportRequest(source);
        private static void SetData(JObject request, string value) { if (request is ProjectResourceImportRequest resource) resource.Data = value; else ((ProjectRevenueImportRequest)request).Data = value; }
        private static void SetRecords(JObject request, int route, JArray records) { if (route == 0) ((ProjectResourceImportRequest)request).Resources = records; else ((ProjectRevenueImportRequest)request).Revenues = records; }
        private static Task<ApiResponse<JToken>> Invoke(HumanResourcesApi api, JObject request, int route, int mode, CancellationToken token)
        {
            if (route == 0) return mode == 0 ? Task.FromResult(api.ImportProjectResourcesWithHttpInfo((ProjectResourceImportRequest)request)) : mode == 1 ? api.ImportProjectResourcesAsyncWithHttpInfo((ProjectResourceImportRequest)request) : api.ImportProjectResourcesAsyncWithHttpInfo((ProjectResourceImportRequest)request, token);
            return mode == 0 ? Task.FromResult(api.ImportProjectRevenuesWithHttpInfo((ProjectRevenueImportRequest)request)) : mode == 1 ? api.ImportProjectRevenuesAsyncWithHttpInfo((ProjectRevenueImportRequest)request) : api.ImportProjectRevenuesAsyncWithHttpInfo((ProjectRevenueImportRequest)request, token);
        }
        private static HumanResourcesApi Api(HttpClient http) => new HumanResourcesApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-planning-import-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body) => new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization, Accept, ContentType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); ContentType = request.Content?.Headers.ContentType?.ToString();
                return await send(token);
            }
        }
    }
}
