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
    public class ContactPropertyWriteTests
    {
        [TestCase("CreateContactPropertyWithResult")]
        [TestCase("UpdateContactPropertyWithResult")]
        public void CompleteCurrentContactPropertyWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ContactApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentContactPropertyWriteRequestIsAvailable()
        {
            Assert.That(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ContactPropertyWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["ContactPropertyForm"]["properties"];
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
        public async Task SnapshotPrecedesActiveHttp([Values(false, true)] bool creating)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously); var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request = Valid(creating); request.Description = "before"; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(creating ? 201 : 200, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.Description = "after"; request["future"]["value"] = 2;
                    Assert.That(JObject.Parse(handler.Body).Value<string>("description"), Is.EqualTo("before")); Assert.That(JObject.Parse(handler.Body)["future"].Value<int>("value"), Is.EqualTo(1));
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
            var handler = new Handler(token => Task.FromResult(Response(status, "original contact-property diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original contact-property diagnostic"));
                var expected = new InvalidOperationException("custom contact-property hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateContactPropertyWithResult" : "UpdateContactPropertyWithResult")); return expected; };
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
            var request = new ContactPropertyWriteRequest { ["future"] = false };
            foreach (var field in Schema().Properties())
            {
                request[field.Name] = JValue.CreateNull();
                typeof(ContactPropertyWriteRequest).GetProperties().Single(p => p.DeclaringType == typeof(ContactPropertyWriteRequest) && p.Name == PropertyName(field.Name)).SetValue(request, null);
                Assert.That(request.ContainsKey(field.Name), Is.False, field.Name);
            }
            Assert.That(request.Count, Is.EqualTo(1));
            Assert.That(request.Value<bool>("future"), Is.False);
        }


        private static string PropertyName(string name) => char.ToUpperInvariant(name[0]) + name.Substring(1);

        [Test]
        public void PinnedContractRequiresBodiesButNoIndividualFields()
        {
            var contract = JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"), Encoding.UTF8).TrimStart('\ufeff'));
            var schema = contract["components"]["schemas"]["ContactPropertyForm"];
            Assert.That(schema["required"], Is.Null);
            foreach (var path in new[] { "/contact/{contactIdOrLegacyNumber}/contactProperty/new", "/contact/contactProperty/{propertyId}" })
            {
                var post = contract["paths"][path]["post"];
                Assert.That(post["requestBody"].Value<bool>("required"), Is.True);
                Assert.That(post["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"), Is.EqualTo("#/components/schemas/ContactPropertyForm"));
                Assert.That(post.Value<string>("description"), Does.Contain("Kontakte bearbeiten (Bearbeiten)"));
                Assert.That(post["responses"][path.EndsWith("/new", StringComparison.Ordinal) ? "201" : "200"]["content"], Is.Null);
            }
            Assert.That(schema["properties"]["spheresDisplay"].Value<string>("example"), Is.EqualTo("Kunde, Lieferant"));
            Assert.That(schema["properties"]["employeeFilterDisplay"]["enum"].Values<string>(), Does.Contain("comma separated list of the employer contact names"));
        }

        [Test]
        public void LegacyModelDefaultsAndEnumsRemainAvailable()
        {
            var legacy = new ContactPropertyForm(spheresDisplay: ContactPropertyForm.SpheresDisplayEnum.Kunde);
            Assert.That(legacy.ValidFrom, Is.EqualTo(0)); Assert.That(legacy.ValidTo, Is.EqualTo(0));
            Assert.That(legacy.EmployeeDomain, Is.False); Assert.That(legacy.PermissionType, Is.EqualTo(0));
            Assert.That(JObject.Parse(legacy.ToJson()).Value<string>("spheresDisplay"), Is.EqualTo("Kunde"));
            Assert.That(typeof(ContactApi).GetMethods().Count(m => m.Name.StartsWith("CreateProperty", StringComparison.Ordinal)), Is.EqualTo(4));
            Assert.That(typeof(ContactApi).GetMethods().Count(m => m.Name.StartsWith("UpdateProperty", StringComparison.Ordinal)), Is.EqualTo(4));
        }

        [Test]
        public void ConstructorCopiesCompleteFutureData()
        {
            var original = new JObject { ["future"] = new JObject { ["value"] = 1 } };
            var request = new ContactPropertyWriteRequest(original); original["future"]["value"] = 2;
            Assert.That(request["future"].Value<int>("value"), Is.EqualTo(1));
            Assert.Throws<ArgumentNullException>((TestDelegate)(() => new ContactPropertyWriteRequest(null)));
        }

        [Test]
        public async Task AllTypedFieldsPreserveCompleteWireAndResult([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode)
        {
            var request = new ContactPropertyWriteRequest(); var expected = new JObject();
            Assert.That(Schema().Count, Is.EqualTo(13));
            Assert.That(typeof(ContactPropertyWriteRequest).GetProperties().Count(p => p.DeclaringType == typeof(ContactPropertyWriteRequest)), Is.EqualTo(13));
            foreach (var field in Schema().Properties())
            {
                var property = typeof(ContactPropertyWriteRequest).GetProperties().Single(p => p.DeclaringType == typeof(ContactPropertyWriteRequest) && p.Name == PropertyName(field.Name)); Assert.That(property, Is.Not.Null, field.Name);
                string kind = field.Value.Value<string>("type");
                object value = kind == "string" ? (object)("  original / " + field.Name) : kind == "boolean" ? (object)false :
                    field.Value.Value<string>("format") == "int64" ? (object)long.MinValue : int.MinValue;
                property.SetValue(request, value); expected[field.Name] = new JValue(value);
            }
            expected["future"] = new JObject { ["nested"] = new JArray(false, 0, JValue.CreateNull()) }; request["future"] = expected["future"].DeepClone();
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, Result)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-contact-property-write-token" };
                var api = new ContactApi(config); var info = await Invoke(api, long.MaxValue, request, creating, mode, CancellationToken.None);
                Assert.That(api.Configuration, Is.SameAs(config)); Assert.That(config.HttpClient, Is.SameAs(http));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(creating ? "/rest/contact/9223372036854775807/contactProperty/new" : "/rest/contact/contactProperty/9223372036854775807"));
                Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-contact-property-write-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json")); Assert.That(handler.ContentType, Does.StartWith("application/json"));
                Assert.That(handler.Body, Is.EqualTo(expected.ToString(Formatting.None))); Assert.That(info.StatusCode, Is.EqualTo(creating ? 201 : 200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Result)); Assert.That(info.Headers["X-Property-Fixture"], Is.EqualTo("retained"));
                Assert.That(info.Headers["Content-Type"], Does.StartWith("application/json")); Assert.That(info.Data["id"].Value<long>(), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["fraction"].Value<decimal>(), Is.EqualTo(123456789.123456789m)); Assert.That(info.Data["date"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1)); Assert.That(config.AccessToken, Is.EqualTo("isolated-contact-property-write-token"));
            }
        }

        [Test]
        public async Task RequiredBodyHasNoInventedFieldDefaults([Values(false, true)] bool creating, [Values(0, 1, 2)] int mode, [Values(false, true)] bool absent)
        {
            var handler = new Handler(token => Task.FromResult(Response(creating ? 201 : 200, "")));
            using (var http = new HttpClient(handler))
            {
                if (absent) Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async () => { await Invoke(Api(http), 0, null, creating, mode, CancellationToken.None); }));
                else { var info = await Invoke(Api(http), 0, new ContactPropertyWriteRequest(), creating, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo("{}")); Assert.That(info.Data, Is.Null); Assert.That(info.RawJsonContent, Is.Empty); }
            }
            Assert.That(handler.Calls, Is.EqualTo(absent ? 0 : 1));
        }

        public static IEnumerable<TestCaseData> NullFields()
        {
            foreach (var field in Schema().Properties()) foreach (var creating in new[] { false, true })
                foreach (var mode in new[] { 0, 1, 2 }) yield return new TestCaseData(field.Name, creating, mode);
        }

        [TestCaseSource(nameof(NullFields))]
        public async Task ExplicitNullCanOnlyReachUpdate(string field, bool creating, int mode)
        {
            var request = new ContactPropertyWriteRequest { [field] = JValue.CreateNull() };
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler))
            {
                if (creating) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1, request, true, mode, CancellationToken.None); }));
                else { await Invoke(Api(http), 1, request, false, mode, CancellationToken.None); Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); }
            }
            Assert.That(handler.Calls, Is.EqualTo(creating ? 0 : 1));
        }

        [Test]
        public void TimestampWidthRejectsCoercion([Values("validFrom", "validTo")] string field,
            [Values("7.25", "\"7\"", "9223372036854775808", "-9223372036854775809")] string json, [Values(false, true)] bool creating)
        {
            var request = new ContactPropertyWriteRequest { [field] = JToken.Parse(json) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), 1, request, creating, 2, CancellationToken.None); })); Assert.That(handler.Calls, Is.Zero);
        }

        [Test]
        public async Task PermissionWidthAndInvariantPropertyIdArePreserved([Values("-2147483649", "2147483648", "-2147483648", "2147483647", "-1", "3")] string number)
        {
            var request = new ContactPropertyWriteRequest { ["permissionType"] = JToken.Parse(number) }; var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            var previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
                using (var http = new HttpClient(handler))
                {
                    if (number == "-2147483649" || number == "2147483648") Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async () => { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); }));
                    else { await Invoke(Api(http), long.MinValue, request, false, 2, CancellationToken.None); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/contact/contactProperty/-9223372036854775808")); Assert.That(JObject.Parse(handler.Body)["permissionType"].ToString(Formatting.None), Is.EqualTo(number)); }
                }
            }
            finally { CultureInfo.CurrentCulture = previous; }
            Assert.That(handler.Calls, Is.EqualTo(number == "-2147483649" || number == "2147483648" ? 0 : 1));
        }

        [Test]
        public async Task MultiSphereAndEmployerNamesRemainOpenStrings([Values(false, true)] bool creating)
        {
            var request = new ContactPropertyWriteRequest { SpheresDisplay = "Kunde, Lieferant", EmployeeFilterDisplay = "Demo AG, Arbeitgeber / Zwei", Description = "", ValidFrom = 0, ValidTo = long.MaxValue, EmployeeDomain = false, PermissionType = 1, ReadPermissionProfiles = "", WritePermissionLogins = " original " };
            var handler = new Handler(token => Task.FromResult(Response(200, "{}")));
            using (var http = new HttpClient(handler)) await Invoke(Api(http), 1, request, creating, 2, CancellationToken.None);
            Assert.That(handler.Body, Is.EqualTo(request.ToString(Formatting.None))); Assert.That(handler.Calls, Is.EqualTo(1));
        }

        [Test]
        public async Task ContactSelectorIsOriginalSingleSegment([Values(null, ".", "..", "0000007", "", "  ", "../next/§ ?#%", "a%2Fb")] string selector, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(201, "{}")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                Func<Task> invoke = async () => { if (mode == 0) api.CreateContactPropertyWithResultWithHttpInfo(selector, new ContactPropertyWriteRequest()); else if (mode == 1) await api.CreateContactPropertyWithResultAsyncWithHttpInfo(selector, new ContactPropertyWriteRequest()); else await api.CreateContactPropertyWithResultAsyncWithHttpInfo(selector, new ContactPropertyWriteRequest(), CancellationToken.None); };
                if (selector == null) Assert.ThrowsAsync<ArgumentNullException>(invoke);
                else if (selector == "." || selector == "..") Assert.ThrowsAsync<ArgumentException>(invoke);
                else { await invoke(); Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/contact/" + Uri.EscapeDataString(selector) + "/contactProperty/new")); Assert.That(handler.Uri.Query, Is.Empty); Assert.That(handler.Uri.Fragment, Is.Empty); }
            }
            Assert.That(handler.Calls, Is.EqualTo(selector == null || selector == "." || selector == ".." ? 0 : 1));
        }

        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static ContactPropertyWriteRequest Valid(bool creating) => new ContactPropertyWriteRequest();
        private static ContactApi Api(HttpClient http) => new ContactApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-contact-property-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ContactApi api, long id, ContactPropertyWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateContactPropertyWithResultWithHttpInfo(id.ToString(CultureInfo.InvariantCulture), request)) : mode == 1 ? api.CreateContactPropertyWithResultAsyncWithHttpInfo(id.ToString(CultureInfo.InvariantCulture), request) : api.CreateContactPropertyWithResultAsyncWithHttpInfo(id.ToString(CultureInfo.InvariantCulture), request, token) :
                mode == 0 ? Task.FromResult(api.UpdateContactPropertyWithResultWithHttpInfo(id, request)) : mode == 1 ? api.UpdateContactPropertyWithResultAsyncWithHttpInfo(id, request) : api.UpdateContactPropertyWithResultAsyncWithHttpInfo(id, request, token);
        private static Task<JToken> InvokeData(ContactApi api, long id, ContactPropertyWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateContactPropertyWithResult(id.ToString(CultureInfo.InvariantCulture), request)) : mode == 1 ? api.CreateContactPropertyWithResultAsync(id.ToString(CultureInfo.InvariantCulture), request) : api.CreateContactPropertyWithResultAsync(id.ToString(CultureInfo.InvariantCulture), request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateContactPropertyWithResult(id, request)) : mode == 1 ? api.UpdateContactPropertyWithResultAsync(id, request) : api.UpdateContactPropertyWithResultAsync(id, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Property-Fixture", "retained"); return response; }
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
