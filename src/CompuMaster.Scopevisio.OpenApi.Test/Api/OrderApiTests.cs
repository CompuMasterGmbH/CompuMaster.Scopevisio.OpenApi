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
    ///  Class for testing OrderApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class OrderApiTests
    {
        private OrderApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new OrderApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of OrderApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOf' OrderApi
            //Assert.IsInstanceOf(typeof(OrderApi), instance);
        }

        
        /// <summary>
        /// Test ConvertOrderToDispatch
        /// </summary>
        [Test]
        public void ConvertOrderToDispatchTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOrderToDispatch(number);
            
        }
        
        /// <summary>
        /// Test ConvertOrderToOffer
        /// </summary>
        [Test]
        public void ConvertOrderToOfferTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOrderToOffer(number);
            
        }
        
        /// <summary>
        /// Test ConvertOrderToOpportunity
        /// </summary>
        [Test]
        public void ConvertOrderToOpportunityTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOrderToOpportunity(number);
            
        }
        
        /// <summary>
        /// Test ConvertOrderToOutgoingInvoice
        /// </summary>
        [Test]
        public void ConvertOrderToOutgoingInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //instance.ConvertOrderToOutgoingInvoice(number);
            
        }
        
        /// <summary>
        /// Test ConvertOrderToRecurringInvoice
        /// </summary>
        [Test]
        public void ConvertOrderToRecurringInvoiceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //ConvertToRecurringInvoiceForm body = null;
            //instance.ConvertOrderToRecurringInvoice(number, body);
            
        }
        
        /// <summary>
        /// Test GetOrder
        /// </summary>
        [Test]
        public void GetOrderTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOrder(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOrderParent
        /// </summary>
        [Test]
        public void GetOrderParentTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOrderParent(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOrderPositions
        /// </summary>
        [Test]
        public void GetOrderPositionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string number = null;
            //string fields = null;
            //instance.GetOrderPositions(number, fields);
            
        }
        
        /// <summary>
        /// Test GetOrders
        /// </summary>
        [Test]
        public void GetOrdersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetOrders(body);
            
        }
        
    }

}