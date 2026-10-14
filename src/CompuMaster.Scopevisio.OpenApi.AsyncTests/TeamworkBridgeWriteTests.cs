using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography;
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
    public class TeamworkBridgeWriteTests
    {
        private static readonly byte[] Bytes = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
        private static Type RequestType
        {
            get
            {
                var type = typeof(TeamworkApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.TeamworkBridgeWriteRequest");
                Assert.That(type, Is.Not.Null, "The copied bridge write request is absent.");
                return type;
            }
        }
        private static object Request(IEnumerable<string> segments, IEnumerable<KeyValuePair<string, string>> query = null, byte[] body = null, string contentType = null)
        {
            try { return RequestType.GetConstructor(new[] { typeof(IEnumerable<string>), typeof(IEnumerable<KeyValuePair<string, string>>), typeof(byte[]), typeof(string) }).Invoke(new object[] { segments, query, body, contentType }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static async Task<object> Call(TeamworkApi api, object request, string verb, int mode, bool info, CancellationToken token = default(CancellationToken))
        {
            var name = verb + "TeamworkResource" + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = typeof(TeamworkApi).GetMethod(name, mode == 2 ? new[] { RequestType, typeof(CancellationToken) } : new[] { RequestType });
            Assert.That(method, Is.Not.Null, "The bridge workflow " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, mode == 2 ? new[] { request, (object)token } : new[] { request }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static TeamworkApi Api(HttpClient http)
            => new TeamworkApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-bridge-write-token" });
        private static HttpResponseMessage Response(int status, byte[] body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new ByteArrayContent(body) };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "one", "two" });
            response.Content.Headers.TryAddWithoutValidation("x-fixture", "three");
            response.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            return response;
        }
        [Test]
        public async Task RoutesQueriesAndOpaqueResponsesArePreserved([Values("Remove", "Put", "Post")] string verb, [Values(1, 2, 3, 4)] int count, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(false, true)] bool json, [Values("none", "empty", "binary", "json", "multipart")] string payload)
        {
            var body = json ? Encoding.UTF8.GetBytes("{\"date\":\"2026-10-13T00:00:00Z\",\"number\":0.123456789012345678901234567890123456789,\"future\":\"Ä\"}") : Bytes;
            var original = new[] { "document / Ä %", "00001", "content+&", "a%2Fb ?#" }.Take(count).ToArray();
            var segments = original.ToArray();
            var pairs = new List<KeyValuePair<string, string>> { Pair("filter", "x / Ä&"), Pair("filter", "second"), Pair("empty", ""), Pair(" key ", "value") };
            var sentBytes = payload == "none" ? null : payload == "empty" ? new byte[0] : payload == "binary" ? Bytes.ToArray() : payload == "json" ? Encoding.UTF8.GetBytes("{\"original\":\"Ä\",\"future\":1}") : Encoding.UTF8.GetBytes("--fixture-boundary\r\nContent-Disposition: form-data; name=metadata\r\n\r\n{}\r\n--fixture-boundary--\r\n");
            var expectedBytes = sentBytes?.ToArray();
            var contentType = payload == "none" ? null : payload == "json" ? "application/json; charset=UTF-8" : payload == "multipart" ? "multipart/form-data; boundary=fixture-boundary" : "application/octet-stream";
            var request = Request(segments, pairs, sentBytes, contentType);
            if (sentBytes != null && sentBytes.Length > 0) sentBytes[0] = 255;
            segments[0] = "changed"; pairs.Clear();
            var handler = new Handler(token => Task.FromResult(Response(200, body)));
            using (var http = new HttpClient(handler))
            {
                if (verb == "Post" && count == 4)
                {
                    Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http), request, verb, mode, info); }));
                    Assert.That(handler.Calls, Is.Zero); return;
                }
                var api = Api(http); var originalToken = api.Configuration.AccessToken; var originalOrg = api.Configuration.OrganisationId;
                var result = await Call(api, request, verb, mode, info);
                Assert.That(api.Configuration.AccessToken, Is.EqualTo(originalToken));
                Assert.That(api.Configuration.OrganisationId, Is.EqualTo(originalOrg));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/teamworkbridge/" + string.Join("/", original.Select(Uri.EscapeDataString))));
                Assert.That(handler.Uri.Query, Is.EqualTo("?filter=x%20%2f%20%C3%84%26&filter=second&empty=&%20key%20=value"));
                Assert.That(handler.Body, Is.EqualTo(expectedBytes));
                Assert.That(handler.ContentType, Is.EqualTo(contentType));
                Assert.That(handler.Method, Is.EqualTo(verb == "Remove" ? HttpMethod.Delete : verb == "Put" ? HttpMethod.Put : HttpMethod.Post));
                Assert.That(handler.Accept, Is.EqualTo("*/*"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-bridge-write-token"));
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
        public void InvalidCountsAreRejected([Values(0, 5)] int count)
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
        public async Task NullQueryAndEmptyResponsesAreRetained([Values("Remove", "Put", "Post")] string verb, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, new byte[0])));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Request(new[] { "folders" }), verb, mode, true);
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(result.Data, Is.Not.Null.And.Empty);
            }
        }
        [Test]
        public void NullRequestFailsBeforeTransport([Values("Remove", "Put", "Post")] string verb, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, Bytes)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Call(Api(http), null, verb, mode, info); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public void ErrorsAndOriginalFactoriesAreRetained([Values("Remove", "Put", "Post")] string verb, [Values(0, 1, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, Encoding.UTF8.GetBytes("original bridge failure"))));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, Request(new[] { "document", "0001" }), verb, mode, true); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original bridge failure"));
                var expected = new InvalidOperationException("original factory failure");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(verb + "TeamworkResource")); return expected; };
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, Request(new[] { "document" }), verb, mode, true); }));
                Assert.That(actual, Is.SameAs(expected));
                api.ExceptionFactory = null;
                var result = (ApiResponse<byte[]>)Call(api, Request(new[] { "document" }), verb, mode, true).GetAwaiter().GetResult();
                Assert.That(result.StatusCode, Is.EqualTo(status));
                Assert.That(result.Data, Is.EqualTo(Encoding.UTF8.GetBytes("original bridge failure")));
            }
        }
        [Test]
        public void PreCancellationPreventsHttp([Values("Remove", "Put", "Post")] string verb, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, Bytes)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http), Request(new[] { "folders" }), verb, 2, info, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesHttp([Values("Remove", "Put", "Post")] string verb, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, Bytes); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Call(Api(http), Request(new[] { "folders" }), verb, 2, info, cancellation.Token);
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
        [TestCase("RemoveTeamworkResource")][TestCase("PutTeamworkResource")][TestCase("PostTeamworkResource")]
        public void CompleteMutationFormsAreAvailable(string name) => Assert.That(typeof(TeamworkApi).GetMethods().Count(m => m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [Test] public void CopiedMutationRequestIsAvailable() => Assert.That(RequestType,Is.Not.Null);
        [Test]
        public void MediaTypeAndBodyPresenceAreExplicit([Values(null, "", " ", "invalid", "text/plain\r\nX-Test: one")] string media)
            => Assert.Throws<ArgumentException>((Action)(() => Request(new[]{"documents"},null,Bytes,media)));
        [Test] public void MediaTypeWithoutBodyIsRejected() => Assert.Throws<ArgumentException>((Action)(() => Request(new[]{"documents"},null,null,"application/json")));
        [Test]
        public void BodyGettersReturnSeparateSnapshots()
        {
            var bytes=Bytes.ToArray();var request=(TeamworkBridgeWriteRequest)Request(new[]{"documents"},null,bytes,"application/octet-stream");bytes[0]=255;var first=request.Body;first[1]=255;Assert.That(request.Body,Is.EqualTo(Bytes));Assert.That(request.Body,Is.Not.SameAs(request.Body));
        }
        [Test]
        public void CurrentElevenRoutesAndUnspecifiedBodiesAndResultsRemainExact()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));var c=JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));var total=0;
            foreach(var method in new[]{"delete","put","post"})for(var count=1;count<=(method=="post"?3:4);count++){var path="/teamworkbridge/"+string.Join("/",Enumerable.Range(1,count).Select(i=>"{arg"+i+"}"));var o=c["paths"][path][method];Assert.That(o["requestBody"],Is.Null);Assert.That(o["responses"]["default"]["content"]["*/*"]["schema"].Value<string>("type"),Is.EqualTo("object"));Assert.That(o["security"][0]["oauth"],Is.Not.Null);total++;}Assert.That(total,Is.EqualTo(11));Assert.That(c["paths"]["/teamworkbridge/{arg1}/{arg2}/{arg3}/{arg4}"]["post"],Is.Null);
        }
        [Test]
        public async Task ActivePayloadSnapshotCannotBeChanged([Values("Remove","Put","Post")] string verb)
        {
            var bytes=Bytes.ToArray();var request=Request(new[]{"document","0001"},null,bytes,"application/octet-stream");var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(204,new byte[0]);});using(var http=new HttpClient(handler)){var operation=Call(Api(http),request,verb,2,true);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));bytes[0]=255;((TeamworkBridgeWriteRequest)request).Body[1]=255;Assert.That(handler.Body,Is.EqualTo(Bytes));}finally{release.TrySetResult(true);}var result=(ApiResponse<byte[]>)await operation;Assert.That(result.StatusCode,Is.EqualTo(204));Assert.That(result.Data,Is.Empty);Assert.That(handler.Calls,Is.EqualTo(1));}
        }
        [Test]
        public void LegacyBridgeDeserializationAndTokenRemainUnchanged([Values("Delete","Put","Post")] string verb,[Values(1,2,3,4)] int count)
        {
            if(verb=="Post"&&count==4)return;
            var handler=new Handler(token=>Task.FromResult(Response(200,Encoding.UTF8.GetBytes("{\"future\":1}"))));using(var http=new HttpClient(handler)){var api=Api(http);var method=typeof(TeamworkApi).GetMethod(verb+"Handler"+count+"WithHttpInfo");var result=(ApiResponse<object>)method.Invoke(api,Enumerable.Repeat((object)"original",count).ToArray());Assert.That(result.Data,Is.Not.Null);Assert.That(handler.Body,Is.Null);Assert.That(api.Configuration.AccessToken,Is.EqualTo("isolated-bridge-write-token"));Assert.That(handler.Calls,Is.EqualTo(1));}
        }
        [Test]
        public async Task EncodedTextRemainsAnOriginalSingleSegment([Values("Remove","Put","Post")]string verb,[Values("a%2Fb","%2e%2e","../x/Ä ?#%+")]string segment,[Values(0,1,2)]int mode,[Values(false,true)]bool info)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,Bytes)));using(var http=new HttpClient(handler)){await Call(Api(http),Request(new[]{segment}),verb,mode,info);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/teamworkbridge/"+Uri.EscapeDataString(segment)));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);Assert.That(handler.Calls,Is.EqualTo(1));}
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal byte[] Body; internal string ContentType, Accept, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid"));
                Calls++; Uri = request.RequestUri; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsByteArrayAsync(); ContentType = request.Content?.Headers.ContentType?.ToString();
                return await send(token);
            }
        }
    }
}
