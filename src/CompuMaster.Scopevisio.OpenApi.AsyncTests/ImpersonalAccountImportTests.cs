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

using System.Reflection;
namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ImpersonalAccountImportTests
    {
        [Test]
        public void CompleteImportFormsAreAvailable()=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith("ImportImpersonalAccountsWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("ImpersonalAccountImportRequest")]
        [TestCase("ImpersonalAccountImportRowRequest")]
        public void CompleteImportRequestsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+name),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["ImpersonalAccountRow"]["properties"];
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static ImpersonalAccountImportRowRequest Row()=>new ImpersonalAccountImportRowRequest{Number="0008405",Name=" original ",Type="Erlöse"};
        private static ImpersonalAccountImportRequest Request(JObject row)=>new ImpersonalAccountImportRequest{Rows=new JArray(row)};
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties())foreach(var mode in new[]{0,1,2})foreach(var nil in new[]{false,true})yield return new TestCaseData(field.Name,mode,nil);}
        [TestCaseSource(nameof(Fields))]
        public void KnownRowKindsAndNullsFailBeforeHttp(string field,int mode,bool nil)
        {
            var row=Row();row[field]=nil?JValue.CreateNull():Schema()[field].Value<string>("type")=="string"?(JToken)new JValue(false):new JValue("invalid");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void RequiredFieldsAreExplicitOriginalStrings([Values("number","name","type")]string field,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Remove(field);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void InvalidRowsFailBeforeHttp([Values("null","{}","true","[null]","[false]","[\"text\"]","[[]]")]string rows,[Values(0,1,2)]int mode)
        {
            var request=new ImpersonalAccountImportRequest{["rows"]=JToken.Parse(rows)};var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void InvalidDimensionCollectionsFailBeforeHttp([Values("null","{}","false","[null]","[false]","[[]]","[\"text\"]")]string dimensions,[Values(0,1,2)]int mode)
        {
            var row=Row();row["dimensions"]=JToken.Parse(dimensions);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void DimensionKnownKindsAreValidated([Values("dimensionId","dimensionAccountNumber")]string field,[Values("null","false","0","[]","{}")]string value,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Dimensions=new JArray(new JObject{[field]=JToken.Parse(value)});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void ConstructorsAndTypedArraysDeeplyCopyAndNullsOmit()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var row=new ImpersonalAccountImportRowRequest(original);var request=new ImpersonalAccountImportRequest(original);original["future"]["value"]=2;Assert.That(row["future"].Value<int>("value"),Is.EqualTo(1));Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new ImpersonalAccountImportRowRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new ImpersonalAccountImportRequest(null)));
            foreach(var field in Schema().Properties()){row[field.Name]=JValue.CreateNull();typeof(ImpersonalAccountImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Single(p=>p.Name==Property(field.Name)).SetValue(row,null);Assert.That(row.ContainsKey(field.Name),Is.False);}Assert.That(row.Count,Is.EqualTo(1));
            var dimensions=new JArray(new JObject{["dimensionId"]="dimension_3",["dimensionAccountNumber"]="0009",["future"]=original.DeepClone()});row.Dimensions=dimensions;dimensions[0]["dimensionId"]="changed";var getter=row.Dimensions;getter[0]["dimensionAccountNumber"]="changed";Assert.That(row.Dimensions[0].Value<string>("dimensionId"),Is.EqualTo("dimension_3"));Assert.That(row.Dimensions[0].Value<string>("dimensionAccountNumber"),Is.EqualTo("0009"));
            var rows=new JArray(row);request.Rows=rows;rows[0]["future"]["value"]=3;var read=request.Rows;read[0]["future"]["value"]=4;Assert.That(request.Rows[0]["future"].Value<int>("value"),Is.EqualTo(1));request.Rows=null;Assert.That(request.ContainsKey("rows"),Is.False);
        }
        [Test]
        public void PrimaryImportContractHasOptionalBodyRowsAndNoFixedResult()
        {
            var c=Contract();var p=c["paths"]["/createimpersonalaccounts"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Does.EndWith("/ImpersonalAccountsForm"));Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p["responses"]["200"].Value<string>("description"),Does.Contain("created or updated"));Assert.That(p.Value<string>("description"),Does.Contain("Datenimport (Bearbeiten)").And.Contain("SKR03"));Assert.That(c["components"]["schemas"]["ImpersonalAccountsForm"]["required"],Is.Null);Assert.That(c["components"]["schemas"]["ImpersonalAccountRow"]["required"].Values<string>(),Is.EquivalentTo(new[]{"number","name","type"}));Assert.That(c["components"]["schemas"]["DimensionForm"]["required"],Is.Null);
        }
        [Test]
        public async Task CompleteTypedWirePreservesConfiguredChartAndFullResult([Values(0,1,2)]int mode)
        {
            var row=Row();Assert.That(Schema().Count,Is.EqualTo(13));Assert.That(typeof(ImpersonalAccountImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Length,Is.EqualTo(13));foreach(var field in Schema().Properties())
            {
                var kind=field.Value.Value<string>("type");object value=kind=="boolean"?(object)false:kind=="array"?(object)new JArray(new JObject{["dimensionId"]=" dimension_custom ",["dimensionAccountNumber"]="0009",["future"]=new JArray(false,0,JValue.CreateNull())}):"  original / "+field.Name;typeof(ImpersonalAccountImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Single(p=>p.Name==Property(field.Name)).SetValue(row,value);
            }
            row["future"]=new JObject{["value"]=null};var request=Request(row);request["futureImport"]=false;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler))
            {
                var result=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/createimpersonalaccounts"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-bank-write-token"));Assert.That(handler.Accept,Does.Contain("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.EqualTo(Result));Assert.That(result.Headers["X-Bank-Fixture"],Does.Contain("retained"));Assert.That(result.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(result.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(result.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(result.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));
            }
            Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OptionalBodyEmptyRowsAndRawFutureRemainDistinct([Values(0,1,2)]int mode,[Values(0,1,2,3,4)]int shape)
        {
            var request=shape==0?null:new ImpersonalAccountImportRequest();if(shape==2)request.Rows=new JArray();if(shape==3)request.Rows=new JArray(Row());if(shape==4)request["future"]=JValue.CreateNull();var expected=request?.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){var response=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(response.Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalBlankStringsAndConfiguredEnumsRemainServerDecisions([Values("number","name","type","category","vatCode","vatKey","discountAccount")]string field,[Values(""," ","00/Configured-税")]string value,[Values(0,1,2)]int mode)
        {
            var row=Row();row[field]=value;var request=Request(row);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body)["rows"][0].Value<string>(field),Is.EqualTo(value));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OmittedFalseTrueFlagsAreIndependent([Values("fixVatKey","deactivated","directPosting","isFinanceAccount","subjectToClearing")]string flag,[Values(-1,0,1)]int choice,[Values(0,1,2)]int mode)
        {
            var row=Row();if(choice>=0)row[flag]=choice==1;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Request(row),mode,CancellationToken.None);var sent=(JObject)JObject.Parse(handler.Body)["rows"][0];if(choice<0)Assert.That(sent.ContainsKey(flag),Is.False);else Assert.That(sent.Value<bool>(flag),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task DimensionSelectionsMayBeSparseAndFuture([Values(0,1,2)]int mode,[Values("[]","[{}]","[{\"dimensionId\":\"dimension_custom\"}]","[{\"dimensionAccountNumber\":\"0009\",\"future\":null}]")]string json)
        {
            var row=Row();row.Dimensions=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Request(row),mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body)["rows"][0]["dimensions"].ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task MultipleRowsRetainOrderAndIndependentSelections([Values(0,1,2)]int mode)
        {
            var first=Row();first.DirectPosting=false;first.Dimensions=new JArray(new JObject{["dimensionId"]="dimension_2",["dimensionAccountNumber"]="0009"});var second=Row();second.Number="0000001";second.Type="Kosten";second.DirectPosting=true;second.Dimensions=new JArray();var request=new ImpersonalAccountImportRequest{Rows=new JArray(first,second)};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"[]")));using(var http=new HttpClient(handler)){var result=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(result.Data,Is.InstanceOf<JArray>());}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task SnapshotPrecedesActiveHttp()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Request(Row());request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),request,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request["rows"][0]["number"]="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }
        [Test]
        public async Task AllDataFormsPreserveResultsWithoutInventedRowSuccess([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"errors\":[\"row\"],\"updated\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),new ImpersonalAccountImportRequest(),mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original import diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,null,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original import diagnostic"));var expected=new InvalidOperationException("custom import hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo("ImportImpersonalAccountsWithResult"));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,null,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
            }
        }
        [Test]
        public void InvalidSuccessJsonIsNotInvented()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),null,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void PrecancellationHasNoEffects()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),null,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }
        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(Api(http),Request(Row()),2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }
        [Test]
        public async Task LegacySpellingRequiredFieldsAndDefaultsRemainUnchanged()
        {
            var row=new ImpersonalAccountRow("0008405","legacy",ImpersonalAccountRow.TypeEnum.Erlse);Assert.That(row.FixVatKey,Is.False);Assert.That(row.Deactivated,Is.False);Assert.That(row.DirectPosting,Is.False);Assert.That(row.IsFinanceAccount,Is.False);Assert.That(row.SubjectToClearing,Is.False);Assert.Throws<InvalidDataException>((TestDelegate)(()=>new ImpersonalAccountRow()));Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name=="CreateImpersonalAcocunts"||m.Name=="CreateImpersonalAcocuntsWithHttpInfo"||m.Name=="CreateImpersonalAcocuntsAsync"||m.Name=="CreateImpersonalAcocuntsAsyncWithHttpInfo"),Is.EqualTo(4));var form=new ImpersonalAccountsForm(new List<ImpersonalAccountRow>{row});var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.CreateImpersonalAcocuntsWithHttpInfo(form).Data,Is.Null);Assert.That((await api.CreateImpersonalAcocuntsAsyncWithHttpInfo(form)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-bank-write-token"});
        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api,ImpersonalAccountImportRequest request,int mode,CancellationToken token)
            =>mode==0?Task.FromResult(api.ImportImpersonalAccountsWithResultWithHttpInfo(request)):mode==1?api.ImportImpersonalAccountsWithResultAsyncWithHttpInfo(request):api.ImportImpersonalAccountsWithResultAsyncWithHttpInfo(request,token);
        private static Task<JToken> InvokeData(AccountingApi api,ImpersonalAccountImportRequest request,int mode)
            =>mode==0?Task.FromResult(api.ImportImpersonalAccountsWithResult(request)):mode==1?api.ImportImpersonalAccountsWithResultAsync(request):api.ImportImpersonalAccountsWithResultAsync(request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Bank-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
