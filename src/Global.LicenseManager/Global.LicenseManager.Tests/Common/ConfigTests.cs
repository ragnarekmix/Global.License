using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Global.LicenseManager.Tests.Common
{
    [TestClass]
    public class ConfigTests
    {
        Config sut;
        Mock<Logger> log;

        [TestInitialize]
        public void Init()
        {
            log = new Mock<Logger>();
            sut = new Config(log.Object);
        }

        [TestMethod]
        public void GetDataSourceShouldThrowAnExceptionIfConfigIsMissed()
        {
            var source = sut.GetDataSource();
            Assert.AreEqual("Xml", source.ToString());
            log.Verify(x => x.Error("Missed configuration. DataSource is not set in appConfig"), Times.Never);
            log.Verify(x => x.Error("Wrong configuration. DataSource value is invalid. Valid values: 'DataBase, Xml'"), Times.Never);
        }

        [TestMethod]
        public void GetXmlSourcePathShouldThrowAnExceptionIfConfigIsMissed()
        {
            var source = sut.GetXmlSourcePath();
            Assert.AreEqual(@"D:\dev\Global.License\src\Db\Source.xml", source);
            log.Verify(x => x.Error("Missed configuration. XmlSourcePath is not set in appConfig"), Times.Never);
        }
    }
}