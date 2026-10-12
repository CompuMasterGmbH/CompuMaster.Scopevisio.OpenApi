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
    public class MlExportReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 4).ToArray();
        private static readonly string[] Names = { "SearchHypatosExport", "SearchInvoiceExport", "SearchJournalExport", "SearchPersonalAccountExport" };
        private static readonly string[] Paths = { "/rest/mlexport/hypatos", "/rest/mlexport/invoice", "/rest/mlexport/journal", "/rest/mlexport/personal-account" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-12T01:02:03Z\",\"decimalText\":\"000123456789.1234567890000000000000\",\"postingXml\":\"<posting amount=\\\"1.2300\\\"> Ä & raw </posting>\",\"ocr\":{\"original\":[1,null,\" text \"]}}],\"more\":false}";

        [Test]
        public async Task ExportUsesExactPostBody([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(JToken.Parse(handler.Body).Type, Is.EqualTo(JTokenType.Object));
                Assert.That(JToken.DeepEquals(JToken.Parse(handler.Body), Query(route)), Is.True);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-ml-export-token"));
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
                Assert.That(info.Data["records"][0].Value<string>("decimalText"), Is.EqualTo("000123456789.1234567890000000000000"));
                Assert.That(info.Data["records"][0].Value<string>("postingXml"), Is.EqualTo("<posting amount=\"1.2300\"> Ä & raw </posting>"));
                Assert.That(info.Data["records"][0]["ocr"].Type, Is.EqualTo(JTokenType.Object));
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
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "required journal or personal-account profile missing")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("required journal or personal-account profile missing"));
                var expected = new InvalidOperationException("custom open-item hook");
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
        public async Task ExplicitEmptySearchDiffersFromOmittedBody([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[0] = route == 2 ? (object)new CompuMaster.Scopevisio.OpenApi.Model.JournalExportSearchRequest() : new CompuMaster.Scopevisio.OpenApi.Model.IdExportSearchRequest();
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                await Call(Api(http), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
                Assert.That(handler.Body, Is.EqualTo("{}"));
                Assert.That(handler.Uri.Query, Is.Empty);
            }
        }

        [Test]
        public async Task SearchSnapshotIsUnaffectedByLaterCallerMutation([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var query = Query(route); var original = query.ToString(Newtonsoft.Json.Formatting.None);
                var args = Arguments(route, false); args[0] = query;
                var operation = Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    query["future"] = "changed"; query["page"] = 99;
                    release.TrySetResult(true); await operation;
                    Assert.That(handler.Body, Is.EqualTo(original));
                }
                finally { release.TrySetResult(true); }
            }
        }

        [Test]
        public async Task StandaloneCancellationOverloadsExposeJson([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, true).Concat(new object[] { CancellationToken.None }).ToArray();
                var data = (JToken)await Call(Api(http), Names[route] + "Async", args, Types(route, true));
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }


        [Test]
        public async Task RawCursorTypesFailBeforeTransport([ValueSource(nameof(Routes))] int route,
            [Values("\"1\"", "1.5", "true", "{}", "9223372036854775808")] string raw, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var query = Query(route); query[route == 2 ? "afterRowNumber" : "afterId"] = JToken.Parse(raw);
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { query }, Types(route, false));
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task RawJournalBoundsFailBeforeTransport([Values("from", "to")] string field,
            [Values("\"1\"", "1.5", "true", "{}", "9223372036854775808")] string raw, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var query = Query(2); query[field] = JToken.Parse(raw);
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[2] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { query }, Types(2, false));
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CursorInt64BoundsRemainJsonIntegers([ValueSource(nameof(Routes))] int route,
            [Values(long.MinValue, 0L, long.MaxValue)] long value)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var query = Query(route); var key = route == 2 ? "afterRowNumber" : "afterId"; query[key] = value;
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", new object[] { query }, Types(route, false));
                var body = JObject.Parse(handler.Body);
                Assert.That(body[key].Type, Is.EqualTo(JTokenType.Integer));
                Assert.That(body.Value<long>(key), Is.EqualTo(value));
            }
        }

        [Test]
        public void CopiedRequestAndTypedNullChoicesPreserveUnknownFields([ValueSource(nameof(Routes))] int route)
        {
            var original = Query(route); var source = (JObject)original.DeepClone();
            var copy = (JObject)Activator.CreateInstance(original.GetType(), source);
            source["future"]["exact"] = "changed";
            Assert.That(copy["future"]["exact"].Type, Is.EqualTo(JTokenType.Null));
            var property = copy.GetType().GetProperty(route == 2 ? "AfterRowNumber" : "AfterId");
            var key = route == 2 ? "afterRowNumber" : "afterId";
            property.SetValue(copy, (long?)0L);
            Assert.That(property.GetValue(copy), Is.EqualTo(0L));
            Assert.That(copy[key].Type, Is.EqualTo(JTokenType.Integer));
            property.SetValue(copy, null);
            Assert.That(copy.Property(key), Is.Null);
            copy[key] = JValue.CreateNull();
            Assert.That(property.GetValue(copy), Is.Null);
            if (route == 2)
            {
                foreach (var pair in new[] { new[] { "From", "from" }, new[] { "To", "to" } })
                {
                    var bound = copy.GetType().GetProperty(pair[0]); bound.SetValue(copy, (long?)long.MinValue);
                    Assert.That(bound.GetValue(copy), Is.EqualTo(long.MinValue));
                    bound.SetValue(copy, null); Assert.That(copy.Property(pair[1]), Is.Null);
                }
            }
        }

        [Test]
        public async Task ExplicitNullCursorRemainsDistinctFromOmission([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var query = Query(route); var key = route == 2 ? "afterRowNumber" : "afterId"; query[key] = JValue.CreateNull();
                await Call(Api(http), Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { query }, Types(route, false));
                Assert.That(JObject.Parse(handler.Body)[key].Type, Is.EqualTo(JTokenType.Null));
            }
        }

        [Test]
        public async Task JournalWindowOrderingRemainsServerValidated([Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var query = new CompuMaster.Scopevisio.OpenApi.Model.JournalExportSearchRequest { From = long.MaxValue, To = long.MinValue };
                await Call(Api(http), Names[2] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { query }, Types(2, false));
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<long>("from"), Is.EqualTo(long.MaxValue));
                Assert.That(body.Value<long>("to"), Is.EqualTo(long.MinValue));
            }
        }

        private static CompuMaster.Scopevisio.OpenApi.Model.SearchRequest Query(int route)
        {
            var search = JObject.Parse("{\"page\":0,\"pageSize\":13,\"fields\":[\"id\",\"amount\"],\"formatValues\":false,\"count\":true,\"order\":[\"id = asc\"],\"search\":[{\"field\":\"name\",\"operator\":\"contains\",\"value\":\"0001\"}],\"future\":{\"exact\":null}}");
            if (route == 2)
            {
                search["afterRowNumber"] = long.MaxValue; search["from"] = long.MinValue; search["to"] = 0L;
                return new CompuMaster.Scopevisio.OpenApi.Model.JournalExportSearchRequest(search);
            }
            search["afterId"] = long.MaxValue;
            return new CompuMaster.Scopevisio.OpenApi.Model.IdExportSearchRequest(search);
        }
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            return new object[] { explicitValues ? Query(route) : null };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var types = new[] { route == 2 ? typeof(CompuMaster.Scopevisio.OpenApi.Model.JournalExportSearchRequest) : typeof(CompuMaster.Scopevisio.OpenApi.Model.IdExportSearchRequest) };
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
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-ml-export-token" });
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
