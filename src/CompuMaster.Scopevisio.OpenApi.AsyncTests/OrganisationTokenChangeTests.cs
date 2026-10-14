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
    public class OrganisationTokenChangeTests
    {
        [TestCase("ChangeOrganisationByIdWithResult")]
        [TestCase("ChangeOrganisationByNameWithResult")]
        public void CompleteOrganisationTokenFormsAreAvailable(string name)=>Assert.That(typeof(AuthorizationApi).GetMethods().Count(m=>m.Name.StartsWith(name,StringComparison.Ordinal)),Is.EqualTo(6));
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        [Test]
        public void PrimaryExplicitSelectorsHaveNoBodyOrFixedSuccessSchema()
        {
            var c=Contract();foreach(var byId in new[]{false,true}){var p=c["paths"][byId?"/token/organisation/{organisationId}":"/token/organisation"]["put"];Assert.That(p["requestBody"],Is.Null);Assert.That(p["responses"]["200"]["content"],Is.Null);Assert.That(p["parameters"][0].Value<bool>("required"),Is.True);Assert.That(p["parameters"][0].Value<string>("in"),Is.EqualTo(byId?"path":"query"));Assert.That(p["parameters"][0]["schema"].Value<string>("type"),Is.EqualTo(byId?"integer":"string"));if(byId)Assert.That(p["parameters"][0]["schema"].Value<string>("format"),Is.EqualTo("int64"));Assert.That(p["responses"]["400"].Value<string>("description"),Does.Contain("Invalid organisation"));}
        }
        [Test]
        public async Task OriginalInvariantInt64PathHasNoBodyOrLookup([Values(long.MinValue,-1L,0L,42L,long.MaxValue)]long id,[Values(0,1,2)]int mode)
        {
            var previous=CultureInfo.CurrentCulture;var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){try{CultureInfo.CurrentCulture=new CultureInfo("ar-SA");await Invoke(Api(http),id,"unused",true,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/token/organisation/"+id.ToString(CultureInfo.InvariantCulture)));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Uri.Fragment,Is.Empty);Assert.That(handler.Body,Is.Null);}finally{CultureInfo.CurrentCulture=previous;}}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OriginalNameIsExactlyOneEncodedQueryValue([Values(""," ",".","..","0007"," original / 税 ?#%&=+ ","a%2Fb","line\r\n name")]string name,[Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler)){await Invoke(Api(http),0,name,false,mode,CancellationToken.None);Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/token/organisation"));Assert.That(handler.Uri.Fragment,Is.Empty);var pairs=handler.Uri.Query.TrimStart('?').Split('&');Assert.That(pairs.Length,Is.EqualTo(1));var pair=pairs[0].Split(new[]{'='},2);Assert.That(pair[0],Is.EqualTo("name"));Assert.That(Uri.UnescapeDataString(pair.Length==2?pair[1].Replace("+"," "):""),Is.EqualTo(name));Assert.That(handler.Body,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void MissingRequiredNameFailsBeforeHttp([Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentNullException>((Func<Task>)(async()=>{await Invoke(Api(http),0,null,false,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }
        [Test]
        public async Task CompleteResponsePreservesFutureTokensButDoesNotInstallOrVerifyThem([Values(false,true)]bool byId,[Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,Result)));using(var http=new HttpClient(handler)){http.Timeout=TimeSpan.FromSeconds(17);var config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-org-token",OrganisationId=42,Timeout=1234};var api=new AuthorizationApi(config);var result=await Invoke(api,42," original requested name ",byId,mode,CancellationToken.None);Assert.That(handler.Method,Is.EqualTo(HttpMethod.Put));Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-org-token"));Assert.That(handler.Accept,Does.Contain("application/json"));Assert.That(handler.Body,Is.Null);Assert.That(result.StatusCode,Is.EqualTo(200));Assert.That(result.RawJsonContent,Is.EqualTo(Result));Assert.That(result.Headers["X-Organisation-Token-Fixture"],Does.Contain("retained"));Assert.That(result.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(result.Data["organisation_id"].Value<long>(),Is.EqualTo(7));Assert.That(result.Data.Value<string>("uid"),Is.EqualTo("different returned user"));Assert.That(result.Data.Value<string>("access_token"),Is.EqualTo("returned-fixture-token"));Assert.That(result.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(result.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(config.AccessToken,Is.EqualTo("isolated-org-token"));Assert.That(config.OrganisationId,Is.EqualTo(42));Assert.That(config.Timeout,Is.EqualTo(1234));Assert.That(config.HttpClient,Is.SameAs(http));Assert.That(http.Timeout,Is.EqualTo(TimeSpan.FromSeconds(17)));Assert.That(api.Configuration,Is.SameAs(config));Assert.That(((JObject)result.Data).ContainsKey("verifiedScope"),Is.False);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task AllDataFormsPreserveJSONWithoutScopeSuccessAssumptions([Values(false,true)]bool byId,[Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"scope_error\":\"original\"}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(Api(http),42,"original",byId,mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task EmptySuccessIsNullWithoutInventedToken([Values(false,true)]bool byId,[Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"")));using(var http=new HttpClient(handler))Assert.That((await Invoke(Api(http),42,"original",byId,mode,CancellationToken.None)).Data,Is.Null);Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void OriginalFailuresAndFactoryPrecedeParsing([Values(false,true)]bool byId,[Values(0,2)]int mode,[Values(400,401,403,404,500)]int status)
        {
            var handler=new Handler(token=>Task.FromResult(Response(status,"original token selector diagnostic")));using(var http=new HttpClient(handler)){var api=Api(http);var error=Assert.ThrowsAsync<ApiException>((Func<Task>)(async()=>{await Invoke(api,42,"original",byId,mode,CancellationToken.None);}));Assert.That(error.ErrorCode,Is.EqualTo(status));Assert.That((object)error.ErrorContent,Is.EqualTo("original token selector diagnostic"));var expected=new InvalidOperationException("custom selector hook");api.ExceptionFactory=(name,response)=>{Assert.That(name,Is.EqualTo(byId?"ChangeOrganisationByIdWithResult":"ChangeOrganisationByNameWithResult"));return expected;};Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async()=>{await Invoke(api,42,"original",byId,mode,CancellationToken.None);})),Is.SameAs(expected));Assert.That(handler.Calls,Is.EqualTo(2));}
        }
        [Test]
        public void InvalidJSONIsNotVerifiedScope([Values(false,true)]bool byId)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(Api(http),42,"original",byId,2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public void PrecancellationHasNoEffects([Values(false,true)]bool byId)
        {
            var handler=new Handler(token=>Task.FromResult(Response(200,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(Api(http),42,null,byId,2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }
        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay([Values(false,true)]bool byId)
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(200,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){var operation=Invoke(Api(http),42,"original",byId,2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}}
        }
        [Test]
        public async Task LegacySelectorsValidationAndDataDiscardingRemainAvailable()
        {
            foreach(var name in new[]{"ChangeOrganisationById","ChangeOrganisationByName"})Assert.That(typeof(AuthorizationApi).GetMethods().Count(m=>m.Name==name||m.Name==name+"WithHttpInfo"||m.Name==name+"Async"||m.Name==name+"AsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(200,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var api=Api(http);Assert.That(api.ChangeOrganisationByIdWithHttpInfo(42).Data,Is.Null);Assert.That((await api.ChangeOrganisationByNameAsyncWithHttpInfo("original")).Data,Is.Null);Assert.Throws<ApiException>((TestDelegate)(()=>api.ChangeOrganisationByNameWithHttpInfo(null)));}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        private const string Result="{\"organisation_id\":7,\"uid\":\"different returned user\",\"access_token\":\"returned-fixture-token\",\"fraction\":123456789.123456789,\"date\":\"2026-10-15T00:00:00Z\",\"future\":false}";
        private static AuthorizationApi Api(HttpClient http)=>new AuthorizationApi(new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-org-token"});
        private static Task<ApiResponse<JToken>> Invoke(AuthorizationApi api,long id,string name,bool byId,int mode,CancellationToken token)=>byId?mode==0?Task.FromResult(api.ChangeOrganisationByIdWithResultWithHttpInfo(id)):mode==1?api.ChangeOrganisationByIdWithResultAsyncWithHttpInfo(id):api.ChangeOrganisationByIdWithResultAsyncWithHttpInfo(id,token):mode==0?Task.FromResult(api.ChangeOrganisationByNameWithResultWithHttpInfo(name)):mode==1?api.ChangeOrganisationByNameWithResultAsyncWithHttpInfo(name):api.ChangeOrganisationByNameWithResultAsyncWithHttpInfo(name,token);
        private static Task<JToken> InvokeData(AuthorizationApi api,long id,string name,bool byId,int mode)=>byId?mode==0?Task.FromResult(api.ChangeOrganisationByIdWithResult(id)):mode==1?api.ChangeOrganisationByIdWithResultAsync(id):api.ChangeOrganisationByIdWithResultAsync(id,CancellationToken.None):mode==0?Task.FromResult(api.ChangeOrganisationByNameWithResult(name)):mode==1?api.ChangeOrganisationByNameWithResultAsync(name):api.ChangeOrganisationByNameWithResultAsync(name,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-Organisation-Token-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
