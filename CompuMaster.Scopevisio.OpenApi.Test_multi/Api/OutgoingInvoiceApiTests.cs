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
using CompuMaster.Scopevisio.OpenApi.Model;

namespace CompuMaster.Scopevisio.OpenApi.Test
{
    /// <summary>
    ///  Class for testing OutgoingInvoiceApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [Obsolete("TODO: Unstable or incomplete implementation", false)]
    public class OutgoingInvoiceApiTests
    {
        private OutgoingInvoiceApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new OutgoingInvoiceApi(new TestConfig());
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of OutgoingInvoiceApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            Assert.IsInstanceOf(typeof(OutgoingInvoiceApi), instance);
        }

        
        /// <summary>
        /// Test AddFileToOutgoingInvoice
        /// </summary>
        [Test]
        public void AddFileToOutgoingInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //FileForm body = null;
            //instance.AddFileToOutgoingInvoice(number, body);
            
        }
        
        /// <summary>
        /// Test ConvertInvoiceToDispatch
        /// </summary>
        [Test]
        public void ConvertInvoiceToDispatchTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertInvoiceToDispatch(number);
            
        }
        
        /// <summary>
        /// Test ConvertInvoiceToOffer
        /// </summary>
        [Test]
        public void ConvertInvoiceToOfferTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertInvoiceToOffer(number);
            
        }
        
        /// <summary>
        /// Test ConvertInvoiceToOpportunity
        /// </summary>
        [Test]
        public void ConvertInvoiceToOpportunityTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertInvoiceToOpportunity(number);
            
        }
        
        /// <summary>
        /// Test ConvertInvoiceToOrder
        /// </summary>
        [Test]
        public void ConvertInvoiceToOrderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertInvoiceToOrder(number);
            
        }
        
        /// <summary>
        /// Test ConvertInvoiceToRecurringInvoice
        /// </summary>
        [Test]
        public void ConvertInvoiceToRecurringInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //ConvertToRecurringInvoiceForm body = null;
            //instance.ConvertInvoiceToRecurringInvoice(number, body);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoice
        /// </summary>
        [Test]
        public void GetOutgoingInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOutgoingInvoice(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoiceExportTemplates
        /// </summary>
        [Test]
        public void GetOutgoingInvoiceExportTemplatesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.GetOutgoingInvoiceExportTemplates();
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoiceFile
        /// </summary>
        [Test]
        public void GetOutgoingInvoiceFileTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.GetOutgoingInvoiceFile(number);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoiceParent
        /// </summary>
        [Test]
        public void GetOutgoingInvoiceParentTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOutgoingInvoiceParent(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoicePositions
        /// </summary>
        [Test]
        public void GetOutgoingInvoicePositionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOutgoingInvoicePositions(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoiceTeamworkFileLink
        /// </summary>
        [Test]
        public void GetOutgoingInvoiceTeamworkFileLinkTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.GetOutgoingInvoiceTeamworkFileLink(number);
            
        }
        
        /// <summary>
        /// Test GetOutgoingInvoices
        /// </summary>
        [Test]
        public void GetOutgoingInvoicesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetOutgoingInvoices(body);
            
        }
        
        /// <summary>
        /// Test ImportOutgoingInvoices
        /// </summary>
        [Test]
        public void ImportOutgoingInvoicesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //OutgoingInvoiceImportForm body = null;
            //instance.ImportOutgoingInvoices(body);
            
        }
        
        /// <summary>
        /// Test PostOutgoingInvoice
        /// </summary>
        [Test]
        public void PostOutgoingInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.PostOutgoingInvoice(number);
            
        }
        
    }

}