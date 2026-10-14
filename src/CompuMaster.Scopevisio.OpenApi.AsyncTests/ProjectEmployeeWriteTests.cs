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
    public class ProjectEmployeeWriteTests
    {
        [TestCase("CreateProjectEmployeeWithResult")]
        [TestCase("UpdateProjectEmployeeWithResult")]
        public void CompleteCurrentProjectEmployeeWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ProjectApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentProjectEmployeeWriteRequestIsAvailable()
        {
            Assert.That(typeof(ProjectApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ProjectEmployeeWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["ProjectEmployeeForm"]["properties"];
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
            var request = Valid(creating); request.Role = "before"; request["future"] = new JObject { ["value"] = 1 };
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(creating ? 201 : 200, "{}"); });
            using (var http = new HttpClient(handler))
            {
                var operation = Invoke(Api(http), 1L, request, creating, 2, CancellationToken.None);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task)); request.Role = "after"; request["future"]["value"] = 2;
                    Assert.That(JObject.Parse(handler.Body).Value<string>("role"), Is.EqualTo("before")); Assert.That(JObject.Parse(handler.Body)["future"].Value<int>("value"), Is.EqualTo(1));
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
            var handler = new Handler(token => Task.FromResult(Response(status, "original project-team-entry diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original project-team-entry diagnostic"));
                var expected = new InvalidOperationException("custom project-team-entry hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateProjectEmployeeWithResult" : "UpdateProjectEmployeeWithResult")); return expected; };
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
            var request = new ProjectEmployeeWriteRequest { ["future"] = false };
            foreach (var field in Schema().Properties())
            {
                request[field.Name] = JValue.CreateNull();
                typeof(ProjectEmployeeWriteRequest).GetProperties().Single(p => p.DeclaringType == typeof(ProjectEmployeeWriteRequest) && p.Name == PropertyName(field.Name)).SetValue(request, null);
                Assert.That(request.ContainsKey(field.Name), Is.False, field.Name);
            }
            Assert.That(request.Count, Is.EqualTo(1));
            Assert.That(request.Value<bool>("future"), Is.False);
        }



        private static string PropertyName(string name) => char.ToUpperInvariant(name[0]) + name.Substring(1);

        [Test]
        public void PinnedContractDistinguishesContactIdAndTeamEntryId()
        {
            var contract=JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"),Encoding.UTF8).TrimStart('\ufeff'));
            var schema=contract["components"]["schemas"]["ProjectEmployeeForm"];
            Assert.That(schema["required"].Values<string>(), Is.EquivalentTo(new[]{"contactId"}));
            Assert.That(schema["properties"]["role"].Value<string>("description"), Does.Contain("catalog"));
            foreach(var path in new[]{"/project/{projectId}/projectemployee/new","/project/{projectId}/projectemployee/{employeeId}"})
            {
                var post=contract["paths"][path]["post"];
                Assert.That(post["requestBody"].Value<bool>("required"),Is.True);
                Assert.That(post["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Is.EqualTo("#/components/schemas/ProjectEmployeeForm"));
                Assert.That(post.Value<string>("description"),Does.Contain("Kontakte, Projekte (Bearbeiten)"));
                Assert.That(post["responses"][path.EndsWith("/new",StringComparison.Ordinal)?"201":"200"]["content"],Is.Null);
                foreach(var selector in post["parameters"])
                { Assert.That(selector.Value<bool>("required"),Is.True); Assert.That(selector["schema"].Value<string>("format"),Is.EqualTo("int64")); }
            }
            Assert.That(contract["paths"]["/project/{projectId}/projectemployee/{employeeId}"]["post"].Value<string>("summary"),Does.Contain("not contactId"));
        }

        [Test]
        public void LegacyDefaultsAndApisRemainAvailable()
        {
            var legacy=new ProjectEmployeeForm(7,role:ProjectEmployeeForm.RoleEnum.Projektleitung);
            Assert.That(legacy.ContactId,Is.EqualTo(7)); Assert.That(legacy.HourlyRate,Is.EqualTo(0)); Assert.That(legacy.DailyRate,Is.EqualTo(0));
            Assert.That(JObject.Parse(legacy.ToJson()).Value<string>("role"),Is.EqualTo("Projektleitung"));
            foreach(var name in new[]{"CreateProjectEmployee","UpdateProjectEmployee"})
                Assert.That(typeof(ProjectApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)&&!m.Name.StartsWith(name+"WithResult",StringComparison.Ordinal)),Is.EqualTo(4));
        }

        [Test]
        public void ConstructorCopiesCompleteFutureData()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}}; var request=new ProjectEmployeeWriteRequest(original); original["future"]["value"]=2;
            Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1)); Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new ProjectEmployeeWriteRequest(null)));
        }

        [Test]
        public async Task AllTypedFieldsPreserveCompleteWireAndResult([Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var request=new ProjectEmployeeWriteRequest(); var expected=new JObject();
            Assert.That(Schema().Count,Is.EqualTo(5)); Assert.That(typeof(ProjectEmployeeWriteRequest).GetProperties().Count(p=>p.DeclaringType==typeof(ProjectEmployeeWriteRequest)),Is.EqualTo(5));
            foreach(var field in Schema().Properties())
            {
                var property=typeof(ProjectEmployeeWriteRequest).GetProperties().Single(p=>p.DeclaringType==typeof(ProjectEmployeeWriteRequest)&&p.Name==PropertyName(field.Name));
                string kind=field.Value.Value<string>("type"); object value=kind=="string"?(object)("  original / "+field.Name):kind=="integer"?(object)long.MinValue:123456789.123456789m;
                property.SetValue(request,value); expected[field.Name]=new JValue(value);
            }
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())}; request["future"]=expected["future"].DeepClone();
            var handler=new Handler(token=>Task.FromResult(Response(creating?201:200,Result)));
            using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-project-team-entry-write-token"}; var api=new ProjectApi(config);
                var info=await Invoke(api,long.MaxValue,request,creating,mode,CancellationToken.None);
                Assert.That(api.Configuration,Is.SameAs(config)); Assert.That(config.HttpClient,Is.SameAs(http));
                Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post)); Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(creating?"/rest/project/9223372036854775807/projectemployee/new":"/rest/project/9223372036854775807/projectemployee/-9223372036854775808"));
                Assert.That(handler.Uri.Query,Is.Empty); Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-project-team-entry-write-token"));
                Assert.That(handler.Accept,Is.EqualTo("application/json")); Assert.That(handler.ContentType,Does.StartWith("application/json"));
                Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None))); Assert.That(info.StatusCode,Is.EqualTo(creating?201:200));
                Assert.That(info.RawJsonContent,Is.EqualTo(Result)); Assert.That(info.Headers["X-Employee-Fixture"],Is.EqualTo("retained")); Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));
                Assert.That(info.Data["id"].Value<long>(),Is.EqualTo(long.MaxValue)); Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m)); Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls,Is.EqualTo(1)); Assert.That(config.AccessToken,Is.EqualTo("isolated-project-team-entry-write-token"));
            }
        }

        [Test]
        public async Task BothBodiesRequiredAndCreationContactIsExplicit([Values(false,true)]bool creating,[Values(0,1,2)]int mode,[Values("null-body","empty","zero","null-contact","contact")]string selection)
        {
            var request=selection=="null-body"?null:new ProjectEmployeeWriteRequest();
            if(selection=="zero")request.ContactId=0; if(selection=="contact")request.ContactId=long.MaxValue; if(selection=="null-contact")request["contactId"]=JValue.CreateNull();
            var handler=new Handler(token=>Task.FromResult(Response(creating?201:200,"")));
            bool invalid=selection=="null-body"||(creating&&(selection=="empty"||selection=="null-contact"));
            using(var http=new HttpClient(handler))
            {
                if(selection=="null-body")Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),0,request,creating,mode,CancellationToken.None);}));
                else if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),0,request,creating,mode,CancellationToken.None);}));
                else {var info=await Invoke(Api(http),0,request,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [TestCaseSource(nameof(Fields))]
        public async Task ExplicitNullReachesOnlyUpdate(string field,bool creating,int mode)
        {
            var request=Valid(creating);request[field]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))
            {
                if(creating)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,true,mode,CancellationToken.None);}));
                else {await Invoke(Api(http),1,request,false,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(creating?0:1));
        }

        [Test]
        public void PersonalContactWidthRejectsCoercion([Values("7.25","\"7\"","9223372036854775808","-9223372036854775809")]string json,[Values(false,true)]bool creating)
        {
            var request=Valid(creating);request["contactId"]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task RatesRetainRawNumericTokens([Values(false,true)]bool creating,[Values("hourlyRate","dailyRate")]string field,
            [Values("0","-12.75","123456789.123456789","92233720368547758081234567890","1e100")]string json)
        {
            var request=Valid(creating);request[field]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=json=="1e100"?FloatParseHandling.Double:FloatParseHandling.Decimal});
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,creating,2,CancellationToken.None);
            Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(JObject.Parse(handler.Body)[field].Type,Is.AnyOf(JTokenType.Integer,JTokenType.Float));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void NonFiniteRatesStopBeforeHttp([Values(false,true)]bool creating,[Values("hourlyRate","dailyRate")]string field,[Values(0,1,2,3,4,5)]int selection,[Values(0,1,2)]int mode)
        {
            var request=Valid(creating);object value=selection==0?(object)double.NaN:selection==1?(object)double.PositiveInfinity:selection==2?(object)double.NegativeInfinity:selection==3?(object)float.NaN:selection==4?(object)float.PositiveInfinity:float.NegativeInfinity;
            request[field]=new JValue(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task ConfiguredRoleAndSparseRatesRemainOriginal([Values(false,true)]bool creating)
        {
            var request=Valid(creating);request.Role="  Konto-spezifische Rolle / § ";request.ContactSphere="Lieferant";request.HourlyRate=0;request.DailyRate=0.01m;
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,creating,2,CancellationToken.None);
            Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task ExplicitProjectAndTeamEntryIdsAreInvariant([Values(0,1,2)]int mode,[Values(long.MinValue,0L,long.MaxValue)]long projectId,[Values(long.MinValue,0L,long.MaxValue)]long employeeId)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));var previous=CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture=new CultureInfo("ar-SA");
                using(var http=new HttpClient(handler))
                {
                    var api=Api(http);var request=new ProjectEmployeeWriteRequest{ContactId=42};
                    if(mode==0)api.UpdateProjectEmployeeWithResultWithHttpInfo(projectId,employeeId,request);
                    else if(mode==1)await api.UpdateProjectEmployeeWithResultAsyncWithHttpInfo(projectId,employeeId,request);
                    else await api.UpdateProjectEmployeeWithResultAsyncWithHttpInfo(projectId,employeeId,request,CancellationToken.None);
                    Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/project/"+projectId.ToString(CultureInfo.InvariantCulture)+"/projectemployee/"+employeeId.ToString(CultureInfo.InvariantCulture)));
                    Assert.That(handler.Body,Is.EqualTo("{\"contactId\":42}"));Assert.That(handler.Calls,Is.EqualTo(1));
                }
            }
            finally {CultureInfo.CurrentCulture=previous;}
        }

        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static ProjectEmployeeWriteRequest Valid(bool creating) => creating ? new ProjectEmployeeWriteRequest { ContactId = 42 } : new ProjectEmployeeWriteRequest();
        private static ProjectApi Api(HttpClient http) => new ProjectApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-project-team-entry-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ProjectApi api, long id, ProjectEmployeeWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProjectEmployeeWithResultWithHttpInfo(id, request)) : mode == 1 ? api.CreateProjectEmployeeWithResultAsyncWithHttpInfo(id, request) : api.CreateProjectEmployeeWithResultAsyncWithHttpInfo(id, request, token) :
                mode == 0 ? Task.FromResult(api.UpdateProjectEmployeeWithResultWithHttpInfo(id, long.MinValue, request)) : mode == 1 ? api.UpdateProjectEmployeeWithResultAsyncWithHttpInfo(id, long.MinValue, request) : api.UpdateProjectEmployeeWithResultAsyncWithHttpInfo(id, long.MinValue, request, token);
        private static Task<JToken> InvokeData(ProjectApi api, long id, ProjectEmployeeWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProjectEmployeeWithResult(id, request)) : mode == 1 ? api.CreateProjectEmployeeWithResultAsync(id, request) : api.CreateProjectEmployeeWithResultAsync(id, request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateProjectEmployeeWithResult(id, long.MinValue, request)) : mode == 1 ? api.UpdateProjectEmployeeWithResultAsync(id, long.MinValue, request) : api.UpdateProjectEmployeeWithResultAsync(id, long.MinValue, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Employee-Fixture", "retained"); return response; }
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
