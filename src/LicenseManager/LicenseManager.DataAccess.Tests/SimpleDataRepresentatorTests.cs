using LicenseManager.Common.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Data;
using System;
using System.Collections.Generic;

namespace LicenseManager.DataAccess.Tests
{
    [TestClass]
    public class SimpleDataRepresentatorTests
    {
        private IDataRepresentator sut;

        [TestInitialize]
        public void SetUp()
        {
            sut = new SimpleDataRepresentator();
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
            var customers = sut.GetAllCustomers();

            Assert.AreEqual(testCustomers.Count, customers.Count);

            for (int i = 0; i < testCustomers.Count; i++)
            {
                Assert.AreEqual(testCustomers[i].Id, customers[i].Id);
                Assert.AreEqual(testCustomers[i].FirstName, customers[i].FirstName);
                Assert.AreEqual(testCustomers[i].LastName, customers[i].LastName);
            }
        }

        [TestMethod]
        public void GetAllLicensesTest()
        {
            var testLicenses = new List<License>
            {
                new License {Id=1, CustomerId = 1, Key = "lasdjflksdf", CreationDate = "02 February 2011", ModificationDate = "02 February 2011"},
                new License {Id=2, CustomerId = 2, Key = "ghjfhgkfjhk", CreationDate = "02 February 2011", ModificationDate = "02 February 2011"}
            };
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(testLicenses.Count, licenses.Count);

            for (int i = 0; i < testLicenses.Count; i++)
            {
                Assert.AreEqual(testLicenses[i].Id, licenses[i].Id);
                Assert.AreEqual(testLicenses[i].CustomerId, licenses[i].CustomerId);
                Assert.AreEqual(testLicenses[i].CreationDate, licenses[i].CreationDate);
                Assert.AreEqual(testLicenses[i].ModificationDate, licenses[i].ModificationDate);
                Assert.AreEqual(testLicenses[i].Key, licenses[i].Key);
            }
        }
    }
}