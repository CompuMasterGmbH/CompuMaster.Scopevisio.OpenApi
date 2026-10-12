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
    public class MaterialMutationTests
    {
        private static readonly string[] Names = { "CreditMaterialInventory", "CreateMaterialPurchaseDelivery", "PostMaterialPurchaseDelivery", "CreateMaterialPurchaseOrder" };
        private static readonly string[] Paths = { "/material/posting/credit", "/material/purchasedelivery/new", "/material/purchasedelivery/0001%2FA/post", "/material/purchaseorder/new" };
        private const string Json = "{\"results\":[{\"documentNumber\":\"0001\",\"error\":\"inspect partial failure\"}],\"future\":{\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\",\"id\":9223372036854775807}}";

        [Test]
        public void ExplicitWorkflowIsAvailable([Range(0, 3)] int route, [Values("", "WithHttpInfo", "AsyncWithHttpInfo")] string suffix)
        { Assert.That(typeof(MaterialManagementApi).GetMethods().Any(method => method.Name == Names[route] + suffix), Is.True); }



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
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
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
                var args = Arguments(route, Valid(route), mode, CancellationToken.None);
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
        public void FacadeSharesConfigurationAndFactoryRemainsReplaceable()
        {
            var config = new Configuration(); var facade = new OpenScopeApiClient(config);
            Assert.That(facade.MaterialManagementApi.Configuration, Is.SameAs(config));
            var replacement = new MaterialManagementApi(config); facade.MaterialManagementApi = replacement;
            Assert.That(facade.MaterialManagementApi, Is.SameAs(replacement));
            replacement.ExceptionFactory = null; Assert.That(replacement.ExceptionFactory, Is.Null);
            ExceptionFactory first = (name, response) => null; replacement.ExceptionFactory = first + first;
            Assert.Throws<InvalidOperationException>((Action)(() => { var factory = replacement.ExceptionFactory; }));
        }
        [Test]
        public async Task OptionalBodiesOmitWithoutProductWarehouseOrDateDefaults([Values(0, 2)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler))
            { var info = await Invoke(Api(http), route, null, mode, CancellationToken.None); Assert.That(handler.Body, Is.Null); Assert.That(info.Data, Is.Null); }
        }
        [Test]
        public void RequiredCreationBodiesPreventHttp([Values(1, 3)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), route, null, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        [Test]
        public async Task PurchaseDeliverySelectionIsEscapedWithoutParsing([Values("0001", " name with spaces ", "A/B", "№?#%", "%2F")] string selection)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Api(http).PostMaterialPurchaseDeliveryAsync(selection, null, CancellationToken.None); }
            Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/material/purchasedelivery/" + Uri.EscapeDataString(selection) + "/post"));
            Assert.That(handler.Body, Is.Null); Assert.That(handler.Uri.Query, Is.Empty);
        }
        [Test]
        public void InvalidDeliverySelectionsPreventHttp([Values(null, "", " ", ".", "..")] string selection)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Api(http).PostMaterialPurchaseDeliveryAsync(selection, null, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        [Test]
        public async Task CreditIdentityAlternativesPreserveDocumentedPrecedence([Values(0, 1, 2)] int choice)
        {
            var request = Valid(0); var row = (JObject)request["materialPostingRows"][0];
            if (choice == 0) row.Remove("productNumber"); if (choice == 1) row.Remove("productId");
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 0, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [Test]
        public void CreditStillRequiresExplicitProductAndQuantity([Values(0, 1)] int missing)
        {
            var request = Valid(0); var row = (JObject)request["materialPostingRows"][0];
            if (missing == 0) { row.Remove("productId"); row.Remove("productNumber"); } else row.Remove("quantity");
            AssertInvalid(0, request);
        }
        [Test]
        public void RequiredPurchaseFieldsPreventHttp([Values(1, 2, 3)] int route, [Values(false, true)] bool explicitNull)
        {
            var request = Valid(route);
            var names = route == 1 ? new[] { "vendor", "purchaseDeliveryDate", "materialPurchaseDeliveryPositions" } : route == 2 ? new[] { "postingDate" } : new[] { "vendor", "purchaseOrderDate", "materialPurchaseOrderPositions" };
            foreach (var name in names) { var original = request[name]; if (explicitNull) request[name] = JValue.CreateNull(); else request.Remove(name); AssertInvalid(route, request); request[name] = original; }
        }
        [Test]
        public void RequiredPurchasePositionFieldsPreventHttp([Values(1, 3)] int route)
        {
            var request = Valid(route); var row = (JObject)request[route == 1 ? "materialPurchaseDeliveryPositions" : "materialPurchaseOrderPositions"][0];
            foreach (var name in new[] { "product", "quantity", "price" }) { var original = row[name]; row.Remove(name); AssertInvalid(route, request); row[name] = original; }
        }
        [Test]
        public async Task ExplicitEmptyArraysAndOriginalDateConflictAreNotReconstructed([Values(0, 1, 2, 3)] int route)
        {
            var request = Valid(route);
            if (route == 0) request["materialPostingRows"] = new JArray();
            if (route == 1) { request["materialPurchaseDeliveryPositions"] = new JArray(); request["purchaseDeliveryDate"] = "31.12.2026"; }
            if (route == 2) request["postingDate"] = "future date";
            if (route == 3) { request["materialPurchaseOrderPositions"] = new JArray(); request["purchaseOrderDate"] = "31.12.2026"; }
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), route, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [TestCaseSource(nameof(InvalidKnownFields))]
        public void KnownNestedKindsAndWidthsPreventHttp(int route, string path, string field, string raw)
        {
            var request = Valid(route); var obj = (JObject)(path == "" ? request : request.SelectToken(path)); obj.Remove(field); obj[field] = JToken.Parse(raw); AssertInvalid(route, request);
        }
        private static System.Collections.IEnumerable InvalidKnownFields()
        {
            yield return new object[] { 0, "", "materialPostingRows", "{}" };
            yield return new object[] { 0, "materialPostingRows[0]", "productId", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "productNumber", "1" };
            yield return new object[] { 0, "materialPostingRows[0]", "quantity", "\"wrong\"" };
            yield return new object[] { 0, "materialPostingRows[0]", "warehouseId", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "contactId", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "amount", "\"wrong\"" };
            yield return new object[] { 0, "materialPostingRows[0]", "price", "\"wrong\"" };
            yield return new object[] { 0, "materialPostingRows[0]", "projectId", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "dimension1", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "dimension2", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "dimension3", "9223372036854775808" };
            yield return new object[] { 0, "materialPostingRows[0]", "postingTs", "9223372036854775808" };
            yield return new object[] { 1, "", "vendor", "[]" };
            yield return new object[] { 1, "", "purchaseDeliveryDate", "1" };
            yield return new object[] { 1, "", "externalPurchaseDeliveryNumber", "1" };
            yield return new object[] { 1, "", "enableAutomapping", "\"wrong\"" };
            yield return new object[] { 1, "", "referencedPurchaseOrderNumbers", "{}" };
            yield return new object[] { 1, "", "generatePurchaseOrderForRemainingValue", "\"wrong\"" };
            yield return new object[] { 1, "", "postingDate", "1" };
            yield return new object[] { 1, "", "postDelivery", "\"wrong\"" };
            yield return new object[] { 1, "", "materialPurchaseDeliveryPositions", "{}" };
            yield return new object[] { 2, "", "postingDate", "1" };
            yield return new object[] { 3, "", "vendor", "[]" };
            yield return new object[] { 3, "", "purchaseOrderDate", "1" };
            yield return new object[] { 3, "", "externalPurchaseOrderNumber", "1" };
            yield return new object[] { 3, "", "materialPurchaseOrderPositions", "{}" };
            yield return new object[] { 1, "vendor", "vendorId", "9223372036854775808" };
            yield return new object[] { 1, "vendor", "externalSystemVendorId", "1" };
            yield return new object[] { 3, "vendor", "vendorId", "9223372036854775808" };
            yield return new object[] { 3, "vendor", "externalSystemVendorId", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0]", "product", "[]" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0]", "quantity", "\"wrong\"" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0]", "unit", "[]" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0]", "price", "\"wrong\"" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0]", "product", "[]" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0]", "quantity", "\"wrong\"" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0]", "unit", "[]" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0]", "price", "\"wrong\"" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "productId", "9223372036854775808" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "externalSystemProductId", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "productName", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "productNumber", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "vendorProductNumber", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "thirdPartyProductNumber", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "productGtin", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].product", "vendorProductGtin", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "productId", "9223372036854775808" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "externalSystemProductId", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "productName", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "productNumber", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "vendorProductNumber", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "thirdPartyProductNumber", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "productGtin", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].product", "vendorProductGtin", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "measurementUnitId", "9223372036854775808" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "deliveryUnitId", "9223372036854775808" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "externalSystemUnitId", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "unitName", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "thirdPartyUnitId", "1" };
            yield return new object[] { 1, "materialPurchaseDeliveryPositions[0].unit", "thirdPartyUnitName", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "measurementUnitId", "9223372036854775808" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "deliveryUnitId", "9223372036854775808" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "externalSystemUnitId", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "unitName", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "thirdPartyUnitId", "1" };
            yield return new object[] { 3, "materialPurchaseOrderPositions[0].unit", "thirdPartyUnitName", "1" };
        }
        [Test]
        public void MinimumZeroRejectsNegativeTinyValuesWithoutDecimalUnderflow([Values(0, 1, 3)] int route, [Values(-1.0, -1e-100)] double number)
        {
            var request = Valid(route); var key = route == 0 ? "materialPostingRows" : route == 1 ? "materialPurchaseDeliveryPositions" : "materialPurchaseOrderPositions";
            ((JObject)request[key][0]).Remove("quantity"); request[key][0]["quantity"] = number; AssertInvalid(route, request);
        }
        [Test]
        public void NonfiniteNumbersPreventHttp([Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double number)
        { var request = Valid(0); var row = (JObject)request["materialPostingRows"][0]; row.Remove("quantity"); row["quantity"] = number; AssertInvalid(0, request); }
        [Test]
        public async Task ZeroAndTinyPositiveQuantitiesAreNotClamped([Values(0.0, 1e-100)] double number)
        {
            var request = Valid(0); var row = (JObject)request["materialPostingRows"][0]; row.Remove("quantity"); row["quantity"] = number;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), 0, request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [Test]
        public async Task CandidateResultsAndFeedbackRemainCompleteWithoutAggregateSuccess()
        {
            const string json = "{\"status\":200,\"results\":[{\"candidate\":true,\"future\":\"original\"}],\"feedback\":[{\"type\":\"FUTURE\",\"message\":\"review required\",\"locationIdentifier\":1}]}";
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            { var info = await Invoke(Api(http), 1, Valid(1), 2, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data["results"][0].Value<bool>("candidate"), Is.True); Assert.That(info.Data["feedback"][0].Value<string>("type"), Is.EqualTo("FUTURE")); }
        }
        [Test]
        public void EveryTypedModelFieldMapsItsSchemaAndNullOmission()
        {
            {
                var model = new MaterialInventoryCreditRequest();
                model.MaterialPostingRows = new JArray(); Assert.That(model["materialPostingRows"], Is.Not.Null); Assert.That(model.MaterialPostingRows, Is.Not.Null); model.MaterialPostingRows = null; Assert.That(model["materialPostingRows"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialInventoryCreditRequest(null)));
            }
            {
                var model = new MaterialInventoryCreditRowRequest();
                model.ProductId = long.MaxValue; Assert.That(model["productId"], Is.Not.Null); Assert.That(model.ProductId, Is.Not.Null); model.ProductId = null; Assert.That(model["productId"], Is.Null);
                model.ProductNumber = "original"; Assert.That(model["productNumber"], Is.Not.Null); Assert.That(model.ProductNumber, Is.Not.Null); model.ProductNumber = null; Assert.That(model["productNumber"], Is.Null);
                model.Quantity = 123456789.123456789m; Assert.That(model["quantity"], Is.Not.Null); Assert.That(model.Quantity, Is.Not.Null); model.Quantity = null; Assert.That(model["quantity"], Is.Null);
                model.WarehouseId = long.MaxValue; Assert.That(model["warehouseId"], Is.Not.Null); Assert.That(model.WarehouseId, Is.Not.Null); model.WarehouseId = null; Assert.That(model["warehouseId"], Is.Null);
                model.ContactId = long.MaxValue; Assert.That(model["contactId"], Is.Not.Null); Assert.That(model.ContactId, Is.Not.Null); model.ContactId = null; Assert.That(model["contactId"], Is.Null);
                model.Amount = 123456789.123456789m; Assert.That(model["amount"], Is.Not.Null); Assert.That(model.Amount, Is.Not.Null); model.Amount = null; Assert.That(model["amount"], Is.Null);
                model.Price = 123456789.123456789m; Assert.That(model["price"], Is.Not.Null); Assert.That(model.Price, Is.Not.Null); model.Price = null; Assert.That(model["price"], Is.Null);
                model.ProjectId = long.MaxValue; Assert.That(model["projectId"], Is.Not.Null); Assert.That(model.ProjectId, Is.Not.Null); model.ProjectId = null; Assert.That(model["projectId"], Is.Null);
                model.Dimension1 = long.MaxValue; Assert.That(model["dimension1"], Is.Not.Null); Assert.That(model.Dimension1, Is.Not.Null); model.Dimension1 = null; Assert.That(model["dimension1"], Is.Null);
                model.Dimension2 = long.MaxValue; Assert.That(model["dimension2"], Is.Not.Null); Assert.That(model.Dimension2, Is.Not.Null); model.Dimension2 = null; Assert.That(model["dimension2"], Is.Null);
                model.Dimension3 = long.MaxValue; Assert.That(model["dimension3"], Is.Not.Null); Assert.That(model.Dimension3, Is.Not.Null); model.Dimension3 = null; Assert.That(model["dimension3"], Is.Null);
                model.PostingTs = long.MaxValue; Assert.That(model["postingTs"], Is.Not.Null); Assert.That(model.PostingTs, Is.Not.Null); model.PostingTs = null; Assert.That(model["postingTs"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialInventoryCreditRowRequest(null)));
            }
            {
                var model = new MaterialPurchaseDeliveryCreationRequest();
                model.Vendor = new JObject(); Assert.That(model["vendor"], Is.Not.Null); Assert.That(model.Vendor, Is.Not.Null); model.Vendor = null; Assert.That(model["vendor"], Is.Null);
                model.PurchaseDeliveryDate = "original"; Assert.That(model["purchaseDeliveryDate"], Is.Not.Null); Assert.That(model.PurchaseDeliveryDate, Is.Not.Null); model.PurchaseDeliveryDate = null; Assert.That(model["purchaseDeliveryDate"], Is.Null);
                model.ExternalPurchaseDeliveryNumber = "original"; Assert.That(model["externalPurchaseDeliveryNumber"], Is.Not.Null); Assert.That(model.ExternalPurchaseDeliveryNumber, Is.Not.Null); model.ExternalPurchaseDeliveryNumber = null; Assert.That(model["externalPurchaseDeliveryNumber"], Is.Null);
                model.EnableAutomapping = false; Assert.That(model["enableAutomapping"], Is.Not.Null); Assert.That(model.EnableAutomapping, Is.Not.Null); model.EnableAutomapping = null; Assert.That(model["enableAutomapping"], Is.Null);
                model.ReferencedPurchaseOrderNumbers = new JArray(); Assert.That(model["referencedPurchaseOrderNumbers"], Is.Not.Null); Assert.That(model.ReferencedPurchaseOrderNumbers, Is.Not.Null); model.ReferencedPurchaseOrderNumbers = null; Assert.That(model["referencedPurchaseOrderNumbers"], Is.Null);
                model.GeneratePurchaseOrderForRemainingValue = false; Assert.That(model["generatePurchaseOrderForRemainingValue"], Is.Not.Null); Assert.That(model.GeneratePurchaseOrderForRemainingValue, Is.Not.Null); model.GeneratePurchaseOrderForRemainingValue = null; Assert.That(model["generatePurchaseOrderForRemainingValue"], Is.Null);
                model.PostingDate = "original"; Assert.That(model["postingDate"], Is.Not.Null); Assert.That(model.PostingDate, Is.Not.Null); model.PostingDate = null; Assert.That(model["postingDate"], Is.Null);
                model.PostDelivery = false; Assert.That(model["postDelivery"], Is.Not.Null); Assert.That(model.PostDelivery, Is.Not.Null); model.PostDelivery = null; Assert.That(model["postDelivery"], Is.Null);
                model.MaterialPurchaseDeliveryPositions = new JArray(); Assert.That(model["materialPurchaseDeliveryPositions"], Is.Not.Null); Assert.That(model.MaterialPurchaseDeliveryPositions, Is.Not.Null); model.MaterialPurchaseDeliveryPositions = null; Assert.That(model["materialPurchaseDeliveryPositions"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchaseDeliveryCreationRequest(null)));
            }
            {
                var model = new MaterialPurchaseDeliveryPostingRequest();
                model.PostingDate = "original"; Assert.That(model["postingDate"], Is.Not.Null); Assert.That(model.PostingDate, Is.Not.Null); model.PostingDate = null; Assert.That(model["postingDate"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchaseDeliveryPostingRequest(null)));
            }
            {
                var model = new MaterialPurchaseOrderCreationRequest();
                model.Vendor = new JObject(); Assert.That(model["vendor"], Is.Not.Null); Assert.That(model.Vendor, Is.Not.Null); model.Vendor = null; Assert.That(model["vendor"], Is.Null);
                model.PurchaseOrderDate = "original"; Assert.That(model["purchaseOrderDate"], Is.Not.Null); Assert.That(model.PurchaseOrderDate, Is.Not.Null); model.PurchaseOrderDate = null; Assert.That(model["purchaseOrderDate"], Is.Null);
                model.ExternalPurchaseOrderNumber = "original"; Assert.That(model["externalPurchaseOrderNumber"], Is.Not.Null); Assert.That(model.ExternalPurchaseOrderNumber, Is.Not.Null); model.ExternalPurchaseOrderNumber = null; Assert.That(model["externalPurchaseOrderNumber"], Is.Null);
                model.MaterialPurchaseOrderPositions = new JArray(); Assert.That(model["materialPurchaseOrderPositions"], Is.Not.Null); Assert.That(model.MaterialPurchaseOrderPositions, Is.Not.Null); model.MaterialPurchaseOrderPositions = null; Assert.That(model["materialPurchaseOrderPositions"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchaseOrderCreationRequest(null)));
            }
            {
                var model = new MaterialPurchasePartyRequest();
                model.VendorId = long.MaxValue; Assert.That(model["vendorId"], Is.Not.Null); Assert.That(model.VendorId, Is.Not.Null); model.VendorId = null; Assert.That(model["vendorId"], Is.Null);
                model.ExternalSystemVendorId = "original"; Assert.That(model["externalSystemVendorId"], Is.Not.Null); Assert.That(model.ExternalSystemVendorId, Is.Not.Null); model.ExternalSystemVendorId = null; Assert.That(model["externalSystemVendorId"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchasePartyRequest(null)));
            }
            {
                var model = new MaterialPurchaseProductRequest();
                model.ProductId = long.MaxValue; Assert.That(model["productId"], Is.Not.Null); Assert.That(model.ProductId, Is.Not.Null); model.ProductId = null; Assert.That(model["productId"], Is.Null);
                model.ExternalSystemProductId = "original"; Assert.That(model["externalSystemProductId"], Is.Not.Null); Assert.That(model.ExternalSystemProductId, Is.Not.Null); model.ExternalSystemProductId = null; Assert.That(model["externalSystemProductId"], Is.Null);
                model.ProductName = "original"; Assert.That(model["productName"], Is.Not.Null); Assert.That(model.ProductName, Is.Not.Null); model.ProductName = null; Assert.That(model["productName"], Is.Null);
                model.ProductNumber = "original"; Assert.That(model["productNumber"], Is.Not.Null); Assert.That(model.ProductNumber, Is.Not.Null); model.ProductNumber = null; Assert.That(model["productNumber"], Is.Null);
                model.VendorProductNumber = "original"; Assert.That(model["vendorProductNumber"], Is.Not.Null); Assert.That(model.VendorProductNumber, Is.Not.Null); model.VendorProductNumber = null; Assert.That(model["vendorProductNumber"], Is.Null);
                model.ThirdPartyProductNumber = "original"; Assert.That(model["thirdPartyProductNumber"], Is.Not.Null); Assert.That(model.ThirdPartyProductNumber, Is.Not.Null); model.ThirdPartyProductNumber = null; Assert.That(model["thirdPartyProductNumber"], Is.Null);
                model.ProductGtin = "original"; Assert.That(model["productGtin"], Is.Not.Null); Assert.That(model.ProductGtin, Is.Not.Null); model.ProductGtin = null; Assert.That(model["productGtin"], Is.Null);
                model.VendorProductGtin = "original"; Assert.That(model["vendorProductGtin"], Is.Not.Null); Assert.That(model.VendorProductGtin, Is.Not.Null); model.VendorProductGtin = null; Assert.That(model["vendorProductGtin"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchaseProductRequest(null)));
            }
            {
                var model = new MaterialPurchaseUnitRequest();
                model.MeasurementUnitId = long.MaxValue; Assert.That(model["measurementUnitId"], Is.Not.Null); Assert.That(model.MeasurementUnitId, Is.Not.Null); model.MeasurementUnitId = null; Assert.That(model["measurementUnitId"], Is.Null);
                model.DeliveryUnitId = long.MaxValue; Assert.That(model["deliveryUnitId"], Is.Not.Null); Assert.That(model.DeliveryUnitId, Is.Not.Null); model.DeliveryUnitId = null; Assert.That(model["deliveryUnitId"], Is.Null);
                model.ExternalSystemUnitId = "original"; Assert.That(model["externalSystemUnitId"], Is.Not.Null); Assert.That(model.ExternalSystemUnitId, Is.Not.Null); model.ExternalSystemUnitId = null; Assert.That(model["externalSystemUnitId"], Is.Null);
                model.UnitName = "original"; Assert.That(model["unitName"], Is.Not.Null); Assert.That(model.UnitName, Is.Not.Null); model.UnitName = null; Assert.That(model["unitName"], Is.Null);
                model.ThirdPartyUnitId = "original"; Assert.That(model["thirdPartyUnitId"], Is.Not.Null); Assert.That(model.ThirdPartyUnitId, Is.Not.Null); model.ThirdPartyUnitId = null; Assert.That(model["thirdPartyUnitId"], Is.Null);
                model.ThirdPartyUnitName = "original"; Assert.That(model["thirdPartyUnitName"], Is.Not.Null); Assert.That(model.ThirdPartyUnitName, Is.Not.Null); model.ThirdPartyUnitName = null; Assert.That(model["thirdPartyUnitName"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchaseUnitRequest(null)));
            }
            {
                var model = new MaterialPurchasePositionRequest();
                model.Product = new JObject(); Assert.That(model["product"], Is.Not.Null); Assert.That(model.Product, Is.Not.Null); model.Product = null; Assert.That(model["product"], Is.Null);
                model.Quantity = 123456789.123456789m; Assert.That(model["quantity"], Is.Not.Null); Assert.That(model.Quantity, Is.Not.Null); model.Quantity = null; Assert.That(model["quantity"], Is.Null);
                model.Unit = new JObject(); Assert.That(model["unit"], Is.Not.Null); Assert.That(model.Unit, Is.Not.Null); model.Unit = null; Assert.That(model["unit"], Is.Null);
                model.Price = 123456789.123456789m; Assert.That(model["price"], Is.Not.Null); Assert.That(model.Price, Is.Not.Null); model.Price = null; Assert.That(model["price"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new MaterialPurchasePositionRequest(null)));
            }
        }
        [Test]
        public void NestedVendorProductUnitAndPositionArraysAreDefensiveCopies()
        {
            var vendor = new JObject { ["externalSystemVendorId"] = "original" };
            var product = new JObject { ["productNumber"] = "0001" };
            var unit = new JObject { ["unitName"] = "original" };
            var position = new MaterialPurchasePositionRequest { Product = product, Unit = unit };
            var rows = new JArray(position); var request = new MaterialPurchaseDeliveryCreationRequest { Vendor = vendor, MaterialPurchaseDeliveryPositions = rows, ReferencedPurchaseOrderNumbers = new JArray("0001") };
            vendor["externalSystemVendorId"] = "source changed"; product["productNumber"] = "source changed"; unit["unitName"] = "source changed"; rows.Clear();
            var copy = request.MaterialPurchaseDeliveryPositions; copy.Clear(); var vendorCopy = request.Vendor; vendorCopy["externalSystemVendorId"] = "getter changed";
            Assert.That(request.Vendor.Value<string>("externalSystemVendorId"), Is.EqualTo("original")); Assert.That(request.MaterialPurchaseDeliveryPositions[0]["product"].Value<string>("productNumber"), Is.EqualTo("0001"));
            var productCopy = position.Product; productCopy["productNumber"] = "getter changed"; var unitCopy = position.Unit; unitCopy["unitName"] = "getter changed";
            Assert.That(position.Product.Value<string>("productNumber"), Is.EqualTo("0001")); Assert.That(position.Unit.Value<string>("unitName"), Is.EqualTo("original"));
            var numbers = request.ReferencedPurchaseOrderNumbers; numbers.Clear(); Assert.That(request.ReferencedPurchaseOrderNumbers.Count, Is.EqualTo(1));
        }
        [Test]
        public void NullArrayItemsAndNonStringReferencesPreventHttp([Values(0, 1, 2)] int kind)
        {
            var route = kind == 0 ? 0 : 1; var request = Valid(route);
            request[kind == 0 ? "materialPostingRows" : kind == 1 ? "materialPurchaseDeliveryPositions" : "referencedPurchaseOrderNumbers"] = new JArray(JValue.CreateNull());
            AssertInvalid(route, request);
        }
        private static void AssertInvalid(int route, JObject request)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), route, request, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        private static JObject Copy(int route, JObject source)
        {
            if (route == 0) return new MaterialInventoryCreditRequest(source);
            if (route == 1) return new MaterialPurchaseDeliveryCreationRequest(source);
            if (route == 2) return new MaterialPurchaseDeliveryPostingRequest(source);
            return new MaterialPurchaseOrderCreationRequest(source);
        }
        private static JObject Valid(int route)
        {
            if (route == 0) return new MaterialInventoryCreditRequest { MaterialPostingRows = new JArray(new MaterialInventoryCreditRowRequest { ProductId = long.MaxValue, ProductNumber = "000123A", Quantity = 10.123456789m, WarehouseId = long.MinValue, ContactId = 0, Amount = 123.123456789m, Price = 1.123456789m, ProjectId = long.MaxValue, Dimension1 = long.MaxValue, Dimension2 = long.MinValue, Dimension3 = 0, PostingTs = long.MaxValue }) };
            if (route == 2) return new MaterialPurchaseDeliveryPostingRequest { PostingDate = "31.12.2026" };
            var vendor = new MaterialPurchasePartyRequest { VendorId = long.MaxValue, ExternalSystemVendorId = "000123A" };
            var product = new MaterialPurchaseProductRequest { ProductId = long.MaxValue, ExternalSystemProductId = "000123A", ProductName = "original", ProductNumber = "0002", VendorProductNumber = "0003", ThirdPartyProductNumber = "0004", ProductGtin = "0005", VendorProductGtin = "0006" };
            var unit = new MaterialPurchaseUnitRequest { MeasurementUnitId = long.MaxValue, DeliveryUnitId = long.MinValue, ExternalSystemUnitId = "0001A", UnitName = "original", ThirdPartyUnitId = "0002B", ThirdPartyUnitName = "original" };
            var positions = new JArray(new MaterialPurchasePositionRequest { Product = product, Quantity = 0m, Unit = unit, Price = 123456789.123456789m });
            if (route == 1) return new MaterialPurchaseDeliveryCreationRequest { Vendor = vendor, PurchaseDeliveryDate = "2026-10-13", ExternalPurchaseDeliveryNumber = "0001A", EnableAutomapping = false, ReferencedPurchaseOrderNumbers = new JArray("0001", "0002"), GeneratePurchaseOrderForRemainingValue = false, PostingDate = "31.12.2026", PostDelivery = false, MaterialPurchaseDeliveryPositions = positions };
            return new MaterialPurchaseOrderCreationRequest { Vendor = vendor, PurchaseOrderDate = "2026-10-13", ExternalPurchaseOrderNumber = "0001A", MaterialPurchaseOrderPositions = positions };
        }
        private static object[] Arguments(int route, JObject request, int mode, CancellationToken token)
        {
            if (route == 2) return mode == 2 ? new object[] { "0001/A", request, token } : new object[] { "0001/A", request };
            return mode == 2 ? new object[] { request, token } : new object[] { request };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var type = route == 0 ? typeof(MaterialInventoryCreditRequest) : route == 1 ? typeof(MaterialPurchaseDeliveryCreationRequest) : route == 2 ? typeof(MaterialPurchaseDeliveryPostingRequest) : typeof(MaterialPurchaseOrderCreationRequest);
            if (route == 2) return cancellation ? new[] { typeof(string), type, typeof(CancellationToken) } : new[] { typeof(string), type };
            return cancellation ? new[] { type, typeof(CancellationToken) } : new[] { type };
        }
        private static async Task<ApiResponse<JToken>> Invoke(MaterialManagementApi api, int route, JObject request, int mode, CancellationToken token)
        { return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), Arguments(route, request, mode, token), Types(route, mode == 2)); }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types); Assert.That(method, Is.Not.Null); object value;
            try { value = method.Invoke(api, args); } catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value; await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static MaterialManagementApi Api(HttpClient http) => new MaterialManagementApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-open-item-token" });
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
