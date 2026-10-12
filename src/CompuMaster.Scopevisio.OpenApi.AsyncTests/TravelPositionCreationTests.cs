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
    public class TravelPositionCreationTests
    {
        private static readonly string[] Names = { "CreateTravelExtraPosition", "CreateTravelOvernightPosition", "CreateTravelVehiclePosition" };
        private static readonly string[] Selections = { "extra", "overnight", "vehicle" };
        private const string ResultJson = "{\"position\":{\"id\":9223372036854775807,\"amount\":123456789.123456789},\"errors\":[\"inspect result\"],\"future\":{\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("CreateTravelExtraPosition")]
        [TestCase("CreateTravelExtraPositionWithHttpInfo")]
        [TestCase("CreateTravelExtraPositionAsyncWithHttpInfo")]
        [TestCase("CreateTravelOvernightPosition")]
        [TestCase("CreateTravelOvernightPositionWithHttpInfo")]
        [TestCase("CreateTravelOvernightPositionAsyncWithHttpInfo")]
        [TestCase("CreateTravelVehiclePosition")]
        [TestCase("CreateTravelVehiclePositionWithHttpInfo")]
        [TestCase("CreateTravelVehiclePositionAsyncWithHttpInfo")]
        public void PublicPositionWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == name), Is.True, "The travel-position creation workflow is absent.");
        }

        [Test]
        public async Task OriginalPositionAndBase64AreSentWithoutFileConversion([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, ResultJson)));
            using (var http = new HttpClient(handler))
            {
                var request = Valid(route);
                request.TypeDisplay = "future catalog";
                request.Amount = -123456789.123456789m;
                request.TaxRate = 19.123456789m;
                request.Fileform = new JArray(new TravelReceiptRequest { Filename = "Beleg / München.pdf", Data = "original not re-encoded Base64\r\ntext" }, new TravelReceiptRequest { Data = "" });
                request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
                if (route > 0) ((TravelDistancePositionCreationRequest)request).Distance = -123.456789m;
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/travelentry/position/" + Selections[route] + "/new"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-travel-position-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(201));
                Assert.That(info.RawJsonContent, Is.EqualTo(ResultJson));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Data["position"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["position"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data["errors"][0].Value<string>(), Is.EqualTo("inspect result"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task EmptyReceiptArraysAndOptionalNullAreRetained([Values(0, 1, 2)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid(route);
                request.Fileform = new JArray();
                request["typeDisplay"] = JValue.CreateNull();
                request["amount"] = JValue.CreateNull();
                request["taxRate"] = JValue.CreateNull();
                if (route > 0) request["distance"] = JValue.CreateNull();
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task IdentifiersAndHighNumbersAreLeftForServerValidation([Values(0, 1, 2)] int route, [Values(long.MinValue, 0L, long.MaxValue)] long id)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid(route); request.TravelEntryId = id; request.Amount = decimal.MaxValue; request.TaxRate = decimal.MinValue;
                if (route > 0) ((TravelDistancePositionCreationRequest)request).Distance = decimal.MaxValue;
                await Invoke(Api(http), route, request, 2, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
            }
        }

        private static readonly string[] Invalid = {
            "{}", "{\"travelEntryId\":null}", "{\"travelEntryId\":\"1\"}", "{\"travelEntryId\":1.0}", "{\"travelEntryId\":9223372036854775808}",
            "{\"fileform\":null}", "{\"fileform\":{}}", "{\"fileform\":[null]}", "{\"fileform\":[{}]}", "{\"fileform\":[{\"data\":null}]}", "{\"fileform\":[{\"data\":1}]}",
            "{\"fileform\":[{\"data\":\"original\",\"filename\":1}]}", "{\"typeDisplay\":true}", "{\"amount\":\"1.0\"}", "{\"taxRate\":[]}"
        };

        [Test]
        public void MalformedKnownValuesFailBeforeHttp([Values(0, 1, 2)] int route, [ValueSource(nameof(Invalid))] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid(route);
                if (json == "{}") request.TravelEntryId = null; else foreach (var p in JObject.Parse(json).Properties()) request[p.Name] = p.Value;
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void InvalidDistanceAndNonfiniteValuesPreventHttp([Values(1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid(route);
                request["distance"] = "not numeric";
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, 2, CancellationToken.None); }));
                request["distance"] = double.NaN;
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, 2, CancellationToken.None); }));
                request["distance"] = double.PositiveInfinity;
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, 2, CancellationToken.None); }));
                request.Remove("distance"); request["amount"] = double.NegativeInfinity;
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void NullRequestsPreventHttp([Values(0, 1, 2)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CopiedRequestsReceiptsAndTransportDoNotAliasCallerData([Values(0, 1, 2)] int route)
        {
            var original = new JObject { ["travelEntryId"] = 1L, ["fileform"] = new JArray(new JObject { ["data"] = "original", ["future"] = new JObject { ["value"] = "original" } }) };
            var request = route == 0 ? new TravelPositionCreationRequest(original) : new TravelDistancePositionCreationRequest(original);
            original["fileform"][0]["data"] = "source changed";
            var copy = request.Fileform; copy[0]["data"] = "getter changed";
            Assert.That(request.Fileform[0].Value<string>("data"), Is.EqualTo("original"));
            var handler = new Handler(token => { request["fileform"][0]["data"] = "caller changed"; request["fileform"][0]["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.Created, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            Assert.That(body["fileform"][0].Value<string>("data"), Is.EqualTo("original"));
            Assert.That(body["fileform"][0]["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void ReceiptCopiesAndTypedNullSettersPreserveOmission()
        {
            var source = new JObject { ["data"] = "original", ["future"] = new JObject { ["value"] = "original" } };
            var receipt = new TravelReceiptRequest(source);
            source["future"]["value"] = "source changed";
            Assert.That(receipt["future"].Value<string>("value"), Is.EqualTo("original"));
            receipt.Filename = "original"; receipt.Filename = null; receipt.Data = null;
            Assert.That(receipt["filename"], Is.Null); Assert.That(receipt["data"], Is.Null);
            var request = new TravelDistancePositionCreationRequest { TravelEntryId = 1, TypeDisplay = "original", Amount = 1m, TaxRate = 1m, Distance = 1m, Fileform = new JArray() };
            request.TravelEntryId = null; request.TypeDisplay = null; request.Amount = null; request.TaxRate = null; request.Distance = null; request.Fileform = null;
            Assert.That(request.Properties(), Is.Empty);
            Assert.Throws<ArgumentNullException>((Action)(() => new TravelReceiptRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new TravelPositionCreationRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new TravelDistancePositionCreationRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsRetainCompleteJson([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, ResultJson)))))
            {
                var args = mode == 2 ? new object[] { Valid(route), CancellationToken.None } : new object[] { Valid(route) };
                var data = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(data["position"].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }

        [Test]
        public async Task UnspecifiedResultShapesRemainExact([Values(0, 1, 2)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, json)))))
            {
                var info = await Invoke(Api(http), route, Valid(route), 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void AmbiguousTravelIdentityOrPermissionErrorRemainsOriginal([Values(0, 1, 2)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.NotFound, "travel entry missing or authorization denied")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(404)); Assert.That((object)error.ErrorContent, Is.EqualTo("travel entry missing or authorization denied"));
                var expected = new InvalidOperationException("custom travel hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsPositionCreation([Values(0, 1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesCreationHttpWithoutRetry([Values(0, 1, 2)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.Created, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, Valid(route), 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static TravelPositionCreationRequest Valid(int route)
        {
            var request = route == 0 ? new TravelPositionCreationRequest() : new TravelDistancePositionCreationRequest();
            request.TravelEntryId = long.MaxValue; request.Fileform = new JArray(); return request;
        }
        private static Type[] Types(int route, bool cancellation) => cancellation ? new[] { route == 0 ? typeof(TravelPositionCreationRequest) : typeof(TravelDistancePositionCreationRequest), typeof(CancellationToken) } : new[] { route == 0 ? typeof(TravelPositionCreationRequest) : typeof(TravelDistancePositionCreationRequest) };
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, TravelPositionCreationRequest request, int mode, CancellationToken token)
        {
            var args = mode == 2 ? new object[] { request, token } : new object[] { request };
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null);
            object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-travel-position-token" });
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
