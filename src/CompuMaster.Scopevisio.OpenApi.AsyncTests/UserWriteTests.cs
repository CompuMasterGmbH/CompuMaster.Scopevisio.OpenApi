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
    public class UserWriteTests
    {
        [TestCase("CreateUsersWithResult")]
        [TestCase("UpdateUsersWithResult")]
        public void CompleteUserWriteFormsAreAvailable(string name)=>Assert.That(typeof(UserApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        [TestCase("UserWriteRequest")]
        [TestCase("UserBatchWriteRequest")]
        public void CurrentUserWriteRequestsAreAvailable(string name)=>Assert.That(typeof(UserApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model."+name),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static JObject Schema()=>(JObject)Contract()["components"]["schemas"]["UserForm"]["properties"];
        private static string Property(string name)=>char.ToUpperInvariant(name[0])+name.Substring(1);
        private static UserWriteRequest Row(bool creating)=>creating?new UserWriteRequest{Email="fixture@users.invalid",FirstName="original first",LastName="original last"}:new UserWriteRequest{Uid="original user UID"};
        private static UserBatchWriteRequest Batch(JObject row)=>new UserBatchWriteRequest(new JArray(row));
        private static string Name(bool creating)=>creating?"CreateUsersWithResult":"UpdateUsersWithResult";
        public static IEnumerable<TestCaseData> Fields(){foreach(var field in Schema().Properties().Where(p=>!p.Value.Value<bool?>("readOnly").GetValueOrDefault()))foreach(var creating in new[]{false,true})foreach(var mode in new[]{0,1,2})yield return new TestCaseData(field.Name,creating,mode);}
        [TestCaseSource(nameof(Fields))]
        public void KnownWritableKindsFailBeforeHttp(string field,bool creating,int mode)
        {
            var row=Row(creating);row[field]=Schema()[field].Value<string>("type")=="string"?(JToken)new JValue(false):new JValue("invalid");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [TestCaseSource(nameof(Fields))]
        public async Task RawNullIsPreservedOnlyForUpdates(string field,bool creating,int mode)
        {
            var row=Row(creating);row[field]=JValue.CreateNull();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){if(creating){Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}else{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);Assert.That(JArray.Parse(handler.Body)[0][field].Type,Is.EqualTo(JTokenType.Null));Assert.That(handler.Calls,Is.EqualTo(1));}}
        }
        [Test]
        public void ReadOnlyFieldsFailEvenWhenNull([Values("status","specialRights","creationTs")]string field,[Values(false,true)]bool creating,[Values(false,true)]bool nil,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);row[field]=nil?JValue.CreateNull():new JValue("original readonly");var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);Assert.That(typeof(UserWriteRequest).GetProperty(Property(field)),Is.Null);
        }
        [Test]
        public void CreationRowsRequireExplicitNamesAndEmail([Values("email","firstName","lastName")]string field,[Values(0,1,2)]int mode)
        {
            var row=Row(true);row.Remove(field);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),true,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void InvalidBatchRowsFailBeforeHttp([Values("[null]","[false]","[0]","[\"row\"]","[[]]")]string json,[Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),new UserBatchWriteRequest(JArray.Parse(json)),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        private static JObject Assignment(string field)=>field=="profiles"?new JObject{["name"]=" original profile ",["organisation"]=" original organisation ", ["future"]=false}:field=="licenses"?new JObject{["type"]=" original license ",["future"]=false}:new JObject{["teamworkRole"]="internal",["organisation"]=" original organisation ",["future"]=false};
        public static IEnumerable<TestCaseData> Assignments()
        {
            foreach(var pair in new[]{new[]{"profiles","Profile"},new[]{"licenses","License"},new[]{"teamworkRoles","TeamworkRole"}})foreach(var key in Contract()["components"]["schemas"][pair[1]]["required"].Values<string>())foreach(var shape in new[]{0,1,2,3})foreach(var creating in new[]{false,true})foreach(var mode in new[]{0,1,2})yield return new TestCaseData(pair[0],key,shape,creating,mode);
        }
        [TestCaseSource(nameof(Assignments))]
        public void AssignmentRequiredStringsAreExplicit(string field,string key,int shape,bool creating,int mode)
        {
            var entry=Assignment(field);if(shape==0)entry.Remove(key);else entry[key]=shape==1?JValue.CreateNull():shape==2?new JValue(false):new JValue(0);var row=Row(creating);row[field]=new JArray(entry);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void AssignmentRowsMustBeObjects([Values("profiles","licenses","teamworkRoles")]string field,[Values("[null]","[false]","[[]]","[\"row\"]")]string json,[Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);row[field]=JArray.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void LicenseExpiryCannotBeWrittenEvenWhenNull([Values(false,true)]bool creating,[Values(false,true)]bool nil,[Values(0,1,2)]int mode)
        {
            var entry=Assignment("licenses");entry["validTill"]=nil?JValue.CreateNull():new JValue(long.MaxValue);var row=Row(creating);row.Licenses=new JArray(entry);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public void ConstructorsTypedNullsAndAssignmentGettersDeeplyCopy()
        {
            var source=new JObject{["future"]=new JObject{["value"]=1}};var row=new UserWriteRequest(source);source["future"]["value"]=2;Assert.That(row["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new UserWriteRequest(null)));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new UserBatchWriteRequest(null)));
            foreach(var field in Schema().Properties().Where(p=>!p.Value.Value<bool?>("readOnly").GetValueOrDefault())){row[field.Name]=JValue.CreateNull();typeof(UserWriteRequest).GetProperty(Property(field.Name)).SetValue(row,null);Assert.That(row.ContainsKey(field.Name),Is.False);}
            foreach(var name in new[]{"profiles","licenses","teamworkRoles"}){var original=new JArray(Assignment(name));typeof(UserWriteRequest).GetProperty(Property(name)).SetValue(row,original);original[0]["future"]=true;var read=(JArray)typeof(UserWriteRequest).GetProperty(Property(name)).GetValue(row);read[0]["future"]=true;Assert.That(row[name][0].Value<bool>("future"),Is.False);}
            var originalRows=new JArray(row);var batch=new UserBatchWriteRequest(originalRows);originalRows[0]["future"]["value"]=9;Assert.That(batch[0]["future"].Value<int>("value"),Is.EqualTo(1));
        }
        [Test]
        public void PrimaryOptionalArrayContractsAndReadOnlyAssignmentsRemainExact()
        {
            var c=Contract();foreach(var verb in new[]{"post","put"}){var p=c["paths"]["/users"][verb];Assert.That(p["requestBody"]["required"],Is.Null);var schema=p["requestBody"]["content"]["application/json"]["schema"];Assert.That(schema.Value<string>("type"),Is.EqualTo("array"));Assert.That(schema["items"].Value<string>("$ref"),Does.EndWith("/UserForm"));Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p.Value<string>("description"),Does.Contain("Benutzer (Bearbeiten)"));}Assert.That(Schema().Count,Is.EqualTo(18));Assert.That(c["components"]["schemas"]["UserForm"]["required"].Values<string>(),Is.EquivalentTo(new[]{"email","firstName","lastName"}));Assert.That(Schema().Properties().Where(p=>p.Value.Value<bool?>("readOnly").GetValueOrDefault()).Select(p=>p.Name),Is.EquivalentTo(new[]{"status","specialRights","creationTs"}));Assert.That(c["components"]["schemas"]["License"]["properties"]["validTill"].Value<bool>("readOnly"),Is.True);Assert.That(c["components"]["schemas"]["Profile"]["properties"]["name"].Value<string>("description"),Does.Contain("administrator"));Assert.That(c["components"]["schemas"]["Profile"]["properties"]["organisation"].Value<string>("description"),Does.Contain("Instanz"));
        }
        [Test]
        public async Task AllWritableFieldsPreserveOriginalArrayWireAndMetadata([Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var row=new UserWriteRequest();Assert.That(typeof(UserWriteRequest).GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Length,Is.EqualTo(15));foreach(var field in Schema().Properties().Where(p=>!p.Value.Value<bool?>("readOnly").GetValueOrDefault())){var kind=field.Value.Value<string>("type");object value=kind=="boolean"?(object)false:kind=="array"?(object)new JArray(Assignment(field.Name)):"  original / "+field.Name;typeof(UserWriteRequest).GetProperty(Property(field.Name)).SetValue(row,value);}row["future"]=new JObject{["value"]=JValue.CreateNull()};var batch=Batch(row);var expected=batch.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler)){var result=await Invoke(Api(http),batch,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/users"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Method,Is.EqualTo(creating?HttpMethod.Post:HttpMethod.Put));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-user-write-token"));Assert.That(handler.Accept,Does.Contain("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.EqualTo(Result));Assert.That(result.Headers["X-User-Fixture"],Does.Contain("retained"));Assert.That(result.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(result.Data[0]["creationTs"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(result.Data[0]["licenses"][0]["validTill"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(result.Data[0]["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(result.Data[0]["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(result.Data[0].Value<string>("status"),Is.EqualTo("INVITED"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OptionalEmptySparseAndMultipleRowsRemainDistinct([Values(false,true)]bool creating,[Values(0,1,2)]int mode,[Values(0,1,2,3,4)]int shape)
        {
            var batch=shape==0?null:new UserBatchWriteRequest();if(shape==2)batch.Add(Row(creating));if(shape==3){var first=Row(creating);first.Uid="original first UID";var second=Row(creating);second.Uid="original second UID";batch.Add(first);batch.Add(second);}if(shape==4)batch.Add(new JObject());var expected=batch?.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler)){if(creating&&shape==4){Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(Api(http),batch,creating,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);}else{var result=await Invoke(Api(http),batch,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));Assert.That(result.Data,Is.Null);Assert.That(handler.Calls,Is.EqualTo(1));}}
        }
        [Test]
        public async Task OmittedFalseTrueFlagsRemainExplicit([Values("administrator","changePassword","updateScopevisioEMail","updateTeamworkEMail")]string field,[Values(-1,0,1)]int choice,[Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);if(choice>=0)row[field]=choice==1;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);var sent=(JObject)JArray.Parse(handler.Body)[0];if(choice<0)Assert.That(sent.ContainsKey(field),Is.False);else Assert.That(sent.Value<bool>(field),Is.EqualTo(choice==1));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalLoginsUidAndGenderAreNotNormalizedOrResolved([Values(false,true)]bool creating,[Values(""," "," original CASE / 税 ")]string text,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);foreach(var field in Schema().Properties().Where(p=>!p.Value.Value<bool?>("readOnly").GetValueOrDefault()&&p.Value.Value<string>("type")=="string"))row[field.Name]=text;var batch=Batch(row);var expected=batch.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),batch,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ProfilesLicensesAndRolesRetainScopeWithoutPrivilegeDefaults([Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);row.Profiles=new JArray(new JObject{["name"]="Firmen und Benutzer",["organisation"]="Instanz"});row.Licenses=new JArray(new JObject{["type"]="original configured license",["future"]=JValue.CreateNull()});row.TeamworkRoles=new JArray(new JObject{["teamworkRole"]=" original future role ",["organisation"]=" original organisation name "});var batch=Batch(row);var expected=batch.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),batch,creating,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(expected));var sent=(JObject)JArray.Parse(handler.Body)[0];Assert.That(sent.ContainsKey("administrator"),Is.False);Assert.That(sent.ContainsKey("buyMissingLicenses"),Is.False);Assert.That(sent.ContainsKey("password"),Is.False);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task AssignmentEmptyArraysRemainExplicit([Values("profiles","licenses","teamworkRoles")]string field,[Values(false,true)]bool creating,[Values(0,1,2)]int mode)
        {
            var row=Row(creating);row[field]=new JArray();var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),Batch(row),creating,mode,CancellationToken.None);Assert.That(JArray.Parse(handler.Body)[0][field].ToString(Formatting.None),Is.EqualTo("[]"));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task ActiveSnapshotIncludesAllRowsAssignmentsAndFutureData([Values(false,true)]bool creating)
        {
            var row=Row(creating);row.Profiles=new JArray(Assignment("profiles"));row.TeamworkRoles=new JArray(Assignment("teamworkRoles"));row["future"]=new JObject{["value"]=1};var batch=Batch(row);var expected=batch.ToString(Formatting.None);var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(200,"{}");});using(var http=new HttpClient(handler)){var operation=Invoke(Api(http),batch,creating,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));batch[0]["profiles"][0]["name"]="after";batch[0]["teamworkRoles"][0]["organisation"]="after";batch[0]["future"]["value"]=9;batch.Add(Row(creating));Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task AllDataFormsPreserveFutureErrorsWithoutAggregateSuccess([Values(false,true)]bool creating,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"errors\":[\"user\"],\"updated\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),null,creating,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void OriginalFactoryPrecedesFailureParsing([Values(false,true)]bool creating,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original user diagnostic")));using(var http=new HttpClient(handler)){var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,null,creating,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original user diagnostic"));var expected=new InvalidOperationException("custom user hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(Name(creating)));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,null,creating,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));}
        }
        [Test]
        public void InvalidSuccessJsonIsNotInvented([Values(false,true)]bool creating)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),null,creating,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void PrecancellationHasNoEffects([Values(false,true)]bool creating)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),null,creating,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }
        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(false,true)]bool creating)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){var operation=Invoke(Api(http),Batch(Row(creating)),creating,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}}
        }
        [Test]
        public async Task LegacyModelsFlagsFieldsAndDataDiscardingRemainUnchanged()
        {
            var legacy=new UserForm(firstName:"original",lastName:"original",email:"fixture@users.invalid");Assert.That(legacy.Administrator,Is.False);Assert.That(legacy.ChangePassword,Is.False);Assert.That(legacy.BuyMissingLicenses,Is.False);Assert.That(typeof(UserForm).GetProperty("Password"),Is.Not.Null);foreach(var property in new[]{"PendingLogin","UpdateScopevisioEMail","UpdateTeamworkEMail","NewEmail"})Assert.That(typeof(UserForm).GetProperty(property),Is.Null);foreach(var name in new[]{"CreateUsers","UpdateUsers"})Assert.That(typeof(UserApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.CreateUsersWithHttpInfo(new List<UserForm>{legacy}).Data,Is.Null);Assert.That((await api.UpdateUsersAsyncWithHttpInfo(new List<UserForm>{legacy})).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        private const string Result="[{\"status\":\"INVITED\",\"creationTs\":9223372036854775807,\"specialRights\":[\"original\"],\"licenses\":[{\"type\":\"original\",\"validTill\":9223372036854775807}],\"fraction\":123456789.123456789,\"date\":\"2026-10-15T00:00:00Z\",\"future\":false}]";
        private static UserApi Api(HttpClient http)=>new UserApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-user-write-token"});
        private static Task<ApiResponse<JToken>> Invoke(UserApi api,UserBatchWriteRequest request,bool creating,int mode,CancellationToken token)=>creating?mode==0?Task.FromResult(api.CreateUsersWithResultWithHttpInfo(request)):mode==1?api.CreateUsersWithResultAsyncWithHttpInfo(request):api.CreateUsersWithResultAsyncWithHttpInfo(request,token):mode==0?Task.FromResult(api.UpdateUsersWithResultWithHttpInfo(request)):mode==1?api.UpdateUsersWithResultAsyncWithHttpInfo(request):api.UpdateUsersWithResultAsyncWithHttpInfo(request,token);
        private static Task<JToken> InvokeData(UserApi api,UserBatchWriteRequest request,bool creating,int mode)=>creating?mode==0?Task.FromResult(api.CreateUsersWithResult(request)):mode==1?api.CreateUsersWithResultAsync(request):api.CreateUsersWithResultAsync(request,CancellationToken.None):mode==0?Task.FromResult(api.UpdateUsersWithResult(request)):mode==1?api.UpdateUsersWithResultAsync(request):api.UpdateUsersWithResultAsync(request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-User-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
