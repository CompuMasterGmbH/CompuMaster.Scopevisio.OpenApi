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
    public class ProjectWriteTests
    {
        [TestCase("CreateProjectWithResult")]
        [TestCase("UpdateProjectWithResult")]
        public void CompleteCurrentProjectWriteFormsAreAvailable(string name)
        {
            Assert.That(typeof(ProjectApi).GetMethods().Count(method => method.Name.StartsWith(name, StringComparison.Ordinal)), Is.EqualTo(6));
        }

        [Test]
        public void CurrentProjectWriteRequestIsAvailable()
        {
            Assert.That(typeof(ProjectApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.ProjectWriteRequest"), Is.Not.Null);
        }

        private static JObject Schema()
        {
            var bytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "openscope-contact-contract.json"));
            using (var hash = SHA256.Create()) Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(), Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return (JObject)JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["ProjectForm"]["properties"];
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
            var handler = new Handler(token => Task.FromResult(Response(status, "original project diagnostic")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, 1L, Valid(creating), creating, mode, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(status)); Assert.That((object)error.ErrorContent, Is.EqualTo("original project diagnostic"));
                var expected = new InvalidOperationException("custom project hook"); api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(creating ? "CreateProjectWithResult" : "UpdateProjectWithResult")); return expected; };
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
            var request = new ProjectWriteRequest { ["future"] = false };
            foreach (var field in Schema().Properties())
            {
                request[field.Name] = JValue.CreateNull();
                typeof(ProjectWriteRequest).GetProperties().Single(p => p.DeclaringType == typeof(ProjectWriteRequest) && p.Name == PropertyName(field.Name)).SetValue(request, null);
                Assert.That(request.ContainsKey(field.Name), Is.False, field.Name);
            }
            Assert.That(request.Count, Is.EqualTo(1));
            Assert.That(request.Value<bool>("future"), Is.False);
        }




        private static string PropertyName(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static JArray Custom()=>new JArray(new JObject{["customFieldName"]="customAmount1",["customFieldValue"]=123456789.123456789m,["future"]=false});
        private static JArray Employees()=>new JArray(new JObject{["contactId"]=long.MaxValue,["role"]="Configured / role",["hourlyRate"]=123456789.123456789m,["future"]=new JObject{["nested"]=false}});

        [Test]
        public void PinnedOptionalBodiesAndCreationResponseRemainDistinct()
        {
            var contract=JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"),Encoding.UTF8).TrimStart('\ufeff'));
            Assert.That(contract["components"]["schemas"]["ProjectForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"name","contactId"}));
            foreach(var path in new[]{"/project/new","/project/{id}"})
            {
                var post=contract["paths"][path]["post"];Assert.That(post["requestBody"]["required"],Is.Null);Assert.That(post.Value<string>("description"),Does.Contain("Projekte (Bearbeiten)"));
                Assert.That(post["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Is.EqualTo("#/components/schemas/ProjectForm"));
            }
            Assert.That(contract["paths"]["/project/new"]["post"]["responses"]["201"]["content"]["application/json"]["schema"].Value<string>("$ref"),Is.EqualTo("#/components/schemas/ProjectNewResponse"));
            var response=contract["components"]["schemas"]["ProjectNewResponse"];Assert.That(response["required"],Is.Null);Assert.That(((JObject)response["properties"]).Properties().Select(p=>p.Name),Is.EquivalentTo(new[]{"status","projectId","location","errors"}));
            Assert.That(contract["paths"]["/project/{id}"]["post"]["responses"]["200"]["content"],Is.Null);
        }

        [Test]
        public void LegacyDefaultsTypesAndApiRemainAvailable()
        {
            var legacy=new ProjectForm(name:"original",category:ProjectForm.CategoryEnum.Kundenprojekt,workdays:4.5m,permissionType:long.MaxValue);
            Assert.That(legacy.Workdays,Is.EqualTo(4.5m));Assert.That(legacy.PermissionType,Is.EqualTo(long.MaxValue));Assert.That(legacy.EstimatedHoursMode,Is.False);Assert.That(legacy.ContactId,Is.EqualTo(0));
            Assert.That(JObject.Parse(legacy.ToJson()).Value<string>("category"),Is.EqualTo("Kundenprojekt"));
            foreach(var name in new[]{"CreateProject","UpdateProject"})
                Assert.That(typeof(ProjectApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));
        }

        [Test]
        public void CompleteConstructorAndArraySettersCopyInputs()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new ProjectWriteRequest(original);original["future"]["value"]=2;
            Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new ProjectWriteRequest(null)));
            var custom=Custom();request.CustomFields=custom;custom[0]["customFieldValue"]=2;Assert.That(request.CustomFields[0].Value<decimal>("customFieldValue"),Is.EqualTo(123456789.123456789m));
            var employees=Employees();request.ProjectEmployees=employees;employees[0]["future"]["nested"]=true;Assert.That(request.ProjectEmployees[0]["future"].Value<bool>("nested"),Is.False);
            request.CustomFields=null;request.ProjectEmployees=null;Assert.That(request.ContainsKey("customFields"),Is.False);Assert.That(request.ContainsKey("projectEmployees"),Is.False);
        }

        [Test]
        public async Task AllTypedFieldsPreserveOriginalWireAndResponse([Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var request=new ProjectWriteRequest();var expected=new JObject();Assert.That(Schema().Count,Is.EqualTo(36));Assert.That(typeof(ProjectWriteRequest).GetProperties().Count(p=>p.DeclaringType==typeof(ProjectWriteRequest)),Is.EqualTo(36));
            foreach(var field in Schema().Properties())
            {
                var property=typeof(ProjectWriteRequest).GetProperties().Single(p=>p.DeclaringType==typeof(ProjectWriteRequest)&&p.Name==PropertyName(field.Name));string kind=field.Value.Value<string>("type");
                object value=kind=="string"?(object)("  original / "+field.Name):kind=="boolean"?(object)false:kind=="number"?(object)123456789.123456789m:kind=="integer"?field.Value.Value<string>("format")=="int64"?(object)long.MinValue:int.MinValue:field.Name=="customFields"?(object)Custom():Employees();
                property.SetValue(request,value);expected[field.Name]=value is JToken token?token.DeepClone():new JValue(value);
            }
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();
            var handler=new Handler(token=>Task.FromResult(Response(creating?201:200,ProjectResult)));
            using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-project-write-token"};var api=new ProjectApi(config);
                var info=await Invoke(api,long.MinValue,request,creating,mode,CancellationToken.None);
                Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));
                Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(creating?"/rest/project/new":"/rest/project/-9223372036854775808"));Assert.That(handler.Uri.Query,Is.Empty);
                Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-project-write-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));
                Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));Assert.That(info.StatusCode,Is.EqualTo(creating?201:200));Assert.That(info.RawJsonContent,Is.EqualTo(ProjectResult));
                Assert.That(info.Headers["X-Project-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));
                Assert.That(info.Data["projectId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls,Is.EqualTo(1));Assert.That(config.AccessToken,Is.EqualTo("isolated-project-write-token"));
            }
        }

        [Test]
        public async Task OptionalBodyAndSuppliedCreationObjectAreDistinct([Values(false,true)]bool creating,[Values(0,1,2)]int mode,[Values("absent","empty","name","contact","both","empty-name","null-name","null-contact")]string selection)
        {
            var request=selection=="absent"?null:new ProjectWriteRequest();
            if(selection=="name"||selection=="both")request.Name="original";
            if(selection=="contact"||selection=="both"||selection=="empty-name")request.ContactId=0;
            if(selection=="empty-name")request.Name="";
            if(selection=="null-name"){request.Name="x";request.ContactId=0;request["name"]=JValue.CreateNull();}
            if(selection=="null-contact"){request.Name="x";request["contactId"]=JValue.CreateNull();}
            bool invalid=creating&&selection!="absent"&&selection!="both"&&selection!="empty-name";
            var handler=new Handler(token=>Task.FromResult(Response(creating?201:200,"")));
            using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),0,request,creating,mode,CancellationToken.None);}));
                else{var info=await Invoke(Api(http),0,request,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
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
                else{await Invoke(Api(http),1,request,false,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(creating?0:1));
        }

        public static IEnumerable<TestCaseData> IntegerCases()
        {
            foreach(var field in Schema().Properties().Where(p=>p.Value.Value<string>("type")=="integer"))
                foreach(var creating in new[]{false,true})foreach(var json in new[]{"7.25","\"7\"",field.Value.Value<string>("format")=="int64"?"9223372036854775808":"2147483648",field.Value.Value<string>("format")=="int64"?"-9223372036854775809":"-2147483649"})
                    yield return new TestCaseData(field.Name,creating,json);
        }
        [TestCaseSource(nameof(IntegerCases))]
        public void IntegerWidthsRejectFractionStringAndOverflow(string field,bool creating,string json)
        {
            var request=Valid(creating);request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task RawEstimatedHoursStayNumeric([Values(false,true)]bool creating,[Values("0","-12.75","123456789.123456789","92233720368547758081234567890","1e100")]string json)
        {
            var request=Valid(creating);request["estimatedHours"]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=json=="1e100"?FloatParseHandling.Double:FloatParseHandling.Decimal});
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,creating,2,CancellationToken.None);
            Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void NonFiniteHoursStopBeforeHttp([Values(false,true)]bool creating,[Values(double.NaN,double.PositiveInfinity,double.NegativeInfinity)]double value,[Values(0,1,2)]int mode)
        {
            var request=Valid(creating);request["estimatedHours"]=new JValue(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task ConfiguredCataloguesAndModesRemainOriginal([Values(false,true)]bool creating)
        {
            var request=Valid(creating);request.Type2="Configured project type / §";request.Phase="Configured phase";request.Status="Configured status";request.DimensionName="";request.OwnerUid="  original UID  ";request.EstimatedHoursMode=false;request.EstimatedHours=0;request.Billable="Ja";request.TimeRecordable="Noch zu genehmigen";request.TimeRecordType="Stunden";
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,creating,2,CancellationToken.None);
            Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task NestedFieldsAreSnapshottedBeforeActiveHttp([Values(false,true)]bool creating)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request=Valid(creating);request.CustomFields=Custom();request.ProjectEmployees=Employees();var expected=request.ToString(Formatting.None);
            var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});
            using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),1,request,creating,2,CancellationToken.None);
                try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.CustomFields[0]["customFieldValue"]=2;request.ProjectEmployees[0]["future"]["nested"]=true;Assert.That(handler.Body,Is.EqualTo(expected));}
                finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task MixedCustomValuesRemainUnconverted([Values(false,true)]bool creating,[Values("false","123456789.123456789","9223372036854775807","\"aa§§bb\"","{}","[]","null")]string json)
        {
            var request=Valid(creating);request.CustomFields=new JArray(new JObject{["customFieldName"]="customText1",["customFieldValue"]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=FloatParseHandling.Decimal}),["future"]=new JObject()});
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(creating&&json=="null")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));
                else{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(creating&&json=="null"?0:1));
        }

        [Test]
        public void InvalidNestedKindsStopBeforeHttp([Values(false,true)]bool creating,[Values("customFields","projectEmployees")]string field,[Values("[null]","[false]","[4]","[\"row\"]")]string json)
        {
            var request=Valid(creating);request[field]=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task NestedTeamFieldsPreserveSparseUpdateAndValidateCreation([Values(false,true)]bool creating,[Values("[]","[{}]","[{\"contactId\":0}]","[{\"contactId\":null}]","[{\"contactId\":\"7\"}]","[{\"contactId\":9223372036854775808}]","[{\"contactId\":7,\"hourlyRate\":\"7\"}]","[{\"contactId\":7,\"role\":4}]","[{\"role\":\"Configured\",\"dailyRate\":0}]")]string json)
        {
            var request=Valid(creating);request.ProjectEmployees=JArray.Parse(json);
            bool invalid=json.Contains("\"7\"")||json.Contains("9223372036854775808")||json.Contains("\"role\":4")||(creating&&(json=="[{}]"||json.Contains("null")||json.StartsWith("[{\"role\"",StringComparison.Ordinal)));
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));
                else{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public void InvalidCustomNamesAndMissingValuesStopBeforeHttp([Values(false,true)]bool creating,[Values("[{}]","[{\"customFieldName\":4,\"customFieldValue\":false}]","[{\"customFieldName\":\"x\"}]")]string json)
        {
            var request=Valid(creating);request.CustomFields=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task InvariantProjectIdDoesNotChooseAnotherResource([Values(long.MinValue,0L,long.MaxValue)]long id)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));var previous=CultureInfo.CurrentCulture;
            try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");using(var http=new HttpClient(handler))await Invoke(Api(http),id,new ProjectWriteRequest(),false,2,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/project/"+id.ToString(CultureInfo.InvariantCulture)));}
            finally{CultureInfo.CurrentCulture=previous;}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void NestedNonFiniteRatesStopBeforeHttp([Values(false,true)]bool creating,[Values("hourlyRate","dailyRate")]string field)
        {
            var request=Valid(creating);request.ProjectEmployees=Employees();var row=(JObject)request.ProjectEmployees[0];row.Remove(field);row[field]=new JValue(double.NaN);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        private const string ProjectResult="{\"status\":2147483647,\"projectId\":9223372036854775807,\"location\":\"original\",\"errors\":{\"future\":false},\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\"}";
        private const string Result = "{\"id\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static ProjectWriteRequest Valid(bool creating) => creating ? new ProjectWriteRequest { Name = "fixture", ContactId = 42 } : new ProjectWriteRequest();
        private static ProjectApi Api(HttpClient http) => new ProjectApi(new Configuration { BasePath = "https://openscope.invalid/rest", HttpClient = http, AccessToken = "isolated-project-write-token" });
        private static Task<ApiResponse<JToken>> Invoke(ProjectApi api, long id, ProjectWriteRequest request, bool creating, int mode, CancellationToken token)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProjectWithResultWithHttpInfo(request)) : mode == 1 ? api.CreateProjectWithResultAsyncWithHttpInfo(request) : api.CreateProjectWithResultAsyncWithHttpInfo(request, token) :
                mode == 0 ? Task.FromResult(api.UpdateProjectWithResultWithHttpInfo(id, request)) : mode == 1 ? api.UpdateProjectWithResultAsyncWithHttpInfo(id, request) : api.UpdateProjectWithResultAsyncWithHttpInfo(id, request, token);
        private static Task<JToken> InvokeData(ProjectApi api, long id, ProjectWriteRequest request, bool creating, int mode)
            => creating ? mode == 0 ? Task.FromResult(api.CreateProjectWithResult(request)) : mode == 1 ? api.CreateProjectWithResultAsync(request) : api.CreateProjectWithResultAsync(request, CancellationToken.None) :
                mode == 0 ? Task.FromResult(api.UpdateProjectWithResult(id, request)) : mode == 1 ? api.UpdateProjectWithResultAsync(id, request) : api.UpdateProjectWithResultAsync(id, request, CancellationToken.None);
        private static HttpResponseMessage Response(int status, string body)
        { var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(body, Encoding.UTF8, "application/json") }; response.Headers.Add("X-Project-Fixture", "retained"); return response; }
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
