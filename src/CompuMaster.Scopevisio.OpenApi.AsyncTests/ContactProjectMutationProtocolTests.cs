using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
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
    public class ContactProjectMutationProtocolTests
    {
        private static readonly int[] Routes = { 0, 1, 2, 3 };
        private static readonly string[] Names = { "ModifyContactFields", "CreateProjectResource", "RemoveTimeEntry", "RemoveTimeEntryByLegacyNumber" };
        private static readonly string[] Paths = { "/rest/contact/modify/id%20%2F%20alpha%3F%23", "/rest/projectresource/new", "/rest/timeentry/id%20%2F%20alpha%3F%23", "/rest/timeentry/legacynumber/id%20%2F%20alpha%3F%23" };
        private const string Created = "{\"status\":201,\"id\":9223372036854775807,\"projectId\":9223372036854775807,\"errors\":{\"projectRole\":\"not listed\"},\"hints\":{\"revenueConflict\":true},\"unknown\":null}";

        [Test]
        public async Task ExplicitMutationsKeepTheirMethodBodyAndStatusWithoutRetries([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Success(route)));
            using (var transport = new HttpClient(handler))
            {
                var input = Input(route);
                var result = await Invoke(route, mode, Api(route, transport), input, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(route == 0 ? new HttpMethod("PATCH") : route == 1 ? HttpMethod.Post : HttpMethod.Delete));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-fixture-token"));
                Assert.That(handler.Calls, Is.EqualTo(1));
                Assert.That((int)result.GetType().GetProperty("StatusCode").GetValue(result), Is.EqualTo(route == 0 ? 200 : route == 1 ? 201 : 204));
                if (route <= 1) Assert.That(handler.Body, Is.EqualTo(((JObject)input).ToString(Formatting.None)));
                else Assert.That(string.IsNullOrEmpty(handler.Body), Is.True);
                var data = result.GetType().GetProperty("Data").GetValue(result);
                if (route == 1)
                {
                    var created = (ProjectResourceCreationResponse)data;
                    Assert.That(created.Id, Is.EqualTo(long.MaxValue));
                    Assert.That(created.ProjectId, Is.EqualTo(long.MaxValue));
                    Assert.That(created.Status, Is.EqualTo(201));
                    Assert.That(created.Errors.Value<string>("projectRole"), Is.EqualTo("not listed"));
                    Assert.That(created.Hints.Value<bool>("revenueConflict"), Is.True);
                    Assert.That(created["unknown"].Type, Is.EqualTo(JTokenType.Null));
                    Assert.That((string)result.GetType().GetProperty("RawJsonContent").GetValue(result), Is.EqualTo(Created));
                    Assert.That(handler.Body, Does.Contain("\"standardFee\":123456789.123456789"));
                    Assert.That(JObject.Parse(handler.Body).Value<bool>("recreate"), Is.True);
                }
                if (route >= 2) Assert.That(data, Is.Null);
            }
        }

        [Test]
        public async Task ContactDateOmissionAssignmentAndClearingStayDistinct([Values("{}", "{\"birthDate\":\"29.02.2024\"}", "{\"birthDate\":\"\"}")] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Success(0)));
            using (var transport = new HttpClient(handler))
            {
                var modification = new ContactModification(JObject.Parse(json));
                await Invoke(0, mode, Api(0, transport), modification, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo(json));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task MinimalCreationDoesNotAutomaticallyRecreateOrRetryOnHints([Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Success(1)));
            using (var transport = new HttpClient(handler))
            {
                var request = new ProjectResourceCreationRequest { ProjectId = 42 };
                await Invoke(1, mode, Api(1, transport), request, CancellationToken.None);
                Assert.That(handler.Body, Is.EqualTo("{\"projectId\":42}"));
                Assert.That(request.Property("recreate"), Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void ModelCopiesAndNullSettersRetainCallerIntent()
        {
            var contact = new ContactModification { BirthDate = "" };
            contact.BirthDate = null;
            Assert.That(contact.ToString(Formatting.None), Is.EqualTo("{}"));
            var allocations = new[] { 5L, 10L };
            var request = new ProjectResourceCreationRequest { ProjectId = long.MaxValue, StandardFee = 50.50m, MonthlyAllocations = allocations, Recreate = false };
            allocations[0] = 99;
            Assert.That(request.MonthlyAllocations[0], Is.EqualTo(5));
            var copy = new ProjectResourceCreationRequest(request);
            request["standardFee"] = 0;
            Assert.That(copy.StandardFee, Is.EqualTo(50.50m));
            var returned = copy.MonthlyAllocations;
            returned[0] = 88;
            Assert.That(copy.MonthlyAllocations[0], Is.EqualTo(5));
            copy.StandardFee = null; copy.Recreate = null; copy.MonthlyAllocations = null;
            Assert.That(copy.ToString(Formatting.None), Is.EqualTo("{\"projectId\":9223372036854775807}"));
        }

        [Test]
        public async Task InvalidBirthDateTokensAreRejectedBeforeHttp(
            [Values("null", "42", "true", "[]", "{}", "\"31.02.2026\"", "\"2026-10-12\"")] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Success(0)));
            using (var transport = new HttpClient(handler))
            {
                var input = new ContactModification(new JObject { ["birthDate"] = JToken.Parse(json) });
                Exception actual = null;
                try { await Invoke(0, mode, Api(0, transport), input, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentException>());
                Assert.That(handler.Calls, Is.Zero);
                Assert.That(input["birthDate"].ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [TestCase("projectId", "null")]
        [TestCase("projectId", "1.1")]
        [TestCase("projectId", "\"42\"")]
        [TestCase("projectId", "true")]
        [TestCase("projectId", "9223372036854775808")]
        [TestCase("humanResourceId", "2.1")]
        [TestCase("productId", "\"8\"")]
        [TestCase("projectRole", "false")]
        [TestCase("projectPosition", "{}")]
        [TestCase("allocationStatus", "[]")]
        [TestCase("recreate", "1")]
        [TestCase("recreate", "\"true\"")]
        [TestCase("standardFee", "\"50\"")]
        [TestCase("standardFee", "-1")]
        [TestCase("standardFee", "-1e-40")]
        [TestCase("monthlyAllocations", "{}")]
        [TestCase("monthlyAllocations", "[1.1]")]
        [TestCase("monthlyAllocations", "[\"1\"]")]
        [TestCase("monthlyAllocations", "[null]")]
        [TestCase("monthlyAllocations", "[-1]")]
        [TestCase("monthlyAllocations", "[9223372036854775808]")]
        public async Task InvalidRawCreationFieldsAreRejectedBeforeSyncAndAsyncHttp(string field, string json)
        {
            var handler = new Handler(token => Task.FromResult(Success(1)));
            using (var transport = new HttpClient(handler))
            {
                foreach (int mode in new[] { 0, 2 })
                {
                    var input = new ProjectResourceCreationRequest { ProjectId = 42 };
                    input[field] = JToken.Parse(json);
                    var originalToken = input[field].ToString(Formatting.None);
                    Exception actual = null;
                    try { await Invoke(1, mode, Api(1, transport), input, CancellationToken.None); }
                    catch (Exception error) { actual = error; }
                    Assert.That(actual, Is.TypeOf<ArgumentException>());
                    Assert.That(handler.Calls, Is.Zero);
                    Assert.That(input[field].ToString(Formatting.None), Is.EqualTo(originalToken));
                }
            }
        }

        [Test]
        public async Task NonFiniteFeesAreRejectedBeforeHttp([Values(double.NaN, double.PositiveInfinity, double.NegativeInfinity)] double fee)
        {
            var handler = new Handler(token => Task.FromResult(Success(1)));
            using (var transport = new HttpClient(handler))
            {
                var input = new ProjectResourceCreationRequest { ProjectId = 42 };
                input["standardFee"] = new JValue(fee);
                Exception actual = null;
                try { await Invoke(1, 2, Api(1, transport), input, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentException>());
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task RequiredBodiesAndProjectIdsCannotBeImplicitlyInvented([Values(0, 1)] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Success(route)));
            using (var transport = new HttpClient(handler))
            {
                Exception actual = null;
                try { await Invoke(route, mode, Api(route, transport), null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentNullException>());
                if (route == 1)
                {
                    actual = null;
                    try { await Invoke(route, mode, Api(route, transport), new ProjectResourceCreationRequest(), CancellationToken.None); }
                    catch (Exception error) { actual = error; }
                    Assert.That(actual, Is.TypeOf<ArgumentException>());
                }
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ServerAndCustomErrorsPreserveTheOriginalExceptions([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "permission denied")));
            using (var transport = new HttpClient(handler))
            {
                var api = Api(route, transport);
                Exception actual = null;
                try { await Invoke(route, mode, api, Input(route), CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ApiException>());
                Assert.That(((ApiException)actual).ErrorCode, Is.EqualTo(403));
                var expected = new InvalidOperationException("isolated mutation factory");
                ExceptionFactory factory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, factory);
                actual = null;
                try { await Invoke(route, mode, api, Input(route), CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.SameAs(expected));
                Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void PreCancellationStopsMutationRequestsBeforeHttp([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Success(route)));
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(route, 2, Api(route, transport), Input(route), cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationIsForwardedWithoutRetryingMutations([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.SetResult(true); await Task.Delay(Timeout.Infinite, token); return Success(route); });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var pending = Invoke(route, 2, Api(route, transport), Input(route), cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, pending, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(pending, Task.Delay(5000)), Is.SameAs(pending));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await pending; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        private static object Input(int route)
        {
            if (route == 0) return new ContactModification { BirthDate = "29.02.2024" };
            if (route == 1) return new ProjectResourceCreationRequest {
                ProjectId = long.MaxValue, HumanResourceId = 4294967296L, ProjectRole = "Projektleitung", ProjectPosition = "Projektleiter",
                AllocationStatus = "Hinzugefügt", StandardFee = 123456789.123456789m, MonthlyAllocations = new[] { 5L, 10L }, ProductId = 100, Recreate = true };
            return null;
        }

        private static object Api(int route, HttpClient transport)
        {
            var config = new Configuration { HttpClient = transport, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-fixture-token" };
            return route == 0 ? (object)new ContactApi(config) : new ProjectApi(config);
        }

        private static async Task<object> Invoke(int route, int mode, object api, object input, CancellationToken token)
        {
            var types = route == 0 ? new[] { typeof(string), typeof(ContactModification) } : route == 1 ? new[] { typeof(ProjectResourceCreationRequest) } : new[] { typeof(string) };
            var values = route == 0 ? new object[] { "id / alpha?#", input } : route == 1 ? new[] { input } : new object[] { "id / alpha?#" };
            if (mode == 2)
            {
                Array.Resize(ref types, types.Length + 1); types[types.Length - 1] = typeof(CancellationToken);
                Array.Resize(ref values, values.Length + 1); values[values.Length - 1] = token;
            }
            object result;
            try { result = api.GetType().GetMethod(Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), types).Invoke(api, values); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (mode == 0) return result;
            await (Task)result;
            return result.GetType().GetProperty("Result").GetValue(result);
        }

        private static HttpResponseMessage Success(int route)
        {
            return route == 0 ? Response(HttpStatusCode.OK, "{\"status\":\"updated\",\"retained\":null}") :
                route == 1 ? Response(HttpStatusCode.Created, Created) : Response(HttpStatusCode.NoContent, "");
        }

        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        }

        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Authorization = request.Headers.Authorization?.ToString();
                return await send(token);
            }
        }
    }
}
