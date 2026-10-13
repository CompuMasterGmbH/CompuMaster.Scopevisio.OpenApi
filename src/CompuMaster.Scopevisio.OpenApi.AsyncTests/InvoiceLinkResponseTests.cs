using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
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
    public class InvoiceLinkResponseTests
    {
        private static readonly string[] Names = { "GetIncomingInvoiceTeamworkFileLinkResponse", "GetOutgoingInvoiceTeamworkFileLinkResponse" };
        private static readonly string[] Selectors = { " IN-0001 / Ä & % ", " RE-0001 / Ä & % " };
        private static readonly string[] Bodies = { "", "https://teamwork.invalid/path?x=1&y=2", "\"https://teamwork.invalid/path\"", "{\"href\":\"future\",\"amount\":1.23456789012345678901234567890123456789e-123}", "<html><a href='future'>Link</a></html>", "  Ä文字\r\noriginal\t ", "not JSON despite declared media type", "null" };

        [Test]
        public async Task AllFormsRetainCompleteDecodedBodyAndOriginalTransportMetadata(
            [Values(0, 1)] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info,
            [Values(0, 1, 2, 3, 4, 5, 6, 7)] int bodyIndex)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bodies[bodyIndex])));
            using (var http = new HttpClient(handler))
            {
                var result = await Invoke(Api(http, route), route, mode, info, Selectors[route], CancellationToken.None);
                if (info)
                {
                    var response = (ApiResponse<string>)result;
                    Assert.That(response.Data, Is.EqualTo(Bodies[bodyIndex]));
                    Assert.That(response.RawJsonContent, Is.Null, "The contract does not establish JSON representation.");
                    Assert.That(response.StatusCode, Is.EqualTo(200));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second,third"));
                    Assert.That(response.Headers["content-type"], Does.StartWith("application/json"));
                }
                else Assert.That((string)result, Is.EqualTo(Bodies[bodyIndex]));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/" + (route == 0 ? "incominginvoice/" : "outgoinginvoice/") + Uri.EscapeDataString(Selectors[route]) + "/teamworkFileLink"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Accept, Is.EqualTo("*/*"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-link-token"));
                Assert.That(handler.Calls, Is.EqualTo(1), "No link navigation or follow-up download is permitted.");
            }
        }

        [Test]
        public void OriginalErrorsRemainAmbiguousAndUnparsed([Values(0, 1)] int route,
            [Values(0, 1, 2)] int mode, [Values(400, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original missing document / permission / file")));
            using (var http = new HttpClient(handler))
            {
                var failure = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, mode, true, Selectors[route], CancellationToken.None); }));
                Assert.That(failure.ErrorCode, Is.EqualTo(status));
                Assert.That((object)failure.ErrorContent, Is.EqualTo("original missing document / permission / file"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void FactorySeesOriginalResponseAndItsExceptionSurvives([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bodies[6])));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var expected = new InvalidOperationException("original hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) =>
                { Assert.That(name, Is.EqualTo(Names[route])); Assert.That(response.Content, Is.EqualTo(Bodies[6])); return expected; }));
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, mode, true, Selectors[route], CancellationToken.None); }));
                Assert.That(actual, Is.SameAs(expected));
            }
        }

        [Test]
        public void MissingOrDotSelectorsFailBeforeHttp([Values(0, 1)] int route,
            [Values(0, 2)] int mode, [Values(null, "", " ", ".", "..")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, mode, true, selector, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void PreCancellationAvoidsHttp([Values(0, 1)] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                cancel.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, 2, info, Selectors[route], cancel.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttp([Values(0, 1)] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, ""); });
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http, route), route, 2, info, Selectors[route], cancel.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancel.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                }
                finally { cancel.Cancel(); }
            }
        }

        [Test]
        public async Task HistoricalMethodsRetainNullDataAndToken()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "https://teamwork.invalid/original")));
            using (var http = new HttpClient(handler))
            {
                var config = Configuration(http);
                var incoming = new IncomingInvoiceApi(config); var outgoing = new OutgoingInvoiceApi(config);
                Assert.That(incoming.GetIncomingInvoiceTeamworkFileLinkWithHttpInfo("0001").Data, Is.Null);
                Assert.That(outgoing.GetOutgoingInvoiceTeamworkFileLinkWithHttpInfo("RE-0001").Data, Is.Null);
                Assert.That((await incoming.GetIncomingInvoiceTeamworkFileLinkAsyncWithHttpInfo("0001")).Data, Is.Null);
                Assert.That((await outgoing.GetOutgoingInvoiceTeamworkFileLinkAsyncWithHttpInfo("RE-0001")).Data, Is.Null);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-link-token"));
                Assert.That(handler.Calls, Is.EqualTo(4));
            }
        }

        private static Configuration Configuration(HttpClient http) => new Configuration { HttpClient = http, BasePath = "https://link-response.invalid/rest", AccessToken = "isolated-link-token" };
        private static object Api(HttpClient http, int route) => route == 0 ? (object)new IncomingInvoiceApi(Configuration(http)) : new OutgoingInvoiceApi(Configuration(http));
        private static async Task<object> Invoke(object api, int route, int mode, bool info, string selector, CancellationToken token)
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var types = mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) };
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The additive link-response form " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, mode == 2 ? new object[] { selector, token } : new object[] { selector }); }
            catch (TargetInvocationException failure) { ExceptionDispatchInfo.Capture(failure.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            response.Content.Headers.TryAddWithoutValidation("x-fixture", "third"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Accept, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                token.ThrowIfCancellationRequested(); Calls++; Uri = request.RequestUri;
                Assert.That(Uri.Host, Is.EqualTo("link-response.invalid"));
                Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
