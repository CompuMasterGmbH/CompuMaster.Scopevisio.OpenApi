using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
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
    public class AccountingReadProtocolTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 9).ToArray();
        private static readonly string[] Names = { "GetAccountMapping", "GetFiscalYears", "GetGainAndLossAdjustmentAccounts",
            "GetStatisticsAccountByNumber", "GetStatisticsPosting", "GetVatScopes",
            "SearchStatisticsAccounts", "SearchStatisticsPostings", "SearchTravelEntries" };
        private static readonly string[] Paths = { "/rest/accountmapping", "/rest/fiscalyears", "/rest/gainandlossadjustmentaccounts",
            "/rest/statisticsaccount/account%20%2F%20alpha%3F%23", "/rest/statisticspostings/9223372036854775807",
            "/rest/vatscopes", "/rest/statisticsaccounts", "/rest/statisticspostings", "/rest/travelentries" };
        private const string Json = "{\"id\":9223372036854775807,\"validFrom\":1791294919524,\"records\":[{\"id\":9223372036854775807,\"caseId\":9223372036854775807,\"createdTs\":1791294919524,\"active\":false,\"amount\":123456789.123456789}],\"entries\":[{\"accountScope\":\"0004711\",\"accountExternal\":\"0008400\"}],\"unknown\":null,\"date\":\"2026-10-12T12:34:56Z\"}";

        [Test]
        public async Task ReadsUseDocumentedWireContractsAndPreserveCompleteResults([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(new AccountingApi(Config(http)), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(route >= 6 ? HttpMethod.Post : HttpMethod.Get));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-accounting-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.EqualTo(route == 0 ? "?accountingArea=9223372036854775807" :
                    route == 2 ? "?balanceDate=29.02.2024" :
                    route == 3 || route == 4 ? "?fields=id,name & custom" : route == 5 ? "?active=false" : ""));
                Assert.That(handler.Body, Is.EqualTo(route >= 6 ? new SearchRequest { Page = 0, PageSize = 1000, CountOnly = false }.ToString(Formatting.None) : null));
                Assert.That(Property<int>(info, "StatusCode"), Is.EqualTo(200));
                Assert.That(Property<string>(info, "RawJsonContent"), Is.EqualTo(Json));
                Assert.That(Property<System.Collections.Generic.Dictionary<string, string>>(info, "Headers")["X-Fixture"], Is.EqualTo("first,second"));
                var data = Property<JToken>(info, "Data");
                Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(data["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(data["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OmittedOptionalValuesDoNotOverrideServerDefaults([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(new AccountingApi(Config(http)), route, 1, false, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.EqualTo(route == 2 ? "?balanceDate=29.02.2024" : ""));
                Assert.That(handler.Body, Is.Null);
                if (route == 3 || route == 4)
                {
                    var api = new AccountingApi(Config(http));
                    await Call(api, Names[route] + "AsyncWithHttpInfo", route == 3 ?
                        new object[] { "account", "", CancellationToken.None } : new object[] { long.MaxValue, "", CancellationToken.None },
                        route == 3 ? new[] { typeof(string), typeof(string), typeof(CancellationToken) } : new[] { typeof(long), typeof(string), typeof(CancellationToken) });
                    Assert.That(handler.Uri.Query, Is.EqualTo("?fields="));
                }
            }
        }

        [Test]
        public async Task EmptyAndJsonNullRepliesRemainDistinctInRawContent([ValueSource(nameof(Routes))] int route, [Values("", "null")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(new AccountingApi(Config(http)), route, 2, false, CancellationToken.None);
                Assert.That(Property<string>(info, "RawJsonContent"), Is.EqualTo(json));
                var data = Property<JToken>(info, "Data");
                if (json == "" || route == 0 || route == 5) Assert.That(data, Is.Null);
                else Assert.That(data.Type, Is.EqualTo(JTokenType.Null));
            }
        }

        [Test]
        public async Task ServerDiagnosticsAndCustomExceptionIdentityArePreserved([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "missing profile")));
            using (var http = new HttpClient(handler))
            {
                var api = new AccountingApi(Config(http));
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(403));
                Assert.That((object)error.ErrorContent, Is.EqualTo("missing profile"));
                var expected = new InvalidOperationException("fixture hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsHttp([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => {
                    await Invoke(new AccountingApi(Config(http)), route, 2, false, cancellation.Token);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesActualPendingTransport([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(new AccountingApi(Config(http)), route, 2, false, cancellation.Token);
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
        public void InvalidBalanceDatesFailBeforeHttp([Values(null, "", " ", "29.02.2023", "2024-02-29", "1.2.2024", "31.04.2024")] string date, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var api = new AccountingApi(Config(http));
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(api, "GetGainAndLossAdjustmentAccounts" + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"),
                        mode == 2 ? new object[] { date, CancellationToken.None } : new object[] { date },
                        mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) });
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void MissingStatisticsAccountNumbersFailBeforeHttp([Values(null, "", " ", ".", "..")] string number, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(new AccountingApi(Config(http)), "GetStatisticsAccountByNumber" + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"),
                        mode == 2 ? new object[] { number, null, CancellationToken.None } : new object[] { number, null },
                        mode == 2 ? new[] { typeof(string), typeof(string), typeof(CancellationToken) } : new[] { typeof(string), typeof(string) });
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ConvenienceMethodsExposeResults([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false);
                var data = (JToken)await Call(new AccountingApi(Config(http)), Names[route] + (asynchronous ? "Async" : ""), args, Types(route, false));
                Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }


        [TestCase(0, "[]"), TestCase(5, "[]"), TestCase(0, "\"text\""), TestCase(5, "\"text\"")]
        [TestCase(0, "{\"entries\":{}}"), TestCase(5, "{\"records\":{}}")]
        [TestCase(0, "{\"entries\":[1]}"), TestCase(5, "{\"records\":[null]}")]
        public void InvalidTypedRootsAndCollectionsFailClearly(int route, string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => {
                    await Invoke(new AccountingApi(Config(http)), route, 2, false, CancellationToken.None);
                }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [TestCase(0), TestCase(1), TestCase(2), TestCase(3)]
        public void TypedModelsDeeplyCopyUnknownFieldsAndRejectNullSources(int model)
        {
            Func<JObject, JObject> create;
            if (model == 0) create = json => new AccountMappingResponse(json);
            else if (model == 1) create = json => new AccountMappingEntryRecord(json);
            else if (model == 2) create = json => new VatScopeRecord(json);
            else create = json => new VatScopesResponse(json);
            Assert.Throws<ArgumentNullException>((Action)(() => { create(null); }));
            var source = JObject.Parse("{\"unknown\":{\"nested\":[1,null]}}");
            var result = create(source);
            ((JArray)source["unknown"]["nested"])[0] = 2;
            Assert.That((int)result["unknown"]["nested"][0], Is.EqualTo(1));
            Assert.That(result["unknown"]["nested"][1].Type, Is.EqualTo(JTokenType.Null));
        }

        [TestCase(false, ""), TestCase(true, "")]
        [TestCase(false, "null"), TestCase(true, "null")]
        [TestCase(false, "[]"), TestCase(true, "[]")]
        public void TypedListsDistinguishUnknownFromKnownEmpty(bool vat, string array)
        {
            var key = vat ? "records" : "entries";
            var source = array == "" ? new JObject() : JObject.Parse("{\"" + key + "\":" + array + "}");
            if (vat)
            {
                var result = new VatScopesResponse(source);
                if (array == "[]") Assert.That(result.Records, Is.Empty); else Assert.That(result.Records, Is.Null);
            }
            else
            {
                var result = new AccountMappingResponse(source);
                if (array == "[]") Assert.That(result.Entries, Is.Empty); else Assert.That(result.Entries, Is.Null);
            }
        }

        [Test]
        public void MappingRecordsPreserveLeadingZerosInt64EpochsAndCopiedEntries()
        {
            var source = JObject.Parse("{\"id\":9223372036854775807,\"validFrom\":1791294919524,\"validTill\":null,\"includePersonalAccounts\":false,\"entryCount\":1,\"entries\":[{\"id\":9223372036854775807,\"accountScope\":\"0004711\",\"accountExternal\":\"0008400\",\"modifiedTs\":1791294919524}]}");
            var response = new AccountMappingResponse(source);
            Assert.That(response.Id, Is.EqualTo(long.MaxValue));
            Assert.That(response.ValidFrom, Is.EqualTo(1791294919524L));
            Assert.That(response.ValidTill, Is.Null);
            Assert.That(response.AccountingArea, Is.Null);
            Assert.That(response.IncludePersonalAccounts, Is.False);
            Assert.That(response.EntryCount, Is.EqualTo(1));
            var entry = response.Entries[0];
            Assert.That(entry.Id, Is.EqualTo(long.MaxValue));
            Assert.That(entry.AccountScope, Is.EqualTo("0004711"));
            Assert.That(entry.AccountExternal, Is.EqualTo("0008400"));
            Assert.That(entry.ModifiedTs, Is.EqualTo(1791294919524L));
            entry["accountScope"] = "changed";
            Assert.That(response.Entries[0].AccountScope, Is.EqualTo("0004711"));
        }

        [Test]
        public void VatRecordsPreserveNullableFalseUnknownFieldsAndInt64CaseIds()
        {
            var response = new VatScopesResponse(JObject.Parse("{\"records\":[{\"id\":9223372036854775807,\"caseId\":9223372036854775807,\"createdTs\":1791294919524,\"active\":false,\"unknown\":[null]}]}"));
            var record = response.Records[0];
            Assert.That(record.Id, Is.EqualTo(long.MaxValue));
            Assert.That(record.CaseId, Is.EqualTo(long.MaxValue));
            Assert.That(record.CreatedTs, Is.EqualTo(1791294919524L));
            Assert.That(record.Active, Is.False);
            Assert.That(record.ModifiedTs, Is.Null);
            Assert.That(record.CreatorUid, Is.Null);
            Assert.That(record["unknown"][0].Type, Is.EqualTo(JTokenType.Null));
            record["active"] = true;
            Assert.That(response.Records[0].Active, Is.False);
        }

        [Test]
        public async Task IntegerAndBooleanQueriesUseInvariantWireValues()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var api = new AccountingApi(Config(http));
                var original = System.Globalization.CultureInfo.CurrentCulture;
                try
                {
                    System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
                    await api.GetAccountMappingAsyncWithHttpInfo(long.MinValue, CancellationToken.None);
                    Assert.That(handler.Uri.Query, Is.EqualTo("?accountingArea=-9223372036854775808"));
                    await api.GetVatScopesAsyncWithHttpInfo(true, CancellationToken.None);
                    Assert.That(handler.Uri.Query, Is.EqualTo("?active=true"));
                    await api.GetStatisticsPostingAsyncWithHttpInfo(long.MinValue, null, CancellationToken.None);
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/statisticspostings/-9223372036854775808"));
                }
                finally { System.Globalization.CultureInfo.CurrentCulture = original; }
            }
        }

        private static Task<object> Invoke(AccountingApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }

        private static object[] Arguments(int route, bool explicitValues)
        {
            if (route == 0) return new object[] { explicitValues ? (long?)long.MaxValue : null };
            if (route == 1) return new object[0];
            if (route == 2) return new object[] { "29.02.2024" };
            if (route == 3) return new object[] { "account / alpha?#", explicitValues ? "id,name & custom" : null };
            if (route == 4) return new object[] { long.MaxValue, explicitValues ? "id,name & custom" : null };
            if (route == 5) return new object[] { explicitValues ? (bool?)false : null };
            return new object[] { explicitValues ? new SearchRequest { Page = 0, PageSize = 1000, CountOnly = false } : null };
        }

        private static Type[] Types(int route, bool cancellation)
        {
            var types = route == 0 ? new[] { typeof(long?) } : route == 1 ? new Type[0] :
                route == 2 ? new[] { typeof(string) } : route == 3 ? new[] { typeof(string), typeof(string) } :
                route == 4 ? new[] { typeof(long), typeof(string) } : route == 5 ? new[] { typeof(bool?) } : new[] { typeof(SearchRequest) };
            return cancellation ? types.Concat(new[] { typeof(CancellationToken) }).ToArray() : types;
        }

        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The public workflow " + name + " is absent.");
            object invoked;
            try { invoked = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(invoked is Task task)) return invoked;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }

        private static T Property<T>(object instance, string name) => (T)instance.GetType().GetProperty(name).GetValue(instance);
        private static Configuration Config(HttpClient http) => new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-accounting-token" };
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
