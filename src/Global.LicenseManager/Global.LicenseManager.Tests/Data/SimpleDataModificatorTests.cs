using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data.Modificators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simple.Data;

namespace Global.LicenseManager.Tests.Data
{
    [TestClass]
    public class SimpleDataModificatorTests
    {
        IDataModificator sut;
        Mock<ILogger> log;

        [TestInitialize]
        public void SetUp()
        {
            log = new Mock<ILogger>();
            sut = new SimpleDataModificator(log.Object);
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}