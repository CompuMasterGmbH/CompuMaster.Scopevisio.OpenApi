using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ApiExceptionCauseTests
    {
        [Test]
        public async Task TransportFailuresRetainTheExactOriginalCauseAndExistingFields([Values(false, true)] bool structured,
            [Values(0, 1, 2)] int mode, [Values(0, 1, 2)] int kind)
        {
            Exception cause = kind == 0 ? (Exception)new HttpRequestException("isolated DNS/socket failure") :
                kind == 1 ? (Exception)new AuthenticationException("isolated TLS failure", new InvalidOperationException("certificate detail")) :
                new TimeoutException("isolated transport timeout");
            var handler = new Handler(token => Task.FromException<HttpResponseMessage>(cause));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Read(api, structured, mode, CancellationToken.None); }));
                Assert.That(error.InnerException, Is.SameAs(cause));
                Assert.That(error.ErrorCode, Is.Zero);
                Assert.That((object)error.ErrorContent, Is.EqualTo(cause.Message));
                Assert.That(error.Message, Is.EqualTo("Error calling " + (structured ? "SearchContacts" : "GetContacts") + ": " + cause.Message));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [TestCase(0, false), TestCase(0, true)]
        [TestCase(400, false), TestCase(400, true)]
        [TestCase(503, false), TestCase(503, true)]
        public void DefaultFactoryKeepsServerContentMessagesAndOptionalInnerCause(int status, bool withCause)
        {
            var cause = withCause ? new InvalidOperationException("underlying transport diagnostic") : null;
            var response = new RestResponse { StatusCode = (HttpStatusCode)status, Content = "server diagnostic",
                ErrorMessage = "transport diagnostic", ErrorException = cause };
            var error = (ApiException)Configuration.DefaultExceptionFactory("ReadFixture", response);
            Assert.That(error.InnerException, Is.SameAs(cause));
            Assert.That(error.ErrorCode, Is.EqualTo(status));
            Assert.That((object)error.ErrorContent, Is.EqualTo(status == 0 ? "transport diagnostic" : "server diagnostic"));
            Assert.That(error.Message, Is.EqualTo("Error calling ReadFixture: " + (status == 0 ? "transport diagnostic" : "server diagnostic")));
        }

        [TestCase(200), TestCase(201), TestCase(204), TestCase(307)]
        public void SuccessfulAndRedirectStatusBehaviorIsUnchanged(int status)
        {
            var response = new RestResponse { StatusCode = (HttpStatusCode)status, ErrorException = new InvalidOperationException("unusual response diagnostic") };
            Assert.That(Configuration.DefaultExceptionFactory("ReadFixture", response), Is.Null);
        }

        [Test]
        public async Task CallerExceptionHooksKeepIdentityAndSeeTheOriginalTransportFailure([Values(false, true)] bool structured, [Values(0, 2)] int mode)
        {
            var cause = new HttpRequestException("isolated failure");
            var handler = new Handler(token => Task.FromException<HttpResponseMessage>(cause));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var expected = new InvalidOperationException("caller hook");
                api.ExceptionFactory = (name, response) => { Assert.That(response.ErrorException, Is.SameAs(cause)); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Read(api, structured, mode, CancellationToken.None);
                })), Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void PreCancellationRemainsCancellationAndDoesNotSend([Values(false, true)] bool structured)
        {
            var handler = new Handler(token => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Read(Api(http), structured, 2, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationRemainsCancellationInsteadOfApiError([Values(false, true)] bool structured)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return new HttpResponseMessage(HttpStatusCode.OK); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Read(Api(http), structured, 2, cancellation.Token);
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

        private static ContactApi Api(HttpClient http) => new ContactApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest" });
        private static async Task Read(ContactApi api, bool structured, int mode, CancellationToken token)
        {
            if (structured)
            {
                if (mode == 0) api.SearchContactsWithHttpInfo();
                else if (mode == 1) await api.SearchContactsAsyncWithHttpInfo();
                else await api.SearchContactsAsyncWithHttpInfo(null, token);
            }
            else
            {
                if (mode == 0) api.GetContactsWithHttpInfo();
                else if (mode == 1) await api.GetContactsAsyncWithHttpInfo();
                else await api.GetContactsAsyncWithHttpInfo(null, token);
            }
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token) { Calls++; return send(token); }
        }
    }
}
