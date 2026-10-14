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
    public class OrganisationCreationTests
    {
        [Test]
        public void CurrentOrganisationFormsAreAvailable()=>Assert.That(typeof(OrganisationApi).GetMethods().Count(m=>m.Name.StartsWith("CreateOrganisationWithResult",StringComparison.Ordinal)),Is.EqualTo(6));
        [Test]
        public void CurrentOrganisationRequestIsAvailable()=>Assert.That(typeof(OrganisationApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model.OrganisationCreationRequest"),Is.Not.Null);
        private static JObject Contract()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));return JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'));
        }
        private static string Name()=>"CreateOrganisationWithResult";
        private static OrganisationCreationRequest Valid()=>new OrganisationCreationRequest{OrganisationName=" original / § ",Scoa="SKR 03"};

        [Test]
        public void PrimaryOptionalBodyRequiredNameAndSlowChartSetupRemainExact()
        {
            var c=Contract();var schema=c["components"]["schemas"]["OrganisationForm"];Assert.That(schema["required"].Values<string>(),Is.EquivalentTo(new[]{"organisationName"}));Assert.That(((JObject)schema["properties"]).Properties().Select(prop=>prop.Name),Is.EquivalentTo(new[]{"organisationName","scoa"}));Assert.That(schema["properties"]["scoa"]["enum"].Count(),Is.EqualTo(12));Assert.That(schema["properties"]["scoa"].Value<string>("description"),Does.Contain("not set up"));var post=c["paths"]["/organisation/new"]["post"];Assert.That(post["requestBody"]["required"],Is.Null);Assert.That(post["responses"]["201"]["content"],Is.Null);Assert.That(post.Value<string>("description"),Does.Contain("Organisation (Bearbeiten)").And.Contain("at least 5 min"));Assert.That(post["responses"]["400"].Value<string>("description"),Does.Contain("already exists").And.Contain("chart of accounts"));
        }
        [Test]
        public void ConstructorsAndTypedOmissionCopyCompleteJson()
        {
            var original=new JObject{["future"]=new JObject{["value"]=1}};var request=new OrganisationCreationRequest(original);original["future"]["value"]=2;Assert.That(request["future"].Value<int>("value"),Is.EqualTo(1));Assert.Throws<ArgumentNullException>((TestDelegate)(()=>new OrganisationCreationRequest(null)));
            request.OrganisationName="";request.Scoa="";request.OrganisationName=null;request.Scoa=null;Assert.That(request.ContainsKey("organisationName"),Is.False);Assert.That(request.ContainsKey("scoa"),Is.False);Assert.That(request.Count,Is.EqualTo(1));
        }

        [Test]
        public async Task OriginalBase64FutureJsonAndAllMetadataRemain([Values(0,1,2)]int mode)
        {
            var request=Valid();request["future"]=new JObject{["nested"]=new JArray(false,0,JValue.CreateNull())};var expected=request.ToString(Formatting.None);var handler=new Handler(token=>Task.FromResult(Response(201,Result)));
            using(var http=new HttpClient(handler))
            {
                var apis=new Apis(http);var info=await Invoke(apis,request,mode,CancellationToken.None);Assert.That(apis.Invoice.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Config.HttpClient,Is.SameAs(http));Assert.That(apis.Config.AccessToken,Is.EqualTo("isolated-organisation-token"));
                Assert.That(handler.Method,Is.EqualTo(HttpMethod.Post));Assert.That(handler.Uri.AbsolutePath,Is.EqualTo("/rest/organisation/new"));Assert.That(handler.Uri.Query,Is.Empty);Assert.That(handler.Authorization,Is.EqualTo("Bearer isolated-organisation-token"));Assert.That(handler.Accept,Is.EqualTo("application/json"));Assert.That(handler.ContentType,Does.StartWith("application/json"));Assert.That(handler.Body,Is.EqualTo(expected));
                Assert.That(info.StatusCode,Is.EqualTo(201));Assert.That(info.RawJsonContent,Is.EqualTo(Result));Assert.That(info.Headers["X-File-Fixture"],Is.EqualTo("retained"));Assert.That(info.Headers["Content-Type"],Does.StartWith("application/json"));Assert.That(info.Data["futureId"].Value<long>(),Is.EqualTo(long.MaxValue));Assert.That(info.Data["fraction"].Value<decimal>(),Is.EqualTo(123456789.123456789m));Assert.That(info.Data["date"].Type,Is.EqualTo(JTokenType.String));Assert.That(handler.Calls,Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalBodyAndFieldsRemainServerDecisions([Values(0,1,2)]int mode,[Values("absent","empty","scoa-only","empty-name","original-name","null-name","null-scoa","empty-scoa")]string selection)
        {
            var request=selection=="absent"?null:new OrganisationCreationRequest();if(selection=="scoa-only")request.Scoa="original";if(selection=="empty-name")request.OrganisationName="";if(selection=="original-name"||selection=="null-scoa"||selection=="empty-scoa")request.OrganisationName=" original ... opaque \r\n ";if(selection=="null-name")request["organisationName"]=JValue.CreateNull();if(selection=="null-scoa")request["scoa"]=JValue.CreateNull();if(selection=="empty-scoa")request.Scoa="";
            bool invalid=selection=="empty"||selection=="scoa-only"||selection=="null-name"||selection=="null-scoa";var handler=new Handler(token=>Task.FromResult(Response(201,"")));using(var http=new HttpClient(handler))
            {
                if(invalid)Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,mode,CancellationToken.None);}));
                else{var info=await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(handler.Body,Is.EqualTo(request?.ToString(Formatting.None)));Assert.That(info.Data,Is.Null);Assert.That(info.RawJsonContent,Is.Empty);}
            }
            Assert.That(handler.Calls,Is.EqualTo(invalid?0:1));
        }

        [Test]
        public void KnownFieldsRequireStringKinds([Values("organisationName","scoa")]string field,[Values("false","7","{}","[]")]string json,[Values(0,1,2)]int mode)
        {
            var request=Valid();request[field]=JToken.Parse(json);var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<ArgumentException>((Func<Task>)(async()=>{await Invoke(new Apis(http),request,mode,CancellationToken.None);}));Assert.That(handler.Calls,Is.Zero);
        }

        [Test]
        public async Task SnapshotPrecedesActiveHttp()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var release=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var request=Valid();request["future"]=new JObject{["value"]=1};var expected=request.ToString(Formatting.None);var handler=new Handler(async token=>{entered.TrySetResult(true);await release.Task;return Response(201,"{}");});using(var http=new HttpClient(handler))
            {
                var operation=Invoke(new Apis(http),request,2,CancellationToken.None);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));request.OrganisationName="after";request.Scoa="after";request["future"]["value"]=2;Assert.That(handler.Body,Is.EqualTo(expected));}finally{release.TrySetResult(true);}await operation;
            }
        }

        [Test]
        public async Task CompleteDataFormsDoNotInventInvoiceId([Values(0,1,2)]int mode,[Values("{}","[]","null","true","{\"future\":false}")]string json)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,json)));using(var http=new HttpClient(handler)){var data=await InvokeData(new Apis(http),Valid(),mode);Assert.That(data.ToString(Formatting.None),Is.EqualTo(json));}Assert.That(handler.Calls,Is.EqualTo(1));
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
            var handler=new Handler(token=>Task.FromResult(Response(201,"{invalid")));using(var http=new HttpClient(handler))Assert.ThrowsAsync<JsonReaderException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),2,CancellationToken.None);}));Assert.That(handler.Calls,Is.EqualTo(1));
        }

        [Test]
        public void PrecancelledOperationHasNoEffects()
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource()){cancel.Cancel();Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await Invoke(new Apis(http),Valid(),2,cancel.Token);}));Assert.That(handler.Calls,Is.Zero);}
        }

        [Test]
        public async Task ActiveCancellationStopsNativeHttpWithoutReplay()
        {
            var entered=new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);var handler=new Handler(async token=>{entered.TrySetResult(true);await Task.Delay(Timeout.Infinite,token);return Response(201,"{}");});using(var http=new HttpClient(handler))using(var cancel=new CancellationTokenSource())
            {
                var operation=Invoke(new Apis(http),Valid(),2,cancel.Token);try{Assert.That(await Task.WhenAny(entered.Task,operation,Task.Delay(5000)),Is.SameAs(entered.Task));cancel.Cancel();Assert.That(await Task.WhenAny(operation,Task.Delay(5000)),Is.SameAs(operation));Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async()=>{await operation;}));Assert.That(handler.Calls,Is.EqualTo(1));}finally{cancel.Cancel();}
            }
        }

        [Test]
        public async Task LegacyOrganisationModelsEnumsAndDataDiscardingRemainAvailable()
        {
            var model=new OrganisationForm("original");Assert.That(model.OrganisationName,Is.EqualTo("original"));Assert.That(model.Scoa,Is.Null);Assert.That(typeof(OrganisationForm).GetProperty("Scoa").PropertyType,Is.EqualTo(typeof(OrganisationForm.ScoaEnum?)));Assert.That(typeof(OrganisationApi).GetMethods().Count(m=>m.Name=="CreateOrganisation"||m.Name=="CreateOrganisationWithHttpInfo"||m.Name=="CreateOrganisationAsync"||m.Name=="CreateOrganisationAsyncWithHttpInfo"),Is.EqualTo(4));var handler=new Handler(token=>Task.FromResult(Response(201,"{\"legacy\":true}")));using(var http=new HttpClient(handler)){var apis=new Apis(http);Assert.That(apis.Invoice.CreateOrganisationWithHttpInfo(model).Data,Is.Null);Assert.That((await apis.Invoice.CreateOrganisationAsyncWithHttpInfo(model)).Data,Is.Null);}Assert.That(handler.Calls,Is.EqualTo(2));
        }
        [Test]
        public async Task OriginalOrganisationAndChartStringsAreNotResolvedOrNormalized([Values("organisationName","scoa")]string field,[Values(""," "," original CASE / 税 ")]string value,[Values(0,1,2)]int mode)
        {
            var request=Valid();request[field]=value;var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body).Value<string>(field),Is.EqualTo(value));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        public static IEnumerable<TestCaseData> Charts(){foreach(var name in Contract()["components"]["schemas"]["OrganisationForm"]["properties"]["scoa"]["enum"].Values<string>())foreach(var mode in new[]{0,1,2})yield return new TestCaseData(name,mode);}
        [TestCaseSource(nameof(Charts))]
        public async Task CurrentChartChoicesRemainOriginal(string chart,int mode)
        {
            var request=Valid();request.Scoa=chart;var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body).Value<string>("scoa"),Is.EqualTo(chart));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task OmittedChartDoesNotInsertSetupOrDefaults([Values(0,1,2)]int mode)
        {
            var request=new OrganisationCreationRequest{OrganisationName="original"};var handler=new Handler(token=>Task.FromResult(Response(201,"{}")));using(var http=new HttpClient(handler)){await Invoke(new Apis(http),request,mode,CancellationToken.None);Assert.That(JObject.Parse(handler.Body).ContainsKey("scoa"),Is.False);}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        [Test]
        public async Task TimeoutsTokensOrganisationAndTransportAreNotChanged([Values(0,1,2)]int mode)
        {
            var handler=new Handler(token=>Task.FromResult(Response(201,"{\"futureOrganisation\":9223372036854775807}")));using(var http=new HttpClient(handler)){http.Timeout=TimeSpan.FromSeconds(17);var apis=new Apis(http);apis.Config.Timeout=1234;apis.Config.OrganisationId=42;var expectedToken=apis.Config.AccessToken;await Invoke(apis,Valid(),mode,CancellationToken.None);Assert.That(apis.Config.Timeout,Is.EqualTo(1234));Assert.That(http.Timeout,Is.EqualTo(TimeSpan.FromSeconds(17)));Assert.That(apis.Config.HttpClient,Is.SameAs(http));Assert.That(apis.Invoice.Configuration,Is.SameAs(apis.Config));Assert.That(apis.Config.AccessToken,Is.EqualTo(expectedToken));Assert.That(apis.Config.OrganisationId,Is.EqualTo(42));}Assert.That(handler.Calls,Is.EqualTo(1));
        }
        private const string Result="{\"futureId\":9223372036854775807,\"fraction\":123456789.123456789,\"date\":\"2026-10-14T00:00:00Z\",\"future\":false}";
        private sealed class Apis
        {
            internal Configuration Config;internal OrganisationApi Invoice;
            internal Apis(HttpClient http){Config=new Configuration{BasePath="https://openscope.invalid/rest",HttpClient=http,AccessToken="isolated-organisation-token"};Invoice=new OrganisationApi(Config);}
        }
        private static Task<ApiResponse<JToken>> Invoke(Apis api,OrganisationCreationRequest request,int mode,CancellationToken token)
            =>mode==0?Task.FromResult(api.Invoice.CreateOrganisationWithResultWithHttpInfo(request)):mode==1?api.Invoice.CreateOrganisationWithResultAsyncWithHttpInfo(request):api.Invoice.CreateOrganisationWithResultAsyncWithHttpInfo(request,token);
        private static Task<JToken> InvokeData(Apis api,OrganisationCreationRequest request,int mode)
            =>mode==0?Task.FromResult(api.Invoice.CreateOrganisationWithResult(request)):mode==1?api.Invoice.CreateOrganisationWithResultAsync(request):api.Invoice.CreateOrganisationWithResultAsync(request,CancellationToken.None);
        private static HttpResponseMessage Response(int status,string body){var response=new HttpResponseMessage((HttpStatusCode)status){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.Add("X-File-Fixture","retained");return response;}
        private sealed class Handler:HttpMessageHandler
        {
            private readonly Func<CancellationToken,Task<HttpResponseMessage>> send;internal int Calls;internal Uri Uri;internal HttpMethod Method;internal string Body,Authorization,Accept,ContentType;
            internal Handler(Func<CancellationToken,Task<HttpResponseMessage>> send){this.send=send;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token){Assert.That(request.RequestUri.Host,Is.EqualTo("openscope.invalid"));Calls++;Uri=request.RequestUri;Method=request.Method;Authorization=request.Headers.Authorization?.ToString();Accept=request.Headers.Accept.ToString();ContentType=request.Content?.Headers.ContentType?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync();return await send(token);}
        }
    }
}
