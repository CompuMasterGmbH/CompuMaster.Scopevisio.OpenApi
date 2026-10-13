using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
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
    public class RecurringConversionTests
    {
        private const string JsonResult = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"date\":\"2026-10-13T00:00:00Z\",\"future\":null}],\"extra\":{\"hasNext\":true}}";
        private static readonly string[] Names = { "ConvertOfferToRecurringInvoiceWithResult", "ConvertOrderToRecurringInvoiceWithResult", "ConvertOpportunityToRecurringInvoiceWithResult", "ConvertInvoiceToRecurringInvoiceWithResult" };
        private const string Selector = " 00001 / Ä ";
        private static readonly string[] Paths = { "/rest/offer/{number}/convertToRecurringInvoice", "/rest/order/{number}/convertToRecurringInvoice", "/rest/opportunity/{number}/convertToRecurringInvoice", "/rest/outgoinginvoice/{number}/convertToRecurringInvoice" };
        private static Type RequestType
        {
            get { var type = typeof(OfferApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.RecurringInvoiceConversionRequest"); Assert.That(type, Is.Not.Null); return type; }
        }
        private static JObject Request(JObject source)
        {
            if (source == null) return null;
            try { return (JObject)RequestType.GetConstructor(new[] { typeof(JObject) }).Invoke(new object[] { source }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-recurring-conversion-token" };
            if (route == 0) return new OfferApi(config);
            if (route == 1) return new OrderApi(config);
            if (route == 2) return new OpportunityApi(config);
            return new OutgoingInvoiceApi(config);
        }
        private static async Task<object> Call(object api, int route, int mode, bool info, JObject query, CancellationToken token = default(CancellationToken), string selector = Selector)
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var method = api.GetType().GetMethod(name, mode == 2 ? new[] { typeof(string), RequestType, typeof(CancellationToken) } : new[] { typeof(string), RequestType });
            Assert.That(method, Is.Not.Null, "The complete recurring-conversion search " + name + " is absent.");
            object result;
            var request = query == null || RequestType.IsInstanceOfType(query) ? query : Request(query);
            try { result = method.Invoke(api, mode == 2 ? new object[] { selector, request, token } : new object[] { selector, request }); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static JObject Query()
        {
            return new JObject { ["validFrom"] = long.MinValue, ["validTo"] = long.MaxValue, ["nextInvoiceDate"] = 0L, ["frequency"] = "monthly",
                ["future"] = new JObject { ["amount"] = 1.234567890123456789m, ["date"] = "2026-10-13", ["id"] = long.MinValue } };
        }
        [Test]
        public async Task OriginalOptionalSearchAndCompleteResultsAreRetained([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(0, 1, 2)] int choice)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            var query = choice == 0 ? null : choice == 1 ? new JObject() : Query();
            var original = query?.ToString(Formatting.None);
            using (var http = new HttpClient(handler))
            {
                if (choice == 1)
                {
                    Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, info, query); }));
                    Assert.That(handler.Calls, Is.Zero); return;
                }
                var result = await Call(Api(http, route), route, mode, info, query);
                Assert.That(handler.Path, Is.EqualTo(Paths[route].Replace("{number}", Uri.EscapeDataString(Selector))));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Body, Is.EqualTo(original));
                Assert.That(handler.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-recurring-conversion-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                if (choice != 0) Assert.That(handler.ContentType, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data;
                    Assert.That(response.RawJsonContent, Is.EqualTo(JsonResult));
                    Assert.That(response.StatusCode, Is.EqualTo(200));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["records"][0]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(data["records"][0]["future"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(data["extra"].Value<bool>("hasNext"), Is.True);
                Assert.That(query?.ToString(Formatting.None), Is.EqualTo(original));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }
        [Test]
        public async Task CountUnknownAndEmptyRepresentationsRemainDistinct([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values("9223372036854775807", "[]", "{\"future\":true}", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, json)));
            using (var http = new HttpClient(handler))
            {
                var query = Query();
                var result = (ApiResponse<JToken>)await Call(Api(http, route), route, mode, true, query);
                Assert.That(result.RawJsonContent, Is.EqualTo(json));
                Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None)));
                if (json == "") Assert.That(result.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(result.Data, JToken.Parse(json)), Is.True);
            }
        }
        [Test]
        public void OriginalErrorsAndFactoriesRemainAuthoritative([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original recurring-conversion diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, route, mode, true, null); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original recurring-conversion diagnostic"));
                var expected = new InvalidOperationException("original hook diagnostic");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, route, mode, true, null); })), Is.SameAs(expected));
            }
        }
        [Test]
        public void MalformedJsonAndDecimalOverflowAreReported([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values("{", "{\"amount\":1e100}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, json)));
            using (var http = new HttpClient(handler))
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, null); }));
        }
        [Test]
        public void PreCancellationPreventsHttp([Values(0, 1, 2, 3)] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(Api(http, route), route, 2, info, null, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }
        [Test]
        public async Task ActiveCancellationReachesHttp([Values(0, 1, 2, 3)] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, JsonResult); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var task = Call(Api(http, route), route, 2, info, Query(), cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, task, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel(); Assert.That(await Task.WhenAny(task, Task.Delay(5000)), Is.SameAs(task));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await task; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }
        [Test]
        public async Task SearchBodyIsSerializedBeforeHttpAdmission([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            var query = Request(Query()); var original = query.ToString(Formatting.None);
            var handler = new Handler(token => { query.RemoveAll(); query["changed"] = true; return Task.FromResult(Response(200, JsonResult)); });
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, true, query);
                Assert.That(handler.Body, Is.EqualTo(original));
                Assert.That(query.Value<bool>("changed"), Is.True);
            }
        }
        [Test]
        public async Task SearchValuesUseInvariantJsonWithoutClientClamping([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            var previous = System.Globalization.CultureInfo.CurrentCulture;
            var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NegativeSign = "NEG"; culture.NumberFormat.NumberDecimalSeparator = ",";
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
                using (var http = new HttpClient(handler))
                {
                    await Call(Api(http, route), route, mode, true, Query());
                    Assert.That(handler.Body, Does.Contain("-9223372036854775808").And.Contain("9223372036854775807").And.Contain("1.234567890123456789"));
                }
            }
            finally { System.Globalization.CultureInfo.CurrentCulture = previous; }
        }
        [Test]
        public void HistoricalOptionalFormAndDefaultsRemainUnchanged([Values(0, 1, 2, 3)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var method = api.GetType().GetMethod(Names[route].Replace("WithResult", "") + "WithHttpInfo",
                    new[] { typeof(string), typeof(ConvertToRecurringInvoiceForm) });
                var response = (ApiResponse<object>)method.Invoke(api, new object[] { Selector, null });
                Assert.That(response.Data, Is.Null); Assert.That(handler.Body, Is.Null);
                var legacy = new ConvertToRecurringInvoiceForm();
                response = (ApiResponse<object>)method.Invoke(api, new object[] { Selector, legacy });
                Assert.That(response.Data, Is.Null);
                Assert.That(legacy.ValidFrom, Is.Zero); Assert.That(legacy.ValidTo, Is.Zero); Assert.That(legacy.NextInvoiceDate, Is.Zero);
                Assert.That((int)legacy.Frequency, Is.Zero);
                Assert.That(JObject.Parse(handler.Body).Value<long>("validFrom"), Is.Zero);
                Assert.That(((Configuration)api.GetType().GetProperty("Configuration").GetValue(api)).AccessToken, Is.EqualTo("isolated-recurring-conversion-token"));
            }
        }

        [Test]
        public async Task AllSevenExplicitFrequenciesRemainOriginal([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values("daily", "weekly", "monthly", "bimonthly", "quarterly", "half-yearly", "yearly")] string frequency)
        {
            var query = Query(); query["frequency"] = frequency;
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, true, query);
                Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None))); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidRequiredDatesFailBeforeIo([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values("validFrom", "validTo", "nextInvoiceDate")] string field, [Values(0, 1, 2, 3, 4)] int kind)
        {
            var query = Query();
            if (kind == 0) query.Remove(field);
            else query[field] = kind == 1 ? JValue.CreateNull() : kind == 2 ? new JValue("1") : kind == 3 ? new JValue(1.5m) : JToken.Parse("9223372036854775808");
            Invalid(route, mode, query);
        }

        [Test]
        public void InvalidFrequencyAndNonfiniteFieldsFailBeforeIo([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(0, 1, 2, 3, 4, 5, 6)] int kind)
        {
            var query = Query();
            if (kind == 0) query.Remove("frequency");
            else if (kind == 6) query["future"] = new JArray(double.NaN);
            else query["frequency"] = kind == 1 ? JValue.CreateNull() : kind == 2 ? new JValue(0) : kind == 3 ? new JValue("") : kind == 4 ? new JValue("MONTHLY") : new JValue("unknown");
            Invalid(route, mode, query);
        }

        [Test]
        public void InvalidNumbersFailBeforeIo([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode, [Values(null, "", " ", ".", "..")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, Query(), selector: selector); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        private static void Invalid(int route, int mode, JObject query)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, query); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void TypedSchedulingStartsUnsetAndReusesHistoricalFrequencyEnum()
        {
            var request = (JObject)Activator.CreateInstance(RequestType);
            Assert.That(request.ToString(Formatting.None), Is.EqualTo("{}"));
            foreach (var name in new[] { "ValidFrom", "ValidTo", "NextInvoiceDate" })
            {
                var property = RequestType.GetProperty(name); Assert.That(property.PropertyType, Is.EqualTo(typeof(long?)));
                property.SetValue(request, 0L); Assert.That(property.GetValue(request), Is.EqualTo(0L)); property.SetValue(request, null);
            }
            var frequency = RequestType.GetProperty("Frequency"); Assert.That(frequency.PropertyType, Is.EqualTo(typeof(ConvertToRecurringInvoiceForm.FrequencyEnum?)));
            frequency.SetValue(request, ConvertToRecurringInvoiceForm.FrequencyEnum.HalfYearly);
            Assert.That(request.Value<string>("frequency"), Is.EqualTo("half-yearly"));
            frequency.SetValue(request, null); Assert.That(request.ToString(Formatting.None), Is.EqualTo("{}"));
            var original = Query(); var copied = Request(original); original.RemoveAll(); Assert.That(copied.ContainsKey("future"), Is.True);
        }

        [Test]
        public async Task IndependentEpochBoundsRemainUnconvertedWithoutOrderingDefaults([Values(0, 1, 2, 3)] int route, [Values(0, 1, 2)] int mode)
        {
            var query = Query(); query["validFrom"] = long.MaxValue; query["validTo"] = long.MinValue; query["nextInvoiceDate"] = long.MaxValue;
            var handler = new Handler(token => Task.FromResult(Response(200, JsonResult)));
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, true, query);
                Assert.That(handler.Body, Is.EqualTo(query.ToString(Formatting.None))); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void UndefinedHistoricalEnumValueDoesNotBecomeFrequencyDefault([Values(0, 1, 2, 3)] int route)
        {
            var request = Request(Query()); RequestType.GetProperty("Frequency").SetValue(request, (ConvertToRecurringInvoiceForm.FrequencyEnum)0);
            Invalid(route, 2, request);
        }

        private static HttpResponseMessage Response(int status, string body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", "first"); response.Content.Headers.TryAddWithoutValidation("x-fixture", "second"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal string Path, Query, Body, Accept, Authorization, ContentType; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid"));
                Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                ContentType = request.Content?.Headers.ContentType?.MediaType;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
