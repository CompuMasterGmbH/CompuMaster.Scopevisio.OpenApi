using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
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
    public class DeleteResultTests
    {
        private const string Selector = " 0001 / Ä & user@example.invalid ";
        private const string JsonResult = "{\"future\":true,\"id\":9223372036854775807,\"amount\":1.234567890123456789,\"date\":\"2026-10-13T00:00:00Z\"}";
        private static readonly string[] Names = { "DeleteContactByIdWithResult", "DeleteUserWithResult", "DeleteTeamworkTokenWithResult", "RevokeTokenWithResult" };
        private static readonly string[] Prefixes = { "/rest/contact/", "/rest/users/", "/rest/teamworktoken", "/rest/token" };
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-delete-token", ClientNumber = "isolated-client" };
            if (route == 0) return new ContactApi(config);
            if (route == 1) return new UserApi(config);
            if (route == 2) return new AdditionalApi(config);
            return new AuthorizationApi(config);
        }
        private static Configuration Config(object api) => (Configuration)api.GetType().GetProperty("Configuration").GetValue(api);
        private static async Task<object> Call(object api, int route, int mode, bool info, string selector = Selector, CancellationToken token = default(CancellationToken))
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            Type[] types; object[] args;
            if (route < 2)
            {
                types = mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) };
                args = mode == 2 ? new object[] { selector, token } : new object[] { selector };
            }
            else
            {
                types = mode == 2 ? new[] { typeof(CancellationToken) } : Type.EmptyTypes;
                args = mode == 2 ? new object[] { token } : new object[0];
            }
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The complete deletion/invalidation form " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }

        [Test]
        public async Task BodylessDeletionRetainsAcknowledgementAndState([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(false, true)] bool future)
        {
            var body = future ? JsonResult : ""; var status = future ? 200 : 204;
            var handler = new Handler(token => Task.FromResult(Response(status, body)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var result = await Call(api, route, mode, info);
                Assert.That(handler.Path, Is.EqualTo(route < 2 ? Prefixes[route] + Uri.EscapeDataString(Selector) : Prefixes[route]));
                Assert.That(handler.Query, Is.Empty); Assert.That(handler.Method, Is.EqualTo(HttpMethod.Delete)); Assert.That(handler.HasBody, Is.False);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-delete-token")); Assert.That(handler.Accept, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data;
                    Assert.That(response.StatusCode, Is.EqualTo(status)); Assert.That(response.RawJsonContent, Is.EqualTo(body));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                if (future) { Assert.That(data.Value<bool>("future"), Is.True); Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue)); Assert.That(data["date"].Type, Is.EqualTo(JTokenType.String)); Assert.That(data.Value<decimal>("amount"), Is.EqualTo(1.234567890123456789m)); }
                else Assert.That(data, Is.Null);
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-delete-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client")); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OriginalContactAndUidEmailSelectorsRemainOneSegment([Values(0, 1)] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values("00001", "USER@Example.invalid", " 00/Ä ")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(204, "")));
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, info, selector);
                Assert.That(handler.Path, Is.EqualTo(Prefixes[route] + Uri.EscapeDataString(selector))); Assert.That(handler.Query, Is.Empty); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidSelectorsPreventHttp([Values(0, 1)] int route, [Values(0, 1, 2)] int mode, [Values(null, "", " ", ".", "..")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(204, "")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, selector); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void ServerFailuresAndOriginalFactoriesRemainAuthoritative([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(400, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original deletion diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, route, mode, true); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original deletion diagnostic"));
                var expected = new InvalidOperationException("original hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, route, mode, true); })), Is.SameAs(expected));
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-delete-token")); Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void MalformedSuccessfulJsonRemainsVisible([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "{invalid")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true); }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void PreCancellationPreventsDeletionAndTokenChanges([Values(0, 1, 2, 3)] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(204, "")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var api = Api(http, route); cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(api, route, 2, info, token: cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero); Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-delete-token"));
            }
        }

        [Test]
        public async Task ActiveCancellationReachesIoWithoutReplayOrLocalReset([Values(0, 1, 2, 3)] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(204, ""); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var api = Api(http, route); var operation = Call(api, route, 2, info, token: cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1)); Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-delete-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client"));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public void HistoricalVoidResultsAndLocalStateRemainUnchanged([Values(0, 1, 2, 3)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(204, "")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var name = Names[route].Replace("WithResult", "") + "WithHttpInfo";
                var method = api.GetType().GetMethod(name, route < 2 ? new[] { typeof(string) } : Type.EmptyTypes);
                var response = (ApiResponse<object>)method.Invoke(api, route < 2 ? new object[] { Selector } : new object[0]);
                Assert.That(response.StatusCode, Is.EqualTo(204)); Assert.That(response.Data, Is.Null); Assert.That(handler.Calls, Is.EqualTo(1));
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-delete-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client"));
            }
        }

        private static HttpResponseMessage Response(int status, string body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", "first"); response.Content.Headers.TryAddWithoutValidation("x-fixture", "second"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal string Path, Query, Authorization, Accept; internal bool HasBody; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method;
                Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); HasBody = request.Content != null;
                return send(token);
            }
        }
    }
}

