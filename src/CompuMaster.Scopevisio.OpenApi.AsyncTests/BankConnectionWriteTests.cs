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
    public class BankConnectionWriteTests
    {
        [TestCase("CreateDebitorBankConnectionWithResult")]
        [TestCase("CreateKreditorBankConnectionWithResult")]
        public void CompleteCurrentBankWriteFormsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentBankRequestIsAvailable()=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.BankConnectionWriteRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["BankConnectionForm"]["properties"];
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static string Name(bool kreditor)=>kreditor?"CreateKreditorBankConnectionWithResult":"CreateDebitorBankConnectionWithResult";
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties())foreach(var kreditor in new[]{false,true})foreach(var mode in new[]{0,1,2})yield return new TestCaseData(field.Name,kreditor,mode);}

        [TestCaseSource(nameof(Fields))]
        public void KnownFieldsRequireOriginalKinds(string field,bool kreditor,int mode)
        {
            var request=new BankConnectionWriteRequest();request[field]=Schema()[field].Value<string>("type")=="boolean"?(JToken)new JValue("false"):new JValue(false);var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,kreditor,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownExplicitNullsFailBeforeHttp(string field,bool kreditor,int mode)
        {
            var request=new BankConnectionWriteRequest{[field]=JValue.CreateNull()};var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,kreditor,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public void ConstructorCopiesFutureDataAndAllTypedNullsOmit()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new BankConnectionWriteRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new BankConnectionWriteRequest(null)));
            foreach(var field in Schema().Properties()){request[field.Name]=JValue.CreateNull();typeof(BankConnectionWriteRequest).GetProperty(Property(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.EqualTo(1));
        }

        [Test]
        public void PrimaryOptionalBodyHasNoInventedRequiredFields()
        {
            var c=Contract();Assert.That(c["components"]["schemas"]["BankConnectionForm"]["required"],Is.Null);foreach(var kreditor in new[]{false,true})
            {
                var p=c["paths"][kreditor?"/kreditoraccounts/{accountNumber}/bankConnection/new":"/debitoraccounts/{accountNumber}/bankConnection/new"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["parameters"][0]["schema"].Value<string>("type"),Is.EqualTo("string"));Assert.That(p["responses"]["201"]["content"],Is.Null);Assert.That(p["responses"]["400"].Value<string>("description"),Does.Contain("iban or bank account number and bank code"));Assert.That(p.Value<string>("description"),Does.Contain(kreditor?"Stammdaten, Kreditorenstamm (Bearbeiten)":"Stammdaten, Debitorenstamm (Bearbeiten)"));
            }
        }

        [Test]
        public async Task AllTypedFieldsRemainOriginalWithCompleteMetadata([Values(false,true)]bool kreditor,[Values(0,1,2)]int mode)
        {
            var request=new BankConnectionWriteRequest();var expected=new JObject();Assert.That(Schema().Count,Is.EqualTo(8));Assert.That(typeof(BankConnectionWriteRequest).GetProperties().Count(p=>p.DeclaringType==typeof(BankConnectionWriteRequest)),Is.EqualTo(8));
            foreach(var field in Schema().Properties()){object value=field.Value.Value<string>("type")=="boolean"?(object)false:"  original / "+field.Name;typeof(BankConnectionWriteRequest).GetProperty(Property(field.Name)).SetValue(request,value);expected[field.Name]=new JValue(value);}
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(201,Result)));using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-bank-write-token"};var api=new AccountingApi(config);var info=await Invoke(api,"0007 / account",request,kreditor,mode,CancellationToken.None);Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(config.AccessToken,Is.EqualTo("isolated-bank-write-token"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo((kreditor?"/rest/kreditoraccounts/":"/rest/debitoraccounts/")+"0007%20%2F%20account/bankConnection/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-bank-write-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(info.StatusCode,Is.EqualTo(201));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Bank-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodyAndStandardChoicesDoNotInsertDefaults([Values(false,true)]bool kreditor,[Values(0,1,2)]int mode,[Values("absent","empty","false","true","strings")]string selection)
        {
            var request=selection=="absent"?null:new BankConnectionWriteRequest();if(selection=="false")request.Standard=false;if(selection=="true")request.Standard=true;if(selection=="strings"){request.Number=" 00001234 ";request.BankCode=" 0000100 ";request.Iban=" original iban / § ";request.SwiftBic=" original BIC ";request.Description="";}var handler=new Handler(token=>Task.FromResult(Response(201,"")));
            using(var http=new HttpClient(handler)){var info=await Invoke(Api(http),"fixture",request,kreditor,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task OriginalAccountSelectorRemainsOneSegment([Values(false,true)]bool kreditor,[Values(0,1,2)]int mode,[Values(null,".","..","0007008","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,new BankConnectionWriteRequest(),kreditor,mode,CancellationToken.None);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,new BankConnectionWriteRequest(),kreditor,mode,CancellationToken.None);}));else{await Invoke(Api(http),selector,new BankConnectionWriteRequest(),kreditor,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo((kreditor?"/rest/kreditoraccounts/":"/rest/debitoraccounts/")+Uri.EscapeDataString(selector)+"/bankConnection/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false,true)]bool kreditor)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=new BankConnectionWriteRequest{Iban="before",Standard=false,["future"]=new JObject{["value"]=1}};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(201,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),"fixture",request,kreditor,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.Iban="after";request.Standard=true;request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventConnectionIds([Values(false,true)]bool kreditor,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),"fixture",new BankConnectionWriteRequest(),kreditor,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(false,true)]bool kreditor,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original bank diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,"fixture",new BankConnectionWriteRequest(),kreditor,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original bank diagnostic"));var expected=new InvalidOperationException("custom bank hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(kreditor)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,"fixture",new BankConnectionWriteRequest(),kreditor,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(false,true)]bool kreditor)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",new BankConnectionWriteRequest(),kreditor,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoEffects([Values(false,true)]bool kreditor)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",new BankConnectionWriteRequest(),kreditor,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(false,true)]bool kreditor)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(201,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),"fixture",new BankConnectionWriteRequest(),kreditor,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        [Test]
        public async Task LegacyDefaultsNamesAndObjectResultsRemainAvailable()
        {
            var original=new BankConnectionForm();Assert.That(original.Standard,Is.False);Assert.That(typeof(BankConnectionForm).GetProperty("Description"),Is.Null);foreach(var name in new[]{"CreateDebitorBankConnection","CreateKebitorBankConnection"})Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));
            var handler=new Handler(token=>Task.FromResult(Response(201,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);var first=api.CreateDebitorBankConnectionWithHttpInfo("legacy",original);var second=await api.CreateKebitorBankConnectionAsyncWithHttpInfo("legacy",original);Assert.That(first.Data,Is.InstanceOf<JObject>());Assert.That(second.Data,Is.InstanceOf<JObject>());Assert.That(((JObject)second.Data).Value<bool>("legacy"),Is.True);}Assert.That(handler.Calls,Is.EqualTo(2));
        }

        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-bank-write-token"});
        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api,string selector,BankConnectionWriteRequest request,bool kreditor,int mode,CancellationToken token)
            =>kreditor?mode==0?Task.FromResult(api.CreateKreditorBankConnectionWithResultWithHttpInfo(selector,request)):mode==1?api.CreateKreditorBankConnectionWithResultAsyncWithHttpInfo(selector,request):api.CreateKreditorBankConnectionWithResultAsyncWithHttpInfo(selector,request,token):
                mode==0?Task.FromResult(api.CreateDebitorBankConnectionWithResultWithHttpInfo(selector,request)):mode==1?api.CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(selector,request):api.CreateDebitorBankConnectionWithResultAsyncWithHttpInfo(selector,request,token);
        private static Task<JToken> InvokeData(AccountingApi api,string selector,BankConnectionWriteRequest request,bool kreditor,int mode)
            =>kreditor?mode==0?Task.FromResult(api.CreateKreditorBankConnectionWithResult(selector,request)):mode==1?api.CreateKreditorBankConnectionWithResultAsync(selector,request):api.CreateKreditorBankConnectionWithResultAsync(selector,request,CancellationToken.None):
                mode==0?Task.FromResult(api.CreateDebitorBankConnectionWithResult(selector,request)):mode==1?api.CreateDebitorBankConnectionWithResultAsync(selector,request):api.CreateDebitorBankConnectionWithResultAsync(selector,request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Bank-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
