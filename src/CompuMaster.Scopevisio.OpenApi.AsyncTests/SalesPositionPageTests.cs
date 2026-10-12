using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
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
    public class SalesPositionPageTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 7).ToArray();
        private static readonly int[] DatedRoutes = Enumerable.Range(0, 7).ToArray();
        private static readonly string[] Names = { "GetCreditPositionPage", "GetDispatchPositionPage", "GetOfferPositionPage", "GetOpportunityPositionPage", "GetOrderPositionPage", "GetOutgoingInvoicePositionPage", "GetRecurringInvoicePositionPage" };
        private static readonly Type[] ApiTypes = { typeof(CreditApi), typeof(DispatchApi), typeof(OfferApi), typeof(OpportunityApi), typeof(OrderApi), typeof(OutgoingInvoiceApi), typeof(RecurringInvoiceApi) };
        private static readonly string[] Paths = { "/rest/credit/all/positions", "/rest/dispatch/all/positions", "/rest/offer/all/positions", "/rest/opportunity/all/positions", "/rest/order/all/positions", "/rest/outgoinginvoice/all/positions", "/rest/recurringinvoice/all/positions" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-12T01:02:03Z\"}],\"more\":false}";

        [Test]
        public async Task PositionPagesUseExactGetQueryContracts([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http, route), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-position-page-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.EqualTo("?startDate=29.02.2024&endDate=01.03.2024&fields=id,amount & custom&page=0&pageSize=5000"));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["records"][0]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["records"][0]["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalValuesAreOmittedWithoutInventingPaging([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http, route), route, 1, false, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.EqualTo("?startDate=29.02.2024&endDate=01.03.2024"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ExplicitEmptyOptionalFiltersRemainObservable([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[2] = "";
                args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                await Call(Api(http, route), Names[route] + "AsyncWithHttpInfo", args, Types(route, true));
                Assert.That(handler.Uri.Query, Is.EqualTo("?startDate=29.02.2024&endDate=01.03.2024&fields="));
            }
        }

        [Test]
        public void InvalidRequiredDatesFailBeforeHttp([ValueSource(nameof(DatedRoutes))] int route,
            [Values(false, true)] bool end, [Values(null, "", " ", "29.02.2023", "2024-02-29", "31.04.2024")] string date, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[end ? 1 : 0] = date;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http, route), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
                }));
                Assert.That(error.ParamName, Is.EqualTo(end ? "endDate" : "startDate"));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ValidDateOrderIsSentWithoutAnUndocumentedClientRule([ValueSource(nameof(DatedRoutes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[0] = "01.03.2024"; args[1] = "29.02.2024";
                await Call(Api(http, route), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                Assert.That(handler.Uri.Query, Does.StartWith("?startDate=01.03.2024&endDate=29.02.2024"));
            }
        }

        [Test]
        public async Task UnspecifiedReplyShapesAndNullAreRetained([ValueSource(nameof(Routes))] int route, [Values("{}", "[]", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http, route), route, 2, false, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(info.Data.Type, Is.EqualTo(json == "{}" ? JTokenType.Object : json == "[]" ? JTokenType.Array : JTokenType.Null));
            }
        }

        [Test]
        public async Task ServerProfileFailuresAndCustomHookExceptionsArePreserved([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "required reporting profile missing")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http, route);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("required reporting profile missing"));
                var expected = new InvalidOperationException("custom reporting hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; }));
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsEveryDataSourceRequest([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http, route), route, 2, false, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesEveryDataSourceTransport([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http, route), route, 2, false, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public async Task ConvenienceMethodsExposeCompleteJson([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = (JToken)await Call(Api(http, route), Names[route] + (asynchronous ? "Async" : ""), Arguments(route, false), Types(route, false));
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }


        [Test]
        public async Task PagingBoundaryValuesRemainExplicit([ValueSource(nameof(Routes))] int route, [Values(0, int.MaxValue)] int page, [Values(1, 5000)] int pageSize)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[3] = page; args[4] = pageSize;
                await Call(Api(http, route), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                Assert.That(handler.Uri.Query, Is.EqualTo("?startDate=29.02.2024&endDate=01.03.2024&page=" + page + "&pageSize=" + pageSize));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void InvalidPageFailsBeforeHttp([ValueSource(nameof(Routes))] int route, [Values(-1, int.MinValue)] int page, [Values(0, 2)] int mode)
        {
            InvalidPaging(route, 3, page, mode, "page");
        }

        [Test]
        public void InvalidPageSizeFailsBeforeHttp([ValueSource(nameof(Routes))] int route, [Values(0, -1, 5001, int.MaxValue)] int pageSize, [Values(0, 2)] int mode)
        {
            InvalidPaging(route, 4, pageSize, mode, "pageSize");
        }

        private static void InvalidPaging(int route, int index, int value, int mode, string parameter)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[index] = value;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentOutOfRangeException>((Func<Task>)(async () => { await Call(Api(http, route), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2)); }));
                Assert.That(error.ParamName, Is.EqualTo(parameter)); Assert.That(handler.Calls, Is.Zero);
            }
        }

        private static async Task<ApiResponse<JToken>> Invoke(object api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            return new object[] { "29.02.2024", "01.03.2024", explicitValues ? "id,amount & custom" : null, explicitValues ? (object)0 : null, explicitValues ? (object)5000 : null };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var types = new[] { typeof(string), typeof(string), typeof(string), typeof(int?), typeof(int?) };
            return cancellation ? types.Concat(new[] { typeof(CancellationToken) }).ToArray() : types;
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The public workflow " + name + " is absent.");
            object value;
            try { value = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static object Api(HttpClient http, int route) => Activator.CreateInstance(ApiTypes[route], new object[] { new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-position-page-token" } });
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            return response;
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
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
