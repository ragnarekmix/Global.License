using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Representators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Data;
using System;
using System.Collections.Generic;

namespace Global.LicenseManager.Tests.Data
{
    [TestClass]
    public class SimpleDataRepresentatorTests
    {
        readonly IDataRepresentator _dataRepresentator = new SimpleDataRepresentator();

        [ClassInitialize]
        public void SetUp()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
            var db = Database.Open();

            db.Customers.Insert(CustomerId: 1, FirstName: "Mihail", LastName: "Podobivsky");
            db.Customers.Insert(CustomerId: 2, FirstName: "Joe", LastName: "Oraely");

            db.Licenses.Insert(LicenseId: 1, CustomerId: 1, Key: "lasdjflksdf", CreationDate: new DateTime(2011, 02, 02), ModificationDate: new DateTime(2011, 02, 02));
            db.Licenses.Insert(LicenseId: 2, CustomerId: 2, Key: "ghjfhgkfjhk", CreationDate: new DateTime(2011, 02, 02), ModificationDate: new DateTime(2011, 02, 02));
        }

        [TestMethod]
        public void GetAllCustomersTest()
        {
            var testCustomers = new List<Customer>
            { 
                new Customer {Id=1, FirstName = "Mihail", LastName ="Podobivsky"},
                new Customer {Id=2, FirstName = "Joe", LastName ="Oraely"}
            };
            var customers = _dataRepresentator.GetAllCustomers();
            Assert.AreEqual(testCustomers, customers);
        }

        [TestMethod]
        public void GetAllLicensesTest()
        {
            var testLicenses = new List<License>
            { 
                new License {Id=1, CustomerId = 1, Key = "lasdjflksdf", CreationDate = "02 February 2011", ModificationDate = "02 February 2011"},
                new License {Id=2, CustomerId = 2, Key = "ghjfhgkfjhk", CreationDate = "02 February 2011", ModificationDate = "02 February 2011"}
            };
            var licenses = _dataRepresentator.GetAllLicenses();
            Assert.AreEqual(testLicenses, licenses);
        }
    }
}
