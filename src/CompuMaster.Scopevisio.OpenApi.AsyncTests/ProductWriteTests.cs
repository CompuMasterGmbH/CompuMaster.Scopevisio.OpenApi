using CompuMaster.Scopevisio.OpenApi.Api;
using NUnit.Framework;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using System;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ProductWriteTests
    {
        [TestCase("CreateProductWithResult")]
        [TestCase("UpdateProductWithResult")]
        public void CompleteCurrentProductWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ProductApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentProductWriteRequestIsAvailable()
        {
            Assert.That(typeof(ProductApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ProductWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["ProductForm"]["properties"];
        }
        public static IEnumerable<TestCaseData> Fields()
        {
            foreach (var field in Schema().Properties())
                foreach (var creating in new[] { false, true })
                    foreach (var mode in new[] { 0, 1, 2 })
                        yield return new TestCaseData(field.Name, creating, mode);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownFieldKindsAreCheckedBeforeHttp(string field, bool creating, int mode)
        {
            string kind = Schema()[field].Value<string>("type"); var request = Valid(creating);
            request[field] = kind == "string" ? new JValue(false) : kind == "boolean" ? new JValue("false") : kind == "integer" ? new JValue(7.25m) : (JToken)new JObject();
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, mode, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request = Valid(creating); request.Name = "before"; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(creating ? 201 : 200, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.Name = "after"; request["future"]["value"] = 2;
                    Assert.That(JObject.Parse(handler.Body).Value<string>("name"), Is.EqualTo("before")); Assert.That(JObject.Parse(handler.Body)["future"].Value<int>("value"), Is.EqualTo(1));
                }
                finally { release.TrySetResult(true); }
                await operation;
            }
        }

        [Test]
        public async Task CompleteResultFormsRetainOriginalJsonWithoutInventedValues([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode, [Values("{}", "[]", "null", "true", "{\"warnings\":[\"inspect\"]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, json)));
            using (var http = new HttpClient(handler))
            {
                var data = await InvokeData(Api(http), -1L, Valid(creating), creating, mode); Assert.That(data.ToString(Formatting.None), Is.EqualTo(json)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void OriginalFailureAndFactoryPrecedeJson([Values(false, true)] bool creating, [Values(0, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original product diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original product diagnostic"));
                var expected = new InvalidOperationException("custom product hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateProductWithResult" : "UpdateProductWithResult")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); })), Is.SameAs(expected)); Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInventedSuccess([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "{invalid")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, Valid(creating), creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void PreCancelledWriteHasNoHttpEffects([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            { cancel.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, Valid(creating), creating, 2, cancel.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task ActiveWriteCancellationDoesNotReplay([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, "{}"); });
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), 1L, Valid(creating), creating, 2, cancel.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancel.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancel.Cancel(); }
            }
        }


        [Test]
        public async Task HttpInfoEmptyResultsDoNotInventData([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "")));
            using (var http = new HttpClient(handler)) { var info = await Invoke(Api(http), 1L, Valid(creating), creating, mode, CancellationToken.None); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty); }
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        private static string PropertyName(string field) => char.ToUpperInvariant(field[0]) + field.Substring(1);

        [Test]
        public void GroupSixFormsAndRequestAreAvailable()
        {
            Assert.That(typeof(ProductApi).GetMethods().Count(m => m.Name.StartsWith("CreateProductGroupWithResult", StringComparison.Ordinal)), Is.EqualTo(6));
            Assert.That(typeof(ProductApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ProductGroupWriteRequest"), Is.Not.Null);
        }

        [Test]
        public async Task All26WritableFieldsKeepOriginalWireValues([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var request = new ProductWriteRequest(); var expected = new JObject();
            Assert.That(Schema().Count, Is.EqualTo(27)); Assert.That(Schema()["type"].Value<bool>("readOnly"), Is.True);
            Assert.That(typeof(ProductWriteRequest).GetProperties().Count(p => p.DeclaringType == typeof(ProductWriteRequest)), Is.EqualTo(26));
            foreach (var field in Schema().Properties().Where(p => !p.Value.Value<bool>("readOnly")))
            {
                string kind = field.Value.Value<string>("type"); object value = kind == "string" ? (object)("  original / " + field.Name) : kind == "boolean" ? (object)false : kind == "integer" ? (object)int.MinValue : 123456789.123456789m;
                var property = typeof(ProductWriteRequest).GetProperty(PropertyName(field.Name)); Assert.That(property, Is.Not.Null, field.Name); property.SetValue(request, value); expected[field.Name] = new JValue(value);
            }
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, Result)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-product-write-token" }; var api = new ProductApi(config);
                var info = await Invoke(api, long.MaxValue, request, creating, mode, CancellationToken.None);
                Assert.That(api.Configuration, Is.SameAs(config)); Assert.That(config.HttpClient, Is.SameAs(http));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(creating ? "/rest/product/new" : "/rest/product/9223372036854775807")); Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-product-write-token")); Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(expected.Count, Is.EqualTo(26)); Assert.That(handler.Body, Is.EqualTo(expected.ToString(Formatting.None))); Assert.That(info.RawJsonContent, Is.EqualTo(Result)); Assert.That(info.StatusCode, Is.EqualTo(creating ? 201 : 200));
                Assert.That(info.Headers["X-Product-Fixture"], Is.EqualTo("retained")); Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json")); Assert.That(info.Data.Value<decimal>("fraction"), Is.EqualTo(123456789.123456789m)); Assert.That(info.Data["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(config.AccessToken, Is.EqualTo("isolated-product-write-token")); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ProductCreationRequiresExplicitUnitAndAcceptsAdditionalCatalogueText([Values(0, 1, 2)] int mode, [Values("null-body", "missing", "null-unit", "unit", "empty-unit", "whitespace")] string selection)
        {
            var request = selection == "null-body" ? null : new ProductWriteRequest();
            if (selection == "null-unit") request["unit"] = JValue.CreateNull();
            if (selection == "unit") request.Unit = "extra-unit-from-catalogue";
            if (selection == "empty-unit") request.Unit = "";
            if (selection == "whitespace") request.Unit = "  ";
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (request == null) Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); }));
                else if (selection == "missing" || selection == "null-unit") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); }));
                else { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(JObject.Parse(handler.Body).Count, Is.EqualTo(1)); }
            }
            Assert.That(handler.Calls, Is.EqualTo(selection == "unit" || selection == "empty-unit" || selection == "whitespace" ? 1 : 0));
        }

        [Test]
        public async Task SparseUpdateNullAndOmissionDoNotPopulateCatalogueOrPriceDefaults([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var result = await Invoke(api, 0L, null, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.Null); Assert.That(result.Data, Is.Null);
                var request = new ProductWriteRequest { Locked = false, RevenueAccount0 = "000123", Unit = "", ["future"] = new JObject { ["value"] = false } };
                request.TaxRate = 7; request.TaxRate = null; await Invoke(api, 0L, request, false, mode, CancellationToken.None); Assert.That(request.ContainsKey("taxRate"), Is.False); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                await Invoke(api, 0L, new ProductWriteRequest(), false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo("{}"));
                foreach (var field in Schema().Properties().Where(p => !p.Value.Value<bool>("readOnly"))) request[field.Name] = JValue.CreateNull();
                await Invoke(api, 0L, request, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
            }
            Assert.That(handler.Calls, Is.EqualTo(4));
        }

        [Test]
        public void ReadOnlyTypeIsNeverSent([Values(false, true)] bool creating, [Values("null", "1", "\"Ware\"")] string json)
        {
            var request = Valid(creating); request["type"] = JToken.Parse(json); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public void NonFinitePricesCannotBecomeStringNumbers([Values(false, true)] bool creating, [Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double number)
        {
            var request = Valid(creating); request["singleAmount"] = new JValue(number); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task OriginalNumericJsonIsNotCoerced([Values(false, true)] bool creating, [Values("0", "-12.345678901234567890123456789", "9223372036854775808", "1e100")] string number)
        {
            var request = Valid(creating); request["singleAmount"] = number == "1e100" ? JToken.Parse(number) : JsonConvert.DeserializeObject<JToken>(number, new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });
            var original = request.ToString(Formatting.None); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(original)); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public async Task AccountModeInt32WidthAndInvariantPathArePreserved([Values("-2147483649", "2147483648", "-2147483648", "2147483647", "-1", "2")] string number)
        {
            var request = new ProductWriteRequest { ["revenueAccountMode"] = JToken.Parse(number) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}"))); var previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
                using (var http = new HttpClient(handler))
                {
                    if (number == "-2147483649" || number == "2147483648") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); }));
                    else { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/product/-9223372036854775808")); Assert.That(JObject.Parse(handler.Body)["revenueAccountMode"].ToString(Formatting.None), Is.EqualTo(number)); }
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
            Assert.That(handler.Calls, Is.EqualTo(number == "-2147483649" || number == "2147483648" ? 0 : 1));
        }

        [Test]
        public void ConstructorInputsAreCopiedAndTypedNullOmitsAllWritableFields()
        {
            var original = new JObject { ["future"] = new JObject { ["value"] = 1 } }; var request = new ProductWriteRequest(original); var group = new ProductGroupWriteRequest(original); original["future"]["value"] = 2;
            Assert.That(request["future"].Value<int>("value"), Is.EqualTo(1)); Assert.That(group["future"].Value<int>("value"), Is.EqualTo(1));
            foreach (var field in Schema().Properties().Where(p => !p.Value.Value<bool>("readOnly"))) { request[field.Name] = JValue.CreateNull(); typeof(ProductWriteRequest).GetProperty(PropertyName(field.Name)).SetValue(request, null); Assert.That(request.ContainsKey(field.Name), Is.False); }
            Assert.That(request.Count, Is.EqualTo(1)); group.Name = "temporary"; group.Name = null; Assert.That(group.ContainsKey("name"), Is.False);
            Assert.Throws<ArgumentNullException>((TestDelegate)(() => new ProductWriteRequest(null))); Assert.Throws<ArgumentNullException>((TestDelegate)(() => new ProductGroupWriteRequest(null)));
        }

        [Test]
        public async Task DocumentedProductCreationResponseSurvivesAllSixForms([Values(0, 1, 2, 3, 4, 5)] int form)
        {
            const string json = "{\"status\":2,\"productId\":9223372036854775807,\"location\":\"/product/9223372036854775807\",\"errors\":{\"original\":[\"inspect\"],\"code\":400},\"future\":false}";
            var handler = new Handler(token => Task.FromResult(Response(201, json)));
            using (var http = new HttpClient(handler))
            {
                JToken data;
                if (form < 3) { var info = await Invoke(Api(http), 0L, Valid(true), true, form, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.StatusCode, Is.EqualTo(201)); data = info.Data; }
                else data = await InvokeData(Api(http), 0L, Valid(true), true, form - 3);
                Assert.That(data.Value<long>("productId"), Is.EqualTo(long.MaxValue)); Assert.That(data.Value<int>("status"), Is.EqualTo(2)); Assert.That(data.Value<string>("location"), Is.EqualTo("/product/9223372036854775807")); Assert.That(data["errors"].Value<int>("code"), Is.EqualTo(400)); Assert.That(data["future"].Value<bool>(), Is.False); Assert.That(data["success"], Is.Null);
            }
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public async Task GroupCreationRetainsOriginalNameAndCompleteResult([Values(0, 1, 2, 3, 4, 5)] int form, [Values("", "  ", "original group / 0001")] string name)
        {
            var request = new ProductGroupWriteRequest { Name = name, ["future"] = new JObject { ["value"] = false } }; var handler = new Handler(token => Task.FromResult(Response(201, Result)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); JToken data;
                if (form < 3) { var info = await Group(api, request, form, CancellationToken.None); Assert.That(info.RawJsonContent, Is.EqualTo(Result)); Assert.That(info.StatusCode, Is.EqualTo(201)); Assert.That(info.Headers["X-Product-Fixture"], Is.EqualTo("retained")); data = info.Data; }
                else data = form == 3 ? api.CreateProductGroupWithResult(request) : form == 4 ? await api.CreateProductGroupWithResultAsync(request) : await api.CreateProductGroupWithResultAsync(request, CancellationToken.None);
                Assert.That(data.Value<decimal>("fraction"), Is.EqualTo(123456789.123456789m)); Assert.That(data["date"].Type, Is.EqualTo(JTokenType.String)); Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/productGroup/new")); Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-product-write-token")); Assert.That(handler.Accept, Is.EqualTo("application/json"));
            }
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void GroupCreationRequiresBodyAndNameBeforeHttp([Values(0, 1, 2)] int mode, [Values("null-body", "missing", "null", "kind")] string selection)
        {
            var request = selection == "null-body" ? null : new ProductGroupWriteRequest(); if (selection == "null") request["name"] = JValue.CreateNull(); if (selection == "kind") request["name"] = false;
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
                if (request == null) Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Group(Api(http), request, mode, CancellationToken.None); }));
                else Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Group(Api(http), request, mode, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task GroupEmptySuccessAndFactoryPreserveResponse([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var request = new ProductGroupWriteRequest { Name = "fixture" }; var result = await Group(api, request, mode, CancellationToken.None); Assert.That(result.Data, Is.Null); Assert.That(result.RawJsonContent, Is.Empty);
                var expected = new InvalidOperationException("group hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("CreateProductGroupWithResult")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Group(api, request, mode, CancellationToken.None); })), Is.SameAs(expected));
            }
            Assert.That(handler.Calls, Is.EqualTo(2));
        }

        [Test]
        public async Task GroupNativeCancellationAndSnapshotHaveNoReplay()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(201, "{}"); }); var request = new ProductGroupWriteRequest { Name = "before", ["future"] = new JObject { ["value"] = false } };
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            {
                var operation = Group(Api(http), request, 2, cancel.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.Name = "after"; request["future"]["value"] = true; var sent = JObject.Parse(handler.Body); Assert.That(sent.Value<string>("name"), Is.EqualTo("before")); Assert.That(sent["future"].Value<bool>("value"), Is.False); cancel.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancel.Cancel(); }
            }
        }

        [Test]
        public void GroupPreCancellationStopsBeforeHttp()
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource()) { cancel.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Group(Api(http), new ProductGroupWriteRequest { Name = "fixture" }, 2, cancel.Token); })); }
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public void GroupHttpErrorsRetainOriginalDiagnosticWithoutReplay([Values(0, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original group diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Group(Api(http), new ProductGroupWriteRequest { Name = "fixture" }, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original group diagnostic"));
            }
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void InvalidGroupSuccessJsonIsNotInventedSuccess()
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{invalid")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async () => { await Group(Api(http), new ProductGroupWriteRequest { Name = "fixture" }, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void SinglePrecisionNonFinitePriceIsRejected([Values(float.NaN, float.PositiveInfinity, float.NegativeInfinity)] float number)
        {
            var request = new ProductWriteRequest { ["taxRate"] = new JValue(number) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, false, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        private static Task<ApiResponse<JToken>> Group(ProductApi api, ProductGroupWriteRequest request, int mode, CancellationToken token)
            => mode == 0 ? Task.FromResult(api.CreateProductGroupWithResultWithHttpInfo(request)) : mode == 1 ? api.CreateProductGroupWithResultAsyncWithHttpInfo(request) : api.CreateProductGroupWithResultAsyncWithHttpInfo(request, token);

        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static ProductWriteRequest Valid(bool creating) => creating ? new ProductWriteRequest { Unit = "extra-unit-from-catalogue" } : new ProductWriteRequest();
        private static ProductApi Api(HttpClient http) => new ProductApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-product-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ProductApi api, long id, ProductWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProductWithResultWithHttpInfo(request)) : mode == 1 ? api.CreateProductWithResultAsyncWithHttpInfo(request) : api.CreateProductWithResultAsyncWithHttpInfo(request, token) :
                mode == 0 ? Task.FromResult(api.UpdateProductWithResultWithHttpInfo(id, request)) : mode == 1 ? api.UpdateProductWithResultAsyncWithHttpInfo(id, request) : api.UpdateProductWithResultAsyncWithHttpInfo(id, request, token);
        private static Task<JToken> InvokeData(ProductApi api, long id, ProductWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProductWithResult(request)) : mode == 1 ? api.CreateProductWithResultAsync(request) : api.CreateProductWithResultAsync(request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateProductWithResult(id, request)) : mode == 1 ? api.UpdateProductWithResultAsync(id, request) : api.UpdateProductWithResultAsync(id, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Product-Fixture", "retained"); return response; }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Authorization, Accept, ContentType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            { Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); ContentType = request.Content?.Headers.ContentType?.ToString(); Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token); }
        }
    }
}
