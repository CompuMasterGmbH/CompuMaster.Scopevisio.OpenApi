using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class AccountingReportDownloadTests
    {
        private static readonly string[] Names = { "DownloadAccountingReport" };
        private static readonly byte[] Bytes = Enumerable.Range(0, 256).Select(value => (byte)value).ToArray();

        [Test]
        public async Task BinaryBytesStatusAndHeadersAreRetained([Values(0)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Names[route] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"),
                    mode == 2 ? new object[] { Request(), CancellationToken.None } : new object[] { Request() }, mode == 2);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/reports/guv"));
                Assert.That(Query(handler, "startDate"), Is.EqualTo("01.01.2026"));
                Assert.That(Query(handler, "endDate"), Is.EqualTo("31.12.2026"));
                Assert.That(Query(handler, "splitDimensions"), Is.EqualTo("2,0 & future"));
                Assert.That(Query(handler, "reportName"), Is.EqualTo(" original & name "));
                Assert.That(Query(handler, "outputFormat"), Is.EqualTo("pdf"));
                Assert.That(Query(handler, "includeHeader"), Is.EqualTo("false"));
                Assert.That(Query(handler, "includeZeroValued"), Is.EqualTo("true"));
                Assert.That(handler.Uri.Query.TrimStart('?').Split('&').Length, Is.EqualTo(7));
                Assert.That(handler.Method, Is.EqualTo(HttpMethod.Get));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Accept, Is.EqualTo("*/*"));
                Assert.That(handler.Authorization, Is.EqualTo("Bearer isolated-download-token"));
                Assert.That(result.Data, Is.EqualTo(Bytes));
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Headers["x-fixture"], Is.EqualTo("first,second,third"));
                Assert.That(result.Headers["content-type"], Is.EqualTo("application/x-fixture"));
                Assert.That(result.Headers["Content-Length"], Is.EqualTo(Bytes.Length.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Assert.That(result.Headers["Content-Disposition"], Is.EqualTo("attachment; filename=\"../unsafe.bin\""));
                Assert.That(result.RawJsonContent, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task EmptyFileIsNotJsonNull([Values(0)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, new byte[0])));
            using (var http = new HttpClient(handler))
            {
                var result = (ApiResponse<byte[]>)await Call(Api(http), Names[route] + "AsyncWithHttpInfo", new object[] { Request() }, false);
                Assert.That(result.Data, Is.Not.Null.And.Empty);
                Assert.That(result.RawJsonContent, Is.Null);
            }
        }

        [Test]
        public async Task AllConvenienceFormsReturnOriginalBytes([Values(0)] int route, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var result = (byte[])await Call(Api(http), Names[route] + (mode == 0 ? "" : "Async"),
                    mode == 2 ? new object[] { Request(), CancellationToken.None } : new object[] { Request() }, mode == 2);
                Assert.That(result, Is.EqualTo(Bytes));
            }
        }

        [Test]
        public async Task MissingPermissionsAndServerErrorsRemainFailures([Values(0)] int route, [Values(403, 404, 500)] int status, [Values(false, true)] bool asynchronous)
        {
            var errorBytes = System.Text.Encoding.UTF8.GetBytes("original file/profile diagnostic");
            var handler = new Handler(token => Task.FromResult(Response((HttpStatusCode)status, errorBytes)));
            using (var http = new HttpClient(handler))
            {
                var error = Assert.ThrowsAsync<ApiException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { Request() }, false);
                }));
                Assert.That(error.ErrorCode, Is.EqualTo(status));
                Assert.That((object)error.ErrorContent, Is.EqualTo("original file/profile diagnostic"));
            }
        }

        [Test]
        public async Task CustomHookRunsBeforeBinaryWrapping([Values(0)] int route, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); var expected = new InvalidOperationException("original custom download failure");
                api.ExceptionFactory = (name, response) => { Assert.That(name, Is.EqualTo(Names[route])); return expected; };
                var actual = Assert.ThrowsAsync<InvalidOperationException>((Func<Task>)(async () => {
                    await Call(api, Names[route] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { Request() }, false);
                }));
                Assert.That(actual, Is.SameAs(expected));
            }
        }


        [Test]
        public async Task DisabledFactoryRetainsOriginalBinaryDiagnostic([Values(0)] int route)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.Forbidden, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var api = Api(http); api.ExceptionFactory = null;
                var response = (ApiResponse<byte[]>)await Call(api, Names[route] + "AsyncWithHttpInfo", new object[] { Request() }, false);
                Assert.That(response.StatusCode, Is.EqualTo(403));
                Assert.That(response.Data, Is.EqualTo(Bytes));
                Assert.That(response.RawJsonContent, Is.Null);
            }
        }

        [Test]
        public void PreCancellationPreventsTransport([Values(0)] int route, [Values(false, true)] bool info)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                Assert.CatchAsync<OperationCanceledException>((Func<Task>)(async () => {
                    await Call(Api(http), Names[route] + (info ? "AsyncWithHttpInfo" : "Async"), new object[] { Request(), cancellation.Token }, true);
                }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ActiveCancellationReachesHttp([Values(0)] int route, [Values(false, true)] bool info)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await Task.Delay(Timeout.Infinite, token); return Response(HttpStatusCode.OK, Bytes); });
            using (var http = new HttpClient(handler))
            using (var cancellation = new CancellationTokenSource())
            {
                var operation = Call(Api(http), Names[route] + (info ? "AsyncWithHttpInfo" : "Async"), new object[] { Request(), cancellation.Token }, true);
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
        public async Task OptionalChoicesPreserveOmissionEmptyAndIndependentFlags([Values(0, 1, 2)] int mode, [Values(false, true)] bool info, [Values(0, 1, 2)] int headerChoice, [Values(0, 1, 2)] int zeroChoice)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var request = new ReportExportRequest { Type = "guv", StartDate = "", EndDate = " original date ", IncludeHeader = headerChoice == 0 ? (bool?)null : headerChoice == 2, IncludeZeroValued = zeroChoice == 0 ? (bool?)null : zeroChoice == 2 };
                if (headerChoice != 0) { request.ReportName = ""; request.OutputFormat = ""; request.SplitDimensions = ""; }
                await Call(Api(http), Names[0] + (mode == 0 ? "" : "Async") + (info ? "WithHttpInfo" : ""), mode == 2 ? new object[] { request, CancellationToken.None } : new object[] { request }, mode == 2);
                Assert.That(Query(handler, "startDate"), Is.Empty);
                Assert.That(Query(handler, "endDate"), Is.EqualTo(" original date "));
                Assert.That(Query(handler, "reportName"), Is.EqualTo(headerChoice == 0 ? null : ""));
                Assert.That(Query(handler, "outputFormat"), Is.EqualTo(headerChoice == 0 ? null : ""));
                Assert.That(Query(handler, "splitDimensions"), Is.EqualTo(headerChoice == 0 ? null : ""));
                Assert.That(Query(handler, "includeHeader"), Is.EqualTo(headerChoice == 0 ? null : headerChoice == 1 ? "false" : "true"));
                Assert.That(Query(handler, "includeZeroValued"), Is.EqualTo(zeroChoice == 0 ? null : zeroChoice == 1 ? "false" : "true"));
                Assert.That(handler.Body, Is.Null);
                Assert.That(handler.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CatalogAndOriginalDateChoicesRemainServerAuthoritative([Values(null, "csv", "pdf", "xml", "json", "future-format")] string format, [Values(0, 1, 2)] int mode)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var request = Request(); request.Type = " future/type & original "; request.StartDate = "31.02.2026"; request.OutputFormat = format;
                await Call(Api(http), Names[0] + (mode == 0 ? "WithHttpInfo" : "AsyncWithHttpInfo"), mode == 2 ? new object[] { request, CancellationToken.None } : new object[] { request }, mode == 2);
                Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/reports/" + Uri.EscapeDataString(request.Type)));
                Assert.That(Query(handler, "startDate"), Is.EqualTo("31.02.2026"));
                Assert.That(Query(handler, "outputFormat"), Is.EqualTo(format));
                Assert.That(handler.Calls, Is.EqualTo(1), "No catalog, date or dimension lookup is permitted.");
            }
        }

        [Test]
        public void MissingRequiredChoicesFailBeforeTransport([Values(0, 1, 2, 3, 4, 5, 6, 7)] int missing, [Values(false, true)] bool asynchronous)
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var request = Request();
                if (missing == 0) request = null;
                else if (missing < 6) request.Type = new[] { null, "", " ", ".", ".." }[missing - 1];
                else if (missing == 6) request.StartDate = null;
                else request.EndDate = null;
                Assert.CatchAsync<ArgumentException>((Func<Task>)(async () => { await Call(Api(http), Names[0] + (asynchronous ? "AsyncWithHttpInfo" : "WithHttpInfo"), new object[] { request }, false); }));
                Assert.That(handler.Calls, Is.Zero);
            }
        }

        [Test]
        public async Task ExportChoicesAreCopiedBeforeActiveHttpCompletes()
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var handler = new Handler(async token => { entered.TrySetResult(true); await release.Task; return Response(HttpStatusCode.OK, Bytes); });
            using (var http = new HttpClient(handler))
            {
                var request = Request(); var operation = Call(Api(http), Names[0] + "AsyncWithHttpInfo", new object[] { request, CancellationToken.None }, true);
                try
                {
                    Assert.That(await Task.WhenAny(entered.Task, operation, Task.Delay(5000)), Is.SameAs(entered.Task));
                    request.Type = "changed"; request.StartDate = "changed"; request.SplitDimensions = "changed"; request.IncludeHeader = true;
                    Assert.That(handler.Uri.AbsolutePath, Is.EqualTo("/rest/reports/guv"));
                    Assert.That(Query(handler, "startDate"), Is.EqualTo("01.01.2026"));
                    Assert.That(Query(handler, "splitDimensions"), Is.EqualTo("2,0 & future"));
                    Assert.That(Query(handler, "includeHeader"), Is.EqualTo("false"));
                    release.TrySetResult(true); await operation;
                }
                finally { release.TrySetResult(true); }
            }
        }

        [Test]
        public void RequestCopyAndDefaultsRetainExplicitChoices()
        {
            var empty = new ReportExportRequest(); Assert.That(empty.OutputFormat, Is.Null); Assert.That(empty.IncludeHeader, Is.Null); Assert.That(empty.IncludeZeroValued, Is.Null); Assert.That(empty.SplitDimensions, Is.Null);
            var source = Request(); var copy = new ReportExportRequest(source);
            source.Type = null; source.StartDate = null; source.EndDate = null; source.ReportName = null; source.OutputFormat = null; source.IncludeHeader = null; source.IncludeZeroValued = null; source.SplitDimensions = null;
            Assert.That(copy.Type, Is.EqualTo("guv")); Assert.That(copy.StartDate, Is.EqualTo("01.01.2026")); Assert.That(copy.EndDate, Is.EqualTo("31.12.2026")); Assert.That(copy.ReportName, Is.EqualTo(" original & name ")); Assert.That(copy.OutputFormat, Is.EqualTo("pdf")); Assert.That(copy.IncludeHeader, Is.False); Assert.That(copy.IncludeZeroValued, Is.True); Assert.That(copy.SplitDimensions, Is.EqualTo("2,0 & future"));
            Assert.Throws<ArgumentNullException>((Action)(() => new ReportExportRequest(null)));
        }

        [Test]
        public void HistoricalReportDataAndTokenRemainUnchanged()
        {
            var handler = new Handler(token => Task.FromResult(Response(HttpStatusCode.OK, Bytes)));
            using (var http = new HttpClient(handler))
            {
                var config = new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-download-token" };
                Assert.That(new ReportApi(config).GetReportWithHttpInfo("guv", "01.01.2026", "31.12.2026").Data, Is.Null);
                Assert.That(Query(handler, "splitDimensions"), Is.Null);
                Assert.That(config.AccessToken, Is.EqualTo("isolated-download-token"));
            }
        }

        private static ReportExportRequest Request() => new ReportExportRequest
        { Type = "guv", StartDate = "01.01.2026", EndDate = "31.12.2026", ReportName = " original & name ", OutputFormat = "pdf", IncludeHeader = false, IncludeZeroValued = true, SplitDimensions = "2,0 & future" };
        private static string Query(Handler handler, string name)
        {
            foreach (var part in handler.Uri.Query.TrimStart('?').Split('&'))
            {
                var pair = part.Split(new[] { '=' }, 2);
                if (Uri.UnescapeDataString(pair[0]) == name) return pair.Length == 2 ? Uri.UnescapeDataString(pair[1].Replace("+", " ")) : "";
            }
            return null;
        }

        private static ReportApi Api(HttpClient http) => new ReportApi(new Configuration { HttpClient = http, BasePath = "https://openscope.invalid/rest", AccessToken = "isolated-download-token" });
        private static async Task<object> Call(ReportApi api, string name, object[] args, bool cancellation)
        {
            var method = api.GetType().GetMethod(name, cancellation ? new[] { typeof(ReportExportRequest), typeof(CancellationToken) } : new[] { typeof(ReportExportRequest) });
            Assert.That(method, Is.Not.Null, "The binary download workflow " + name + " is absent.");
            object result;
            try { result = method.Invoke(api, args); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
            if (!(result is Task task)) return result;
            await task; return task.GetType().GetProperty("Result").GetValue(task);
        }
        private static HttpResponseMessage Response(HttpStatusCode status, byte[] body)
        {
            var response = new HttpResponseMessage(status) { Content = new ByteArrayContent(body) };
            response.Content.Headers.TryAddWithoutValidation("Content-Type", "application/x-fixture");
            response.Content.Headers.ContentLength = body.Length;
            response.Content.Headers.TryAddWithoutValidation("x-fixture", "third");
            response.Content.Headers.TryAddWithoutValidation("Content-Disposition", "attachment; filename=\"../unsafe.bin\"");
            response.Headers.TryAddWithoutValidation("X-Fixture", new[] { "first", "second" });
            return response;
        }
        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> send;
            internal int Calls; internal Uri Uri; internal HttpMethod Method; internal string Body, Accept, Authorization;
            internal Handler(Func<CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                Assert.That(request.RequestUri.Host, Is.EqualTo("openscope.invalid"));
                Calls++; Uri = request.RequestUri; Method = request.Method; Accept = request.Headers.Accept.ToString(); Authorization = request.Headers.Authorization?.ToString();
                Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(); return await send(token);
            }
        }
    }
}
