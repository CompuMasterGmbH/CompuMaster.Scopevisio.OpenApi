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
    public class IncomingInvoiceResponsibilityTests
    {
        private const string Json = "{\"future\":{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("SetIncomingInvoiceResponsibleContact")]
        [TestCase("SetIncomingInvoiceResponsibleContactWithHttpInfo")]
        [TestCase("SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo")]
        public void ResponsibleContactWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(IncomingInvoiceApi).GetMethods().Any(method => method.Name == name), Is.True, "The responsible-contact workflow is absent.");
        }

        [Test]
        public async Task FullInt64AssignmentAndExplicitRemovalStayOriginal([Values(long.MinValue, -1L, 0L, 1L, long.MaxValue)] long contact, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var request = new IncomingInvoiceResponsibleContactRequest { ContactId = contact };
                request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
                var info = await Invoke(Api(http), "0000123", request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Put));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/incominginvoice/0000123/responsiblecontact"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-invoice-responsibility-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task InvoiceNumbersAreEscapedWithoutParsingOrTrimming([Values("0001", " ID with spaces ", "invoice/part", "№?#%", "%2F")] string selection, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), selection, null, mode, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/incominginvoice/" + Uri.EscapeDataString(selection) + "/responsiblecontact"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Body, Is.Null);
            }
        }

        [Test]
        public async Task OptionalBodyEmptyObjectAndExplicitNullRemainDistinct([Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var request = selection == 0 ? null : new IncomingInvoiceResponsibleContactRequest();
                if (selection == 2) request["contactId"] = JValue.CreateNull();
                var info = await Invoke(Api(http), "0001", request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request?.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidSelectionsPreventHttp([Values(null, "", " ", ".", "..")] string selection, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), selection, null, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void InvalidRawContactKindsPreventHttp([Values("\"1\"", "1.0", "true", "[]", "{}", "9223372036854775808")] string value, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = new IncomingInvoiceResponsibleContactRequest(JObject.Parse("{\"contactId\":" + value + "}"));
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), "0001", request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CopiesAndTransportSnapshotsDoNotAliasCallerJson()
        {
            var source = new JObject { ["contactId"] = 1L, ["future"] = new JObject { ["value"] = "original" } };
            var request = new IncomingInvoiceResponsibleContactRequest(source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request.ContactId = 0; request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), "0001", request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            Assert.That(body.Value<long>("contactId"), Is.EqualTo(1L));
            Assert.That(body["future"].Value<string>("value"), Is.EqualTo("original"));
            request.ContactId = null; Assert.That(request["contactId"], Is.Null);
            Assert.Throws<ArgumentNullException>((Action)(() => new IncomingInvoiceResponsibleContactRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsExposeCompleteJson([Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var api = Api(http);
                var data = mode == 0 ? api.SetIncomingInvoiceResponsibleContact("0001") : mode == 1 ? await api.SetIncomingInvoiceResponsibleContactAsync("0001") : await api.SetIncomingInvoiceResponsibleContactAsync("0001", null, CancellationToken.None);
                Assert.That(data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }

        [Test]
        public async Task UndeclaredResultShapesRemainExact([Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), "0001", null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalWorkflowAndAssignmentErrorsRunBeforeParser([Values(404, 500)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original invoice/workflow/contact diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, "0001", null, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original invoice/workflow/contact diagnostic"));
                var expected = new InvalidOperationException("custom responsible-contact hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("SetIncomingInvoiceResponsibleContact")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, "0001", null, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsAssignment()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), null, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesAssignmentHttpWithoutRetry()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), "0001", null, 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static Task<ApiResponse<JToken>> Invoke(IncomingInvoiceApi api, string selection, IncomingInvoiceResponsibleContactRequest request, int mode, CancellationToken token)
        {
            return mode == 0 ? Task.FromResult(api.SetIncomingInvoiceResponsibleContactWithHttpInfo(selection, request)) : mode == 1 ? api.SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(selection, request) : api.SetIncomingInvoiceResponsibleContactAsyncWithHttpInfo(selection, request, token);
        }
        private static IncomingInvoiceApi Api(HttpClient http) => new IncomingInvoiceApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-invoice-responsibility-token" });
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
