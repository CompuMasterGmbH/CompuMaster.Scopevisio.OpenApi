using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Globalization;
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
    public class UserListDetailReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 1).ToArray();
        private static readonly string[] Names = { "GetUsersDetails" };
        
        private static readonly string[] Paths = { "/rest/users" };
        private const string Json = "{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-13T01:02:03Z\",\"future\":{\"flag\":false}}";

        [Test]
        public async Task InfoUsesCurrentUserListContract([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, route), route, mode, true, Arguments(route, null, null), CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-user-list-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data.Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data["future"].Value<bool>("flag"), Is.False);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ConvenienceFormsReturnCompleteJson([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = (JToken)await Invoke(Api(http, route), route, mode, false, Arguments(route, null, null), CancellationToken.None);
                Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["future"].Value<bool>("flag"), Is.False);
                Assert.That(handler.Uri.Query, Is.Empty);
            }
        }

        [Test]
        public async Task UnknownSuccessShapesRemainComplete([ValueSource(nameof(Routes))] int route, [Values("{}", "[]", "null", "", "{\"records\":[]}", "\"future\"", "false")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, route), route, 2, true, Arguments(route, null, null), CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(info.Data, JToken.Parse(json)), Is.True);
            }
        }

        [Test]
        public void AmbiguousServerDiagnosticsArePreserved([ValueSource(nameof(Routes))] int route, [Values(400, 403, 404)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original not-found or permission diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, mode, true, Arguments(route, null, null), CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original not-found or permission diagnostic"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void ExceptionFactoryRunsBeforeJsonParsing([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "not JSON")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var expected = new InvalidOperationException("original caller hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                var error = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, mode, true, Arguments(route, null, null), CancellationToken.None); }));
                Assert.That(error, Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsHttp([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                cancel.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, 2, info, Arguments(route, null, null), cancel.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesNativeHttp([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http, route), route, 2, true, Arguments(route, null, null), cancel.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancel.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancel.Cancel(); }
            }
        }

        [Test]
        public async Task HistoricalTypedMethodsRetainIdentityAndDefaults()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[{\"uid\":\"original\",\"future\":true}]")));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { HttpClient = http, BasePath = "https://user-list.invalid/rest", AccessToken = "isolated-user-list-token" };
                var api = new UserApi(config);
                Assert.That(api.GetUsersWithHttpInfo().Data.Single().Uid, Is.EqualTo("original"));
                Assert.That((await api.GetUsersAsyncWithHttpInfo()).Data.Single().Uid, Is.EqualTo("original"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-user-list-token"));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public async Task UserArraysRetainNestedUnknownFieldsAndOriginalDates([Values(0, 1, 2)] int mode)
        {
            const string entries = "[{\"id\":9223372036854775807,\"when\":\"2026-10-13T01:02:03Z\",\"future\":{\"amount\":123456789.123456789,\"unknown\":null,\"done\":false}}]";
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, entries)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, 0), 0, mode, true, Arguments(0, null, null), CancellationToken.None);
                Assert.That(info.Data.Type, Is.EqualTo(JTokenType.Array));
                Assert.That(info.Data[0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data[0]["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data[0]["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data[0]["future"]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data[0]["future"].Value<bool>("done"), Is.False);
                Assert.That(info.RawJsonContent, Is.EqualTo(entries));
            }
        }

        [Test]
        public async Task FilterChoicesRemainIndependentAndInvariant([Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int activeChoice, [Values(0, 1, 2, 3)] int organisationChoice, [Values(false, true)] bool info)
        {
            var active = new bool?[] { null, false, true };
            var organisations = new long?[] { null, long.MinValue, 0, long.MaxValue };
            var previous = CultureInfo.CurrentCulture;
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone(); culture.NumberFormat.NegativeSign = "NEG";
            try
            {
                CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
                using (var http = new HttpClient(handler))
                {
                    var api = Api(http, 0);
                    await Invoke(api, 0, mode, info, new object[] { active[activeChoice], organisations[organisationChoice] }, CancellationToken.None);
                    Assert.That(Query(handler, "onlyActive"), Is.EqualTo(active[activeChoice].HasValue ? active[activeChoice].Value ? "true" : "false" : null));
                    Assert.That(Query(handler, "organisationId"), Is.EqualTo(organisations[organisationChoice]?.ToString(CultureInfo.InvariantCulture)));
                    Assert.That(handler.Uri.Query, Does.Not.Contain("fields").And.Not.Contain("page").And.Not.Contain("formatValues"));
                    Assert.That(handler.Body, Is.Null);
                    Assert.That(handler.Calls, Is.EqualTo(1));
                    Assert.That(((UserApi)api).Configuration.AccessToken, Is.EqualTo("isolated-user-list-token"));
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
        }

        private static string Query(Handler handler, string name)
        {
            foreach (var part in handler.Uri.Query.TrimStart('?').Split('&'))
            {
                var pair = part.Split(new[] { '=' }, 2);
                if (Uri.UnescapeDataString(pair[0]) == name) return pair.Length == 2 ? Uri.UnescapeDataString(pair[1].Replace("+", " ")) : "";
            }
            return null;
        }

        private static object[] Arguments(int route, string fields, bool? formatting) { return new object[] { null, null }; }
        private static Type[] Types(int route) { return new[] { typeof(bool?), typeof(long?) }; }
        private static async Task<object> Invoke(object api, int route, int mode, bool info, object[] args, CancellationToken token)
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var types = Types(route);
            if (mode == 2) { args = args.Concat(new object[] { token }).ToArray(); types = types.Concat(new[] { typeof(CancellationToken) }).ToArray(); }
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The additive user-list workflow " + name + " is absent.");
            object value;
            try { value = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://user-list.invalid/rest", AccessToken = "isolated-user-list-token" };
            return new UserApi(config);
        }
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" }); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization, Accept;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                token.ThrowIfCancellationRequested(); Calls++; Uri = request.RequestUri;
                Assert.That(Uri.Host, Is.EqualTo("user-list.invalid"));
                Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
