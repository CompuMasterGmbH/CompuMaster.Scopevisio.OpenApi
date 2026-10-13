using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Globalization;
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
    public class SalesPositionDetailReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 7).ToArray();
        private static readonly string[] Names = { "GetOfferPositionsDetails", "GetOrderPositionsDetails", "GetDispatchPositionsDetails", "GetOutgoingInvoicePositionsDetails", "GetCreditPositionsDetails", "GetRecurringInvoicePositionsDetails", "GetOpportunityPositionsDetails" };
        private static readonly string[] Paths = { "/rest/offer/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/order/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/dispatch/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/outgoinginvoice/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/credit/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/recurringinvoice/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions", "/rest/opportunity/%2000001%20%2F%20%C3%84%20%26%20custom%20/positions" };
        private const string Selection = " 00001 / Ä & custom ";
        private const string Fields = "id,amount & custom";
        private const string Json = "{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-13T01:02:03Z\",\"future\":{\"flag\":false}}";

        [Test]
        public async Task InfoUsesCurrentPositionReadContract([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, route), route, mode, true, Arguments(route, Fields, false), CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-sales-position-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(Query(handler, "fields"), Is.EqualTo(Fields));
                Assert.That(Query(handler, "formatValues"), Is.Null);
                Assert.That(handler.Uri.Query.Split('&').Length, Is.EqualTo(1));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data.Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data["future"].Value<bool>("flag"), Is.False);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ConvenienceFormsReturnCompleteJson([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = (JToken)await Invoke(Api(http, route), route, mode, false, Arguments(route, null, null), CancellationToken.None);
                Assert.That(data.Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(data["future"].Value<bool>("flag"), Is.False);
                Assert.That(handler.Uri.Query, Is.Empty);
            }
        }

        [Test]
        public async Task UnknownSuccessShapesRemainComplete([ValueSource(nameof(Routes))] int route, [Values("{}", "[]", "null", "", "{\"records\":[]}", "\"future\"", "false")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, route), route, 2, true, Arguments(route, null, null), CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(JToken.DeepEquals(info.Data, JToken.Parse(json)), Is.True);
            }
        }

        [Test]
        public void AmbiguousServerDiagnosticsArePreserved([ValueSource(nameof(Routes))] int route, [Values(400, 403, 404)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original not-found or permission diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, mode, true, Arguments(route, null, null), CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original not-found or permission diagnostic"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void ExceptionFactoryRunsBeforeJsonParsing([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "not JSON")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route); var expected = new InvalidOperationException("original caller hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                var error = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, route, mode, true, Arguments(route, null, null), CancellationToken.None); }));
                Assert.That(error, Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsHttp([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                cancel.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, 2, info, Arguments(route, null, null), cancel.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesNativeHttp([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http, route), route, 2, true, Arguments(route, null, null), cancel.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancel.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancel.Cancel(); }
            }
        }

        [Test]
        public void HistoricalMethodsKeepTheirOriginalData()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { HttpClient = http, BasePath = "https://sales-position.invalid/rest", AccessToken = "isolated-sales-position-token" };
                Assert.That(new OfferApi(config).GetOfferPositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(new OrderApi(config).GetOrderPositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(new DispatchApi(config).GetDispatchPositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(new OutgoingInvoiceApi(config).GetOutgoingInvoicePositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(new CreditApi(config).GetCreditPositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(new RecurringInvoiceApi(config).GetRecurringInvoicePositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-sales-position-token"));
                Assert.That(new OpportunityApi(config).GetOpportunityPositionsWithHttpInfo("00001").Data, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(7));
            }
        }

        [Test]
        public void ExplicitDocumentSelectionsAreRequired([ValueSource(nameof(Routes))] int route, [Values(null, "", " ", ".", "..")] string selection, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, mode, true, new object[] { selection, null, null, null }, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task EmptyFieldSelectionIsRetained([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http, route), route, 2, true, new object[] { "00001", "", null, null }, CancellationToken.None);
                Assert.That(Query(handler, "fields"), Is.Empty);
                Assert.That(Query(handler, "formatValues"), Is.Null);
                Assert.That(handler.Body, Is.Null);
            }
        }


        [Test]
        public async Task IndependentPagingSelectionsUseInvariantInt32Values(
            [ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode,
            [Values(false, true)] bool info, [Values(null, 0, int.MinValue, int.MaxValue)] int? page,
            [Values(null, 0, int.MinValue, int.MaxValue)] int? pageSize)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                culture.NumberFormat.NegativeSign = "minus";
                CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
                using (var http = new HttpClient(handler))
                {
                    await Invoke(Api(http, route), route, mode, info, new object[] { Selection, Fields, page, pageSize }, CancellationToken.None);
                    Assert.That(Query(handler, "fields"), Is.EqualTo(Fields));
                    Assert.That(Query(handler, "page"), Is.EqualTo(page?.ToString(CultureInfo.InvariantCulture)));
                    Assert.That(Query(handler, "pageSize"), Is.EqualTo(pageSize?.ToString(CultureInfo.InvariantCulture)));
                    Assert.That(handler.Uri.Query.TrimStart('?').Split('&').Length, Is.EqualTo(1 + (page.HasValue ? 1 : 0) + (pageSize.HasValue ? 1 : 0)));
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                    Assert.That(handler.Body, Is.Null);
                    Assert.That(handler.Calls, Is.EqualTo(1), "One explicit page must never trigger automatic pagination.");
                }
            }
            finally { CultureInfo.CurrentCulture = originalCulture; }
        }

        [Test]
        public async Task DocumentedMaximumAndLargerRequestsRemainServerAuthoritative(
            [ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode,
            [Values(false, true)] bool info, [Values(5000, 5001)] int size)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http, route), route, mode, info, new object[] { "00001", null, 0, size }, CancellationToken.None);
                Assert.That(Query(handler, "page"), Is.EqualTo("0"));
                Assert.That(Query(handler, "pageSize"), Is.EqualTo(size.ToString(CultureInfo.InvariantCulture)));
                Assert.That(Query(handler, "fields"), Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private static object[] Arguments(int route, string fields, bool? ignoredFormatting) => new object[] { Selection, fields, null, null };
        private static Type[] Types(int route) => new[] { typeof(string), typeof(string), typeof(int?), typeof(int?) };
        private static async Task<object> Invoke(object api, int route, int mode, bool info, object[] args, CancellationToken token)
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var types = Types(route);
            if (mode == 2) { args = args.Concat(new object[] { token }).ToArray(); types = types.Concat(new[] { typeof(CancellationToken) }).ToArray(); }
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The additive sales-position workflow " + name + " is absent.");
            object value;
            try { value = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static string Query(Handler handler, string name)
        {
            foreach (var part in handler.Uri.Query.TrimStart('?').Split('&'))
            {
                var pair = part.Split(new[] { '=' }, 2);
                if (Uri.UnescapeDataString(pair[0]) == name) return pair.Length == 2 ? Uri.UnescapeDataString(pair[1].Replace("+", " ")) : "";
            }
            return null;
        }
        private static object Api(HttpClient http, int route)
        {
            var config = new Configuration { HttpClient = http, BasePath = "https://sales-position.invalid/rest", AccessToken = "isolated-sales-position-token" };
            var groups = new[] { "Offer", "Order", "Dispatch", "OutgoingInvoice", "Credit", "RecurringInvoice", "Opportunity" };
            return Activator.CreateInstance(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Api." + groups[route] + "Api", true), new object[] { config });
        }
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" }); return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization, Accept;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                token.ThrowIfCancellationRequested(); Calls++; Uri = request.RequestUri;
                Assert.That(Uri.Host, Is.EqualTo("sales-position.invalid"));
                Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
