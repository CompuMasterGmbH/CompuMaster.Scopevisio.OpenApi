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
    public class ProductDetailReadTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 1).ToArray();
        private static readonly string[] Names = { "GetProductDetails" };
        private static readonly string[] Paths = { "/rest/product/9223372036854775807" };
        private const string Fields = "id,amount & custom";
        private const string Json = "{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-13T01:02:03Z\",\"future\":{\"flag\":false}}";

        [Test]
        public async Task InfoUsesCurrentProductReadContract([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, route), route, mode, true, Arguments(route, Fields, false), CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-product-detail-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(Query(handler, "fields"), Is.EqualTo(Fields));
                Assert.That(Query(handler, "formatValues"), Is.EqualTo(route == 0 ? "false" : null));
                Assert.That(handler.Uri.Query.Split('&').Length, Is.EqualTo(route == 0 ? 2 : 1));
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
        public async Task FormattingChoicePreservesOmissionFalseAndTrue([Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int choice)
        {
            bool? format = choice == 0 ? (bool?)null : choice == 2;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http, 0), 0, mode, true, Arguments(0, "", format), CancellationToken.None);
                Assert.That(Query(handler, "fields"), Is.EqualTo(""));
                Assert.That(Query(handler, "formatValues"), Is.EqualTo(choice == 0 ? null : choice == 1 ? "false" : "true"));
                Assert.That(handler.Uri.Query, Does.Not.Contain("count").And.Not.Contain("offset"));
            }
        }

        [Test]
        public async Task SignedInt64SelectionsUseInvariantText([ValueSource(nameof(Routes))] int route, [Values(long.MinValue, 0L, long.MaxValue)] long selection)
        {
            var previous = CultureInfo.CurrentCulture;
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone(); culture.NumberFormat.NegativeSign = "NEG";
            try
            {
                CultureInfo.CurrentCulture = culture;
                var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
                using (var http = new HttpClient(handler))
                {
                    var args = Arguments(route, null, null); args[0] = selection;
                    await Invoke(Api(http, route), route, 2, true, args, CancellationToken.None);
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route].Replace(long.MaxValue.ToString(CultureInfo.InvariantCulture), selection.ToString(CultureInfo.InvariantCulture))));
                    Assert.That(handler.Uri.Query, Is.Empty);
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
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
        public void HistoricalDetailResultsRemainUnchanged()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { HttpClient = http, BasePath = "https://product-detail.invalid/rest", AccessToken = "isolated-product-detail-token" };
                Assert.That(new ProductApi(config).GetProductWithHttpInfo(1L).Data, Is.Null);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-product-detail-token"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task FormattingAndExtendedFieldsRemainIndependent([Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int formatChoice, [Values(0, 1, 2)] int extendedChoice)
        {
            bool? format = formatChoice == 0 ? (bool?)null : formatChoice == 2;
            bool? extended = extendedChoice == 0 ? (bool?)null : extendedChoice == 2;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{\"id\":1}")));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, 0), 0, mode, true, new object[] { 1L, null, format, extended }, CancellationToken.None);
                Assert.That(Query(handler, "formatValues"), Is.EqualTo(formatChoice == 0 ? null : formatChoice == 1 ? "false" : "true"));
                Assert.That(Query(handler, "listExtended"), Is.EqualTo(extendedChoice == 0 ? null : extendedChoice == 1 ? "false" : "true"));
                Assert.That(Query(handler, "fields"), Is.Null);
                Assert.That(info.Data["name"], Is.Null, "Missing product fields must not be defaulted.");
                Assert.That(info.Data["billable"], Is.Null);
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CompleteProductAndFormattedValuesAreNotConverted([Values(0, 1, 2)] int mode)
        {
            const string product = "{\"id\":9223372036854775807,\"externalModifieddTs\":9223372036854775807,\"number\":\"00001\",\"singleAmount\":\"1.234,56\",\"customAmount30\":123456789.123456789,\"customDate30\":\"13.10.2026\",\"customBoolean30\":false,\"packingUnit\":\"\",\"future\":{\"field\":null}}";
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, product)));
            using (var http = new HttpClient(handler))
            {
                var info = (ApiResponse<JToken>)await Invoke(Api(http, 0), 0, mode, true, new object[] { long.MaxValue, null, true, true }, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(product));
                Assert.That(info.Data.Value<long>("externalModifieddTs"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data.Value<string>("number"), Is.EqualTo("00001"));
                Assert.That(info.Data["singleAmount"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(info.Data.Value<string>("singleAmount"), Is.EqualTo("1.234,56"));
                Assert.That(info.Data.Value<decimal>("customAmount30"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data.Value<string>("customDate30"), Is.EqualTo("13.10.2026"));
                Assert.That(info.Data.Value<bool>("customBoolean30"), Is.False);
                Assert.That(info.Data.Value<string>("packingUnit"), Is.Empty);
                Assert.That(info.Data["future"]["field"].Type, Is.EqualTo(JTokenType.Null));
            }
        }

        private static object[] Arguments(int route, string fields, bool? formatting)
        {
            if (route == 0) return new object[] { long.MaxValue, fields, formatting, null };
            return new object[] { long.MaxValue, fields };
        }
        private static Type[] Types(int route)
        {
            if (route == 0) return new[] { typeof(long), typeof(string), typeof(bool?), typeof(bool?) };
            return new[] { typeof(long), typeof(string) };
        }
        private static async Task<object> Invoke(object api, int route, int mode, bool info, object[] args, CancellationToken token)
        {
            var name = Names[route] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : "");
            var types = Types(route);
            if (mode == 2) { args = args.Concat(new object[] { token }).ToArray(); types = types.Concat(new[] { typeof(CancellationToken) }).ToArray(); }
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The additive product workflow " + name + " is absent.");
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
            var config = new Configuration { HttpClient = http, BasePath = "https://product-detail.invalid/rest", AccessToken = "isolated-product-detail-token" };
            return new ProductApi(config);
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
                Assert.That(Uri.Host, Is.EqualTo("product-detail.invalid"));
                Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
