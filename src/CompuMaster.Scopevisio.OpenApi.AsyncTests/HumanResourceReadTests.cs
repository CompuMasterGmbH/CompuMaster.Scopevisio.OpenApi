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
    public class HumanResourceReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 2).ToArray();
                private static readonly string[] Names = { "GetHumanResourceDetails", "GetHumanResourceAvailability" };
        private static readonly string[] Paths = { "/rest/humanresource/9223372036854775807", "/rest/humanresourcesavailability/v2" };
        private const string Json = "{\"id\":9223372036854775807,\"fee\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-12T01:02:03Z\",\"records\":[{\"id\":9223372036854775807,\"contactId\":9223372036854775807,\"month\":\"2026-10\",\"available\":5}],\"more\":false}";

        [Test]
        public async Task HumanResourceReadsUseExactContracts([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(route == 0 ? HttpMethod.Get : HttpMethod.Post));
                if (route == 0) Assert.That(handler.Body, Is.Null);
                else Assert.That(JObject.Parse(handler.Body).ToString(Newtonsoft.Json.Formatting.None), Is.EqualTo(RequestJson(true)));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-human-resource-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.EqualTo(route == 0 ? "?fields=id,amount & custom&formatValues=false" : ""));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data.Value<decimal>("fee"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalValuesAreOmittedWithoutInventingPaging([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), route, 1, false, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.Empty);
                if (route == 1) Assert.That(JObject.Parse(handler.Body).ToString(Newtonsoft.Json.Formatting.None), Is.EqualTo(RequestJson(false)));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task UnspecifiedReplyShapesAndNullAreRetained([ValueSource(nameof(Routes))] int route, [Values("{}", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, 2, false, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "" || json == "null") Assert.That(info.Data, Is.Null);
                else Assert.That(info.Data.Type, Is.EqualTo(JTokenType.Object));
            }
        }

        [Test]
        public async Task ServerProfileFailuresAndCustomHookExceptionsArePreserved([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "required reporting profile missing")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("required reporting profile missing"));
                var expected = new InvalidOperationException("custom reporting hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsEveryDataSourceRequest([ValueSource(nameof(Routes))] int route)
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
        public async Task ActiveCancellationReachesEveryDataSourceTransport([ValueSource(nameof(Routes))] int route)
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
                Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }



        [Test]
        public async Task DetailFormattingAndEmptyFieldsStayExplicit([Values(false, true)] bool formatting, [Values(long.MinValue, 0L, long.MaxValue)] long id)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Api(http).GetHumanResourceDetailsAsyncWithHttpInfo(id, "", formatting, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/humanresource/" + id.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Assert.That(handler.Uri.Query, Is.EqualTo("?fields=&formatValues=" + (formatting ? "true" : "false")));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void AvailabilityRequiresRawNonblankStringBounds([Values("validFrom", "validTill")] string field, [Values("missing", "null", "0", "202601", "2026.01", "true", "[]", "{}", "\"\"", "\" \"")] string token, [Values(0, 2)] int mode)
        {
            var query = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest("2026-01", "2026-12");
            if (token == "missing") query.Remove(field); else query[field] = JToken.Parse(token);
            var handler = new Handler(cancellation => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    if (mode == 0) Api(http).GetHumanResourceAvailabilityWithHttpInfo(query);
                    else await Api(http).GetHumanResourceAvailabilityAsyncWithHttpInfo(query, CancellationToken.None);
                }));
                Assert.That(error.ParamName, Is.EqualTo("query")); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void AvailabilitySearchRequiresObjectOrNull([Values("[]", "1", "true", "\"search\"")] string token, [Values(0, 2)] int mode)
        {
            var query = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest("2026-01", "2026-12");
            query["searchSpecification"] = JToken.Parse(token);
            var handler = new Handler(cancellation => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    if (mode == 0) Api(http).GetHumanResourceAvailabilityWithHttpInfo(query);
                    else await Api(http).GetHumanResourceAvailabilityAsyncWithHttpInfo(query, CancellationToken.None);
                }));
                Assert.That(error.ParamName, Is.EqualTo("query")); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task CallerSelectedMonthFormatsAndOrderAreNotRewritten([Values("2026-01", "01.01.2026", " 2026-01 ")] string from, [Values("2025-01", "31.12.2025")] string till)
        {
            var query = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest(from, till);
            query["futureField"] = 9223372036854775807L; query["searchSpecification"] = JValue.CreateNull();
            var handler = new Handler(cancellation => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Api(http).GetHumanResourceAvailabilityAsyncWithHttpInfo(query, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<string>("validFrom"), Is.EqualTo(from)); Assert.That(body.Value<string>("validTill"), Is.EqualTo(till));
                Assert.That(body.Value<long>("futureField"), Is.EqualTo(long.MaxValue)); Assert.That(body["searchSpecification"].Type, Is.EqualTo(JTokenType.Null));
            }
        }

        [Test]
        public void AvailabilityNullQueryFailsBeforeHttp([Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(cancellation => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<ArgumentNullException>((Func<Task>)(async () => {
                    if (asynchronous) await Api(http).GetHumanResourceAvailabilityAsyncWithHttpInfo(null, CancellationToken.None);
                    else Api(http).GetHumanResourceAvailabilityWithHttpInfo(null);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void TypedWorkflowsRejectNonObjectReplies([ValueSource(nameof(Routes))] int route, [Values("[]", "1", "\"text\"")] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, body)));
            using (var http = new HttpClient(handler))
                Assert.ThrowsAsync<Newtonsoft.Json.JsonSerializationException>((Func<Task>)(async () => { await Invoke(Api(http), route, 2, false, CancellationToken.None); }));
        }

        [Test]
        public void AvailabilityRejectsMalformedRecordCollections([Values("1", "{}", "[null]", "[1]", "[[]]")] string records)
        {
            var json = "{\"records\":" + records + "}";
            Assert.Throws<Newtonsoft.Json.JsonSerializationException>((Action)(() => new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityResponse(JObject.Parse(json))));
        }

        [Test]
        public void TypedModelsRetainUnknownsAndCopiedRecords()
        {
            var source = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(Json, new Newtonsoft.Json.JsonSerializerSettings { DateParseHandling = Newtonsoft.Json.DateParseHandling.None, FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Decimal });
            var record = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceRecord(source);
            source["id"] = 0; Assert.That(record.Id, Is.EqualTo(long.MaxValue)); Assert.That(record.Fee, Is.EqualTo(123456789.123456789m));
            Assert.That(record["unknown"].Type, Is.EqualTo(JTokenType.Null)); Assert.That(record["when"].Type, Is.EqualTo(JTokenType.String));
            var response = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityResponse(record);
            var records = response.Records; Assert.That(records[0].Id, Is.EqualTo(long.MaxValue)); Assert.That(records[0].ContactId, Is.EqualTo(long.MaxValue));
            Assert.That(records[0].Month, Is.EqualTo("2026-10")); Assert.That(records[0].Available, Is.EqualTo(5));
            records[0]["id"] = 0; Assert.That(response.Records[0].Id, Is.EqualTo(long.MaxValue));
            Assert.That(((System.Collections.Generic.ICollection<CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRecord>)records).IsReadOnly, Is.True);
            var querySource = JObject.Parse(RequestJson(true));
            var query = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest(querySource);
            querySource["validFrom"] = "changed"; Assert.That(query.ValidFrom, Is.EqualTo("2026-01"));
            var search = query.SearchSpecification; search["changed"] = true; Assert.That(query.SearchSpecification["changed"], Is.Null);
            query.SearchSpecification = null; Assert.That(query.ContainsKey("searchSpecification"), Is.False);
        }

        [Test]
        public void MissingNullAndKnownEmptyRecordMetadataRemainDistinct([Values("{}", "{\"records\":null}", "{\"records\":[]}") ] string json)
        {
            var response = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityResponse(JObject.Parse(json));
            if (json.Contains("[]")) Assert.That(response.Records, Is.Empty); else Assert.That(response.Records, Is.Null);
        }

        [Test]
        public async Task TypedNativeConvenienceOverloadsCompileForStandaloneConsumers()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var query = new CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest("2026-01", "2026-12");
                Assert.That((await api.GetHumanResourceDetailsAsync(long.MaxValue, null, false, CancellationToken.None)).Id, Is.EqualTo(long.MaxValue));
                Assert.That((await api.GetHumanResourceAvailabilityAsync(query, CancellationToken.None)).Records[0].ContactId, Is.EqualTo(long.MaxValue));
            }
        }

        private sealed class Result { public int StatusCode; public string RawJsonContent; public JToken Data; public System.Collections.Generic.IDictionary<string, string> Headers; }
        private static string RequestJson(bool search) => search ? "{\"validFrom\":\"2026-01\",\"validTill\":\"2026-12\",\"searchSpecification\":{\"search\":{\"contactId\":9223372036854775807},\"unknown\":null}}" : "{\"validFrom\":\"2026-01\",\"validTill\":\"2026-12\"}";

        private static async Task<Result> Invoke(HumanResourcesApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            var response = await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
            return new Result { Data = (JToken)response.GetType().GetProperty("Data").GetValue(response), StatusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response),
                RawJsonContent = (string)response.GetType().GetProperty("RawJsonContent").GetValue(response), Headers = (System.Collections.Generic.IDictionary<string, string>)response.GetType().GetProperty("Headers").GetValue(response) };
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            if (route == 0) return new object[] { long.MaxValue, explicitValues ? "id,amount & custom" : null, explicitValues ? (object)false : null };
            var type = typeof(HumanResourcesApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest");
            Assert.That(type, Is.Not.Null, "The structured request is absent.");
            return new object[] { Activator.CreateInstance(type, new object[] { JObject.Parse(RequestJson(explicitValues)) }) };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var types = route == 0 ? new[] { typeof(long), typeof(string), typeof(bool?) } : new[] { typeof(HumanResourcesApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.HumanResourceAvailabilityRequest") };
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
        private static HumanResourcesApi Api(HttpClient http) => new HumanResourcesApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-human-resource-token" });
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
