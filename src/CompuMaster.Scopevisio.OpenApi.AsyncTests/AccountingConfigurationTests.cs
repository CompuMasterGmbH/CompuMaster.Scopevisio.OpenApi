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
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class AccountingConfigurationTests
    {
        private static readonly string[] Names = { "ImportFilters", "EditFiscalYearState" };
        private const string Json = "{\"future\":{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"},\"errors\":[\"inspect original result\"]}";

        [TestCase("ImportFilters")]
        [TestCase("ImportFiltersWithHttpInfo")]
        [TestCase("ImportFiltersAsyncWithHttpInfo")]
        [TestCase("EditFiscalYearState")]
        [TestCase("EditFiscalYearStateWithHttpInfo")]
        [TestCase("EditFiscalYearStateAsyncWithHttpInfo")]
        public void ExplicitConfigurationWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == name), Is.True, "The explicit configuration workflow is absent.");
        }

        [Test]
        public async Task OriginalConfigurationAndCompleteResultUseExactWire([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var request = Request(route);
            request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(route == 0 ? "/rest/filter/import" : "/rest/fiscalyears/edit"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-accounting-configuration-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data["errors"][0].Value<string>(), Is.EqualTo("inspect original result"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodiesAndExplicitEmptyXmlStayOriginal([Values(0, 1)] int route, [Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            JObject request = null;
            if (selection > 0) request = route == 0 ? (JObject)new FilterImportRequest { Data = "" } : new FiscalYearStateRequest();
            if (selection == 2) {
                if (route == 0) request["updateExistingFilters"] = JValue.CreateNull();
                else { request["fiscalYearName"] = JValue.CreateNull(); request["periodId"] = JValue.CreateNull(); request["open"] = JValue.CreateNull(); }
            }
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request?.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task XmlIsNeitherParsedNorReencodedAndUpdateChoiceIsExplicit([Values(false, true)] bool update)
        {
            var original = "<?xml version=\"1.0\"?>\r\n<filters unknown=\"München\">  original &amp; text </filters> malformed tail";
            var request = new FilterImportRequest { Data = original, UpdateExistingFilters = update };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 0, request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body); Assert.That(body.Value<string>("data"), Is.EqualTo(original)); Assert.That(body.Value<bool>("updateExistingFilters"), Is.EqualTo(update));
        }

        [Test]
        public async Task FullInt32PeriodAndOpenCloseChoiceAreNotInferred([Values(int.MinValue, 0, int.MaxValue)] int period, [Values(false, true)] bool open)
        {
            var request = new FiscalYearStateRequest { FiscalYearName = " future year ", PeriodId = period, Open = open };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 1, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        private static readonly string[] InvalidFilter = { "{}", "{\"data\":null}", "{\"data\":1}", "{\"data\":[]}", "{\"updateExistingFilters\":\"true\"}", "{\"updateExistingFilters\":1}" };
        private static readonly string[] InvalidYear = { "{\"fiscalYearName\":1}", "{\"open\":\"true\"}", "{\"periodId\":1.0}", "{\"periodId\":\"1\"}", "{\"periodId\":2147483648}", "{\"periodId\":-2147483649}" };

        [Test]
        public void InvalidFilterFieldsPreventHttp([ValueSource(nameof(InvalidFilter))] string json, [Values(0, 2)] int mode)
        {
            var request = new FilterImportRequest { Data = "original XML" };
            if (json == "{}") request.Data = null; else foreach (var property in JObject.Parse(json).Properties()) request[property.Name] = property.Value;
            AssertInvalid(0, request, mode);
        }

        [Test]
        public void InvalidYearFieldsPreventHttp([ValueSource(nameof(InvalidYear))] string json, [Values(0, 2)] int mode)
        {
            AssertInvalid(1, new FiscalYearStateRequest(JObject.Parse(json)), mode);
        }

        private static void AssertInvalid(int route, JObject request, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CopiedRequestsAndTransportSnapshotPreserveUnknownJson([Values(0, 1)] int route)
        {
            var original = new JObject { ["future"] = new JObject { ["value"] = "original" } };
            if (route == 0) original["data"] = "<original/>";
            var request = route == 0 ? (JObject)new FilterImportRequest(original) : new FiscalYearStateRequest(original);
            original["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(JObject.Parse(handler.Body)["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void TypedNullSettersOmitFieldsAndConstructorSourcesAreRequired()
        {
            var filters = new FilterImportRequest { Data = "original", UpdateExistingFilters = false }; filters.Data = null; filters.UpdateExistingFilters = null;
            var year = new FiscalYearStateRequest { FiscalYearName = "original", PeriodId = 1, Open = false }; year.FiscalYearName = null; year.PeriodId = null; year.Open = null;
            Assert.That(filters.Properties(), Is.Empty); Assert.That(year.Properties(), Is.Empty);
            Assert.Throws<ArgumentNullException>((Action)(() => new FilterImportRequest(null))); Assert.Throws<ArgumentNullException>((Action)(() => new FiscalYearStateRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsRetainCompleteJson([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = mode == 2 ? new object[] { Request(route), CancellationToken.None } : new object[] { Request(route) };
                var data = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(data["errors"][0].Value<string>(), Is.EqualTo("inspect original result"));
            }
        }

        [Test]
        public async Task UnspecifiedResultsRemainExact([Values(0, 1)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), route, null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalImportAndBusinessConstraintsAreNotReplayed([Values(0, 1)] int route, [Values(400, 404, 409, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original import/year diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, null, 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original import/year diagnostic"));
                var expected = new InvalidOperationException("custom configuration hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, null, 2, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsConfigurationChanges([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Values(0, 1)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, null, 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static JObject Request(int route) => route == 0 ? (JObject)new FilterImportRequest { Data = "<original>  unchanged </original>", UpdateExistingFilters = false } : new FiscalYearStateRequest { FiscalYearName = " original year ", PeriodId = int.MaxValue, Open = false };
        private static Type[] Types(int route, bool cancellation) => cancellation ? new[] { route == 0 ? typeof(FilterImportRequest) : typeof(FiscalYearStateRequest), typeof(CancellationToken) } : new[] { route == 0 ? typeof(FilterImportRequest) : typeof(FiscalYearStateRequest) };
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JObject request, int mode, CancellationToken token)
        {
            var args = mode == 2 ? new object[] { request, token } : new object[] { request };
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null);
            object value; try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-accounting-configuration-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body) => new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Authorization, Accept;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            { Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token); }
        }
    }
}
