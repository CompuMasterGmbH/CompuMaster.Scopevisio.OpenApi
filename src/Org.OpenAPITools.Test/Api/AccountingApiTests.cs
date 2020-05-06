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

using Org.OpenAPITools.Client;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test
{
    /// <summary>
    ///  Class for testing AccountingApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class AccountingApiTests
    {
        private AccountingApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AccountingApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of AccountingApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOf' AccountingApi
            //Assert.IsInstanceOf(typeof(AccountingApi), instance);
        }

        
        /// <summary>
        /// Test AddDimensionEntry
        /// </summary>
        [Test]
        public void AddDimensionEntryTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string dimensionNameOrNumber = null;
            //DimensionEntryForm body = null;
            //instance.AddDimensionEntry(dimensionNameOrNumber, body);
            
        }
        
        /// <summary>
        /// Test AddFile
        /// </summary>
        [Test]
        public void AddFileTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string documentNumber = null;
            //FileForm body = null;
            //instance.AddFile(documentNumber, body);
            
        }
        
        /// <summary>
        /// Test CancelPostingDocument
        /// </summary>
        [Test]
        public void CancelPostingDocumentTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string documentNumber = null;
            //instance.CancelPostingDocument(documentNumber);
            
        }
        
        /// <summary>
        /// Test CreateDebitor
        /// </summary>
        [Test]
        public void CreateDebitorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //PersonalAccountForm body = null;
            //instance.CreateDebitor(body);
            
        }
        
        /// <summary>
        /// Test CreateDebitorBankConnection
        /// </summary>
        [Test]
        public void CreateDebitorBankConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //BankConnectionForm body = null;
            //var response = instance.CreateDebitorBankConnection(accountNumber, body);
            //Assert.IsInstanceOf(typeof(Object), response, "response is Object");
        }
        
        /// <summary>
        /// Test CreateImpersonalAcocunts
        /// </summary>
        [Test]
        public void CreateImpersonalAcocuntsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ImpersonalAccountsForm body = null;
            //instance.CreateImpersonalAcocunts(body);
            
        }
        
        /// <summary>
        /// Test CreateKebitorBankConnection
        /// </summary>
        [Test]
        public void CreateKebitorBankConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //BankConnectionForm body = null;
            //var response = instance.CreateKebitorBankConnection(accountNumber, body);
            //Assert.IsInstanceOf(typeof(Object), response, "response is Object");
        }
        
        /// <summary>
        /// Test CreateKreditor
        /// </summary>
        [Test]
        public void CreateKreditorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //PersonalAccountForm body = null;
            //instance.CreateKreditor(body);
            
        }
        
        /// <summary>
        /// Test CreatePaymentTerm
        /// </summary>
        [Test]
        public void CreatePaymentTermTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //PaymentTermForm body = null;
            //var response = instance.CreatePaymentTerm(body);
            //Assert.IsInstanceOf(typeof(Object), response, "response is Object");
        }
        
        /// <summary>
        /// Test CreatePostings
        /// </summary>
        [Test]
        public void CreatePostingsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //Postings body = null;
            //instance.CreatePostings(body);
            
        }
        
        /// <summary>
        /// Test ExportDatev
        /// </summary>
        [Test]
        public void ExportDatevTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatevExportSettingsForm body = null;
            //instance.ExportDatev(body);
            
        }
        
        /// <summary>
        /// Test GetAccountingInfos
        /// </summary>
        [Test]
        public void GetAccountingInfosTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.GetAccountingInfos();
            
        }
        
        /// <summary>
        /// Test GetAllDimensionEntries
        /// </summary>
        [Test]
        public void GetAllDimensionEntriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetAllDimensionEntries(body);
            
        }
        
        /// <summary>
        /// Test GetBankConnectionsForDebitor
        /// </summary>
        [Test]
        public void GetBankConnectionsForDebitorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //string fields = null;
            //instance.GetBankConnectionsForDebitor(accountNumber, fields);
            
        }
        
        /// <summary>
        /// Test GetBankConnectionsForKreditor
        /// </summary>
        [Test]
        public void GetBankConnectionsForKreditorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //string fields = null;
            //instance.GetBankConnectionsForKreditor(accountNumber, fields);
            
        }
        
        /// <summary>
        /// Test GetDebitorAccounts
        /// </summary>
        [Test]
        public void GetDebitorAccountsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetDebitorAccounts(body);
            
        }
        
        /// <summary>
        /// Test GetDimensionReportEntries
        /// </summary>
        [Test]
        public void GetDimensionReportEntriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string dimensionNameOrNumber = null;
            //instance.GetDimensionReportEntries(dimensionNameOrNumber);
            
        }
        
        /// <summary>
        /// Test GetGeneralJournalEntryFile
        /// </summary>
        [Test]
        public void GetGeneralJournalEntryFileTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string documentNumber = null;
            //instance.GetGeneralJournalEntryFile(documentNumber);
            
        }
        
        /// <summary>
        /// Test GetImpersonalAccounts
        /// </summary>
        [Test]
        public void GetImpersonalAccountsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetImpersonalAccounts(body);
            
        }
        
        /// <summary>
        /// Test GetJournalEntries
        /// </summary>
        [Test]
        public void GetJournalEntriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetJournalEntries(body);
            
        }
        
        /// <summary>
        /// Test GetJournalEntryByDocumentNumber
        /// </summary>
        [Test]
        public void GetJournalEntryByDocumentNumberTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string documentNumber = null;
            //string fields = null;
            //instance.GetJournalEntryByDocumentNumber(documentNumber, fields);
            
        }
        
        /// <summary>
        /// Test GetKreditorAccounts
        /// </summary>
        [Test]
        public void GetKreditorAccountsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetKreditorAccounts(body);
            
        }
        
        /// <summary>
        /// Test GetPaymentTerm
        /// </summary>
        [Test]
        public void GetPaymentTermTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long id = null;
            //instance.GetPaymentTerm(id);
            
        }
        
        /// <summary>
        /// Test GetPaymentTerms
        /// </summary>
        [Test]
        public void GetPaymentTermsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string fields = null;
            //instance.GetPaymentTerms(fields);
            
        }
        
        /// <summary>
        /// Test GetPersonalJournalEntries
        /// </summary>
        [Test]
        public void GetPersonalJournalEntriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetPersonalJournalEntries(body);
            
        }
        
        /// <summary>
        /// Test GetTextTemplates
        /// </summary>
        [Test]
        public void GetTextTemplatesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.GetTextTemplates();
            
        }
        
        /// <summary>
        /// Test GetVatMatrixEntries
        /// </summary>
        [Test]
        public void GetVatMatrixEntriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.GetVatMatrixEntries();
            
        }
        
        /// <summary>
        /// Test GetZones
        /// </summary>
        [Test]
        public void GetZonesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string body = null;
            //instance.GetZones(body);
            
        }
        
        /// <summary>
        /// Test ImportDatevData
        /// </summary>
        [Test]
        public void ImportDatevDataTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatevPostings body = null;
            //instance.ImportDatevData(body);
            
        }
        
        /// <summary>
        /// Test UpdateDebitor
        /// </summary>
        [Test]
        public void UpdateDebitorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //UpdatePersonalAccountForm body = null;
            //instance.UpdateDebitor(accountNumber, body);
            
        }
        
        /// <summary>
        /// Test UpdateDimensionEntry
        /// </summary>
        [Test]
        public void UpdateDimensionEntryTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string dimensionNameOrNumber = null;
            //DimensionEntryForm body = null;
            //instance.UpdateDimensionEntry(dimensionNameOrNumber, body);
            
        }
        
        /// <summary>
        /// Test UpdateKreditor
        /// </summary>
        [Test]
        public void UpdateKreditorTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string accountNumber = null;
            //UpdatePersonalAccountForm body = null;
            //instance.UpdateKreditor(accountNumber, body);
            
        }
        
        /// <summary>
        /// Test UpdatePaymentTerm
        /// </summary>
        [Test]
        public void UpdatePaymentTermTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long id = null;
            //PaymentTermForm body = null;
            //instance.UpdatePaymentTerm(id, body);
            
        }
        
    }

}
