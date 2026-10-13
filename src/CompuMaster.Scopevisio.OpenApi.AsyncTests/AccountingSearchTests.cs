using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
    public class AccountingSearchTests
    {
        private const string JsonResult = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-13T00:00:00Z\",\"future\":null}],\"extra\":{\"hasNext\":true}}";
        private static readonly string[] Names = { "SearchDebitorAccounts", "SearchKreditorAccounts", "SearchImpersonalAccounts", "SearchJournalEntries", "SearchPersonalJournalEntries", "SearchDimensions", "SearchZones" };
        private static readonly string[] LegacyNames = { "GetDebitorAccounts", "GetKreditorAccounts", "GetImpersonalAccounts", "GetJournalEntries", "GetPersonalJournalEntries", "GetAllDimensionEntries", "GetZones" };
        private static readonly string[] Paths = { "/rest/debitoraccounts", "/rest/kreditoraccounts", "/rest/impersonalaccounts", "/rest/journal", "/rest/personaljournal", "/rest/dimensions", "/rest/zones" };
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-accounting-token" };
            return new AccountingApi(config);
        }
        private static async Task<object> Call(object api, int route, int mode, bool info, SearchRequest query, CancellationToken token = default(CancellationToken))
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = api.GetType().GetMethod(name, mode == 2 ? new[] { typeof(SearchRequest), typeof(CancellationToken) } : new[] { typeof(SearchRequest) });
            Assert.That(method, Is.Not.Null, "The complete accounting search " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, mode == 2 ? new object[] { query, token } : new object[] { query }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static SearchRequest Query()
        {
            var query = new SearchRequest { Page = int.MinValue, PageSize = int.MaxValue, Fields = new[] { "amount", "future" }, FormatValues = false, Order = new[] { "amount = desc" }, CountOnly = false };
            query.Search = new JArray(new JObject { ["fields"] = "number, name", ["values"] = "0001, Ä", ["operator"] = "contains" }, new JObject { ["field"] = "amount", ["value"] = 1.234567890123456789m, ["operator"] = "greater" }, new JObject { ["field"] = "future", ["value"] = JValue.CreateNull(), ["operator"] = "equal" });
            query["futureFilter"] = new JObject { ["date"] = "2026-10-13", ["id"] = long.MinValue };
            query["createdSince"] = long.MinValue; query["createdBefore"] = long.MaxValue;
            query["postingDateSince"] = 0L; query["postingDateBefore"] = -1L; query["externalDocumentDateSince"] = long.MaxValue;
            return query;
        }
        [Test]
        public async Task OriginalOptionalSearchAndCompleteResultsAreRetained([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(0, 1, 2)] int choice)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            var query = choice == 0 ? null : choice == 1 ? new SearchRequest() : Query();
            var original = query?.ToString(Formatting.None);
            using (var http = new HttpClient(handler))
            {
                if (route < 3 && choice == 0)
                {
                    Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, info, query); }));
                    Assert.That(handler.Calls, Is.Zero); return;
                }
                var result = await Call(Api(http, route), route, mode, info, query);
                Assert.That(handler.Path, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Body, Is.EqualTo(original));
                Assert.That(handler.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-accounting-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                if (choice != 0) Assert.That(handler.ContentType, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data;
                    Assert.That(response.RawJsonContent, Is.EqualTo(JsonResult));
                    Assert.That(response.StatusCode, Is.EqualTo(200));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["records"][0]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(data["records"][0]["future"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(data["extra"].Value<bool>("hasNext"), Is.True);
                Assert.That(query?.ToString(Formatting.None), Is.EqualTo(original));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public async Task CountUnknownAndEmptyRepresentationsRemainDistinct([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode, [Values("9223372036854775807", "[]", "{\"future\":true}", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, json)));
            using (var http = new HttpClient(handler))
            {
                var query = new SearchRequest { CountOnly = true, Page = -1, PageSize = 2000 };
                var result = (ApiResponse<JToken>)await Call(Api(http, route), route, mode, true, query);
                Assert.That(result.RawJsonContent, Is.EqualTo(json));
                Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None)));
                if (json == "") Assert.That(result.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(result.Data, JToken.Parse(json)), Is.True);
            }
        }
        [Test]
        public void OriginalErrorsAndFactoriesRemainAuthoritative([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode, [Values(403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original accounting diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, route, mode, true, Query()); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original accounting diagnostic"));
                var expected = new InvalidOperationException("original hook diagnostic");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, route, mode, true, Query()); })), Is.SameAs(expected));
            }
        }
        [Test]
        public void MalformedJsonAndDecimalOverflowAreReported([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode, [Values("{", "{\"amount\":1e100}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, json)));
            using (var http = new HttpClient(handler))
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, Query()); }));
        }
        [Test]
        public void PreCancellationPreventsHttp([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http, route), route, 2, info, null, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesHttp([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, JsonResult); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Call(Api(http, route), route, 2, info, Query(), cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, task, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel(); Assert.That(await Task.WhenAny(task, Task.Delay(5000)), Is.SameAs(task));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await task; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }
        [Test]
        public async Task SearchBodyIsSerializedBeforeHttpAdmission([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode)
        {
            var query = Query(); var original = query.ToString(Formatting.None);
            var handler = new Handler(token => { query.RemoveAll(); query["changed"] = true; return Task.FromResult(Response(200, JsonResult)); });
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, true, query);
                Assert.That(handler.Body, Is.EqualTo(original));
                Assert.That(query.Value<bool>("changed"), Is.True);
            }
        }
        [Test]
        public async Task SearchValuesUseInvariantJsonWithoutClientClamping([Values(0, 1, 2, 3, 4, 5, 6)] int route, [Values(0, 1, 2)] int mode)
        {
            var previous = System.Globalization.CultureInfo.CurrentCulture;
            var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NegativeSign = "NEG"; culture.NumberFormat.NumberDecimalSeparator = ",";
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
                using (var http = new HttpClient(handler))
                {
                    await Call(Api(http, route), route, mode, true, Query());
                    Assert.That(handler.Body, Does.Contain("-2147483648").And.Contain("2147483647").And.Contain("1.234567890123456789").And.Contain("-9223372036854775808"));
                }
            }
            finally { System.Globalization.CultureInfo.CurrentCulture = previous; }
        }
        [Test]
        public void LegacyDataAndOptionalBodyRemainUnchanged([Values(0, 1, 2, 3, 4, 5, 6)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var method = api.GetType().GetMethod(LegacyNames[route] + "WithHttpInfo", new[] { typeof(string) });
                if (route < 3)
                {
                    var missing = Assert.Throws<TargetInvocationException>((Action)(() => { method.Invoke(api, new object[] { null }); }));
                    Assert.That(missing.InnerException, Is.TypeOf<ApiException>()); Assert.That(handler.Calls, Is.Zero);
                }
                else
                {
                    var omitted = (ApiResponse<object>)method.Invoke(api, new object[] { null });
                    Assert.That(omitted.Data, Is.Null); Assert.That(handler.Body, Is.Null);
                }
                var response = (ApiResponse<object>)method.Invoke(api, new object[] { "{}" });
                Assert.That(response.Data, Is.Null); Assert.That(handler.Body, Is.EqualTo(JsonConvert.SerializeObject("{}")));
                Assert.That(((Configuration)api.GetType().GetProperty("Configuration").GetValue(api)).AccessToken, Is.EqualTo("isolated-accounting-token"));
            }
        }
        [Test]
        public void RequiredAndOptionalSignaturesMatchBodyContracts([Values(0, 1, 2, 3, 4, 5, 6)] int route)
        {
            foreach (var suffix in new[] { "", "WithHttpInfo", "Async", "AsyncWithHttpInfo" })
            {
                var method = typeof(AccountingApi).GetMethod(Names[route] + suffix, new[] { typeof(SearchRequest) });
                Assert.That(method, Is.Not.Null);
                var parameter = method.GetParameters()[0];
                Assert.That(parameter.IsOptional, Is.EqualTo(route >= 3));
                if (route >= 3) Assert.That(parameter.DefaultValue, Is.Null);
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
            internal int Calls; internal string Path, Query, Body, Accept, Authorization, ContentType; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid"));
                Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                ContentType = request.Content?.Headers.ContentType?.MediaType;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
