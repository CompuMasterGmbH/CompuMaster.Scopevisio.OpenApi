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
    public class DatevTransferTests
    {
        [TestCase("ExportDatevWithResult")]
        [TestCase("ImportDatevDataWithResult")]
        public void CompleteTransferFormsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("DatevExportRequest")]
        [TestCase("DatevImportRequest")]
        public void CompleteTransferRequestsAreAvailable(string name)=>Assert.That(typeof(AccountingApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+name),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema(bool importing)=>(JObject)Contract()["components"]["schemas"][importing?"DatevPostings":"DatevExportSettingsForm"]["properties"];
        private static JObject Request(bool importing)=>importing?(JObject)new DatevImportRequest{Data="original"}:new DatevExportRequest{FromCreationDateTs=0,ToCreationDateTs=0};
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static string Name(bool importing)=>importing?"ImportDatevDataWithResult":"ExportDatevWithResult";
        public static IEnumerable<TestCaseData> Fields(){foreach(var importing in new[]{false,true})foreach(var field in Schema(importing).Properties())foreach(var mode in new[]{0,1,2})foreach(var nil in new[]{false,true})yield return new TestCaseData(importing,field.Name,mode,nil);}
        [TestCaseSource(nameof(Fields))]
        public void KnownFieldsRequireOriginalKinds(bool importing,string field,int mode,bool nil)
        {
            var request=Request(importing);request[field]=nil?JValue.CreateNull():Schema(importing)[field].Value<string>("type")=="string"?(JToken)new JValue(false):new JValue("invalid");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,importing,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void ImportSuppliedObjectRequiresOriginalData([Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),new DatevImportRequest(),true,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void CreationDateFilterRequiresBothBounds([Values("fromCreationDateTs","toCreationDateTs")]string field,[Values(0,1,2)]int mode)
        {
            var request=new DatevExportRequest{[field]=0};var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,false,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void OriginalEpochWidthsAreValidated([Values("fromPostingDateTs","toPostingDateTs","fromCreationDateTs","toCreationDateTs")]string field,[Values("9223372036854775808","-9223372036854775809","1.25","\"0\"","true")]string json,[Values(0,1,2)]int mode)
        {
            var request=Request(false);request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),request,false,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task SignedEpochBoundsRemainOriginalWithoutOrderingOrDefaults([Values("fromPostingDateTs","toPostingDateTs","fromCreationDateTs","toCreationDateTs")]string field,[Values(long.MinValue,0L,long.MaxValue)]long value,[Values(0,1,2)]int mode)
        {
            var request=Request(false);request[field]=value;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"original export")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,false,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void ConstructorsCopyFutureDataAndTypedNullsOmit([Values(false,true)]bool importing)
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};JObject request=importing?(JObject)new DatevImportRequest(original):new DatevExportRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));foreach(var field in Schema(importing).Properties()){request[field.Name]=JValue.CreateNull();request.GetType().GetProperty(Property(field.Name)).SetValue(request,null);Assert.That(request.ContainsKey(field.Name),Is.False);}Assert.That(request.Count,Is.EqualTo(1));if(importing)Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new DatevImportRequest(null)));else Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new DatevExportRequest(null)));
        }
        [Test]
        public void ImportByteFactoryCopiesAllBytesWithoutFileOrTextConversion()
        {
            var bytes=Enumerable.Range(0,256).Select(x=>(byte)x).ToArray();var expected=(byte[])bytes.Clone();var request=DatevImportRequest.FromBytes(bytes);bytes[0]=255;Assert.That(Convert.FromBase64String(request.Data),Is.EqualTo(expected));Assert.That(request.Count,Is.EqualTo(1));Assert.That(DatevImportRequest.FromBytes(new byte[0]).Data,Is.Empty);Assert.Throws<ArgumentNullException>((TestDelegate)(()=>DatevImportRequest.FromBytes(null)));
        }
        [Test]
        public void PrimaryContractDeclaresOptionalBodiesAndUnspecifiedResults()
        {
            var c=Contract();foreach(var importing in new[]{false,true}){var p=c["paths"][importing?"/datevpostings/new":"/datevexport"]["post"];Assert.That(p["requestBody"]["required"],Is.Null);Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p.Value<string>("description"),Does.Contain(importing?"Datenimport (Bearbeiten)":"Datenexport (Anzeigen)"));}Assert.That(c["components"]["schemas"]["DatevExportSettingsForm"]["required"],Is.Null);Assert.That(c["components"]["schemas"]["DatevPostings"]["required"].Values<string>(),Is.EquivalentTo(new[]{"data"}));Assert.That(Schema(false)["vatMatrixDatevKeys"].Value<bool>("default"),Is.True);Assert.That(Schema(false)["preferRowTexts"].Value<bool>("default"),Is.False);Assert.That(Schema(false)["fromCreationDateTs"].Value<string>("description"),Does.Contain("Both"));Assert.That(Schema(true)["zone"].Value<string>("description"),Does.Contain("standard zone"));
        }
        [Test]
        public async Task AllTypedWireFieldsAndFutureJsonRemainOriginal([Values(false,true)]bool importing,[Values(0,1,2)]int mode)
        {
            var request=Request(importing);Assert.That(Schema(importing).Count,Is.EqualTo(importing?2:13));Assert.That(request.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Length,Is.EqualTo(importing?2:13));foreach(var field in Schema(importing).Properties()){object value=field.Value.Value<string>("type")=="boolean"?(object)false:field.Value.Value<string>("type")=="integer"?(object)long.MaxValue:"  original / "+field.Name;request.GetType().GetProperty(Property(field.Name)).SetValue(request,value);}request["future"]=new JObject{["value"]=new JArray(false,0,JValue.CreateNull())};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,importing,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo(importing?"/rest/datevpostings/new":"/rest/datevexport"));Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-datev-token"));Assert.That(handler.Accept,Does.Contain(importing?"application/json":"*/*"));Assert.That(handler.ContentType,Does.StartWith("application/json"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalStringsArePreservedWithoutMappingZoneOrFormatRepair([Values(false,true)]bool importing,[Values(""," ","00/Configured-税")]string value,[Values(0,1,2)]int mode)
        {
            var request=Request(importing);foreach(var field in Schema(importing).Properties().Where(p=>p.Value.Value<string>("type")=="string"))request[field.Name]=value;var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,importing,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ExportOmittedFalseTrueFlagsRemainDistinct([Values("skipCancellations","vatMatrixDatevKeys","preferRowTexts")]string field,[Values(-1,0,1)]int choice,[Values(0,1,2)]int mode)
        {
            var request=new DatevExportRequest();if(choice>=0)request[field]=choice==1;var handler=new Handler(token=>Task.FromResult(Response(200,"original")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,false,mode,CancellationToken.None);var sent=JObject.Parse(handler.Body);if(choice<0)Assert.That(sent.ContainsKey(field),Is.False);else Assert.That(sent.Value<bool>(field),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OptionalBodiesRemainDistinct([Values(false,true)]bool importing,[Values(0,1,2)]int mode,[Values(false,true)]bool omitted)
        {
            JObject request=omitted?null:importing?(JObject)new DatevImportRequest{Data=""}:new DatevExportRequest();var expected=request?.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){await Invoke(Api(http),request,importing,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ExportRetainsAllBufferedBytesAndHeadersWithoutParsing([Values(0,1,2)]int mode,[Values("application/octet-stream","text/csv","application/json","application/x-custom")]string media,[Values(false,true)]bool empty)
        {
            var bytes=empty?new byte[0]:Enumerable.Range(0,256).Select(x=>(byte)x).ToArray();var handler=new Handler(token=>{var response=new HttpResponseMessage(HttpStatusCode.OK){Content=new ByteArrayContent(bytes)};response.Content.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue(media);response.Content.Headers.Add("Content-Disposition","attachment; filename=original.extf");response.Headers.Add("X-Datev-Fixture","retained");return Task.FromResult(response);});using(var http=new HttpClient(handler)){var result=await Export(Api(http),null,mode,CancellationToken.None);Assert.That(result.Data,Is.EqualTo(bytes));Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.Null);Assert.That(result.Headers["Content-Type"],Is.EqualTo(media));Assert.That(result.Headers["Content-Disposition"],Does.Contain("original.extf"));Assert.That(result.Headers["X-Datev-Fixture"],Does.Contain("retained"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ExportDataFormsPreserveOriginalBytes([Values(0,1,2)]int mode)
        {
            var bytes=new byte[]{0,255,13,10,128};var handler=new Handler(token=>Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK){Content=new ByteArrayContent(bytes)}));using(var http=new HttpClient(handler)){var api=Api(http);var data=mode==0?api.ExportDatevWithResult():mode==1?await api.ExportDatevWithResultAsync():await api.ExportDatevWithResultAsync(null,CancellationToken.None);Assert.That(data,Is.EqualTo(bytes));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ImportInfoPreservesRawJsonStatusHeadersAndFutureData([Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler)){var result=await Import(Api(http),DatevImportRequest.FromBytes(new byte[]{0,255}),mode,CancellationToken.None);Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.EqualTo(Result));Assert.That(result.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(result.Headers["X-Datev-Fixture"],Does.Contain("retained"));Assert.That(result.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(result.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(result.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(JObject.Parse(handler.Body).Value<string>("data"),Is.EqualTo("AP8="));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ImportDataFormsPreserveResultsWithoutInventingPostingSuccess([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"errors\":[\"row\"],\"posted\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var api=Api(http);var data=mode==0?api.ImportDatevDataWithResult():mode==1?await api.ImportDatevDataWithResultAsync():await api.ImportDatevDataWithResultAsync(null,CancellationToken.None);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ImportEmptyResponseIsNull([Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler))Assert.That((await Import(Api(http),null,mode,CancellationToken.None)).Data,Is.Null);Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task SnapshotPrecedesActiveHttp([Values(false,true)]bool importing)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Request(importing);request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler)){var operation=Invoke(Api(http),request,importing,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request[importing?"data":"mapping"]="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;}
        }
        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(false,true)]bool importing,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original DATEV diagnostic")));using(var http=new HttpClient(handler)){var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,null,importing,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original DATEV diagnostic"));var expected=new InvalidOperationException("custom DATEV hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(importing)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,null,importing,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));}
        }
        [Test]
        public void ImportInvalidSuccessJsonIsNotInvented()
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Import(Api(http),null,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void PrecancellationHasNoEffects([Values(false,true)]bool importing)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),null,importing,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }
        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(false,true)]bool importing)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){var operation=Invoke(Api(http),Request(importing),importing,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}}
        }
        [Test]
        public async Task LegacyModelsDefaultsAndDataDiscardingRemainUnchanged()
        {
            var export=new DatevExportSettingsForm();Assert.That(export.VatMatrixDatevKeys,Is.False);Assert.That(export.SkipCancellations,Is.False);Assert.That(export.FromPostingDateTs,Is.Zero);Assert.That(export.FromCreationDateTs,Is.Zero);Assert.That(typeof(DatevExportSettingsForm).GetProperty("Mapping"),Is.Null);Assert.That(typeof(DatevExportSettingsForm).GetProperty("PreferRowTexts"),Is.Null);foreach(var name in new[]{"ExportDatev","ImportDatevData"})Assert.That(typeof(AccountingApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.ExportDatevWithHttpInfo(export).Data,Is.Null);Assert.That((await api.ImportDatevDataAsyncWithHttpInfo(new DatevPostings(data: "original"))).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private static AccountingApi Api(HttpClient http)=>new AccountingApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-datev-token"});
        private static async Task Invoke(AccountingApi api,JObject request,bool importing,int mode,CancellationToken token){if(importing)await Import(api,(DatevImportRequest)request,mode,token);else await Export(api,(DatevExportRequest)request,mode,token);}
        private static Task<ApiResponse<JToken>> Import(AccountingApi api,DatevImportRequest request,int mode,CancellationToken token)=>mode==0?Task.FromResult(api.ImportDatevDataWithResultWithHttpInfo(request)):mode==1?api.ImportDatevDataWithResultAsyncWithHttpInfo(request):api.ImportDatevDataWithResultAsyncWithHttpInfo(request,token);
        private static Task<ApiResponse<byte[]>> Export(AccountingApi api,DatevExportRequest request,int mode,CancellationToken token)=>mode==0?Task.FromResult(api.ExportDatevWithResultWithHttpInfo(request)):mode==1?api.ExportDatevWithResultAsyncWithHttpInfo(request):api.ExportDatevWithResultAsyncWithHttpInfo(request,token);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Datev-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
