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
    public class AccountMappingWriteTests
    {
        private static readonly string[] Names = { "CreateAccountMapping", "UpdateAccountMappingEntry", "DeleteAccountMapping" };
        private const string Json = "{\"future\":{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"},\"errors\":[\"inspect original result\"]}";

        [TestCase("CreateAccountMapping")]
        [TestCase("CreateAccountMappingWithHttpInfo")]
        [TestCase("CreateAccountMappingAsyncWithHttpInfo")]
        [TestCase("UpdateAccountMappingEntry")]
        [TestCase("UpdateAccountMappingEntryWithHttpInfo")]
        [TestCase("UpdateAccountMappingEntryAsyncWithHttpInfo")]
        [TestCase("DeleteAccountMapping")]
        [TestCase("DeleteAccountMappingWithHttpInfo")]
        [TestCase("DeleteAccountMappingAsyncWithHttpInfo")]
        public void PublicMappingMutationIsAvailable(string name)
        {
            Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == name), Is.True, "The public account-mapping mutation is absent.");
        }

        [Test]
        public async Task WirePreservesFullMappingChoicesAndResults([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(route == 0 ? HttpStatusCode.Created : HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var request = Request(route);
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(route == 0 ? HttpMethod.Post : route == 1 ? HttpMethod.Put : HttpMethod.Delete));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(route == 0 ? "/rest/accountmapping" : route == 1 ? "/rest/accountmapping/entry/9223372036854775807" : "/rest/accountmapping/9223372036854775807"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-mapping-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request?.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(route == 0 ? 201 : 200));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodiesAreOmittedAndEmptySuccessRemainsNull([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(route == 0 ? HttpStatusCode.Created : HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, null, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.Null);
                Assert.That(info.RawJsonContent, Is.Empty);
                Assert.That(info.Data, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CreationDefaultsAreOmittedAndEmptyRequiredTextIsOriginal([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = new AccountMappingCreationRequest { Description = "" };
                await Invoke(Api(http), 0, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo("{\"description\":\"\"}"));
                request.IncludePersonalAccounts = false;
                request.Entries = new JArray();
                request["accountingArea"] = JValue.CreateNull();
                await Invoke(Api(http), 0, request, mode, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<bool>("includePersonalAccounts"), Is.False);
                Assert.That(body["accountingArea"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That((JArray)body["entries"], Is.Empty);
            }
        }

        [Test]
        public async Task FullInt64SelectionsAreNotAdjusted([Values(long.MinValue, 0L, long.MaxValue)] long id, [Values(0, 1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(route == 0 ? HttpStatusCode.Created : HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = route == 0 ? new object[] { new AccountMappingCreationRequest { Description = "original", AccountingArea = id, ValidFrom = id, ValidTill = id, GlAccountLengthFrom = id, GlAccountLengthTo = id, PersonalAccountLengthFrom = id, PersonalAccountLengthTo = id }, CancellationToken.None } :
                    route == 1 ? new object[] { id, new AccountMappingEntryUpdateRequest { AccountScope = "", AccountExternal = "" }, CancellationToken.None } : new object[] { id, CancellationToken.None };
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, true));
                if (route == 0) { var body = JObject.Parse(handler.Body); foreach (var key in new[] { "accountingArea", "validFrom", "validTill", "glAccountLengthFrom", "glAccountLengthTo", "personalAccountLengthFrom", "personalAccountLengthTo" }) Assert.That(body.Value<long>(key), Is.EqualTo(id)); }
                else Assert.That(handler.Uri.AbsolutePath, Does.EndWith("/" + id.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

        [Test]
        public async Task SnapshotAndModelCopiesRetainLeadingZerosAndUnknownFields([Values(0, 1)] int route)
        {
            var source = route == 0 ? new JObject { ["description"] = "original", ["entries"] = new JArray(new JObject { ["accountScope"] = "0001", ["accountExternal"] = "0090" }), ["future"] = new JObject { ["value"] = "original" } } :
                new JObject { ["accountScope"] = "0001", ["accountExternal"] = "0090", ["future"] = new JObject { ["value"] = "original" }, ["id"] = long.MaxValue, ["modifiedTs"] = long.MinValue };
            var request = route == 0 ? (JObject)new AccountMappingCreationRequest(source) : new AccountMappingEntryUpdateRequest(source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            Assert.That(body["future"].Value<string>("value"), Is.EqualTo("original"));
            var entry = route == 0 ? body["entries"][0] : body;
            Assert.That(entry.Value<string>("accountScope"), Is.EqualTo("0001"));
            Assert.That(entry.Value<string>("accountExternal"), Is.EqualTo("0090"));
        }

        [Test]
        public void EntryArraysAreDefensiveAndTypedNullRemovesFields()
        {
            var entries = new JArray(new JObject { ["accountScope"] = "001", ["accountExternal"] = "002" });
            var request = new AccountMappingCreationRequest { Description = "original", Entries = entries, IncludePersonalAccounts = true };
            entries[0]["accountScope"] = "source changed";
            var result = request.Entries; result[0]["accountScope"] = "copy changed";
            Assert.That(request.Entries[0].Value<string>("accountScope"), Is.EqualTo("001"));
            request.Entries = null; request.IncludePersonalAccounts = null; request.Description = null;
            Assert.That(request.Properties(), Is.Empty);
            var entry = new AccountMappingEntryUpdateRequest { AccountScope = "001", AccountExternal = "002" }; entry.AccountScope = null; entry.AccountExternal = null;
            Assert.That(entry.Properties(), Is.Empty);
        }

        private static readonly string[] InvalidCreation = { "{}", "{\"description\":null}", "{\"description\":1}", "{\"accountingArea\":\"1\"}", "{\"validFrom\":1.0}", "{\"validTill\":9223372036854775808}", "{\"glAccountLengthFrom\":true}", "{\"glAccountLengthTo\":[]}", "{\"personalAccountLengthFrom\":{}}", "{\"personalAccountLengthTo\":\"9\"}", "{\"includePersonalAccounts\":1}", "{\"accountFormat\":false}", "{\"accountType\":[]}", "{\"entries\":{}}", "{\"entries\":[null]}", "{\"entries\":[{}]}", "{\"entries\":[{\"accountScope\":\"1\",\"accountExternal\":2}]}" };
        private static readonly string[] InvalidEntry = { "{}", "{\"accountScope\":null}", "{\"accountExternal\":1}", "{\"id\":1.0}", "{\"modifiedTs\":9223372036854775808}" };

        [Test]
        public void MalformedCreationFailsBeforeTransport([ValueSource(nameof(InvalidCreation))] string json, [Values(0, 2)] int mode)
        {
            var request = new AccountMappingCreationRequest { Description = "original" };
            if (json == "{}") request.Description = null; else foreach (var p in JObject.Parse(json).Properties()) request[p.Name] = p.Value;
            AssertInvalid(0, request, mode);
        }
        [Test]
        public void MalformedEntryFailsBeforeTransport([ValueSource(nameof(InvalidEntry))] string json, [Values(0, 2)] int mode)
        {
            var request = new AccountMappingEntryUpdateRequest { AccountScope = "001", AccountExternal = "002" };
            if (json == "{}") request.AccountScope = null; else foreach (var p in JObject.Parse(json).Properties()) request[p.Name] = p.Value;
            AssertInvalid(1, request, mode);
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
        public async Task ConvenienceFormsExposeCompleteJson([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = Arguments(route, Request(route));
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var data = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(data["errors"][0].Value<string>(), Is.EqualTo("inspect original result"));
            }
        }

        [Test]
        public async Task UndeclaredResultShapesStayExact([Values(0, 1, 2)] int route, [Values("null", "{}", "[]", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), route, null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalConflictAndProfileErrorsPrecedeParsing([Values(0, 1, 2)] int route, [Values(403, 404, 409)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original mapping error")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, null, 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original mapping error"));
                var expected = new InvalidOperationException("custom mapping hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, null, 2, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsMutations([Values(0, 1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesTransportWithoutRetry([Values(0, 1, 2)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, Request(route), 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static JObject Request(int route)
        {
            if (route == 2) return null;
            if (route == 1) return new AccountMappingEntryUpdateRequest { AccountScope = "0001", AccountExternal = "0090" };
            return new AccountMappingCreationRequest { Description = "original", AccountingArea = long.MaxValue, IncludePersonalAccounts = false, ValidFrom = long.MinValue, ValidTill = 0, AccountFormat = "future format", AccountType = "future type", GlAccountLengthFrom = 4, GlAccountLengthTo = 8, PersonalAccountLengthFrom = 5, PersonalAccountLengthTo = 9, Entries = new JArray(new JObject { ["accountScope"] = "0001", ["accountExternal"] = "0090", ["future"] = new JArray() }) };
        }
        private static object[] Arguments(int route, JObject request) => route == 0 ? new object[] { request } : route == 1 ? new object[] { long.MaxValue, request } : new object[] { long.MaxValue };
        private static Type[] Types(int route, bool cancellation)
        {
            var types = route == 0 ? new[] { typeof(AccountMappingCreationRequest) } : route == 1 ? new[] { typeof(long), typeof(AccountMappingEntryUpdateRequest) } : new[] { typeof(long) };
            return cancellation ? types.Concat(new[] { typeof(CancellationToken) }).ToArray() : types;
        }
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JObject request, int mode, CancellationToken token)
        {
            var args = Arguments(route, request);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null);
            object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-mapping-token" });
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
