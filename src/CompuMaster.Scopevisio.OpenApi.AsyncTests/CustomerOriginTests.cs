using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class CustomerOriginTests
    {
        [Test] public void OptionalOriginIsAvailable()=>Assert.That(typeof(Customer).GetProperty("Origin")?.PropertyType,Is.EqualTo(typeof(string)));
        private static Customer Valid()=>new Customer("0007","fixture-customer","fixture-status");
        [Test]
        public void CurrentPrimaryOriginHasNoInventedMeaningOrRequirement()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));var c=JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["Customer"];Assert.That(((JObject)c["properties"]).Properties().Select(x=>x.Name),Is.EquivalentTo(new[]{"publicId","name","status","origin"}));Assert.That(c["required"],Is.Null);Assert.That(c["properties"]["origin"].ToString(),Is.EqualTo(new JObject{["type"]="string"}.ToString()));
        }
        [Test] public void DataContractIsOptionalWithExactWireName(){var a=typeof(Customer).GetProperty("Origin").GetCustomAttribute<DataMemberAttribute>();Assert.That(a.Name,Is.EqualTo("origin"));Assert.That(a.IsRequired,Is.False);Assert.That(a.EmitDefaultValue,Is.False);}
        [Test]
        public void NullAndAbsentKeepLegacySerialization([Values(false,true)]bool explicitNull)
        {
            var input=JObject.Parse(Valid().ToJson());if(explicitNull)input["origin"]=JValue.CreateNull();var model=input.ToObject<Customer>();Assert.That(model.Origin,Is.Null);Assert.That(JObject.Parse(model.ToJson()),Is.EqualTo(JObject.Parse(Valid().ToJson())));
        }
        [Test]
        public void OriginalMetadataRoundtripsWithoutEndpointParsing([Values("","  ","0001","original / Ä?%","https://prod.invalid/rest")]string value,[Values(false,true)]bool xml)
        {
            var model=Valid();model.Origin=value;Customer actual;if(xml){var serializer=new DataContractSerializer(typeof(Customer));using(var stream=new MemoryStream()){serializer.WriteObject(stream,model);stream.Position=0;actual=(Customer)serializer.ReadObject(stream);}}else actual=JObject.Parse(model.ToJson()).ToObject<Customer>();Assert.That(actual.Origin,Is.EqualTo(value));Assert.That(actual.PublicId,Is.EqualTo("0007"));Assert.That(actual.Name,Is.EqualTo("fixture-customer"));Assert.That(actual.Status,Is.EqualTo("fixture-status"));
        }
        [Test]
        public void GeneratedEqualityHashAndDiagnosticsStayUnchanged()
        {
            var original=Valid();var model=Valid();model.Origin="origin-metadata";Assert.That(model.Equals(original),Is.True);Assert.That(model.GetHashCode(),Is.EqualTo(original.GetHashCode()));Assert.That(model.ToString(),Is.EqualTo(original.ToString()));Assert.That(JObject.Parse(model.ToJson()).Value<string>("origin"),Is.EqualTo("origin-metadata"));model.Origin=null;Assert.That(model.ToJson(),Is.EqualTo(original.ToJson()));
        }
        [Test]
        public async Task NestedOriginAndRawAccountResponseDoNotRetargetContext([Values(0,1,2)]int mode,[Values("absent","null","","  ","original / Ä?%","https://prod.invalid/rest")]string shape)
        {
            var customer=JObject.Parse(Valid().ToJson());if(shape!="absent")customer["origin"]=shape=="null"?JValue.CreateNull():new JValue(shape);customer["future"]=new JObject{["value"]=false};var account=new JObject{["customer"]=customer,["organisation"]=new JObject{["id"]=7,["name"]="fixture-org",["teamworkTenantId"]="fixture-tw"},["user"]=new JObject{["uid"]="fixture-uid"}};var body=account.ToString();var handler=new Handler(body);using(var http=new HttpClient(handler)){var config=new Configuration{HttpClient=http,BasePath="https://origin.invalid/rest",AccessToken="fixture-origin-token",OrganisationId=7};var api=new AdditionalApi(config);var response=mode==0?api.GetApplicationContextWithHttpInfo():mode==1?await api.GetApplicationContextAsyncWithHttpInfo():await api.GetApplicationContextAsyncWithHttpInfo(CancellationToken.None);Assert.That(response.Data.Customer.Origin,Is.EqualTo(shape=="absent"||shape=="null"?null:shape));Assert.That(response.RawJsonContent,Is.EqualTo(body));Assert.That(response.StatusCode,Is.EqualTo(200));Assert.That(response.Headers["X-Origin-Fixture"],Does.Contain("retained"));Assert.That(response.Headers["Content-Type"],Does.Contain("application/json"));Assert.That(response.Data.Organisation.Id,Is.EqualTo(7));Assert.That(response.Data.Organisation.TeamworkTenantId,Is.EqualTo("fixture-tw"));Assert.That(response.Data.User.Uid,Is.EqualTo("fixture-uid"));Assert.That(config.BasePath,Is.EqualTo("https://origin.invalid/rest"));Assert.That(config.OrganisationId,Is.EqualTo(7));Assert.That(config.AccessToken,Is.EqualTo("fixture-origin-token"));Assert.That(handler.Calls,Is.EqualTo(1));}
        }
        private sealed class Handler:HttpMessageHandler
        {
            private readonly string body;internal int Calls;internal Handler(string body){this.body=body;}
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellation)
            {
                Assert.That(request.RequestUri.Host,Is.EqualTo("origin.invalid"));Assert.That(request.RequestUri.AbsolutePath,Is.EqualTo("/rest/myaccount"));Assert.That(request.Method,Is.EqualTo(HttpMethod.Get));Assert.That(request.Content,Is.Null);Assert.That(request.RequestUri.Query,Is.Empty);Assert.That(request.Headers.Authorization?.Parameter,Is.EqualTo("fixture-origin-token"));Calls++;var response=new HttpResponseMessage(HttpStatusCode.OK){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.TryAddWithoutValidation("X-Origin-Fixture","retained");return Task.FromResult(response);
            }
        }
    }
}
