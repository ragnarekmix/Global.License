using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data.Representators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Modificators
{
    public class XmlDataModificator : IDataModificator
    {
        ILogger log;
        Config config;
        FileSystem fileSystem;

        public XmlDataModificator(ILogger log, Config config, FileSystem fileSystem)
        {
            this.log = log;
            this.config = config;
            this.fileSystem = fileSystem;
        }

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            var source = config.GetXmlSourcePath();
            var doc = XDocument.Parse(fileSystem.ReadFile(source));
            var licensesByCustomerId = (from customer in doc.Root.Elements("Customer")
                                        where int.Parse(customer.Element("CustomerId").Value) == customerId
                                        select customer.Element("Licenses")).First();
            var newLicense = new XElement("License");
            newLicense.Add(new XElement("LicenseId", licenseId));
            newLicense.Add(new XElement("Key", key));
            newLicense.Add(new XElement("CreationDate", creationDate));
            newLicense.Add(new XElement("ModificationDate", creationDate));
            licensesByCustomerId.Add(newLicense);

            fileSystem.SaveFile(doc, source);
        }

        public void ChangeLicense(int id, string key)
        {
            var modificationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            var source = config.GetXmlSourcePath();
            var doc = XDocument.Parse(fileSystem.ReadFile(source));
            var customerList = (from customer in doc.Root.Elements("Customer")
                                select customer).ToList();
            foreach (var license in from customer in customerList
                                    select (from license in customer.Element("Licenses").Elements("License")
                                            select license) into licenses
                                    from license in licenses
                                    where int.Parse(license.Element("LicenseId").Value) == id
                                    select license)
            {
                license.Element("Key").Value = key;
                license.Element("ModificationDate").Value = modificationDate;
            }
            fileSystem.SaveFile(doc, source);
        }

        public void DeleteLicense(int id)
        {
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Parse(fileSystem.ReadFile(source));
                var customerList = (from customer in doc.Root.Elements("Customer")
                                    select customer).ToList();
                foreach (var license in from customer in customerList
                                        select (from license in customer.Element("Licenses").Elements("License")
                                                select license) into licenses
                                        from license in licenses
                                        where int.Parse(license.Element("LicenseId").Value) == id
                                        select license)
                {
                    license.Remove();
                }
                fileSystem.SaveFile(doc, source);
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataModificator while DeleteLicense", e);
            }
        }
    }
}