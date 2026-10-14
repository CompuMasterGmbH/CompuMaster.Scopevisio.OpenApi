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
    public class RecurringInvoiceUpdateTests
    {
        [TestCase("UpdateRecurringInvoiceWithResult")]
        public void CompleteCurrentRecurrenceFormsAreAvailable(string name)=>Assert.That(typeof(RecurringInvoiceApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentRecurrenceRequestIsAvailable()=>Assert.That(typeof(RecurringInvoiceApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.RecurringInvoiceUpdateRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["RecurringInvoiceForm"]["properties"];
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static string Name()=>"UpdateRecurringInvoiceWithResult";
        private static RecurringInvoiceUpdateRequest Valid()=>new RecurringInvoiceUpdateRequest{ValidFrom=0,ValidTo=0,NextInvoiceDate=0,Frequency="monthly"};
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(field.Name,mode);}

        [TestCaseSource(nameof(Fields))]
        public void KnownFieldsRequireOriginalKinds(string field,int mode)
        {
            var request=Valid();request[field]=Schema()[field].Value<string>("type")=="string"?(JToken)new JValue(false):new JValue("0");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [TestCaseSource(nameof(Fields))]
        public void KnownExplicitNullsFailBeforeHttp(string field,int mode)
        {
            var request=new RecurringInvoiceUpdateRequest{[field]=JValue.CreateNull()};var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public void ConstructorCopiesFutureDataAndAllTypedNullsOmit()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new RecurringInvoiceUpdateRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new RecurringInvoiceUpdateRequest(null)));
            foreach(var field in Schema().Properties()){request[field.Name]=JValue.CreateNull();typeof(RecurringInvoiceUpdateRequest).GetProperty(Property(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.EqualTo(1));
        }

        [Test]
        public void PrimaryOptionalBodyRequiresCompleteDedicatedRecurrenceObject()
        {
            var c=Contract();var schema=c["components"]["schemas"]["RecurringInvoiceForm"];Assert.That(schema["required"].Values<string>(),Is.EquivalentTo(new[]{"validFrom","validTo","frequency","nextInvoiceDate"}));Assert.That(schema["properties"]["frequency"]["enum"].Values<string>(),Is.EquivalentTo(new[]{"daily","weekly","monthly","bimonthly","quarterly","half-yearly","yearly"}));var p=c["paths"]["/recurringinvoice/{number}"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["parameters"][0]["schema"].Value<string>("type"),Is.EqualTo("string"));Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p.Value<string>("description"),Does.Contain("Rechnungen (Bearbeiten)"));Assert.That(p["responses"]["404"].Value<string>("description"),Does.Contain("not found").And.Contain("write permissions"));
        }
        [Test]
        public async Task AllTypedFieldsRemainOriginalWithCompleteMetadata([Values(0,1,2)]int mode)
        {
            var request=new RecurringInvoiceUpdateRequest(new JObject());var expected=new JObject();Assert.That(Schema().Count,Is.EqualTo(4));Assert.That(typeof(RecurringInvoiceUpdateRequest).GetProperties().Count(p=>p.DeclaringType==typeof(RecurringInvoiceUpdateRequest)),Is.EqualTo(4));
            foreach(var field in Schema().Properties()){object value=field.Value.Value<string>("type")=="integer"?(object)long.MaxValue:"  original / "+field.Name;typeof(RecurringInvoiceUpdateRequest).GetProperty(Property(field.Name)).SetValue(request,value);expected[field.Name]=new JValue(value);}
            expected["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};request["future"]=expected["future"].DeepClone();var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler))
            {
                var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-recurrence-token"};var api=new RecurringInvoiceApi(config);var info=await Invoke(api,"0007 / account",request,mode,CancellationToken.None);Assert.That(api.Configuration,Is.SameAs(config));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(config.AccessToken,Is.EqualTo("isolated-recurrence-token"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/recurringinvoice/"+"0007%20%2F%20account"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-recurrence-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected.ToString(Formatting.None)));
                Assert.That(info.StatusCode,Is.EqualTo(200));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-Recurrence-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodyCompleteObjectAndFutureDataRemainDistinct([Values(0,1,2)]int mode,[Values("absent","empty","complete","future")]string shape)
        {
            var request=shape=="absent"?null:shape=="empty"?new RecurringInvoiceUpdateRequest(new JObject()):Valid();if(shape=="future")request["future"]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){if(shape=="empty"){Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}else{var result=await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(result.Data,Is.Null);Assert.That(handler.Calls,Is.EqualTo(1));}}
        }
        [Test]
        public async Task OriginalInternalInvoiceSelectorRemainsOneSegment([Values(0,1,2)]int mode,[Values(null,".","..","0007008","","  ","../x/§ ?#%","a%2Fb")]string selector)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(selector==null)Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,Valid(),mode,CancellationToken.None);}));else if(selector=="."||selector=="..")Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),selector,Valid(),mode,CancellationToken.None);}));else{await Invoke(Api(http),selector,Valid(),mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/recurringinvoice/"+Uri.EscapeDataString(selector)+""));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(selector==null||selector=="."||selector==".."?0:1));
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid();request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),"fixture",request,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.Frequency="after";request.NextInvoiceDate=1;request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventInvoices([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),"fixture",Valid(),mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original recurrence diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,"fixture",Valid(),mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original recurrence diagnostic"));var expected=new InvalidOperationException("custom recurrence hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name()));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,"fixture",Valid(),mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }

        [Test]
        public void InvalidSuccessJsonIsNotInvented()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",Valid(),2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancellationHasNoEffects()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",Valid(),2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),"fixture",Valid(),2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        [Test]
        public async Task LegacyModelsEnumsAndDataDiscardingRemainAvailable()
        {
            var original=new RecurringInvoiceForm(0,0,RecurringInvoiceForm.FrequencyEnum.Monthly,0);Assert.That(original.ValidFrom,Is.Zero);Assert.That(original.ValidTo,Is.Zero);Assert.That(original.NextInvoiceDate,Is.Zero);Assert.That(original.Frequency,Is.EqualTo(RecurringInvoiceForm.FrequencyEnum.Monthly));Assert.That(Enum.GetValues(typeof(RecurringInvoiceForm.FrequencyEnum)).Length,Is.EqualTo(7));Assert.That(typeof(RecurringInvoiceApi).GetMethods().Count(m=>m.Name=="UpdateRecurringInvoice"||m.Name=="UpdateRecurringInvoiceWithHttpInfo"||m.Name=="UpdateRecurringInvoiceAsync"||m.Name=="UpdateRecurringInvoiceAsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.UpdateRecurringInvoiceWithHttpInfo("legacy",original).Data,Is.Null);Assert.That((await api.UpdateRecurringInvoiceAsyncWithHttpInfo("legacy",original)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        [Test]
        public void SuppliedObjectRequiresEveryExplicitField([Values("validFrom","validTo","frequency","nextInvoiceDate")]string field,[Values(0,1,2)]int mode)
        {
            var request=Valid();request.Remove(field);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void Int64EpochWidthsAreValidated([Values("validFrom","validTo","nextInvoiceDate")]string field,[Values("9223372036854775808","-9223372036854775809","1.25","\"0\"","true")]string value,[Values(0,1,2)]int mode)
        {
            var request=Valid();request[field]=JToken.Parse(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task SignedEpochsRemainOriginalWithoutDayTruncationOrOrdering([Values("validFrom","validTo","nextInvoiceDate")]string field,[Values(long.MinValue,0L,long.MaxValue)]long value,[Values(0,1,2)]int mode)
        {
            var request=Valid();request[field]=value;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalFrequencyIsNotTrimmedOrDefaulted([Values("daily","weekly","monthly","bimonthly","quarterly","half-yearly","yearly",""," monthly ","future-frequency")]string frequency,[Values(0,1,2)]int mode)
        {
            var request=Valid();request.Frequency=frequency;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),"fixture",request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body).Value<string>("frequency"),Is.EqualTo(frequency));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static RecurringInvoiceApi Api(HttpClient http)=>new RecurringInvoiceApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-recurrence-token"});
        private static Task<ApiResponse<JToken>> Invoke(RecurringInvoiceApi api,string selector,RecurringInvoiceUpdateRequest request,int mode,CancellationToken token)
            =>mode==0?Task.FromResult(api.UpdateRecurringInvoiceWithResultWithHttpInfo(selector,request)):mode==1?api.UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(selector,request):api.UpdateRecurringInvoiceWithResultAsyncWithHttpInfo(selector,request,token);
        private static Task<JToken> InvokeData(RecurringInvoiceApi api,string selector,RecurringInvoiceUpdateRequest request,int mode)
            =>mode==0?Task.FromResult(api.UpdateRecurringInvoiceWithResult(selector,request)):mode==1?api.UpdateRecurringInvoiceWithResultAsync(selector,request):api.UpdateRecurringInvoiceWithResultAsync(selector,request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Recurrence-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
