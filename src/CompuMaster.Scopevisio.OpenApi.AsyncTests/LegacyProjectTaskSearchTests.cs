using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
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
    public class LegacyProjectTaskSearchTests
    {
        private static readonly string[] Bodies = { "{}", " { \"page\": 0, \"pageSize\": 1000 }\r\n",
            "{\"search\":[{\"field\":\"topic\",\"value\":\"Ä & \\\"quoted\\\"\",\"operator\":\"equal\"}],\"amount\":123456789.123456789}" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789}]}";

        [Test]
        public async Task PreSerializedSearchObjectsReachTheWireUnchanged([Values(false, true)] bool task,
            [Values(0, 1, 2)] int mode, [ValueSource(nameof(Bodies))] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Read(Api(task, http), task, mode, body, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(body));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(task ? "/rest/tasks" : "/rest/projects"));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.RequestType, Does.StartWith("application/json"));
                Assert.That(handler.Accept, Is.EqualTo("application/custom+json"));
                Assert.That(handler.Caller, Is.EqualTo("preserved"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-legacy-token"));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Legacy"], Is.EqualTo("server-value"));
                Assert.That(info.Data, Is.Null, "The existing legacy Object/void result contract remains unchanged.");
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task NullBodyOmissionAndExplicitEmptyTextKeepCallerIntent([Values(false, true)] bool task,
            [Values(0, 1, 2)] int mode, [Values(null, "")] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                await Read(Api(task, http), task, mode, body, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(body));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task HttpErrorsAndCallerHooksRetainLegacyNamesAndExceptionIdentity([Values(false, true)] bool task, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "missing profile")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(task, http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Read(api, task, mode, null, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("missing profile"));
                var expected = new InvalidOperationException("legacy hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => {
                    Assert.That(name, Is.EqualTo(task ? "GetTasks" : "GetProjects"));
                    return expected;
                }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Read(api, task, mode, "{}", CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationDoesNotSendOrCallErrorHooks([Values(false, true)] bool task)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                var api = Api(task, http);
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.Fail("Cancellation must not be an API error."); return null; }));
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Read(api, task, 2, "{}", cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesPendingHttpIo([Values(false, true)] bool task)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, Json); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Read(Api(task, http), task, 2, "{}", cancellation.Token);
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
        public async Task ExistingAndCancellableConvenienceMethodsKeepVoidTaskContracts([Values(false, true)] bool task, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(task, http);
                var name = (task ? "GetTasks" : "GetProjects") + (mode == 0 ? "" : "Async");
                var method = api.GetType().GetMethod(name, mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) });
                Assert.That(method, Is.Not.Null);
                Assert.That(method.ReturnType, Is.EqualTo(mode == 0 ? typeof(void) : typeof(Task)));
                object value;
                try { value = method.Invoke(api, mode == 2 ? new object[] { Bodies[2], CancellationToken.None } : new object[] { Bodies[2] }); }
                catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
                if (value is Task operation) await operation;
                Assert.That(handler.Body, Is.EqualTo(Bodies[2]));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private static async Task<ApiResponse<object>> Read(object api, bool task, int mode, string body, CancellationToken token)
        {
            var name = (task ? "GetTasks" : "GetProjects") + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo");
            var method = api.GetType().GetMethod(name, mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) });
            Assert.That(method, Is.Not.Null);
            object value;
            try { value = method.Invoke(api, mode == 2 ? new object[] { body, token } : new object[] { body }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            return mode == 0 ? (ApiResponse<object>)value : await (Task<ApiResponse<object>>)value;
        }
        private static object Api(bool task, HttpClient http)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-legacy-token" };
            config.DefaultHeader["X-Caller"] = "preserved"; config.DefaultHeader["Accept"] = "application/custom+json";
            if (task) return new TaskApi(config);
            return new ProjectApi(config);
        }
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Legacy", "server-value");
            return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization, Accept, Caller, RequestType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Caller = string.Join(",", request.Headers.GetValues("X-Caller")); RequestType = request.Content?.Headers.ContentType?.ToString();
                return await send(token);
            }
        }
    }
}
