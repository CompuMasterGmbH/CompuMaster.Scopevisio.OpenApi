using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class BusinessReadContractTests
    {
        private const string SkuJson = "{\"records\":[{\"id\":9223372036854775807,\"organisation\":9223372036854775807,\"productNumber\":\"00001\",\"ecommerceAccount\":9223372036854775807,\"ecommerceAccountDisplay\":\"Ä / account\",\"platformNumber\":\"00002\",\"fraction\":123456789.123456789,\"date\":\"2026-10-12T01:02:03Z\",\"unknown\":null}],\"future\":false}";

        [Test]
        public void NewFacadeGroupsShareConstructorConfigurationWithoutChangingExistingGroups()
        {
            var handler = new Handler("{}");
            using (var http = new HttpClient(handler))
            {
                var config = Config(http);
                var client = new OpenScopeApiClient(config);
                Assert.That(client.Config, Is.SameAs(config));
                Assert.That(client.CampaignApi.Configuration, Is.SameAs(config));
                Assert.That(client.ReportsheetApi.Configuration, Is.SameAs(config));
                Assert.That(client.ProductApi.Configuration, Is.SameAs(config));
                Assert.That(client.AuthorizationApi.Configuration, Is.SameAs(config));
                var selected = new CampaignApi(new Configuration { BasePath = "https://other.invalid/rest" });
                client.CampaignApi = selected;
                Assert.That(client.CampaignApi, Is.SameAs(selected));
                Assert.That(client.Config, Is.SameAs(config)); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void GroupConstructorsRetainExistingDefaultAndOriginConventions()
        {
            Assert.That(new CampaignApi().Configuration, Is.SameAs(Configuration.Default));
            Assert.That(new ReportsheetApi().Configuration, Is.SameAs(Configuration.Default));
            Assert.That(new CampaignApi("https://campaign.invalid/rest").Configuration.BasePath, Is.EqualTo("https://campaign.invalid/rest"));
            Assert.That(new ReportsheetApi("https://reportsheet.invalid/rest").Configuration.BasePath, Is.EqualTo("https://reportsheet.invalid/rest"));
        }

        [Test]
        public void NewGroupExceptionHooksRejectMulticastDelegates()
        {
            ExceptionFactory first = (name, response) => null, second = (name, response) => null;
            var campaign = new CampaignApi { ExceptionFactory = first + second };
            var reportsheet = new ReportsheetApi { ExceptionFactory = first + second };
            Assert.Throws<InvalidOperationException>((Action)(() => { var factory = campaign.ExceptionFactory; }));
            Assert.Throws<InvalidOperationException>((Action)(() => { var factory = reportsheet.ExceptionFactory; }));
        }

        [Test]
        public async Task ExplicitNullHookRetainsCallerSelectedClassificationPolicy()
        {
            var handler = new Handler("{\"error\":\"server denied\"}", HttpStatusCode.Forbidden);
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http));
                client.CampaignApi.ExceptionFactory = null;
                var info = await client.CampaignApi.SearchCampaignsAsyncWithHttpInfo(null, CancellationToken.None);
                Assert.That(info.StatusCode, Is.EqualTo(403)); Assert.That(info.Data.Value<string>("error"), Is.EqualTo("server denied"));
            }
        }

        [Test]
        public void InvalidReportsheetNumbersFailBeforeHttp([Values(null, "", " ", ".", "..")] string number, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler("{}");
            using (var http = new HttpClient(handler))
            {
                var api = new ReportsheetApi(Config(http));
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    if (asynchronous) await api.GetReportsheetByNumberAsyncWithHttpInfo(number, null, CancellationToken.None);
                    else api.GetReportsheetByNumberWithHttpInfo(number);
                }));
                Assert.That(error.ParamName, Is.EqualTo("number")); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task EmptyReadFieldsAreSentExplicitly([Values(false, true)] bool sku)
        {
            var handler = new Handler("{}");
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http));
                if (sku) await client.ProductApi.GetECommerceSkusAsyncWithHttpInfo(long.MinValue, "", CancellationToken.None);
                else await client.ReportsheetApi.GetReportsheetByNumberAsyncWithHttpInfo("00001", "", CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.EqualTo("?fields="));
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(sku ? "/rest/product/-9223372036854775808/ecommerceskus" : "/rest/reportsheet/00001"));
            }
        }

        [Test]
        public async Task SearchPagingAndFutureNullFiltersAreNotClamped([Values(0, 1, 2)] int route)
        {
            var handler = new Handler("{}");
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http));
                var query = new SearchRequest { Page = -1, PageSize = 1001, FormatValues = false, Fields = new[] { "id" }, CountOnly = true };
                query["future"] = JValue.CreateNull();
                if (route == 0) await client.CampaignApi.SearchCampaignsAsyncWithHttpInfo(query, CancellationToken.None);
                else if (route == 1) await client.CampaignApi.SearchCampaignRunEntriesAsyncWithHttpInfo(long.MinValue, query, CancellationToken.None);
                else await client.ReportsheetApi.SearchReportsheetsAsyncWithHttpInfo(query, CancellationToken.None);
                var body = JObject.Parse(handler.Body);
                Assert.That(body.Value<int>("page"), Is.EqualTo(-1)); Assert.That(body.Value<int>("pageSize"), Is.EqualTo(1001));
                Assert.That(body.Value<bool>("formatValues"), Is.False); Assert.That(body.Value<bool>("count"), Is.True);
                Assert.That(body["future"].Type, Is.EqualTo(JTokenType.Null)); Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task UnspecifiedSearchAndReportsheetShapesStayComplete([Values(0, 1, 2, 3)] int route, [Values("[]", "1", "true", "\"text\"")] string json)
        {
            var handler = new Handler(json);
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http)); ApiResponse<JToken> info;
                if (route == 0) info = await client.CampaignApi.SearchCampaignsAsyncWithHttpInfo(null, CancellationToken.None);
                else if (route == 1) info = await client.CampaignApi.SearchCampaignRunEntriesAsyncWithHttpInfo(1, null, CancellationToken.None);
                else if (route == 2) info = await client.ReportsheetApi.SearchReportsheetsAsyncWithHttpInfo(null, CancellationToken.None);
                else info = await client.ReportsheetApi.GetReportsheetByNumberAsyncWithHttpInfo("00001", null, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json)); Assert.That(info.Data.ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public void MissingECommerceSettingsRetainOriginalServerDiagnostic()
        {
            const string diagnostic = "The option to display extended product fields must be set to true in the e-Commerce settings.";
            var handler = new Handler(diagnostic, HttpStatusCode.BadRequest);
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http));
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await client.ProductApi.GetECommerceSkusAsyncWithHttpInfo(1, null, CancellationToken.None); }));
                Assert.That(error.ErrorCode, Is.EqualTo(400)); Assert.That((object)error.ErrorContent, Is.EqualTo(diagnostic));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void SkuResponsesRejectIncompatibleShapes([Values("[]", "1", "{\"records\":1}", "{\"records\":{}}", "{\"records\":[null]}")] string json)
        {
            var handler = new Handler(json);
            using (var http = new HttpClient(handler))
                Assert.ThrowsAsync<JsonSerializationException>((Func<Task>)(async () => { await new ProductApi(Config(http)).GetECommerceSkusAsyncWithHttpInfo(1, null, CancellationToken.None); }));
        }

        [Test]
        public void TypedSkuCopiesRetainIdentifiersNumbersUnknownsAndEmptyMetadata()
        {
            var source = JsonConvert.DeserializeObject<JObject>(SkuJson, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None, FloatParseHandling = FloatParseHandling.Decimal });
            var envelope = new ECommerceSkusResponse(source); source["future"] = true;
            Assert.That(envelope.Value<bool>("future"), Is.False);
            var record = envelope.Records[0];
            Assert.That(record.Id, Is.EqualTo(long.MaxValue)); Assert.That(record.Organisation, Is.EqualTo(long.MaxValue)); Assert.That(record.ECommerceAccount, Is.EqualTo(long.MaxValue));
            Assert.That(record.ProductNumber, Is.EqualTo("00001")); Assert.That(record.PlatformNumber, Is.EqualTo("00002")); Assert.That(record.ECommerceAccountDisplay, Is.EqualTo("Ä / account"));
            Assert.That(record["unknown"].Type, Is.EqualTo(JTokenType.Null)); Assert.That(record["date"].Type, Is.EqualTo(JTokenType.String));
            Assert.That(record.Value<decimal>("fraction"), Is.EqualTo(123456789.123456789m));
            record["id"] = 0; Assert.That(envelope.Records[0].Id, Is.EqualTo(long.MaxValue));
            Assert.That(((ICollection<ECommerceSkuRecord>)envelope.Records).IsReadOnly, Is.True);
            Assert.That(new ECommerceSkusResponse(new JObject()).Records, Is.Null);
            Assert.That(new ECommerceSkusResponse(new JObject { ["records"] = JValue.CreateNull() }).Records, Is.Null);
            Assert.That(new ECommerceSkusResponse(new JObject { ["records"] = new JArray() }).Records, Is.Empty);
            var missing = new ECommerceSkuRecord(new JObject());
            Assert.That(missing.Id, Is.Null); Assert.That(missing.Organisation, Is.Null); Assert.That(missing.ECommerceAccount, Is.Null);
            Assert.That(missing.ProductNumber, Is.Null); Assert.That(missing.PlatformNumber, Is.Null); Assert.That(missing.ECommerceAccountDisplay, Is.Null);
        }

        [Test]
        public async Task PublicFacadeNativeConvenienceMethodsCompileAndRetainResults()
        {
            var handler = new Handler(SkuJson);
            using (var http = new HttpClient(handler))
            {
                var client = new OpenScopeApiClient(Config(http)); var query = new SearchRequest();
                Assert.That((await client.ProductApi.GetECommerceSkusAsync(1, null, CancellationToken.None)).Records[0].ProductNumber, Is.EqualTo("00001"));
                Assert.That(await client.CampaignApi.SearchCampaignsAsync(query, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That(await client.CampaignApi.SearchCampaignRunEntriesAsync(1, query, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That(await client.ReportsheetApi.SearchReportsheetsAsync(query, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That(await client.ReportsheetApi.GetReportsheetByNumberAsync("00001", null, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That(handler.Calls, Is.EqualTo(5));
            }
        }

        private static Configuration Config(HttpClient http) => new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-business-contract-token" };
        private sealed class Handler : HttpMessageHandler
        {
            private readonly string body; private readonly HttpStatusCode status;
            internal int Calls; internal Uri Uri; internal string Body;
            internal Handler(string body, HttpStatusCode status = HttpStatusCode.OK) { this.body = body; this.status = status; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            }
        }
    }
}
