using LicenseManager.Common;
using LicenseManager.Common.Entities;
using LicenseManager.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace LicenseManager.Tests
{
    [TestClass]
    public class LicenseManagerBaundaryTests
    {
        private LicenseManagerBaundary sut;
        private IDataRepresentator reader;
        private SimpleDataModificator dbWriter;
        private XmlDataModificator xmlWriter;
        private List<Customer> testCustomers;
        private List<License> testLicenses;

        [TestInitialize]
        public void SetUp()
        {
            testCustomers = new List<Customer> { new Customer { Id = 1, FirstName = "Name", LastName = "Name" } };
            testLicenses = new List<License> { new License { Id = 1, CustomerId = 1, Key = "key1" }, new License { Id = 2, CustomerId = 1, Key = "key2" } };
            reader = Substitute.For<IDataRepresentator>();
            reader.GetAllCustomers().Returns(testCustomers);
            reader.GetAllLicenses().Returns(testLicenses);
            dbWriter = Substitute.For<SimpleDataModificator>((TimeContext)null);
            xmlWriter = Substitute.For<XmlDataModificator>(null, null);
            sut = new LicenseManagerBaundary(reader, dbWriter, xmlWriter);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomers()
        {
            var customers = sut.GetAllCustomers();
            Assert.AreEqual(testCustomers, customers);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenses()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(testLicenses, licenses);
        }

        [TestMethod]
        public void AddNewLicenseShouldStoreNewLicenseToDb()
        {
            var customerId = 1;
            var key = "SomeNewKey";
            sut.CreateNewLicense(customerId, key);
            dbWriter.Received().CreateLicense(Arg.Any<int>(), customerId, key);
        }

        [TestMethod]
        public void AddNewLicenseShouldStoreNewLicenseToXml()
        {
            var customerId = 1;
            var key = "SomeNewKey";
            sut.CreateNewLicense(customerId, key);
            xmlWriter.Received().CreateLicense(Arg.Any<int>(), customerId, key);
        }

        [TestMethod]
        public void AddNewLicenseShouldCreateNewLicenseWithIncrementedId()
        {
            sut.CreateNewLicense(1, "SomeNewKey");
            xmlWriter.Received().CreateLicense(3, Arg.Any<int>(), Arg.Any<string>());
            dbWriter.Received().CreateLicense(3, Arg.Any<int>(), Arg.Any<string>());
        }
    }
}