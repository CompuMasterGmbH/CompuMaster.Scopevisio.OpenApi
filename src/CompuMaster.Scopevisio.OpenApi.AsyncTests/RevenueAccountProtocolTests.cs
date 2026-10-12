using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class RevenueAccountProtocolTests
    {
        private static readonly int[] Routes = { 0, 1, 2 };
        private static readonly string[] Names = { "GetProductRevenueAccounts", "GetProductsRevenueAccounts", "GetStandardRevenueAccounts" };
        private static readonly string[] Paths = { "/rest/revenueaccounts/product/9223372036854775807", "/rest/revenueaccounts/products", "/rest/revenueaccounts/standard" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"accountNumber\":\"0004711\",\"productType\":9223372036854775807,\"createdTs\":1791294919524,\"matrix\":false,\"futureAmount\":123456789.123456789,\"futureNull\":null}],\"more\":false}";

        [Test]
        public async Task ReadsPreserveDocumentedGetContractsAndCompleteResults([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-revenue-token"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.EqualTo("?fields=accountNumber,createdTs & custom" +
                    (route == 0 ? "&preferred=false" : route == 1 ? "&page=0&pageSize=1001" : "") +
                    "&country=DE&vatScope=-9223372036854775808&active=false&servicesRenderedDate=29.02.2024"));
                Assert.That(Property<int>(info, "StatusCode"), Is.EqualTo(200));
                Assert.That(Property<string>(info, "RawJsonContent"), Is.EqualTo(Json));
                var data = Property<Newtonsoft.Json.Linq.JToken>(info, "Data");
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["records"][0].Value<string>("accountNumber"), Is.EqualTo("0004711"));
                Assert.That(data["records"][0].Value<decimal>("futureAmount"), Is.EqualTo(123456789.123456789m));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalDefaultsAreOmittedAndNoPagingIsInvented([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), route, 1, false, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task HttpErrorsAndCallerHooksKeepDiagnosticsAndIdentity([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.NotFound, "no product or missing profile")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(404));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("no product or missing profile"));
                var expected = new InvalidOperationException("caller hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationDoesNotSend([ValueSource(nameof(Routes))] int route)
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
        public async Task ActiveCancellationReachesHttpIo([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Invoke(Api(http), route, 2, false, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, task, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(task, Task.Delay(5000)), Is.SameAs(task));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await task; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public async Task ConvenienceMethodsReturnTypedData([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var result = (Newtonsoft.Json.Linq.JToken)await Call(Api(http), Names[route] + (asynchronous ? "Async" : ""), Arguments(route, false), Types(route, false));
                Assert.That(result["records"][0].Value<long>("productType"), Is.EqualTo(long.MaxValue));
            }
        }


        [Test]
        public void InvalidDatesAreRejectedBeforeHttp([ValueSource(nameof(Routes))] int route,
            [Values("", " ", "2024-02-29", "29.02.2023", "31.04.2024", "1.2.2024")] string date, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[args.Length - 1] = date;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
                }));
                Assert.That(error.ParamName, Is.EqualTo("servicesRenderedDate"));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void PreferredAndNegativeVatScopesAreRejectedWithoutHttp([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(0, true); args[2] = (bool?)true;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[0] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(0, mode == 2));
                }));
                Assert.That(error.ParamName, Is.EqualTo("vatScope"));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task PreferredDoesNotRewriteExplicitActiveOrSelectedVatScope([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(0, true); args[2] = (bool?)true; args[4] = (long?)long.MaxValue;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                await Call(Api(http), Names[0] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(0, mode == 2));
                var query = Uri.UnescapeDataString(handler.Uri.Query);
                Assert.That(query, Does.Contain("preferred=true"));
                Assert.That(query, Does.Contain("active=false"));
                Assert.That(query, Does.Contain("vatScope=9223372036854775807"));
                Assert.That(handler.Calls, Is.EqualTo(1), "Effective active filtering remains a server rule.");
            }
        }

        [Test]
        public async Task ExplicitEmptyQueriesAndUndefinedZeroVatScopeRemainObservable([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false);
                args[route == 0 ? 1 : 0] = "";
                args[args.Length - 4] = ""; args[args.Length - 3] = (long?)0;
                args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, true));
                Assert.That(handler.Uri.Query, Is.EqualTo("?fields=&country=&vatScope=0"));
            }
        }

        [Test]
        public async Task EmptyAndNullRepliesKeepExactRawText([ValueSource(nameof(Routes))] int route, [Values("", "null")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, 2, false, CancellationToken.None);
                Assert.That(Property<string>(info, "RawJsonContent"), Is.EqualTo(json));
                Assert.That(Property<JObject>(info, "Data"), Is.Null);
            }
        }

        [Test]
        public void InvalidRootsAndRecordShapesFailClearly([ValueSource(nameof(Routes))] int route,
            [Values("[]", "\"text\"", "{\"records\":{}}", "{\"records\":[null]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Invoke(Api(http), route, 2, false, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [TestCase(""), TestCase("null"), TestCase("[]")]
        public void RecordsDistinguishUnknownFromKnownEmpty(string records)
        {
            var source = records == "" ? new JObject() : JObject.Parse("{\"records\":" + records + "}");
            var response = new RevenueAccountsResponse(source);
            if (records == "[]") Assert.That(response.Records, Is.Empty); else Assert.That(response.Records, Is.Null);
        }

        [Test]
        public void TypedRecordsKeepAllDeclaredFieldsUnknownDataAndCopyIsolation()
        {
            var longs = new[] { "Id", "AccountingArea", "CreatedTs", "ModifiedTs", "AccountId", "TaxCaseId", "TaxTypeId",
                "ProductId", "ProductType", "MerchandiseGroupId", "ValidFrom", "ValidTill", "AdvanceAccountId" };
            var strings = new[] { "CreatorUid", "CreatorName", "ModifierUid", "ModifierName", "AccountNumber", "VatKey",
                "TaxCaseName", "TaxTypeDisplay", "TaxKey", "Description", "CountryCode", "CountryIso", "OriginalCountryCode",
                "OriginalCountryIso", "ProductTypeDisplay", "AdvanceAccountNumber", "AdvanceVatKey", "VatKeyDescription" };
            var booleans = new[] { "Matrix", "ReverseCharge", "Advance" };
            Assert.That(longs.Length + strings.Length + booleans.Length, Is.EqualTo(34));
            var source = new JObject { ["unknown"] = new JArray(1, null) };
            foreach (var name in longs) source[char.ToLowerInvariant(name[0]) + name.Substring(1)] = long.MaxValue;
            foreach (var name in strings) source[char.ToLowerInvariant(name[0]) + name.Substring(1)] = "0004711";
            foreach (var name in booleans) source[char.ToLowerInvariant(name[0]) + name.Substring(1)] = false;
            var responseSource = new JObject { ["records"] = new JArray(source) };
            var response = new RevenueAccountsResponse(responseSource);
            var record = response.Records[0];
            foreach (var name in longs) Assert.That(Property<long?>(record, name), Is.EqualTo(long.MaxValue));
            foreach (var name in strings) Assert.That(Property<string>(record, name), Is.EqualTo("0004711"));
            foreach (var name in booleans) Assert.That(Property<bool?>(record, name), Is.False);
            record["accountNumber"] = "changed"; ((JArray)record["unknown"])[0] = 2;
            Assert.That(response.Records[0].AccountNumber, Is.EqualTo("0004711"));
            Assert.That((int)response.Records[0]["unknown"][0], Is.EqualTo(1));
            ((JObject)responseSource["records"][0])["id"] = 0;
            Assert.That(response.Records[0].Id, Is.EqualTo(long.MaxValue));
        }

        [Test]
        public void MissingNullFieldsAndNullModelSourcesRemainExplicit()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => { new RevenueAccountRecord(null); }));
            Assert.Throws<ArgumentNullException>((Action)(() => { new RevenueAccountsResponse(null); }));
            var record = new RevenueAccountRecord(JObject.Parse("{\"id\":null,\"matrix\":null,\"accountNumber\":null}"));
            Assert.That(record.Id, Is.Null); Assert.That(record.Matrix, Is.Null);
            Assert.That(record.AccountNumber, Is.Null); Assert.That(record.CreatedTs, Is.Null);
        }

        private static Task<object> Invoke(AccountingApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            object[] tail = explicitValues ? new object[] { "DE", (long?)long.MinValue, (bool?)false, "29.02.2024" } : new object[] { null, null, null, null };
            object[] prefix = route == 0 ? new object[] { long.MaxValue, explicitValues ? "accountNumber,createdTs & custom" : null, explicitValues ? (bool?)false : null } :
                route == 1 ? new object[] { explicitValues ? "accountNumber,createdTs & custom" : null, explicitValues ? (int?)0 : null, explicitValues ? (int?)1001 : null } :
                new object[] { explicitValues ? "accountNumber,createdTs & custom" : null };
            return prefix.Concat(tail).ToArray();
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var prefix = route == 0 ? new[] { typeof(long), typeof(string), typeof(bool?) } :
                route == 1 ? new[] { typeof(string), typeof(int?), typeof(int?) } : new[] { typeof(string) };
            var types = prefix.Concat(new[] { typeof(string), typeof(long?), typeof(bool?), typeof(string) }).ToArray();
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
        private static T Property<T>(object value, string name) => (T)value.GetType().GetProperty(name).GetValue(value);
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-revenue-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body) => new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
