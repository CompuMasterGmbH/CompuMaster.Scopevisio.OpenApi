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
    public class OutgoingInvoiceUpdateTests
    {
        private const string Json = "{\"future\":{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"}}";

        [Test]
        public async Task InvoiceNumbersAreEscapedWithoutParsingOrTrimming([Values("0001", " ID with spaces ", "invoice/part", "№?#%", "%2F")] string selection, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), selection, null, mode, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/outgoinginvoice/" + Uri.EscapeDataString(selection)));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Body, Is.Null);
            }
        }


        [Test]
        public void InvalidSelectionsPreventHttp([Values(null, "", " ", ".", "..")] string selection, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), selection, null, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }


        [Test]
        public async Task ConvenienceFormsExposeCompleteJson([Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)))))
            {
                var api = Api(http);
                var data = mode == 0 ? api.UpdateOutgoingInvoice("0001") : mode == 1 ? await api.UpdateOutgoingInvoiceAsync("0001") : await api.UpdateOutgoingInvoiceAsync("0001", null, CancellationToken.None);
                Assert.That(data["future"].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }


        [Test]
        public async Task UndeclaredResultShapesRemainExact([Values("{}", "[]", "null", "true")] string json)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)))))
            {
                var info = await Invoke(Api(http), "0001", null, 2, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }


        [Test]
        public void OriginalWorkflowAndAssignmentErrorsRunBeforeParser([Values(400, 403, 404, 500)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original invoice/workflow/contact diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, "0001", null, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original invoice/workflow/contact diagnostic"));
                var expected = new InvalidOperationException("custom outgoing-invoice hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("UpdateOutgoingInvoice")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, "0001", null, mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }


        [Test]
        public void PreCancellationPreventsAssignment()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), null, null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }


        [Test]
        public async Task ActiveCancellationReachesAssignmentHttpWithoutRetry()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), "0001", null, 2, cancellation.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancellation.Cancel(); }
            }
        }


        [TestCase("UpdateOutgoingInvoice")]
        [TestCase("UpdateOutgoingInvoiceWithHttpInfo")]
        [TestCase("UpdateOutgoingInvoiceAsyncWithHttpInfo")]
        public void WorkflowExists(string name) { Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Any(m => m.Name == name), Is.True); }

        [Test]
        public async Task AllCallerFieldsAndNestedPositionsRemainExact([Values(0, 1, 2)] int mode)
        {
            var request = Valid(); request["future"] = new JObject { ["nullable"] = JValue.CreateNull(), ["flag"] = false };
            var response = Response(HttpStatusCode.OK, Json); response.Headers.TryAddWithoutValidation("X-Request", "normal"); response.Content.Headers.TryAddWithoutValidation("X-Content", "content");
            var handler = new Handler(token => Task.FromResult(response));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), "0001/A", request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/outgoinginvoice/0001%2FA"));
                Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(handler.Calls, Is.EqualTo(1));
                Assert.That(info.StatusCode, Is.EqualTo(200)); Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Request"], Is.EqualTo("normal")); Assert.That(info.Headers["X-Content"], Is.EqualTo("content"));
                Assert.That(info.Data["future"].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
            }
        }
        [Test]
        public async Task OptionalBodyNullOmitsAndEmptyRequiredValuesStayOriginal([Values(false, true)] bool supplied)
        {
            var request = supplied ? new OutgoingInvoiceUpdateRequest { CustomerContactId = 0, DocumentDate = long.MinValue } : null;
            if (request != null) { request["documentNumber"] = ""; request["positionsForm"] = JValue.CreateNull(); request["isIntercompany"] = JValue.CreateNull(); }
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "")));
            using (var http = new HttpClient(handler)) { var info = await Invoke(Api(http), "0001", request, 2, CancellationToken.None); Assert.That(info.Data, Is.Null); }
            Assert.That(handler.Body, Is.EqualTo(request?.ToString(Formatting.None)));
        }
        [Test]
        public async Task ModelAndTransportSnapshotsDoNotAliasSources()
        {
            var source = Valid(); source["future"] = new JObject { ["value"] = "original" };
            var request = new OutgoingInvoiceUpdateRequest(source); source["future"]["value"] = "source changed";
            var handler = new Handler(token => { request["future"]["value"] = "caller changed"; return Task.FromResult(Response(HttpStatusCode.OK, "{}")); });
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), "0001", request, 2, CancellationToken.None); }
            Assert.That(JObject.Parse(handler.Body)["future"].Value<string>("value"), Is.EqualTo("original"));
        }
        [Test]
        public void RequiredRootAndPositionFieldsRejectMissingOrNull([Values(0, 1, 2)] int level, [Values(false, true)] bool explicitNull)
        {
            var request = Valid();
            var obj = level == 0 ? (JObject)request : level == 1 ? (JObject)request["positionsForm"] : (JObject)request["positionsForm"]["positions"][0];
            var names = level == 0 ? new[] { "customerContactId", "documentDate" } : level == 1 ? new[] { "positions" } : new[] { "name" };
            foreach (var name in names) { var original = obj[name]; if (explicitNull) obj[name] = JValue.CreateNull(); else obj.Remove(name); AssertInvalid(request); obj[name] = original; }
        }
        [Test]
        public void MalformedNestedShapesPreventHttp([Values(0, 1, 2, 3, 4, 5)] int kind)
        {
            var request = Valid();
            if (kind == 0) request["positionsForm"] = new JArray();
            if (kind == 1) request["positionsForm"]["positions"] = new JObject();
            if (kind == 2) request["positionsForm"]["positions"] = new JArray(JValue.CreateNull());
            if (kind == 3) request["customFields"] = new JObject();
            if (kind == 4) request["customFields"] = new JArray(JValue.CreateNull());
            if (kind == 5) request["customFields"] = new JArray(new JObject { ["customFieldName"] = "x" });
            AssertInvalid(request);
        }
        [Test]
        public async Task CustomFieldValueSchemaExampleConflictAndExplicitNullArePreserved([Values(0, 1, 2)] int kind)
        {
            var request = Valid(); var field = (JObject)request["customFields"][0];
            field["customFieldValue"] = kind == 0 ? (JToken)new JValue("original text") : kind == 1 ? new JObject { ["future"] = true } : (JToken)JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler)) { await Invoke(Api(http), "0001", request, 2, CancellationToken.None); }
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }
        [Test]
        public void NonfiniteUpdateAmountsPreventHttp([Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double number)
        {
            var request = Valid(); request.Remove("deliveryCosts"); request["deliveryCosts"] = number; AssertInvalid(request);
        }
        [Test]
        public void NestedObjectsArraysAndCustomJsonAreDefensiveCopies()
        {
            var source = new JObject { ["positions"] = new JArray(new JObject { ["name"] = "original" }) };
            var request = new OutgoingInvoiceUpdateRequest { PositionsForm = source, CustomFields = new JArray(new JObject { ["customFieldName"] = "x", ["customFieldValue"] = new JObject { ["future"] = true } }) };
            source["positions"][0]["name"] = "source changed"; var copy = request.PositionsForm; copy["positions"][0]["name"] = "getter changed";
            Assert.That(request.PositionsForm["positions"][0].Value<string>("name"), Is.EqualTo("original"));
            var fields = request.CustomFields; fields.Clear(); Assert.That(request.CustomFields.Count, Is.EqualTo(1));
            var value = new JObject { ["future"] = "original" }; var field = new OutgoingInvoiceUpdateCustomFieldRequest { CustomFieldValue = value };
            value["future"] = "source changed"; var valueCopy = field.CustomFieldValue; valueCopy["future"] = "getter changed";
            Assert.That(field.CustomFieldValue.Value<string>("future"), Is.EqualTo("original"));
        }
        private static void AssertInvalid(OutgoingInvoiceUpdateRequest request)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            { Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), "0001", request, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero); }
        }
        private static OutgoingInvoiceUpdateRequest Valid()
        {
            var row = new OutgoingInvoiceUpdatePositionRequest
            {
                TypeDisplay = "original typeDisplay",
                Number = "0001/A",
                Name = "original name",
                Description = "original description",
                Quantity = -123456789.123456789m,
                Unit = "original unit",
                SingleAmount = -123456789.123456789m,
                Account = "0001/A",
                VatKey = "original vatKey",
                Discount = -123456789.123456789m,
                FreeText1 = "original freeText1",
                FreeText2 = "original freeText2",
                SingleCostAmount = -123456789.123456789m,
                ValutaDate = long.MaxValue,
                Dimension1 = "original dimension_1",
                Dimension2 = "original dimension_2",
                Dimension3 = "original dimension_3",
                Dimension4 = "original dimension_4",
                Dimension5 = "original dimension_5",
                Dimension6 = "original dimension_6",
                Dimension7 = "original dimension_7",
                Dimension8 = "original dimension_8",
                Dimension9 = "original dimension_9",
                Dimension10 = "original dimension_10",
                AsIfSold = false,
                DateFrom = long.MaxValue,
                DateTo = long.MaxValue,
            };
            var request = new OutgoingInvoiceUpdateRequest
            {
                ProjectId = long.MaxValue,
                CustomerContactId = long.MaxValue,
                Name = "original name",
                Text = "original text",
                CustomerContactPerson = long.MaxValue,
                DocumentDate = long.MaxValue,
                DocumentNumber = "0001/A",
                OurContactPerson = "original ourContactPerson",
                DeliveryType = "original deliveryType",
                DeliveryCosts = -123456789.123456789m,
                CashOnDelivery = -123456789.123456789m,
                TextTitle = "original textTitle",
                TextSubTitle = "original textSubTitle",
                TextIntro = "original textIntro",
                TextExtro = "original textExtro",
                SelectedAddress = long.MaxValue,
                OtherAddress = "original otherAddress",
                Gross = false,
                PermissionType = long.MaxValue,
                ReadPermissionProfiles = "original readPermissionProfiles",
                ReadPermissionLogins = "original readPermissionLogins",
                WritePermissionProfiles = "original writePermissionProfiles",
                WritePermissionLogins = "original writePermissionLogins",
                CustomFields = new JArray(new OutgoingInvoiceUpdateCustomFieldRequest { CustomFieldName = "customText1", CustomFieldValue = new JValue("original text") }),
                PositionsForm = new OutgoingInvoiceUpdatePositionsRequest { CopyProductsToPositions = false, Positions = new JArray(row) },
                DeliveryDate = long.MaxValue,
                DeliveryDateTo = long.MaxValue,
                IsEndDiscountAbsolute = false,
                EndDiscount = -123456789.123456789m,
                PaymentTypeName = "original paymentTypeName",
                IsPaid = false,
                DiscountDate1 = long.MaxValue,
                DiscountDate2 = long.MaxValue,
                DueDate = long.MaxValue,
                DiscountPercent1 = -123456789.123456789m,
                DiscountPercent2 = -123456789.123456789m,
                ExplicitDiscount = false,
                DiscountAmount1 = -123456789.123456789m,
                DiscountAmount2 = -123456789.123456789m,
                OrderNumber = "0001/A",
                ZoneNumber = "0001/A",
                PostingDate = long.MaxValue,
                DocumentText = "original documentText",
                Locale = "original locale",
                Currency = "original currency",
                ExchangeRate = -123456789.123456789m,
                Reference = "original reference",
                TaxCountryCodeIso2 = "original taxCountryCodeIso2",
                IsIntercompany = false,
            };
            return request;
        }
        [TestCaseSource(nameof(InvalidKnownFields))]
        public void AllKnownKindsAndWidthsPreventHttp(int level, string field, string raw)
        {
            var request = Valid(); var obj = level == 0 ? (JObject)request : level == 1 ? (JObject)request["positionsForm"] : (JObject)request["positionsForm"]["positions"][0]; obj.Remove(field); obj[field] = JToken.Parse(raw); AssertInvalid(request);
        }
        private static System.Collections.IEnumerable InvalidKnownFields()
        {
            yield return new object[] { 0, "projectId", "9223372036854775808" };
            yield return new object[] { 0, "customerContactId", "9223372036854775808" };
            yield return new object[] { 0, "customerContactName", "1" };
            yield return new object[] { 0, "customerContactCustomerNumber", "1" };
            yield return new object[] { 0, "customerPersonalAccountId", "9223372036854775808" };
            yield return new object[] { 0, "customerPersonalAccountNumber", "1" };
            yield return new object[] { 0, "customerPersonalAccountName", "1" };
            yield return new object[] { 0, "customerPersonalAccountExternalNumber", "1" };
            yield return new object[] { 0, "customerName", "1" };
            yield return new object[] { 0, "customerNumber", "1" };
            yield return new object[] { 0, "customerNameExtra", "1" };
            yield return new object[] { 0, "name", "1" };
            yield return new object[] { 0, "text", "1" };
            yield return new object[] { 0, "customerContactPerson", "9223372036854775808" };
            yield return new object[] { 0, "documentDate", "9223372036854775808" };
            yield return new object[] { 0, "documentNumber", "1" };
            yield return new object[] { 0, "ourContactPerson", "1" };
            yield return new object[] { 0, "deliveryType", "1" };
            yield return new object[] { 0, "deliveryCosts", "\"wrong\"" };
            yield return new object[] { 0, "cashOnDelivery", "\"wrong\"" };
            yield return new object[] { 0, "textTitle", "1" };
            yield return new object[] { 0, "textSubTitle", "1" };
            yield return new object[] { 0, "textIntro", "1" };
            yield return new object[] { 0, "textExtro", "1" };
            yield return new object[] { 0, "selectedAddress", "9223372036854775808" };
            yield return new object[] { 0, "otherAddress", "1" };
            yield return new object[] { 0, "gross", "\"wrong\"" };
            yield return new object[] { 0, "permissionType", "9223372036854775808" };
            yield return new object[] { 0, "readPermissionProfiles", "1" };
            yield return new object[] { 0, "readPermissionLogins", "1" };
            yield return new object[] { 0, "writePermissionProfiles", "1" };
            yield return new object[] { 0, "writePermissionLogins", "1" };
            yield return new object[] { 0, "customFields", "{}" };
            yield return new object[] { 0, "deliveryDate", "9223372036854775808" };
            yield return new object[] { 0, "deliveryDateTo", "9223372036854775808" };
            yield return new object[] { 0, "isEndDiscountAbsolute", "\"wrong\"" };
            yield return new object[] { 0, "endDiscount", "\"wrong\"" };
            yield return new object[] { 0, "paymentTypeName", "1" };
            yield return new object[] { 0, "isPaid", "\"wrong\"" };
            yield return new object[] { 0, "discountDate1", "9223372036854775808" };
            yield return new object[] { 0, "discountDate2", "9223372036854775808" };
            yield return new object[] { 0, "dueDate", "9223372036854775808" };
            yield return new object[] { 0, "discountPercent1", "\"wrong\"" };
            yield return new object[] { 0, "discountPercent2", "\"wrong\"" };
            yield return new object[] { 0, "explicitDiscount", "\"wrong\"" };
            yield return new object[] { 0, "discountAmount1", "\"wrong\"" };
            yield return new object[] { 0, "discountAmount2", "\"wrong\"" };
            yield return new object[] { 0, "orderNumber", "1" };
            yield return new object[] { 0, "zoneNumber", "1" };
            yield return new object[] { 0, "postingDate", "9223372036854775808" };
            yield return new object[] { 0, "documentText", "1" };
            yield return new object[] { 0, "locale", "1" };
            yield return new object[] { 0, "currency", "1" };
            yield return new object[] { 0, "exchangeRate", "\"wrong\"" };
            yield return new object[] { 0, "reference", "1" };
            yield return new object[] { 0, "taxCountryCodeIso2", "1" };
            yield return new object[] { 0, "isIntercompany", "\"wrong\"" };
            yield return new object[] { 1, "copyProductsToPositions", "\"wrong\"" };
            yield return new object[] { 1, "positions", "{}" };
            yield return new object[] { 2, "typeDisplay", "1" };
            yield return new object[] { 2, "number", "1" };
            yield return new object[] { 2, "name", "1" };
            yield return new object[] { 2, "description", "1" };
            yield return new object[] { 2, "quantity", "\"wrong\"" };
            yield return new object[] { 2, "unit", "1" };
            yield return new object[] { 2, "singleAmount", "\"wrong\"" };
            yield return new object[] { 2, "account", "1" };
            yield return new object[] { 2, "vatKey", "1" };
            yield return new object[] { 2, "discount", "\"wrong\"" };
            yield return new object[] { 2, "freeText1", "1" };
            yield return new object[] { 2, "freeText2", "1" };
            yield return new object[] { 2, "singleCostAmount", "\"wrong\"" };
            yield return new object[] { 2, "valutaDate", "9223372036854775808" };
            yield return new object[] { 2, "dimension_1", "1" };
            yield return new object[] { 2, "dimension_2", "1" };
            yield return new object[] { 2, "dimension_3", "1" };
            yield return new object[] { 2, "dimension_4", "1" };
            yield return new object[] { 2, "dimension_5", "1" };
            yield return new object[] { 2, "dimension_6", "1" };
            yield return new object[] { 2, "dimension_7", "1" };
            yield return new object[] { 2, "dimension_8", "1" };
            yield return new object[] { 2, "dimension_9", "1" };
            yield return new object[] { 2, "dimension_10", "1" };
            yield return new object[] { 2, "asIfSold", "\"wrong\"" };
            yield return new object[] { 2, "dateFrom", "9223372036854775808" };
            yield return new object[] { 2, "dateTo", "9223372036854775808" };
        }
        [Test]
        public void EveryWritableTypedFieldUsesItsExactSchemaNameAndNullOmission()
        {
            {
                var model = new OutgoingInvoiceUpdateRequest();
                model.ProjectId = long.MaxValue; Assert.That(model["projectId"], Is.Not.Null); Assert.That(model.ProjectId, Is.Not.Null); model.ProjectId = null; Assert.That(model["projectId"], Is.Null);
                model.CustomerContactId = long.MaxValue; Assert.That(model["customerContactId"], Is.Not.Null); Assert.That(model.CustomerContactId, Is.Not.Null); model.CustomerContactId = null; Assert.That(model["customerContactId"], Is.Null);
                model.Name = "original name"; Assert.That(model["name"], Is.Not.Null); Assert.That(model.Name, Is.Not.Null); model.Name = null; Assert.That(model["name"], Is.Null);
                model.Text = "original text"; Assert.That(model["text"], Is.Not.Null); Assert.That(model.Text, Is.Not.Null); model.Text = null; Assert.That(model["text"], Is.Null);
                model.CustomerContactPerson = long.MaxValue; Assert.That(model["customerContactPerson"], Is.Not.Null); Assert.That(model.CustomerContactPerson, Is.Not.Null); model.CustomerContactPerson = null; Assert.That(model["customerContactPerson"], Is.Null);
                model.DocumentDate = long.MaxValue; Assert.That(model["documentDate"], Is.Not.Null); Assert.That(model.DocumentDate, Is.Not.Null); model.DocumentDate = null; Assert.That(model["documentDate"], Is.Null);
                model.DocumentNumber = "0001/A"; Assert.That(model["documentNumber"], Is.Not.Null); Assert.That(model.DocumentNumber, Is.Not.Null); model.DocumentNumber = null; Assert.That(model["documentNumber"], Is.Null);
                model.OurContactPerson = "original ourContactPerson"; Assert.That(model["ourContactPerson"], Is.Not.Null); Assert.That(model.OurContactPerson, Is.Not.Null); model.OurContactPerson = null; Assert.That(model["ourContactPerson"], Is.Null);
                model.DeliveryType = "original deliveryType"; Assert.That(model["deliveryType"], Is.Not.Null); Assert.That(model.DeliveryType, Is.Not.Null); model.DeliveryType = null; Assert.That(model["deliveryType"], Is.Null);
                model.DeliveryCosts = -123456789.123456789m; Assert.That(model["deliveryCosts"], Is.Not.Null); Assert.That(model.DeliveryCosts, Is.Not.Null); model.DeliveryCosts = null; Assert.That(model["deliveryCosts"], Is.Null);
                model.CashOnDelivery = -123456789.123456789m; Assert.That(model["cashOnDelivery"], Is.Not.Null); Assert.That(model.CashOnDelivery, Is.Not.Null); model.CashOnDelivery = null; Assert.That(model["cashOnDelivery"], Is.Null);
                model.TextTitle = "original textTitle"; Assert.That(model["textTitle"], Is.Not.Null); Assert.That(model.TextTitle, Is.Not.Null); model.TextTitle = null; Assert.That(model["textTitle"], Is.Null);
                model.TextSubTitle = "original textSubTitle"; Assert.That(model["textSubTitle"], Is.Not.Null); Assert.That(model.TextSubTitle, Is.Not.Null); model.TextSubTitle = null; Assert.That(model["textSubTitle"], Is.Null);
                model.TextIntro = "original textIntro"; Assert.That(model["textIntro"], Is.Not.Null); Assert.That(model.TextIntro, Is.Not.Null); model.TextIntro = null; Assert.That(model["textIntro"], Is.Null);
                model.TextExtro = "original textExtro"; Assert.That(model["textExtro"], Is.Not.Null); Assert.That(model.TextExtro, Is.Not.Null); model.TextExtro = null; Assert.That(model["textExtro"], Is.Null);
                model.SelectedAddress = long.MaxValue; Assert.That(model["selectedAddress"], Is.Not.Null); Assert.That(model.SelectedAddress, Is.Not.Null); model.SelectedAddress = null; Assert.That(model["selectedAddress"], Is.Null);
                model.OtherAddress = "original otherAddress"; Assert.That(model["otherAddress"], Is.Not.Null); Assert.That(model.OtherAddress, Is.Not.Null); model.OtherAddress = null; Assert.That(model["otherAddress"], Is.Null);
                model.Gross = false; Assert.That(model["gross"], Is.Not.Null); Assert.That(model.Gross, Is.Not.Null); model.Gross = null; Assert.That(model["gross"], Is.Null);
                model.PermissionType = long.MaxValue; Assert.That(model["permissionType"], Is.Not.Null); Assert.That(model.PermissionType, Is.Not.Null); model.PermissionType = null; Assert.That(model["permissionType"], Is.Null);
                model.ReadPermissionProfiles = "original readPermissionProfiles"; Assert.That(model["readPermissionProfiles"], Is.Not.Null); Assert.That(model.ReadPermissionProfiles, Is.Not.Null); model.ReadPermissionProfiles = null; Assert.That(model["readPermissionProfiles"], Is.Null);
                model.ReadPermissionLogins = "original readPermissionLogins"; Assert.That(model["readPermissionLogins"], Is.Not.Null); Assert.That(model.ReadPermissionLogins, Is.Not.Null); model.ReadPermissionLogins = null; Assert.That(model["readPermissionLogins"], Is.Null);
                model.WritePermissionProfiles = "original writePermissionProfiles"; Assert.That(model["writePermissionProfiles"], Is.Not.Null); Assert.That(model.WritePermissionProfiles, Is.Not.Null); model.WritePermissionProfiles = null; Assert.That(model["writePermissionProfiles"], Is.Null);
                model.WritePermissionLogins = "original writePermissionLogins"; Assert.That(model["writePermissionLogins"], Is.Not.Null); Assert.That(model.WritePermissionLogins, Is.Not.Null); model.WritePermissionLogins = null; Assert.That(model["writePermissionLogins"], Is.Null);
                model.CustomFields = new JArray(); Assert.That(model["customFields"], Is.Not.Null); Assert.That(model.CustomFields, Is.Not.Null); model.CustomFields = null; Assert.That(model["customFields"], Is.Null);
                model.PositionsForm = new JObject(); Assert.That(model["positionsForm"], Is.Not.Null); Assert.That(model.PositionsForm, Is.Not.Null); model.PositionsForm = null; Assert.That(model["positionsForm"], Is.Null);
                model.DeliveryDate = long.MaxValue; Assert.That(model["deliveryDate"], Is.Not.Null); Assert.That(model.DeliveryDate, Is.Not.Null); model.DeliveryDate = null; Assert.That(model["deliveryDate"], Is.Null);
                model.DeliveryDateTo = long.MaxValue; Assert.That(model["deliveryDateTo"], Is.Not.Null); Assert.That(model.DeliveryDateTo, Is.Not.Null); model.DeliveryDateTo = null; Assert.That(model["deliveryDateTo"], Is.Null);
                model.IsEndDiscountAbsolute = false; Assert.That(model["isEndDiscountAbsolute"], Is.Not.Null); Assert.That(model.IsEndDiscountAbsolute, Is.Not.Null); model.IsEndDiscountAbsolute = null; Assert.That(model["isEndDiscountAbsolute"], Is.Null);
                model.EndDiscount = -123456789.123456789m; Assert.That(model["endDiscount"], Is.Not.Null); Assert.That(model.EndDiscount, Is.Not.Null); model.EndDiscount = null; Assert.That(model["endDiscount"], Is.Null);
                model.PaymentTypeName = "original paymentTypeName"; Assert.That(model["paymentTypeName"], Is.Not.Null); Assert.That(model.PaymentTypeName, Is.Not.Null); model.PaymentTypeName = null; Assert.That(model["paymentTypeName"], Is.Null);
                model.IsPaid = false; Assert.That(model["isPaid"], Is.Not.Null); Assert.That(model.IsPaid, Is.Not.Null); model.IsPaid = null; Assert.That(model["isPaid"], Is.Null);
                model.DiscountDate1 = long.MaxValue; Assert.That(model["discountDate1"], Is.Not.Null); Assert.That(model.DiscountDate1, Is.Not.Null); model.DiscountDate1 = null; Assert.That(model["discountDate1"], Is.Null);
                model.DiscountDate2 = long.MaxValue; Assert.That(model["discountDate2"], Is.Not.Null); Assert.That(model.DiscountDate2, Is.Not.Null); model.DiscountDate2 = null; Assert.That(model["discountDate2"], Is.Null);
                model.DueDate = long.MaxValue; Assert.That(model["dueDate"], Is.Not.Null); Assert.That(model.DueDate, Is.Not.Null); model.DueDate = null; Assert.That(model["dueDate"], Is.Null);
                model.DiscountPercent1 = -123456789.123456789m; Assert.That(model["discountPercent1"], Is.Not.Null); Assert.That(model.DiscountPercent1, Is.Not.Null); model.DiscountPercent1 = null; Assert.That(model["discountPercent1"], Is.Null);
                model.DiscountPercent2 = -123456789.123456789m; Assert.That(model["discountPercent2"], Is.Not.Null); Assert.That(model.DiscountPercent2, Is.Not.Null); model.DiscountPercent2 = null; Assert.That(model["discountPercent2"], Is.Null);
                model.ExplicitDiscount = false; Assert.That(model["explicitDiscount"], Is.Not.Null); Assert.That(model.ExplicitDiscount, Is.Not.Null); model.ExplicitDiscount = null; Assert.That(model["explicitDiscount"], Is.Null);
                model.DiscountAmount1 = -123456789.123456789m; Assert.That(model["discountAmount1"], Is.Not.Null); Assert.That(model.DiscountAmount1, Is.Not.Null); model.DiscountAmount1 = null; Assert.That(model["discountAmount1"], Is.Null);
                model.DiscountAmount2 = -123456789.123456789m; Assert.That(model["discountAmount2"], Is.Not.Null); Assert.That(model.DiscountAmount2, Is.Not.Null); model.DiscountAmount2 = null; Assert.That(model["discountAmount2"], Is.Null);
                model.OrderNumber = "0001/A"; Assert.That(model["orderNumber"], Is.Not.Null); Assert.That(model.OrderNumber, Is.Not.Null); model.OrderNumber = null; Assert.That(model["orderNumber"], Is.Null);
                model.ZoneNumber = "0001/A"; Assert.That(model["zoneNumber"], Is.Not.Null); Assert.That(model.ZoneNumber, Is.Not.Null); model.ZoneNumber = null; Assert.That(model["zoneNumber"], Is.Null);
                model.PostingDate = long.MaxValue; Assert.That(model["postingDate"], Is.Not.Null); Assert.That(model.PostingDate, Is.Not.Null); model.PostingDate = null; Assert.That(model["postingDate"], Is.Null);
                model.DocumentText = "original documentText"; Assert.That(model["documentText"], Is.Not.Null); Assert.That(model.DocumentText, Is.Not.Null); model.DocumentText = null; Assert.That(model["documentText"], Is.Null);
                model.Locale = "original locale"; Assert.That(model["locale"], Is.Not.Null); Assert.That(model.Locale, Is.Not.Null); model.Locale = null; Assert.That(model["locale"], Is.Null);
                model.Currency = "original currency"; Assert.That(model["currency"], Is.Not.Null); Assert.That(model.Currency, Is.Not.Null); model.Currency = null; Assert.That(model["currency"], Is.Null);
                model.ExchangeRate = -123456789.123456789m; Assert.That(model["exchangeRate"], Is.Not.Null); Assert.That(model.ExchangeRate, Is.Not.Null); model.ExchangeRate = null; Assert.That(model["exchangeRate"], Is.Null);
                model.Reference = "original reference"; Assert.That(model["reference"], Is.Not.Null); Assert.That(model.Reference, Is.Not.Null); model.Reference = null; Assert.That(model["reference"], Is.Null);
                model.TaxCountryCodeIso2 = "original taxCountryCodeIso2"; Assert.That(model["taxCountryCodeIso2"], Is.Not.Null); Assert.That(model.TaxCountryCodeIso2, Is.Not.Null); model.TaxCountryCodeIso2 = null; Assert.That(model["taxCountryCodeIso2"], Is.Null);
                model.IsIntercompany = false; Assert.That(model["isIntercompany"], Is.Not.Null); Assert.That(model.IsIntercompany, Is.Not.Null); model.IsIntercompany = null; Assert.That(model["isIntercompany"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new OutgoingInvoiceUpdateRequest(null)));
            }
            {
                var model = new OutgoingInvoiceUpdatePositionsRequest();
                model.CopyProductsToPositions = false; Assert.That(model["copyProductsToPositions"], Is.Not.Null); Assert.That(model.CopyProductsToPositions, Is.Not.Null); model.CopyProductsToPositions = null; Assert.That(model["copyProductsToPositions"], Is.Null);
                model.Positions = new JArray(); Assert.That(model["positions"], Is.Not.Null); Assert.That(model.Positions, Is.Not.Null); model.Positions = null; Assert.That(model["positions"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new OutgoingInvoiceUpdatePositionsRequest(null)));
            }
            {
                var model = new OutgoingInvoiceUpdatePositionRequest();
                model.TypeDisplay = "original typeDisplay"; Assert.That(model["typeDisplay"], Is.Not.Null); Assert.That(model.TypeDisplay, Is.Not.Null); model.TypeDisplay = null; Assert.That(model["typeDisplay"], Is.Null);
                model.Number = "0001/A"; Assert.That(model["number"], Is.Not.Null); Assert.That(model.Number, Is.Not.Null); model.Number = null; Assert.That(model["number"], Is.Null);
                model.Name = "original name"; Assert.That(model["name"], Is.Not.Null); Assert.That(model.Name, Is.Not.Null); model.Name = null; Assert.That(model["name"], Is.Null);
                model.Description = "original description"; Assert.That(model["description"], Is.Not.Null); Assert.That(model.Description, Is.Not.Null); model.Description = null; Assert.That(model["description"], Is.Null);
                model.Quantity = -123456789.123456789m; Assert.That(model["quantity"], Is.Not.Null); Assert.That(model.Quantity, Is.Not.Null); model.Quantity = null; Assert.That(model["quantity"], Is.Null);
                model.Unit = "original unit"; Assert.That(model["unit"], Is.Not.Null); Assert.That(model.Unit, Is.Not.Null); model.Unit = null; Assert.That(model["unit"], Is.Null);
                model.SingleAmount = -123456789.123456789m; Assert.That(model["singleAmount"], Is.Not.Null); Assert.That(model.SingleAmount, Is.Not.Null); model.SingleAmount = null; Assert.That(model["singleAmount"], Is.Null);
                model.Account = "0001/A"; Assert.That(model["account"], Is.Not.Null); Assert.That(model.Account, Is.Not.Null); model.Account = null; Assert.That(model["account"], Is.Null);
                model.VatKey = "original vatKey"; Assert.That(model["vatKey"], Is.Not.Null); Assert.That(model.VatKey, Is.Not.Null); model.VatKey = null; Assert.That(model["vatKey"], Is.Null);
                model.Discount = -123456789.123456789m; Assert.That(model["discount"], Is.Not.Null); Assert.That(model.Discount, Is.Not.Null); model.Discount = null; Assert.That(model["discount"], Is.Null);
                model.FreeText1 = "original freeText1"; Assert.That(model["freeText1"], Is.Not.Null); Assert.That(model.FreeText1, Is.Not.Null); model.FreeText1 = null; Assert.That(model["freeText1"], Is.Null);
                model.FreeText2 = "original freeText2"; Assert.That(model["freeText2"], Is.Not.Null); Assert.That(model.FreeText2, Is.Not.Null); model.FreeText2 = null; Assert.That(model["freeText2"], Is.Null);
                model.SingleCostAmount = -123456789.123456789m; Assert.That(model["singleCostAmount"], Is.Not.Null); Assert.That(model.SingleCostAmount, Is.Not.Null); model.SingleCostAmount = null; Assert.That(model["singleCostAmount"], Is.Null);
                model.ValutaDate = long.MaxValue; Assert.That(model["valutaDate"], Is.Not.Null); Assert.That(model.ValutaDate, Is.Not.Null); model.ValutaDate = null; Assert.That(model["valutaDate"], Is.Null);
                model.Dimension1 = "original dimension_1"; Assert.That(model["dimension_1"], Is.Not.Null); Assert.That(model.Dimension1, Is.Not.Null); model.Dimension1 = null; Assert.That(model["dimension_1"], Is.Null);
                model.Dimension2 = "original dimension_2"; Assert.That(model["dimension_2"], Is.Not.Null); Assert.That(model.Dimension2, Is.Not.Null); model.Dimension2 = null; Assert.That(model["dimension_2"], Is.Null);
                model.Dimension3 = "original dimension_3"; Assert.That(model["dimension_3"], Is.Not.Null); Assert.That(model.Dimension3, Is.Not.Null); model.Dimension3 = null; Assert.That(model["dimension_3"], Is.Null);
                model.Dimension4 = "original dimension_4"; Assert.That(model["dimension_4"], Is.Not.Null); Assert.That(model.Dimension4, Is.Not.Null); model.Dimension4 = null; Assert.That(model["dimension_4"], Is.Null);
                model.Dimension5 = "original dimension_5"; Assert.That(model["dimension_5"], Is.Not.Null); Assert.That(model.Dimension5, Is.Not.Null); model.Dimension5 = null; Assert.That(model["dimension_5"], Is.Null);
                model.Dimension6 = "original dimension_6"; Assert.That(model["dimension_6"], Is.Not.Null); Assert.That(model.Dimension6, Is.Not.Null); model.Dimension6 = null; Assert.That(model["dimension_6"], Is.Null);
                model.Dimension7 = "original dimension_7"; Assert.That(model["dimension_7"], Is.Not.Null); Assert.That(model.Dimension7, Is.Not.Null); model.Dimension7 = null; Assert.That(model["dimension_7"], Is.Null);
                model.Dimension8 = "original dimension_8"; Assert.That(model["dimension_8"], Is.Not.Null); Assert.That(model.Dimension8, Is.Not.Null); model.Dimension8 = null; Assert.That(model["dimension_8"], Is.Null);
                model.Dimension9 = "original dimension_9"; Assert.That(model["dimension_9"], Is.Not.Null); Assert.That(model.Dimension9, Is.Not.Null); model.Dimension9 = null; Assert.That(model["dimension_9"], Is.Null);
                model.Dimension10 = "original dimension_10"; Assert.That(model["dimension_10"], Is.Not.Null); Assert.That(model.Dimension10, Is.Not.Null); model.Dimension10 = null; Assert.That(model["dimension_10"], Is.Null);
                model.AsIfSold = false; Assert.That(model["asIfSold"], Is.Not.Null); Assert.That(model.AsIfSold, Is.Not.Null); model.AsIfSold = null; Assert.That(model["asIfSold"], Is.Null);
                model.DateFrom = long.MaxValue; Assert.That(model["dateFrom"], Is.Not.Null); Assert.That(model.DateFrom, Is.Not.Null); model.DateFrom = null; Assert.That(model["dateFrom"], Is.Null);
                model.DateTo = long.MaxValue; Assert.That(model["dateTo"], Is.Not.Null); Assert.That(model.DateTo, Is.Not.Null); model.DateTo = null; Assert.That(model["dateTo"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new OutgoingInvoiceUpdatePositionRequest(null)));
            }
            {
                var model = new OutgoingInvoiceUpdateCustomFieldRequest();
                model.CustomFieldName = "original customFieldName"; Assert.That(model["customFieldName"], Is.Not.Null); Assert.That(model.CustomFieldName, Is.Not.Null); model.CustomFieldName = null; Assert.That(model["customFieldName"], Is.Null);
                model.CustomFieldValue = new JValue("original text"); Assert.That(model["customFieldValue"], Is.Not.Null); Assert.That(model.CustomFieldValue, Is.Not.Null); model.CustomFieldValue = null; Assert.That(model["customFieldValue"], Is.Null);
                Assert.That(model.Properties(), Is.Empty); Assert.Throws<ArgumentNullException>((Action)(() => new OutgoingInvoiceUpdateCustomFieldRequest(null)));
            }
        }
        private static Task<ApiResponse<JToken>> Invoke(OutgoingInvoiceApi api, string selection, OutgoingInvoiceUpdateRequest request, int mode, CancellationToken token)
        {
            return mode == 0 ? Task.FromResult(api.UpdateOutgoingInvoiceWithHttpInfo(selection, request)) : mode == 1 ? api.UpdateOutgoingInvoiceAsyncWithHttpInfo(selection, request) : api.UpdateOutgoingInvoiceAsyncWithHttpInfo(selection, request, token);
        }
        private static OutgoingInvoiceApi Api(HttpClient http) => new OutgoingInvoiceApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-invoice-responsibility-token" });
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
