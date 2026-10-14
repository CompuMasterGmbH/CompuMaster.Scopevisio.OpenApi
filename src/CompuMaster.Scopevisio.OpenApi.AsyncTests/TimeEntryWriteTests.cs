using CompuMaster.Scopevisio.OpenApi.Api;
using NUnit.Framework;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using System;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class TimeEntryWriteTests
    {
        [TestCase("CreateTimeEntryWithResult")]
        [TestCase("UpdateTimeEntryWithResult")]
        public void CompleteCurrentTimeEntryWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ProjectApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentTimeEntryWriteRequestIsAvailable()
        {
            Assert.That(typeof(ProjectApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.TimeEntryWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["TimeEntryForm"]["properties"];
        }
        public static IEnumerable<TestCaseData> Fields()
        {
            foreach (var field in Schema().Properties())
                foreach (var creating in new[] { false, true })
                    foreach (var mode in new[] { 0, 1, 2 })
                        yield return new TestCaseData(field.Name, creating, mode);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownFieldKindsAreCheckedBeforeHttp(string field, bool creating, int mode)
        {
            string kind = Schema()[field].Value<string>("type"); var request = Valid(creating);
            request[field] = kind == "string" ? new JValue(false) : kind == "boolean" ? new JValue("false") : kind == "integer" ? new JValue(7.25m) : (JToken)new JObject();
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, mode, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public void ConstructorAndArraySettersCopyInput()
        {
            var original = new JObject { ["future"] = new JObject { ["value"] = 1 } }; var request = new TimeEntryWriteRequest(original);
            original["future"]["value"] = 2; Assert.That(request["future"]["value"].Value<int>(), Is.EqualTo(1));
            var custom = new JArray(new JObject { ["customFieldName"] = "customBoolean1", ["customFieldValue"] = false }); request.CustomFields = custom;
            custom[0]["customFieldValue"] = true; Assert.That(request.CustomFields[0].Value<bool>("customFieldValue"), Is.False);
            request.CustomFields = null; Assert.That(request.ContainsKey("customFields"), Is.False);
            Assert.Throws<ArgumentNullException>((TestDelegate)(() => new TimeEntryWriteRequest(null)));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request = Valid(creating); request.WorkLocation = "before"; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(creating ? 201 : 200, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.WorkLocation = "after"; request["future"]["value"] = 2;
                    Assert.That(JObject.Parse(handler.Body).Value<string>("workLocation"), Is.EqualTo("before")); Assert.That(JObject.Parse(handler.Body)["future"].Value<int>("value"), Is.EqualTo(1));
                }
                finally { release.TrySetResult(true); }
                await operation;
            }
        }

        [Test]
        public async Task CompleteResultFormsRetainOriginalJsonWithoutInventedValues([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode, [Values("{}", "[]", "null", "true", "{\"warnings\":[\"inspect\"]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, json)));
            using (var http = new HttpClient(handler))
            {
                var data = await InvokeData(Api(http), -1L, Valid(creating), creating, mode); Assert.That(data.ToString(Formatting.None), Is.EqualTo(json)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void OriginalFailureAndFactoryPrecedeJson([Values(false, true)] bool creating, [Values(0, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original time-entry diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original time-entry diagnostic"));
                var expected = new InvalidOperationException("custom time-entry hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateTimeEntryWithResult" : "UpdateTimeEntryWithResult")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); })), Is.SameAs(expected)); Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInventedSuccess([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "{invalid")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, Valid(creating), creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void PreCancelledWriteHasNoHttpEffects([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            { cancel.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, Valid(creating), creating, 2, cancel.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task ActiveWriteCancellationDoesNotReplay([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, "{}"); });
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), 1L, Valid(creating), creating, 2, cancel.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancel.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancel.Cancel(); }
            }
        }


        [Test]
        public void TypedNullSettersOmitEveryKnownField()
        {
            var request = new TimeEntryWriteRequest { ["future"] = false };
            foreach (var field in Schema().Properties())
            {
                request[field.Name] = JValue.CreateNull();
                typeof(TimeEntryWriteRequest).GetProperty(PropertyName(field.Name)).SetValue(request, null);
                Assert.That(request.ContainsKey(field.Name), Is.False, field.Name);
            }
            Assert.That(request.Count, Is.EqualTo(1));
            Assert.That(request.Value<bool>("future"), Is.False);
        }

        [Test]
        public async Task CustomNullIsRetainedOnlyForUpdates([Values(false, true)] bool creating)
        {
            var request = Valid(creating);
            request.CustomFields = new JArray(new JObject { ["customFieldName"] = "customText1", ["customFieldValue"] = JValue.CreateNull() });
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (creating) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, 2, CancellationToken.None); }));
                else { await Invoke(Api(http), 1L, request, false, 2, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); }
            }
            Assert.That(handler.Calls, Is.EqualTo(creating ? 0 : 1));
        }


        private static string PropertyName(string field) => field == "legacynumber" ? "LegacyNumber" : char.ToUpperInvariant(field[0]) + field.Substring(1);

        [Test]
        public async Task AllCurrentTypedFieldsRetainWireValuesAcrossAlternativeSelections([Values(false, true)] bool creating, [Values(false, true)] bool mail, [Values(false, true)] bool orderNumber, [Values(0, 1, 2)] int mode)
        {
            var request = new TimeEntryWriteRequest(); var expected = new JObject();
            Assert.That(Schema().Count, Is.EqualTo(24));
            Assert.That(typeof(TimeEntryWriteRequest).GetProperties().Count(p => p.DeclaringType == typeof(TimeEntryWriteRequest)), Is.EqualTo(24));
            foreach (var field in Schema().Properties())
            {
                var property = typeof(TimeEntryWriteRequest).GetProperty(PropertyName(field.Name)); Assert.That(property, Is.Not.Null, field.Name);
                if (field.Name == (mail ? "employeeId" : "employeeMail") || field.Name == (orderNumber ? "orderId" : "orderNumber")) continue;
                string kind = field.Value.Value<string>("type");
                object value = kind == "string" ? (object)("  original / " + field.Name) : kind == "boolean" ? (object)false : kind == "integer" ? field.Value.Value<string>("format") == "int64" ? (object)long.MinValue : int.MinValue :
                    new JArray(new JObject { ["customFieldName"] = "customAmount1", ["customFieldValue"] = 123456789.123456789m, ["future"] = false });
                property.SetValue(request, value); expected[field.Name] = value is JToken token ? token.DeepClone() : new JValue(value);
            }
            Assert.That(expected.Count, Is.EqualTo(22));
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, Result)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-time-entry-write-token" };
                var api = new ProjectApi(config); var info = await Invoke(api, long.MaxValue, request, creating, mode, CancellationToken.None);
                Assert.That(api.Configuration, Is.SameAs(config)); Assert.That(config.HttpClient, Is.SameAs(http));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(creating ? "/rest/timeentry/new" : "/rest/timeentry/9223372036854775807"));
                Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-time-entry-write-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(handler.Body, Is.EqualTo(expected.ToString(Formatting.None))); Assert.That(info.StatusCode, Is.EqualTo(creating ? 201 : 200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Result)); Assert.That(info.Headers["X-Time-Fixture"], Is.EqualTo("retained"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json")); Assert.That(info.Data["id"].Value<long>(), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["fraction"].Value<decimal>(), Is.EqualTo(123456789.123456789m)); Assert.That(info.Data["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1)); Assert.That(config.AccessToken, Is.EqualTo("isolated-time-entry-write-token"));
            }
        }

        [Test]
        public async Task CreationRequiresBodyAndExactlyOneEmployeeWithoutChoosingDefaults([Values(0, 1, 2)] int mode, [Values("null-body", "empty", "both", "id", "mail", "empty-mail", "null-id", "null-mail")] string selection)
        {
            var request = selection == "null-body" ? null : new TimeEntryWriteRequest();
            if (selection == "both" || selection == "id") request.EmployeeId = 0;
            if (selection == "both" || selection == "mail") request.EmployeeMail = "  fixture@example.invalid ";
            if (selection == "empty-mail") request.EmployeeMail = "";
            if (selection == "null-id") request["employeeId"] = JValue.CreateNull();
            if (selection == "null-mail") request["employeeMail"] = JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (request == null) Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); }));
                else if (selection == "empty" || selection == "both" || selection.StartsWith("null-", StringComparison.Ordinal)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); }));
                else { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(JObject.Parse(handler.Body).Count, Is.EqualTo(1)); }
            }
            Assert.That(handler.Calls, Is.EqualTo(selection == "id" || selection == "mail" || selection == "empty-mail" ? 1 : 0));
        }

        [Test]
        public async Task SparseUpdatesAndExplicitNullDoNotInsertDefaults([Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var info = await Invoke(api, 0L, null, false, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.Null); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty);
                var request = new TimeEntryWriteRequest { FullDay = false, WorkLocation = "", LegacyNumber = "000123", ["future"] = new JObject { ["nullable"] = JValue.CreateNull() } };
                request.EmployeeId = 1; request.EmployeeId = null;
                await Invoke(api, 0L, request, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(request.ContainsKey("employeeId"), Is.False);
                await Invoke(api, 0L, new TimeEntryWriteRequest(), false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo("{}"));
                foreach (var field in Schema().Properties()) request[field.Name] = JValue.CreateNull();
                await Invoke(api, 0L, request, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
            }
            Assert.That(handler.Calls, Is.EqualTo(4));
        }

        [Test]
        public void BothEmployeeOrOrderSelectorsAreRejectedWithoutLookup([Values(false, true)] bool creating, [Values(false, true)] bool employee, [Values(0, 1, 2)] int mode)
        {
            var request = Valid(creating);
            if (employee) { request.EmployeeId = 1; request.EmployeeMail = "fixture@example.invalid"; }
            else { request.OrderId = 2; request.OrderNumber = "AN-2026-1"; }
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task UpdateNullSelectorDoesNotConflictWithItsExplicitAlternative([Values("employeeId", "employeeMail", "orderId", "orderNumber")] string nullField)
        {
            var request = new TimeEntryWriteRequest { [nullField] = JValue.CreateNull() };
            if (nullField == "employeeId") request.EmployeeMail = "fixture@example.invalid";
            if (nullField == "employeeMail") request.EmployeeId = 0;
            if (nullField == "orderId") request.OrderNumber = "0001";
            if (nullField == "orderNumber") request.OrderId = 0;
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) await Invoke(Api(http), 1L, request, false, 2, CancellationToken.None);
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void Int64FieldsCannotRoundOrOverflow([Values("employeeId", "startTs", "endTs", "contactId", "projectId", "taskId", "orderId")] string field,
            [Values("7.25", "\"7\"", "9223372036854775808", "-9223372036854775809")] string json, [Values(false, true)] bool creating)
        {
            var request = Valid(creating); request[field] = JToken.Parse(json); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task PermissionWidthAndInvariantPathArePreserved([Values("-2147483649", "2147483648", "-2147483648", "2147483647", "-1", "3")] string number)
        {
            var request = new TimeEntryWriteRequest { ["permissionType"] = JToken.Parse(number) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            var previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
                using (var http = new HttpClient(handler))
                {
                    if (number == "-2147483649" || number == "2147483648") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); }));
                    else { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/timeentry/-9223372036854775808")); Assert.That(JObject.Parse(handler.Body)["permissionType"].ToString(Formatting.None), Is.EqualTo(number)); }
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
            Assert.That(handler.Calls, Is.EqualTo(number == "-2147483649" || number == "2147483648" ? 0 : 1));
        }

        [Test]
        public async Task MixedCustomValuesAreNotConverted([Values(false, true)] bool creating, [Values("false", "123456789.123456789", "9223372036854775807", "\"aa§§bb\"", "{}", "[]", "null")] string json)
        {
            var request = Valid(creating); request.CustomFields = new JArray(new JObject { ["customFieldName"] = "customText1", ["customFieldValue"] = JsonConvert.DeserializeObject<JToken>(json, new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal }), ["future"] = new JObject() });
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (creating && json == "null") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, 2, CancellationToken.None); }));
                else { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); }
            }
            Assert.That(handler.Calls, Is.EqualTo(creating && json == "null" ? 0 : 1));
        }

        [Test]
        public void InvalidCustomEntriesStopBeforeHttp([Values(false, true)] bool creating, [Values("[]", "[null]", "[false]", "[{}]", "[{\"customFieldName\":4,\"customFieldValue\":false}]", "[{\"customFieldName\":\"x\"}]")] string json)
        {
            var request = Valid(creating); request.CustomFields = JArray.Parse(json); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (json == "[]") { Invoke(Api(http), 1L, request, creating, 0, CancellationToken.None).GetAwaiter().GetResult(); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); }
                else Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); }));
            }
            Assert.That(handler.Calls, Is.EqualTo(json == "[]" ? 1 : 0));
        }

        [Test]
        public async Task HttpInfoEmptyResultsDoNotInventData([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "")));
            using (var http = new HttpClient(handler)) { var info = await Invoke(Api(http), 1L, Valid(creating), creating, mode, CancellationToken.None); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty); }
            Assert.That(handler.Calls, Is.EqualTo(1));
        }

        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static TimeEntryWriteRequest Valid(bool creating) => creating ? new TimeEntryWriteRequest { EmployeeId = 42 } : new TimeEntryWriteRequest();
        private static ProjectApi Api(HttpClient http) => new ProjectApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-time-entry-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ProjectApi api, long id, TimeEntryWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateTimeEntryWithResultWithHttpInfo(request)) : mode == 1 ? api.CreateTimeEntryWithResultAsyncWithHttpInfo(request) : api.CreateTimeEntryWithResultAsyncWithHttpInfo(request, token) :
                mode == 0 ? Task.FromResult(api.UpdateTimeEntryWithResultWithHttpInfo(id, request)) : mode == 1 ? api.UpdateTimeEntryWithResultAsyncWithHttpInfo(id, request) : api.UpdateTimeEntryWithResultAsyncWithHttpInfo(id, request, token);
        private static Task<JToken> InvokeData(ProjectApi api, long id, TimeEntryWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateTimeEntryWithResult(request)) : mode == 1 ? api.CreateTimeEntryWithResultAsync(request) : api.CreateTimeEntryWithResultAsync(request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateTimeEntryWithResult(id, request)) : mode == 1 ? api.UpdateTimeEntryWithResultAsync(id, request) : api.UpdateTimeEntryWithResultAsync(id, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Time-Fixture", "retained"); return response; }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Authorization, Accept, ContentType;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            { Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid")); Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString(); ContentType = request.Content?.Headers.ContentType?.ToString(); Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token); }
        }
    }
}
