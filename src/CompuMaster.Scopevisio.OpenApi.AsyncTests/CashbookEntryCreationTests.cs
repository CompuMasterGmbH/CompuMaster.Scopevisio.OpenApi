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
    public class CashbookEntryCreationTests
    {
        private const string JsonResult = "{\"created\":{\"id\":9223372036854775807,\"date\":\"2026-10-13T00:00:00Z\",\"amount\":1.234567890123456789},\"future\":true}";
        private static Type RequestType
        {
            get { var t = typeof(CashbookApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.CashbookEntryCreationRequest"); Assert.That(t, Is.Not.Null, "The explicit cashbook creation request is absent."); return t; }
        }
        private static JObject Request(JObject original)
        {
            try { return (JObject)RequestType.GetConstructor(new[] { typeof(JObject) }).Invoke(new object[] { original }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static JObject Selection(bool full = false)
        {
            var value = new JObject { ["cashbookId"] = long.MinValue, ["documentDate"] = long.MaxValue };
            if (full)
            {
                value["externalDocumentNumber"] = " 0001 / Ä "; value["freeText"] = ""; value["debitAmount"] = 0m; value["creditAmount"] = -1.234567890123456789m;
                value["impersonalAccountNumber"] = "001360"; value["personalAccountNumber"] = "00070000"; value["notice"] = JValue.CreateNull();
                value["dimensions"] = new JArray(new JObject { ["dimensionNumber"] = long.MinValue, ["dimensionEntryNumber"] = long.MaxValue, ["dimensionEntryName"] = " New / Ä ", ["future"] = true });
                value["future"] = new JObject { ["date"] = "2026-10-13", ["null"] = JValue.CreateNull() };
            }
            return value;
        }
        private static CashbookApi Api(HttpClient http) => new CashbookApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-entry-token" });
        private static async Task<object> Call(CashbookApi api, JObject request, int mode, bool info, CancellationToken token = default(CancellationToken))
        {
            var name = "CreateCashbookEntryWithResult" + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = typeof(CashbookApi).GetMethod(name, mode == 2 ? new[] { RequestType, typeof(CancellationToken) } : new[] { RequestType });
            Assert.That(method, Is.Not.Null, "The complete cashbook creation " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, mode == 2 ? new object[] { request, token } : new object[] { request }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        [Test]
        public async Task AllFormsPreserveExplicitEntrySelectionsAndComplete201Results([Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(false, true)] bool full)
        {
            var original = Selection(full); var expected = original.ToString(Formatting.None); var request = Request(original); original.RemoveAll();
            var handler = new Handler(token => { request.RemoveAll(); return Task.FromResult(Response(201, JsonResult)); });
            using (var http = new HttpClient(handler))
            {
                var result = await Call(Api(http), request, mode, info);
                Assert.That(handler.Body, Is.EqualTo(expected)); Assert.That(handler.Path, Is.EqualTo("/rest/cashbookentry/new"));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-entry-token")); Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data;
                    Assert.That(response.StatusCode, Is.EqualTo(201)); Assert.That(response.RawJsonContent, Is.EqualTo(JsonResult));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                Assert.That(data["created"].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["created"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(data["created"].Value<decimal>("amount"), Is.EqualTo(1.234567890123456789m));
                Assert.That(data.Value<bool>("future"), Is.True); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public void TypedPropertiesOmitUnsetValuesAndCopyDimensions()
        {
            var request = (JObject)Activator.CreateInstance(RequestType);
            Assert.That(request.ToString(Formatting.None), Is.EqualTo("{}"));
            foreach (var name in new[] { "CashbookId", "DocumentDate" })
            {
                var p = RequestType.GetProperty(name); Assert.That(p.PropertyType, Is.EqualTo(typeof(long?))); p.SetValue(request, 0L);
            }
            foreach (var name in new[] { "DebitAmount", "CreditAmount" })
            {
                var p = RequestType.GetProperty(name); Assert.That(p.PropertyType, Is.EqualTo(typeof(decimal?))); p.SetValue(request, 0m); Assert.That(request.Value<decimal>(name == "DebitAmount" ? "debitAmount" : "creditAmount"), Is.Zero); p.SetValue(request, null);
            }
            foreach (var name in new[] { "ExternalDocumentNumber", "FreeText", "ImpersonalAccountNumber", "PersonalAccountNumber", "Notice" })
            {
                var p = RequestType.GetProperty(name); Assert.That(p.PropertyType, Is.EqualTo(typeof(string))); p.SetValue(request, " original "); Assert.That(p.GetValue(request), Is.EqualTo(" original ")); p.SetValue(request, null);
            }
            var array = (JArray)Selection(true)["dimensions"]; var dimensions = RequestType.GetProperty("Dimensions"); dimensions.SetValue(request, array);
            array.RemoveAll(); Assert.That(((JArray)dimensions.GetValue(request)).Count, Is.EqualTo(1));
            var returned = (JArray)dimensions.GetValue(request); returned.RemoveAll(); Assert.That(((JArray)dimensions.GetValue(request)).Count, Is.EqualTo(1));
            dimensions.SetValue(request, null);
            Assert.That(request.Properties().Count(), Is.EqualTo(2));
        }
        [Test]
        public void InvalidRequiredFieldsFailBeforeHttp([Values("cashbookId", "documentDate")] string field, [Values(0, 1, 2, 3, 4)] int invalid, [Values(0, 1, 2)] int mode)
        {
            var json = Selection();
            if (invalid == 0) json.Remove(field);
            else json[field] = invalid == 1 ? JValue.CreateNull() : invalid == 2 ? new JValue("1") : invalid == 3 ? new JValue(1.5m) : JToken.Parse("9223372036854775808");
            AssertInvalid(json, mode);
        }
        [Test]
        public void ReadOnlyAndInvalidOptionalFieldsFailBeforeHttp([Values(0, 1, 2)] int mode, [Values(0, 1, 2, 3)] int invalid)
        {
            var json = Selection();
            if (invalid == 0) json["internalDocumentNumber"] = JValue.CreateNull();
            if (invalid == 1) json["freeText"] = 5;
            if (invalid == 2) json["debitAmount"] = "1";
            if (invalid == 3) json["dimensions"] = new JObject();
            AssertInvalid(json, mode);
        }
        [Test]
        public void NonfiniteNumbersFailBeforeHttp([Values(0, 1, 2)] int mode, [Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double number, [Values(false, true)] bool nested)
        {
            var json = Selection(); json[nested ? "future" : "creditAmount"] = nested ? (JToken)new JArray(new JObject { ["number"] = number }) : new JValue(number);
            AssertInvalid(json, mode);
        }
        [Test]
        public void InvalidDimensionSelectionsFailBeforeHttp([Values(0, 1, 2)] int mode, [Values(0, 1, 2, 3, 4)] int invalid)
        {
            var json = Selection(); var entry = new JObject { ["dimensionNumber"] = 0L, ["dimensionEntryNumber"] = 0L };
            if (invalid == 0) json["dimensions"] = new JArray(JValue.CreateNull());
            if (invalid == 1) { entry.Remove("dimensionNumber"); json["dimensions"] = new JArray(entry); }
            if (invalid == 2) { entry["dimensionEntryNumber"] = "1"; json["dimensions"] = new JArray(entry); }
            if (invalid == 3) { entry["dimensionNumber"] = JToken.Parse("9223372036854775808"); json["dimensions"] = new JArray(entry); }
            if (invalid == 4) { entry["dimensionEntryName"] = false; json["dimensions"] = new JArray(entry); }
            AssertInvalid(json, mode);
        }
        private static void AssertInvalid(JObject json, int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http), Request(json), mode, true); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task UndefinedCreationRepresentationsRemainDistinct([Values(0, 1, 2)] int mode, [Values("[]", "7", "null", "", "{\"future\":true}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, json)));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<JToken>)await Call(Api(http), Request(Selection()), mode, true);
                Assert.That(result.StatusCode, Is.EqualTo(201)); Assert.That(result.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(result.Data, Is.Null); else Assert.That(JToken.DeepEquals(result.Data, JToken.Parse(json)), Is.True);
            }
        }
        [Test]
        public void OriginalServerAndFactoryFailuresAreRetained([Values(0, 1, 2)] int mode, [Values(400, 403, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original entry diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var request = Request(Selection());
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, request, mode, true); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original entry diagnostic"));
                var expected = new InvalidOperationException("original hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("CreateCashbookEntryWithResult")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, request, mode, true); })), Is.SameAs(expected));
            }
        }
        [Test]
        public void PreCancellationAndNullRequestPreventHttp([Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, JsonResult)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Call(Api(http), null, 2, info); }));
                cancellation.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http), Request(Selection()), 2, info, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesHttpWithoutReplay([Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(201, JsonResult); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Call(Api(http), Request(Selection()), 2, info, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, task, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel(); Assert.That(await Task.WhenAny(task, Task.Delay(5000)), Is.SameAs(task));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await task; })); Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }
        [Test]
        public void TypedDimensionRequestsPreserveSelectionsAndUnknownCopiedFields()
        {
            var type = typeof(CashbookApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.CashbookEntryDimensionRequest");
            Assert.That(type, Is.Not.Null);
            var original = new JObject { ["future"] = new JObject { ["date"] = "2026-10-13" } };
            var request = (JObject)type.GetConstructor(new[] { typeof(JObject) }).Invoke(new object[] { original });
            original["future"]["date"] = "changed"; Assert.That(request["future"].Value<string>("date"), Is.EqualTo("2026-10-13"));
            foreach (var name in new[] { "DimensionNumber", "DimensionEntryNumber" })
            {
                var property = type.GetProperty(name); Assert.That(property.PropertyType, Is.EqualTo(typeof(long?)));
                property.SetValue(request, long.MaxValue); Assert.That(property.GetValue(request), Is.EqualTo(long.MaxValue)); property.SetValue(request, null);
            }
            var text = type.GetProperty("DimensionEntryName"); text.SetValue(request, " original "); Assert.That(text.GetValue(request), Is.EqualTo(" original ")); text.SetValue(request, null);
            Assert.That(request.Properties().Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task ExplicitZeroFullInt64AndEmptyDimensionsRemainUnchanged([Values(0, 1, 2)] int mode, [Values(long.MinValue, 0L, long.MaxValue)] long value)
        {
            var json = new JObject { ["cashbookId"] = value, ["documentDate"] = value, ["dimensions"] = new JArray() };
            var handler = new Handler(token => Task.FromResult(Response(201, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http), Request(json), mode, true);
                Assert.That(handler.Body, Is.EqualTo(json.ToString(Formatting.None)));
            }
        }
        [Test]
        public void MalformedOrOverflowingSuccessJsonRetainsParserFailure([Values(0, 1, 2)] int mode, [Values("{", "{\"amount\":1e100}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, json)));
            using (var http = new HttpClient(handler))
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http), Request(Selection()), mode, true); }));
        }
        [Test]
        public async Task DisabledFactoryPreservesOriginalErrorJsonAndStatus([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(500, "{\"originalError\":true}")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); api.ExceptionFactory = null;
                var response = (ApiResponse<JToken>)await Call(api, Request(Selection()), mode, true);
                Assert.That(response.StatusCode, Is.EqualTo(500)); Assert.That(response.Data.Value<bool>("originalError"), Is.True);
                Assert.That(response.RawJsonContent, Is.EqualTo("{\"originalError\":true}")); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public void LegacyCreationNullDataAndDefaultAmountsRemainUnchanged()
        {
            var handler = new Handler(token => Task.FromResult(Response(201, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var form = new CashbookEntryForm(cashbookId: 0, documentDate: 0);
                Assert.That(api.CreateCashbookEntryWithHttpInfo(form).Data, Is.Null);
                var json = JObject.Parse(handler.Body); Assert.That(json.Value<decimal>("debitAmount"), Is.Zero); Assert.That(json.Value<decimal>("creditAmount"), Is.Zero);
                Assert.That(api.Configuration.AccessToken, Is.EqualTo("isolated-entry-token"));
            }
        }
        private static HttpResponseMessage Response(int status, string json)
        {
            var r = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            r.Headers.TryAddWithoutValidation("X-Fixture", "first"); r.Content.Headers.TryAddWithoutValidation("x-fixture", "second"); return r;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal string Path, Query, Body, Authorization, Accept, ContentType; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method;
                Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); ContentType = request.Content?.Headers.ContentType?.MediaType;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
