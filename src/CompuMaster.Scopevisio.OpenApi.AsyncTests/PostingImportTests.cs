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
    public class PostingImportTests
    {
        [Test]
        public void CompleteImportFormsAreAvailable()=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith("ImportPostingsWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("PostingImportRequest")]
        [TestCase("PostingImportRowRequest")]
        public void CompleteImportRequestsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+name),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["PostingRow"]["properties"];
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static PostingImportRowRequest Row()=>new PostingImportRowRequest{PostingDate=0,DocumentNumber="original",Account="0008405",Amount=0};
        private static PostingImportRequest Request(JObject row)=>new PostingImportRequest{Rows=new JArray(row)};
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties())foreach(var mode in new[]{0,1,2})foreach(var nil in new[]{false,true})yield return new TestCaseData(field.Name,mode,nil);}
        [TestCaseSource(nameof(Fields))]
        public void KnownRowKindsAndNullsFollowCurrentContract(string field,int mode,bool nil)
        {
            var row=Row();row[field]=nil?JValue.CreateNull():Schema()[field].Value<string>("type")=="string"?(JToken)new JValue(false):new JValue("invalid");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))
            {
                if(nil&&!new[]{"postingDate","documentNumber","account","amount"}.Contains(field)){Invoke(Api(http),Request(row),mode,CancellationToken.None).GetAwaiter().GetResult();Assert.That(JObject.Parse(handler.Body)["rows"][0][field].Type,Is.EqualTo(JTokenType.Null));Assert.That(handler.Calls,Is.EqualTo(1));}
                else{Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}
            }
        }
        [Test]
        public void RequiredFieldsAreExplicitOriginalValues([Values("postingDate","documentNumber","account","amount")]string field,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Remove(field);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void InvalidRowsFailBeforeHttp([Values("{}","true","[null]","[false]","[\"text\"]","[[]]")]string rows,[Values(0,1,2)]int mode)
        {
            var request=new PostingImportRequest{["rows"]=JToken.Parse(rows)};var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void InvalidDimensionCollectionsFailBeforeHttp([Values("{}","false","[null]","[false]","[[]]","[\"text\"]")]string dimensions,[Values(0,1,2)]int mode)
        {
            var row=Row();row["dimensions"]=JToken.Parse(dimensions);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void DimensionKnownKindsAreValidated([Values("dimensionId","dimensionAccountNumber")]string field,[Values("false","0","[]","{}")]string value,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Dimensions=new JArray(new JObject{[field]=JToken.Parse(value)});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void ConstructorsAndTypedArraysDeeplyCopyAndNullsOmit()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var row=new PostingImportRowRequest(original);var request=new PostingImportRequest(original);original["future"]["value"]=2;Assert.That(row["future"].Value<int>("value"),Is.EqualTo(1));Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new PostingImportRowRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new PostingImportRequest(null)));
            foreach(var field in Schema().Properties()){row[field.Name]=JValue.CreateNull();typeof(PostingImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Single(p=>p.Name==Property(field.Name)).SetValue(row,null);Assert.That(row.ContainsKey(field.Name),Is.False);}Assert.That(row.Count,Is.EqualTo(1));
            var dimensions=new JArray(new JObject{["dimensionId"]="dimension_3",["dimensionAccountNumber"]="0009",["future"]=original.DeepClone()});row.Dimensions=dimensions;dimensions[0]["dimensionId"]="changed";var getter=row.Dimensions;getter[0]["dimensionAccountNumber"]="changed";Assert.That(row.Dimensions[0].Value<string>("dimensionId"),Is.EqualTo("dimension_3"));Assert.That(row.Dimensions[0].Value<string>("dimensionAccountNumber"),Is.EqualTo("0009"));
            var rows=new JArray(row);request.Rows=rows;rows[0]["future"]["value"]=3;var read=request.Rows;read[0]["future"]["value"]=4;Assert.That(request.Rows[0]["future"].Value<int>("value"),Is.EqualTo(1));request.Rows=null;Assert.That(request.ContainsKey("rows"),Is.False);
        }
        [Test]
        public void PrimaryPostingContractAndResponseRemainExact()
        {
            var c=Contract();var p=c["paths"]["/postings/new"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["requestBody"]["content"]["application/json"]["schema"].Value<string>("$ref"),Does.EndWith("/Postings"));Assert.That(p["responses"]["200"]["content"]["application/json"]["schema"].Value<string>("$ref"),Does.EndWith("/PostingsNewResponse"));Assert.That(p.Value<string>("description"),Does.Contain("Datenimport (Bearbeiten)").And.Contain("SKR03").And.Contain("two or more"));Assert.That(c["components"]["schemas"]["Postings"]["required"],Is.Null);Assert.That(c["components"]["schemas"]["PostingRow"]["required"].Values<string>(),Is.EquivalentTo(new[]{"postingDate","documentNumber","account","amount"}));var response=c["components"]["schemas"]["PostingsNewResponse"];Assert.That(response["required"],Is.Null);Assert.That(response["properties"]["status"].Value<string>("format"),Is.EqualTo("int32"));Assert.That(response["properties"]["insertCount"].Value<string>("type"),Is.EqualTo("string"));Assert.That(response["properties"]["updateCount"].Value<string>("type"),Is.EqualTo("string"));Assert.That(c["components"]["schemas"]["DimensionForm"]["required"],Is.Null);
        }
        [Test]
        public async Task CompleteTypedWirePreservesConfiguredChartAndFullResult([Values(0,1,2)]int mode)
        {
            var row=Row();Assert.That(Schema().Count,Is.EqualTo(24));Assert.That(typeof(PostingImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Length,Is.EqualTo(24));foreach(var field in Schema().Properties())
            {
                var kind=field.Value.Value<string>("type");object value=kind=="integer"?(object)long.MaxValue:kind=="number"?(object)(-123.456789m):kind=="array"?(object)new JArray(new JObject{["dimensionId"]=" dimension_custom ",["dimensionAccountNumber"]="0009",["future"]=new JArray(false,0,JValue.CreateNull())}):"  original / "+field.Name;typeof(PostingImportRowRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Single(p=>p.Name==Property(field.Name)).SetValue(row,value);
            }
            row["future"]=new JObject{["value"]=null};var request=Request(row);request["futureImport"]=false;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler))
            {
                var result=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/postings/new"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-posting-import-token"));Assert.That(handler.Accept,Does.Contain("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.EqualTo(Result));Assert.That(result.Headers["X-Posting-Fixture"],Does.Contain("retained"));Assert.That(result.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(result.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(result.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(result.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));
            }
            Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OptionalBodyEmptyRowsAndRawFutureRemainDistinct([Values(0,1,2)]int mode,[Values(0,1,2,3,4,5)]int shape)
        {
            var request=shape==0?null:new PostingImportRequest();if(shape==2)request.Rows=new JArray();if(shape==3)request.Rows=new JArray(Row());if(shape==4)request["future"]=JValue.CreateNull();if(shape==5)request["rows"]=JValue.CreateNull();var expected=request?.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){var response=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(response.Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalBlankStringsAndConfiguredEnumsRemainServerDecisions([Values("documentNumber","externalDocumentNumber","account","summaryAccount","cancelDocument","foreignCurrencyCode","documentText","rowText","vatKey","discountAccount","paymentType","internalDocumentNumber","ustId")]string field,[Values(""," ","00/Configured-税")]string value,[Values(0,1,2)]int mode)
        {
            var row=Row();row[field]=value;var request=Request(row);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body)["rows"][0].Value<string>(field),Is.EqualTo(value));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OmittedFalseTrueFlagsAreIndependent([Values("adjustVatKey","autoCreateTax")]string flag,[Values(-1,0,1)]int choice,[Values(0,1,2)]int mode)
        {
            var request=Request(Row());if(choice>=0)request[flag]=choice==1;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,mode,CancellationToken.None);var sent=JObject.Parse(handler.Body);if(choice<0)Assert.That(sent.ContainsKey(flag),Is.False);else Assert.That(sent.Value<bool>(flag),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task DimensionSelectionsMayBeSparseAndFuture([Values(0,1,2)]int mode,[Values("[]","[{}]","[{\"dimensionId\":\"dimension_custom\"}]","[{\"dimensionAccountNumber\":\"0009\",\"future\":null}]")]string json)
        {
            var row=Row();row.Dimensions=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Request(row),mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body)["rows"][0]["dimensions"].ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task MultipleRowsRetainOrderAndIndependentSelections([Values(0,1,2)]int mode)
        {
            var first=Row();first.Amount=17.5m;first.Dimensions=new JArray(new JObject{["dimensionId"]="dimension_2",["dimensionAccountNumber"]="0009"});var second=Row();second.Account="0000001";second.Amount=-17.5m;second.Dimensions=new JArray();var request=new PostingImportRequest{Rows=new JArray(first,second)};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"[]")));using(var http=new HttpClient(handler)){var result=await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(result.Data,Is.InstanceOf<JArray>());}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task SnapshotPrecedesActiveHttp()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Request(Row());request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(Api(http),request,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request["rows"][0]["account"]="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }
        [Test]
        public async Task AllDataFormsPreserveResultsWithoutInventedRowSuccess([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"errors\":[\"row\"],\"updated\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),new PostingImportRequest(),mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original import diagnostic")));using(var http=new HttpClient(handler))
            {
                var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,null,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original import diagnostic"));var expected=new InvalidOperationException("custom import hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo("ImportPostingsWithResult"));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,null,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));
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
        public async Task LegacyDefaultsNamesAndDataDiscardingRemainUnchanged()
        {
            var row=new PostingRow(postingDate:0,documentNumber:"legacy",account:"0008405",amount:0);Assert.That(row.ForeignCurrencyAmount,Is.Zero);Assert.That(row.NettimeLimit,Is.Zero);Assert.That(typeof(PostingRow).GetProperty("UstId"),Is.Null);Assert.That(typeof(PostingRow).GetProperty("NetTimeLimit"),Is.Null);Assert.That(typeof(PostingRow).GetProperty("DiscountAccount"),Is.Null);Assert.That(typeof(PostingRow).GetProperty("DisountAccount"),Is.Not.Null);var form=new Postings();Assert.That(form.AdjustVatKey,Is.False);Assert.That(form.AutoCreateTax,Is.False);Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name=="CreatePostings"||m.Name=="CreatePostingsWithHttpInfo"||m.Name=="CreatePostingsAsync"||m.Name=="CreatePostingsAsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.CreatePostingsWithHttpInfo(form).Data,Is.Null);Assert.That((await api.CreatePostingsAsyncWithHttpInfo(form)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        [Test]
        public void Int64WidthsAreValidated([Values("postingDate","documentDate","discountPeriod1","discountPeriod2","netTimeLimit")]string field,[Values("9223372036854775808","-9223372036854775809","1.25","\"0\"")]string value,[Values(0,1,2)]int mode)
        {
            var row=Row();row[field]=JToken.Parse(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void NonfiniteNumbersFailBeforeHttp([Values("amount","foreignCurrencyAmount","foreignCurrencyRate","discountPercent1","discountPercent2")]string field,[Values(double.NaN,double.PositiveInfinity,double.NegativeInfinity)]double value,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Remove(field);row[field]=new JValue(value);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Request(row),mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task NumericSelectionsRemainOriginalWithoutBalanceOrDateConversion([Values(0,1,2)]int mode)
        {
            var row=Row();row.PostingDate=long.MinValue;row.DocumentDate=long.MaxValue;row.NetTimeLimit=long.MinValue;row.ForeignCurrencyRate=-123.456789m;row.Amount=-0.0000000000000000000000000001m;row.DiscountPercent1=1000m;row.DiscountAccount="0003736";row.UstId=" original VAT ";var request=Request(row);var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void WrapperKindsAreValidatedAndOptionalNullsRemainAvailable([Values("adjustVatKey","autoCreateTax")]string flag,[Values(false,true)]bool nil,[Values(0,1,2)]int mode)
        {
            var request=Request(Row());request[flag]=nil?JValue.CreateNull():new JValue("false");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){if(nil){Invoke(Api(http),request,mode,CancellationToken.None).GetAwaiter().GetResult();Assert.That(JObject.Parse(handler.Body)[flag].Type,Is.EqualTo(JTokenType.Null));Assert.That(handler.Calls,Is.EqualTo(1));}else{Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}}
        }
        [Test]
        public async Task KnownDimensionNullsRemainServerDecisions([Values("dimensionId","dimensionAccountNumber")]string field,[Values(0,1,2)]int mode)
        {
            var row=Row();row.Dimensions=new JArray(new JObject{[field]=JValue.CreateNull()});var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Request(row),mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body)["rows"][0]["dimensions"][0][field].Type,Is.EqualTo(JTokenType.Null));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task DeclaredResponseCountsRemainStringsWithoutAggregateSuccess([Values(0,1,2)]int mode)
        {
            const string json="{\"status\":201,\"insertCount\":\"0001\",\"updateCount\":\"0\",\"errors\":[\"row\"],\"future\":false}";var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var result=await Invoke(Api(http),Request(Row()),mode,CancellationToken.None);Assert.That(result.RawJsonContent,Is.EqualTo(json));Assert.That(result.Data["status"].Value<int>(),Is.EqualTo(201));Assert.That(result.Data["insertCount"].Type,Is.EqualTo(JTokenType.String));Assert.That(result.Data.Value<string>("insertCount"),Is.EqualTo("0001"));Assert.That(result.Data.Value<string>("updateCount"),Is.EqualTo("0"));Assert.That(result.Data["errors"][0].Value<string>(),Is.EqualTo("row"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-posting-import-token"});
        private static Task<ApiResponse<JToken>> Invoke(AccountingApi api,PostingImportRequest request,int mode,CancellationToken token)
            =>mode==0?Task.FromResult(api.ImportPostingsWithResultWithHttpInfo(request)):mode==1?api.ImportPostingsWithResultAsyncWithHttpInfo(request):api.ImportPostingsWithResultAsyncWithHttpInfo(request,token);
        private static Task<JToken> InvokeData(AccountingApi api,PostingImportRequest request,int mode)
            =>mode==0?Task.FromResult(api.ImportPostingsWithResult(request)):mode==1?api.ImportPostingsWithResultAsync(request):api.ImportPostingsWithResultAsync(request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Posting-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
