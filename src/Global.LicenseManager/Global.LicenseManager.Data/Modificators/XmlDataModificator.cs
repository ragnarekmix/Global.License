using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
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

        public XmlDataModificator(ILogger log, Config config)
        {
            this.log = log;
            this.config = config;
        }

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Load(source);
                XElement customerById = (from customer in doc.Root.Elements("Customer")
                                         where int.Parse(customer.Element("CustomerId").Value) == customerId
                                         select customer.Element("Licenses")).First();
                XElement newLicense = new XElement("License");
                newLicense.Add(new XElement("LicenseId", licenseId));
                newLicense.Add(new XElement("Key", key));
                newLicense.Add(new XElement("CreationDate", creationDate));
                newLicense.Add(new XElement("ModificationDate", creationDate));
                customerById.Add(newLicense);

                doc.Save(source);
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataModificator while AddNewLicense", e);
            }
        }

        public void ChangeLicense(int id, string key)
        {
            var modificationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Load(source);
                List<XElement> customerList = (from customer in doc.Root.Elements("Customer")
                                               select customer).ToList();
                foreach (var customer in customerList)
                {
                    IEnumerable<XElement> licenses = from license in customer.Element("Licenses").Elements("License")
                                                     select license;
                    foreach (var license in licenses)
                    {
                        if (int.Parse(license.Element("LicenseId").Value) == id)
                        {
                            license.Element("Key").Value = key;
                            license.Element("ModificationDate").Value = modificationDate;
                        }
                    }
                }
                doc.Save(source);
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataModificator while ChangeLicense", e);
            }
        }

        public void DeleteLicense(int id)
        {
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Load(source);
                List<XElement> customerList = (from customer in doc.Root.Elements("Customer")
                                               select customer).ToList();
                foreach (var customer in customerList)
                {
                    IEnumerable<XElement> licenses = from license in customer.Element("Licenses").Elements("License")
                                                     select license;
                    foreach (var license in licenses)
                    {
                        if (int.Parse(license.Element("LicenseId").Value) == id)
                        {
                            license.Remove();
                        }
                    }
                }
                doc.Save(source);
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataModificator while DeleteLicense", e);
            }
        }
    }
}