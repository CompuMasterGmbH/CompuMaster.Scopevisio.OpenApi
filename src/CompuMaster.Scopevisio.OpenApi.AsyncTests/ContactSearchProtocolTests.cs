using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ContactSearchProtocolTests
    {
        private const string Search = "{\"page\":2,\"pageSize\":250,\"fields\":[\"id\",\"firstname\",\"customAmount3\"],\"formatValues\":false,\"search\":[{\"field\":\"customText7\",\"value\":null,\"operator\":\"is null\"},{\"field\":\"customAmount3\",\"value\":123456789.123456789,\"operator\":\"equal\"}]}";
        private const string Records = "{\"records\":[{\"id\":4294967296,\"firstname\":\"Fixture\",\"lastname\":null,\"customDateTime4\":1600852271000,\"customAmount3\":12.34}],\"page\":2}";

        [TestCase(false), TestCase(true)]
        public async Task SearchFilterIsAnUnchangedJsonObjectAndResponseKeepsTypedRecords(bool asynchronous)
        {
            var handler = new FakeHandler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            {
                var api = CreateApi(transport);
                var response = asynchronous ? await api.GetContactsAsyncWithHttpInfo(Search) : api.GetContactsWithHttpInfo(Search);
                Assert.That(handler.LastBody, Is.EqualTo(Search));
                Assert.That(JToken.Parse(handler.LastBody).Type, Is.EqualTo(JTokenType.Object));
                Assert.That(handler.LastMethod, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.LastPath, Is.EqualTo("/rest/contacts"));
                Assert.That(handler.LastAuthorization, Is.EqualTo("Bearer isolated-fixture-token"));
                Assert.That(handler.LastContentType, Is.EqualTo("application/json"));
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(response.RawJsonContent, Is.EqualTo(Records));
                Assert.That(response.Data.Items.Count, Is.EqualTo(1));
                Assert.That(response.Data.Items[0].Id, Is.EqualTo(4294967296L));
                Assert.That(response.Data.Items[0].FirstName, Is.EqualTo("Fixture"));
                Assert.That(response.Data.Items[0].LastName, Is.Null);
                Assert.That(response.Data.Items[0].Value<long>("customDateTime4"), Is.EqualTo(1600852271000L));
                Assert.That(response.Data.Items[0].Value<decimal>("customAmount3"), Is.EqualTo(12.34m));
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task MissingFilterKeepsAnEmptyBodyAndEmptyTypedRecords(bool asynchronous)
        {
            var handler = new FakeHandler(token => Task.FromResult(Response(HttpStatusCode.OK, "{\"records\":[]}")));
            using (var transport = new HttpClient(handler))
            {
                var api = CreateApi(transport);
                var response = asynchronous ? await api.GetContactsAsyncWithHttpInfo() : api.GetContactsWithHttpInfo();
                Assert.That(string.IsNullOrEmpty(handler.LastBody), Is.True);
                Assert.That(response.Data.Items, Is.Empty);
            }
        }

        [Test]
        public async Task CancellationReachesTheActiveContactRequestWithoutChangingAuthorization()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new FakeHandler(async token =>
            {
                entered.SetResult(true);
                await Task.Delay(Timeout.Infinite, token);
                return Response(HttpStatusCode.OK, Records);
            });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var api = CreateApi(transport);
                var pending = api.GetContactsAsyncWithHttpInfo(Search, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, pending, Task.Delay(5000)), Is.SameAs(entered.Task), "The HTTP request did not enter the fixture transport.");
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(pending, Task.Delay(5000)), Is.SameAs(pending), "The active request ignored cancellation.");
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await pending; }));
                    Assert.That(api.Configuration.AccessToken, Is.EqualTo("isolated-fixture-token"));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public void PreCancelledContactRequestDoesNotReachTheTransport()
        {
            var handler = new FakeHandler(token => Task.FromResult(Response(HttpStatusCode.OK, Records)));
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                var api = CreateApi(transport);
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await api.GetContactsAsyncWithHttpInfo(null, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task ExistingExceptionFactoryReceivesAnErrorBeforeResponseParsing(bool asynchronous)
        {
            var handler = new FakeHandler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "permission denied")));
            using (var transport = new HttpClient(handler))
            {
                var expected = new InvalidOperationException("isolated fixture factory");
                var api = CreateApi(transport);
                api.ExceptionFactory = (name, response) =>
                {
                    Assert.That(name, Is.EqualTo("GetContacts"));
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    return expected;
                };
                Exception actual = null;
                try
                {
                    if (asynchronous) await api.GetContactsAsyncWithHttpInfo(Search);
                    else api.GetContactsWithHttpInfo(Search);
                }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.SameAs(expected));
            }
        }

        [Test]
        public void LegacyAsyncSignatureRetainsItsOptionalNullFilterAndTypedReturn()
        {
            var method = typeof(ContactApi).GetMethod("GetContactsAsyncWithHttpInfo", new[] { typeof(string) });
            Assert.That(method, Is.Not.Null);
            Assert.That(method.ReturnType, Is.EqualTo(typeof(Task<ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.RecordCollection<CompuMaster.Scopevisio.OpenApi.Model.Contact>>>)));
            Assert.That(method.GetParameters()[0].Name, Is.EqualTo("body"));
            Assert.That(method.GetParameters()[0].IsOptional, Is.True);
            Assert.That(method.GetParameters()[0].DefaultValue, Is.Null);
        }

        private static ContactApi CreateApi(HttpClient transport)
        {
            return new ContactApi(new Configuration { HttpClient = transport, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-fixture-token" });
        }

        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        }

        private sealed class FakeHandler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal string LastBody;
            internal string LastPath;
            internal string LastAuthorization;
            internal string LastContentType;
            internal HttpMethod LastMethod;
            internal FakeHandler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++;
                LastBody = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                LastPath = request.RequestUri.AbsolutePath;
                LastMethod = request.Method;
                LastAuthorization = request.Headers.Authorization == null ? null : request.Headers.Authorization.ToString();
                LastContentType = request.Content == null ? null : request.Content.Headers.ContentType.MediaType;
                return await send(token);
            }
        }
    }
}
