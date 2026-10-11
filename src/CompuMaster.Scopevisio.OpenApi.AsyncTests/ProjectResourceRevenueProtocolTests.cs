using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ProjectResourceRevenueProtocolTests
    {
        private static readonly int[] Routes = { 0, 1, 2, 3 };
        private static readonly string[] Names = { "GetProjectResourceDetails", "GetProjectRevenueDetails", "SearchProjectResources", "SearchProjectRevenues" };
        private static readonly string[] Paths = { "/rest/project/9223372036854775807/projectresources", "/rest/project/9223372036854775807/projectrevenue", "/rest/projectresources/v2", "/rest/projectrevenue/v2" };
        private const string ResourceJson = "{\"records\":[{\"id\":9223372036854775807,\"organisation\":4294967296,\"projectId\":123,\"projectName\":null,\"projectNumber\":\"P1\",\"humanResourceId\":234,\"humanResourceDisplay\":\"Person\",\"projectRole\":\"Role\",\"projectPosition\":\"Position\",\"availableDays\":30,\"bookedDays\":5,\"bookedRatio\":0.1234567890123456789,\"allocationStatusDisplay\":\"Allocated\",\"fee\":123456789.123456789,\"month\":\"2026-10\",\"monthDays\":31,\"workDays\":22,\"availableMonthDays\":17,\"unknown\":null}],\"unknownMetadata\":{\"next\":true}}";
        private const string RevenueJson = "{\"records\":[{\"id\":9223372036854775807,\"organisation\":4294967296,\"revenueType\":4294967296,\"projectId\":123,\"projectName\":null,\"projectNumber\":\"P1\",\"revenueGroup\":\"Future group\",\"revenue\":123456789.123456789,\"extraRevenue\":0.1234567890123456789,\"automaticRow\":false,\"averageFee\":25.25,\"revenueStatusDisplay\":\"Planned\",\"month\":\"2026-10\",\"monthRevenue\":999.99,\"unknown\":null}],\"unknownMetadata\":{\"next\":true}}";

        [Test]
        public async Task CurrentRoutesRetainTypedRecordsAndPreciseWireData([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            string json = IsRevenue(route) ? RevenueJson : ResourceJson;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var transport = new HttpClient(handler))
            {
                var response = await Invoke(route, mode, new ProjectApi(ConfigurationFor(transport)), ValidQuery(route), "id,fee & name", ProjectRevenueType.Realization, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(route < 2 ? HttpMethod.Get : HttpMethod.Post));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-fixture-token"));
                if (route < 2)
                {
                    Assert.That(string.IsNullOrEmpty(handler.Body), Is.True);
                    Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(route == 0 ? "?fields=id,fee & name" : "?fields=id,fee & name&revenueType=1"));
                }
                else
                {
                    var body = JObject.Parse(handler.Body);
                    Assert.That(body.Value<string>("projectStartFrom"), Is.EqualTo("2026-01-01"));
                    Assert.That(body.Value<string>("projectStartTo"), Is.EqualTo("31.12.2026"));
                    Assert.That(body["searchSpecification"].Value<int>("page"), Is.EqualTo(2));
                    Assert.That(body["searchSpecification"].Value<bool>("formatValues"), Is.False);
                    if (route == 3) Assert.That(body.Value<int>("revenueType"), Is.EqualTo(2));
                }
                Assert.That((int)response.GetType().GetProperty("StatusCode").GetValue(response), Is.EqualTo(200));
                Assert.That((string)response.GetType().GetProperty("RawJsonContent").GetValue(response), Is.EqualTo(json));
                var data = (JObject)response.GetType().GetProperty("Data").GetValue(response);
                Assert.That(data["unknownMetadata"].Value<bool>("next"), Is.True);
                if (IsRevenue(route))
                {
                    var record = ((ProjectRevenuesResponse)data).Records[0];
                    Assert.That(record.Id, Is.EqualTo(long.MaxValue));
                    Assert.That(record.Organisation, Is.EqualTo(4294967296L));
                    Assert.That(record.RevenueType, Is.EqualTo(4294967296L));
                    Assert.That(record.Revenue, Is.EqualTo(123456789.123456789m));
                    Assert.That(record.ExtraRevenue, Is.EqualTo(0.1234567890123456789m));
                    Assert.That(record.AutomaticRow, Is.False);
                    Assert.That(record.AverageFee, Is.EqualTo(25.25m));
                    Assert.That(record.MonthRevenue, Is.EqualTo(999.99m));
                    Assert.That(record.Month, Is.EqualTo("2026-10"));
                    Assert.That(record.ProjectName, Is.Null);
                    Assert.That(record["unknown"].Type, Is.EqualTo(JTokenType.Null));
                }
                else
                {
                    var record = ((ProjectResourcesResponse)data).Records[0];
                    Assert.That(record.Id, Is.EqualTo(long.MaxValue));
                    Assert.That(record.Organisation, Is.EqualTo(4294967296L));
                    Assert.That(record.Fee, Is.EqualTo(123456789.123456789m));
                    Assert.That(record.BookedRatio, Is.EqualTo(0.1234567890123456789m));
                    Assert.That(record.WorkDays, Is.EqualTo(22));
                    Assert.That(record.Month, Is.EqualTo("2026-10"));
                    Assert.That(record.ProjectName, Is.Null);
                    Assert.That(record["unknown"].Type, Is.EqualTo(JTokenType.Null));
                }
            }
        }

        [Test]
        public void RequestModelsRetainOmissionExplicitNullAndCopiedSpecifications()
        {
            var resource = new ProjectResourcesSearchRequest();
            Assert.That(resource.ToString(Formatting.None), Is.EqualTo("{}"));
            var specification = new SearchRequest { Page = 2, FormatValues = false };
            resource.SearchSpecification = specification;
            specification.Page = 9;
            Assert.That(resource.SearchSpecification.Page, Is.EqualTo(2));
            var getterCopy = resource.SearchSpecification;
            getterCopy.Page = 7;
            Assert.That(resource.SearchSpecification.Page, Is.EqualTo(2));
            resource.SearchSpecification = null;
            Assert.That(resource.Property("searchSpecification"), Is.Null);
            resource["searchSpecification"] = JValue.CreateNull();
            var copy = new ProjectResourcesSearchRequest(resource);
            Assert.That(copy["searchSpecification"].Type, Is.EqualTo(JTokenType.Null));
            var revenue = new ProjectRevenuesSearchRequest();
            Assert.That(revenue.RevenueType, Is.Null);
            revenue.RevenueType = ProjectRevenueType.Planning;
            Assert.That(revenue.Value<int>("revenueType"), Is.Zero);
            revenue.RevenueType = null;
            Assert.That(revenue.Property("revenueType"), Is.Null);
            var unknown = new ProjectRevenuesSearchRequest(new JObject { ["future"] = 42 });
            Assert.That(unknown.Value<int>("future"), Is.EqualTo(42));
        }

        [Test]
        public async Task OptionalGetQueriesAreOmittedAndPlanningIsNotForced([Values(0, 1)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{\"records\":[]}")));
            using (var transport = new HttpClient(handler))
            {
                await Invoke(route, 1, new ProjectApi(ConfigurationFor(transport)), null, null, null, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.Empty);
                await Invoke(route, 2, new ProjectApi(ConfigurationFor(transport)), null, "", ProjectRevenueType.Planning, CancellationToken.None);
                Assert.That(handler.Uri.Query, Is.EqualTo(route == 0 ? "?fields=" : "?fields=&revenueType=0"));
            }
        }

        [Test]
        public async Task MissingRequiredDateBoundsFailBeforeHttp([Values(2, 3)] int route, [Values(false, true)] bool missingQuery)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{\"records\":[]}")));
            using (var transport = new HttpClient(handler))
            {
                object query = missingQuery ? null : route == 2 ? (object)new ProjectResourcesSearchRequest { ProjectStartFrom = "2026-01-01" } : new ProjectRevenuesSearchRequest { ProjectStartFrom = "2026-01-01" };
                Exception actual = null;
                try { await Invoke(route, 2, new ProjectApi(ConfigurationFor(transport)), query, null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.InstanceOf<ArgumentException>());
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task UnsupportedRevenueTypesFailBeforeHttp([Values(1, 3)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{\"records\":[]}")));
            using (var transport = new HttpClient(handler))
            {
                var query = ValidQuery(route);
                if (query is ProjectRevenuesSearchRequest revenue) revenue.RevenueType = (ProjectRevenueType)3;
                Exception actual = null;
                try { await Invoke(route, 0, new ProjectApi(ConfigurationFor(transport)), query, null, (ProjectRevenueType)3, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [TestCase("{\"records\":[]}", false, 0)]
        [TestCase("{}", true, 0)]
        [TestCase("{\"records\":null}", true, 0)]
        public void TypedEnvelopesDistinguishAbsentNullAndEmptyRecords(string json, bool missing, int count)
        {
            var resources = new ProjectResourcesResponse(JObject.Parse(json));
            var revenues = new ProjectRevenuesResponse(JObject.Parse(json));
            Assert.That(resources.Records == null, Is.EqualTo(missing));
            Assert.That(revenues.Records == null, Is.EqualTo(missing));
            if (!missing)
            {
                Assert.That(resources.Records.Count, Is.EqualTo(count));
                Assert.That(revenues.Records.Count, Is.EqualTo(count));
            }
        }

        [Test]
        public async Task MalformedEnvelopeShapesAreRejected([Values(0, 3)] int route, [Values("[]", "{\"records\":{}}", "{\"records\":[null]}")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var transport = new HttpClient(handler))
            {
                Exception actual = null;
                try { await Invoke(route, 2, new ProjectApi(ConfigurationFor(transport)), ValidQuery(route), null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<JsonSerializationException>());
            }
        }

        [Test]
        public async Task ServerAndCustomErrorsPrecedeTypedParsing([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "permission denied")));
            using (var transport = new HttpClient(handler))
            {
                var api = new ProjectApi(ConfigurationFor(transport));
                Exception actual = null;
                try { await Invoke(route, mode, api, ValidQuery(route), null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ApiException>());
                Assert.That(((ApiException)actual).ErrorCode, Is.EqualTo(403));
                var expected = new InvalidOperationException("isolated project factory");
                api.ExceptionFactory = (name, response) => {
                    Assert.That(name, Is.EqualTo(Names[route]));
                    return expected;
                };
                actual = null;
                try { await Invoke(route, mode, api, ValidQuery(route), null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationStopsAllFourRoutesBeforeHttp([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => {
                    await Invoke(route, 2, new ProjectApi(ConfigurationFor(transport)), ValidQuery(route), null, null, cancellation.Token);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesEveryProjectReadRoute([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => {
                entered.SetResult(true);
                await Task.Delay(Timeout.Infinite, token);
                return Response(HttpStatusCode.OK, "{}");
            });
            using (var transport = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var config = ConfigurationFor(transport);
                var pending = Invoke(route, 2, new ProjectApi(config), ValidQuery(route), null, null, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, pending, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(pending, Task.Delay(5000)), Is.SameAs(pending));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await pending; }));
                    Assert.That(config.AccessToken, Is.EqualTo("isolated-fixture-token"));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public async Task ImportedRevenueTokensAreValidatedBeforeHttp(
            [Values("2.1", "-0.1", "true", "false", "\"2\"", "3", "-1", "4294967296", "9999999999999999999999999999999999999999", "{}", "[]")] string json,
            [Values(false, true)] bool copyConstructor, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, RevenueJson)));
            using (var transport = new HttpClient(handler))
            {
                var source = new JObject { ["projectStartFrom"] = "2026-01-01", ["projectStartTo"] = "2026-12-31", ["revenueType"] = JToken.Parse(json) };
                var query = copyConstructor ? new ProjectRevenuesSearchRequest(source) :
                    new ProjectRevenuesSearchRequest { ProjectStartFrom = "2026-01-01", ProjectStartTo = "2026-12-31" };
                if (!copyConstructor) query["revenueType"] = JToken.Parse(json);
                Exception actual = null;
                try { await Invoke(3, mode, new ProjectApi(ConfigurationFor(transport)), query, null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentOutOfRangeException>(), "A raw non-integer token must not be rounded or coerced for validation.");
                Assert.That(handler.Calls, Is.Zero);
                Assert.That(query["revenueType"].ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public async Task RawDateBoundsMustBeStringsBeforeHttp([Values(2, 3)] int route,
            [Values("20260101", "true")] string json, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, IsRevenue(route) ? RevenueJson : ResourceJson)));
            using (var transport = new HttpClient(handler))
            {
                var query = (JObject)ValidQuery(route);
                query["projectStartFrom"] = JToken.Parse(json);
                Exception actual = null;
                try { await Invoke(route, mode, new ProjectApi(ConfigurationFor(transport)), query, null, null, CancellationToken.None); }
                catch (Exception error) { actual = error; }
                Assert.That(actual, Is.TypeOf<ArgumentException>(), "A raw date value must not be coerced to a string only for validation.");
                Assert.That(handler.Calls, Is.Zero);
                Assert.That(query["projectStartFrom"].ToString(Formatting.None), Is.EqualTo(json));
            }
        }

        [Test]
        public async Task RawRevenueOmissionAndExplicitNullRemainCallerControlled([Values(false, true)] bool explicitNull, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, RevenueJson)));
            using (var transport = new HttpClient(handler))
            {
                var query = new ProjectRevenuesSearchRequest { ProjectStartFrom = "2026-01-01", ProjectStartTo = "2026-12-31" };
                if (explicitNull) query["revenueType"] = JValue.CreateNull();
                Assert.That(query.RevenueType, Is.Null);
                await Invoke(3, mode, new ProjectApi(ConfigurationFor(transport)), query, null, null, CancellationToken.None);
                var sent = JObject.Parse(handler.Body);
                if (explicitNull) Assert.That(sent["revenueType"].Type, Is.EqualTo(JTokenType.Null));
                else Assert.That(sent.Property("revenueType"), Is.Null);
            }
        }

        private static bool IsRevenue(int route) { return route == 1 || route == 3; }
        private static object ValidQuery(int route)
        {
            var search = new SearchRequest { Page = 2, FormatValues = false };
            if (route == 2) return new ProjectResourcesSearchRequest { ProjectStartFrom = "2026-01-01", ProjectStartTo = "31.12.2026", SearchSpecification = search };
            if (route == 3) return new ProjectRevenuesSearchRequest { ProjectStartFrom = "2026-01-01", ProjectStartTo = "31.12.2026", SearchSpecification = search, RevenueType = ProjectRevenueType.Deviation };
            return null;
        }

        private static async Task<object> Invoke(int route, int mode, ProjectApi api, object query, string fields, ProjectRevenueType? revenueType, CancellationToken token)
        {
            string name = Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo");
            Type[] types;
            object[] values;
            if (route == 0) { types = new[] { typeof(long), typeof(string) }; values = new object[] { long.MaxValue, fields }; }
            else if (route == 1) { types = new[] { typeof(long), typeof(string), typeof(ProjectRevenueType?) }; values = new object[] { long.MaxValue, fields, revenueType }; }
            else { types = new[] { route == 2 ? typeof(ProjectResourcesSearchRequest) : typeof(ProjectRevenuesSearchRequest) }; values = new[] { query }; }
            if (mode == 2)
            {
                Array.Resize(ref types, types.Length + 1); types[types.Length - 1] = typeof(CancellationToken);
                Array.Resize(ref values, values.Length + 1); values[values.Length - 1] = token;
            }
            object result;
            try { result = typeof(ProjectApi).GetMethod(name, types).Invoke(api, values); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (mode == 0) return result;
            await (Task)result;
            return result.GetType().GetProperty("Result").GetValue(result);
        }

        private static Configuration ConfigurationFor(HttpClient transport)
        {
            return new Configuration { HttpClient = transport, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-fixture-token" };
        }

        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        }

        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++;
                Uri = request.RequestUri; Method = request.Method;
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Authorization = request.Headers.Authorization?.ToString();
                return await send(token);
            }
        }
    }
}
