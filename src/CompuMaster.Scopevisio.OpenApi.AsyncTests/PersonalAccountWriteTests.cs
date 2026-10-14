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
    public class PersonalAccountWriteTests
    {
        [TestCase("CreateDebitorWithResult")]
        [TestCase("CreateKreditorWithResult")]
        [TestCase("UpdateDebitorWithResult")]
        [TestCase("UpdateKreditorWithResult")]
        public void CompleteCurrentAccountWriteFormsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("PersonalAccountCreationRequest")]
        [TestCase("PersonalAccountUpdateRequest")]
        public void CurrentAccountRequestIsAvailable(string model)=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+model),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static bool Creating(int family)=>family<2;
        private static JObject Schema(int family)=>(JObject)Contract()["components"]["schemas"][Creating(family)?"PersonalAccountForm":"UpdatePersonalAccountForm"]["properties"];
        private static Type Model(int family)=>Creating(family)?typeof(PersonalAccountCreationRequest):typeof(PersonalAccountUpdateRequest);
        private static JObject Valid(int family)=>Creating(family)?(JObject)new PersonalAccountCreationRequest{ContactId=42}:new PersonalAccountUpdateRequest();
        private static string Name(int family)=>new[]{"CreateDebitorWithResult","CreateKreditorWithResult","UpdateDebitorWithResult","UpdateKreditorWithResult"}[family];
        private static string FieldProperty(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static string Kind(JToken field)=>field.Value<string>("type")??"object";
        private static JObject Payment()=>new JObject{["name"]="original payment",["percentage1"]=123456789.123456789m,["cashDiscountDays1"]=0,["future"]=new JObject{["value"]=1}};
        public static IEnumerable<TestCaseData> Fields(){foreach(var family in new[]{0,1,2,3})foreach(var field in Schema(family).Properties())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(family,field.Name,mode);}

        [TestCaseSource(nameof(Fields))]
        public void KnownKindsStopBeforeHttp(int family,string field,int mode)
        {
            var request=Valid(family);string kind=Kind(Schema(family)[field]);request[field]=kind=="string"?new JValue(false):kind=="integer"?new JValue(7.25m):kind=="boolean"?new JValue("false"):new JValue(false);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public async Task ExplicitNullReachesOnlySparseUpdate(int family,string field,int mode)
        {
            var request=Valid(family);request[field]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(Creating(family))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);}));else{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(Creating(family)?0:1));
        }

        [Test]
        public void ConstructorsNestedSetterAndTypedNullsCopyAndOmit([Values(0,2)]int family)
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};JObject request=Creating(family)?(JObject)new PersonalAccountCreationRequest(original):new PersonalAccountUpdateRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));
            var payment=Payment();Model(family).GetProperty("PaymentTermForm").SetValue(request,payment);payment["future"]["value"]=2;Assert.That(request["paymentTermForm"]["future"].Value<int>("value"),Is.EqualTo(1));
            foreach(var field in Schema(family).Properties()){request[field.Name]=JValue.CreateNull();Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.EqualTo(1));
            Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new PersonalAccountCreationRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new PersonalAccountUpdateRequest(null)));
        }

        [Test]
        public void PrimaryBodyRequirementsStatusAndNestedServerEffectsRemainExplicit()
        {
            var c=Contract();var paths=new[]{"/createdebitor","/createkreditor","/debitoraccounts/{accountNumber}","/kreditoraccounts/{accountNumber}"};var profiles=new[]{"Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten)","Datenimport (Bearbeiten), Kontakte bearbeiten (Bearbeiten)","Stammdaten, Debitorenstamm (Bearbeiten)","Stammdaten, Kreditorenstamm (Bearbeiten)"};
            foreach(var family in new[]{0,1,2,3}){var p=c["paths"][paths[family]]["post"];if(Creating(family))Assert.That(p["requestBody"]["required"],Is.Null);else Assert.That(p["requestBody"].Value<bool>("required"),Is.True);Assert.That(p.Value<string>("description"),Does.Contain(profiles[family]));Assert.That(p["responses"]["200"]["content"],Is.Null);if(Creating(family))Assert.That(p["responses"]["201"]["content"],Is.Null);}
            Assert.That(c["components"]["schemas"]["PersonalAccountForm"]["required"],Is.Null);Assert.That(c["components"]["schemas"]["UpdatePersonalAccountForm"]["required"],Is.Null);Assert.That(Schema(0)["group"].Value<string>("description"),Does.Contain("created"));Assert.That(Schema(0)["paymentTermId"].Value<string>("description"),Does.Contain("If both"));Assert.That(c["paths"]["/createkreditor"]["post"]["responses"]["200"].Value<string>("description"),Does.Contain("no free numbers"));
        }

        [Test]
        public void LegacyModelsFieldsAndApisRemainAvailable()
        {
            var original=new PersonalAccountForm();var update=new UpdatePersonalAccountForm();Assert.That(original.ContactId,Is.EqualTo(0));Assert.That(update.NumberRangeNumber,Is.EqualTo(0));foreach(var field in new[]{"ExternalNumber","SumAccountNumber","ContoProDiverse"})Assert.That(typeof(PersonalAccountForm).GetProperty(field),Is.Null);foreach(var field in new[]{"ExternalNumber","SumAccountNumber"})Assert.That(typeof(UpdatePersonalAccountForm).GetProperty(field),Is.Null);
            foreach(var name in new[]{"CreateDebitor","CreateKreditor","UpdateDebitor","UpdateKreditor"})Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));
        }

        [Test]
        public async Task AllTypedFieldsRetainOriginalWireAndMetadata([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode)
        {
            JObject request=Creating(family)?(JObject)new PersonalAccountCreationRequest():new PersonalAccountUpdateRequest();var expected=new JObject();int count=Creating(family)?17:12;Assert.That(Schema(family).Count,Is.EqualTo(count));Assert.That(Model(family).GetProperties().Count(p=>p.DeclaringType==Model(family)),Is.EqualTo(count));
            foreach(var field in Schema(family).Properties()){string kind=Kind(field.Value);object value=kind=="string"?(object)("  original / "+field.Name):kind=="integer"?(object)long.MinValue:kind=="boolean"?(object)false:Payment();Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,value);expected[field.Name]=value is JToken token?token.DeepClone():new JValue(value);}
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,Result)));using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-personal-account-token"};var api=new AccountingApi(config);var info=await Invoke(api,"0007 / account",request,family,mode,CancellationToken.None);
                Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(config.AccessToken,Is.EqualTo("isolated-personal-account-token"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(family==0?"/rest/createdebitor":family==1?"/rest/createkreditor":family==2?"/rest/debitoraccounts/0007%20%2F%20account":"/rest/kreditoraccounts/0007%20%2F%20account"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-personal-account-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(info.StatusCode,Is.EqualTo(Creating(family)?201:200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Account-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureNumber"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task AtLeastOneContactSelectorIsRequiredWithoutXorOrDefaults([Values(0,1)]int family,[Values(0,1,2)]int mode,[Values(0,1,2,3,4,5,6,7)]int mask)
        {
            var request=new PersonalAccountCreationRequest();if((mask&1)!=0)request.ContactId=0;if((mask&2)!=0)request.CustomerNumber=" 0007 ";if((mask&4)!=0)request.Email=" original@example.invalid ";var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));
            using(var http=new HttpClient(handler)){if(mask==0)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);}));else{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}}Assert.That(handler.Calls,Is.EqualTo(mask==0?0:1));
        }

        [Test]
        public async Task CreationBodyIsOptionalWhileUpdateBodyIsRequired([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode,[Values(false,true)]bool absent)
        {
            var request=absent?null:Valid(family);var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,"")));using(var http=new HttpClient(handler))
            {
                if(absent&&!Creating(family))Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);}));else{var info=await Invoke(Api(http),"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(absent&&!Creating(family)?0:1));
        }

        public static IEnumerable<TestCaseData> Integers(){foreach(var family in new[]{0,1,2,3})foreach(var field in Schema(family).Properties().Where(p=>Kind(p.Value)=="integer"))foreach(var json in new[]{"7.25","\"7\"","9223372036854775808","-9223372036854775809"})yield return new TestCaseData(family,field.Name,json);}
        [TestCaseSource(nameof(Integers))]
        public void Int64FieldsCannotCoerceOrOverflow(int family,string field,string json)
        {
            var request=Valid(family);request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task NestedPaymentIdAndFormRemainTogetherWithoutLookup([Values(0,1,2,3)]int family)
        {
            var request=Valid(family);request["paymentTermId"]=0;request["paymentTermForm"]=Payment();request["group"]=" caller chosen group ";request["vatCode"]=" configured-vat / § ";request["paymentType"]="Rechnung,EC-Karte";request["currency"]=" original currency ";request["language"]=" original language ";request["sumAccountNumber"]=" 001400 ";request["externalNumber"]=" 0007 ";var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))await Invoke(Api(http),"fixture",request,family,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void InvalidNestedPaymentCannotReachHttp([Values(0,1,2,3)]int family,[Values("{}","{\"name\":null}","{\"name\":4}","{\"name\":\"x\",\"percentage1\":\"7\"}","{\"name\":\"x\",\"cashDiscountDays1\":7.25}","{\"name\":\"x\",\"netTimeLimit\":9223372036854775808}")]string json)
        {
            var request=Valid(family);request["paymentTermId"]=42;request["paymentTermForm"]=JObject.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public void NestedNonFinitePercentageCannotReachHttp([Values(0,1,2,3)]int family)
        {
            var request=Valid(family);var payment=new JObject{["name"]="x",["percentage1"]=double.NaN};request["paymentTermForm"]=payment;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task SnapshotIncludesNestedPaymentBeforeActiveHttp([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid(family);request["paymentTermForm"]=Payment();request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),"fixture",request,family,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request["paymentTermForm"]["future"]["value"]=2;request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task AccountNumbersStayOriginalEscapedStrings([Values(2,3)]int family,[Values(0,1,2)]int mode,[Values(null,".","..","0007008","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,Valid(family),family,mode,CancellationToken.None);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,Valid(family),family,mode,CancellationToken.None);}));else{await Invoke(Api(http),selector,Valid(family),family,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo((family==2?"/rest/debitoraccounts/":"/rest/kreditoraccounts/")+Uri.EscapeDataString(selector)));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task BothCreationStatusesPreserveResultsWithoutCreatedInference([Values(0,1)]int family,[Values(0,1,2)]int mode,[Values(200,201)]int status)
        {
            var json="{\"reason\":\"existing-or-range-exhausted\",\"number\":\"0007008\"}";var handler=new Handler(token=>Task.FromResult(Response(status,json)));using(var http=new HttpClient(handler)){var info=await Invoke(Api(http),"fixture",Valid(family),family,mode,CancellationToken.None);Assert.That(info.StatusCode,Is.EqualTo(status));Assert.That(info.RawJsonContent,Is.EqualTo(json));Assert.That(info.Data.ToString(Formatting.None),Is.EqualTo(json));Assert.That(((JObject)info.Data).Count,Is.EqualTo(2));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task CompleteDataFormsRetainEveryJsonKind([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),"fixture",Valid(family),family,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,1,2,3)]int family,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original account diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,"fixture",Valid(family),family,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original account diagnostic"));var expected=new InvalidOperationException("custom account hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(family)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,"fixture",Valid(family),family,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",Valid(family),family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoEffects([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",Valid(family),family,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeTransportWithoutReplay([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),"fixture",Valid(family),family,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        private const string Result="{\"futureNumber\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-personal-account-token"});
        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api,string selector,JObject request,int family,int mode,CancellationToken token)
            =>family==0?mode==0?Task.FromResult(api.CreateDebitorWithResultWithHttpInfo((PersonalAccountCreationRequest)request)):mode==1?api.CreateDebitorWithResultAsyncWithHttpInfo((PersonalAccountCreationRequest)request):api.CreateDebitorWithResultAsyncWithHttpInfo((PersonalAccountCreationRequest)request,token):
              family==1?mode==0?Task.FromResult(api.CreateKreditorWithResultWithHttpInfo((PersonalAccountCreationRequest)request)):mode==1?api.CreateKreditorWithResultAsyncWithHttpInfo((PersonalAccountCreationRequest)request):api.CreateKreditorWithResultAsyncWithHttpInfo((PersonalAccountCreationRequest)request,token):
              family==2?mode==0?Task.FromResult(api.UpdateDebitorWithResultWithHttpInfo(selector,(PersonalAccountUpdateRequest)request)):mode==1?api.UpdateDebitorWithResultAsyncWithHttpInfo(selector,(PersonalAccountUpdateRequest)request):api.UpdateDebitorWithResultAsyncWithHttpInfo(selector,(PersonalAccountUpdateRequest)request,token):
              mode==0?Task.FromResult(api.UpdateKreditorWithResultWithHttpInfo(selector,(PersonalAccountUpdateRequest)request)):mode==1?api.UpdateKreditorWithResultAsyncWithHttpInfo(selector,(PersonalAccountUpdateRequest)request):api.UpdateKreditorWithResultAsyncWithHttpInfo(selector,(PersonalAccountUpdateRequest)request,token);
        private static Task<JToken> InvokeData(AccountingApi api,string selector,JObject request,int family,int mode)
            =>family==0?mode==0?Task.FromResult(api.CreateDebitorWithResult((PersonalAccountCreationRequest)request)):mode==1?api.CreateDebitorWithResultAsync((PersonalAccountCreationRequest)request):api.CreateDebitorWithResultAsync((PersonalAccountCreationRequest)request,CancellationToken.None):
              family==1?mode==0?Task.FromResult(api.CreateKreditorWithResult((PersonalAccountCreationRequest)request)):mode==1?api.CreateKreditorWithResultAsync((PersonalAccountCreationRequest)request):api.CreateKreditorWithResultAsync((PersonalAccountCreationRequest)request,CancellationToken.None):
              family==2?mode==0?Task.FromResult(api.UpdateDebitorWithResult(selector,(PersonalAccountUpdateRequest)request)):mode==1?api.UpdateDebitorWithResultAsync(selector,(PersonalAccountUpdateRequest)request):api.UpdateDebitorWithResultAsync(selector,(PersonalAccountUpdateRequest)request,CancellationToken.None):
              mode==0?Task.FromResult(api.UpdateKreditorWithResult(selector,(PersonalAccountUpdateRequest)request)):mode==1?api.UpdateKreditorWithResultAsync(selector,(PersonalAccountUpdateRequest)request):api.UpdateKreditorWithResultAsync(selector,(PersonalAccountUpdateRequest)request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Account-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
