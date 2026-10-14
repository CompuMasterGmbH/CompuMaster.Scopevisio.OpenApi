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
    public class TeamworkFileWriteTests
    {
        [TestCase("AddContactFileWithResult",0)]
        [TestCase("AddProjectFileWithResult",1)]
        [TestCase("AddTaskFileWithResult",2)]
        public void CompleteTeamworkFileFormsAreAvailable(string name,int entity)=>Assert.That((entity==0?typeof(ContactApi):entity==1?typeof(ProjectApi):typeof(TaskApi)).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentTeamworkFileRequestIsAvailable()=>Assert.That(typeof(ContactApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.TeamworkFileWriteRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static string Name(int entity)=>entity==0?"AddContactFileWithResult":entity==1?"AddProjectFileWithResult":"AddTaskFileWithResult";
        private static TeamworkFileWriteRequest Valid()=>new TeamworkFileWriteRequest{Data=" original opaque Base64 \r\n ",Filename=" original / §.bin ",Collection=" original collection name "};
        [Test]
        public void PrimaryBodiesCollectionNamesAndIndependentAccessRemainExact()
        {
            var c=Contract();var schema=c["components"]["schemas"]["TeamworkFileForm"];Assert.That(schema["required"].Values<string>(),Is.EquivalentTo(new[]{"data"}));Assert.That(((JObject)schema["properties"]).Properties().Select(prop=>prop.Name),Is.EquivalentTo(new[]{"filename","data","collection","createCollectionIfNotExist"}));Assert.That(schema["properties"]["collection"].Value<string>("description"),Does.Contain("name"));Assert.That(schema["properties"]["createCollectionIfNotExist"].Value<string>("description"),Does.Contain("without being added"));var paths=new[]{"/contact/{contactIdOrLegacyNumber}/file/new","/project/{projectId}/file/new","/task/{taskId}/file/new"};var profiles=new[]{"Kontakte","Projekte","Aufgaben"};for(var i=0;i<3;i++){var p=c["paths"][paths[i]]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Does.EndWith("/TeamworkFileForm"));Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p.Value<string>("description"),Does.Contain(profiles[i]+" (Bearbeiten)"));Assert.That(p["responses"]["400"].Value<string>("description"),Does.Contain("teamwork access"));Assert.That(p["responses"]["404"].Value<string>("description"),Does.Contain("authorization"));}
        }
        [Test]
        public void ConstructorsTypedNullsAndBytesCopyWithoutFileOrFormatInference()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new TeamworkFileWriteRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new TeamworkFileWriteRequest(null)));request.Data="";request.Filename="";request.Collection="";request.CreateCollectionIfNotExist=false;request.Data=null;request.Filename=null;request.Collection=null;request.CreateCollectionIfNotExist=null;Assert.That(request.Count,Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>TeamworkFileWriteRequest.FromBytes(null)));var bytes=Enumerable.Range(0,256).Select(x=>(byte)x).ToArray();var expected=bytes.ToArray();var fromBytes=TeamworkFileWriteRequest.FromBytes(bytes);bytes[0]=255;Assert.That(Convert.FromBase64String(fromBytes.Data),Is.EqualTo(expected));Assert.That(fromBytes.Count,Is.EqualTo(1));Assert.That(TeamworkFileWriteRequest.FromBytes(new byte[0]).Data,Is.Empty);
        }
        [Test]
        public void KnownKindsAndNullsFailBeforeHttp([Values("filename","data","collection","createCollectionIfNotExist")]string field,[Values(0,1,2)]int entity,[Values(0,1,2)]int mode,[Values(false,true)]bool nil)
        {
            var request=Valid();request[field]=nil?JValue.CreateNull():field=="createCollectionIfNotExist"?(JToken)new JValue("false"):new JValue(false);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,entity,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task OptionalBodyAndSuppliedRequiredDataRemainDistinct([Values("absent","empty","filename-only","empty-data","opaque-data","null-data","null-filename","empty-filename")]string shape,[Values(0,1,2)]int entity,[Values(0,1,2)]int mode)
        {
            var request=shape=="absent"?null:new TeamworkFileWriteRequest();if(shape=="filename-only")request.Filename="original";if(shape=="empty-data")request.Data="";if(shape=="opaque-data"||shape=="null-filename"||shape=="empty-filename")request.Data=" original opaque ";if(shape=="null-data")request["data"]=JValue.CreateNull();if(shape=="null-filename")request["filename"]=JValue.CreateNull();if(shape=="empty-filename")request.Filename="";var invalid=shape=="empty"||shape=="filename-only"||shape=="null-data"||shape=="null-filename";var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){if(invalid){Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,entity,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}else{var result=await Invoke(new Apis(http),request,entity,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(result.Data,Is.Null);Assert.That(handler.Calls,Is.EqualTo(1));}}
        }
        [Test]
        public async Task OriginalCollectionAndCreateChoiceDoNotCauseLookupOrSeparateWrites([Values(-1,0,1)]int choice,[Values(0,1,2)]int entity,[Values(0,1,2)]int mode)
        {
            var request=Valid();request.Collection=" original / collection ?#¤ ";if(choice>=0)request.CreateCollectionIfNotExist=choice==1;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,entity,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));if(choice<0)Assert.That(JObject.Parse(handler.Body).ContainsKey("createCollectionIfNotExist"),Is.False);else Assert.That(JObject.Parse(handler.Body).Value<bool>("createCollectionIfNotExist"),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task AllBytesReachWireWithoutPDFOrFilenameDefaults([Values(0,1,256)]int length,[Values(0,1,2)]int entity,[Values(0,1,2)]int mode)
        {
            var bytes=Enumerable.Range(0,length).Select(x=>(byte)x).ToArray();var request=TeamworkFileWriteRequest.FromBytes(bytes);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,entity,mode,CancellationToken.None);var sent=JObject.Parse(handler.Body);Assert.That(Convert.FromBase64String(sent.Value<string>("data")),Is.EqualTo(bytes));Assert.That(sent.Count,Is.EqualTo(1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalStringsFutureJsonAndAllMetadataRemain([Values(0,1,2)]int entity,[Values(0,1,2)]int mode)
        {
            var request=Valid();request.CreateCollectionIfNotExist=false;request["future"]=new JObject{["value"]=new JArray(false,0,JValue.CreateNull())};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler)){var apis=new Apis(http);var info=await Invoke(apis,request,entity,mode,CancellationToken.None,"0007 / contact",long.MinValue);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(entity==0?"/rest/contact/0007%20%2F%20contact/file/new":entity==1?"/rest/project/-9223372036854775808/file/new":"/rest/task/-9223372036854775808/file/new"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-teamwork-file-token"));Assert.That(handler.Accept,Does.Contain("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(info.StatusCode,Is.EqualTo(200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Teamwork-File-Fixture"],Does.Contain("retained"));Assert.That(info.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(apis.Config.HttpClient,Is.SameAs(http));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalContactSelectorIsOneEscapedSegment([Values(null,".","..","0007","","  ","../x/§ ?#%","a%2Fb")]string selector,[Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),0,mode,CancellationToken.None,selector);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),0,mode,CancellationToken.None,selector);}));else{await Invoke(new Apis(http),Valid(),0,mode,CancellationToken.None,selector);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/contact/"+Uri.EscapeDataString(selector)+"/file/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}}Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }
        [Test]
        public async Task NumericEntityIdsRemainInvariantAndDistinct([Values(long.MinValue,0L,long.MaxValue)]long id,[Values(1,2)]int entity,[Values(0,1,2)]int mode)
        {
            var previous=CultureInfo.CurrentCulture;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");await Invoke(new Apis(http),Valid(),entity,mode,CancellationToken.None,"unused",id);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/"+(entity==1?"project/":"task/")+id.ToString(CultureInfo.InvariantCulture)+"/file/new"));}finally{CultureInfo.CurrentCulture=previous;}}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ActiveSnapshotIncludesCollectionChoiceAndFutureJson([Values(0,1,2)]int entity)
        {
            var request=Valid();request.CreateCollectionIfNotExist=false;request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler)){var operation=Invoke(new Apis(http),request,entity,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.Collection="after";request.CreateCollectionIfNotExist=true;request.Data="after";request["future"]["value"]=9;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;}
        }
        [Test]
        public async Task AllDataFormsRetainErrorsWithoutInventedFileIds([Values(0,1,2)]int entity,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"errors\":[\"file\"],\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(new Apis(http),Valid(),entity,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,1,2)]int entity,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original teamwork-file diagnostic")));using(var http=new HttpClient(handler)){var apis=new Apis(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(apis,Valid(),entity,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original teamwork-file diagnostic"));var expected=new InvalidOperationException("custom teamwork-file hook");ExceptionFactory factory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(entity)));return expected;};apis.Contact.ExceptionFactory=factory;apis.Project.ExceptionFactory=factory;apis.Task.ExceptionFactory=factory;Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(apis,Valid(),entity,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));}
        }
        [Test]
        public void InvalidSuccessJsonIsNotInventedAccess([Values(0,1,2)]int entity)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),entity,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void PrecancellationHasNoEffects([Values(0,1,2)]int entity)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),entity,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }
        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(0,1,2)]int entity)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){var operation=Invoke(new Apis(http),Valid(),entity,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}}
        }
        [Test]
        public async Task LegacyModelsDefaultsAndDataDiscardingRemainAvailable()
        {
            var legacy=new TeamworkFileForm(data:"original Base64");Assert.That(legacy.CreateCollectionIfNotExist,Is.False);Assert.That(legacy.Collection,Is.Null);var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var apis=new Apis(http);Assert.That(apis.Contact.AddContactFileWithHttpInfo("legacy",legacy).Data,Is.Null);Assert.That(apis.Project.AddProjectFileWithHttpInfo(42,legacy).Data,Is.Null);Assert.That((await apis.Task.AddTaskFileAsyncWithHttpInfo(42,legacy)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(3));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-15T00:00:00Z\",\"future\":false}";
        private sealed class Apis
        {
            internal Configuration Config;internal ContactApi Contact;internal ProjectApi Project;internal TaskApi Task;
            internal Apis(HttpClient http){Config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-teamwork-file-token"};Contact=new ContactApi(Config);Project=new ProjectApi(Config);Task=new TaskApi(Config);}
        }
        private static Task<ApiResponse<JToken>> Invoke(Apis api,TeamworkFileWriteRequest request,int entity,int mode,CancellationToken token,string contact="fixture",long id=42)=>entity==0?mode==0?Task.FromResult(api.Contact.AddContactFileWithResultWithHttpInfo(contact,request)):mode==1?api.Contact.AddContactFileWithResultAsyncWithHttpInfo(contact,request):api.Contact.AddContactFileWithResultAsyncWithHttpInfo(contact,request,token):entity==1?mode==0?Task.FromResult(api.Project.AddProjectFileWithResultWithHttpInfo(id,request)):mode==1?api.Project.AddProjectFileWithResultAsyncWithHttpInfo(id,request):api.Project.AddProjectFileWithResultAsyncWithHttpInfo(id,request,token):mode==0?Task.FromResult(api.Task.AddTaskFileWithResultWithHttpInfo(id,request)):mode==1?api.Task.AddTaskFileWithResultAsyncWithHttpInfo(id,request):api.Task.AddTaskFileWithResultAsyncWithHttpInfo(id,request,token);
        private static Task<JToken> InvokeData(Apis api,TeamworkFileWriteRequest request,int entity,int mode)=>entity==0?mode==0?Task.FromResult(api.Contact.AddContactFileWithResult("fixture",request)):mode==1?api.Contact.AddContactFileWithResultAsync("fixture",request):api.Contact.AddContactFileWithResultAsync("fixture",request,CancellationToken.None):entity==1?mode==0?Task.FromResult(api.Project.AddProjectFileWithResult(42,request)):mode==1?api.Project.AddProjectFileWithResultAsync(42,request):api.Project.AddProjectFileWithResultAsync(42,request,CancellationToken.None):mode==0?Task.FromResult(api.Task.AddTaskFileWithResult(42,request)):mode==1?api.Task.AddTaskFileWithResultAsync(42,request):api.Task.AddTaskFileWithResultAsync(42,request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Teamwork-File-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
