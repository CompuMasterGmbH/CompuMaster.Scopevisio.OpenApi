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
    public class CatalogWriteTests
    {
        [TestCase("CreatePaymentTermWithResult")]
        [TestCase("UpdatePaymentTermWithResult")]
        [TestCase("AddDimensionEntryWithResult")]
        [TestCase("UpdateDimensionEntryWithResult")]
        public void CompleteCurrentCatalogWriteFormsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("PaymentTermWriteRequest")]
        [TestCase("DimensionEntryWriteRequest")]
        public void CurrentCatalogRequestIsAvailable(string model)=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+model),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema(int family)=>(JObject)Contract()["components"]["schemas"][family<2?"PaymentTermForm":"DimensionEntryForm"]["properties"];
        private static Type Model(int family)=>family<2?typeof(PaymentTermWriteRequest):typeof(DimensionEntryWriteRequest);
        private static bool Creating(int family)=>family==0||family==2;
        private static JObject Valid(int family)=>family==0?(JObject)new PaymentTermWriteRequest{Name="fixture"}:family==1?new PaymentTermWriteRequest():family==2?(JObject)new DimensionEntryWriteRequest{Number=42,Name="fixture",Locked=false}:new DimensionEntryWriteRequest{Number=42,Name="fixture"};
        private static string Name(int family)=>new[]{"CreatePaymentTermWithResult","UpdatePaymentTermWithResult","AddDimensionEntryWithResult","UpdateDimensionEntryWithResult"}[family];
        private static string FieldProperty(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        public static IEnumerable<TestCaseData> Fields()
        {
            foreach(var family in new[]{0,1,2,3})foreach(var field in Schema(family).Properties())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(family,field.Name,mode);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownKindsStopBeforeHttp(int family,string field,int mode)
        {
            var request=Valid(family);string kind=Schema(family)[field].Value<string>("type");request[field]=kind=="string"?new JValue(false):kind=="boolean"?new JValue("false"):kind=="integer"?new JValue(7.25m):(JToken)new JObject();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public async Task ExplicitNullKeepsUpdateSelectionRequirements(int family,string field,int mode)
        {
            var request=Valid(family);request[field]=JValue.CreateNull();bool invalid=Creating(family)||(family==3&&(field=="number"||field=="name"));var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",request,family,mode,CancellationToken.None);}));
                else{await Invoke(Api(http),1,"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public void EveryTypedNullSetterOmitsFields([Values(0,2)]int family)
        {
            var request=family==0?(JObject)new PaymentTermWriteRequest():new DimensionEntryWriteRequest();request["future"]=false;
            foreach(var field in Schema(family).Properties()){request[field.Name]=JValue.CreateNull();Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}
            Assert.That(request.Count,Is.EqualTo(1));Assert.That(request.Value<bool>("future"),Is.False);
        }

        [Test]
        public void ConstructorsCopyFutureData()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var payment=new PaymentTermWriteRequest(original);var dimension=new DimensionEntryWriteRequest(original);original["future"]["value"]=2;
            Assert.That(payment["future"].Value<int>("value"),Is.EqualTo(1));Assert.That(dimension["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new PaymentTermWriteRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new DimensionEntryWriteRequest(null)));
        }

        [Test]
        public void PrimaryOptionalPaymentRequiredDimensionAndRenameSemanticsStayExplicit()
        {
            var c=Contract();var paths=new[]{"/paymentterm/new","/paymentterm/{id}","/dimensions/{dimensionNameOrNumber}/dimensionentry/new","/dimensions/{dimensionNameOrNumber}/dimensionentry"};
            foreach(var family in new[]{0,1,2,3})
            {
                var post=c["paths"][paths[family]]["post"];if(family<2)Assert.That(post["requestBody"]["required"],Is.Null);else Assert.That(post["requestBody"].Value<bool>("required"),Is.True);
                Assert.That(post.Value<string>("description"),Does.Contain(family<2?"Stammdaten, Zahlungsbedingungen (Bearbeiten)":"Berichte (Bearbeiten)"));Assert.That(post["responses"][Creating(family)?"201":"200"]["content"],Is.Null);
            }
            Assert.That(c["components"]["schemas"]["PaymentTermForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"name"}));Assert.That(c["paths"][paths[0]]["post"]["responses"]["400"].Value<string>("description"),Does.Contain("nettimelimit missing"));
            Assert.That(c["components"]["schemas"]["DimensionEntryForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"number","name","locked"}));Assert.That(c["paths"][paths[3]]["post"].Value<string>("summary"),Does.Contain("name"));Assert.That(c["paths"][paths[3]]["post"]["responses"]["400"].Value<string>("description"),Does.Contain("number"));
        }

        [Test]
        public async Task LegacyPaymentObjectResultsAndModelsRemainAvailable()
        {
            var payment=new PaymentTermForm(name:"legacy");var dimension=new DimensionEntryForm(name:"legacy");Assert.That(payment.CashDiscountDays1,Is.EqualTo(0));Assert.That(payment.Percentage1,Is.EqualTo(0));Assert.That(dimension.Number,Is.EqualTo(0));Assert.That(typeof(DimensionEntryForm).GetProperty("Locked"),Is.Null);Assert.That(typeof(DimensionEntryForm).GetProperty("LegacyId"),Is.Null);
            foreach(var name in new[]{"CreatePaymentTerm","UpdatePaymentTerm","AddDimensionEntry","UpdateDimensionEntry"})Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));
            var handler=new Handler(token=>Task.FromResult(Response(201,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var info=await Api(http).CreatePaymentTermAsyncWithHttpInfo(payment);Assert.That(info.Data,Is.InstanceOf<JObject>());Assert.That(((JObject)info.Data).Value<bool>("legacy"),Is.True);Assert.That(info.RawJsonContent,Is.EqualTo("{\"legacy\":true}"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task AllTypedFieldsPreserveWireAndMetadata([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode)
        {
            var request=family<2?(JObject)new PaymentTermWriteRequest():new DimensionEntryWriteRequest();var expected=new JObject();int count=family<2?7:4;Assert.That(Schema(family).Count,Is.EqualTo(count));Assert.That(Model(family).GetProperties().Count(p=>p.DeclaringType==Model(family)),Is.EqualTo(count));
            foreach(var field in Schema(family).Properties())
            {
                string kind=field.Value.Value<string>("type");object value=kind=="string"?(object)("  original / "+field.Name):kind=="boolean"?(object)false:kind=="number"?(object)123456789.123456789m:long.MinValue;
                Model(family).GetProperties().Single(p=>p.DeclaringType==Model(family)&&p.Name==FieldProperty(field.Name)).SetValue(request,value);expected[field.Name]=new JValue(value);
            }
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,Result)));
            using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-catalog-write-token"};var api=new AccountingApi(config);var info=await Invoke(api,long.MinValue,"0007 / dim",request,family,mode,CancellationToken.None);
                Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(family==0?"/rest/paymentterm/new":family==1?"/rest/paymentterm/-9223372036854775808":family==2?"/rest/dimensions/0007%20%2F%20dim/dimensionentry/new":"/rest/dimensions/0007%20%2F%20dim/dimensionentry"));Assert.That(handler.Uri.Query,Is.Empty);
                Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-catalog-write-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));Assert.That(info.StatusCode,Is.EqualTo(Creating(family)?201:200));
                Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Catalog-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(config.AccessToken,Is.EqualTo("isolated-catalog-write-token"));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task PaymentBodyOmissionAndSuppliedCreationNameStayDistinct([Values(0,1)]int family,[Values(0,1,2)]int mode,[Values("absent","empty","name","empty-name","null-name")]string selection)
        {
            var request=selection=="absent"?null:new PaymentTermWriteRequest();if(selection=="name")request.Name="fixture";if(selection=="empty-name")request.Name="";if(selection=="null-name")request["name"]=JValue.CreateNull();bool invalid=family==0&&(selection=="empty"||selection=="null-name");var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,"")));
            using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),0,"fixture",request,family,mode,CancellationToken.None);}));
                else{var info=await Invoke(Api(http),0,"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public async Task DimensionRequiresOriginalNumberNameAndExplicitCreationLock([Values(2,3)]int family,[Values(0,1,2)]int mode,[Values("absent","empty","number","name","number-name","locked-false","locked-true","locked-null")]string selection)
        {
            var request=selection=="absent"?null:new DimensionEntryWriteRequest();if(selection=="number"||selection=="number-name"||selection.StartsWith("locked-",StringComparison.Ordinal))request.Number=0;if(selection=="name"||selection=="number-name"||selection.StartsWith("locked-",StringComparison.Ordinal))request.Name="";
            if(selection=="locked-false")request.Locked=false;if(selection=="locked-true")request.Locked=true;if(selection=="locked-null")request["locked"]=JValue.CreateNull();
            bool invalid=selection=="absent"||selection=="empty"||selection=="number"||selection=="name"||(family==2&&(selection=="number-name"||selection=="locked-null"));var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,"")));
            using(var http=new HttpClient(handler))
            {
                if(request==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),0,"fixture",request,family,mode,CancellationToken.None);}));else if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),0,"fixture",request,family,mode,CancellationToken.None);}));
                else{var info=await Invoke(Api(http),0,"fixture",request,family,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        public static IEnumerable<TestCaseData> Integers()
        {
            foreach(var family in new[]{0,1,2,3})foreach(var field in Schema(family).Properties().Where(p=>p.Value.Value<string>("type")=="integer"))foreach(var json in new[]{"7.25","\"7\"","9223372036854775808","-9223372036854775809"})yield return new TestCaseData(family,field.Name,json);
        }
        [TestCaseSource(nameof(Integers))]
        public void Int64WidthsRejectCoercion(int family,string field,string json)
        {
            var request=Valid(family);request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",request,family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task PercentageTokensRemainOriginal([Values(0,1)]int family,[Values("percentage1","percentage2")]string field,[Values("0","-12.75","123456789.123456789","92233720368547758081234567890","1e100")]string json)
        {
            var request=Valid(family);request[field]=JsonConvert.DeserializeObject<JToken>(json,new JsonSerializerSettings{FloatParseHandling=json=="1e100"?FloatParseHandling.Double:FloatParseHandling.Decimal});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))await Invoke(Api(http),1,"fixture",request,family,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request.ToString(Formatting.None)));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void NonFinitePercentagesStopBeforeHttp([Values(0,1)]int family,[Values("percentage1","percentage2")]string field,[Values(0,1,2,3,4,5)]int selection,[Values(0,1,2)]int mode)
        {
            object value=selection==0?(object)double.NaN:selection==1?(object)double.PositiveInfinity:selection==2?(object)double.NegativeInfinity:selection==3?(object)float.NaN:selection==4?(object)float.PositiveInfinity:float.NegativeInfinity;var request=Valid(family);request[field]=new JValue(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));
            using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",request,family,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task NoScheduleOrUpdateLockIsChosenImplicitly()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);await Invoke(api,1,"fixture",new PaymentTermWriteRequest{Name="original"},0,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo("{\"name\":\"original\"}"));
                await Invoke(api,1,"fixture",new DimensionEntryWriteRequest{Number=7,Name="new"},3,2,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo("{\"number\":7,\"name\":\"new\"}"));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid(family);request["name"]="before";request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);
            var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),1,"fixture",request,family,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request["name"]="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task OriginalDimensionSelectorRemainsOneSegment([Values(2,3)]int family,[Values(0,1,2)]int mode,[Values(null,".","..","0007","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),1,selector,Valid(family),family,mode,CancellationToken.None);}));
                else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),1,selector,Valid(family),family,mode,CancellationToken.None);}));
                else{await Invoke(Api(http),1,selector,Valid(family),family,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/dimensions/"+Uri.EscapeDataString(selector)+"/dimensionentry"+(family==2?"/new":"")));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task OriginalPaymentTermIdUsesInvariantText([Values(long.MinValue,0L,long.MaxValue)]long id)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));var previous=CultureInfo.CurrentCulture;try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");using(var http=new HttpClient(handler))await Invoke(Api(http),id,"fixture",new PaymentTermWriteRequest(),1,2,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/paymentterm/"+id.ToString(CultureInfo.InvariantCulture)));}finally{CultureInfo.CurrentCulture=previous;}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public async Task CompleteDataFormsRetainEveryResultKind([Values(0,1,2,3)]int family,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),1,"fixture",Valid(family),family,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,1,2,3)]int family,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original catalog diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,1,"fixture",Valid(family),family,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original catalog diagnostic"));var expected=new InvalidOperationException("custom catalog hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(family)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,1,"fixture",Valid(family),family,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(Creating(family)?201:200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",Valid(family),family,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoEffects([Values(0,1,2,3)]int family)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),1,"fixture",Valid(family),family,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(0,1,2,3)]int family)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),1,"fixture",Valid(family),family,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-catalog-write-token"});
        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api,long id,string selector,JObject request,int family,int mode,CancellationToken token)
            =>family==0?mode==0?Task.FromResult(api.CreatePaymentTermWithResultWithHttpInfo((PaymentTermWriteRequest)request)):mode==1?api.CreatePaymentTermWithResultAsyncWithHttpInfo((PaymentTermWriteRequest)request):api.CreatePaymentTermWithResultAsyncWithHttpInfo((PaymentTermWriteRequest)request,token):
              family==1?mode==0?Task.FromResult(api.UpdatePaymentTermWithResultWithHttpInfo(id,(PaymentTermWriteRequest)request)):mode==1?api.UpdatePaymentTermWithResultAsyncWithHttpInfo(id,(PaymentTermWriteRequest)request):api.UpdatePaymentTermWithResultAsyncWithHttpInfo(id,(PaymentTermWriteRequest)request,token):
              family==2?mode==0?Task.FromResult(api.AddDimensionEntryWithResultWithHttpInfo(selector,(DimensionEntryWriteRequest)request)):mode==1?api.AddDimensionEntryWithResultAsyncWithHttpInfo(selector,(DimensionEntryWriteRequest)request):api.AddDimensionEntryWithResultAsyncWithHttpInfo(selector,(DimensionEntryWriteRequest)request,token):
              mode==0?Task.FromResult(api.UpdateDimensionEntryWithResultWithHttpInfo(selector,(DimensionEntryWriteRequest)request)):mode==1?api.UpdateDimensionEntryWithResultAsyncWithHttpInfo(selector,(DimensionEntryWriteRequest)request):api.UpdateDimensionEntryWithResultAsyncWithHttpInfo(selector,(DimensionEntryWriteRequest)request,token);
        private static Task<JToken> InvokeData(AccountingApi api,long id,string selector,JObject request,int family,int mode)
            =>family==0?mode==0?Task.FromResult(api.CreatePaymentTermWithResult((PaymentTermWriteRequest)request)):mode==1?api.CreatePaymentTermWithResultAsync((PaymentTermWriteRequest)request):api.CreatePaymentTermWithResultAsync((PaymentTermWriteRequest)request,CancellationToken.None):
              family==1?mode==0?Task.FromResult(api.UpdatePaymentTermWithResult(id,(PaymentTermWriteRequest)request)):mode==1?api.UpdatePaymentTermWithResultAsync(id,(PaymentTermWriteRequest)request):api.UpdatePaymentTermWithResultAsync(id,(PaymentTermWriteRequest)request,CancellationToken.None):
              family==2?mode==0?Task.FromResult(api.AddDimensionEntryWithResult(selector,(DimensionEntryWriteRequest)request)):mode==1?api.AddDimensionEntryWithResultAsync(selector,(DimensionEntryWriteRequest)request):api.AddDimensionEntryWithResultAsync(selector,(DimensionEntryWriteRequest)request,CancellationToken.None):
              mode==0?Task.FromResult(api.UpdateDimensionEntryWithResult(selector,(DimensionEntryWriteRequest)request)):mode==1?api.UpdateDimensionEntryWithResultAsync(selector,(DimensionEntryWriteRequest)request):api.UpdateDimensionEntryWithResultAsync(selector,(DimensionEntryWriteRequest)request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Catalog-Fixture","retained");return response;}
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
