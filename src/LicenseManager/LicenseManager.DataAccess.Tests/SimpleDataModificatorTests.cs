using LicenseManager.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Simple.Data;
using System;
using System.Linq;

namespace LicenseManager.DataAccess.Tests
{
    [TestClass]
    public class SimpleDataModificatorTests
    {
        private SimpleDataModificator sut;
        private SimpleDataRepresentator reader;
        private TimeContext timeContext;

        [TestInitialize]
        public void SetUp()
        {
            timeContext = Substitute.For<TimeContext>();
            sut = new SimpleDataModificator(timeContext);
            reader = new SimpleDataRepresentator();
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDb()
        {
            sut.CreateLicense(10, 11, "SomeString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDbWithCorrectLicenseId()
        {
            var licenseId = 10;
            sut.CreateLicense(licenseId, 11, "SomeString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(licenseId, result.FirstOrDefault().Id);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDbWithCorrectCustomerId()
        {
            var customerId = 11;
            sut.CreateLicense(10, customerId, "SomeString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(customerId, result.FirstOrDefault().CustomerId);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDbWithCorrectLicenseKey()
        {
            var key = "SomeString";
            sut.CreateLicense(10, 11, key);
            var result = reader.GetAllLicenses();
            Assert.AreEqual(key, result.FirstOrDefault().Key);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDbWithCurrentCreationDateTime()
        {
            timeContext.Now().Returns(new DateTime(2014, 10, 10));
            sut = new SimpleDataModificator(timeContext);

            sut.CreateLicense(10, 11, "SomeString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(new DateTime(2014, 10, 10).ToString("dd MMMM yyyy"), result.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToDbWithCurrentModificationDateTime()
        {
            var testDate = new DateTime(2014, 10, 10);
            timeContext.Now().Returns(testDate);
            sut = new SimpleDataModificator(timeContext);

            sut.CreateLicense(10, 11, "SomeString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(testDate.ToString("dd MMMM yyyy"), result.FirstOrDefault().ModificationDate);
        }

        [TestMethod]
        public void UpdateLicenseShouldUpdateLicenseKey()
        {
            var licenseId = 10;
            var newKey = "NewString";
            sut.CreateLicense(licenseId, 11, "SomeString");
            sut.UpdateLicense(licenseId, newKey);
            var result = reader.GetAllLicenses();
            Assert.AreEqual(newKey, result.FirstOrDefault().Key);
        }

        [TestMethod]
        public void UpdateLicenseShouldUpdateModificationDate()
        {
            var testDate = new DateTime(2014, 11, 11);
            timeContext.Now().Returns(x => new DateTime(2014, 10, 10), x => testDate);
            sut = new SimpleDataModificator(timeContext);

            sut.CreateLicense(10, 11, "SomeString");
            sut.UpdateLicense(10, "NewString");
            var result = reader.GetAllLicenses();
            Assert.AreEqual(testDate.ToString("dd MMMM yyyy"), result.FirstOrDefault().ModificationDate);
        }

        [TestMethod]
        public void DeleteLicenseShouldDeleteLicenseFromDb()
        {
            var licenseId = 10;
            sut.CreateLicense(licenseId, 11, "SomeString");
            sut.DeleteLicense(licenseId);
            var result = reader.GetAllLicenses();
            Assert.AreEqual(0, result.Count);
        }
    }
}