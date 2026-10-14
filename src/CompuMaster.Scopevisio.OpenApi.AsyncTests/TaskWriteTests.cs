using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class TaskWriteTests
    {
        [TestCase("CreateTaskWithResult")]
        [TestCase("UpdateTaskWithResult")]
        [TestCase("CreateSubtaskWithResult")]
        public void CompleteCurrentTaskWriteFormsAreAvailable(string name)=>Assert.That(typeof(TaskApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));

        [TestCase("TaskWriteRequest")]
        [TestCase("SubtaskWriteRequest")]
        public void CurrentTaskRequestIsAvailable(string model)=>Assert.That(typeof(TaskApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+model),Is.Not.Null);

        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));
            using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));
            return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema(int family)=>(JObject)Contract()["components"]["schemas"][family==2?"SubtaskForm":"TaskForm"]["properties"];
        private static Type Model(int family)=>family==2?typeof(SubtaskWriteRequest):typeof(TaskWriteRequest);
        private static JObject Valid(int family)=>family==2?(JObject)new SubtaskWriteRequest{TaskId=42}:family==0?new TaskWriteRequest{Topic="fixture"}:new TaskWriteRequest();
        private static string Name(int family)=>family==0?"CreateTaskWithResult":family==1?"UpdateTaskWithResult":"CreateSubtaskWithResult";
        private static string FieldProperty(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        public static IEnumerable<TestCaseData> Fields()
        {
            foreach(var family in new[]{0,1,2})foreach(var field in Schema(family).Properties())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(family,field.Name,mode);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownFieldKindsStopBeforeHttp(int family,string field,int mode)
        {
            var request=Valid(family);string kind=Schema(family)[field].Value<string>("type");request[field]=kind=="string"?new JValue(false):kind=="boolean"?new JValue("false"):kind=="integer"?new JValue(7.25m):(JToken)new JObject();
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public async Task ExplicitNullPreservesOnlyTaskUpdate(int family,string field,int mode)
        {
            var request=Valid(family);request[field]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))
            {
                if(family!=1)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,mode,CancellationToken.None);}));
                else{await Invoke(Api(http),1,request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(family==1?1:0));
        }

        [Test]
        public void EveryTypedNullSetterOmitsWithoutDefaults([Values(0,2)]int family)
        {
            var request=family==2?(JObject)new SubtaskWriteRequest():new TaskWriteRequest();request["future"]=false;
            foreach(var field in Schema(family).Properties())
            {
                request[field.Name]=JValue.CreateNull();Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False,field.Name);
            }
            Assert.That(request.Count,Is.EqualTo(1));Assert.That(request.Value<bool>("future"),Is.False);
        }

        [Test]
        public void ConstructorAndCustomSetterCopyInputs()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var task=new TaskWriteRequest(original);var subtask=new SubtaskWriteRequest(original);original["future"]["value"]=2;
            Assert.That(task["future"].Value<int>("value"),Is.EqualTo(1));Assert.That(subtask["future"].Value<int>("value"),Is.EqualTo(1));
            Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new TaskWriteRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new SubtaskWriteRequest(null)));
            var custom=Custom();task.CustomFields=custom;custom[0]["customFieldValue"]=2;Assert.That(task.CustomFields[0].Value<decimal>("customFieldValue"),Is.EqualTo(123456789.123456789m));task.CustomFields=null;Assert.That(task.ContainsKey("customFields"),Is.False);
        }

        [Test]
        public void PrimaryBodiesFieldsProfilesAndSuccessSchemasRemainDistinct()
        {
            var c=Contract();foreach(var family in new[]{0,1,2})
            {
                string path=family==0?"/task/new":family==1?"/task/{id}":"/subtask/new";var p=c["paths"][path]["post"];
                if(family==1)Assert.That(p["requestBody"]["required"],Is.Null);else Assert.That(p["requestBody"].Value<bool>("required"),Is.True);
                Assert.That(p.Value<string>("description"),Does.Contain(family==1?"Aufgabe (Bearbeiten)":"Aufgabe, Kontakte (Bearbeiten)"));
                Assert.That(p["responses"][family==1?"200":"201"]["content"],Is.Null);
            }
            Assert.That(c["components"]["schemas"]["TaskForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"topic"}));Assert.That(c["components"]["schemas"]["SubtaskForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"taskId"}));
            Assert.That(Schema(0)["priority"].Value<string>("default"),Is.EqualTo("normal"));Assert.That(Schema(0)["contactSphere"]["enum"].Values<string>(),Does.Contain("Keditor"));
            Assert.That(Schema(0)["contactLegacyId"].Value<string>("description"),Does.Contain("not evaluated"));
        }

        [Test]
        public void LegacyEnumsDefaultsAndMethodsRemainAvailable()
        {
            var task=new TaskForm(topic:"legacy",priority:TaskForm.PriorityEnum.Normal,permissionType:long.MaxValue);var subtask=new SubtaskForm();
            Assert.That(JObject.Parse(task.ToJson()).Value<string>("priority"),Is.EqualTo("Normal"));Assert.That(task.PermissionType,Is.EqualTo(long.MaxValue));Assert.That(task.EmployeeDomain,Is.False);Assert.That(task.ContactId,Is.EqualTo(0));Assert.That(subtask.TaskId,Is.EqualTo(0));
            foreach(var name in new[]{"CreateTask","UpdateTask","CreateSubtask"})Assert.That(typeof(TaskApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));
        }

        [Test]
        public async Task AllTypedFieldsRetainOriginalRequestAndMetadata([Values(0,1,2)]int family,[Values(0,1,2)]int mode)
        {
            var request=family==2?(JObject)new SubtaskWriteRequest():new TaskWriteRequest();var expected=new JObject();int count=family==2?6:23;
            Assert.That(Schema(family).Count,Is.EqualTo(count));Assert.That(Model(family).GetProperties().Count(p=>p.DeclaringType==Model(family)),Is.EqualTo(count));
            foreach(var field in Schema(family).Properties())
            {
                string kind=field.Value.Value<string>("type");object value=kind=="string"?(object)("  original / "+field.Name):kind=="boolean"?(object)false:kind=="number"?(object)123456789.123456789m:kind=="integer"?field.Value.Value<string>("format")=="int64"?(object)long.MinValue:int.MinValue:Custom();
                Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,value);expected[field.Name]=value is JToken token?token.DeepClone():new JValue(value);
            }
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(family==1?200:201,Result)));
            using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-task-write-token"};var api=new TaskApi(config);var info=await Invoke(api,long.MinValue,request,family,mode,CancellationToken.None);
                Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(family==0?"/rest/task/new":family==1?"/rest/task/-9223372036854775808":"/rest/subtask/new"));Assert.That(handler.Uri.Query,Is.Empty);
                Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-task-write-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(info.StatusCode,Is.EqualTo(family==1?200:201));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Task-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));
                Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(config.AccessToken,Is.EqualTo("isolated-task-write-token"));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CreationRequiresOwnFieldButUpdateBodyIsOptional([Values(0,1,2)]int family,[Values(0,1,2)]int mode,[Values("absent","empty","required","empty-topic","null-required")]string selection)
        {
            JObject request=selection=="absent"?null:family==2?(JObject)new SubtaskWriteRequest():new TaskWriteRequest();string required=family==2?"taskId":"topic";
            if(selection=="required"||selection=="empty-topic")request[required]=family==2?(JToken)new JValue(0):new JValue(selection=="empty-topic"?"":"  original  ");
            if(selection=="null-required")request[required]=JValue.CreateNull();bool invalid=family!=1&&(selection=="absent"||selection=="empty"||selection=="null-required");
            var handler=new Handler(token=>Task.FromResult(Response(family==1?200:201,"")));using(var http=new HttpClient(handler))
            {
                if(invalid&&request==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),0,request,family,mode,CancellationToken.None);}));
                else if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),0,request,family,mode,CancellationToken.None);}));
                else{var info=await Invoke(Api(http),0,request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        public static IEnumerable<TestCaseData> Integers()
        {
            foreach(var family in new[]{0,1,2})foreach(var field in Schema(family).Properties().Where(p=>p.Value.Value<string>("type")=="integer"))
                foreach(var json in new[]{"7.25","\"7\"",field.Value.Value<string>("format")=="int64"?"9223372036854775808":"2147483648",field.Value.Value<string>("format")=="int64"?"-9223372036854775809":"-2147483649"})yield return new TestCaseData(family,field.Name,json);
        }
        [TestCaseSource(nameof(Integers))]
        public void IntegerWidthsRejectCoercion(int family,string field,string json)
        {
            var request=Valid(family);request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task HoursRetainRawNumbers([Values(0,1,2)]int family,[Values("0","-12.75","123456789.123456789","92233720368547758081234567890","1e100")]string json)
        {
            var request=Valid(family);request["estimatedHours"]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=json=="1e100"?FloatParseHandling.Double:FloatParseHandling.Decimal});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,family,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void NonFiniteHoursStopBeforeHttp([Values(0,1,2)]int family,[Values(0,1,2,3,4,5)]int selection,[Values(0,1,2)]int mode)
        {
            object value=selection==0?(object)double.NaN:selection==1?(object)double.PositiveInfinity:selection==2?(object)double.NegativeInfinity:selection==3?(object)float.NaN:selection==4?(object)float.PositiveInfinity:float.NegativeInfinity;
            var request=Valid(family);request["estimatedHours"]=new JValue(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task ContactSelectorsStringsAndModesRemainCallerChoices([Values(0,1)]int family)
        {
            var request=(TaskWriteRequest)Valid(family);request.ContactId=0;request.ContactLegacyId=" 0007 ";request.Priority="normal";request.ContactSphere="Keditor";request.Status="Anstehend";request.EmployeeDomain=false;request.SetEstimatedHoursManually=false;request.EstimatedHours=0;request.ReminderDateTS=0;request.Tags=" original, tags / § ";
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))await Invoke(Api(http),1,request,family,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(0,1,2)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var request=Valid(family);request["topic"]="before";request["future"]=new JObject{["value"]=1};if(family!=2)((TaskWriteRequest)request).CustomFields=Custom();var expected=request.ToString(Formatting.None);
            var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),1,request,family,2,CancellationToken.None);
                try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request["topic"]="after";request["future"]["value"]=2;if(family!=2)((TaskWriteRequest)request).CustomFields[0]["customFieldValue"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}
                finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task MixedCustomValuesStayOriginal([Values(0,1)]int family,[Values("false","123456789.123456789","9223372036854775807","\"aa§§bb\"","{}","[]","null")]string json)
        {
            var request=(TaskWriteRequest)Valid(family);request.CustomFields=new JArray(new JObject{["customFieldName"]="customText1",["customFieldValue"]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=FloatParseHandling.Decimal}),["future"]=false});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))
            {
                if(family==0&&json=="null")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,2,CancellationToken.None);}));
                else{await Invoke(Api(http),1,request,family,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(family==0&&json=="null"?0:1));
        }

        [Test]
        public void InvalidCustomEntriesStopBeforeHttp([Values(0,1)]int family,[Values("[null]","[false]","[{}]","[{\"customFieldName\":4,\"customFieldValue\":false}]","[{\"customFieldName\":\"x\"}]")]string json)
        {
            var request=(TaskWriteRequest)Valid(family);request.CustomFields=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventSuccess([Values(0,1,2)]int family,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(family==1?200:201,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),-1,Valid(family),family,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,1,2)]int family,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original task diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,1,Valid(family),family,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original task diagnostic"));
                var expected=new InvalidOperationException("custom task hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(family)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,1,Valid(family),family,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(0,1,2)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(family==1?200:201,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),1,Valid(family),family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoHttpEffects([Values(0,1,2)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),1,Valid(family),family,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeTransportWithoutReplay([Values(0,1,2)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});
            using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),1,Valid(family),family,2,cancel.Token);
                try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}
                finally{cancel.Cancel();}
            }
        }

        [Test]
        public async Task OriginalSelectedTaskIdUsesInvariantText([Values(long.MinValue,0L,long.MaxValue)]long id)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));var previous=CultureInfo.CurrentCulture;
            try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");using(var http=new HttpClient(handler))await Invoke(Api(http),id,new TaskWriteRequest(),1,2,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/task/"+id.ToString(CultureInfo.InvariantCulture)));}
            finally{CultureInfo.CurrentCulture=previous;}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static JArray Custom()=>new JArray(new JObject{["customFieldName"]="customAmount1",["customFieldValue"]=123456789.123456789m,["future"]=false});
        private static TaskApi Api(HttpClient http)=>new TaskApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-task-write-token"});
        private static Task<ApiResponse<JToken>> Invoke(TaskApi api,long id,JObject request,int family,int mode,CancellationToken token)
            =>family==0?mode==0?Task.FromResult(api.CreateTaskWithResultWithHttpInfo((TaskWriteRequest)request)):mode==1?api.CreateTaskWithResultAsyncWithHttpInfo((TaskWriteRequest)request):api.CreateTaskWithResultAsyncWithHttpInfo((TaskWriteRequest)request,token):
                family==1?mode==0?Task.FromResult(api.UpdateTaskWithResultWithHttpInfo(id,(TaskWriteRequest)request)):mode==1?api.UpdateTaskWithResultAsyncWithHttpInfo(id,(TaskWriteRequest)request):api.UpdateTaskWithResultAsyncWithHttpInfo(id,(TaskWriteRequest)request,token):
                mode==0?Task.FromResult(api.CreateSubtaskWithResultWithHttpInfo((SubtaskWriteRequest)request)):mode==1?api.CreateSubtaskWithResultAsyncWithHttpInfo((SubtaskWriteRequest)request):api.CreateSubtaskWithResultAsyncWithHttpInfo((SubtaskWriteRequest)request,token);
        private static Task<JToken> InvokeData(TaskApi api,long id,JObject request,int family,int mode)
            =>family==0?mode==0?Task.FromResult(api.CreateTaskWithResult((TaskWriteRequest)request)):mode==1?api.CreateTaskWithResultAsync((TaskWriteRequest)request):api.CreateTaskWithResultAsync((TaskWriteRequest)request,CancellationToken.None):
                family==1?mode==0?Task.FromResult(api.UpdateTaskWithResult(id,(TaskWriteRequest)request)):mode==1?api.UpdateTaskWithResultAsync(id,(TaskWriteRequest)request):api.UpdateTaskWithResultAsync(id,(TaskWriteRequest)request,CancellationToken.None):
                mode==0?Task.FromResult(api.CreateSubtaskWithResult((SubtaskWriteRequest)request)):mode==1?api.CreateSubtaskWithResultAsync((SubtaskWriteRequest)request):api.CreateSubtaskWithResultAsync((SubtaskWriteRequest)request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Task-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;
            internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
            {Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
