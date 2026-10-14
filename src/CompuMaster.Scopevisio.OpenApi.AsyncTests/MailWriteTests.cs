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
    public class MailWriteTests
    {
        [TestCase(typeof(ContactApi),"AssignMailToContactWithResult")]
        [TestCase(typeof(ProjectApi),"AssignMailToProjectWithResult")]
        [TestCase(typeof(TaskApi),"AssignMailToTaskWithResult")]
        [TestCase(typeof(ProjectApi),"ImportIncomingMailWithResult")]
        public void CompleteCurrentMailWriteFormsAreAvailable(Type api,string name)=>Assert.That(api.GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("MailAssignmentWriteRequest")]
        [TestCase("IncomingMailWriteRequest")]
        public void CurrentMailRequestIsAvailable(string model)=>Assert.That(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+model),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema(int family)=>(JObject)Contract()["components"]["schemas"][family==3?"IncomingMailForm":"AssignMailForm"]["properties"];
        private static Type Model(int family)=>family==3?typeof(IncomingMailWriteRequest):typeof(MailAssignmentWriteRequest);
        private static JObject Valid(int family)=>family==3?(JObject)new IncomingMailWriteRequest{Eml=" original EML\r\n "}:new MailAssignmentWriteRequest{MailId=42};
        private static string Name(int family)=>new[]{"AssignMailToContactWithResult","AssignMailToProjectWithResult","AssignMailToTaskWithResult","ImportIncomingMailWithResult"}[family];
        private static string FieldProperty(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        public static IEnumerable<TestCaseData> Fields(){foreach(var family in new[]{0,1,2,3})foreach(var field in Schema(family).Properties())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(family,field.Name,mode);}

        [TestCaseSource(nameof(Fields))]
        public void KnownKindsStopBeforeHttp(int family,string field,int mode)
        {
            var request=Valid(family);string kind=Schema(family)[field].Value<string>("type");request[field]=kind=="string"?new JValue(false):kind=="boolean"?new JValue("false"):new JValue(7.25m);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownNullsStopBeforeHttp(int family,string field,int mode)
        {
            var request=Valid(family);request[field]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public void ConstructorsAndTypedNullsCopyAndOmit([Values(0,3)]int family)
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};JObject request=family==3?(JObject)new IncomingMailWriteRequest(original):new MailAssignmentWriteRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));
            foreach(var field in Schema(family).Properties()){request[field.Name]=JValue.CreateNull();Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.EqualTo(1));
            Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new MailAssignmentWriteRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new IncomingMailWriteRequest(null)));
        }

        [Test]
        public void PrimaryBodyProfilesAndAutomaticImportAssociationStayExplicit()
        {
            var c=Contract();var paths=new[]{"/contact/{contactIdOrLegacyNumber}/assignMail","/project/{projectId}/assignMail","/task/{taskId}/assignMail","/incomingmail/new"};var profiles=new[]{"Kontakte, Mailbot, Mailagent (Bearbeiten)","Projekte, Mailbot, Mailagent (Bearbeiten)","Aufgaben,Mailbot,Mailagent (Bearbeiten)","Mailbot, Mailagent (Bearbeiten)"};
            foreach(var family in new[]{0,1,2,3}){var p=c["paths"][paths[family]]["post"];if(family==3)Assert.That(p["requestBody"]["required"],Is.Null);else Assert.That(p["requestBody"].Value<bool>("required"),Is.True);Assert.That(p.Value<string>("description"),Does.Contain(profiles[family]));Assert.That(p["responses"]["200"]["content"],Is.Null);}
            Assert.That(c["components"]["schemas"]["AssignMailForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"mailId"}));Assert.That(c["components"]["schemas"]["IncomingMailForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"eml"}));Assert.That(Schema(3)["skipStandardContactAssignment"].Value<bool>("default"),Is.False);Assert.That(c["paths"][paths[3]]["post"].Value<string>("description"),Does.Contain("tries to assign"));
        }

        [Test]
        public void LegacyModelsAndOwnersRemainAvailable()
        {
            var assign=new AssignMailForm();var incoming=new IncomingMailForm(encoding:IncomingMailForm.EncodingEnum.Base64,eml:"original");Assert.That(assign.MailId,Is.EqualTo(0));Assert.That(JObject.Parse(incoming.ToJson()).Value<string>("encoding"),Is.EqualTo("base64"));Assert.That(typeof(IncomingMailForm).GetProperty("SkipStandardContactAssignment"),Is.Null);
            foreach(var pair in new[]{Tuple.Create(typeof(ContactApi),"AssignMailContact"),Tuple.Create(typeof(ProjectApi),"AssignMailProject"),Tuple.Create(typeof(TaskApi),"AssignMailTask"),Tuple.Create(typeof(ProjectApi),"CreateIncomingMail")})Assert.That(pair.Item1.GetMethods().Count(m=>m.Name==pair.Item2||m.Name==pair.Item2+"WithHttpInfo"||m.Name==pair.Item2+"Async"||m.Name==pair.Item2+"AsyncWithHttpInfo"),Is.EqualTo(4));
        }

        [Test]
        public async Task AllTypedFieldsPreserveOriginalWireAndMetadata([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode)
        {
            JObject request=family==3?(JObject)new IncomingMailWriteRequest():new MailAssignmentWriteRequest();var expected=new JObject();int count=family==3?3:1;Assert.That(Schema(family).Count,Is.EqualTo(count));Assert.That(Model(family).GetProperties().Count(p=>p.DeclaringType==Model(family)),Is.EqualTo(count));
            foreach(var field in Schema(family).Properties()){string kind=field.Value.Value<string>("type");object value=kind=="string"?(object)("  original / "+field.Name+"\r\n"):kind=="boolean"?(object)false:long.MinValue;Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,value);expected[field.Name]=new JValue(value);}
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(200,Result)));
            using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var info=await Invoke(apis,"0007 / contact",long.MinValue,request,family,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(family==0?"/rest/contact/0007%20%2F%20contact/assignMail":family==1?"/rest/project/-9223372036854775808/assignMail":family==2?"/rest/task/-9223372036854775808/assignMail":"/rest/incomingmail/new"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-mail-write-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(apis.Config.HttpClient,Is.SameAs(http));Assert.That(apis.Config.AccessToken,Is.EqualTo("isolated-mail-write-token"));Assert.That(apis.Contact.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Project.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Task.Configuration,Is.SameAs(apis.Config));Assert.That(info.StatusCode,Is.EqualTo(200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Mail-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task AssignmentRequiresOwnIdWhileImportBodyIsOptional([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode,[Values("absent","empty","required","empty-text","null-required")]string selection)
        {
            JObject request=selection=="absent"?null:family==3?(JObject)new IncomingMailWriteRequest():new MailAssignmentWriteRequest();string required=family==3?"eml":"mailId";
            if(selection=="required"||selection=="empty-text")request[required]=family==3?(JToken)new JValue(selection=="empty-text"?"":" original EML "):new JValue(0);if(selection=="null-required")request[required]=JValue.CreateNull();bool invalid=selection=="empty"||selection=="null-required"||(family!=3&&selection=="absent");var handler=new Handler(token=>Task.FromResult(Response(200,"")));
            using(var http=new HttpClient(handler))
            {
                if(invalid&&request==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,request,family,mode,CancellationToken.None);}));else if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,request,family,mode,CancellationToken.None);}));else{var info=await Invoke(new Apis(http),"fixture",1,request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public void MailIdWidthRejectsCoercion([Values(0,1,2)]int family,[Values("7.25","\"7\"","9223372036854775808","-9223372036854775809")]string json)
        {
            var request=Valid(family);request["mailId"]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task ImportEncodingAndAssociationFlagsAreExplicitCallerChoices([Values(null,"UTF-8","base64")]string encoding,[Values(0,1,2)]int flag,[Values(""," original EML\r\n "," original Base64 ==\r\n ")]string eml)
        {
            var request=new IncomingMailWriteRequest{Encoding=encoding,Eml=eml};if(flag!=0)request.SkipStandardContactAssignment=flag==2;var expected=new JObject{["eml"]=eml};if(encoding!=null)expected["encoding"]=encoding;if(flag!=0)expected["skipStandardContactAssignment"]=flag==2;
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))await Invoke(new Apis(http),"fixture",1,request,3,2,CancellationToken.None);Assert.That(JToken.DeepEquals(JObject.Parse(handler.Body),expected),Is.True);Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task OriginalContactSelectorIsOneSegment([Values(0,1,2)]int mode,[Values(null,".","..","0007","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(new Apis(http),selector,1,Valid(0),0,mode,CancellationToken.None);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),selector,1,Valid(0),0,mode,CancellationToken.None);}));else{await Invoke(new Apis(http),selector,1,Valid(0),0,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/contact/"+Uri.EscapeDataString(selector)+"/assignMail"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task ProjectTaskAndMailIdsStaySeparateAndInvariant([Values(1,2)]int family,[Values(long.MinValue,0L,long.MaxValue)]long target,[Values(0,1,2)]int mode)
        {
            var request=new MailAssignmentWriteRequest{MailId=42};var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));var previous=CultureInfo.CurrentCulture;
            try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");using(var http=new HttpClient(handler))await Invoke(new Apis(http),"fixture",target,request,family,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo((family==1?"/rest/project/":"/rest/task/")+target.ToString(CultureInfo.InvariantCulture)+"/assignMail"));Assert.That(handler.Body,Is.EqualTo("{\"mailId\":42}"));}finally{CultureInfo.CurrentCulture=previous;}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid(family);request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(new Apis(http),"fixture",1,request,family,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request[family==3?"eml":"mailId"]=family==3?(JToken)new JValue("after"):new JValue(99);request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventImportedIds([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(new Apis(http),"fixture",1,Valid(family),family,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,1,2,3)]int family,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original mail diagnostic")));using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(apis,"fixture",1,Valid(family),family,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original mail diagnostic"));var expected=new InvalidOperationException("custom mail hook");apis.SetFactory((name,response)=>{Assert.That(name,Is.EqualTo(Name(family)));return expected;});Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(apis,"fixture",1,Valid(family),family,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,Valid(family),family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoEffects([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"fixture",1,Valid(family),family,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeTransportWithoutReplay([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(new Apis(http),"fixture",1,Valid(family),family,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private sealed class Apis
        {
            internal Configuration Config;internal ContactApi Contact;internal ProjectApi Project;internal TaskApi Task;
            internal Apis(HttpClient http){Config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-mail-write-token"};Contact=new ContactApi(Config);Project=new ProjectApi(Config);Task=new TaskApi(Config);}
            internal void SetFactory(ExceptionFactory factory){Contact.ExceptionFactory=factory;Project.ExceptionFactory=factory;Task.ExceptionFactory=factory;}
        }
        private static Task<ApiResponse<JToken>> Invoke(Apis api,string selector,long target,JObject request,int family,int mode,CancellationToken token)
            =>family==0?mode==0?Task.FromResult(api.Contact.AssignMailToContactWithResultWithHttpInfo(selector,(MailAssignmentWriteRequest)request)):mode==1?api.Contact.AssignMailToContactWithResultAsyncWithHttpInfo(selector,(MailAssignmentWriteRequest)request):api.Contact.AssignMailToContactWithResultAsyncWithHttpInfo(selector,(MailAssignmentWriteRequest)request,token):
              family==1?mode==0?Task.FromResult(api.Project.AssignMailToProjectWithResultWithHttpInfo(target,(MailAssignmentWriteRequest)request)):mode==1?api.Project.AssignMailToProjectWithResultAsyncWithHttpInfo(target,(MailAssignmentWriteRequest)request):api.Project.AssignMailToProjectWithResultAsyncWithHttpInfo(target,(MailAssignmentWriteRequest)request,token):
              family==2?mode==0?Task.FromResult(api.Task.AssignMailToTaskWithResultWithHttpInfo(target,(MailAssignmentWriteRequest)request)):mode==1?api.Task.AssignMailToTaskWithResultAsyncWithHttpInfo(target,(MailAssignmentWriteRequest)request):api.Task.AssignMailToTaskWithResultAsyncWithHttpInfo(target,(MailAssignmentWriteRequest)request,token):
              mode==0?Task.FromResult(api.Project.ImportIncomingMailWithResultWithHttpInfo((IncomingMailWriteRequest)request)):mode==1?api.Project.ImportIncomingMailWithResultAsyncWithHttpInfo((IncomingMailWriteRequest)request):api.Project.ImportIncomingMailWithResultAsyncWithHttpInfo((IncomingMailWriteRequest)request,token);
        private static Task<JToken> InvokeData(Apis api,string selector,long target,JObject request,int family,int mode)
            =>family==0?mode==0?Task.FromResult(api.Contact.AssignMailToContactWithResult(selector,(MailAssignmentWriteRequest)request)):mode==1?api.Contact.AssignMailToContactWithResultAsync(selector,(MailAssignmentWriteRequest)request):api.Contact.AssignMailToContactWithResultAsync(selector,(MailAssignmentWriteRequest)request,CancellationToken.None):
              family==1?mode==0?Task.FromResult(api.Project.AssignMailToProjectWithResult(target,(MailAssignmentWriteRequest)request)):mode==1?api.Project.AssignMailToProjectWithResultAsync(target,(MailAssignmentWriteRequest)request):api.Project.AssignMailToProjectWithResultAsync(target,(MailAssignmentWriteRequest)request,CancellationToken.None):
              family==2?mode==0?Task.FromResult(api.Task.AssignMailToTaskWithResult(target,(MailAssignmentWriteRequest)request)):mode==1?api.Task.AssignMailToTaskWithResultAsync(target,(MailAssignmentWriteRequest)request):api.Task.AssignMailToTaskWithResultAsync(target,(MailAssignmentWriteRequest)request,CancellationToken.None):
              mode==0?Task.FromResult(api.Project.ImportIncomingMailWithResult((IncomingMailWriteRequest)request)):mode==1?api.Project.ImportIncomingMailWithResultAsync((IncomingMailWriteRequest)request):api.Project.ImportIncomingMailWithResultAsync((IncomingMailWriteRequest)request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Mail-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
