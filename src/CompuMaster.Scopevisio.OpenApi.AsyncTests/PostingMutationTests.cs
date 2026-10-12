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
    public class PostingMutationTests
    {
        private static readonly string[] Names = { "CorrectPostingXml", "CancelPosting", "ImportPostingCorrections", "UpdatePostingDocumentNumbers" };
        private static readonly string[] Paths = { "/correctpostings", "/posting/cancel", "/postings/correction", "/postings/update" };
        private const string Json = "{\"results\":[{\"documentNumber\":\"0001\",\"error\":\"inspect partial failure\"}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\",\"id\":9223372036854775807}}";

        [Test]
        public void ExplicitWorkflowIsAvailable([Range(0, 3)] int route, [Values("", "WithHttpInfo", "AsyncWithHttpInfo")] string suffix)
        { Assert.That(typeof(AccountingApi).GetMethods().Any(method => method.Name == Names[route] + suffix), Is.True); }


        [Test]
        public async Task ExactChoicesAndCompletePartialResultsReachEveryForm([Range(0, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            var request = Valid(route);
            request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            var response = Response(HttpStatusCode.OK, Json);
            response.Headers.TryAddWithoutValidation("X-Request", "normal");
            response.Content.Headers.TryAddWithoutValidation("X-Content", "content");
            var handler = new Handler(token => Task.FromResult(response));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(route == 3 ? HttpMethod.Put : HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest" + Paths[route]));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-open-item-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.Headers["X-Request"], Is.EqualTo("normal"));
                Assert.That(info.Headers["X-Content"], Is.EqualTo("content"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Data["results"][0].Value<string>("error"), Is.EqualTo("inspect partial failure"));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }


        [Test]
        public async Task ConvenienceFormsPreservePartialFeedback([Range(0, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var args = mode == 2 ? new object[] { Valid(route), CancellationToken.None } : new object[] { Valid(route) };
                var result = (JToken)await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"), args, Types(route, mode == 2));
                Assert.That(result["results"][0].Value<string>("error"), Is.EqualTo("inspect partial failure"));
            }
        }


        [Test]
        public async Task SourceCopiesAndTransportSnapshotsRemainIndependent([Range(0, 3)] int route)
        {
            var source = Valid(route);
            source["future"] = new JObject { ["value"] = "original" };
            var request = Copy(route, source);
            source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(JObject.Parse(handler.Body)["future"].Value<string>("value"), Is.EqualTo("original"));
        }


        [Test]
        public async Task UnknownResultShapesKeepExactRawJson([Range(0, 3)] int route, [Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            { var info = await Invoke(Api(http), route, Valid(route), 2, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json)); }
        }


        [Test]
        public void OriginalErrorsAndExceptionHookRemainUnchanged([Range(0, 3)] int route, [Values(400, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original accounting error")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original accounting error"));
                var expected = new InvalidOperationException("custom open-item hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, Valid(route), 2, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }


        [Test]
        public void PreCancellationPreventsMutation([Range(0, 3)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            { cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, 2, cancellation.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }


        [Test]
        public async Task ActiveCancellationReachesHttpWithoutRetry([Range(0, 3)] int route)
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


        [Test]
        public async Task OptionalBodiesOmitWithoutSynthesizingTargets([Range(0, 3)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            { var info = await Invoke(Api(http), route, null, mode, CancellationToken.None); Assert.That(handler.Body, Is.Null); Assert.That(info.Data, Is.Null); }
        }
        [Test]
        public async Task EmptyFalseAndNullChoicesRemainOriginal([Range(0, 3)] int route)
        {
            var request = Valid(route);
            if (route == 0) request["postingXml"] = "";
            if (route == 1) { request["documentNumber"] = ""; request["cancellationDate"] = JValue.CreateNull(); }
            if (route == 2) request["rows"] = new JArray();
            if (route == 3) { request["documentNumber"] = ""; request["externalDocumentNumber"] = JValue.CreateNull(); }
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [Test]
        public async Task OmittedOptionalFieldsDoNotChooseDatesAccountsOrRows([Values(1, 2)] int route)
        {
            var request = Copy(route, new JObject());
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo("{}"));
        }
        [Test]
        public void RequiredFieldsPreventHttp([Values(0, 2, 3)] int route, [Values(false, true)] bool explicitNull, [Values(0, 2)] int mode)
        {
            var request = Valid(route); var obj = route == 2 ? (JObject)request["rows"][0] : request;
            var names = route == 0 ? new[] { "postingXml" } : route == 3 ? new[] { "documentNumber" } : new[] { "postingDate", "documentNumber", "account", "amount" };
            foreach (var name in names) { var original = obj[name]; if (explicitNull) obj[name] = JValue.CreateNull(); else obj.Remove(name); AssertInvalid(route, request, mode); obj[name] = original; }
        }
        [TestCaseSource(nameof(InvalidFields))]
        public void KnownKindsAndWidthsPreventHttp(int route, string json)
        { AssertInvalid(route, Copy(route, JObject.Parse(json)), 2); }
        private static System.Collections.IEnumerable InvalidFields()
        {
            foreach (var name in new[] { "postingXml", "importMasterDataMode", "documentNumber", "cancellationDate", "generateVat", "postingBatch", "importMasterDataEnabled", "importMasterDataAccountSearchOnly", "importFilesEnabled", "generateVatCreatorInfos", "refreshPaymentTerm", "importReferenceAttributes" })
            { var request = Valid(0); request[name] = 1; yield return new object[] { 0, request.ToString(Formatting.None) }; }
            foreach (var route in new[] { 0, 1 })
            { var request = Valid(route); request["pdeRowNumber"] = JToken.Parse("9223372036854775808"); yield return new object[] { route, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "documentNumber", "cancellationDate" })
            { var request = Valid(1); request[name] = true; yield return new object[] { 1, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "documentNumber", "externalDocumentNumber", "internalDocumentNumber" })
            { var request = Valid(3); request[name] = 1; yield return new object[] { 3, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "adjustVatKey", "autoCreateTax" })
            { var request = Valid(2); request[name] = 1; yield return new object[] { 2, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "documentNumber", "externalDocumentNumber", "account", "summaryAccount", "cancelDocument", "foreignCurrencyCode", "documentText", "rowText", "vatKey", "discountAccount", "paymentType", "internalDocumentNumber", "ustId", "amount", "foreignCurrencyAmount", "foreignCurrencyRate", "discountPercent1", "discountPercent2" })
            { var request = Valid(2); request["rows"][0][name] = true; yield return new object[] { 2, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "postingDate", "documentDate", "discountPeriod1", "discountPeriod2", "netTimeLimit" })
            { var request = Valid(2); request["rows"][0][name] = JToken.Parse("9223372036854775808"); yield return new object[] { 2, request.ToString(Formatting.None) }; }
            foreach (var name in new[] { "dimensionId", "dimensionAccountNumber" })
            { var request = Valid(2); request["rows"][0]["dimensions"][0][name] = 1; yield return new object[] { 2, request.ToString(Formatting.None) }; }
            foreach (var raw in new[] { "{\"rows\":{}}", "{\"rows\":[null]}" }) yield return new object[] { 2, raw };
            foreach (var raw in new[] { "{}", "[null]" })
            { var request = Valid(2); request["rows"][0]["dimensions"] = JToken.Parse(raw); yield return new object[] { 2, request.ToString(Formatting.None) }; }
        }
        [Test]
        public void NonfiniteAmountsPreventHttp([Values("amount", "foreignCurrencyAmount", "foreignCurrencyRate", "discountPercent1", "discountPercent2")] string field, [Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double value)
        { var request = Valid(2); ((JObject)request["rows"][0]).Remove(field); request["rows"][0][field] = value; AssertInvalid(2, request, 2); }
        [Test]
        public async Task ZeroNegativeAndTinyFiniteAmountsStayExact([Values(0.0, -1.0, 1e-100)] double value)
        {
            var request = Valid(2); request["rows"][0]["amount"] = value;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 2, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        private static void AssertInvalid(int route, JObject request, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        [Test]
        public void TypedPropertiesMapEveryDeclaredFieldAndNullOmission()
        {
            {
                var model = new PostingCorrectionXmlRequest();
                model.GenerateVat = false; Assert.That(model["generateVat"], Is.Not.Null); Assert.That(model.GenerateVat, Is.Not.Null); model.GenerateVat = null; Assert.That(model["generateVat"], Is.Null);
                model.PostingBatch = false; Assert.That(model["postingBatch"], Is.Not.Null); Assert.That(model.PostingBatch, Is.Not.Null); model.PostingBatch = null; Assert.That(model["postingBatch"], Is.Null);
                model.PostingXml = "original"; Assert.That(model["postingXml"], Is.Not.Null); Assert.That(model.PostingXml, Is.Not.Null); model.PostingXml = null; Assert.That(model["postingXml"], Is.Null);
                model.ImportMasterDataEnabled = false; Assert.That(model["importMasterDataEnabled"], Is.Not.Null); Assert.That(model.ImportMasterDataEnabled, Is.Not.Null); model.ImportMasterDataEnabled = null; Assert.That(model["importMasterDataEnabled"], Is.Null);
                model.ImportMasterDataMode = "original"; Assert.That(model["importMasterDataMode"], Is.Not.Null); Assert.That(model.ImportMasterDataMode, Is.Not.Null); model.ImportMasterDataMode = null; Assert.That(model["importMasterDataMode"], Is.Null);
                model.ImportMasterDataAccountSearchOnly = false; Assert.That(model["importMasterDataAccountSearchOnly"], Is.Not.Null); Assert.That(model.ImportMasterDataAccountSearchOnly, Is.Not.Null); model.ImportMasterDataAccountSearchOnly = null; Assert.That(model["importMasterDataAccountSearchOnly"], Is.Null);
                model.ImportFilesEnabled = false; Assert.That(model["importFilesEnabled"], Is.Not.Null); Assert.That(model.ImportFilesEnabled, Is.Not.Null); model.ImportFilesEnabled = null; Assert.That(model["importFilesEnabled"], Is.Null);
                model.GenerateVatCreatorInfos = false; Assert.That(model["generateVatCreatorInfos"], Is.Not.Null); Assert.That(model.GenerateVatCreatorInfos, Is.Not.Null); model.GenerateVatCreatorInfos = null; Assert.That(model["generateVatCreatorInfos"], Is.Null);
                model.RefreshPaymentTerm = false; Assert.That(model["refreshPaymentTerm"], Is.Not.Null); Assert.That(model.RefreshPaymentTerm, Is.Not.Null); model.RefreshPaymentTerm = null; Assert.That(model["refreshPaymentTerm"], Is.Null);
                model.ImportReferenceAttributes = false; Assert.That(model["importReferenceAttributes"], Is.Not.Null); Assert.That(model.ImportReferenceAttributes, Is.Not.Null); model.ImportReferenceAttributes = null; Assert.That(model["importReferenceAttributes"], Is.Null);
                model.DocumentNumber = "original"; Assert.That(model["documentNumber"], Is.Not.Null); Assert.That(model.DocumentNumber, Is.Not.Null); model.DocumentNumber = null; Assert.That(model["documentNumber"], Is.Null);
                model.PdeRowNumber = long.MaxValue; Assert.That(model["pdeRowNumber"], Is.Not.Null); Assert.That(model.PdeRowNumber, Is.Not.Null); model.PdeRowNumber = null; Assert.That(model["pdeRowNumber"], Is.Null);
                model.CancellationDate = "original"; Assert.That(model["cancellationDate"], Is.Not.Null); Assert.That(model.CancellationDate, Is.Not.Null); model.CancellationDate = null; Assert.That(model["cancellationDate"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingCorrectionXmlRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
            {
                var model = new PostingCancellationRequest();
                model.DocumentNumber = "original"; Assert.That(model["documentNumber"], Is.Not.Null); Assert.That(model.DocumentNumber, Is.Not.Null); model.DocumentNumber = null; Assert.That(model["documentNumber"], Is.Null);
                model.PdeRowNumber = long.MaxValue; Assert.That(model["pdeRowNumber"], Is.Not.Null); Assert.That(model.PdeRowNumber, Is.Not.Null); model.PdeRowNumber = null; Assert.That(model["pdeRowNumber"], Is.Null);
                model.CancellationDate = "original"; Assert.That(model["cancellationDate"], Is.Not.Null); Assert.That(model.CancellationDate, Is.Not.Null); model.CancellationDate = null; Assert.That(model["cancellationDate"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingCancellationRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
            {
                var model = new PostingDocumentNumbersUpdateRequest();
                model.DocumentNumber = "original"; Assert.That(model["documentNumber"], Is.Not.Null); Assert.That(model.DocumentNumber, Is.Not.Null); model.DocumentNumber = null; Assert.That(model["documentNumber"], Is.Null);
                model.ExternalDocumentNumber = "original"; Assert.That(model["externalDocumentNumber"], Is.Not.Null); Assert.That(model.ExternalDocumentNumber, Is.Not.Null); model.ExternalDocumentNumber = null; Assert.That(model["externalDocumentNumber"], Is.Null);
                model.InternalDocumentNumber = "original"; Assert.That(model["internalDocumentNumber"], Is.Not.Null); Assert.That(model.InternalDocumentNumber, Is.Not.Null); model.InternalDocumentNumber = null; Assert.That(model["internalDocumentNumber"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingDocumentNumbersUpdateRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
            {
                var model = new PostingCorrectionImportRequest();
                model.AdjustVatKey = false; Assert.That(model["adjustVatKey"], Is.Not.Null); Assert.That(model.AdjustVatKey, Is.Not.Null); model.AdjustVatKey = null; Assert.That(model["adjustVatKey"], Is.Null);
                model.AutoCreateTax = false; Assert.That(model["autoCreateTax"], Is.Not.Null); Assert.That(model.AutoCreateTax, Is.Not.Null); model.AutoCreateTax = null; Assert.That(model["autoCreateTax"], Is.Null);
                model.Rows = new JArray(new JObject { ["future"] = true }); Assert.That(model["rows"], Is.Not.Null); Assert.That(model.Rows, Is.Not.Null); model.Rows = null; Assert.That(model["rows"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingCorrectionImportRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
            {
                var model = new PostingCorrectionRowRequest();
                model.PostingDate = long.MaxValue; Assert.That(model["postingDate"], Is.Not.Null); Assert.That(model.PostingDate, Is.Not.Null); model.PostingDate = null; Assert.That(model["postingDate"], Is.Null);
                model.DocumentDate = long.MaxValue; Assert.That(model["documentDate"], Is.Not.Null); Assert.That(model.DocumentDate, Is.Not.Null); model.DocumentDate = null; Assert.That(model["documentDate"], Is.Null);
                model.DocumentNumber = "original"; Assert.That(model["documentNumber"], Is.Not.Null); Assert.That(model.DocumentNumber, Is.Not.Null); model.DocumentNumber = null; Assert.That(model["documentNumber"], Is.Null);
                model.ExternalDocumentNumber = "original"; Assert.That(model["externalDocumentNumber"], Is.Not.Null); Assert.That(model.ExternalDocumentNumber, Is.Not.Null); model.ExternalDocumentNumber = null; Assert.That(model["externalDocumentNumber"], Is.Null);
                model.Account = "original"; Assert.That(model["account"], Is.Not.Null); Assert.That(model.Account, Is.Not.Null); model.Account = null; Assert.That(model["account"], Is.Null);
                model.SummaryAccount = "original"; Assert.That(model["summaryAccount"], Is.Not.Null); Assert.That(model.SummaryAccount, Is.Not.Null); model.SummaryAccount = null; Assert.That(model["summaryAccount"], Is.Null);
                model.Amount = 123456789.123456789m; Assert.That(model["amount"], Is.Not.Null); Assert.That(model.Amount, Is.Not.Null); model.Amount = null; Assert.That(model["amount"], Is.Null);
                model.CancelDocument = "original"; Assert.That(model["cancelDocument"], Is.Not.Null); Assert.That(model.CancelDocument, Is.Not.Null); model.CancelDocument = null; Assert.That(model["cancelDocument"], Is.Null);
                model.ForeignCurrencyAmount = 123456789.123456789m; Assert.That(model["foreignCurrencyAmount"], Is.Not.Null); Assert.That(model.ForeignCurrencyAmount, Is.Not.Null); model.ForeignCurrencyAmount = null; Assert.That(model["foreignCurrencyAmount"], Is.Null);
                model.ForeignCurrencyCode = "original"; Assert.That(model["foreignCurrencyCode"], Is.Not.Null); Assert.That(model.ForeignCurrencyCode, Is.Not.Null); model.ForeignCurrencyCode = null; Assert.That(model["foreignCurrencyCode"], Is.Null);
                model.ForeignCurrencyRate = 123456789.123456789m; Assert.That(model["foreignCurrencyRate"], Is.Not.Null); Assert.That(model.ForeignCurrencyRate, Is.Not.Null); model.ForeignCurrencyRate = null; Assert.That(model["foreignCurrencyRate"], Is.Null);
                model.DocumentText = "original"; Assert.That(model["documentText"], Is.Not.Null); Assert.That(model.DocumentText, Is.Not.Null); model.DocumentText = null; Assert.That(model["documentText"], Is.Null);
                model.RowText = "original"; Assert.That(model["rowText"], Is.Not.Null); Assert.That(model.RowText, Is.Not.Null); model.RowText = null; Assert.That(model["rowText"], Is.Null);
                model.VatKey = "original"; Assert.That(model["vatKey"], Is.Not.Null); Assert.That(model.VatKey, Is.Not.Null); model.VatKey = null; Assert.That(model["vatKey"], Is.Null);
                model.Dimensions = new JArray(new JObject { ["future"] = true }); Assert.That(model["dimensions"], Is.Not.Null); Assert.That(model.Dimensions, Is.Not.Null); model.Dimensions = null; Assert.That(model["dimensions"], Is.Null);
                model.DiscountPercent1 = 123456789.123456789m; Assert.That(model["discountPercent1"], Is.Not.Null); Assert.That(model.DiscountPercent1, Is.Not.Null); model.DiscountPercent1 = null; Assert.That(model["discountPercent1"], Is.Null);
                model.DiscountPeriod1 = long.MaxValue; Assert.That(model["discountPeriod1"], Is.Not.Null); Assert.That(model.DiscountPeriod1, Is.Not.Null); model.DiscountPeriod1 = null; Assert.That(model["discountPeriod1"], Is.Null);
                model.DiscountPercent2 = 123456789.123456789m; Assert.That(model["discountPercent2"], Is.Not.Null); Assert.That(model.DiscountPercent2, Is.Not.Null); model.DiscountPercent2 = null; Assert.That(model["discountPercent2"], Is.Null);
                model.DiscountPeriod2 = long.MaxValue; Assert.That(model["discountPeriod2"], Is.Not.Null); Assert.That(model.DiscountPeriod2, Is.Not.Null); model.DiscountPeriod2 = null; Assert.That(model["discountPeriod2"], Is.Null);
                model.NetTimeLimit = long.MaxValue; Assert.That(model["netTimeLimit"], Is.Not.Null); Assert.That(model.NetTimeLimit, Is.Not.Null); model.NetTimeLimit = null; Assert.That(model["netTimeLimit"], Is.Null);
                model.DiscountAccount = "original"; Assert.That(model["discountAccount"], Is.Not.Null); Assert.That(model.DiscountAccount, Is.Not.Null); model.DiscountAccount = null; Assert.That(model["discountAccount"], Is.Null);
                model.PaymentType = "original"; Assert.That(model["paymentType"], Is.Not.Null); Assert.That(model.PaymentType, Is.Not.Null); model.PaymentType = null; Assert.That(model["paymentType"], Is.Null);
                model.InternalDocumentNumber = "original"; Assert.That(model["internalDocumentNumber"], Is.Not.Null); Assert.That(model.InternalDocumentNumber, Is.Not.Null); model.InternalDocumentNumber = null; Assert.That(model["internalDocumentNumber"], Is.Null);
                model.UstId = "original"; Assert.That(model["ustId"], Is.Not.Null); Assert.That(model.UstId, Is.Not.Null); model.UstId = null; Assert.That(model["ustId"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingCorrectionRowRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
            {
                var model = new PostingCorrectionDimensionRequest();
                model.DimensionId = "original"; Assert.That(model["dimensionId"], Is.Not.Null); Assert.That(model.DimensionId, Is.Not.Null); model.DimensionId = null; Assert.That(model["dimensionId"], Is.Null);
                model.DimensionAccountNumber = "original"; Assert.That(model["dimensionAccountNumber"], Is.Not.Null); Assert.That(model.DimensionAccountNumber, Is.Not.Null); model.DimensionAccountNumber = null; Assert.That(model["dimensionAccountNumber"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); var error = Assert.Throws<TargetInvocationException>((Action)(() => Activator.CreateInstance(typeof(PostingCorrectionDimensionRequest), new object[] { null }))); Assert.That(error.InnerException, Is.TypeOf<ArgumentNullException>());
            }
        }
        [Test]
        public void RowsAndDimensionsAreDefensiveCopies()
        {
            var dimensions = new JArray(new PostingCorrectionDimensionRequest { DimensionId = "dimension_3", DimensionAccountNumber = "000123" });
            var row = new PostingCorrectionRowRequest { Dimensions = dimensions }; dimensions[0]["dimensionId"] = "source changed";
            var copy = row.Dimensions; copy[0]["dimensionId"] = "getter changed";
            Assert.That(row.Dimensions[0].Value<string>("dimensionId"), Is.EqualTo("dimension_3"));
            var rows = new JArray(row); var request = new PostingCorrectionImportRequest { Rows = rows }; rows.Clear(); var rowCopy = request.Rows; rowCopy[0]["dimensions"].Replace(new JArray());
            Assert.That(request.Rows[0]["dimensions"][0].Value<string>("dimensionAccountNumber"), Is.EqualTo("000123"));
        }
        private static JObject Copy(int route, JObject source)
        {
            if (route == 0) return new PostingCorrectionXmlRequest(source);
            if (route == 1) return new PostingCancellationRequest(source);
            if (route == 2) return new PostingCorrectionImportRequest(source);
            return new PostingDocumentNumbersUpdateRequest(source);
        }
        private static JObject Valid(int route)
        {
            if (route == 0) return new PostingCorrectionXmlRequest { PostingXml = "<original invalid xml & tail", GenerateVat = false, PostingBatch = false, ImportMasterDataEnabled = false, ImportMasterDataMode = "CREATEONLY", ImportMasterDataAccountSearchOnly = false, ImportFilesEnabled = false, GenerateVatCreatorInfos = false, RefreshPaymentTerm = false, ImportReferenceAttributes = false, DocumentNumber = "0001/A", PdeRowNumber = long.MaxValue, CancellationDate = "31.12.2026" };
            if (route == 1) return new PostingCancellationRequest { DocumentNumber = "0001/A", PdeRowNumber = long.MinValue, CancellationDate = "31.12.2026" };
            if (route == 3) return new PostingDocumentNumbersUpdateRequest { DocumentNumber = "0001/A", ExternalDocumentNumber = "0002/X", InternalDocumentNumber = "0003/Y" };
            var row = new PostingCorrectionRowRequest { PostingDate = long.MinValue, DocumentDate = long.MaxValue, DocumentNumber = "0001/A", ExternalDocumentNumber = "0002", Account = "00001A", SummaryAccount = "0001600", Amount = -123456789.123456789m, CancelDocument = "0004", ForeignCurrencyAmount = 0m, ForeignCurrencyCode = "future", ForeignCurrencyRate = 1.123456789m, DocumentText = "", RowText = "", VatKey = "future", Dimensions = new JArray(new PostingCorrectionDimensionRequest { DimensionId = "dimension_3", DimensionAccountNumber = "000123" }), DiscountPercent1 = 4.5m, DiscountPeriod1 = long.MaxValue, DiscountPercent2 = 2.5m, DiscountPeriod2 = long.MinValue, NetTimeLimit = 0, DiscountAccount = "0003736", PaymentType = "future", InternalDocumentNumber = "0005", UstId = "original" };
            return new PostingCorrectionImportRequest { AdjustVatKey = false, AutoCreateTax = false, Rows = new JArray(row, row.DeepClone()) };
        }
        private static Type[] Types(int route, bool cancellation)
        { var type = route == 0 ? typeof(PostingCorrectionXmlRequest) : route == 1 ? typeof(PostingCancellationRequest) : route == 2 ? typeof(PostingCorrectionImportRequest) : typeof(PostingDocumentNumbersUpdateRequest); return cancellation ? new[] { type, typeof(CancellationToken) } : new[] { type }; }
        private static async Task<ApiResponse<JToken>> Invoke(AccountingApi api, int route, JObject request, int mode, CancellationToken token)
        { var args = mode == 2 ? new object[] { request, token } : new object[] { request }; return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2)); }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null); object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static AccountingApi Api(HttpClient http) => new AccountingApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-open-item-token" });
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
