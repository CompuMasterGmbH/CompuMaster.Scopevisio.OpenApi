using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    public class TeamworkBridgeReadTests
    {
        private static readonly byte[] Bytes = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
        private static Type RequestType
        {
            get
            {
                var type = typeof(TeamworkApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.TeamworkBridgeReadRequest");
                Assert.That(type, Is.Not.Null, "The copied bridge read request is absent.");
                return type;
            }
        }
        private static object Request(IEnumerable<string> segments, IEnumerable<KeyValuePair<string, string>> query = null)
        {
            try { return RequestType.GetConstructor(new[] { typeof(IEnumerable<string>), typeof(IEnumerable<KeyValuePair<string, string>>) }).Invoke(new object[] { segments, query }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static async Task<object> Call(TeamworkApi api, object request, int mode, bool info, CancellationToken token = default(CancellationToken))
        {
            var name = "GetTeamworkResource" + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = typeof(TeamworkApi).GetMethod(name, mode == 2 ? new[] { RequestType, typeof(CancellationToken) } : new[] { RequestType });
            Assert.That(method, Is.Not.Null, "The bridge workflow " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, mode == 2 ? new[] { request, (object)token } : new[] { request }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static TeamworkApi Api(HttpClient http)
            => new TeamworkApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-bridge-token" });
        private static HttpResponseMessage Response(int status, byte[] body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new ByteArrayContent(body) };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "one", "two" });
            response.Content.Headers.TryAddWithoutValidation("x-fixture", "three");
            response.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            return response;
        }
        [Test]
        public async Task RoutesQueriesAndOpaqueResponsesArePreserved([Values(1, 2, 3)] int count, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(false, true)] bool json)
        {
            var body = json ? Encoding.UTF8.GetBytes("{\"date\":\"2026-10-13T00:00:00Z\",\"number\":0.123456789012345678901234567890123456789,\"future\":\"Ä\"}") : Bytes;
            var original = new[] { "document / Ä %", "00001", "content+&" }.Take(count).ToArray();
            var segments = original.ToArray();
            var pairs = new List<KeyValuePair<string, string>> { Pair("filter", "x / Ä&"), Pair("filter", "second"), Pair("empty", ""), Pair(" key ", "value") };
            var request = Request(segments, pairs);
            segments[0] = "changed"; pairs.Clear();
            var handler = new Handler(token => Task.FromResult(Response(200, body)));
            using (var http = new HttpClient(handler))
            {
                var result = await Call(Api(http), request, mode, info);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/teamworkbridge/" + string.Join("/", original.Select(Uri.EscapeDataString))));
                Assert.That(handler.Uri.Query, Is.EqualTo("?filter=x%20%2f%20%C3%84%26&filter=second&empty=&%20key%20=value"));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Accept, Is.EqualTo("*/*"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-bridge-token"));
                if (info)
                {
                    var response = (ApiResponse<byte[]>)result;
                    Assert.That(response.Data, Is.EqualTo(body));
                    Assert.That(response.StatusCode, Is.EqualTo(200));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("one,two,three"));
                    Assert.That(response.RawJsonContent, Is.Null);
                }
                else Assert.That((byte[])result, Is.EqualTo(body));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        private static KeyValuePair<string, string> Pair(string key, string value) => new KeyValuePair<string, string>(key, value);
        [Test]
        public void InvalidSegmentsAreRejected([Values(null, "", " ", ".", "..")] string value)
            => Assert.Throws<ArgumentException>((Action)(() => Request(new[] { value })));
        [Test]
        public void InvalidCountsAreRejected([Values(0, 4)] int count)
            => Assert.Throws<ArgumentException>((Action)(() => Request(Enumerable.Repeat("document", count))));
        [Test]
        public void MissingSegmentsAreRejected()
            => Assert.Throws<ArgumentNullException>((Action)(() => Request(null)));
        [Test]
        public void NullQueryKeysOrValuesAreRejected([Values(false, true)] bool key)
            => Assert.Throws<ArgumentException>((Action)(() => Request(new[] { "documents" }, new[] { Pair(key ? null : "key", key ? "value" : null) })));
        [Test]
        public void EmptyQueryKeyIsRejected([Values("", " ")] string key)
            => Assert.Throws<ArgumentException>((Action)(() => Request(new[] { "documents" }, new[] { Pair(key, "value") })));
        [Test]
        public void CopiedCollectionsCannotBeModified()
        {
            var request = Request(new[] { "documents" }, new[] { Pair("key", "value") });
            var paths = (IList<string>)RequestType.GetProperty("PathSegments").GetValue(request);
            var query = (IList<KeyValuePair<string, string>>)RequestType.GetProperty("Query").GetValue(request);
            Assert.Throws<NotSupportedException>((Action)(() => paths[0] = "other"));
            Assert.Throws<NotSupportedException>((Action)(() => query[0] = Pair("other", "other")));
        }
        [Test]
        public async Task NullQueryAndEmptyResponsesAreRetained([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, new byte[0])));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Request(new[] { "folders" }), mode, true);
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(result.Data, Is.Not.Null.And.Empty);
            }
        }
        [Test]
        public void NullRequestFailsBeforeTransport([Values(0, 1, 2)] int mode, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, Bytes)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Call(Api(http), null, mode, info); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public void ErrorsAndOriginalFactoriesAreRetained([Values(0, 1, 2)] int mode, [Values(403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, Encoding.UTF8.GetBytes("original bridge failure"))));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, Request(new[] { "document", "0001" }), mode, true); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original bridge failure"));
                var expected = new InvalidOperationException("original factory failure");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("GetTeamworkResource")); return expected; };
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, Request(new[] { "document" }), mode, true); }));
                Assert.That(actual, Is.SameAs(expected));
                api.ExceptionFactory = null;
                var result = (ApiResponse<byte[]>)Call(api, Request(new[] { "document" }), mode, true).GetAwaiter().GetResult();
                Assert.That(result.StatusCode, Is.EqualTo(status));
                Assert.That(result.Data, Is.EqualTo(Encoding.UTF8.GetBytes("original bridge failure")));
            }
        }
        [Test]
        public void PreCancellationPreventsHttp([Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, Bytes)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http), Request(new[] { "folders" }), 2, info, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesHttp([Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, Bytes); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Call(Api(http), Request(new[] { "folders" }), 2, info, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, task, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(task, Task.Delay(5000)), Is.SameAs(task));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await task; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }
        [Test]
        public void LegacyBridgeDeserializationAndTokenRemainUnchanged()
        {
            var handler = new Handler(token => Task.FromResult(Response(200, Encoding.UTF8.GetBytes("{\"future\":1}"))));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                Assert.That(api.GetHandler1WithHttpInfo("documents").Data, Is.Not.Null);
                Assert.That(api.GetHandler2WithHttpInfo("document", "0001").Data, Is.Not.Null);
                Assert.That(api.GetHandler3WithHttpInfo("document", "0001", "metadata").Data, Is.Not.Null);
                Assert.That(api.Configuration.AccessToken, Is.EqualTo("isolated-bridge-token"));
            }
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Accept, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid"));
                Calls++; Uri = request.RequestUri; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
