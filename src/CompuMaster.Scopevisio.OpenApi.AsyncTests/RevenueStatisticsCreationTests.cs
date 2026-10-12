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
    public class RevenueStatisticsCreationTests
    {
        private static readonly string[] Names = { "CreateProductRevenueAccounts", "CreateStatisticsPostings" };
        private const string Json = "{\"status\":200,\"results\":[{\"id\":9223372036854775807}],\"feedback\":[{\"message\":\"inspect warning\",\"type\":\"FUTURE\",\"locationIdentifier\":1}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("CreateProductRevenueAccounts")]
        [TestCase("CreateProductRevenueAccountsWithHttpInfo")]
        [TestCase("CreateProductRevenueAccountsAsyncWithHttpInfo")]
        [TestCase("CreateStatisticsPostings")]
        [TestCase("CreateStatisticsPostingsWithHttpInfo")]
        [TestCase("CreateStatisticsPostingsAsyncWithHttpInfo")]
        public void ExplicitCreationWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == name), Is.True, "The explicit creation workflow is absent.");
        }

        [Test]
        public async Task FullOriginalCreationChoicesAndFeedbackStayExact([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var request = Valid(route);
            request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            var status = route == 1 && mode > 0 ? HttpStatusCode.Created : HttpStatusCode.OK;
            var handler = new Handler(token => Task.FromResult(Response(status, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(route == 0 ? "/rest/revenueaccounts/new" : "/rest/statisticspostings/new"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-revenue-statistics-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo((int)status));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["results"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["feedback"][0].Value<string>("type"), Is.EqualTo("FUTURE"));
                Assert.That(info.Data["feedback"][0]["locationIdentifier"].Type, Is.EqualTo(JTokenType.Integer));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task RequiredBodiesHaveNoInventedRootMinimum([Values(0, 1)] int route, [Values(0, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            var request = Empty(route);
            if (selection == 1) request[Key(route)] = new JArray();
            if (selection == 2) request[Key(route)] = JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void MissingRequiredBodiesPreventHttp([Values(0, 1)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task DefaultAccountStringsAndConflictingDateFormatsStayOriginal([Values("true", "false", "inherit", "INHERIT", "future")] string choice, [Values("2026-01-15", "15.01.2026", "")] string date)
        {
            var request = new ProductRevenueAccountsCreationRequest { Entries = new JArray(new ProductRevenueAccountEntryRequest {
                ProductNumber = "0001", UseDefaultAccounts = choice, Accounts = new JArray(new ProductRevenueAccountAssignmentRequest { AccountNumber = "0008400", TaxCase = "unknown", TaxCaseId = long.MinValue, CountryIsoCode = " future ", ValidFrom = date, ValidTo = " earlier date " }) }) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 0, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        [Test]
        public async Task AccountCreationIsNeverEnabledAutomatically([Values(0, 1, 2)] int selection)
        {
            var request = new StatisticsPostingsCreationRequest();
            if (selection == 1) request.AutoCreateAccount = false;
            if (selection == 2) request.AutoCreateAccount = true;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 1, request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            if (selection == 0) Assert.That(body["autoCreateAccount"], Is.Null);
            else Assert.That(body.Value<bool>("autoCreateAccount"), Is.EqualTo(selection == 2));
        }

        [Test]
        public async Task StatisticsOriginalDimensionsTimestampAndNonzeroAmountStayExact([Values(long.MinValue, 0L, long.MaxValue)] long id, [Values(-1, 1)] int sign)
        {
            var posting = new StatisticsPostingEntryRequest { Date = id, AccountNumber = "0001A", Amount = sign * 123456789.123456789m, Text = "", ZoneNumber = " unknown " };
            for (var i = 1; i <= 10; i++) typeof(StatisticsPostingEntryRequest).GetProperty("Dimension" + i).SetValue(posting, id);
            var request = new StatisticsPostingsCreationRequest { AutoCreateAccount = false, AutoAccountDecimal = id, StatisticsPostings = new JArray(posting) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler)) { var info = await Invoke(Api(http), 1, request, 2, CancellationToken.None); Assert.That(info.StatusCode, Is.EqualTo(201)); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        private static readonly string[] InvalidRevenue = { "{\"entries\":{}}", "{\"entries\":[null]}", "{\"entries\":[{}]}", "{\"entries\":[{\"productNumber\":null}]}", "{\"entries\":[{\"productNumber\":1}]}", "{\"entries\":[{\"productNumber\":\"1\",\"useDefaultAccounts\":true}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":{}}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":[null]}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":[{}]}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":[{\"accountNumber\":1}]}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":[{\"accountNumber\":\"1\",\"taxCaseId\":9223372036854775808}]}]}", "{\"entries\":[{\"productNumber\":\"1\",\"accounts\":[{\"accountNumber\":\"1\",\"validFrom\":1}]}]}" };
        private static readonly string[] InvalidStatistics = { "{\"autoCreateAccount\":1}", "{\"autoAccountDecimal\":\"2\"}", "{\"statisticsPostings\":{}}", "{\"statisticsPostings\":[null]}", "{\"statisticsPostings\":[{}]}", "{\"statisticsPostings\":[{\"date\":1,\"accountNumber\":\"1\",\"amount\":0}]}", "{\"statisticsPostings\":[{\"date\":null,\"accountNumber\":\"1\",\"amount\":1}]}", "{\"statisticsPostings\":[{\"date\":1.0,\"accountNumber\":\"1\",\"amount\":1}]}", "{\"statisticsPostings\":[{\"date\":9223372036854775808,\"accountNumber\":\"1\",\"amount\":1}]}", "{\"statisticsPostings\":[{\"date\":1,\"accountNumber\":null,\"amount\":1}]}", "{\"statisticsPostings\":[{\"date\":1,\"accountNumber\":\"1\",\"amount\":\"1\"}]}", "{\"statisticsPostings\":[{\"date\":1,\"accountNumber\":\"1\",\"amount\":1,\"text\":[]}]}", "{\"statisticsPostings\":[{\"date\":1,\"accountNumber\":\"1\",\"amount\":1,\"zoneNumber\":1}]}" };

        [Test]
        public void InvalidProductAccountsPreventHttp([ValueSource(nameof(InvalidRevenue))] string json, [Values(0, 2)] int mode)
        { AssertInvalid(0, new ProductRevenueAccountsCreationRequest(JObject.Parse(json)), mode); }
        [Test]
        public void InvalidStatisticsPreventHttp([ValueSource(nameof(InvalidStatistics))] string json, [Values(0, 2)] int mode)
        { AssertInvalid(1, new StatisticsPostingsCreationRequest(JObject.Parse(json)), mode); }

        [Test]
        public void AllDimensionWidthsAreValidated([Range(1, 10)] int dimension)
        {
            var row = new StatisticsPostingEntryRequest { Date = 1L, AccountNumber = "0001", Amount = 1m };
            row["dimension_" + dimension] = JToken.Parse("9223372036854775808");
            AssertInvalid(1, new StatisticsPostingsCreationRequest { StatisticsPostings = new JArray(row) }, 2);
        }

        [Test]
        public void ZeroAndNonfiniteStatisticsAmountsPreventHttp([Values("0", "0.0", "-0.0", "0e100", "-0e-100")] string number)
        {
            var row = JObject.Parse("{\"date\":1,\"accountNumber\":\"0001\",\"amount\":" + number + "}");
            AssertInvalid(1, new StatisticsPostingsCreationRequest { StatisticsPostings = new JArray(row) }, 2);
        }

        [Test]
        public void NonfiniteAmountsPreventHttp([Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double amount)
        {
            var row = new JObject { ["date"] = 1L, ["accountNumber"] = "0001", ["amount"] = amount };
            AssertInvalid(1, new StatisticsPostingsCreationRequest { StatisticsPostings = new JArray(row) }, 2);
        }

        [Test]
        public async Task TinyFiniteNonzeroAmountIsNotRejectedByDecimalUnderflow()
        {
            var row = new JObject { ["date"] = 1L, ["accountNumber"] = "0001", ["amount"] = new JValue(1e-100) };
            var request = new StatisticsPostingsCreationRequest { StatisticsPostings = new JArray(row) };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 1, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
            Assert.That(JObject.Parse(handler.Body)["statisticsPostings"][0].Value<double>("amount"), Is.EqualTo(1e-100));
        }

        private static void AssertInvalid(int route, JObject request, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task DeepModelsArrayCopiesAndTransportSnapshotsDoNotAliasSources([Values(0, 1)] int route)
        {
            var source = Valid(route);
            source["future"] = new JObject { ["value"] = "original" };
            var request = route == 0 ? (JObject)new ProductRevenueAccountsCreationRequest(source) : new StatisticsPostingsCreationRequest(source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(JObject.Parse(handler.Body)["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void EntryArraySettersGettersAndNullOmissionsAreDefensive()
        {
            var rows = new JArray(new ProductRevenueAccountEntryRequest { ProductNumber = "0001" });
            var request = new ProductRevenueAccountsCreationRequest { Entries = rows };
            rows[0]["productNumber"] = "source changed"; var copy = request.Entries; copy[0]["productNumber"] = "getter changed";
            Assert.That(request.Entries[0].Value<string>("productNumber"), Is.EqualTo("0001"));
            request.Entries = null; Assert.That(request.Properties(), Is.Empty);
            var statistics = new StatisticsPostingsCreationRequest { AutoCreateAccount = false, AutoAccountDecimal = 2, StatisticsPostings = new JArray() };
            statistics.AutoCreateAccount = null; statistics.AutoAccountDecimal = null; statistics.StatisticsPostings = null; Assert.That(statistics.Properties(), Is.Empty);
            Assert.Throws<ArgumentNullException>((Action)(() => new ProductRevenueAccountsCreationRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new ProductRevenueAccountEntryRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new ProductRevenueAccountAssignmentRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new StatisticsPostingsCreationRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new StatisticsPostingEntryRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsExposeCompleteFeedback([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = mode == 2 ? new object[] { Valid(route), CancellationToken.None } : new object[] { Valid(route) };
                var data = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(data["feedback"][0].Value<string>("message"), Is.EqualTo("inspect warning"));
            }
        }

        [Test]
        public async Task UnspecifiedFutureResultsRemainExact([Values(0, 1)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), route, Empty(route), 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalParseProfileAndPersistenceErrorsArePreserved([Values(0, 1)] int route, [Values(400, 403, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original creation error")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original creation error"));
                var expected = new InvalidOperationException("custom revenue/statistics hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsCreation([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            { cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Values(0, 1)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, Valid(route), 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static string Key(int route) => route == 0 ? "entries" : "statisticsPostings";
        private static JObject Empty(int route) => route == 0 ? (JObject)new ProductRevenueAccountsCreationRequest() : new StatisticsPostingsCreationRequest();
        private static JObject Valid(int route)
        {
            if (route == 0) return new ProductRevenueAccountsCreationRequest { Entries = new JArray(new ProductRevenueAccountEntryRequest { ProductNumber = "0001", UseDefaultAccounts = "inherit", Accounts = new JArray(new ProductRevenueAccountAssignmentRequest { AccountNumber = "0008400", TaxCase = "future", TaxCaseId = long.MaxValue, CountryIsoCode = "DE", ValidFrom = "2026-01-15", ValidTo = "31.12.2026" }) }) };
            var row = new StatisticsPostingEntryRequest { Date = long.MaxValue, AccountNumber = "0001A", Amount = -123456789.123456789m, Text = "", ZoneNumber = "future" };
            for (var i = 1; i <= 10; i++) typeof(StatisticsPostingEntryRequest).GetProperty("Dimension" + i).SetValue(row, long.MaxValue);
            return new StatisticsPostingsCreationRequest { AutoCreateAccount = false, AutoAccountDecimal = long.MaxValue, StatisticsPostings = new JArray(row) };
        }
        private static Type[] Types(int route, bool cancellation) => cancellation ? new[] { route == 0 ? typeof(ProductRevenueAccountsCreationRequest) : typeof(StatisticsPostingsCreationRequest), typeof(CancellationToken) } : new[] { route == 0 ? typeof(ProductRevenueAccountsCreationRequest) : typeof(StatisticsPostingsCreationRequest) };
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JObject request, int mode, CancellationToken token)
        {
            var args = mode == 2 ? new object[] { request, token } : new object[] { request };
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null); object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-revenue-statistics-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body) => new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Authorization, Accept;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            { Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token); }
        }
    }
}
