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
    ///  Class for testing OfferApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class OfferApiTests
    {
        private OfferApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new OfferApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of OfferApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOf' OfferApi
            //Assert.IsInstanceOf(typeof(OfferApi), instance);
        }

        
        /// <summary>
        /// Test ConvertOfferToDispatch
        /// </summary>
        [Test]
        public void ConvertOfferToDispatchTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOfferToDispatch(number);
            
        }
        
        /// <summary>
        /// Test ConvertOfferToOpportunity
        /// </summary>
        [Test]
        public void ConvertOfferToOpportunityTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOfferToOpportunity(number);
            
        }
        
        /// <summary>
        /// Test ConvertOfferToOrder
        /// </summary>
        [Test]
        public void ConvertOfferToOrderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOfferToOrder(number);
            
        }
        
        /// <summary>
        /// Test ConvertOfferToOutgoingInvoice
        /// </summary>
        [Test]
        public void ConvertOfferToOutgoingInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOfferToOutgoingInvoice(number);
            
        }
        
        /// <summary>
        /// Test ConvertOfferToRecurringInvoice
        /// </summary>
        [Test]
        public void ConvertOfferToRecurringInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //ConvertToRecurringInvoiceForm body = null;
            //instance.ConvertOfferToRecurringInvoice(number, body);
            
        }
        
        /// <summary>
        /// Test GetOffer
        /// </summary>
        [Test]
        public void GetOfferTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOffer(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOfferParent
        /// </summary>
        [Test]
        public void GetOfferParentTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOfferParent(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOfferPositions
        /// </summary>
        [Test]
        public void GetOfferPositionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOfferPositions(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOffers
        /// </summary>
        [Test]
        public void GetOffersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetOffers(body);
            
        }
        
    }

}