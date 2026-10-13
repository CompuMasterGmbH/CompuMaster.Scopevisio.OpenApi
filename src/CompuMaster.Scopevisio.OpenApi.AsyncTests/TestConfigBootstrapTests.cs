using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CompuMaster.Scopevisio.OpenApi.Api;
using NUnit.Framework;
using HarnessConfig = CompuMaster.Scopevisio.OpenApi.Test.TestConfig;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class TestConfigBootstrapTests
    {
        [Test]
        public void ColdCacheFirstAccountReadUsesTheFreshlyObtainedToken()
        {
            var store = new MemoryStore();
            var handler = new FakeHandler();
            using (var http = new HttpClient(handler))
            {
                var config = new HarnessConfig(false, store.ReadInput, store.ReadToken, store.WriteToken,
                    tokenInfo => Authorize(tokenInfo, http));
                config.HttpClient = http;
                var response = new AdditionalApi(config).GetApplicationContextWithHttpInfo();
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(config.AccessToken, Is.EqualTo(FakeHandler.Token));
                Assert.That(handler.AccountToken, Is.EqualTo(FakeHandler.Token));
                Assert.That(store.Token, Is.EqualTo(FakeHandler.Token));
                Assert.That(handler.Authorizations, Is.EqualTo(1));
            }
        }

        [Test]
        public void WarmCacheIsInstalledWithoutAnotherAuthorizationRequest()
        {
            var store = new MemoryStore { Token = FakeHandler.Token };
            var handler = new FakeHandler();
            using (var http = new HttpClient(handler))
            {
                var config = new HarnessConfig(false, store.ReadInput, store.ReadToken, store.WriteToken,
                    tokenInfo => throw new InvalidOperationException("Unexpected fixture authorization."));
                config.HttpClient = http;
                Assert.That(new AdditionalApi(config).GetApplicationContextWithHttpInfo().StatusCode, Is.EqualTo(200));
                Assert.That(handler.AccountToken, Is.EqualTo(FakeHandler.Token));
                Assert.That(handler.Authorizations, Is.Zero);
            }
        }

        [Test]
        public void ExplicitNoAuthorizationModeDoesNotReadOrInstallTheCachedToken()
        {
            var store = new MemoryStore { Token = FakeHandler.Token };
            var config = new HarnessConfig(true, store.ReadInput, store.ReadToken, store.WriteToken,
                tokenInfo => throw new InvalidOperationException("Unexpected fixture authorization."));
            Assert.That(config.AccessToken, Is.Null);
            Assert.That(store.Reads, Is.Zero);
            Assert.That(store.Writes, Is.Zero);
            Assert.That(store.Token, Is.EqualTo(FakeHandler.Token));
        }

        [Test]
        public void FailedAuthorizationPreservesTheOriginalFailureAndDoesNotCacheAToken()
        {
            var store = new MemoryStore();
            var failure = new IOException("Fixture authorization failure.");
            var actual = Assert.Throws<IOException>((TestDelegate)(() => new HarnessConfig(false, store.ReadInput, store.ReadToken,
                store.WriteToken, tokenInfo => throw failure)));
            Assert.That(actual, Is.SameAs(failure));
            Assert.That(store.Token, Is.Null);
            Assert.That(store.Writes, Is.Zero);
        }

        private static void Authorize(HarnessConfig config, HttpClient http)
        {
            config.HttpClient = http;
            new AuthorizationApi(config).Token("password", config.ClientNumber, null, null, config.Username,
                null, config.OrganisationName, config.Password, null, null, null, null);
        }

        private sealed class MemoryStore
        {
            internal string Token;
            internal int Reads;
            internal int Writes;
            internal string ReadInput(string name) => "fixture-" + name;
            internal string ReadToken() { Reads++; return Token; }
            internal void WriteToken(string value) { Writes++; Token = value; }
        }

        private sealed class FakeHandler : HttpMessageHandler
        {
            internal const string Token = "fixture-bootstrap-token";
            internal string AccountToken;
            internal int Authorizations;
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request.Method == HttpMethod.Post)
                {
                    Authorizations++;
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"access_token\":\"" + Token + "\",\"token_type\":\"bearer\",\"expires_in\":3600,\"refresh_token\":\"fixture-refresh\"}")
                    });
                }
                AccountToken = request.Headers.Authorization?.Parameter;
                return Task.FromResult(new HttpResponseMessage(AccountToken == Token ? HttpStatusCode.OK : HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("{}")
                });
            }
        }
    }
}
