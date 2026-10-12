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
    public class IncomingInvoiceUpdateTests
    {
        private static readonly string[] Strings = { "documentNumber", "orderNumber", "text1", "taxCountryIsoCode2", "buyerName", "buyerId", "buyerGTIN", "buyerDepartment", "buyerZipCode", "buyerCity", "buyerStreet", "buyerAddressExtra", "buyerCountry", "shiptoName", "shiptoId", "shiptoGtin", "shiptoDepartment", "shiptoZipCode", "shiptoCity", "shiptoStreet", "shiptoAddressExtra", "shiptoCountry", "invoiceeName", "invoiceeId", "invoiceeGtin", "invoiceeDepartment", "invoiceeZipCode", "invoiceeCity", "invoiceeStreet", "invoiceeAddressExtra", "invoiceeCountry", "paymentTermName", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins", "deliveryNumber", "dimensionLabel1", "dimensionLabel2", "dimensionLabel3", "dimensionLabel4", "dimensionLabel5", "dimensionLabel6", "dimensionLabel7", "dimensionLabel8", "dimensionLabel9", "dimensionLabel10", "deferredCostAccount", "deferredCreditAccount", "lastComment" };
        private static readonly string[] Int64Fields = { "incomingTs", "documentDate", "buyerContactId", "deliveryDateFrom", "deliveryDateTo", "buyerContactPersonId", "shiptoContactPersonId", "invoiceeContactPersonId", "vendorContactId", "discountDate1", "discountDate2", "dueDate", "postingDate", "deferredDate", "deferredSteps", "deferredPostingPlan" };
        private static readonly string[] Int32Fields = { "permissionType", "deferredPeriod", "deferredAccountSelection" };
        private static readonly string[] Numbers = { "discountPercent1", "discountPercent2", "discountAmount1", "discountAmount2" };
        private static readonly string[] AllFields = Strings.Concat(Int64Fields).Concat(Int32Fields).Concat(Numbers).Concat(new[] { "explicitDiscount" }).ToArray();
        private const string Json = "{\"future\":{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"},\"errors\":[\"inspect result\"]}";

        [TestCase("UpdateIncomingInvoice")]
        [TestCase("UpdateIncomingInvoiceWithHttpInfo")]
        [TestCase("UpdateIncomingInvoiceAsyncWithHttpInfo")]
        public void UnverifiedInvoiceUpdateWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(IncomingInvoiceApi).GetMethods().Any(method => method.Name == name), Is.True, "The invoice update workflow is absent.");
        }

        [Test]
        public async Task AllCurrentTypedPropertiesMapToOriginalWireValues([Values(0, 1, 2)] int mode)
        {
            var request = new IncomingInvoiceUpdateRequest();
            var expected = new JObject();
            foreach (var name in AllFields)
            {
                object value = Strings.Contains(name) ? (object)("0001 / original " + name) : Int64Fields.Contains(name) ? (object)long.MinValue :
                    Int32Fields.Contains(name) ? (object)1 : Numbers.Contains(name) ? (object)123456789.123456789m : false;
                var property = typeof(IncomingInvoiceUpdateRequest).GetProperty(char.ToUpperInvariant(name[0]) + name.Substring(1));
                Assert.That(property, Is.Not.Null, "The typed field " + name + " is absent.");
                property.SetValue(request, value);
                expected[name] = new JValue(value);
            }
            Assert.That(expected.Properties().Count(), Is.EqualTo(74));
            request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            expected["future"] = request["future"].DeepClone();
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), long.MaxValue, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/incominginvoice/9223372036854775807"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-invoice-update-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task BodyOmissionEmptyRequestAndExplicitNullDoNotInsertDefaults([Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int selection)
        {
            var request = selection == 0 ? null : new IncomingInvoiceUpdateRequest();
            if (selection == 2) foreach (var name in AllFields) request[name] = JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), 1L, request, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(request?.ToString(Formatting.None)));
                Assert.That(info.Data, Is.Null);
                if (selection == 1) Assert.That(handler.Body, Is.EqualTo("{}"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidKnownFieldKindsFailBeforeHttp([ValueSource(nameof(AllFields))] string name, [Values(0, 2)] int mode)
        {
            var request = new IncomingInvoiceUpdateRequest { [name] = new JObject() };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void Int64FieldsRejectOverflowAndFractionalKinds([ValueSource(nameof(Int64Fields))] string name, [Values("9223372036854775808", "1.0", "\"1\"")] string value)
        {
            var request = new IncomingInvoiceUpdateRequest(JObject.Parse("{\"" + name + "\":" + value + "}"));
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [TestCase("permissionType", -1)]
        [TestCase("permissionType", 3)]
        [TestCase("deferredPeriod", -1)]
        [TestCase("deferredPeriod", 3)]
        [TestCase("deferredAccountSelection", -1)]
        [TestCase("deferredAccountSelection", 2)]
        public void ExplicitSchemaEnumBoundsAreChecked(string name, int value)
        {
            var request = new IncomingInvoiceUpdateRequest { [name] = value };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void NonfiniteDiscountNumbersFailBeforeHttp([ValueSource(nameof(Numbers))] string name, [Values(double.NaN, double.PositiveInfinity)] double value)
        {
            var request = new IncomingInvoiceUpdateRequest { [name] = value };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, 2, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task OriginalTimestampsContactSelectionsAndPaymentChoicesAreNotRepaired([Values(long.MinValue, 0L, long.MaxValue)] long id)
        {
            var request = new IncomingInvoiceUpdateRequest { IncomingTs = id, DocumentDate = id, BuyerContactId = id, PostingDate = id, DiscountDate1 = id, DiscountDate2 = id, DueDate = id,
                DiscountPercent1 = 1.123456789m, DiscountAmount1 = -2.123456789m, ExplicitDiscount = false, PaymentTermName = "", PermissionType = 2, DeferredPeriod = 2, DeferredAccountSelection = 1 };
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), id, request, 2, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/incominginvoice/" + id.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
            }
        }

        [Test]
        public async Task CopiedRequestAndSnapshotRetainUnknownNestedData()
        {
            var source = new JObject { ["documentNumber"] = "0001", ["future"] = new JObject { ["value"] = "original" } };
            var request = new IncomingInvoiceUpdateRequest(source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request.DocumentNumber = "changed"; request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 1L, request, 2, CancellationToken.None); }
            var body = JObject.Parse(handler.Body);
            Assert.That(body.Value<string>("documentNumber"), Is.EqualTo("0001"));
            Assert.That(body["future"].Value<string>("value"), Is.EqualTo("original"));
            request.DocumentNumber = null; request.PermissionType = 1; request.PermissionType = null; request.ExplicitDiscount = false; request.ExplicitDiscount = null;
            Assert.That(request["documentNumber"], Is.Null); Assert.That(request["permissionType"], Is.Null); Assert.That(request["explicitDiscount"], Is.Null);
            Assert.Throws<ArgumentNullException>((Action)(() => new IncomingInvoiceUpdateRequest(null)));
        }

        [Test]
        public async Task ConvenienceFormsRetainCompleteResult([Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var api = Api(http);
                var data = mode == 0 ? api.UpdateIncomingInvoice(1L) : mode == 1 ? await api.UpdateIncomingInvoiceAsync(1L) : await api.UpdateIncomingInvoiceAsync(1L, null, CancellationToken.None);
                Assert.That(data["errors"][0].Value<string>(), Is.EqualTo("inspect result"));
            }
        }

        [Test]
        public async Task UndeclaredResultShapesRemainExact([Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), 1L, null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void OriginalInputAndServerErrorsRunBeforeParser([Values(400, 403, 500)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original unverified invoice diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, null, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original unverified invoice diagnostic"));
                var expected = new InvalidOperationException("custom update hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("UpdateIncomingInvoice")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, 1L, null, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsInvoiceUpdate()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), 1L, null, 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }

        private static Task<ApiResponse<JToken>> Invoke(IncomingInvoiceApi api, long id, IncomingInvoiceUpdateRequest request, int mode, CancellationToken token)
        {
            return mode == 0 ? Task.FromResult(api.UpdateIncomingInvoiceWithHttpInfo(id, request)) : mode == 1 ? api.UpdateIncomingInvoiceAsyncWithHttpInfo(id, request) : api.UpdateIncomingInvoiceAsyncWithHttpInfo(id, request, token);
        }
        private static IncomingInvoiceApi Api(HttpClient http) => new IncomingInvoiceApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-invoice-update-token" });
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
