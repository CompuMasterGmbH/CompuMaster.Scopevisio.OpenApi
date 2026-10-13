using CompuMaster.Scopevisio.OpenApi.Api;
using NUnit.Framework;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class CommentCreationTests
    {
        [TestCase(typeof(ContactApi), "CreateContactCommentWithResult")]
        [TestCase(typeof(ProjectApi), "CreateProjectCommentWithResult")]
        [TestCase(typeof(TaskApi), "CreateTaskCommentWithResult")]
        public void CompleteCommentCreationFormsAreAvailable(Type api, string name)
        {
            Assert.That(api.GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CopiedExtensibleCommentCreationRequestIsAvailable()
        {
            Assert.That(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.CommentCreationRequest"), Is.Not.Null);
        }

        [Test]
        public async Task ExactCommentWireAndCompleteResult([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode,
            [Values("Notiz", "Anruf", "Besprechung", "Vertrag", "E-mail")] string category,
            [Values("", " \r\n", "<p>verbatim & \"text\"</p>")] string text)
        {
            var request = new CommentCreationRequest { Category = category, Body = text };
            request["future"] = new JObject { ["id"] = long.MaxValue, ["nullable"] = JValue.CreateNull() };
            var handler = new Handler(token => Task.FromResult(Response(201, Result)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(route, http), long.MaxValue, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/" + Resource(route) + "/9223372036854775807/comment/new"));
                Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-comment-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(JToken.DeepEquals(JObject.Parse(handler.Body), request), Is.True);
                Assert.That(info.StatusCode, Is.EqualTo(201)); Assert.That(info.RawJsonContent, Is.EqualTo(Result));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json")); Assert.That(info.Headers["X-Comment-Fixture"], Is.EqualTo("preserved"));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"].Value<decimal>("fraction"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OmissionDoesNotInsertDefaults([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            var request = selection == 0 ? null : new CommentCreationRequest();
            if (selection == 2) { request.Category = "Anruf"; request.Body = "remove"; request.Category = null; request.Body = null; }
            var handler = new Handler(token => Task.FromResult(Response(201, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(route, http), 0L, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(selection == 0 ? null : "{}")); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty);
                Assert.That(handler.Uri.AbsolutePath, Does.Contain("/0/comment/new"));
            }
        }

        [Test]
        public async Task SignedMasterIdsUseInvariantText([Values(0, 1, 2)] int route, [Values(0, 2)] int mode, [Values(long.MinValue, -1L, long.MaxValue)] long id)
        {
            var previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
                var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
                using (var http = new HttpClient(handler))
                {
                    await Invoke(Api(route, http), id, null, mode, CancellationToken.None);
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/" + Resource(route) + "/" + id.ToString(CultureInfo.InvariantCulture) + "/comment/new"));
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
        }

        [Test]
        public void DeepCopyAndTypedOmissionPreserveUnknownFields()
        {
            var source = JObject.Parse("{\"category\":\"Anruf\",\"body\":\"original\",\"future\":{\"value\":1}}");
            var request = new CommentCreationRequest(source); source["future"]["value"] = 2;
            Assert.That(request["future"]["value"].Value<int>(), Is.EqualTo(1)); request["future"]["value"] = 3;
            Assert.That(source["future"]["value"].Value<int>(), Is.EqualTo(2)); request.Category = null; request.Body = null;
            Assert.That(request.Properties().Select(property => property.Name), Is.EqualTo(new[] { "future" }));
            Assert.Throws<ArgumentNullException>((TestDelegate)(() => new CommentCreationRequest(null)));
        }

        [Test]
        public void InvalidKnownJsonKindsFailBeforeHttp([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode,
            [Values("category", "body")] string field, [Values("null", "1", "1.5", "true", "[]", "{}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = new CommentCreationRequest { [field] = JToken.Parse(json) };
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(route, http), 1L, request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void CategoryMatchingIsOrdinal([Values("", "notiz", "E-Mail", "E mail", " Notiz", "Notiz\n")] string category)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(0, http), 1L, new CommentCreationRequest { Category = category }, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveTransportUsesThePreIoSnapshot([Values(0, 1, 2)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request = new CommentCreationRequest { Body = "before", Category = "Vertrag" }; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(201, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(route, http), 1L, request, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    request.Body = "after"; request.Category = "Notiz"; request["future"]["value"] = 2;
                    var sent = JObject.Parse(handler.Body); Assert.That(sent.Value<string>("body"), Is.EqualTo("before"));
                    Assert.That(sent.Value<string>("category"), Is.EqualTo("Vertrag")); Assert.That(sent["future"].Value<int>("value"), Is.EqualTo(1));
                }
                finally { release.TrySetResult(true); }
                await operation;
            }
        }

        [Test]
        public async Task DataFormsRetainUnspecifiedResultShapes([Values(0, 1, 2)] int route, [Values(0, 1, 2)] int mode,
            [Values("{}", "[]", "null", "true", "\"opaque\"", "{\"errors\":[\"inspect\"]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, json)));
            using (var http = new HttpClient(handler))
            {
                var data = await InvokeData(Api(route, http), -1L, null, mode);
                Assert.That(data.ToString(Formatting.None), Is.EqualTo(json)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void HttpErrorsAndFactoryRunBeforeParsing([Values(0, 1, 2)] int route, [Values(0, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original comment diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(route, http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, null, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original comment diagnostic"));
                var expected = new InvalidOperationException("custom comment hook");
                SetFactory(api, (name, response) => { Assert.That(name, Is.EqualTo("Create" + Title(route) + "CommentWithResult")); return expected; });
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, 1L, null, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInventedSuccess([Values(0, 1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{invalid")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async () => { await Invoke(Api(route, http), 1L, null, 2, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void PreCancellationPreventsCreation([Values(0, 1, 2)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler)) using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(route, http), 1L, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutReplay([Values(0, 1, 2)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(201, "{}"); });
            using (var http = new HttpClient(handler)) using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(route, http), 1L, null, 2, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public void LegacyDefaultAndSignaturesRemainAvailable()
        {
            Assert.That(new BlogEntryForm().Category, Is.Null);
            foreach (var api in new[] { typeof(ContactApi), typeof(ProjectApi), typeof(TaskApi) })
            {
                var name = "Create" + api.Name.Replace("Api", "") + "Comment";
                Assert.That(api.GetMethod(name, new[] { typeof(long), typeof(BlogEntryForm) }), Is.Not.Null);
                Assert.That(api.GetMethod(name + "AsyncWithHttpInfo", new[] { typeof(long), typeof(BlogEntryForm) }), Is.Not.Null);
            }
        }

        private const string Result = "{\"future\":{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T01:02:03Z\"}}";
        private static string Resource(int route) => route == 0 ? "contact" : route == 1 ? "project" : "task";
        private static string Title(int route) => route == 0 ? "Contact" : route == 1 ? "Project" : "Task";
        private static object Api(int route, HttpClient http)
        {
            var config = new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-comment-token" };
            return route == 0 ? (object)new ContactApi(config) : route == 1 ? (object)new ProjectApi(config) : new TaskApi(config);
        }
        private static Task<ApiResponse<JToken>> Invoke(object api, long id, CommentCreationRequest request, int mode, CancellationToken token)
        {
            if (api is ContactApi contact) return mode == 0 ? Task.FromResult(contact.CreateContactCommentWithResultWithHttpInfo(id, request)) : mode == 1 ? contact.CreateContactCommentWithResultAsyncWithHttpInfo(id, request) : contact.CreateContactCommentWithResultAsyncWithHttpInfo(id, request, token);
            if (api is ProjectApi project) return mode == 0 ? Task.FromResult(project.CreateProjectCommentWithResultWithHttpInfo(id, request)) : mode == 1 ? project.CreateProjectCommentWithResultAsyncWithHttpInfo(id, request) : project.CreateProjectCommentWithResultAsyncWithHttpInfo(id, request, token);
            var task = (TaskApi)api; return mode == 0 ? Task.FromResult(task.CreateTaskCommentWithResultWithHttpInfo(id, request)) : mode == 1 ? task.CreateTaskCommentWithResultAsyncWithHttpInfo(id, request) : task.CreateTaskCommentWithResultAsyncWithHttpInfo(id, request, token);
        }
        private static Task<JToken> InvokeData(object api, long id, CommentCreationRequest request, int mode)
        {
            if (api is ContactApi contact) return mode == 0 ? Task.FromResult(contact.CreateContactCommentWithResult(id, request)) : mode == 1 ? contact.CreateContactCommentWithResultAsync(id, request) : contact.CreateContactCommentWithResultAsync(id, request, CancellationToken.None);
            if (api is ProjectApi project) return mode == 0 ? Task.FromResult(project.CreateProjectCommentWithResult(id, request)) : mode == 1 ? project.CreateProjectCommentWithResultAsync(id, request) : project.CreateProjectCommentWithResultAsync(id, request, CancellationToken.None);
            var task = (TaskApi)api; return mode == 0 ? Task.FromResult(task.CreateTaskCommentWithResult(id, request)) : mode == 1 ? task.CreateTaskCommentWithResultAsync(id, request) : task.CreateTaskCommentWithResultAsync(id, request, CancellationToken.None);
        }
        private static void SetFactory(object api, ExceptionFactory factory)
        {
            if (api is ContactApi contact) contact.ExceptionFactory = factory; else if (api is ProjectApi project) project.ExceptionFactory = factory; else ((TaskApi)api).ExceptionFactory = factory;
        }
        private static HttpResponseMessage Response(int status, string body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Comment-Fixture", "preserved"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Authorization, Accept, ContentType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Uri = request.RequestUri; Method = request.Method;
                Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); ContentType = request.Content?.Headers.ContentType?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
