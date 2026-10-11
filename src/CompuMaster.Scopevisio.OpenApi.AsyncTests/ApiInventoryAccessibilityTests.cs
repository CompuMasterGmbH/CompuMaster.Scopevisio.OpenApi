using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CompuMaster.Scopevisio.OpenApi.AsyncTests
{
    [TestFixture]
    public class ApiInventoryAccessibilityTests
    {
        [Test]
        public void SourceInventoryAgreesWithCompiledApiAccessibilityAndCancellationParameters()
        {
            var report = JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "coverage.json")));
            var assembly = typeof(OpenScopeApiClient).Assembly;
            foreach (var entry in report["sdk_methods"])
            {
                string className = (string)entry["class"];
                string methodName = (string)entry["method"];
                var type = assembly.GetType("CompuMaster.Scopevisio.OpenApi.Api." + className, true);
                Assert.That(type.IsPublic, Is.EqualTo((string)entry["class_access"] == "public"), className);
                var candidates = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(method => method.Name == methodName && method.IsPublic == ((string)entry["access"] == "public"));
                if ((bool)entry["native_cancellation_parameter"])
                    candidates = candidates.Where(method => method.GetParameters().Any(parameter => parameter.ParameterType == typeof(CancellationToken)));
                Assert.That(candidates.Any(), Is.True, className + "." + methodName);
            }
        }

        [Test]
        public void HandwrittenContactResultRemainsPublicAndStronglyTyped()
        {
            var type = typeof(OpenScopeApiClient).Assembly.GetType("CompuMaster.Scopevisio.OpenApi.Api.ContactApi", true);
            var method = type.GetMethod("GetContactsWithHttpInfo", new[] { typeof(string) });
            Assert.That(method, Is.Not.Null);
            Assert.That(method.IsPublic, Is.True);
            Assert.That(method.ReturnType, Is.EqualTo(typeof(ApiResponse<RecordCollection<Contact>>)));
        }
    }
}
