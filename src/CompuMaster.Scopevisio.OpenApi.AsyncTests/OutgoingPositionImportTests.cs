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
    public class OutgoingPositionImportTests
    {
        private static readonly string[] Names = { "DeleteOutgoingInvoicePositions", "UpdateAllOutgoingInvoicePositions", "UpdateOutgoingInvoicePositions" };
        private static readonly string[] Paths = { "/outgoinginvoices/deletepositions", "/outgoinginvoices/updateallpositions", "/outgoinginvoices/updatepositions" };
        private const string Json = "{\"results\":[{\"documentNumber\":\"0001\",\"error\":\"inspect partial failure\"}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\",\"id\":9223372036854775807}}";

        [Test]
        public void ExplicitWorkflowIsAvailable([Range(0, 2)] int route, [Values("", "WithHttpInfo", "AsyncWithHttpInfo")] string suffix)
        { Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Any(method => method.Name == Names[route] + suffix), Is.True); }



        [Test]
        public async Task ExactChoicesAndCompletePartialResultsReachEveryForm([Range(0, 2)] int route, [Values(0, 1, 2)] int mode)
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
        public async Task ConvenienceFormsPreservePartialFeedback([Range(0, 2)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = mode == 2 ? new object[] { Valid(route), CancellationToken.None } : new object[] { Valid(route) };
                var result = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(result["results"][0].Value<string>("error"), Is.EqualTo("inspect partial failure"));
            }
        }



        [Test]
        public async Task SourceCopiesAndTransportSnapshotsRemainIndependent([Range(0, 2)] int route)
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
        public async Task UnknownResultShapesKeepExactRawJson([Range(0, 2)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            { var info = await Invoke(Api(http), route, Valid(route), 2, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json)); }
        }



        [Test]
        public void OriginalErrorsAndExceptionHookRemainUnchanged([Range(0, 2)] int route, [Values(400, 404, 500)] int status)
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
        public void PreCancellationPreventsMutation([Range(0, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            { cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }



        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Range(0, 2)] int route)
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



        [Test]
        public async Task OptionalBodiesOmitWithoutSynthesizingTargets([Range(0, 2)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            { var info = await Invoke(Api(http), route, null, mode, CancellationToken.None); Assert.That(handler.Body, Is.Null); Assert.That(info.Data, Is.Null); }
        }

        [Test]
        public async Task OriginalInvalidEmptyXmlAndOptionsStayUnchanged([Range(0, 2)] int route, [Values(false, true)] bool empty)
        {
            var request = Valid(route); if (empty) request.Data = "";
            request["template"] = JValue.CreateNull(); request["future"] = new JObject { ["original"] = true };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [Test]
        public void DataMustBePresentAndString([Range(0, 2)] int route, [Values(0, 1, 2)] int kind, [Values(0, 2)] int mode)
        {
            var request = Valid(route); if (kind == 0) request.Remove("data"); else request["data"] = kind == 1 ? (JToken)JValue.CreateNull() : new JValue(1);
            AssertInvalid(route, request, mode);
        }
        [Test]
        public void EveryKnownOptionKindIsValidated([Range(0, 2)] int route, [Values("generateDocumentNumbers", "doPost", "skipDuplicates", "createPdf", "template", "copyProductToPosition", "copyProductToPositionOverwriteMode", "copyImpersonalAccountFieldsToPosition", "copyVatKeyAndTaxRateToPosition")] string field)
        { var request = Valid(route); request[field] = 1; AssertInvalid(route, request, 2); }
        [Test]
        public void TypedModelNullSettersOmitAllTenFieldsAndSourceIsCopied()
        {
            var source = Valid(0); source["future"] = new JObject { ["value"] = "original" };
            var copy = new OutgoingInvoicePositionImportRequest(source); source["future"]["value"] = "source changed";
            Assert.That(copy["future"].Value<string>("value"), Is.EqualTo("original"));
            foreach (var property in typeof(OutgoingInvoicePositionImportRequest).GetProperties().Where(p => p.DeclaringType == typeof(OutgoingInvoicePositionImportRequest)))
            { Assert.That(property.GetValue(copy), Is.Not.Null); property.SetValue(copy, null); }
            Assert.That(copy.Properties().Select(p => p.Name), Is.EqualTo(new[] { "future" }));
            Assert.Throws<ArgumentNullException>((Action)(() => new OutgoingInvoicePositionImportRequest(null)));
        }
        private static void AssertInvalid(int route, JObject request, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        private static JObject Copy(int route, JObject source) => new OutgoingInvoicePositionImportRequest(source);
        private static OutgoingInvoicePositionImportRequest Valid(int route) => new OutgoingInvoicePositionImportRequest
        {
            Data = "<original invalid XML & tail", GenerateDocumentNumbers = false, DoPost = false, SkipDuplicates = false, CreatePdf = false,
            Template = "original template", CopyProductToPosition = false, CopyProductToPositionOverwriteMode = false,
            CopyImpersonalAccountFieldsToPosition = false, CopyVatKeyAndTaxRateToPosition = false
        };
        private static Type[] Types(int route, bool cancellation) => cancellation ? new[] { typeof(OutgoingInvoicePositionImportRequest), typeof(CancellationToken) } : new[] { typeof(OutgoingInvoicePositionImportRequest) };
        private static async Task<ApiResponse<JToken>> Invoke(OutgoingInvoiceApi api, int route, JObject request, int mode, CancellationToken token)
        { var args = mode == 2 ? new object[] { request, token } : new object[] { request }; return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2)); }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null); object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static OutgoingInvoiceApi Api(HttpClient http) => new OutgoingInvoiceApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-open-item-token" });
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
