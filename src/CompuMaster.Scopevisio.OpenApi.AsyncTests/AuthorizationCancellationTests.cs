using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class AuthorizationCancellationTests
    {
        [TestCase(false), TestCase(true)]
        public async Task CancellationReachesActiveTokenAndAccountRequests(bool tokenRequest)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new FakeHandler(async cancellation =>
            {
                entered.SetResult(true);
                await Task.Delay(Timeout.Infinite, cancellation);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var client = new OpenScopeApiClient(new Configuration { HttpClient = transport, ClientNumber = "isolated-fixture" });
                Task request = tokenRequest
                    ? (Task)client.AuthorizeWithUserCredentialsAsync(cancellation.Token)
                    : client.AdditionalApi.GetApplicationContextAsyncWithHttpInfo(cancellation.Token);
                await entered.Task;
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await request; }));
                Assert.That(client.Token, Is.Null);
                Assert.That(client.Config.AccessToken, Is.Null);
            }
        }

        [Test]
        public async Task AuthorizationInstallsTokensAndRefreshUsesTheInstalledRefreshToken()
        {
            var handler = new FakeHandler(cancellation => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"token_type\":\"bearer\",\"access_token\":\"fixture-access\",\"expires_in\":3600,\"refresh_token\":\"fixture-refresh\",\"uid\":\"fixture-user\",\"organisationId\":1,\"organisationName\":\"fixture-organisation\"}")
            }));
            using (var transport = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(new Configuration { HttpClient = transport, ClientNumber = "isolated-fixture" });
                await client.AuthorizeWithUserCredentialsAsync();
                Assert.That(client.Token.AccessToken, Is.EqualTo("fixture-access"));
                Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-access"));
                await client.RefreshAuthorizationAsync();
                Assert.That(handler.Calls, Is.EqualTo(2));
                Assert.That(handler.LastBody, Does.Contain("grant_type=refresh_token"));
                Assert.That(handler.LastBody, Does.Contain("refresh_token=fixture-refresh"));
            }
        }

        private sealed class FakeHandler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal string LastBody;
            internal FakeHandler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++;
                LastBody = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
