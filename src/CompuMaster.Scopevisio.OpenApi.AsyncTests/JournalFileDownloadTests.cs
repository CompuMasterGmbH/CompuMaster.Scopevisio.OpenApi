using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class JournalFileDownloadTests
    {
        private static readonly string[] Names = { "DownloadJournalEntryFile", "DownloadStampedJournalEntryFile" };
        private static readonly byte[] Bytes = Enumerable.Range(0, 256).Select(value => (byte)value).ToArray();

        [Test]
        public async Task BinaryBytesStatusAndHeadersAreRetained([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"),
                    mode == 2 ? new object[] { " 2026 / Ä & custom ", CancellationToken.None } : new object[] { " 2026 / Ä & custom " }, mode == 2);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/journal/%202026%20%2F%20%C3%84%20%26%20custom%20/" + (route == 0 ? "file" : "filewithstamp")));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Accept, Is.EqualTo("*/*"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-download-token"));
                Assert.That(result.Data, Is.EqualTo(Bytes));
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Headers["x-fixture"], Is.EqualTo("first,second,third"));
                Assert.That(result.Headers["content-type"], Is.EqualTo("application/x-fixture"));
                Assert.That(result.Headers["Content-Length"], Is.EqualTo(Bytes.Length.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Assert.That(result.Headers["Content-Disposition"], Is.EqualTo("attachment; filename=\"../unsafe.bin\""));
                Assert.That(result.RawJsonContent, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task EmptyFileIsNotJsonNull([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, new byte[0])));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Names[route] + "AsyncWithHttpInfo", new object[] { "2026-00001" }, false);
                Assert.That(result.Data, Is.Not.Null.And.Empty);
                Assert.That(result.RawJsonContent, Is.Null);
            }
        }

        [Test]
        public async Task AllConvenienceFormsReturnOriginalBytes([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var result = (byte[])await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"),
                    mode == 2 ? new object[] { "2026-00001", CancellationToken.None } : new object[] { "2026-00001" }, mode == 2);
                Assert.That(result, Is.EqualTo(Bytes));
            }
        }

        [Test]
        public async Task MissingPermissionsAndServerErrorsRemainFailures([Values(0, 1)] int route, [Values(403, 404, 500)] int status, [Values(false, true)] bool asynchronous)
        {
            var errorBytes = System.Text.Encoding.UTF8.GetBytes("original file/profile diagnostic");
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, errorBytes)));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { "2026-00001" }, false);
                }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original file/profile diagnostic"));
            }
        }

        [Test]
        public async Task CustomHookRunsBeforeBinaryWrapping([Values(0, 1)] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var expected = new InvalidOperationException("original custom download failure");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Call(api, Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { "2026-00001" }, false);
                }));
                Assert.That(actual, Is.SameAs(expected));
            }
        }


        [Test]
        public async Task DisabledFactoryRetainsOriginalBinaryDiagnostic([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); api.ExceptionFactory = null;
                var response = (ApiResponse<byte[]>)await Call(api, Names[route] + "AsyncWithHttpInfo", new object[] { "2026-00001" }, false);
                Assert.That(response.StatusCode, Is.EqualTo(403));
                Assert.That(response.Data, Is.EqualTo(Bytes));
                Assert.That(response.RawJsonContent, Is.Null);
            }
        }

        [Test]
        public async Task RequiredSelectionFailsBeforeTransport([Values(0, 1)] int route, [Values(null, "", " ", ".", "..")] string number, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { number }, false);
                }));
                Assert.That(error.ParamName, Is.EqualTo("documentNumber"));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void PreCancellationPreventsTransport([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + "AsyncWithHttpInfo", new object[] { "2026-00001", cancellation.Token }, true);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttp([Values(0, 1)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, Bytes); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Call(Api(http), Names[route] + "Async", new object[] { "2026-00001", cancellation.Token }, true);
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

        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-download-token" });
        private static async Task<object> Call(AccountingApi api, string name, object[] args, bool cancellation)
        {
            var method = api.GetType().GetMethod(name, cancellation ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) });
            Assert.That(method, Is.Not.Null, "The binary download workflow " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static HttpResponseMessage Response(HttpStatusCode status, byte[] body)
        {
            var response = new HttpResponseMessage(status) { Content = new ByteArrayContent(body) };
            response.Content.Headers.TryAddWithoutValidation("Content-Type", "application/x-fixture");
            response.Content.Headers.ContentLength = body.Length;
            response.Content.Headers.TryAddWithoutValidation("x-fixture", "third");
            response.Content.Headers.TryAddWithoutValidation("Content-Disposition", "attachment; filename=\"../unsafe.bin\"");
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Accept, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
