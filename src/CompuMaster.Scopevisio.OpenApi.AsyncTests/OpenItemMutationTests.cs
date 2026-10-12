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
    public class OpenItemMutationTests
    {
        private static readonly string[] Names = { "ClearCreditorOpenItems", "ClearDebtorOpenItems", "RebookCreditorOpenItems", "RebookDebtorOpenItems", "SetOpenItemReminderLevels" };
        private static readonly string[] Paths = { "/openitems/creditor/clearing", "/openitems/debitor/clearing", "/openitems/creditor/rebook", "/openitems/debitor/rebook", "/openitems/setReminderLevel" };
        private const string Json = "{\"results\":[{\"documentNumber\":\"0001\",\"error\":\"inspect partial failure\"}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\",\"id\":9223372036854775807}}";

        [Test]
        public void ExplicitWorkflowIsAvailable([Range(0, 4)] int route, [Values("", "WithHttpInfo", "AsyncWithHttpInfo")] string suffix)
        { Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == Names[route] + suffix), Is.True); }

        [Test]
        public async Task ExactChoicesAndCompletePartialResultsReachEveryForm([Range(0, 4)] int route, [Values(0, 1, 2)] int mode)
        {
            var request = Valid(route);
            request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            var response = Response(HttpStatusCode.OK, Json);
            response.Headers.TryAddWithoutValidation("X-Request", "normal");
            response.Content.Headers.TryAddWithoutValidation("X-Content", "content");
            var handler = new Handler(token => Task.FromResult(response));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest" + Paths[route]));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-open-item-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.Headers["X-Request"], Is.EqualTo("normal"));
                Assert.That(info.Headers["X-Content"], Is.EqualTo("content"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["results"][0].Value<string>("error"), Is.EqualTo("inspect partial failure"));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ConvenienceFormsPreservePartialFeedback([Range(0, 4)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = mode == 2 ? new object[] { Valid(route), CancellationToken.None } : new object[] { Valid(route) };
                var result = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(result["results"][0].Value<string>("error"), Is.EqualTo("inspect partial failure"));
            }
        }

        [Test]
        public async Task NoConcreteDefaultsOrMinimumArrayCountsAreInserted([Range(0, 4)] int route, [Values(0, 2)] int mode)
        {
            var request = Empty(route);
            if (route == 4) request["openItems"] = new JArray();
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(route == 4 ? "{\"openItems\":[]}" : "{}"));
                Assert.That(info.Data, Is.Null);
            }
        }

        [Test]
        public async Task OptionalClearingNullAndEmptySelectionsArePreserved([Values(0, 1)] int route, [Values(0, 1, 2, 3)] int selection)
        {
            var request = new OpenItemClearingRequest();
            request["clearings"] = selection == 0 ? (JToken)JValue.CreateNull() : selection == 1 ? new JArray() :
                new JArray(new JObject { ["documentNumber"] = JValue.CreateNull(), ["documents"] = selection == 2 ? (JToken)JValue.CreateNull() : new JArray(new JObject()) });
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        [Test]
        public async Task CallerAmountsAreNotRoundedOrClamped([Values(0, 1, 2)] int choice)
        {
            var amount = choice == 0 ? 0m : choice == 1 ? -1m : 123456789.123456789m;
            var request = new OpenItemClearingRequest { Clearings = new JArray(new OpenItemClearingGroupRequest { Documents = new JArray(new OpenItemClearingDocumentRequest { ClearingAmount = amount }) }) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 0, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        [Test]
        public async Task ReminderInt32BoundsAndEmptyTextRemainServerChoices([Values(int.MinValue, 0, int.MaxValue)] int level)
        {
            var request = new OpenItemReminderLevelsRequest { OpenItems = new JArray(new OpenItemReminderLevelRequest { DocumentNumber = "", ReminderLevel = level }) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 4, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        private static readonly string[] InvalidClearings = { "{\"clearings\":{}}", "{\"clearings\":[null]}", "{\"clearings\":[{\"documentNumber\":1}]}", "{\"clearings\":[{\"documents\":{}}]}", "{\"clearings\":[{\"documents\":[null]}]}", "{\"clearings\":[{\"documents\":[{\"documentNumber\":false}]}]}", "{\"clearings\":[{\"documents\":[{\"clearingAmount\":\"1\"}]}]}" };
        private static readonly string[] InvalidReminders = { "{}", "{\"openItems\":null}", "{\"openItems\":{}}", "{\"openItems\":[null]}", "{\"openItems\":[{}]}", "{\"openItems\":[{\"documentNumber\":null,\"reminderLevel\":0}]}", "{\"openItems\":[{\"documentNumber\":1,\"reminderLevel\":0}]}", "{\"openItems\":[{\"documentNumber\":\"1\"}]}", "{\"openItems\":[{\"documentNumber\":\"1\",\"reminderLevel\":null}]}", "{\"openItems\":[{\"documentNumber\":\"1\",\"reminderLevel\":1.0}]}", "{\"openItems\":[{\"documentNumber\":\"1\",\"reminderLevel\":2147483648}]}", "{\"openItems\":[{\"documentNumber\":\"1\",\"reminderLevel\":-2147483649}]}" };
        [Test]
        public void InvalidClearingKindsPreventHttp([ValueSource(nameof(InvalidClearings))] string json, [Values(0, 1)] int route, [Values(0, 2)] int mode)
        { AssertInvalid(route, new OpenItemClearingRequest(JObject.Parse(json)), mode); }
        [Test]
        public void InvalidReminderRequiredFieldsPreventHttp([ValueSource(nameof(InvalidReminders))] string json, [Values(0, 2)] int mode)
        { AssertInvalid(4, new OpenItemReminderLevelsRequest(JObject.Parse(json)), mode); }
        [Test]
        public void RebookKnownFieldsMustRemainStrings([Values("documentNumber", "postingDate", "postingPeriod", "sourceNumber", "targetNumber")] string field, [Values(2, 3)] int route, [Values(0, 2)] int mode)
        { var request = new OpenItemRebookRequest(); request[field] = 1; AssertInvalid(route, request, mode); }
        [Test]
        public void NonfiniteClearingAmountsPreventHttp([Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double number)
        { AssertInvalid(0, new OpenItemClearingRequest { Clearings = new JArray(new JObject { ["documents"] = new JArray(new JObject { ["clearingAmount"] = number }) }) }, 2); }
        private static void AssertInvalid(int route, JObject request, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public void RequiredBodiesPreventHttp([Range(0, 4)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task SourceCopiesAndTransportSnapshotsRemainIndependent([Range(0, 4)] int route)
        {
            var source = Valid(route);
            source["future"] = new JObject { ["value"] = "original" };
            var request = Copy(route, source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(JObject.Parse(handler.Body)["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void ModelsPreserveCopiesAndTypedNullOmissions()
        {
            var rows = new JArray(new OpenItemClearingGroupRequest { Documents = new JArray(new OpenItemClearingDocumentRequest { DocumentNumber = "0001", ClearingAmount = 1.5m }) });
            var request = new OpenItemClearingRequest { Clearings = rows };
            rows[0]["documents"][0]["documentNumber"] = "source changed";
            var copy = request.Clearings; copy[0]["documents"][0]["documentNumber"] = "getter changed";
            Assert.That(request.Clearings[0]["documents"][0].Value<string>("documentNumber"), Is.EqualTo("0001"));
            request.Clearings = null; Assert.That(request.Properties(), Is.Empty);
            var group = new OpenItemClearingGroupRequest { Documents = rows }; var documents = group.Documents; documents.Clear(); Assert.That(group.Documents.Count, Is.EqualTo(1));
            group.DocumentNumber = null; group.Documents = null; Assert.That(group.Properties(), Is.Empty);
            var reminder = new OpenItemReminderLevelsRequest { OpenItems = new JArray(new OpenItemReminderLevelRequest { DocumentNumber = "1", ReminderLevel = 0 }) };
            var items = reminder.OpenItems; items.Clear(); Assert.That(reminder.OpenItems.Count, Is.EqualTo(1));
            reminder.OpenItems = null; Assert.That(reminder.Properties(), Is.Empty);
            foreach (var type in new[] { typeof(OpenItemClearingRequest), typeof(OpenItemClearingGroupRequest), typeof(OpenItemClearingDocumentRequest), typeof(OpenItemRebookRequest), typeof(OpenItemReminderLevelsRequest), typeof(OpenItemReminderLevelRequest) })
            { var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(type, new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>()); }
        }

        [Test]
        public async Task UnknownResultShapesKeepExactRawJson([Range(0, 4)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            { var info = await Invoke(Api(http), route, Valid(route), 2, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json)); }
        }

        [Test]
        public void OriginalErrorsAndExceptionHookRemainUnchanged([Range(0, 4)] int route, [Values(400, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original accounting error")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original accounting error"));
                var expected = new InvalidOperationException("custom open-item hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsMutation([Range(0, 4)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            { cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Range(0, 4)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, Valid(route), 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static JObject Empty(int route) => route < 2 ? (JObject)new OpenItemClearingRequest() : route < 4 ? new OpenItemRebookRequest() : (JObject)new OpenItemReminderLevelsRequest();
        private static JObject Copy(int route, JObject source) => route < 2 ? (JObject)new OpenItemClearingRequest(source) : route < 4 ? new OpenItemRebookRequest(source) : (JObject)new OpenItemReminderLevelsRequest(source);
        private static JObject Valid(int route)
        {
            if (route < 2) return new OpenItemClearingRequest { Clearings = new JArray(new OpenItemClearingGroupRequest { DocumentNumber = "0001/A", Documents = new JArray(new OpenItemClearingDocumentRequest { DocumentNumber = "0002", ClearingAmount = -123456789.123456789m }) }) };
            if (route < 4) return new OpenItemRebookRequest { DocumentNumber = "0001/A", PostingDate = "31.12.2026", PostingPeriod = "13", SourceNumber = "00002", TargetNumber = "00001A" };
            return new OpenItemReminderLevelsRequest { OpenItems = new JArray(new OpenItemReminderLevelRequest { DocumentNumber = "0001/A", ReminderLevel = int.MaxValue }) };
        }
        private static Type[] Types(int route, bool cancellation)
        { var type = route < 2 ? typeof(OpenItemClearingRequest) : route < 4 ? typeof(OpenItemRebookRequest) : typeof(OpenItemReminderLevelsRequest); return cancellation ? new[] { type, typeof(CancellationToken) } : new[] { type }; }
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JObject request, int mode, CancellationToken token)
        { var args = mode == 2 ? new object[] { request, token } : new object[] { request }; return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2)); }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null); object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-open-item-token" });
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
