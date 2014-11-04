using LicenseManager.Common.Entities;
using LicenseManager.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace LicenseManager
{
    public class LicenseManagerBaundary
    {
        private readonly IDataRepresentator reader;
        private readonly SimpleDataModificator dbWriter;
        private readonly XmlDataModificator xmlWriter;

        public LicenseManagerBaundary(IDataRepresentator reader, SimpleDataModificator dbWriter, XmlDataModificator xmlWriter)
        {
            this.reader = reader;
            this.dbWriter = dbWriter;
            this.xmlWriter = xmlWriter;
        }

        public List<Customer> GetAllCustomers()
        {
            return reader.GetAllCustomers();
        }

        public List<License> GetAllLicenses()
        {
            return reader.GetAllLicenses();
        }

        public void CreateNewLicense(int customerId, string key)
        {
            var id = GetNewLicenseId();
            dbWriter.CreateLicense(id, customerId, key);
            xmlWriter.CreateLicense(id, customerId, key);
        }

        private int GetNewLicenseId()
        {
            var firstOrDefault = reader.GetAllLicenses().OrderByDescending(x => x.Id).FirstOrDefault();
            if (firstOrDefault != null)
                return firstOrDefault.Id + 1;
            return 1;
        }
    }
}