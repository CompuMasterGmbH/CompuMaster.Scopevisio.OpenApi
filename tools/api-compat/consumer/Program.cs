using CompuMaster.Scopevisio.OpenApi;
using CompuMaster.Scopevisio.OpenApi.Api;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
    private const string Json = "{\"records\":[{\"id\":1,\"name\":\"original\",\"future\":{\"flag\":false}}]}";
    private const string Body = "{\"count\":1}";
    private static async Task<int> Main()
    {
        var handler = new IsolatedHandler();
        using (var http = new HttpClient(handler))
        {
            var configuration = new Configuration { HttpClient = http, BasePath = "https://compatibility.invalid/rest", AccessToken = "isolated-binary-consumer-token" };
            var facade = new OpenScopeApiClient(configuration);
            Require(ReferenceEquals(facade.Config, configuration), "Facade configuration identity changed.");
            Require(ReferenceEquals(facade.ContactApi.Configuration, configuration), "API group configuration identity changed.");
            var response = facade.ContactApi.GetContactsWithHttpInfo(Body);
            Require(response.Data.Items.Count == 1, "Typed legacy contact result changed.");
            Require(response.Data.Items[0]["future"].Value<bool>("flag") == false, "Legacy extensible contact metadata changed.");
            Require(response.RawJsonContent == Json && response.StatusCode == 200, "Original HTTP-info contract changed.");
            Require(handler.Body == Body && handler.Uri.AbsolutePath == "/rest/contacts", "Existing synchronous raw-body behavior changed.");
            var asynchronous = await facade.ContactApi.GetContactsAsyncWithHttpInfo(Body);
            Require(asynchronous.Data.Items.Count == 1, "Existing asynchronous typed contact result changed.");
            var legacy = facade.ContactApi.GetContactByIdWithHttpInfo(1L, "id,name");
            Require(legacy.Data == null && legacy.RawJsonContent == Json, "Existing void/object result default changed.");
            Require(handler.Authorization == "Bearer isolated-binary-consumer-token", "Caller-selected authorization changed.");
            var calls = handler.Calls;
            using (var canceled = new CancellationTokenSource())
            {
                canceled.Cancel();
                try { await facade.AuthorizationApi.TokenAsyncWithHttpInfo("password", "isolated-customer", canceled.Token); throw new InvalidOperationException("Pre-cancellation was ignored."); }
                catch (OperationCanceledException) { }
            }
            Require(handler.Calls == calls, "Canceled work issued HTTP.");
            Require(configuration.AccessToken == "isolated-binary-consumer-token", "Read/cancellation changed the token.");
            var expected = new InvalidOperationException("original caller hook");
            facade.ContactApi.ExceptionFactory = (name, result) => expected;
            try { facade.ContactApi.GetContactsWithHttpInfo(Body); throw new InvalidOperationException("The exception hook was ignored."); }
            catch (InvalidOperationException error) { Require(ReferenceEquals(error, expected), "The original exception instance changed."); }
            facade.ContactApi.ExceptionFactory = Configuration.DefaultExceptionFactory;
            var records = new Records<JObject> { Items = new[] { JObject.Parse("{\"id\":1,\"future\":true}") } };
            var contacts = new RecordCollection<Contact>(records);
            Require(contacts.Items.Count == 1 && contacts.Items[0].Value<bool>("future"), "Existing public record constructors changed.");
        }
        Console.WriteLine("Unchanged baseline consumer passed with isolated HTTP.");
        return 0;
    }
    private static void Require(bool condition, string message)
    { if (!condition) throw new InvalidOperationException(message); }
    private sealed class IsolatedHandler : HttpMessageHandler
    {
        internal int Calls;
        internal string Body, Authorization;
        internal Uri Uri;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested(); Calls++;
            Uri = request.RequestUri; Require(Uri.Host == "compatibility.invalid", "Unexpected server target.");
            Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
            Authorization = request.Headers.Authorization?.ToString();
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Json, Encoding.UTF8, "application/json") };
        }
    }
}
