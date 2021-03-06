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
using CompuMaster.Scopevisio.OpenApi.Test;

namespace CompuMaster.Scopevisio.OpenApi.Test
{
    /// <summary>
    ///  Class for testing AuthorizationApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class AuthorizationApiTests
    {
        private AuthorizationApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AuthorizationApi(new TestConfig(true));
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of AuthorizationApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            Assert.IsInstanceOf(typeof(AuthorizationApi), instance);
            Assert.IsNotEmpty(instance.Configuration.Username);
            Assert.IsNotEmpty(instance.Configuration.ClientNumber);
            Assert.IsNotEmpty(instance.Configuration.Password);
            Assert.IsNull(instance.Configuration.OrganisationName);
            Assert.IsNull(instance.Configuration.AccessToken);
        }


        /// <summary>
        /// Test ChangeOrganisationById
        /// </summary>
        [Test]
        public void ChangeOrganisationByIdTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long organisationId = null;
            //instance.ChangeOrganisationById(organisationId);
            
        }
        
        /// <summary>
        /// Test ChangeOrganisationByName
        /// </summary>
        [Test]
        public void ChangeOrganisationByNameTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string name = null;
            //instance.ChangeOrganisationByName(name);
            
        }
        
        /// <summary>
        /// Test RevokeToken
        /// </summary>
        [Test]
        public void RevokeTokenTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.RevokeToken();
            
        }
        
        /// <summary>
        /// Test Token
        /// </summary>
        [Test]
        public void TokenTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string grantType = null;
            //string customer = null;
            //string clientId = null;
            //string clientSecret = null;
            //string username = null;
            //long? organisationId = null;
            //string organisation = null;
            //string password = null;
            //string totpResponse = null;
            //string refreshToken = null;
            //string code = null;
            //string requestcookie = null;
            //instance.Token(grantType, customer, clientId, clientSecret, username, organisationId, organisation, password, totpResponse, refreshToken, code, requestcookie);
            CompuMaster.Scopevisio.OpenApi.Model.TokenResponse result = instance.TokenWithHttpInfo("password", instance.Configuration.ClientNumber, null, null, instance.Configuration.Username, null, instance.Configuration.OrganisationName, instance.Configuration.Password, null, null, null, null).Data;
            Assert.IsNotEmpty(result.AccessToken);
            Assert.IsNotEmpty(result.RefreshToken);
            Assert.AreEqual(Model.TokenResponse.TokenTypeEnum.Bearer, result.TokenType);
            Assert.IsNotEmpty(instance.Configuration.AccessToken);
            //Assert.IsNotEmpty(instance.Configuration.RefreshToken);
        }

        /// <summary>
        /// Test Token and re-authenticate in a 2nd request
        /// </summary>
        [Test]
        public void TokenWorkflowTest_MultiHttpRequests()
        {
            this.TokenTest();
            Assert.IsNotEmpty(instance.Configuration.AccessToken);
            Assert.IsNotEmpty(new Api.AdditionalApi(new TestConfig()).HelloJsonWithHttpInfo().Data.HelloMessage);
        }


    }

}
