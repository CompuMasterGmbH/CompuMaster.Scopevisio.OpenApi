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
    public class OutgoingInvoiceImportTests
    {
        [Test]
        public void CompleteImportFormsAreAvailable()=>Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Count(m=>m.Name.StartsWith("ImportOutgoingInvoicesWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentImportRequestIsAvailable()=>Assert.That(typeof(OutgoingInvoiceApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.OutgoingInvoiceImportRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static string Name()=>"ImportOutgoingInvoicesWithResult";
        private static OutgoingInvoiceImportRequest Valid()=>new OutgoingInvoiceImportRequest{Data=" <documents>原 &amp; \"quoted\"</documents>\r\n ",Template=" original / § "};

        [Test]
        public void PrimaryOptionalBodyRequiresDataAndPreservesResponseErrors()
        {
            var c=Contract();var schema=c["components"]["schemas"]["OutgoingInvoiceImportForm"];Assert.That(schema["required"].Values<string>(),Is.EquivalentTo(new[]{"data"}));Assert.That(((JObject)schema["properties"]).Count,Is.EqualTo(10));Assert.That(((JObject)schema["properties"]).Properties().Count(p=>p.Value.Value<string>("type")=="boolean"),Is.EqualTo(8));Assert.That(schema["properties"]["data"].Value<string>("description"),Is.EqualTo("XML import document"));var route=c["paths"]["/outgoinginvoices/import"]["post"];Assert.That(route["requestBody"]["required"],Is.Null);Assert.That(route["responses"]["200"]["content"],Is.Null);Assert.That(route["responses"]["200"].Value<string>("description"),Does.Contain("Errors"));Assert.That(route.Value<string>("description"),Does.Contain("Angebote, Aufträge, Lieferscheine, Rechnungen (Bearbeiten)"));
        }
        [Test]
        public void ConstructorsAndTypedOmissionCopyCompleteJson()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new OutgoingInvoiceImportRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new OutgoingInvoiceImportRequest(null)));
            request.Data="";request.Template="";request.Data=null;request.Template=null;Assert.That(request.ContainsKey("data"),Is.False);Assert.That(request.ContainsKey("template"),Is.False);Assert.That(request.Count,Is.EqualTo(1));
        }

        [Test]
        public async Task OriginalBase64FutureJsonAndAllMetadataRemain([Values(0,1,2)]int mode)
        {
            var request=Valid();request["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));
            using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var info=await Invoke(apis,request,mode,CancellationToken.None);Assert.That(apis.Invoice.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Config.HttpClient,Is.SameAs(http));Assert.That(apis.Config.AccessToken,Is.EqualTo("isolated-outgoing-invoice-token"));
                Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/outgoinginvoices/import"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-outgoing-invoice-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected));
                Assert.That(info.StatusCode,Is.EqualTo(200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-File-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodyAndFieldsRemainServerDecisions([Values(0,1,2)]int mode,[Values("absent","empty","template-only","empty-data","opaque-data","null-data","null-template","empty-template")]string selection)
        {
            var request=selection=="absent"?null:new OutgoingInvoiceImportRequest();if(selection=="template-only")request.Template="original";if(selection=="empty-data")request.Data="";if(selection=="opaque-data"||selection=="null-template"||selection=="empty-template")request.Data=" original ... opaque \r\n ";if(selection=="null-data")request["data"]=JValue.CreateNull();if(selection=="null-template")request["template"]=JValue.CreateNull();if(selection=="empty-template")request.Template="";
            bool invalid=selection=="empty"||selection=="template-only"||selection=="null-data"||selection=="null-template";var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,mode,CancellationToken.None);}));
                else{var info=await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["OutgoingInvoiceImportForm"]["properties"];
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties())foreach(var mode in new[]{0,1,2})foreach(var shape in new[]{0,1,2,3,4})yield return new TestCaseData(field.Name,mode,shape);}
        [TestCaseSource(nameof(Fields))]
        public void KnownFieldsRequireTheirOriginalKinds(string field,int mode,int shape)
        {
            var request=Valid();request[field]=shape==0?JValue.CreateNull():shape==1?new JObject():shape==2?(JToken)new JArray():shape==3?new JValue(0):Schema()[field].Value<string>("type")=="boolean"?new JValue("false"):new JValue(false);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task SnapshotPrecedesActiveHttp()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid();request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(new Apis(http),request,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.Data="after";request.Template="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventInvoiceId([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(new Apis(http),Valid(),mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryRunsBeforeParsingErrors([Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original invoice diagnostic")));using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(apis,Valid(),mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original invoice diagnostic"));var expected=new InvalidOperationException("custom invoice hook");ExceptionFactory factory=(name,response)=>{Assert.That(name,Is.EqualTo(Name()));return expected;};apis.Invoice.ExceptionFactory=factory;
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(apis,Valid(),mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void MalformedSuccessIsNotAnInventedInvoice()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancelledOperationHasNoEffects()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(new Apis(http),Valid(),2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        [Test]
        public async Task LegacyModelFlagsAndDataDiscardingRemainAvailable()
        {
            var model=new OutgoingInvoiceImportForm(data:"original XML");Assert.That(model.GenerateDocumentNumbers,Is.False);Assert.That(model.DoPost,Is.False);Assert.That(model.CreatePdf,Is.False);Assert.That(model.CopyImpersonalAccountFieldsToPosition,Is.False);Assert.That(typeof(OutgoingInvoiceImportForm).GetProperty("CopyVatKeyAndTaxRateToPosition"),Is.Null);Assert.That(typeof(OutgoingInvoiceApi).GetMethods().Count(m=>m.Name=="ImportOutgoingInvoices"||m.Name=="ImportOutgoingInvoicesWithHttpInfo"||m.Name=="ImportOutgoingInvoicesAsync"||m.Name=="ImportOutgoingInvoicesAsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var apis=new Apis(http);Assert.That(apis.Invoice.ImportOutgoingInvoicesWithHttpInfo(model).Data,Is.Null);Assert.That((await apis.Invoice.ImportOutgoingInvoicesAsyncWithHttpInfo(model)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        [Test]
        public async Task OmittedFalseTrueChoicesAreOriginal([Values("generateDocumentNumbers","doPost","skipDuplicates","createPdf","copyProductToPosition","copyProductToPositionOverwriteMode","copyImpersonalAccountFieldsToPosition","copyVatKeyAndTaxRateToPosition")]string flag,[Values(-1,0,1)]int choice,[Values(0,1,2)]int mode)
        {
            var request=Valid();if(choice>=0)request[flag]=choice==1;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,mode,CancellationToken.None);var sent=JObject.Parse(handler.Body);if(choice<0)Assert.That(sent.ContainsKey(flag),Is.False);else Assert.That(sent.Value<bool>(flag),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task TypedFieldsRemainCompleteWithoutXMLOrTemplateRepair([Values(0,1,2)]int mode)
        {
            var request=new OutgoingInvoiceImportRequest();Assert.That(request.GetType().GetProperties().Count(p=>p.DeclaringType==typeof(OutgoingInvoiceImportRequest)),Is.EqualTo(10));foreach(var field in Schema().Properties()){object value=field.Value.Value<string>("type")=="boolean"?(object)false:" original / "+field.Name;request.GetType().GetProperty(char.ToUpperInvariant(field.Name[0])+field.Name.Substring(1)).SetValue(request,value);}request["future"]=new JObject{["value"]=JValue.CreateNull()};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void AllTypedNullsOmitOriginalFields()
        {
            var request=Valid();foreach(var field in Schema().Properties()){request[field.Name]=JValue.CreateNull();request.GetType().GetProperty(char.ToUpperInvariant(field.Name[0])+field.Name.Substring(1)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.Zero);
        }
        [Test]
        public async Task RawXMLTextIsPreservedIncludingEmptyAndUnicode([Values(""," "," <documents>原 &amp; \"quoted\"</documents>\r\n ","original...not Base64","<documents><amount>-0.001</amount></documents>")]string xml,[Values(0,1,2)]int mode)
        {
            var request=new OutgoingInvoiceImportRequest{Data=xml};var handler=new Handler(token=>Task.FromResult(Response(200,"{\"errors\":[\"row\"],\"imported\":false}")));using(var http=new HttpClient(handler)){var result=await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body).Value<string>("data"),Is.EqualTo(xml));Assert.That(result.Data["errors"][0].Value<string>(),Is.EqualTo("row"));Assert.That(result.Data.Value<bool>("imported"),Is.False);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private sealed class Apis
        {
            internal Configuration Config;internal OutgoingInvoiceApi Invoice;
            internal Apis(HttpClient http){Config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-outgoing-invoice-token"};Invoice=new OutgoingInvoiceApi(Config);}
        }
        private static Task<ApiResponse<JToken>> Invoke(Apis api,OutgoingInvoiceImportRequest request,int mode,CancellationToken token)
            =>mode==0?Task.FromResult(api.Invoice.ImportOutgoingInvoicesWithResultWithHttpInfo(request)):mode==1?api.Invoice.ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(request):api.Invoice.ImportOutgoingInvoicesWithResultAsyncWithHttpInfo(request,token);
        private static Task<JToken> InvokeData(Apis api,OutgoingInvoiceImportRequest request,int mode)
            =>mode==0?Task.FromResult(api.Invoice.ImportOutgoingInvoicesWithResult(request)):mode==1?api.Invoice.ImportOutgoingInvoicesWithResultAsync(request):api.Invoice.ImportOutgoingInvoicesWithResultAsync(request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-File-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
