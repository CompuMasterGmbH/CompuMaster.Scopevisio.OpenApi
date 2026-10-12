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
    public class HumanResourceCreationTests
    {
        private const string ResultJson = "{\"status\":201,\"humanResourceId\":9223372036854775807,\"location\":\"future-location\",\"errors\":{\"planning\":[\"inspect even on 201\"]},\"future\":{\"fee\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\"}}";

        [TestCase("CreateHumanResource")]
        [TestCase("CreateHumanResourceWithHttpInfo")]
        [TestCase("CreateHumanResourceAsyncWithHttpInfo")]
        public void PublicCreationWorkflowIsAvailable(string name)
        {
            Assert.That(typeof(HumanResourcesApi).GetMethods().Any(method => method.Name == name), Is.True, "The public creation workflow is absent.");
        }

        [Test]
        public async Task CompleteCreationUsesExactJsonAndRetainsWarnings([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, ResultJson)));
            using (var http = new HttpClient(handler))
            {
                var request = Valid();
                request.Role = "future catalog";
                request.Position = "";
                request.Department = "Applikationsentwicklung";
                request.ProjectBox = "unknown box";
                request.Topic = "Digital Operations";
                request.Location = "München";
                request.Fee = -123456789.123456789m;
                request.AllowMultiple = true;
                request["future"] = new JObject { ["nested"] = new JArray(1, JValue.CreateNull()) };
                var info = await Invoke(Api(http), request, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/humanresource/new"));
                Assert.That(handler.Uri.Query, Is.Empty);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-human-resource-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.That(info.StatusCode, Is.EqualTo(201));
                Assert.That(info.RawJsonContent, Is.EqualTo(ResultJson));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json"));
                Assert.That(info.Headers["Content-Length"], Is.EqualTo(Encoding.UTF8.GetByteCount(ResultJson).ToString()));
                Assert.That(info.Data.Status, Is.EqualTo(201));
                Assert.That(info.Data.HumanResourceId, Is.EqualTo(long.MaxValue));
                Assert.That(info.Data.Location, Is.EqualTo("future-location"));
                Assert.That(info.Data.Errors["planning"][0].Value<string>(), Is.EqualTo("inspect even on 201"));
                Assert.That(info.Data["future"].Value<decimal>("fee"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["future"]["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ConvenienceFormsExposeCompleteResult([Values(0, 1, 2)] int mode)
        {
            using (var http = new HttpClient(new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, ResultJson)))))
            {
                var api = Api(http);
                var data = mode == 0 ? api.CreateHumanResource(Valid()) : mode == 1 ?
                    await api.CreateHumanResourceAsync(Valid()) : await api.CreateHumanResourceAsync(Valid(), CancellationToken.None);
                Assert.That(data.HumanResourceId, Is.EqualTo(long.MaxValue));
                Assert.That(data.Errors["planning"], Is.TypeOf<JArray>());
            }
        }

        [Test]
        public async Task OriginalConflictingDatesAndIdentifiersAreNotConverted([Values("2026-10-15", "15.10.2026", "future date")] string date, [Values(long.MinValue, 0L, long.MaxValue)] long id)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid();
                request.ContactId = id;
                request.ValidFrom = date;
                request.ValidTill = " earlier date ";
                await Invoke(Api(http), request, 2, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<long>("contactId"), Is.EqualTo(id));
                Assert.That(body.Value<string>("validFrom"), Is.EqualTo(date));
                Assert.That(body.Value<string>("validTill"), Is.EqualTo(" earlier date "));
            }
        }

        [Test]
        public async Task TypedNullOmitsOptionalFieldsAndFalseIsExplicit([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid();
                request.Role = "remove";
                request.Role = null;
                request.Fee = 1m;
                request.Fee = null;
                request.AllowMultiple = null;
                await Invoke(Api(http), request, mode, CancellationToken.None);
                Assert.That(JObject.Parse(handler.Body).Properties().Count(), Is.EqualTo(3));
                request.AllowMultiple = false;
                request["role"] = JValue.CreateNull();
                await Invoke(Api(http), request, mode, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body["allowMultiple"].Type, Is.EqualTo(JTokenType.Boolean));
                Assert.That(body.Value<bool>("allowMultiple"), Is.False);
                Assert.That(body["role"].Type, Is.EqualTo(JTokenType.Null));
            }
        }

        private static readonly string[] InvalidRequests = {
            "{}", "{\"contactId\":null}", "{\"contactId\":\"1\"}", "{\"contactId\":1.0}", "{\"contactId\":true}",
            "{\"contactId\":9223372036854775808}", "{\"validFrom\":null}", "{\"validFrom\":\"\"}", "{\"validFrom\":\"  \"}",
            "{\"validFrom\":20261001}", "{\"validTill\":null}", "{\"validTill\":\"\"}", "{\"validTill\":[]}",
            "{\"role\":1}", "{\"position\":false}", "{\"department\":{}}", "{\"projectBox\":[]}", "{\"topic\":1.2}", "{\"location\":true}",
            "{\"allowMultiple\":\"true\"}", "{\"allowMultiple\":1}", "{\"fee\":\"1.2\"}", "{\"fee\":false}", "{\"fee\":[]}"
        };

        [Test]
        public void MalformedKnownRawFieldsFailBeforeTransport([ValueSource(nameof(InvalidRequests))] string changes, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                var request = Valid();
                if (changes == "{}") request.ContactId = null;
                else foreach (var property in JObject.Parse(changes).Properties()) request[property.Name] = property.Value;
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void NullRequestAndNonfiniteFeeFailBeforeTransport([Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            {
                Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), null, mode, CancellationToken.None); }));
                var request = Valid();
                request["fee"] = new JValue(double.NaN);
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), request, mode, CancellationToken.None); }));
                request["fee"] = new JValue(double.PositiveInfinity);
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), request, mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task SnapshotAndCopiedModelsDoNotAliasCallerJson()
        {
            var source = new JObject { ["contactId"] = 1L, ["validFrom"] = "2026-10-01", ["validTill"] = "2026-10-31", ["future"] = new JObject { ["value"] = "original" } };
            var request = new HumanResourceCreationRequest(source);
            source["future"]["value"] = "source changed";
            Assert.That(request["future"].Value<string>("value"), Is.EqualTo("original"));
            var handler = new Handler(token => {
                request.ValidFrom = "changed during transport";
                request["future"]["value"] = "caller changed";
                return Task.FromResult(Response(HttpStatusCode.Created, ResultJson));
            });
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), request, 2, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<string>("validFrom"), Is.EqualTo("2026-10-01"));
                Assert.That(body["future"].Value<string>("value"), Is.EqualTo("original"));
            }
            var original = JObject.Parse("{\"errors\":{\"value\":\"original\"}}");
            var result = new HumanResourceCreationResponse(original);
            original["errors"]["value"] = "changed";
            Assert.That(result.Errors.Value<string>("value"), Is.EqualTo("original"));
        }

        [Test]
        public void ConstructorsRejectNullSources()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => new HumanResourceCreationRequest(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => new HumanResourceCreationResponse(null)));
        }

        [Test]
        public async Task EmptyAndUnknownObjectResultsRetainExactResponse([Values("", "null", "{}", "{\"future\":[]}")] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), Valid(), mode, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "" || json == "null") Assert.That(info.Data, Is.Null);
                else { Assert.That(info.Data.Status, Is.Null); Assert.That(info.Data.HumanResourceId, Is.Null); }
            }
        }

        [Test]
        public void InvalidResponseShapesAreRejectedAfterFactory([Values("[]", "true", "not json")] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, json)));
            using (var http = new HttpClient(handler))
            {
                Assert.CatchAsync<JsonException>((Func<Task>)(async () => { await Invoke(Api(http), Valid(), mode, CancellationToken.None); }));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void OriginalErrorsAndCustomFactoryExceptionsArePreserved([Values(400, 403, 404)] int status, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, "original creation diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, Valid(), mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original creation diagnostic"));
                var expected = new InvalidOperationException("custom human-resource hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo("CreateHumanResource")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, Valid(), mode, CancellationToken.None); })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationPreventsCreation()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Created, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), null, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesCreationTransportWithoutRetry()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.Created, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), Valid(), 2, cancellation.Token);
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

        private static HumanResourceCreationRequest Valid() => new HumanResourceCreationRequest { ContactId = long.MaxValue, ValidFrom = "2026-10-01", ValidTill = "2026-10-31" };
        private static Task<ApiResponse<HumanResourceCreationResponse>> Invoke(HumanResourcesApi api, HumanResourceCreationRequest request, int mode, CancellationToken token)
        {
            return mode == 0 ? Task.FromResult(api.CreateHumanResourceWithHttpInfo(request)) : mode == 1 ?
                api.CreateHumanResourceAsyncWithHttpInfo(request) : api.CreateHumanResourceAsyncWithHttpInfo(request, token);
        }
        private static HumanResourcesApi Api(HttpClient http) => new HumanResourcesApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-human-resource-token" });
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
            internal string Body, Authorization, Accept, ContentType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                ContentType = request.Content?.Headers.ContentType?.ToString();
                return await send(token);
            }
        }
    }
}
