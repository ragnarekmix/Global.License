using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LicenseManager.Tests
{
    [TestClass]
    public class LicenseManagerBaundaryTests
    {
        private LicenseManagerBaundary sut;

        [TestInitialize]
        public void SetUp()
        {
            sut = new LicenseManagerBaundary();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}