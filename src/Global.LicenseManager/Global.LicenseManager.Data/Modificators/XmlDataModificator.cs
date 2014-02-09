using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Representators;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Modificators
{
    public class XmlDataModificator : IDataModificator
    {
        private string source = ConfigurationManager.AppSettings["XmlSourcePath"];
        private readonly ILog _log = LogManager.GetLogger(typeof(XmlDataModificator));

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date;

            try
            {
                var doc = XDocument.Load(source);
                XElement customerById = (from customer in doc.Root.Elements("Customer")
                                         where int.Parse(customer.Element("CustomerId").Value) == customerId
                                         select customer).First();
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
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }
        }

        public void ChangeLicense(int id, string key)
        {
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
                        }
                    }
                }
                doc.Save(source);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }
        }

        public void DeleteLicense(int id)
        {
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
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }
        }
    }
}
