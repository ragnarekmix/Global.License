using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data;
using Global.LicenseManager.Data.Representators;
using Global.LicenseManager.Tests.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Global.LicenseManager.Tests.Data
{
    [TestClass]
    public class XmlDataRepresentatorTests
    {
        IDataRepresentator sut;
        Mock<ILogger> log;
        Mock<Config> config;
        Mock<FileSystem> fileSystem;

        [TestInitialize]
        public void SetUp()
        {
            log = new Mock<ILogger>();
            config = new Mock<Config>(log.Object);
            fileSystem = new Mock<FileSystem>(log.Object);
            fileSystem.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(Resources.Source);
            sut = new XmlDataRepresentator(log.Object, config.Object, fileSystem.Object);
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