using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
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
    public class BodylessSalesConversionTests
    {
        private const string Selector = " 0001 / Ä & user@example.invalid ";
        private const string JsonResult = "{\"future\":true,\"id\":9223372036854775807,\"amount\":1.234567890123456789,\"date\":\"2026-10-13T00:00:00Z\"}";
        private static readonly int[] Routes = Enumerable.Range(0, 21).ToArray();
        private static readonly string[] Names = { "ConvertOfferToDispatchWithResult", "ConvertOfferToOpportunityWithResult", "ConvertOfferToOrderWithResult", "ConvertOfferToOutgoingInvoiceWithResult", "ConvertOpportunityToDispatchWithResult", "ConvertOpportunityToOfferWithResult", "ConvertOpportunityToOrderWithResult", "ConvertOpportunityToOutgoingInvoiceWithResult", "ConvertOrderToDispatchWithResult", "ConvertOrderToOfferWithResult", "ConvertOrderToOpportunityWithResult", "ConvertOrderToOutgoingInvoiceWithResult", "ConvertInvoiceToDispatchWithResult", "ConvertInvoiceToOfferWithResult", "ConvertInvoiceToOpportunityWithResult", "ConvertInvoiceToOrderWithResult", "ConvertRecurringInvoiceToDispatchWithResult", "ConvertRecurringInvoiceToOfferWithResult", "ConvertRecurringInvoiceToOpportunityWithResult", "ConvertRecurringInvoiceToOrderWithResult", "ConvertRecurringInvoiceToOutgoingInvoiceWithResult" };
        private static readonly string[] Sources = { "Offer", "Offer", "Offer", "Offer", "Opportunity", "Opportunity", "Opportunity", "Opportunity", "Order", "Order", "Order", "Order", "OutgoingInvoice", "OutgoingInvoice", "OutgoingInvoice", "OutgoingInvoice", "RecurringInvoice", "RecurringInvoice", "RecurringInvoice", "RecurringInvoice", "RecurringInvoice" };
        private static readonly string[] Paths = { "/rest/offer/{number}/convertToDispatch", "/rest/offer/{number}/convertToOpportunity", "/rest/offer/{number}/convertToOrder", "/rest/offer/{number}/convertToOutgoingInvoice", "/rest/opportunity/{number}/convertToDispatch", "/rest/opportunity/{number}/convertToOffer", "/rest/opportunity/{number}/convertToOrder", "/rest/opportunity/{number}/convertToOutgoingInvoice", "/rest/order/{number}/convertToDispatch", "/rest/order/{number}/convertToOffer", "/rest/order/{number}/convertToOpportunity", "/rest/order/{number}/convertToOutgoingInvoice", "/rest/outgoinginvoice/{number}/convertToDispatch", "/rest/outgoinginvoice/{number}/convertToOffer", "/rest/outgoinginvoice/{number}/convertToOpportunity", "/rest/outgoinginvoice/{number}/convertToOrder", "/rest/recurringinvoice/{number}/convertToDispatch", "/rest/recurringinvoice/{number}/convertToOffer", "/rest/recurringinvoice/{number}/convertToOpportunity", "/rest/recurringinvoice/{number}/convertToOrder", "/rest/recurringinvoice/{number}/convertToOutgoingInvoice" };
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-conversion-token", ClientNumber = "isolated-client" };
            switch (Sources[route])
            {
                case "Offer": return new OfferApi(config);
                case "Opportunity": return new OpportunityApi(config);
                case "Order": return new OrderApi(config);
                case "OutgoingInvoice": return new OutgoingInvoiceApi(config);
                case "RecurringInvoice": return new RecurringInvoiceApi(config);
                default: throw new ArgumentOutOfRangeException(nameof(route));
            }
        }
        private static Configuration Config(object api) => (Configuration)api.GetType().GetProperty("Configuration").GetValue(api);
        private static async Task<object> Call(object api, int route, int mode, bool info, string selector = Selector, CancellationToken token = default(CancellationToken))
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var types = mode == 2 ? new[] { typeof(string), typeof(CancellationToken) } : new[] { typeof(string) };
            var args = mode == 2 ? new object[] { selector, token } : new object[] { selector };
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The complete conversion form " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }

        [Test]
        public async Task BodylessConversionRetainsCompleteResultAndState([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(false, true)] bool future)
        {
            var body = future ? JsonResult : ""; var status = 200;
            var handler = new Handler(token => Task.FromResult(Response(status, body)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var result = await Call(api, route, mode, info);
                Assert.That(handler.Path, Is.EqualTo(Paths[route].Replace("{number}", Uri.EscapeDataString(Selector))));
                Assert.That(handler.Query, Is.Empty); Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.HasBody, Is.False);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-conversion-token")); Assert.That(handler.Accept, Is.EqualTo("application/json"));
                JToken data;
                if (info)
                {
                    var response = (ApiResponse<JToken>)result; data = response.Data;
                    Assert.That(response.StatusCode, Is.EqualTo(status)); Assert.That(response.RawJsonContent, Is.EqualTo(body));
                    Assert.That(response.Headers["x-fixture"], Is.EqualTo("first,second"));
                }
                else data = (JToken)result;
                if (future) { Assert.That(data.Value<bool>("future"), Is.True); Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue)); Assert.That(data["date"].Type, Is.EqualTo(JTokenType.String)); Assert.That(data.Value<decimal>("amount"), Is.EqualTo(1.234567890123456789m)); }
                else Assert.That(data, Is.Null);
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-conversion-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client")); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OriginalInternalNumbersRemainOneSegment([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values("00001", "USER@Example.invalid", " 00/Ä ")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            {
                await Call(Api(http, route), route, mode, info, selector);
                Assert.That(handler.Path, Is.EqualTo(Paths[route].Replace("{number}", Uri.EscapeDataString(selector)))); Assert.That(handler.Query, Is.Empty); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidSelectorsPreventHttp([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode, [Values(null, "", " ", ".", "..")] string selector)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true, selector); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void ServerFailuresAndOriginalFactoriesRemainAuthoritative([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode, [Values(400, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original conversion diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Call(api, route, mode, true); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original conversion diagnostic"));
                var expected = new InvalidOperationException("original hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Call(api, route, mode, true); })), Is.SameAs(expected));
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-conversion-token")); Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void MalformedSuccessfulJsonRemainsVisible([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "{invalid")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Call(Api(http, route), route, mode, true); }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void PreCancellationPreventsConversionAndStateChanges([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var api = Api(http, route); cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Call(api, route, 2, info, token: cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero); Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-conversion-token"));
            }
        }

        [Test]
        public async Task ActiveCancellationReachesIoWithoutReplayOrLocalReset([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, ""); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var api = Api(http, route); var operation = Call(api, route, 2, info, token: cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1)); Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-conversion-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client"));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public void HistoricalVoidResultsAndLocalStateRemainUnchanged([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var name = Names[route].Replace("WithResult", "") + "WithHttpInfo";
                var method = api.GetType().GetMethod(name, new[] { typeof(string) });
                var response = (ApiResponse<object>)method.Invoke(api, new object[] { Selector });
                Assert.That(response.StatusCode, Is.EqualTo(200)); Assert.That(response.Data, Is.Null); Assert.That(handler.Calls, Is.EqualTo(1));
                Assert.That(Config(api).AccessToken, Is.EqualTo("isolated-conversion-token")); Assert.That(Config(api).ClientNumber, Is.EqualTo("isolated-client"));
            }
        }

        [Test]
        public async Task UndefinedSuccessShapesRemainComplete([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode, [Values("9223372036854775807", "[]", "[{\"future\":true}]", "\"server-result\"", "null", "")] string body)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, body)));
            using (var http = new HttpClient(handler))
            {
                var response = (ApiResponse<JToken>)await Call(Api(http, route), route, mode, true);
                Assert.That(response.RawJsonContent, Is.EqualTo(body)); Assert.That(response.StatusCode, Is.EqualTo(200));
                if (body == "") Assert.That(response.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(response.Data, JToken.Parse(body)), Is.True);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private static HttpResponseMessage Response(int status, string body)
        {
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", "first"); response.Content.Headers.TryAddWithoutValidation("x-fixture", "second"); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal string Path, Query, Authorization, Accept; internal bool HasBody; internal HttpMethod Method;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Path = request.RequestUri.AbsolutePath; Query = request.RequestUri.Query; Method = request.Method;
                Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); HasBody = request.Content != null;
                return send(token);
            }
        }
    }
}

