using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    public class LegacyResponseHeaderTests
    {
        private static readonly string[] Groups = { "Accounting", "Activities", "Additional", "Authorization", "Cashbook", "Contact", "Credit", "Dispatch", "HumanResources", "IncomingInvoice", "Offer", "Opportunity", "Order", "Organisation", "OutgoingInvoice", "Product", "Project", "RecurringInvoice", "Report", "Task", "Teamwork", "User" };
        private static readonly string[] Methods = { "GetAccountingInfos", "GetEvents", "GetApplicationContext", "ChangeOrganisationById", "GetCashbookSheets", "GetContactById", "GetCredit", "GetDispatch", "GetHumanResources", "GetIncomingInvoiceTeamworkFileLink", "GetOffer", "GetOpportunities", "GetOrder", "CreateOrganisation", "GetOutgoingInvoice", "GetProduct", "GetAllTimEntries", "GetRecurringInvoice", "GetReport", "GetSubtasks", "GetHandler1", "GetUser" };
        private static readonly int[] Workflows = Enumerable.Range(0, Groups.Length).ToArray();
        private const string Json = "{\"future\":\"Ω\"}";
        private const string ContactJson = "{\"records\":[{\"id\":1,\"future\":\"Ω\"}]}";
        private const string TokenJson = "{\"access_token\":\"isolated-returned-token\",\"token_type\":\"bearer\",\"future\":true}";

        [Test]
        public async Task ExistingApiGroupsRetainDuplicateAndContentHeaders([ValueSource(nameof(Workflows))] int workflow, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(HttpStatusCode.OK, Json);
            using (var http = new HttpClient(handler))
            {
                var config = Config(http); var api = CreateApi(workflow, config);
                var method = SelectedMethod(api, Methods[workflow] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"));
                var response = await Invoke(api, method, Arguments(method));
                Headers(response, Json);
                var data = response.GetType().GetProperty("Data").GetValue(response);
                if (workflow == 2) Assert.That(data, Is.TypeOf<AccountInfo>());
                else if (workflow == 8 || workflow == 16 || workflow == 20) Assert.That(JToken.DeepEquals((JToken)data, JObject.Parse(Json)), Is.True);
                else Assert.That(data, Is.Null, "Historical void/Object Data must remain null.");
                Assert.That(config.AccessToken, Is.EqualTo("isolated-original-token"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ExistingTypedContactsPreserveRecordsAndRawResponse([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(HttpStatusCode.OK, ContactJson);
            using (var http = new HttpClient(handler))
            {
                var config = Config(http); var api = new ContactApi(config);
                ApiResponse<RecordCollection<Contact>> response;
                if (mode == 0) response = api.GetContactsWithHttpInfo("{}");
                else if (mode == 1) response = await api.GetContactsAsyncWithHttpInfo("{}");
                else response = await api.GetContactsAsyncWithHttpInfo("{}", CancellationToken.None);
                Headers(response, ContactJson);
                Assert.That(response.Data.Items[0].Id, Is.EqualTo(1L));
                Assert.That(response.Data.Items[0].Value<string>("future"), Is.EqualTo("Ω"));
                Assert.That(config.AccessToken, Is.EqualTo("isolated-original-token"));
            }
        }

        [Test]
        public async Task ExistingTokenFormsPreserveConfigurationAndDeserialization([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(HttpStatusCode.OK, TokenJson);
            using (var http = new HttpClient(handler))
            {
                var config = Config(http); var api = new AuthorizationApi(config);
                ApiResponse<TokenResponse> response;
                if (mode == 0) response = api.TokenWithHttpInfo("password", "isolated-customer");
                else if (mode == 1) response = await api.TokenAsyncWithHttpInfo("password", "isolated-customer");
                else response = await api.TokenAsyncWithHttpInfo("password", "isolated-customer", CancellationToken.None);
                Headers(response, TokenJson);
                Assert.That(response.Data.AccessToken, Is.EqualTo("isolated-returned-token"));
                Assert.That(config.AccessToken, Is.EqualTo("isolated-returned-token"));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/token"));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Body, Does.Contain("grant_type=password").And.Contain("customer=isolated-customer"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task NativeLegacyPartialsRetainHeaderMetadata([Values(0, 1, 2)] int workflow)
        {
            var handler = new Handler(HttpStatusCode.OK, Json);
            using (var http = new HttpClient(handler))
            {
                var config = Config(http); object response;
                if (workflow == 0) response = await new AdditionalApi(config).GetApplicationContextAsyncWithHttpInfo(CancellationToken.None);
                else if (workflow == 1) response = await new ProjectApi(config).GetProjectsAsyncWithHttpInfo("{}", CancellationToken.None);
                else response = await new TaskApi(config).GetTasksAsyncWithHttpInfo("{}", CancellationToken.None);
                Headers(response, Json);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-original-token"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void ExistingExceptionFactoriesObserveUnmodifiedResponseBeforeCollection([ValueSource(nameof(Workflows))] int workflow, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(HttpStatusCode.OK, "not JSON");
            using (var http = new HttpClient(handler))
            {
                var config = Config(http); var api = CreateApi(workflow, config);
                var expected = new InvalidOperationException("original caller hook");
                api.GetType().GetProperty("ExceptionFactory").SetValue(api, (ExceptionFactory)((name, response) => {
                    Assert.That(name, Is.EqualTo(Methods[workflow]));
                    Assert.That(response.Content, Is.EqualTo("not JSON"));
                    Assert.That(response.Headers.Count(header => header.Name == "X-Fixture"), Is.EqualTo(2));
                    Assert.That(response.ContentHeaders.Any(header => header.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)), Is.True);
                    return expected;
                }));
                var method = SelectedMethod(api, Methods[workflow] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"));
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, method, Arguments(method)); }));
                Assert.That(actual, Is.SameAs(expected));
                Assert.That(config.AccessToken, Is.EqualTo("isolated-original-token"));
            }
        }

        [Test]
        public void DefaultFailuresKeepOriginalStatusAndDiagnostic([Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(HttpStatusCode.Forbidden, "original permission diagnostic");
            using (var http = new HttpClient(handler))
            {
                var api = new ContactApi(Config(http));
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => {
                    if (asynchronous) await api.GetContactByIdAsyncWithHttpInfo(1L);
                    else api.GetContactByIdWithHttpInfo(1L);
                }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("original permission diagnostic"));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private static Configuration Config(HttpClient http) => new Configuration { HttpClient = http, BasePath = "https://legacy-headers.invalid/rest", AccessToken = "isolated-original-token" };
        private static object CreateApi(int workflow, Configuration config)
            => Activator.CreateInstance(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Api." + Groups[workflow] + "Api", true), new object[] { config });
        private static MethodInfo SelectedMethod(object api, string name)
            => api.GetType().GetMethods().Single(method => method.Name == name && !method.GetParameters().Any(parameter => parameter.ParameterType == typeof(CancellationToken)));
        private static object[] Arguments(MethodInfo method)
            => method.GetParameters().Select(parameter => parameter.IsOptional ? parameter.DefaultValue : parameter.ParameterType == typeof(string) ? (object)"isolated-selection" : parameter.ParameterType == typeof(long) ? 1L : throw new InvalidOperationException("An explicit isolated argument is missing.")).ToArray();
        private static async Task<object> Invoke(object api, MethodInfo method, object[] arguments)
        {
            object result;
            try { result = method.Invoke(api, arguments); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static void Headers(object response, string raw)
        {
            var type = response.GetType();
            var headers = (IDictionary<string, string>)type.GetProperty("Headers").GetValue(response);
            Assert.That(headers["x-fixture"], Is.EqualTo("first,second,third"));
            Assert.That(headers["CONTENT-TYPE"], Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(headers["content-length"], Is.EqualTo(Encoding.UTF8.GetByteCount(raw).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            Assert.That(headers["CONTENT-LANGUAGE"], Is.EqualTo("de-DE"));
            Assert.That(type.GetProperty("RawJsonContent").GetValue(response), Is.EqualTo(raw));
            Assert.That(type.GetProperty("StatusCode").GetValue(response), Is.EqualTo(200));
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly HttpStatusCode status;
            private readonly string json;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body;
            internal Handler(HttpStatusCode status, string json) { this.status = status; this.json = json; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                token.ThrowIfCancellationRequested(); Calls++; Uri = request.RequestUri;
                Assert.That(Uri.Host, Is.EqualTo("legacy-headers.invalid"));
                Method = request.Method;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                var response = new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
                response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
                response.Content.Headers.TryAddWithoutValidation("x-fixture", "third");
                response.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(json);
                response.Content.Headers.ContentLanguage.Add("de-DE");
                return response;
            }
        }
    }
}
