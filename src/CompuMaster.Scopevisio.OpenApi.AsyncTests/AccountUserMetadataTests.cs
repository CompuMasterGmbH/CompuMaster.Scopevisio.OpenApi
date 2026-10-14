using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
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
    public class AccountUserMetadataTests
    {
        [TestCase("Licenses",typeof(List<License>))][TestCase("Profiles",typeof(List<Profile>))]
        public void CurrentListsAreAvailable(string name,Type type)=>Assert.That(typeof(User).GetProperty(name)?.PropertyType,Is.EqualTo(type));
        private static User Valid()=>new User("fixture-user","fixture-login","fixture-first","fixture-last","fixture-status");
        private static JObject Json(string shape,long expiry=long.MaxValue)
        {
            var result=JObject.Parse(Valid().ToJson());if(shape=="absent")return result;if(shape=="null"){result["licenses"]=JValue.CreateNull();result["profiles"]=JValue.CreateNull();return result;}if(shape=="empty"){result["licenses"]=new JArray();result["profiles"]=new JArray();return result;}
            result["licenses"]=new JArray(new JObject{["type"]=" original / license Ä ",["validTill"]=expiry,["assignedToUid"]="unrelated-license-user",["assignedToEmail"]="original@example.invalid"},new JObject{["type"]=" second original "},new JObject{["type"]=" original / license Ä ",["validTill"]=expiry});result["profiles"]=new JArray(new JObject{["name"]=" original profile ",["organisation"]=" original org name "},new JObject{["name"]="Firmen und Benutzer",["organisation"]="Instanz"},new JObject{["name"]=" original profile ",["organisation"]=" original org name "});return result;
        }
        [Test]
        public void PrimaryArraysAndChildMetadataAreNotPermissionDecisions()
        {
            var bytes=File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"));using(var hash=SHA256.Create())Assert.That(BitConverter.ToString(hash.ComputeHash(bytes)).Replace("-","").ToLowerInvariant(),Is.EqualTo("40060e2bbbf187b5d6f8a0397bc97a5e09e8c8cf5d548644d676b16e8f13c339"));var schemas=JObject.Parse(Encoding.UTF8.GetString(bytes).TrimStart('\ufeff'))["components"]["schemas"];var user=schemas["User"];Assert.That(((JObject)user["properties"]).Count,Is.EqualTo(7));Assert.That(user["required"],Is.Null);Assert.That(user["properties"]["licenses"]["items"].Value<string>("$ref"),Does.EndWith("/License"));Assert.That(user["properties"]["profiles"]["items"].Value<string>("$ref"),Does.EndWith("/Profile"));Assert.That(schemas["License"]["properties"]["type"].Value<string>("type"),Is.EqualTo("string"));Assert.That(schemas["License"]["properties"]["validTill"].Value<string>("format"),Is.EqualTo("int64"));Assert.That(schemas["License"]["properties"]["validTill"].Value<bool>("readOnly"),Is.True);Assert.That(schemas["Profile"]["properties"]["organisation"].Value<string>("description"),Does.Contain("Instanz"));Assert.That(schemas["Profile"]["required"].Values<string>(),Is.EquivalentTo(new[]{"name","organisation"}));Assert.That(typeof(License).GetProperty("ValidTill").GetSetMethod(),Is.Null);
        }
        [Test]
        public void CurrentAccountFieldAuditUsesActualDeclaredTypesAndKinds()
        {
            var schema=JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"openscope-contact-contract.json"),Encoding.UTF8).TrimStart('\ufeff'))["components"]["schemas"];var count=0;
            foreach(var type in new[]{typeof(AccountInfo),typeof(Customer),typeof(Organisation),typeof(User),typeof(Teamwork),typeof(TokenResponse),typeof(License),typeof(Profile)})
            {
                var fields=type.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Select(p=>new{Property=p,Member=p.GetCustomAttribute<DataMemberAttribute>()}).Where(x=>x.Member!=null).ToDictionary(x=>x.Member.Name,x=>x.Property);
                foreach(var field in ((JObject)schema[type.Name]["properties"]).Properties())
                {
                    Assert.That(fields.ContainsKey(field.Name),Is.True,type.Name+"."+field.Name+" must be declared on its actual model,not a similarly named form.");var declared=fields[field.Name].PropertyType;var underlying=Nullable.GetUnderlyingType(declared)??declared;var kind=field.Value.Value<string>("type");
                    if(kind=="string")Assert.That(underlying==typeof(string)||underlying.IsEnum,Is.True,type.Name+"."+field.Name);
                    else if(kind=="integer")Assert.That(underlying,Is.EqualTo(typeof(long)),type.Name+"."+field.Name);
                    else if(kind=="boolean")Assert.That(underlying,Is.EqualTo(typeof(bool)),type.Name+"."+field.Name);
                    else if(kind=="array"){Assert.That(declared.GetGenericTypeDefinition(),Is.EqualTo(typeof(List<>)));Assert.That(declared.GetGenericArguments()[0].Name,Is.EqualTo(field.Value["items"].Value<string>("$ref").Split('/').Last()));}
                    else if(field.Value["$ref"]!=null)Assert.That(underlying.Name,Is.EqualTo(field.Value.Value<string>("$ref").Split('/').Last()));
                    count++;
                }
            }
            Assert.That(count,Is.EqualTo(34));
        }
        [TestCase("Licenses","licenses")][TestCase("Profiles","profiles")]
        public void ExactDataContractsAreOptional(string name,string wire){var a=typeof(User).GetProperty(name).GetCustomAttribute<DataMemberAttribute>();Assert.That(a.Name,Is.EqualTo(wire));Assert.That(a.EmitDefaultValue,Is.False);Assert.That(a.IsRequired,Is.False);}
        [Test]
        public void OptionalStateOrderDuplicatesAndExpiryRoundtrip([Values("absent","null","empty","populated")]string shape,[Values(long.MinValue,0L,long.MaxValue)]long expiry,[Values(false,true)]bool xml)
        {
            var model=Json(shape,expiry).ToObject<User>();User actual;if(xml){var serializer=new DataContractSerializer(typeof(User));using(var stream=new MemoryStream()){serializer.WriteObject(stream,model);stream.Position=0;actual=(User)serializer.ReadObject(stream);}}else actual=JObject.Parse(model.ToJson()).ToObject<User>();Assert.That(actual.Uid,Is.EqualTo("fixture-user"));var json=JObject.Parse(actual.ToJson());if(shape=="absent"||shape=="null"){Assert.That(actual.Licenses,Is.Null);Assert.That(actual.Profiles,Is.Null);Assert.That(json.ContainsKey("licenses"),Is.False);Assert.That(json.ContainsKey("profiles"),Is.False);}else if(shape=="empty"){Assert.That(actual.Licenses,Is.Empty);Assert.That(actual.Profiles,Is.Empty);Assert.That(json["licenses"].Type,Is.EqualTo(JTokenType.Array));Assert.That(json["profiles"].Type,Is.EqualTo(JTokenType.Array));}else{Assert.That(actual.Licenses.Select(x=>x.Type),Is.EqualTo(new[]{" original / license Ä "," second original "," original / license Ä "}));Assert.That(actual.Licenses[0].ValidTill,Is.EqualTo(expiry));Assert.That(actual.Licenses[1].ValidTill,Is.Null);Assert.That(actual.Licenses[2].ValidTill,Is.EqualTo(expiry));Assert.That(actual.Licenses[0].AssignedToUid,Is.EqualTo("unrelated-license-user"));Assert.That(actual.Licenses[0].AssignedToEmail,Is.EqualTo("original@example.invalid"));Assert.That(actual.Profiles.Select(x=>x.Name),Is.EqualTo(new[]{" original profile ","Firmen und Benutzer"," original profile "}));Assert.That(actual.Profiles.Select(x=>x.Organisation),Is.EqualTo(new[]{" original org name ","Instanz"," original org name "}));}
        }
        [Test]
        public void OriginalEqualityHashAndDiagnosticsDoNotBecomeScopeProof()
        {
            var original=Valid();var model=Json("populated").ToObject<User>();Assert.That(model.Equals(original),Is.True);Assert.That(model.GetHashCode(),Is.EqualTo(original.GetHashCode()));Assert.That(model.ToString(),Is.EqualTo(original.ToString()));Assert.That(JObject.Parse(model.ToJson())["licenses"],Is.Not.Null);model.Licenses=null;model.Profiles=null;Assert.That(model.ToJson(),Is.EqualTo(original.ToJson()));
        }
        private static JObject Account(JObject user)=>new JObject{["user"]=user,["organisation"]=new JObject{["id"]=7,["teamworkTenantId"]="fixture-tw"}};
        [Test]
        public async Task AllAccountReadFormsRetainListsRawFutureFieldsAndContext([Values(0,1,2)]int mode,[Values("absent","null","empty","populated")]string shape)
        {
            var user=Json(shape);if(shape=="populated")user["licenses"][0]["future"]=new JObject{["value"]=false};var body=Account(user).ToString();var handler=new Handler(body);using(var http=new HttpClient(handler)){var config=new Configuration{HttpClient=http,BasePath="https://account-metadata.invalid/rest",AccessToken="fixture-account-token",OrganisationId=7};var api=new AdditionalApi(config);var response=mode==0?api.GetApplicationContextWithHttpInfo():mode==1?await api.GetApplicationContextAsyncWithHttpInfo():await api.GetApplicationContextAsyncWithHttpInfo(CancellationToken.None);Assert.That(response.RawJsonContent,Is.EqualTo(body));Assert.That(response.StatusCode,Is.EqualTo(200));Assert.That(response.Headers["X-Account-Fixture"],Does.Contain("retained"));Assert.That(response.Data.User.Uid,Is.EqualTo("fixture-user"));Assert.That(response.Data.Organisation.Id,Is.EqualTo(7));Assert.That(response.Data.Organisation.TeamworkTenantId,Is.EqualTo("fixture-tw"));if(shape=="populated"){Assert.That(response.Data.User.Licenses.Count,Is.EqualTo(3));Assert.That(response.Data.User.Licenses[0].ValidTill,Is.EqualTo(long.MaxValue));Assert.That(response.Data.User.Profiles[1].Organisation,Is.EqualTo("Instanz"));}else if(shape=="empty"){Assert.That(response.Data.User.Licenses,Is.Empty);Assert.That(response.Data.User.Profiles,Is.Empty);}else{Assert.That(response.Data.User.Licenses,Is.Null);Assert.That(response.Data.User.Profiles,Is.Null);}Assert.That(config.BasePath,Is.EqualTo("https://account-metadata.invalid/rest"));Assert.That(config.AccessToken,Is.EqualTo("fixture-account-token"));Assert.That(config.OrganisationId,Is.EqualTo(7));Assert.That(handler.Calls,Is.EqualTo(1));}
        }
        [Test]
        public async Task InstanceProfilesAndLicensesCannotOverrideFreshIdentity([Values(0,1,2)]int mode,[Values("ok","wrong-organisation","wrong-user")]string shape)
        {
            var user=Json("populated");if(shape=="wrong-user")user["uid"]="other-user";var account=Account(user);if(shape=="wrong-organisation")account["organisation"]["id"]=8;var handler=new Handler(account.ToString(),true);using(var http=new HttpClient(handler)){var config=new Configuration{HttpClient=http,BasePath="https://account-metadata.invalid/rest",AccessToken="fixture-prior",OrganisationId=7,ClientNumber="fixture-customer",Username="fixture-login",Password="fixture-password"};var client=new OpenScopeApiClient(config);var prior=new TokenResponse(TokenResponse.TokenTypeEnum.Bearer,"fixture-prior",123,"fixture-refresh","fixture-user",7,"fixture-org","fixture-tw");client.Token=prior;Func<Task> authorize=()=>{if(mode==0){client.AuthorizeWithUserCredentials();return Task.CompletedTask;}return mode==1?client.AuthorizeWithUserCredentialsAsync():client.AuthorizeWithUserCredentialsAsync(CancellationToken.None);};if(shape=="ok"){await authorize();Assert.That(config.AccessToken,Is.EqualTo("fixture-candidate"));Assert.That(client.Token.Uid,Is.EqualTo("fixture-user"));}else{Assert.ThrowsAsync<InvalidDataException>((Func<Task>)(async()=>await authorize()));Assert.That(client.Token,Is.SameAs(prior));Assert.That(config.AccessToken,Is.EqualTo("fixture-prior"));}Assert.That(config.OrganisationId,Is.EqualTo(7));Assert.That(handler.Calls,Is.EqualTo(2));}
        }
        private sealed class Handler:HttpMessageHandler
        {
            private readonly string account;private readonly bool authorization;internal int Calls;internal Handler(string account,bool authorization=false){this.account=account;this.authorization=authorization;}
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellation)
            {
                Assert.That(request.RequestUri.Host,Is.EqualTo("account-metadata.invalid"));Calls++;string body;if(request.RequestUri.AbsolutePath=="/rest/token"){Assert.That(authorization,Is.True);Assert.That(request.Method,Is.EqualTo(HttpMethod.Post));Assert.That(await request.Content.ReadAsStringAsync(),Does.Contain("organisation_id=7"));body="{\"token_type\":\"Bearer\",\"access_token\":\"fixture-candidate\",\"refresh_token\":\"fixture-refresh\",\"expires_in\":123,\"uid\":\"fixture-user\",\"organisationId\":7,\"organisationName\":\"fixture-org\"}";}else{Assert.That(request.RequestUri.AbsolutePath,Is.EqualTo("/rest/myaccount"));Assert.That(request.Method,Is.EqualTo(HttpMethod.Get));Assert.That(request.Content,Is.Null);Assert.That(request.RequestUri.Query,Is.Empty);Assert.That(request.Headers.Authorization?.Parameter,Is.EqualTo(authorization?"fixture-candidate":"fixture-account-token"));body=account;}var response=new HttpResponseMessage(HttpStatusCode.OK){Content=new StringContent(body,Encoding.UTF8,"application/json")};response.Headers.TryAddWithoutValidation("X-Account-Fixture","retained");return response;
            }
        }
    }
}
