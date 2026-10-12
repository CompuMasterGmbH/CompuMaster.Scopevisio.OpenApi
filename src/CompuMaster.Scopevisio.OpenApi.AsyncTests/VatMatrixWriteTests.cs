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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class VatMatrixWriteTests
    {
        private const string Json = "{\"entries\":[{\"id\":9223372036854775807,\"vatRate\":123456789.123456789}],\"future\":{\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("ReplaceVatMatrixEntries")]
        [TestCase("ReplaceVatMatrixEntriesWithHttpInfo")]
        [TestCase("ReplaceVatMatrixEntriesAsyncWithHttpInfo")]
        [TestCase("UpdateVatMatrixEntries")]
        [TestCase("UpdateVatMatrixEntriesWithHttpInfo")]
        [TestCase("UpdateVatMatrixEntriesAsyncWithHttpInfo")]
        public void ExplicitMatrixWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == name), Is.True, "The explicit VAT matrix operation is absent.");
        }

        [Test]
        public async Task ExplicitOperationAndFullEntryAreSentUnchanged([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var entry = new VatMatrixEntryRequest { Id = long.MaxValue, VatRate = -123456789.123456789m, DatevKey = "001", DatevKeyReduced = "", VatKey = "future key", VatKeyDescription = "Vorsteuer 5,5%", VatCode = "future region", PurchaseAccount = "00001", SalesAccount = "00002", SalesNotDueAccount = "00003", CashDiscountAccount = "00004" };
                entry["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
                var entries = new JArray(entry);
                var info = await Invoke(Api(http), route, entries, mode, CancellationToken.None);
                Assert.That(handler.Method.Method, Is.EqualTo(route == 0 ? "POST" : "PATCH"));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/vatmatrixentries"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-vat-matrix-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(entries.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["entries"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["entries"][0].Value<decimal>("vatRate"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalEmptyAndExplicitNullFieldsStayDistinct([Values(0, 1)] int route, [Values(0, 1, 2)] int mode, [Values(0, 1, 2, 3)] int selection)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var entries = selection == 0 ? null : selection == 1 ? new JArray() : selection == 2 ? new JArray(new JObject()) : new JArray(new JObject { ["id"] = JValue.CreateNull(), ["vatRate"] = JValue.CreateNull(), ["salesNotDueAccount"] = JValue.CreateNull() });
                var info = await Invoke(Api(http), route, entries, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(entries?.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private static readonly string[] Invalid = {
            "[null]", "[1]", "[[]]", "[{\"id\":\"1\"}]", "[{\"id\":1.0}]", "[{\"id\":9223372036854775808}]",
            "[{\"vatRate\":\"19.0\"}]", "[{\"vatRate\":false}]", "[{\"datevKey\":1}]", "[{\"datevKeyReduced\":[]}]",
            "[{\"vatKey\":false}]", "[{\"vatKeyDescription\":{}}]", "[{\"vatCode\":1}]", "[{\"purchaseAccount\":1}]",
            "[{\"salesAccount\":true}]", "[{\"salesNotDueAccount\":[] }]", "[{\"cashDiscountAccount\":{}}]"
        };

        [Test]
        public void InvalidKnownKindsFailBeforeHttp([Values(0, 1)] int route, [ValueSource(nameof(Invalid))] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, JArray.Parse(json), mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void NonfiniteRatesPreventHttp([Values(0, 1)] int route, [Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double rate)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, new JArray(new JObject { ["vatRate"] = rate }), 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task OriginalIdentityAndUnboundedRateAreLeftToServer([Values(0, 1)] int route, [Values(long.MinValue, 0L, long.MaxValue)] long id)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var entries = new JArray(new VatMatrixEntryRequest { Id = id, VatRate = decimal.MaxValue });
                await Invoke(Api(http), route, entries, 2, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(entries.ToString(Formatting.None)));
            }
        }

        [Test]
        public async Task EntryCopiesAndTransportSnapshotsDoNotAliasCallerData([Values(0, 1)] int route)
        {
            var source = new JObject { ["vatKey"] = "original", ["future"] = new JObject { ["value"] = "original" } };
            var entry = new VatMatrixEntryRequest(source);
            source["future"]["value"] = "source changed";
            Assert.That(entry["future"].Value<string>("value"), Is.EqualTo("original"));
            var entries = new JArray(entry);
            var handler = new Handler(token => { entries[0]["vatKey"] = "caller changed"; entries[0]["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, entries, 2, CancellationToken.None); }
            var body = JArray.Parse(handler.Body);
            Assert.That(body[0].Value<string>("vatKey"), Is.EqualTo("original"));
            Assert.That(body[0]["future"].Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void TypedNullSettersOmitAndNullConstructorFails()
        {
            var entry = new VatMatrixEntryRequest { Id = 1, VatRate = 19m, DatevKey = "1", SalesNotDueAccount = "2" };
            entry.Id = null; entry.VatRate = null; entry.DatevKey = null; entry.SalesNotDueAccount = null;
            Assert.That(entry.Properties(), Is.Empty);
            Assert.Throws<ArgumentNullException>((Action)(() => new VatMatrixEntryRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsRetainCompleteJson([Values(0, 1)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var api = Api(http);
                JToken data;
                if (route == 0) data = mode == 0 ? api.ReplaceVatMatrixEntries() : mode == 1 ? await api.ReplaceVatMatrixEntriesAsync() : await api.ReplaceVatMatrixEntriesAsync(null, CancellationToken.None);
                else data = mode == 0 ? api.UpdateVatMatrixEntries() : mode == 1 ? await api.UpdateVatMatrixEntriesAsync() : await api.UpdateVatMatrixEntriesAsync(null, CancellationToken.None);
                Assert.That(data["entries"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }

        [Test]
        public async Task UnspecifiedResultShapesRemainExact([Values(0, 1)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), route, null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalProfileAndInputErrorsArePreservedWithoutRetry([Values(0, 1)] int route, [Values(400, 403)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original VAT diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, null, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original VAT diagnostic"));
                var expected = new InvalidOperationException("custom VAT hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(route == 0 ? "ReplaceVatMatrixEntries" : "UpdateVatMatrixEntries")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, null, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsTaxChanges([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Values(0, 1)] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, null, 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JArray entries, int mode, CancellationToken token)
        {
            if (route == 0) return mode == 0 ? Task.FromResult(api.ReplaceVatMatrixEntriesWithHttpInfo(entries)) : mode == 1 ? api.ReplaceVatMatrixEntriesAsyncWithHttpInfo(entries) : api.ReplaceVatMatrixEntriesAsyncWithHttpInfo(entries, token);
            return mode == 0 ? Task.FromResult(api.UpdateVatMatrixEntriesWithHttpInfo(entries)) : mode == 1 ? api.UpdateVatMatrixEntriesAsyncWithHttpInfo(entries) : api.UpdateVatMatrixEntriesAsyncWithHttpInfo(entries, token);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-vat-matrix-token" });
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
