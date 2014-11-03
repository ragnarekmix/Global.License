using LicenseManager.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Simple.Data;
using System;
using System.Linq;

namespace LicenseManager.DataAccess.Tests
{
    [TestClass]
    public class SimpleDataRepresentatorTests
    {
        private SimpleDataRepresentator sut;
        private SimpleDataModificator writer;
        private TimeContext timeContext;
        private DateTime tesеTime;

        [TestInitialize]
        public void SetUp()
        {
            sut = new SimpleDataRepresentator();
            timeContext = Substitute.For<TimeContext>();
            tesеTime = new DateTime(2014, 10, 10);
            timeContext.Now().Returns(tesеTime);
            writer = new SimpleDataModificator(timeContext);
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnAllCustomersEntries()
        {
            var db = Database.Open();
            db.Customers.Insert(CustomerId: 1, FirstName: "Mihail", LastName: "Podobivsky");
            db.Customers.Insert(CustomerId: 2, FirstName: "Joe", LastName: "Oraely");

            var customers = sut.GetAllCustomers();
            Assert.AreEqual(2, customers.Count);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectId()
        {
            var customerId = 10;
            var db = Database.Open();
            db.Customers.Insert(CustomerId: customerId, FirstName: "Mihail", LastName: "Podobivsky");

            var customers = sut.GetAllCustomers();
            Assert.AreEqual(customerId, customers.First().Id);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectFirstName()
        {
            var firstName = "Mihail";
            var db = Database.Open();
            db.Customers.Insert(CustomerId: 1, FirstName: firstName, LastName: "Podobivsky");

            var customers = sut.GetAllCustomers();
            Assert.AreEqual(firstName, customers.First().FirstName);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectLastName()
        {
            var lastName = "Podobivsky";
            var db = Database.Open();
            db.Customers.Insert(CustomerId: 1, FirstName: "Mihail", LastName: lastName);

            var customers = sut.GetAllCustomers();
            Assert.AreEqual(lastName, customers.First().LastName);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnAllEnries()
        {
            writer.CreateLicense(1, 1, "SomeKey1");
            writer.CreateLicense(2, 2, "SomeKey2");
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(2, licenses.Count);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectId()
        {
            var licenseId = 10;
            writer.CreateLicense(licenseId, 1, "SomeKey");
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(licenseId, licenses.First().Id);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectCustomerId()
        {
            var customerId = 1;
            writer.CreateLicense(10, customerId, "SomeKey");
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(customerId, licenses.First().CustomerId);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectKey()
        {
            var key = "SomeKey";
            writer.CreateLicense(10, 1, key);
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(key, licenses.First().Key);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectCreationDate()
        {
            writer.CreateLicense(10, 1, "SomeKey");
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(tesеTime.ToString("dd MMMM yyyy"), licenses.First().CreationDate);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectModificationDate()
        {
            writer.CreateLicense(10, 1, "SomeKey");
            var licenses = sut.GetAllLicenses();

            Assert.AreEqual(tesеTime.ToString("dd MMMM yyyy"), licenses.First().ModificationDate);
        }
    }
}