using Global.LicenseManager.Common.Interfaces;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Modificators
{
    public class XmlDataModificator : IDataModificator
    {
        FileSystem fileSystem;

        public XmlDataModificator(FileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var doc = XDocument.Parse(fileSystem.ReadXmlFile());

            var licensesByCustomerId = GetCustomerLicenses(customerId, doc);
            var newLicense = GetNewLicense(licenseId, key);
            licensesByCustomerId.Add(newLicense);

            fileSystem.SaveXmlFile(doc.ToString());
        }

        public void ChangeLicense(int id, string key)
        {
            var doc = XDocument.Parse(fileSystem.ReadXmlFile());

            var license = GetLicenseById(id, doc);
            var modificationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            license.Element("Key").Value = key;
            license.Element("ModificationDate").Value = modificationDate;

            fileSystem.SaveXmlFile(doc.ToString());
        }

        public void DeleteLicense(int id)
        {
            var doc = XDocument.Parse(fileSystem.ReadXmlFile());

            var license = GetLicenseById(id, doc);
            license.Remove();

            fileSystem.SaveXmlFile(doc.ToString());
        }

        private XElement GetCustomerLicenses(int customerId, XDocument doc)
        {
            return (doc.Root.Elements("Customer")
                .Where(customer => int.Parse(customer.Element("CustomerId").Value) == customerId)
                .Select(customer => customer.Element("Licenses"))).First();
        }

        private XElement GetLicenseById(int id, XDocument doc)
        {
            return (doc.Root.Elements("Customer")
                .Select(customer => (customer.Element("Licenses").Elements("License")))
                .SelectMany(licenses => licenses, (licenses, license) => new { licenses, license })
                .Where(@t => int.Parse(@t.license.Element("LicenseId").Value) == id)
                .Select(@t => @t.license)).First();
        }

        private XElement GetNewLicense(int licenseId, string key)
        {
            var creationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            var newLicense = new XElement("License");
            newLicense.Add(new XElement("LicenseId", licenseId));
            newLicense.Add(new XElement("Key", key));
            newLicense.Add(new XElement("CreationDate", creationDate));
            newLicense.Add(new XElement("ModificationDate", creationDate));
            return newLicense;
        }
    }
}