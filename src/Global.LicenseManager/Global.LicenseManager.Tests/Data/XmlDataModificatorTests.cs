using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Tests.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Global.LicenseManager.Tests.Data
{
    [TestClass]
    public class XmlDataModificatorTests
    {
        IDataModificator sut;
        Mock<ILogger> log;
        Mock<Config> config;
        Mock<FileSystem> fileSystem;

        [TestInitialize]
        public void SetUp()
        {
            log = new Mock<ILogger>();
            config = new Mock<Config>(log.Object);
            fileSystem = new Mock<FileSystem>(log.Object, config.Object);
            fileSystem.Setup(x => x.ReadXmlFile()).Returns(Resources.SimpleSource);
            sut = new XmlDataModificator(fileSystem.Object);
        }

        [TestMethod]
        public void AddNewLicenseShoulCallFileSystemReadAndSaveFileMethods()
        {
            sut.AddNewLicense(1, 1, "NewKeyValue");
            fileSystem.Verify(x => x.ReadXmlFile(), Times.Once);
            fileSystem.Verify(x => x.SaveXmlFile(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ChangeLicenseShoulCallFileSystemReadAndSaveFileMethods()
        {
            sut.ChangeLicense(1, "NewKeyValue");
            fileSystem.Verify(x => x.ReadXmlFile(), Times.Once);
            fileSystem.Verify(x => x.SaveXmlFile(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void DeleteLicenseShoulCallFileSystemReadAndSaveFileMethods()
        {
            sut.DeleteLicense(1);
            fileSystem.Verify(x => x.ReadXmlFile(), Times.Once);
            fileSystem.Verify(x => x.SaveXmlFile(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void AddNewLicenseShoulCallFileSystemFileMethodWithCorrectParameter()
        {
            sut.AddNewLicense(1, 1, "NewKeyValue");
            fileSystem.Verify(x => x.SaveXmlFile(It.Is<string>(s => s.Contains("NewKeyValue"))), Times.Once);
        }

        [TestMethod]
        public void ChangeLicenseShoulCallFileSystemFileMethodWithCorrectParameter()
        {
            sut.ChangeLicense(1, "NewKeyValue");
            fileSystem.Verify(x => x.SaveXmlFile(It.Is<string>(s => s.Contains("NewKeyValue"))), Times.Once);
            fileSystem.Verify(x => x.SaveXmlFile(It.Is<string>(s => !s.Contains("OldLicenseKey"))), Times.Once);
        }

        [TestMethod]
        public void DeleteLicenseShoulCallFileSystemFileMethodWithCorrectParameter()
        {
            sut.DeleteLicense(1);
            fileSystem.Verify(x => x.SaveXmlFile(It.Is<string>(s => !s.Contains("OldLicenseKey"))), Times.Once);
        }
    }
}