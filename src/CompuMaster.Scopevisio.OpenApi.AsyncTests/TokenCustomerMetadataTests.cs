using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    public class TokenCustomerMetadataTests
    {
        [TestCase("CustomerId")][TestCase("PublicId")]
        public void CurrentOptionalMetadataIsAvailable(string name)=>Assert.That(typeof(TokenResponse).GetProperty(name)?.PropertyType,Is.EqualTo(typeof(string)));
        private static TokenResponse Valid()=>new TokenResponse(TokenResponse.TokenTypeEnum.Bearer,"fixture-access",123,"fixture-refresh","fixture-uid",7,"fixture-org","fixture-tw");
        [Test]
        public void CurrentPrimaryFieldsAreOptionalStringsAndNamespacesAreDistinct()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));var c=JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"]["TokenResponse"];Assert.That(((JObject)c["properties"]).Count,Is.EqualTo(10));Assert.That(c["required"].Count(),Is.EqualTo(7));foreach(var name in new[]{"customerId","publicId"}){Assert.That(c["properties"][name].Value<string>("type"),Is.EqualTo("string"));Assert.That(c["required"].Values<string>(),Does.Not.Contain(name));}Assert.That(c["properties"]["organisationId"].Value<string>("format"),Is.EqualTo("int64"));Assert.That(c["properties"]["customerId"].Value<string>("description"),Is.EqualTo("Customer (tenant) id"));Assert.That(c["properties"]["publicId"].Value<string>("description"),Is.EqualTo("Public customer number"));
        }
        [TestCase("CustomerId","customerId")][TestCase("PublicId","publicId")]
        public void MetadataUsesExactOptionalDataContract(string property,string wire)
        {
            var attribute=typeof(TokenResponse).GetProperty(property).GetCustomAttribute<DataMemberAttribute>();Assert.That(attribute.Name,Is.EqualTo(wire));Assert.That(attribute.EmitDefaultValue,Is.False);Assert.That(attribute.IsRequired,Is.False);
        }
        [Test]
        public void MissingOrNullMetadataDoesNotChangeLegacyFields([Values(false,true)]bool explicitNull)
        {
            var json=JObject.Parse(Valid().ToJson());if(explicitNull){json["customerId"]=JValue.CreateNull();json["publicId"]=JValue.CreateNull();}var model=json.ToObject<TokenResponse>();Assert.That(model.CustomerId,Is.Null);Assert.That(model.PublicId,Is.Null);Assert.That(model.Equals(Valid()),Is.True);var output=JObject.Parse(model.ToJson());Assert.That(output.ContainsKey("customerId"),Is.False);Assert.That(output.ContainsKey("publicId"),Is.False);Assert.That(output,Is.EqualTo(JObject.Parse(Valid().ToJson())));
        }
        [Test]
        public void OriginalStringsRoundtripWithoutNumericOrGuidConversion([Values("CustomerId","PublicId")]string property,[Values("","  ","000001","customer_original / Ä%?","9223372036854775808")]string value,[Values(false,true)]bool xml)
        {
            var model=Valid();typeof(TokenResponse).GetProperty(property).SetValue(model,value);TokenResponse roundtrip;if(xml){var serializer=new DataContractSerializer(typeof(TokenResponse));using(var stream=new MemoryStream()){serializer.WriteObject(stream,model);stream.Position=0;roundtrip=(TokenResponse)serializer.ReadObject(stream);}}else roundtrip=JsonConvert.DeserializeObject<TokenResponse>(model.ToJson());Assert.That(typeof(TokenResponse).GetProperty(property).GetValue(roundtrip),Is.EqualTo(value));Assert.That(roundtrip.OrganisationId,Is.EqualTo(7));Assert.That(roundtrip.TeamworkTenantId,Is.EqualTo("fixture-tw"));Assert.That(roundtrip.Uid,Is.EqualTo("fixture-uid"));
        }
        [Test]
        public void LegacyEqualityHashAndDiagnosticFormattingRemainUnchanged()
        {
            var original=Valid();var model=Valid();model.CustomerId="fixture-customer-original";model.PublicId="0000001";Assert.That(model.Equals(original),Is.True);Assert.That(model.GetHashCode(),Is.EqualTo(original.GetHashCode()));Assert.That(model.ToString(),Is.EqualTo(original.ToString()));var json=JObject.Parse(model.ToJson());Assert.That(json.Value<string>("customerId"),Is.EqualTo("fixture-customer-original"));Assert.That(json.Value<string>("publicId"),Is.EqualTo("0000001"));model.CustomerId=null;model.PublicId=null;Assert.That(JObject.Parse(model.ToJson()),Is.EqualTo(JObject.Parse(original.ToJson())));
        }
        [Test]
        public async Task MetadataCannotSubstituteForExplicitOrganisationAndFreshUserProof([Values(0,1,2)]int mode,[Values("ok","wrong-token-org","missing-token-org","empty-token-uid","wrong-account-org","wrong-account-uid")]string shape)
        {
            var candidate=JObject.Parse(Valid().ToJson());candidate["customerId"]="7";candidate["publicId"]="0007";var account=new JObject{["user"]=new JObject{["uid"]="fixture-uid"},["organisation"]=new JObject{["id"]=7}};if(shape=="wrong-token-org")candidate["organisationId"]=8;if(shape=="missing-token-org")candidate.Remove("organisationId");if(shape=="empty-token-uid")candidate["uid"]="";if(shape=="wrong-account-org")account["organisation"]["id"]=8;if(shape=="wrong-account-uid")account["user"]["uid"]="fixture-other";var handler=new Handler(candidate.ToString(),account.ToString());using(var http=new HttpClient(handler)){var config=new Configuration{HttpClient=http,BasePath="https://metadata.invalid/rest",OrganisationId=7,ClientNumber="fixture-customer",Username="fixture-login",Password="fixture-password",AccessToken="fixture-prior"};var client=new OpenScopeApiClient(config);var prior=Valid();client.Token=prior;Func<Task> authorize=()=>{if(mode==0){client.AuthorizeWithUserCredentials();return Task.CompletedTask;}return mode==1?client.AuthorizeWithUserCredentialsAsync():client.AuthorizeWithUserCredentialsAsync(CancellationToken.None);};if(shape=="ok"){await authorize();Assert.That(client.Token.CustomerId,Is.EqualTo("7"));Assert.That(client.Token.PublicId,Is.EqualTo("0007"));Assert.That(client.Token.TeamworkTenantId,Is.EqualTo("fixture-tw"));Assert.That(client.Token.OrganisationId,Is.EqualTo(7));Assert.That(client.Token.Uid,Is.EqualTo("fixture-uid"));Assert.That(config.AccessToken,Is.EqualTo("fixture-access"));Assert.That(handler.Paths,Is.EqualTo(new[]{"/rest/token","/rest/myaccount"}));}else{Assert.ThrowsAsync<InvalidDataException>((Func<Task>)(async()=>await authorize()));Assert.That(client.Token,Is.SameAs(prior));Assert.That(config.AccessToken,Is.EqualTo("fixture-prior"));Assert.That(handler.Paths.Count,Is.EqualTo(shape.StartsWith("wrong-account",StringComparison.Ordinal)?2:1));}Assert.That(config.OrganisationId,Is.EqualTo(7));}
        }
        private sealed class Handler:HttpMessageHandler
        {
            private readonly string tokenBody,accountBody;internal readonly List<string> Paths=new List<string>();
            internal Handler(string tokenBody,string accountBody){this.tokenBody=tokenBody;this.accountBody=accountBody;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellation)
            {
                Assert.That(request.RequestUri.Host,Is.EqualTo("metadata.invalid"));Paths.Add(request.RequestUri.AbsolutePath);if(request.RequestUri.AbsolutePath=="/rest/token"){Assert.That(request.Method,Is.EqualTo(HttpMethod.Post));var form=await request.Content.ReadAsStringAsync();Assert.That(form,Does.Contain("organisation_id=7"));Assert.That(form,Does.Not.Contain("organisation="));return new HttpResponseMessage(HttpStatusCode.OK){Content=new StringContent(tokenBody,Encoding.UTF8,"application/json")};}Assert.That(request.RequestUri.AbsolutePath,Is.EqualTo("/rest/myaccount"));Assert.That(request.Method,Is.EqualTo(HttpMethod.Get));Assert.That(request.Headers.Authorization?.Parameter,Is.EqualTo("fixture-access"));return new HttpResponseMessage(HttpStatusCode.OK){Content=new StringContent(accountBody,Encoding.UTF8,"application/json")};
            }
        }
    }
}
