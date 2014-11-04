using LicenseManager.Common.Configuration;
using LicenseManager.DataAccess.Tests.Sourse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;

namespace LicenseManager.DataAccess.Tests
{
    [TestClass]
    public class XmlDataRepresentatorTests
    {
        private XmlDataRepresentator sut;
        private FileSystem fileSystem;

        [TestInitialize]
        public void SetUp()
        {
            fileSystem = Substitute.For<FileSystem>((Config)null);
            fileSystem.ReadXmlFile().Returns(Resource.SimpleSource);
            sut = new XmlDataRepresentator(fileSystem);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnAllCustomersEntries()
        {
            var customers = sut.GetAllCustomers();
            Assert.AreEqual(2, customers.Count);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectId()
        {
            var customers = sut.GetAllCustomers();
            Assert.AreEqual(1, customers.First().Id);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectFirstName()
        {
            var customers = sut.GetAllCustomers();
            Assert.AreEqual("Mihail", customers.First().FirstName);
        }

        [TestMethod]
        public void GetAllCustomersShouldReturnCustomerWithCorrectLastName()
        {
            var customers = sut.GetAllCustomers();
            Assert.AreEqual("Podobivsky", customers.First().LastName);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnAllEnries()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(2, licenses.Count);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectId()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(1, licenses.First().Id);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectCustomerId()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(1, licenses.First().CustomerId);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectKey()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual("SomeKey1", licenses.First().Key);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectCreationDate()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(new DateTime(2014, 10, 10).ToString("dd MMMM yyyy"), licenses.First().CreationDate);
        }

        [TestMethod]
        public void GetAllLicensesShouldReturnLicenseWithCorrectModificationDate()
        {
            var licenses = sut.GetAllLicenses();
            Assert.AreEqual(new DateTime(2014, 10, 11).ToString("dd MMMM yyyy"), licenses.First().ModificationDate);
        }
    }
}