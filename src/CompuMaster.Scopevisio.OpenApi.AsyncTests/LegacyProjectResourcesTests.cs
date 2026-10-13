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
    public class LegacyProjectResourcesTests
    {
        private const string JsonResult = "{\"records\":[{\"id\":9223372036854775807,\"days\":1.234567890123456789,\"date\":\"2026-10-13T00:00:00Z\"}],\"future\":true}";
        private static Type RequestType
        {
            get { var type = typeof(ProjectApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ProjectResourcesLegacySearchRequest"); Assert.That(type, Is.Not.Null, "The explicit legacy range request is absent."); return type; }
        }
        private static JObject Request(JObject source)
        {
            try { return (JObject)RequestType.GetConstructor(new[] { typeof(JObject) }).Invoke(new object[] { source }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static JObject Range(long value, int search)
        {
            var valueObject = new JObject { ["projectStartFrom"] = value, ["projectStartTo"] = value };
            if (search == 1) valueObject["searchSpecification"] = JValue.CreateNull();
            if (search == 2) valueObject["searchSpecification"] = new JObject();
            if (search == 3) valueObject["searchSpecification"] = new JObject { ["page"] = -1, ["pageSize"] = 2000, ["fields"] = new JArray("id", "month"), ["search"] = new JArray(new JObject { ["field"] = "monthRevenue", ["operator"] = "greater", ["value"] = 1.234567890123456789m }), ["future"] = true };
            return valueObject;
        }
        private static ProjectApi Api(HttpClient http) => new ProjectApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-legacy-availability-token" });
        private static async Task<object> Call(ProjectApi api, JObject request, int mode, bool info, CancellationToken token = default(CancellationToken))
        {
            var name = "SearchProjectResourcesLegacy" + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = typeof(ProjectApi).GetMethod(name, mode == 2 ? new[] { RequestType, typeof(CancellationToken) } : new[] { RequestType });
            Assert.That(method, Is.Not.Null, "The explicit legacy availability form " + name + " is absent.");
            Assert.That(method.GetCustomAttribute<ObsoleteAttribute>(), Is.Not.Null);
            object result;
            try { result = method.Invoke(api, mode == 2 ? new object[] { request, token } : new object[] { request }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        [Test]
        public async Task OriginalRangeAndSearchChoicesAreCopiedWithoutConversion([Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(long.MinValue, 0L, long.MaxValue)] long value, [Values(0, 1, 2, 3)] int search)
        {
            var original = Range(value, search); var expected = original.ToString(Formatting.None); var request = Request(original); original.RemoveAll();
            var handler = new Handler(token => { request.RemoveAll(); return Task.FromResult(Response(200, JsonResult)); });
            using (var http = new HttpClient(handler))
            {
                var result = await Call(Api(http), request, mode, info);
                Assert.That(handler.Path, Is.EqualTo("/rest/projectresources")); Assert.That(handler.Query, Is.Empty);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Body, Is.EqualTo(expected));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-legacy-availability-token")); Assert.That(handler.Accept, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data; Assert.That(response.StatusCode, Is.EqualTo(200)); Assert.That(response.RawJsonContent, Is.EqualTo(JsonResult)); Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue)); Assert.That(data["records"][0]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(data["records"][0].Value<decimal>("days"), Is.EqualTo(1.234567890123456789m)); Assert.That(data.Value<bool>("future"), Is.True); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public void TypedChoicesRemainUnsetAndSearchCopiesAreDefensive()
        {
            Assert.That(RequestType.GetCustomAttribute<ObsoleteAttribute>(), Is.Not.Null);
            var request = (JObject)Activator.CreateInstance(RequestType); Assert.That(request.ToString(Formatting.None), Is.EqualTo("{}"));
            foreach (var name in new[] { "ProjectStartFrom", "ProjectStartTo" })
            {
                var property = RequestType.GetProperty(name); Assert.That(property.PropertyType, Is.EqualTo(typeof(long?))); property.SetValue(request, 0L); Assert.That(property.GetValue(request), Is.EqualTo(0L)); property.SetValue(request, null);
            }
            var source = new JObject { ["future"] = new JArray(1, 2) }; var search = RequestType.GetProperty("SearchSpecification"); search.SetValue(request, source);
            source.RemoveAll(); Assert.That(((JObject)search.GetValue(request)).ContainsKey("future"), Is.True);
            ((JObject)search.GetValue(request)).RemoveAll(); Assert.That(((JObject)search.GetValue(request)).ContainsKey("future"), Is.True);
            search.SetValue(request, null); Assert.That(request.ToString(Formatting.None), Is.EqualTo("{}"));
        }
        [Test]
        public void InvalidRequiredOrSearchKindsFailBeforeHttp([Values(0, 1, 2)] int mode, [Values("projectStartFrom", "projectStartTo")] string field, [Values(0, 1, 2, 3, 4)] int invalid)
        {
            var json = Range(0, 0);
            if (invalid == 0) json.Remove(field);
            else json[field] = invalid == 1 ? JValue.CreateNull() : invalid == 2 ? new JValue("1") : invalid == 3 ? new JValue(1.5m) : JToken.Parse("9223372036854775808");
            Invalid(json, mode);
        }
        [Test]
        public void InvalidSearchOrNonfiniteValuesFailBeforeHttp([Values(0, 1, 2)] int mode, [Values(false, true)] bool nonfinite)
        {
            var json = Range(0, 0); json[nonfinite ? "future" : "searchSpecification"] = nonfinite ? (JToken)new JArray(double.NaN) : new JValue("original string");
            Invalid(json, mode);
        }
        private static void Invalid(JObject json, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http), Request(json), mode, true); })); Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public void OriginalFactoriesAndErrorsRemainAuthoritative([Values(0, 1, 2)] int mode, [Values(400, 403, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original availability diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var request = Request(Range(0, 0));
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, request, mode, true); })); Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original availability diagnostic"));
                var expected = new InvalidOperationException("original hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("SearchProjectResourcesLegacy")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, request, mode, true); })), Is.SameAs(expected));
            }
        }
        [Test]
        public void PreCancellationAndNullBodyPreventHttp([Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Call(Api(http), null, 2, info); }));
                cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http), Request(Range(0, 0)), 2, info, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesIoWithoutReplay([Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, JsonResult); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Call(Api(http), Request(Range(0, 0)), 2, info, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }
        [Test]
        public void HistoricalYearMethodAndTokenRemainUnchanged()
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); Assert.That(api.GetProjectResourcesWithHttpInfo(0, 0).Data, Is.TypeOf<JObject>());
                Assert.That(handler.Path, Is.EqualTo("/rest/projectresources")); Assert.That(api.Configuration.AccessToken, Is.EqualTo("isolated-legacy-availability-token")); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public async Task UnspecifiedSuccessShapeAndRawContentAreRetained([Values(0, 1, 2)] int mode, [Values("", "null", "7", "true", "[]", "[{\"future\":true}]")] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, body)));
            using (var http = new HttpClient(handler))
            {
                var response = (ApiResponse<JToken>)await Call(Api(http), Request(Range(0, 0)), mode, true);
                Assert.That(response.RawJsonContent, Is.EqualTo(body));
                if (body == "") Assert.That(response.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(response.Data, JToken.Parse(body)), Is.True);
            }
        }

        [Test]
        public void MalformedAndDecimalOverflowResponsesRemainVisible([Values(0, 1, 2)] int mode, [Values("{invalid", "{\"days\":1e100}")] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, body)));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http), Request(Range(0, 0)), mode, true); }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task IndependentRangeValuesRemainInvariantWithoutOrderingRule([Values(0, 1, 2)] int mode)
        {
            var previous = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
                var json = new JObject { ["projectStartFrom"] = long.MaxValue, ["projectStartTo"] = long.MinValue, ["future"] = 1.25m };
                var expected = json.ToString(Formatting.None);
                var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
                using (var http = new HttpClient(handler))
                {
                    await Call(Api(http), Request(json), mode, true);
                    Assert.That(handler.Body, Is.EqualTo(expected)); Assert.That(handler.Calls, Is.EqualTo(1));
                }
            }
            finally { System.Globalization.CultureInfo.CurrentCulture = previous; }
        }

        private static HttpResponseMessage Response(int status, string body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.TryAddWithoutValidation("X-Fixture", "first"); response.Content.Headers.TryAddWithoutValidation("x-fixture", "second"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send; internal int Calls; internal string Path, Query, Body, Authorization, Accept; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
