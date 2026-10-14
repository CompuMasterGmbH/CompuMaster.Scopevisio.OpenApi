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
    public class AccountingFileWriteTests
    {
        [Test]
        public void JournalWriteFormsAreAvailable()=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith("AddJournalFileWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void InvoiceWriteFormsAreAvailable()=>Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Count(m=>m.Name.StartsWith("AddFileToOutgoingInvoiceWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentFileRequestIsAvailable()=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.FileWriteRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static string Name(bool invoice)=>invoice?"AddFileToOutgoingInvoiceWithResult":"AddJournalFileWithResult";
        private static FileWriteRequest Valid()=>new FileWriteRequest{Data="  YQ==\r\n ",Filename=" original / §.bin "};

        [Test]
        public void PrimaryOptionalBodyAndRequiredDataAreDifferentFromTeamworkFiles()
        {
            var c=Contract();Assert.That(c["components"]["schemas"]["FileForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"data"}));Assert.That(((JObject)c["components"]["schemas"]["FileForm"]["properties"]).Properties().Select(p=>p.Name),Is.EquivalentTo(new[]{"filename","data"}));
            foreach(var invoice in new[]{false,true})
            {
                var p=c["paths"][invoice?"/outgoinginvoice/{number}/file/new":"/journal/{documentNumber}/file/new"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Is.EqualTo("#/components/schemas/FileForm"));Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p.Value<string>("description"),Does.Contain(invoice?"Rechnungen (Bearbeiten)":"Journal (Bearbeiten)"));
            }
            Assert.That(c["paths"]["/contact/{contactIdOrLegacyNumber}/file/new"]["post"]["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Is.EqualTo("#/components/schemas/TeamworkFileForm"));
        }

        [Test]
        public void ConstructorsAndTypedOmissionCopyCompleteJson()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new FileWriteRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new FileWriteRequest(null)));
            request.Data="";request.Filename="";request.Data=null;request.Filename=null;Assert.That(request.ContainsKey("data"),Is.False);Assert.That(request.ContainsKey("filename"),Is.False);Assert.That(request.Count,Is.EqualTo(1));
        }

        [Test]
        public async Task ByteFactoryPreservesEveryByteAndCopiesCallerArray([Values(false,true)]bool invoice,[Values(0,1,2)]int mode,[Values(0,1,256)]int length)
        {
            var bytes=Enumerable.Range(0,length).Select(i=>(byte)i).ToArray();var expected=bytes.ToArray();var request=FileWriteRequest.FromBytes(bytes);if(length!=0)bytes[0]=255;
            Assert.That(Convert.FromBase64String(request.Data),Is.EqualTo(expected));Assert.That(request.ContainsKey("filename"),Is.False);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))await Invoke(new Apis(http),"original",request,invoice,mode,CancellationToken.None);Assert.That(Convert.FromBase64String(JObject.Parse(handler.Body).Value<string>("data")),Is.EqualTo(expected));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void ByteFactoryRequiresArrayWithoutChoosingFilename()
        {
            Assert.Throws<ArgumentNullException>((TestDelegate)(()=>FileWriteRequest.FromBytes(null)));var request=FileWriteRequest.FromBytes(new byte[]{0,255}," original / § ");Assert.That(request.Filename,Is.EqualTo(" original / § "));Assert.That(request.Data,Is.EqualTo("AP8="));
        }

        [Test]
        public async Task OriginalBase64FutureJsonAndAllMetadataRemain([Values(false,true)]bool invoice,[Values(0,1,2)]int mode)
        {
            var request=Valid();request["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));
            using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var info=await Invoke(apis,"0007 / doc",request,invoice,mode,CancellationToken.None);Assert.That(apis.Accounting.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Invoice.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Config.HttpClient,Is.SameAs(http));Assert.That(apis.Config.AccessToken,Is.EqualTo("isolated-accounting-file-token"));
                Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(invoice?"/rest/outgoinginvoice/0007%20%2F%20doc/file/new":"/rest/journal/0007%20%2F%20doc/file/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-accounting-file-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected));
                Assert.That(info.StatusCode,Is.EqualTo(200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-File-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodyRequiresDataOnlyWhenSupplied([Values(false,true)]bool invoice,[Values(0,1,2)]int mode,[Values("absent","empty","filename-only","empty-data","opaque-data","null-data","null-filename","empty-filename")]string selection)
        {
            var request=selection=="absent"?null:new FileWriteRequest();if(selection=="filename-only")request.Filename="original";if(selection=="empty-data")request.Data="";if(selection=="opaque-data"||selection=="null-filename"||selection=="empty-filename")request.Data=" original ... opaque \r\n ";if(selection=="null-data")request["data"]=JValue.CreateNull();if(selection=="null-filename")request["filename"]=JValue.CreateNull();if(selection=="empty-filename")request.Filename="";
            bool invalid=selection=="empty"||selection=="filename-only"||selection=="null-data"||selection=="null-filename";var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"original",request,invoice,mode,CancellationToken.None);}));
                else{var info=await Invoke(new Apis(http),"original",request,invoice,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public void KnownFieldsRequireStringKinds([Values(false,true)]bool invoice,[Values("data","filename")]string field,[Values("false","7","{}","[]")]string json,[Values(0,1,2)]int mode)
        {
            var request=Valid();request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"original",request,invoice,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task OriginalSelectorIsOneEscapedDocumentSegment([Values(false,true)]bool invoice,[Values(0,1,2)]int mode,[Values(null,".","..","0007","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(new Apis(http),selector,Valid(),invoice,mode,CancellationToken.None);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),selector,Valid(),invoice,mode,CancellationToken.None);}));
                else{await Invoke(new Apis(http),selector,Valid(),invoice,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo((invoice?"/rest/outgoinginvoice/":"/rest/journal/")+Uri.EscapeDataString(selector)+"/file/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false,true)]bool invoice)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid();request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(new Apis(http),"original",request,invoice,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.Data="after";request.Filename="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventAttachmentId([Values(false,true)]bool invoice,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(new Apis(http),"original",Valid(),invoice,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryRunsBeforeParsingErrors([Values(false,true)]bool invoice,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original attachment diagnostic")));using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(apis,"original",Valid(),invoice,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original attachment diagnostic"));var expected=new InvalidOperationException("custom attachment hook");ExceptionFactory factory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(invoice)));return expected;};apis.Accounting.ExceptionFactory=factory;apis.Invoice.ExceptionFactory=factory;
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(apis,"original",Valid(),invoice,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void MalformedSuccessIsNotAnInventedAttachment([Values(false,true)]bool invoice)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"original",Valid(),invoice,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancelledOperationHasNoEffects([Values(false,true)]bool invoice)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(new Apis(http),"original",Valid(),invoice,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(false,true)]bool invoice)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(new Apis(http),"original",Valid(),invoice,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        [Test]
        public void LegacyFileModelAndApisRemainAvailable()
        {
            var file=new FileForm(data:"original");Assert.That(file.Filename,Is.Null);Assert.That(file.Data,Is.EqualTo("original"));Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name=="AddFile"||m.Name=="AddFileWithHttpInfo"||m.Name=="AddFileAsync"||m.Name=="AddFileAsyncWithHttpInfo"),Is.EqualTo(4));Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Count(m=>m.Name=="AddFileToOutgoingInvoice"||m.Name=="AddFileToOutgoingInvoiceWithHttpInfo"||m.Name=="AddFileToOutgoingInvoiceAsync"||m.Name=="AddFileToOutgoingInvoiceAsyncWithHttpInfo"),Is.EqualTo(4));
        }

        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private sealed class Apis
        {
            internal Configuration Config;internal AccountingApi Accounting;internal OutgoingInvoiceApi Invoice;
            internal Apis(HttpClient http){Config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-accounting-file-token"};Accounting=new AccountingApi(Config);Invoice=new OutgoingInvoiceApi(Config);}
        }
        private static Task<ApiResponse<JToken>> Invoke(Apis api,string selector,FileWriteRequest request,bool invoice,int mode,CancellationToken token)
            =>invoice?mode==0?Task.FromResult(api.Invoice.AddFileToOutgoingInvoiceWithResultWithHttpInfo(selector,request)):mode==1?api.Invoice.AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(selector,request):api.Invoice.AddFileToOutgoingInvoiceWithResultAsyncWithHttpInfo(selector,request,token):
                mode==0?Task.FromResult(api.Accounting.AddJournalFileWithResultWithHttpInfo(selector,request)):mode==1?api.Accounting.AddJournalFileWithResultAsyncWithHttpInfo(selector,request):api.Accounting.AddJournalFileWithResultAsyncWithHttpInfo(selector,request,token);
        private static Task<JToken> InvokeData(Apis api,string selector,FileWriteRequest request,bool invoice,int mode)
            =>invoice?mode==0?Task.FromResult(api.Invoice.AddFileToOutgoingInvoiceWithResult(selector,request)):mode==1?api.Invoice.AddFileToOutgoingInvoiceWithResultAsync(selector,request):api.Invoice.AddFileToOutgoingInvoiceWithResultAsync(selector,request,CancellationToken.None):
                mode==0?Task.FromResult(api.Accounting.AddJournalFileWithResult(selector,request)):mode==1?api.Accounting.AddJournalFileWithResultAsync(selector,request):api.Accounting.AddJournalFileWithResultAsync(selector,request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-File-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
