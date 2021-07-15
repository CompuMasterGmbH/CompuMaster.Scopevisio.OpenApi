/* 
 * Scopevisio OpenScope REST API
 *
 * <p>This is an interactive API reference for the Scopevisio REST API.</p><p>Below you will see the main sections of the API. Click each section in order to see the endpoints that are available in that category and use the 'Try it out' button to make API calls.</p><p>For endpoints that require authentication you can click the lock symbol in the top right of the endpoint description, which will take you to a login form.</p><h3>Authentication</h3><p>In order to use the \"Try it out\" function in Swagger you need your own Scopevisio account. If you have not set up a Scopevisio user account already you can do so <a href='https://www.scopevisio.com' target='_blank'>here</a>.</p><h3>Example Application</h3><p>We also have an example application you can reach <a href='../browser/index.html'> here </a>. Usage requires a Scopevisio account.</p><h3>Search Documentation</h3><p>Our documentation regarding <a href='../browser/index.html#!/searchscope'>search specification and paging  </a>.</p><h3>General Documentation</h3><p>Our general documentation can be found <a href='../browser/index.html#!/documentation'> here </a>.</p>
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: support@scopevisio.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Api;

namespace CompuMaster.Scopevisio.OpenApi.Test
{
    /// <summary>
    ///  Class for testing AdditionalApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]public class AdditionalApiTests
    {
        private AdditionalApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AdditionalApi(new TestConfig());
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of AdditionalApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            Assert.IsInstanceOf(typeof(AdditionalApi), instance);
        }

        
        /// <summary>
        /// Test DeleteTeamworkToken
        /// </summary>
        [Test]
        public void DeleteTeamworkTokenTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.DeleteTeamworkToken();
            
        }
        
        /// <summary>
        /// Test GetApplicationContext
        /// </summary>
        [Test]
        public void GetApplicationContextTest()
        {
            Client.ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.AccountInfo> result = instance.GetApplicationContextWithHttpInfo();
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            Assert.IsNotEmpty(result.Data.Customer.Name);
            Assert.IsNotEmpty(result.Data.Customer.PublicId);
            Assert.IsNotEmpty(result.Data.Customer.Status);
            Assert.NotZero(result.Data.Organisation.Id);
            Assert.IsNotEmpty(result.Data.Organisation.Name);
            Assert.IsNotEmpty(result.Data.Organisation.TeamworkTenantId);
            Assert.IsNotEmpty(result.Data.Organisation.TeamworkTenantName);
            Assert.IsNotEmpty(result.Data.User.Uid);
            Assert.IsNotEmpty(result.Data.User.Status);
            Assert.IsNotEmpty(result.Data.User.Login);
            Assert.IsNotEmpty(result.Data.Teamwork.TeamworkAppRootUrl);
        }

        /// <summary>
        /// Test GetLicences
        /// </summary>
        [Test]
        public void GetLicencesTest()
        {
            Client.ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.License[]> result = instance.GetLicencesWithHttpInfo();
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            foreach (CompuMaster.Scopevisio.OpenApi.Model.License lic in result.Data)
            {
                System.Console.WriteLine(lic);
                Assert.IsNotEmpty(lic.Type);
                Assert.IsNotEmpty(lic.AssignedToUid);
                Assert.IsNotEmpty(lic.AssignedToEmail);
            }
            Assert.NotZero(result.Data.Length);
        }
        
        /// <summary>
        /// Test GetTeamworkToken
        /// </summary>
        [Test]
        public void GetTeamworkTokenTest()
        {
            bool? force = null;
            Client.ApiResponse<Object> result = instance.GetTeamworkTokenWithHttpInfo(force);
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            //System.Console.WriteLine(instance.GetTeamworkTokenWithHttpInfo(force).Data);
        }

        /// <summary>
        /// Test GetVersion
        /// </summary>
        [Test]
        public void GetVersionTest()
        {
            Client.ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.Version> result = instance.GetVersionWithHttpInfo();
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            Assert.Less(new DateTime(2020, 05, 01), result.Data.BuildDate);
            Assert.Less(10000, result.Data.BuildNumber);
            Assert.Less(new DateTime(2020, 05, 01), result.Data.CommitDate);
            Assert.IsNotEmpty(result.Data.CommitHash);
        }

        /// <summary>
        /// Test HelloJson
        /// </summary>
        [Test]
        public void HelloJsonTest()
        {
            Client.ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.Hello> result = instance.HelloJsonWithHttpInfo();
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            Assert.AreEqual(new TestConfig().Username, result.Data.HelloMessage);
        }
        
        /// <summary>
        /// Test OrganisationJson
        /// </summary>
        [Test]
        public void OrganisationJsonTest()
        {
            Client.ApiResponse<CompuMaster.Scopevisio.OpenApi.Model.Records<CompuMaster.Scopevisio.OpenApi.Model.Organisation>> result= instance.OrganisationJsonWithHttpInfo();
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            System.Console.WriteLine("RAW JSON:");
            System.Console.WriteLine(result.RawJsonContent);
            System.Console.WriteLine("/RAW JSON\r\n");
            System.Console.WriteLine(result.Data.ToString());

            Assert.NotZero(result.Data.Items.Count);
            Assert.NotZero(result.Data.Items[0].Id);
        }

    }

}