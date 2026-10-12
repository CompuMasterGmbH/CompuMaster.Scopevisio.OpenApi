using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;
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
    public class ConfigurableFinancialReportTests
    {
        private static readonly int[] Routes = Enumerable.Range(0, 2).ToArray();
        private static readonly int[] DatedRoutes = new[] { 0 };
        private static readonly string[] Names = { "GetGeneralJournalDataSource", "GetFinancialReportDataSource" };
        private static readonly string[] Paths = { "/rest/datasource/journal", "/rest/datasource/proReport" };
        private const string Json = "{\"records\":[{\"id\":9223372036854775807,\"amount\":123456789.123456789,\"unknown\":null,\"when\":\"2026-10-12T01:02:03Z\"}],\"more\":false}";

        [Test]
        public async Task ConfigurableReportsUseExactGetQueryContracts([ValueSource(nameof(Routes))] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, mode, true, CancellationToken.None);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo(Paths[route]));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-configurable-report-token"));
                Assert.That(handler.Accept, Is.EqualTo("application/json"));
                var query = Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " "));
                Assert.That(query, Is.EqualTo(route == 0 ? "?startDate=29.02.2024&endDate=01.03.2024&reportName= Layout / Ä & custom &unassigned=false&forcePersonalEntries=true&hideCancellationEntries=false&dimension1=true&dimension2=false&dimension3=true&dimension4=false&dimension5=true&dimension6=false&dimension7=true&dimension8=false&dimension9=true&dimension10=false&accountingEntityNumbers=00001,00002 & custom&fields=id,amount & custom" : "?month=2&year=-2024&definitionType=9223372036854775807&definitionName= Definition / Ä & custom &layoutName= Layout / Ä & custom &budgetGroupName= Budget & custom &organisations= Org / Ä & custom &showAccounts=false&showGuvAccounts=true&showEmptyRows=false&showLineNumbers=true&useForeignLanguage=false&amountSuffix=NoSuffix&amountFormatting=DivideByThousand&percentFormatting=OneDecimalDigit"));
                Assert.That(info.StatusCode, Is.EqualTo(200));
                Assert.That(info.RawJsonContent, Is.EqualTo(Json));
                Assert.That(info.Headers["X-Fixture"], Is.EqualTo("first,second"));
                Assert.That(info.Data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
                Assert.That(info.Data["records"][0].Value<decimal>("amount"), Is.EqualTo(123456789.123456789m));
                Assert.That(info.Data["records"][0]["unknown"].Type, Is.EqualTo(JTokenType.Null));
                Assert.That(info.Data["records"][0]["when"].Type, Is.EqualTo(JTokenType.String));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task OptionalValuesAreOmittedWithoutInventingPaging([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "[]")));
            using (var http = new HttpClient(handler))
            {
                await Invoke(Api(http), route, 1, false, CancellationToken.None);
                Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(RequiredQuery(route)));
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ExplicitEmptyOptionalFiltersRemainObservable([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[args.Length - 1] = Options(route, false);
                foreach (var property in args[args.Length - 1].GetType().GetProperties().Where(p => p.PropertyType == typeof(string))) property.SetValue(args[args.Length - 1], "");
                args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, true));
                var suffix = route == 0 ? "&reportName=&accountingEntityNumbers=&fields=" : "&budgetGroupName=&organisations=&amountSuffix=&amountFormatting=&percentFormatting=";
                Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(RequiredQuery(route) + suffix));
            }
        }

        [Test]
        public void InvalidRequiredDatesFailBeforeHttp([ValueSource(nameof(DatedRoutes))] int route,
            [Values(false, true)] bool end, [Values(null, "", " ", "29.02.2023", "2024-02-29", "31.04.2024")] string date, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[end ? 1 : 0] = date;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
                }));
                Assert.That(error.ParamName, Is.EqualTo(end ? "endDate" : "startDate"));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ValidDateOrderIsSentWithoutAnUndocumentedClientRule([ValueSource(nameof(DatedRoutes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[0] = "01.03.2024"; args[1] = "29.02.2024";
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                Assert.That(handler.Uri.Query, Does.StartWith("?startDate=01.03.2024&endDate=29.02.2024"));
            }
        }

        [Test]
        public async Task UnspecifiedReplyShapesAndNullAreRetained([ValueSource(nameof(Routes))] int route, [Values("{}", "[]", "null", "")] string json)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, json)));
            using (var http = new HttpClient(handler))
            {
                var info = await Invoke(Api(http), route, 2, false, CancellationToken.None);
                Assert.That(info.RawJsonContent, Is.EqualTo(json));
                if (json == "") Assert.That(info.Data, Is.Null);
                else Assert.That(info.Data.Type, Is.EqualTo(json == "{}" ? JTokenType.Object : json == "[]" ? JTokenType.Array : JTokenType.Null));
            }
        }

        [Test]
        public async Task ServerProfileFailuresAndCustomHookExceptionsArePreserved([ValueSource(nameof(Routes))] int route, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, "required reporting profile missing")));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var actual = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => { await Invoke(api, route, mode, false, CancellationToken.None); }));
                Assert.That(actual.ErrorCode, Is.EqualTo(403));
                Assert.That((object)actual.ErrorContent, Is.EqualTo("required reporting profile missing"));
                var expected = new InvalidOperationException("custom reporting hook");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                Assert.That(Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Invoke(api, route, mode, false, CancellationToken.None);
                })), Is.SameAs(expected));
            }
        }

        [Test]
        public void PreCancellationPreventsEveryDataSourceRequest([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await Invoke(Api(http), route, 2, false, cancellation.Token); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesEveryDataSourceTransport([ValueSource(nameof(Routes))] int route)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, "{}"); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Invoke(Api(http), route, 2, false, cancellation.Token);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    cancellation.Cancel();
                    Assert.That(await Task.WhenAny(operation, Task.Delay(5000)), Is.SameAs(operation));
                    Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => { await operation; }));
                    Assert.That(handler.Calls, Is.EqualTo(1));
                }
                finally { cancellation.Cancel(); }
            }
        }

        [Test]
        public async Task ConvenienceMethodsExposeCompleteJson([ValueSource(nameof(Routes))] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var data = (JToken)await Call(Api(http), Names[route] + (asynchronous ? "Async" : ""), Arguments(route, false), Types(route, false));
                Assert.That(data["records"][0].Value<long>("id"), Is.EqualTo(long.MaxValue));
            }
        }



        private static readonly object[] BooleanCases = {
            new TestCaseData(0, "Unassigned", "unassigned", false), new TestCaseData(0, "Unassigned", "unassigned", true),
            new TestCaseData(0, "ForcePersonalEntries", "forcePersonalEntries", false), new TestCaseData(0, "ForcePersonalEntries", "forcePersonalEntries", true),
            new TestCaseData(0, "HideCancellationEntries", "hideCancellationEntries", false), new TestCaseData(0, "HideCancellationEntries", "hideCancellationEntries", true),
            new TestCaseData(0, "Dimension1", "dimension1", false), new TestCaseData(0, "Dimension1", "dimension1", true),
            new TestCaseData(0, "Dimension2", "dimension2", false), new TestCaseData(0, "Dimension2", "dimension2", true),
            new TestCaseData(0, "Dimension3", "dimension3", false), new TestCaseData(0, "Dimension3", "dimension3", true),
            new TestCaseData(0, "Dimension4", "dimension4", false), new TestCaseData(0, "Dimension4", "dimension4", true),
            new TestCaseData(0, "Dimension5", "dimension5", false), new TestCaseData(0, "Dimension5", "dimension5", true),
            new TestCaseData(0, "Dimension6", "dimension6", false), new TestCaseData(0, "Dimension6", "dimension6", true),
            new TestCaseData(0, "Dimension7", "dimension7", false), new TestCaseData(0, "Dimension7", "dimension7", true),
            new TestCaseData(0, "Dimension8", "dimension8", false), new TestCaseData(0, "Dimension8", "dimension8", true),
            new TestCaseData(0, "Dimension9", "dimension9", false), new TestCaseData(0, "Dimension9", "dimension9", true),
            new TestCaseData(0, "Dimension10", "dimension10", false), new TestCaseData(0, "Dimension10", "dimension10", true),
            new TestCaseData(1, "ShowAccounts", "showAccounts", false), new TestCaseData(1, "ShowAccounts", "showAccounts", true),
            new TestCaseData(1, "ShowGuvAccounts", "showGuvAccounts", false), new TestCaseData(1, "ShowGuvAccounts", "showGuvAccounts", true),
            new TestCaseData(1, "ShowEmptyRows", "showEmptyRows", false), new TestCaseData(1, "ShowEmptyRows", "showEmptyRows", true),
            new TestCaseData(1, "ShowLineNumbers", "showLineNumbers", false), new TestCaseData(1, "ShowLineNumbers", "showLineNumbers", true),
            new TestCaseData(1, "UseForeignLanguage", "useForeignLanguage", false), new TestCaseData(1, "UseForeignLanguage", "useForeignLanguage", true)
        };
        [TestCaseSource(nameof(BooleanCases))]
        public async Task EachBooleanOptionDistinguishesFalseAndTrue(int route, string property, string key, bool value)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); var options = Options(route, false);
                options.GetType().GetProperty(property).SetValue(options, value); args[args.Length - 1] = options;
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(RequiredQuery(route) + "&" + key + "=" + (value ? "true" : "false")));
            }
        }

        [Test]
        public async Task EmptyOptionsRetainAllServerDefaults([ValueSource(nameof(Routes))] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(route, false); args[args.Length - 1] = Options(route, false);
                await Call(Api(http), Names[route] + "AsyncWithHttpInfo", args, Types(route, false));
                Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(RequiredQuery(route)));
            }
        }

        [Test]
        public void InvalidFinancialMonthFailsBeforeHttp([Values(-1, 0, 13, int.MaxValue)] int month, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(1, false); args[0] = month;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentOutOfRangeException>((Func<Task>)(async () => { await Call(Api(http), Names[1] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(1, mode == 2)); }));
                Assert.That(error.ParamName, Is.EqualTo("month")); Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public void MissingFinancialNamesFailBeforeHttp([Values(3, 4)] int index, [Values(null, "", " ")] string value, [Values(0, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(1, false); args[index] = value;
                if (mode == 2) args = args.Concat(new object[] { CancellationToken.None }).ToArray();
                var error = Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http), Names[1] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(1, mode == 2)); }));
                Assert.That(error.ParamName, Is.EqualTo(index == 3 ? "definitionName" : "layoutName")); Assert.That(handler.Calls, Is.Zero);
            }
        }


        private static readonly object[] FormattingCases = {
            new TestCaseData("AmountSuffix", "amountSuffix", "NoSuffix"),
            new TestCaseData("AmountSuffix", "amountSuffix", "DebitCredit"),
            new TestCaseData("AmountSuffix", "amountSuffix", "future-format"),
            new TestCaseData("AmountFormatting", "amountFormatting", "NoDecimalDigits"),
            new TestCaseData("AmountFormatting", "amountFormatting", "TwoDecimalDigits"),
            new TestCaseData("AmountFormatting", "amountFormatting", "DivideByThousand"),
            new TestCaseData("AmountFormatting", "amountFormatting", "future-format"),
            new TestCaseData("PercentFormatting", "percentFormatting", "NoDecimalDigits"),
            new TestCaseData("PercentFormatting", "percentFormatting", "OneDecimalDigit"),
            new TestCaseData("PercentFormatting", "percentFormatting", "TwoDecimalDigits"),
            new TestCaseData("PercentFormatting", "percentFormatting", "future-format")
        };
        [TestCaseSource(nameof(FormattingCases))]
        public async Task FormattingStringsRemainExact(string property, string key, string value)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                var args = Arguments(1, false); var options = Options(1, false);
                options.GetType().GetProperty(property).SetValue(options, value); args[args.Length - 1] = options;
                await Call(Api(http), Names[1] + "AsyncWithHttpInfo", args, Types(1, false));
                Assert.That(Uri.UnescapeDataString(handler.Uri.Query.Replace("+", " ")), Is.EqualTo(RequiredQuery(1) + "&" + key + "=" + value));
            }
        }

        [Test]
        public async Task PeriodBoundariesAndDefinitionCodesUseInvariantIntegers([Values(1, 12)] int month, [Values(int.MinValue, int.MaxValue)] int year, [Values(0L, 1L, 5L, long.MinValue, long.MaxValue)] long definitionType)
        {
            var original = System.Globalization.CultureInfo.CurrentCulture;
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, "{}")));
            using (var http = new HttpClient(handler))
            {
                try
                {
                    System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
                    var args = Arguments(1, false); args[0] = month; args[1] = year; args[2] = definitionType;
                    await Call(Api(http), Names[1] + "AsyncWithHttpInfo", args, Types(1, false));
                    var prefix = "?month=" + month.ToString(System.Globalization.CultureInfo.InvariantCulture) + "&year=" + year.ToString(System.Globalization.CultureInfo.InvariantCulture) + "&definitionType=" + definitionType.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    Assert.That(handler.Uri.Query, Does.StartWith(prefix + "&definitionName="));
                }
                finally { System.Globalization.CultureInfo.CurrentCulture = original; }
            }
        }

        [Test]
        public async Task TypedOptionsAndAllOverloadsCompileForStandaloneConsumers()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Json)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http);
                var journal = new CompuMaster.Scopevisio.OpenApi.Model.JournalDataSourceOptions { ReportName = "General", Dimension1 = false, Dimension10 = true, AccountingEntityNumbers = "00001", Fields = "id" };
                var financial = new CompuMaster.Scopevisio.OpenApi.Model.FinancialReportDataSourceOptions { BudgetGroupName = "Plan", Organisations = "Org", ShowGuvAccounts = false, AmountSuffix = "NoSuffix", AmountFormatting = "TwoDecimalDigits", PercentFormatting = "OneDecimalDigit" };
                Assert.That(api.GetGeneralJournalDataSource("29.02.2024", "01.03.2024", journal), Is.TypeOf<JObject>());
                Assert.That(api.GetGeneralJournalDataSourceWithHttpInfo("29.02.2024", "01.03.2024", journal).Data, Is.TypeOf<JObject>());
                Assert.That(await api.GetGeneralJournalDataSourceAsync("29.02.2024", "01.03.2024", journal), Is.TypeOf<JObject>());
                Assert.That((await api.GetGeneralJournalDataSourceAsyncWithHttpInfo("29.02.2024", "01.03.2024", journal)).Data, Is.TypeOf<JObject>());
                Assert.That(await api.GetGeneralJournalDataSourceAsync("29.02.2024", "01.03.2024", journal, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That((await api.GetGeneralJournalDataSourceAsyncWithHttpInfo("29.02.2024", "01.03.2024", journal, CancellationToken.None)).Data, Is.TypeOf<JObject>());
                Assert.That(api.GetFinancialReportDataSource(2, 2024, 1L, "Definition", "Layout", financial), Is.TypeOf<JObject>());
                Assert.That(api.GetFinancialReportDataSourceWithHttpInfo(2, 2024, 1L, "Definition", "Layout", financial).Data, Is.TypeOf<JObject>());
                Assert.That(await api.GetFinancialReportDataSourceAsync(2, 2024, 1L, "Definition", "Layout", financial), Is.TypeOf<JObject>());
                Assert.That((await api.GetFinancialReportDataSourceAsyncWithHttpInfo(2, 2024, 1L, "Definition", "Layout", financial)).Data, Is.TypeOf<JObject>());
                Assert.That(await api.GetFinancialReportDataSourceAsync(2, 2024, 1L, "Definition", "Layout", financial, CancellationToken.None), Is.TypeOf<JObject>());
                Assert.That((await api.GetFinancialReportDataSourceAsyncWithHttpInfo(2, 2024, 1L, "Definition", "Layout", financial, CancellationToken.None)).Data, Is.TypeOf<JObject>());
                Assert.That(handler.Calls, Is.EqualTo(12));
            }
        }

        private static string RequiredQuery(int route) => route == 0 ? "?startDate=29.02.2024&endDate=01.03.2024" : "?month=2&year=-2024&definitionType=9223372036854775807&definitionName= Definition / Ä & custom &layoutName= Layout / Ä & custom ";
        private static object Options(int route, bool values)
        {
            var type = typeof(ReportApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model." + (route == 0 ? "JournalDataSourceOptions" : "FinancialReportDataSourceOptions"));
            Assert.That(type, Is.Not.Null, "The public options contract is absent.");
            var options = Activator.CreateInstance(type);
            if (values)
            {
                var entries = route == 0 ? new System.Collections.Generic.Dictionary<string, object>
                {
                    ["ReportName"] = " Layout / Ä & custom ", ["Unassigned"] = false, ["ForcePersonalEntries"] = true, ["HideCancellationEntries"] = false,
                    ["Dimension1"] = true, ["Dimension2"] = false, ["Dimension3"] = true, ["Dimension4"] = false, ["Dimension5"] = true, ["Dimension6"] = false, ["Dimension7"] = true, ["Dimension8"] = false, ["Dimension9"] = true, ["Dimension10"] = false,
                    ["AccountingEntityNumbers"] = "00001,00002 & custom", ["Fields"] = "id,amount & custom"
                } : new System.Collections.Generic.Dictionary<string, object>
                {
                    ["BudgetGroupName"] = " Budget & custom ", ["Organisations"] = " Org / Ä & custom ", ["ShowAccounts"] = false, ["ShowGuvAccounts"] = true,
                    ["ShowEmptyRows"] = false, ["ShowLineNumbers"] = true, ["UseForeignLanguage"] = false, ["AmountSuffix"] = "NoSuffix",
                    ["AmountFormatting"] = "DivideByThousand", ["PercentFormatting"] = "OneDecimalDigit"
                };
                foreach (var pair in entries) type.GetProperty(pair.Key).SetValue(options, pair.Value);
            }
            return options;
        }

        private static async Task<ApiResponse<JToken>> Invoke(ReportApi api, int route, int mode, bool explicitValues, CancellationToken token)
        {
            var args = Arguments(route, explicitValues);
            if (mode == 2) args = args.Concat(new object[] { token }).ToArray();
            return (ApiResponse<JToken>)await Call(api, Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), args, Types(route, mode == 2));
        }
        private static object[] Arguments(int route, bool explicitValues)
        {
            return route == 0 ? new object[] { "29.02.2024", "01.03.2024", explicitValues ? Options(route, true) : null } :
                new object[] { 2, -2024, long.MaxValue, " Definition / Ä & custom ", " Layout / Ä & custom ", explicitValues ? Options(route, true) : null };
        }
        private static Type[] Types(int route, bool cancellation)
        {
            var options = typeof(ReportApi).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Model." + (route == 0 ? "JournalDataSourceOptions" : "FinancialReportDataSourceOptions"));
            Assert.That(options, Is.Not.Null, "The public options contract is absent.");
            var types = route == 0 ? new[] { typeof(string), typeof(string), options } : new[] { typeof(int), typeof(int), typeof(long), typeof(string), typeof(string), options };
            return cancellation ? types.Concat(new[] { typeof(CancellationToken) }).ToArray() : types;
        }
        private static async Task<object> Call(object api, string name, object[] args, Type[] types)
        {
            var method = api.GetType().GetMethod(name, types);
            Assert.That(method, Is.Not.Null, "The public workflow " + name + " is absent.");
            object value;
            try { value = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(value is Task task)) return value;
            await task;
            return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static ReportApi Api(HttpClient http) => new ReportApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-configurable-report-token" });
        private static HttpResponseMessage Response(HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls;
            internal Uri Uri;
            internal HttpMethod Method;
            internal string Body, Authorization, Accept;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Calls++; Uri = request.RequestUri; Method = request.Method; Authorization = request.Headers.Authorization?.ToString(); Accept = request.Headers.Accept.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return await send(token);
            }
        }
    }
}
