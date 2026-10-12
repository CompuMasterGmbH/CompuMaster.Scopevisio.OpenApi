using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
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
    public class EurodataInfoReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 1).ToArray();
        private static readonly string[] Names = { "GetContactEurodataInfo" };
        private static readonly string[] Paths = { "/rest/contact/9223372036854775807/eurodataInfo" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-12T01:02:03Z\"}],\"more\":false}";

        [Test]
        public async Task InfoUsesExactInvariantIdentifier([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-eurodata-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.Empty);
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["records"][0]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["records"][0]["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalValuesAreOmittedWithoutInventingPaging([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), route, 1, false, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task UnspecifiedReplyShapesAndNullAreRetained([ValueSource(nameof(Routes))] int route, [Values("{}", "[]", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, 2, false, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(info.Data.Type, Is.EqualTo(json == "{}" ? JTokenType.Object : json == "[]" ? JTokenType.Array : JTokenType.Null));
            }
        }

        [Test]
        public async Task ServerProfileFailuresAndCustomHookExceptionsArePreserved([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "original payroll/contact diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("original payroll/contact diagnostic"));
                var expected = new InvalidOperationException("custom Eurodata hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsOpenItemRequest([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, 2, false, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesOpenItemTransport([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, 2, false, cancellation.Token);
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

        [Test]
        public async Task ConvenienceMethodsExposeCompleteJson([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = (JToken)await Call(Api(http), Names[route] + (asynchronous ? "Async" : ""), Arguments(route, false), Types(route, false));
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }





        [Test]
        public async Task Int64SelectionsUseInvariantText([Values(long.MinValue, 0L, long.MaxValue)] long id, [Values(0, 1, 2)] int mode)
        {
            var previous = System.Globalization.CultureInfo.CurrentCulture;
            var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NegativeSign = "NEG";
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
                using (var http = new HttpClient(handler))
                {
                    var args = mode == 2 ? new object[] { id, CancellationToken.None } : new object[] { id };
                    await Call(Api(http), Names[0] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(0, mode == 2));
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/contact/" + id.ToString(System.Globalization.CultureInfo.InvariantCulture) + "/eurodataInfo"));
                    Assert.That(handler.Body, Is.Null);
                    Assert.That(handler.Uri.Query, Is.Empty);
                }
            }
            finally { System.Globalization.CultureInfo.CurrentCulture = previous; }
        }


        [Test]
        public async Task StandaloneCancellationOverloadReturnsCompleteJson()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = await Api(http).GetContactEurodataInfoAsync(long.MaxValue, CancellationToken.None);
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }

        [Test]
        public void AmbiguousBadRequestRemainsOriginalDiagnostic([Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.BadRequest, "contact unavailable or unreadable")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[0] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { 1L }, Types(0, false));
                }));
                Assert.That(error.ErrorCode, Is.EqualTo(400));
                Assert.That((object)error.ErrorContent, Is.EqualTo("contact unavailable or unreadable"));
            }
        }

        private static async Task<ApiResponse<JToken>> Invoke(ContactApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            return new object[] { long.MaxValue };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var types = new[] { typeof(long) };
            return cancellation ? types.Concat(new[] { typeof(CancellationToken) }).ToArray() : types;
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The public workflow " + name + " is absent.");
            object value;
            try { value = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static ContactApi Api(HttpClient http) => new ContactApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-eurodata-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            return response;
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
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
