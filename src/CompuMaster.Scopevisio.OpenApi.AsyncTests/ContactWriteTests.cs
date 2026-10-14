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
    public class ContactWriteTests
    {
        [TestCase("CreateContactWithResult")]
        [TestCase("UpdateContactByIdWithResult")]
        public void CompleteCurrentContactWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ContactApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentContactWriteRequestIsAvailable()
        {
            Assert.That(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ContactWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["KontaktForm"]["properties"];
        }
        public static IEnumerable<TestCaseData> Fields()
        {
            foreach (var field in Schema().Properties())
                foreach (var creating in new[] { false, true })
                    foreach (var mode in new[] { 0, 1, 2 })
                        yield return new TestCaseData(field.Name, creating, mode);
        }

        [Test]
        public async Task All108CurrentTypedFieldsRetainExactWireValues([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var request = new ContactWriteRequest(); var expected = new JObject();
            foreach (var field in Schema().Properties())
            {
                string kind = field.Value.Value<string>("type");
                object value = kind == "string" ? (object)("  original / " + field.Name) : kind == "boolean" ? (object)false : kind == "integer" ? field.Value.Value<string>("format") == "int64" ? (object)long.MinValue : int.MinValue :
                    field.Name == "customFields" ? (object)new JArray(new JObject { ["customFieldName"] = "customAmount1", ["customFieldValue"] = 123456789.123456789m, ["future"] = false }) :
                    new JArray(new JObject { ["employerId"] = long.MinValue, ["legacyNumber"] = "0001", ["companyType"] = "original catalogue", ["companyDocumentVisibility"] = false, ["validFrom"] = long.MinValue, ["validTo"] = long.MaxValue, ["future"] = new JObject() });
                var property = typeof(ContactWriteRequest).GetProperty(char.ToUpperInvariant(field.Name[0]) + field.Name.Substring(1));
                Assert.That(property, Is.Not.Null, field.Name); property.SetValue(request, value);
                expected[field.Name] = value is JToken token ? token.DeepClone() : new JValue(value);
            }
            Assert.That(expected.Count, Is.EqualTo(108));
            Assert.That(typeof(ContactWriteRequest).GetProperties().Count(p => p.DeclaringType == typeof(ContactWriteRequest)), Is.EqualTo(108));
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, Result)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var info = await Invoke(api, long.MaxValue, request, creating, mode, CancellationToken.None);
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(creating ? "/rest/contact/new" : "/rest/contact/9223372036854775807"));
                Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-contact-write-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(handler.Body, Is.EqualTo(expected.ToString(Formatting.None))); Assert.That(info.StatusCode, Is.EqualTo(creating ? 201 : 200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Result)); Assert.That(info.Headers["X-Contact-Fixture"], Is.EqualTo("retained"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json")); Assert.That(info.Data["id"].Value<long>(), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["fraction"].Value<decimal>(), Is.EqualTo(123456789.123456789m)); Assert.That(info.Data["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1)); Assert.That(api.Configuration.AccessToken, Is.EqualTo("isolated-contact-write-token"));
            }
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
        public async Task OptionalBodyAndSparseUpdatesDoNotInsertDefaults([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "")));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), 0L, null, creating, mode, CancellationToken.None);
                Assert.That(handler.Body, Is.Null); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty);
                if (!creating)
                {
                    var request = new ContactWriteRequest { SphereWorker = false, PersonalDataInsuranceNumber = "000123", Lastname = "" };
                    request["customerPersonId"] = JValue.CreateNull(); request["future"] = new JObject { ["nullable"] = JValue.CreateNull() };
                    request.Person = true; request.Person = null;
                    await Invoke(Api(http), 0L, request, false, mode, CancellationToken.None);
                    Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(request.ContainsKey("person"), Is.False);
                    await Invoke(Api(http), 0L, new ContactWriteRequest(), false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo("{}"));
                }
            }
        }

        [Test]
        public async Task ExplicitUpdateNullIsNotReplacedByDefaults([Values(0, 1, 2)] int mode)
        {
            var request = new ContactWriteRequest(); foreach (var field in Schema().Properties()) request[field.Name] = JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), 1L, request, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
                Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, mode, CancellationToken.None); })); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void SuppliedCreationObjectRequiresLastnameAndPerson([Values("empty", "lastname-missing", "lastname-null", "person-missing", "person-null")] string mode)
        {
            var request = mode == "empty" ? new ContactWriteRequest() : Valid(true);
            if (mode == "lastname-missing") request.Remove("lastname"); if (mode == "lastname-null") request["lastname"] = JValue.CreateNull();
            if (mode == "person-missing") request.Remove("person"); if (mode == "person-null") request["person"] = JValue.CreateNull();
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, true, 2, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task CreationRetainsExplicitFalseAndUnnormalizedName([Values("", "  ")] string lastname)
        {
            var request = new ContactWriteRequest { Lastname = lastname, Person = false }; var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler)) await Invoke(Api(http), 0L, request, true, 2, CancellationToken.None);
            Assert.That(JObject.Parse(handler.Body).Value<string>("lastname"), Is.EqualTo(lastname)); Assert.That(JObject.Parse(handler.Body).Value<bool>("person"), Is.False);
        }

        [Test]
        public void IntegersCannotRoundOrOverflow([Values("customerPersonId", "personalDataStart", "personalDataEnd")] string field,
            [Values("7.25", "\"7\"", "9223372036854775808", "-9223372036854775809")] string json, [Values(false, true)] bool creating)
        {
            var request = Valid(creating); request[field] = JToken.Parse(json); var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); }));
            Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task Int32WidthAndInvariantPathArePreserved([Values("-2147483649", "2147483648", "-2147483648", "2147483647")] string number)
        {
            string field = Schema().Properties().Single(p => p.Value.Value<string>("format") == "int32").Name;
            var request = new ContactWriteRequest { [field] = JToken.Parse(number) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            var previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
                using (var http = new HttpClient(handler))
                {
                    if (number == "-2147483649" || number == "2147483648") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); }));
                    else { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/contact/-9223372036854775808")); Assert.That(JObject.Parse(handler.Body)[field].ToString(Formatting.None), Is.EqualTo(number)); }
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
        }

        [Test]
        public void ConstructorAndArraySettersCopyInput()
        {
            var original = new JObject { ["future"] = new JObject { ["value"] = 1 } }; var request = new ContactWriteRequest(original);
            original["future"]["value"] = 2; Assert.That(request["future"]["value"].Value<int>(), Is.EqualTo(1));
            var custom = new JArray(new JObject { ["customFieldName"] = "customBoolean1", ["customFieldValue"] = false }); request.CustomFields = custom;
            custom[0]["customFieldValue"] = true; Assert.That(request.CustomFields[0].Value<bool>("customFieldValue"), Is.False);
            request.CustomFields = null; Assert.That(request.ContainsKey("customFields"), Is.False);
            Assert.Throws<ArgumentNullException>((TestDelegate)(() => new ContactWriteRequest(null)));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request = Valid(creating); request.PersonalDataInsuranceNumber = "before"; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(creating ? 201 : 200, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.PersonalDataInsuranceNumber = "after"; request["future"]["value"] = 2;
                    Assert.That(JObject.Parse(handler.Body).Value<string>("personalDataInsuranceNumber"), Is.EqualTo("before")); Assert.That(JObject.Parse(handler.Body)["future"].Value<int>("value"), Is.EqualTo(1));
                }
                finally { release.TrySetResult(true); }
                await operation;
            }
        }

        [Test]
        public async Task CatalogueStringsAndMixedCustomValuesRemainOriginal([Values(false, true)] bool creating,
            [Values("\"aa§§bb\"", "123456789.123456789", "false", "1735689600000")] string value)
        {
            var request = Valid(creating); request.PersonalDataType = "Abteilungsleiter"; request.Position = "Geschäftsführer"; request.PersonalDataInsuranceType = "future catalogue value";
            request.CustomFields = new JArray(new JObject { ["customFieldName"] = "customText7", ["customFieldValue"] = JToken.Parse(value), ["future"] = false });
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "{}")));
            using (var http = new HttpClient(handler)) await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None)));
        }

        [Test]
        public void KnownNestedShapesFailBeforeHttp([Values("array-scalar", "custom-no-name", "custom-no-value", "employer-no-selector", "employer-fraction", "employer-overflow", "employer-boolean-kind")] string mode, [Values(false, true)] bool creating)
        {
            var request = Valid(creating);
            request[mode.StartsWith("employer", StringComparison.Ordinal) ? "employers" : "customFields"] = mode == "array-scalar" ? new JArray(false) :
                mode == "custom-no-name" ? new JArray(new JObject { ["customFieldValue"] = "x" }) : mode == "custom-no-value" ? new JArray(new JObject { ["customFieldName"] = "customText1" }) :
                mode == "employer-no-selector" ? new JArray(new JObject()) : mode == "employer-fraction" ? new JArray(new JObject { ["employerId"] = 7.25m }) :
                mode == "employer-overflow" ? new JArray(new JObject { ["employerId"] = 1, ["validFrom"] = JToken.Parse("9223372036854775808") }) :
                new JArray(new JObject { ["legacyNumber"] = "0001", ["companyCommentVisibility"] = "false" });
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task CompleteResultFormsRetainOriginalJsonWithoutInventedValues([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode, [Values("{}", "[]", "null", "true", "{\"warnings\":[\"inspect\"]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, json)));
            using (var http = new HttpClient(handler))
            {
                var data = await InvokeData(Api(http), -1L, null, creating, mode); Assert.That(data.ToString(Formatting.None), Is.EqualTo(json)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void OriginalFailureAndFactoryPrecedeJson([Values(false, true)] bool creating, [Values(0, 2)] int mode, [Values(400, 401, 403, 404, 500)] int status)
        {
            var handler = new Handler(token => Task.FromResult(Response(status, "original contact diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, null, creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original contact diagnostic"));
                var expected = new InvalidOperationException("custom contact hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateContactWithResult" : "UpdateContactByIdWithResult")); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => { await Invoke(api, 1L, null, creating, mode, CancellationToken.None); })), Is.SameAs(expected)); Assert.That(handler.Calls, Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInventedSuccess([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "{invalid")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, null, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public void PreCancelledWriteHasNoHttpEffects([Values(false, true)] bool creating)
        {
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            { cancel.Cancel(); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, null, creating, 2, cancel.Token); })); Assert.That(handler.Calls, Is.Zero); }
        }

        [Test]
        public async Task ActiveWriteCancellationDoesNotReplay([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(200, "{}"); });
            using (var http = new HttpClient(handler)) using (var cancel = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), 1L, null, creating, 2, cancel.Token);
                try { Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); cancel.Cancel(); Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation)); Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; })); Assert.That(handler.Calls, Is.EqualTo(1)); }
                finally { cancel.Cancel(); }
            }
        }


        [Test]
        public void TypedNullSettersOmitEveryKnownField()
        {
            var request = new ContactWriteRequest { ["future"] = false };
            foreach (var field in Schema().Properties())
            {
                request[field.Name] = JValue.CreateNull();
                typeof(ContactWriteRequest).GetProperty(char.ToUpperInvariant(field.Name[0]) + field.Name.Substring(1)).SetValue(request, null);
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


        [Test]
        public async Task DocumentedCreationResultSurvivesAllSixForms([Values(0, 1, 2, 3, 4, 5)] int form)
        {
            const string json = "{\"status\":2,\"contactId\":9223372036854775807,\"location\":\"/contact/9223372036854775807\",\"errors\":{\"original\":[\"inspect\"],\"nested\":{\"code\":400}},\"future\":{\"retained\":true}}";
            var handler = new Handler(token => Task.FromResult(Response(201, json)));
            using (var http = new HttpClient(handler))
            {
                JToken data;
                if (form < 3)
                {
                    var info = await Invoke(Api(http), 0L, Valid(true), true, form, CancellationToken.None);
                    Assert.That(info.StatusCode, Is.EqualTo(201)); Assert.That(info.RawJsonContent, Is.EqualTo(json));
                    Assert.That(info.Headers["X-Contact-Fixture"], Is.EqualTo("retained")); data = info.Data;
                }
                else data = await InvokeData(Api(http), 0L, Valid(true), true, form - 3);
                Assert.That(data.Value<int>("status"), Is.EqualTo(2));
                Assert.That(data.Value<long>("contactId"), Is.EqualTo(long.MaxValue));
                Assert.That(data.Value<string>("location"), Is.EqualTo("/contact/9223372036854775807"));
                Assert.That(data["errors"]["nested"].Value<int>("code"), Is.EqualTo(400));
                Assert.That(data["errors"]["original"][0].Value<string>(), Is.EqualTo("inspect"));
                Assert.That(data["future"].Value<bool>("retained"), Is.True);
                Assert.That(data["success"], Is.Null); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void PermissionSemanticRangeRemainsServerAuthoritative([Values(-1, 3)] int permission)
        {
            var request = new ContactWriteRequest { PermissionType = permission };
            var handler = new Handler(token => Task.FromResult(Response(400, "original permission diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(Api(http), 1L, request, false, 2, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(400)); Assert.That((object)error.ErrorContent, Is.EqualTo("original permission diagnostic"));
                Assert.That(JObject.Parse(handler.Body).Value<int>("permissionType"), Is.EqualTo(permission));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static ContactWriteRequest Valid(bool creating) => creating ? new ContactWriteRequest { Lastname = "fixture contact", Person = false } : new ContactWriteRequest();
        private static ContactApi Api(HttpClient http) => new ContactApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-contact-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ContactApi api, long id, ContactWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateContactWithResultWithHttpInfo(request)) : mode == 1 ? api.CreateContactWithResultAsyncWithHttpInfo(request) : api.CreateContactWithResultAsyncWithHttpInfo(request, token) :
                mode == 0 ? Task.FromResult(api.UpdateContactByIdWithResultWithHttpInfo(id, request)) : mode == 1 ? api.UpdateContactByIdWithResultAsyncWithHttpInfo(id, request) : api.UpdateContactByIdWithResultAsyncWithHttpInfo(id, request, token);
        private static Task<JToken> InvokeData(ContactApi api, long id, ContactWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateContactWithResult(request)) : mode == 1 ? api.CreateContactWithResultAsync(request) : api.CreateContactWithResultAsync(request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateContactByIdWithResult(id, request)) : mode == 1 ? api.UpdateContactByIdWithResultAsync(id, request) : api.UpdateContactByIdWithResultAsync(id, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Contact-Fixture", "retained"); return response; }
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
