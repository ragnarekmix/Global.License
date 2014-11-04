using LicenseManager.Common;
using LicenseManager.Common.Configuration;
using LicenseManager.DataAccess.Tests.Sourse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace LicenseManager.DataAccess.Tests
{
    [TestClass]
    public class XmlDataModificatorTests
    {
        private XmlDataModificator sut;
        private TimeContext timeContext;
        private FileSystem fileSystem;

        [TestInitialize]
        public void SetUp()
        {
            fileSystem = Substitute.For<FileSystem>((Config)null);
            fileSystem.ReadXmlFile().Returns(Resource.SimpleSource);
            timeContext = Substitute.For<TimeContext>();
            sut = new XmlDataModificator(timeContext, fileSystem);
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToXmlWithCorrectLicenseId()
        {
            var licenseId = 10101;
            sut.CreateLicense(licenseId, 1, "SomeString");
            fileSystem.Received().SaveXmlFile(Arg.Is<string>(x => x.Contains(licenseId.ToString())));
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToXmlWithCorrectLicenseKey()
        {
            var key = "SomeTestString";
            sut.CreateLicense(10, 1, key);
            fileSystem.Received().SaveXmlFile(Arg.Is<string>(x => x.Contains(key)));
        }

        [TestMethod]
        public void CreateLicenseShouldAddLicenseToXmlWithCurrentCreationDateTime()
        {
            var testTime = new DateTime(2014, 10, 10);
            timeContext.Now().Returns(testTime);
            sut = new XmlDataModificator(timeContext, fileSystem);

            sut.CreateLicense(10, 1, "SomeString");
            fileSystem.Received().SaveXmlFile(Arg.Is<string>(x => x.Contains(testTime.ToString("dd MMMM yyyy"))));
        }

        [TestMethod]
        public void UpdateLicenseShouldUpdateLicenseKey()
        {
            var newKey = "NewString";
            sut.UpdateLicense(1, newKey);
            fileSystem.Received().SaveXmlFile(Arg.Is<string>(x => x.Contains(newKey)));
            fileSystem.DidNotReceive().SaveXmlFile(Arg.Is<string>(x => x.Contains("SomeKey1")));
        }

        [TestMethod]
        public void UpdateLicenseShouldUpdateModificationDate()
        {
            var testDate = new DateTime(2014, 11, 11);
            timeContext.Now().Returns(x => testDate);
            sut = new XmlDataModificator(timeContext, fileSystem);

            sut.UpdateLicense(1, "NewString");

            fileSystem.Received().SaveXmlFile(Arg.Is<string>(x => x.Contains(testDate.ToString("dd MMMM yyyy"))));
            fileSystem.DidNotReceive().SaveXmlFile(Arg.Is<string>(x => x.Contains("11 October 2014")));
        }

        [TestMethod]
        public void DeleteLicenseShouldDeleteLicenseFromDb()
        {
            sut.DeleteLicense(1);
            fileSystem.DidNotReceive().SaveXmlFile(Arg.Is<string>(x => x.Contains("SomeKey1")));
        }
    }
}