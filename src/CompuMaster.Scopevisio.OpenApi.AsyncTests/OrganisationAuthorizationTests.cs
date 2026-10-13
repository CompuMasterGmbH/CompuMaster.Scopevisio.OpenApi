using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CompuMaster.Scopevisio.OpenApi.Client;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class OrganisationAuthorizationTests
    {
        [TestCase(false), TestCase(true)]
        public async Task NumericSelectorIsExactAndDoesNotLookUpOrSendTheDisplayName(bool native)
        {
            var handler = new Handler();
            var previousCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
                using (var http = new HttpClient(handler))
                {
                    var client = CreateClient(http);
                    await Authorize(client, native);
                    Assert.That(handler.Form["organisation_id"], Is.EqualTo(long.MaxValue.ToString(CultureInfo.InvariantCulture)));
                    Assert.That(handler.Form.ContainsKey("organisation"), Is.False);
                    Assert.That(handler.Paths, Is.EqualTo(new[] { "/rest/token", "/rest/myaccount" }));
                    Assert.That(client.Token.OrganisationId, Is.EqualTo(long.MaxValue));
                    Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-candidate"));
                }
            }
            finally { CultureInfo.CurrentCulture = previousCulture; }
        }

        [TestCase(false), TestCase(true)]
        public async Task CandidateStateIsNotExposedBeforeValidationAndBorrowedTransportRemainsUsable(bool native)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                client.Config.AddDefaultHeader("X-Unittests", "preserved");
                handler.Observe = request =>
                {
                    AssertUnchanged(client, prior);
                    Assert.That(request.Headers.GetValues("X-Unittests"), Is.EqualTo(new[] { "preserved" }));
                };
                await Authorize(client, native);
                handler.Observe = null;
                Assert.That(client.Token, Is.Not.SameAs(prior));
                Assert.That(client.AdditionalApi.GetApplicationContextWithHttpInfo().StatusCode, Is.EqualTo(200));
                Assert.That(handler.Paths.Count, Is.EqualTo(3));
            }
        }

        [TestCase(false, null), TestCase(true, null), TestCase(false, ""), TestCase(true, ""),
         TestCase(false, "unittests_legacy_name"), TestCase(true, "unittests_legacy_name")]
        public async Task UnselectedDefaultAndNameLoginsRetainTheOriginalSingleRequest(bool native, string name)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                client.Config.OrganisationId = null;
                client.Config.OrganisationName = name;
                await Authorize(client, native);
                Assert.That(handler.Form.ContainsKey("organisation_id"), Is.False);
                Assert.That(handler.Form.ContainsKey("organisation"), Is.EqualTo(name != null));
                if (name != null) Assert.That(handler.Form["organisation"], Is.EqualTo(name));
                Assert.That(handler.Paths, Is.EqualTo(new[] { "/rest/token" }));
            }
        }

        [TestCase(false, "organisation"), TestCase(true, "organisation"),
         TestCase(false, "missing-organisation"), TestCase(true, "missing-organisation"),
         TestCase(false, "user"), TestCase(true, "user"), TestCase(false, "bearer"), TestCase(true, "bearer")]
        public async Task UnconfirmedTokenDoesNotReadAccountOrReplaceInstalledState(bool native, string failure)
        {
            var handler = new Handler();
            var body = JObject.Parse(handler.TokenBody);
            if (failure == "organisation") body["organisationId"] = 7;
            if (failure == "missing-organisation") body.Remove("organisationId");
            if (failure == "user") body.Remove("uid");
            if (failure == "bearer") body.Remove("access_token");
            handler.TokenBody = body.ToString();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var actual = await Catch(() => Authorize(client, native));
                Assert.That(actual, Is.TypeOf<InvalidDataException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths, Is.EqualTo(new[] { "/rest/token" }));
            }
        }

        [TestCase(false, "organisation"), TestCase(true, "organisation"),
         TestCase(false, "missing-organisation"), TestCase(true, "missing-organisation"),
         TestCase(false, "missing-id"), TestCase(true, "missing-id"),
         TestCase(false, "missing-user"), TestCase(true, "missing-user"),
         TestCase(false, "missing-uid"), TestCase(true, "missing-uid"), TestCase(false, "uid"), TestCase(true, "uid")]
        public async Task UnconfirmedAccountDoesNotInstallCandidateOrFallBack(bool native, string failure)
        {
            var handler = new Handler();
            var body = JObject.Parse(handler.AccountBody);
            if (failure == "organisation") body["organisation"]["id"] = 7;
            if (failure == "missing-organisation") body.Remove("organisation");
            if (failure == "missing-id") ((JObject)body["organisation"]).Remove("id");
            if (failure == "missing-user") body.Remove("user");
            if (failure == "missing-uid") ((JObject)body["user"]).Remove("uid");
            if (failure == "uid") body["user"]["uid"] = "fixture-other-user";
            handler.AccountBody = body.ToString();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var actual = await Catch(() => Authorize(client, native));
                Assert.That(actual, Is.TypeOf<InvalidDataException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(2));
            }
        }

        [TestCase(false, false), TestCase(true, false), TestCase(false, true), TestCase(true, true)]
        public async Task ZeroDefaultOfAMissingJsonIdIsNotAnExplicitIdentityConfirmation(bool native, bool account)
        {
            var handler = new Handler();
            var token = JObject.Parse(handler.TokenBody);
            token["organisationId"] = 0;
            if (!account) token.Remove("organisationId");
            handler.TokenBody = token.ToString();
            handler.AccountBody = "{\"user\":{\"uid\":\"fixture-scope-user\"},\"organisation\":{}}";
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                client.Config.OrganisationId = 0;
                var prior = SeedPrevious(client);
                Assert.That(await Catch(() => Authorize(client, native)), Is.TypeOf<InvalidDataException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(account ? 2 : 1));
            }
        }

        [TestCase(false, false), TestCase(true, false), TestCase(false, true), TestCase(true, true)]
        public async Task FractionalWireIdentityCannotBeRoundedIntoTheRequestedInteger(bool native, bool account)
        {
            var handler = new Handler();
            var token = JObject.Parse(handler.TokenBody);
            token["organisationId"] = account ? (JToken)7L : 7.25;
            handler.TokenBody = token.ToString();
            handler.AccountBody = "{\"user\":{\"uid\":\"fixture-scope-user\"},\"organisation\":{\"id\":7.25}}";
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                client.Config.OrganisationId = 7;
                var prior = SeedPrevious(client);
                Assert.That(await Catch(() => Authorize(client, native)), Is.TypeOf<InvalidDataException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(account ? 2 : 1));
            }
        }

        [TestCase(false, false), TestCase(true, false), TestCase(false, true), TestCase(true, true)]
        public async Task OriginalFactoryFailuresArePreservedWithoutFallback(bool native, bool account)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var failure = new IOException("unittests original factory failure");
                if (account) client.AdditionalApi.ExceptionFactory = (method, response) => failure;
                else client.AuthorizationApi.ExceptionFactory = (method, response) => failure;
                Assert.That(await Catch(() => Authorize(client, native)), Is.SameAs(failure));
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(account ? 2 : 1));
            }
        }

        [TestCase(false, false), TestCase(true, false), TestCase(false, true), TestCase(true, true)]
        public async Task HttpAuthorizationAndAccountFailuresRetainStatusAndInstalledState(bool native, bool account)
        {
            var handler = new Handler();
            if (account) handler.AccountStatus = HttpStatusCode.Forbidden;
            else handler.TokenStatus = HttpStatusCode.Unauthorized;
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var actual = await Catch(() => Authorize(client, native));
                Assert.That(actual, Is.TypeOf<ApiException>());
                Assert.That(((ApiException)actual).ErrorCode, Is.EqualTo(account ? 403 : 401));
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(account ? 2 : 1));
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task ChangedExplicitSelectionIsNeverInstalledOrReplacedByDefault(bool native)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                handler.Observe = request => { if (request.Method == HttpMethod.Get) client.Config.OrganisationId = 7; };
                Assert.That(await Catch(() => Authorize(client, native)), Is.TypeOf<InvalidOperationException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(2));
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task ReplacedConfigurationIsNotOverwrittenEvenWhenItsOrganisationIdMatches(bool native)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                var original = client.Config;
                var prior = SeedPrevious(client);
                var replacement = new Configuration { OrganisationId = long.MaxValue, AccessToken = "fixture-other-profile" };
                handler.Observe = request => { if (request.Method == HttpMethod.Get) client.Config = replacement; };
                Assert.That(await Catch(() => Authorize(client, native)), Is.TypeOf<InvalidOperationException>());
                Assert.That(client.Config, Is.SameAs(replacement));
                Assert.That(replacement.AccessToken, Is.EqualTo("fixture-other-profile"));
                Assert.That(original.AccessToken, Is.EqualTo("fixture-prior"));
                Assert.That(client.Token, Is.SameAs(prior));
            }
        }

        [Test]
        public async Task PreCancellationDoesNotSendHttpOrInstallState()
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                cancellation.Cancel();
                Assert.That(await Catch(() => Authorize(client, true, cancellation.Token)), Is.InstanceOf<OperationCanceledException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths, Is.Empty);
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task CancellationReachesTokenAndContextAndReleasesAuthorizationGate(bool account)
        {
            var handler = new Handler { BlockPath = account ? "/rest/myaccount" : "/rest/token" };
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var request = Authorize(client, true, cancellation.Token);
                await handler.Entered.Task;
                cancellation.Cancel();
                Assert.That(await Catch(() => request), Is.InstanceOf<OperationCanceledException>());
                AssertUnchanged(client, prior);
                Assert.That(handler.Paths.Count, Is.EqualTo(account ? 2 : 1));
                handler.BlockPath = null;
                await Authorize(client, true);
                Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-candidate"));
            }
        }

        [Test]
        public async Task CancellationWhileWaitingForTheGateDoesNotSendAnotherRequest()
        {
            var handler = new Handler { BlockPath = "/rest/token" };
            using (var http = new HttpClient(handler))
            using (var firstCancellation = new CancellationTokenSource())
            using (var secondCancellation = new CancellationTokenSource())
            {
                var client = CreateClient(http);
                var prior = SeedPrevious(client);
                var first = Authorize(client, true, firstCancellation.Token);
                await handler.Entered.Task;
                var second = Authorize(client, true, secondCancellation.Token);
                secondCancellation.Cancel();
                Assert.That(await Catch(() => second), Is.InstanceOf<OperationCanceledException>());
                Assert.That(handler.Paths.Count, Is.EqualTo(1));
                AssertUnchanged(client, prior);
                firstCancellation.Cancel();
                Assert.That(await Catch(() => first), Is.InstanceOf<OperationCanceledException>());
            }
        }

        [Test]
        public async Task RefreshValidatesFreshIdentityAndKeepsTheOriginalRefreshWireContract()
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                await Authorize(client, true);
                handler.TokenBody = handler.TokenBody.Replace("fixture-candidate", "fixture-refreshed");
                handler.ExpectedAccountBearer = "fixture-refreshed";
                await client.RefreshAuthorizationAsync();
                Assert.That(handler.Form["grant_type"], Is.EqualTo("refresh_token"));
                Assert.That(handler.Form["refresh_token"], Is.EqualTo("fixture-refresh"));
                Assert.That(handler.Form.ContainsKey("organisation_id"), Is.False);
                Assert.That(handler.Form.ContainsKey("organisation"), Is.False);
                Assert.That(handler.Paths.Count, Is.EqualTo(4));
                Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-refreshed"));
            }
        }

        [TestCase(false), TestCase(true)]
        public async Task RefreshedTokenCannotRetargetOrganisationOrUser(bool user)
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                await Authorize(client, true);
                var prior = client.Token;
                var body = JObject.Parse(handler.TokenBody);
                if (user)
                {
                    body["uid"] = "fixture-other-user";
                    handler.AccountBody = handler.AccountBody.Replace("fixture-scope-user", "fixture-other-user");
                }
                else body["organisationId"] = 7;
                handler.TokenBody = body.ToString();
                Assert.That(await Catch(() => client.RefreshAuthorizationAsync()), Is.TypeOf<InvalidDataException>());
                Assert.That(client.Token, Is.SameAs(prior));
                Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-candidate"));
                Assert.That(handler.Paths.Count, Is.EqualTo(3));
            }
        }

        [Test]
        public async Task RefreshRejectsADifferentConfiguredOrganisationBeforeHttp()
        {
            var handler = new Handler();
            using (var http = new HttpClient(handler))
            {
                var client = CreateClient(http);
                await Authorize(client, true);
                var prior = client.Token;
                client.Config.OrganisationId = 7;
                Assert.That(await Catch(() => client.RefreshAuthorizationAsync()), Is.TypeOf<InvalidDataException>());
                Assert.That(client.Token, Is.SameAs(prior));
                Assert.That(handler.Paths.Count, Is.EqualTo(2));
            }
        }

        private static CompuMaster.Scopevisio.OpenApi.Model.TokenResponse SeedPrevious(OpenScopeApiClient client)
        {
            var prior = JObject.Parse("{\"access_token\":\"fixture-prior\",\"refresh_token\":\"fixture-prior-refresh\",\"uid\":\"fixture-scope-user\",\"organisationId\":9223372036854775807}").ToObject<CompuMaster.Scopevisio.OpenApi.Model.TokenResponse>();
            client.Token = prior;
            client.Config.AccessToken = "fixture-prior";
            return prior;
        }

        private static void AssertUnchanged(OpenScopeApiClient client, CompuMaster.Scopevisio.OpenApi.Model.TokenResponse prior)
        {
            Assert.That(client.Token, Is.SameAs(prior));
            Assert.That(client.Config.AccessToken, Is.EqualTo("fixture-prior"));
        }

        private static async Task<Exception> Catch(Func<Task> operation)
        {
            try { await operation(); }
            catch (Exception failure) { return failure; }
            Assert.Fail("Expected authorization to fail.");
            return null;
        }

        private static OpenScopeApiClient CreateClient(HttpClient http)
        {
            return new OpenScopeApiClient(new Configuration
            {
                HttpClient = http, BasePath = "https://fixture.invalid/rest", ClientNumber = "fixture-customer",
                Username = "fixture-login", Password = "fixture-password", OrganisationId = long.MaxValue,
                OrganisationName = "unittests_display_name_must_not_be_used"
            });
        }

        private static Task Authorize(OpenScopeApiClient client, bool native, CancellationToken cancellation = default(CancellationToken))
        {
            if (native) return client.AuthorizeWithUserCredentialsAsync(cancellation);
            client.AuthorizeWithUserCredentials();
            return Task.CompletedTask;
        }

        private sealed class Handler : HttpMessageHandler
        {
            internal readonly List<string> Paths = new List<string>();
            internal Dictionary<string, string> Form;
            internal Action<HttpRequestMessage> Observe;
            internal HttpStatusCode TokenStatus = HttpStatusCode.OK;
            internal HttpStatusCode AccountStatus = HttpStatusCode.OK;
            internal string ExpectedAccountBearer = "fixture-candidate";
            internal string BlockPath;
            internal readonly TaskCompletionSource<bool> Entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            internal string TokenBody = "{\"access_token\":\"fixture-candidate\",\"refresh_token\":\"fixture-refresh\",\"uid\":\"fixture-scope-user\",\"organisationId\":9223372036854775807}";
            internal string AccountBody = "{\"user\":{\"uid\":\"fixture-scope-user\"},\"organisation\":{\"id\":9223372036854775807}}";

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
            {
                Paths.Add(request.RequestUri.AbsolutePath);
                Observe?.Invoke(request);
                if (request.RequestUri.AbsolutePath == BlockPath)
                {
                    Entered.TrySetResult(true);
                    await Task.Delay(Timeout.Infinite, cancellation);
                }
                if (request.Method == HttpMethod.Post)
                {
                    Form = new Dictionary<string, string>();
                    foreach (var pair in (await request.Content.ReadAsStringAsync()).Split('&'))
                    {
                        var parts = pair.Split(new[] { '=' }, 2);
                        Form.Add(Uri.UnescapeDataString(parts[0]), Uri.UnescapeDataString(parts[1].Replace("+", " ")));
                    }
                    return new HttpResponseMessage(TokenStatus) { Content = new StringContent(TokenBody) };
                }
                Assert.That(request.RequestUri.AbsolutePath, Is.EqualTo("/rest/myaccount"), "Only the fresh account read is permitted.");
                Assert.That(request.Headers.Authorization?.Parameter, Is.EqualTo(ExpectedAccountBearer));
                return new HttpResponseMessage(AccountStatus) { Content = new StringContent(AccountBody) };
            }
        }
    }
}
