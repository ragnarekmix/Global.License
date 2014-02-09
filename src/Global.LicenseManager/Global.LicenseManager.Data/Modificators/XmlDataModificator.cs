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
        public ILog Log { get; set; }
        public string Source { get; set; }

        public XmlDataModificator()
        {
            Source = ConfigurationManager.AppSettings["XmlSourcePath"];
            Log = LogManager.GetLogger(typeof(XmlDataModificator));
        }


        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");

            try
            {
                var doc = XDocument.Load(Source);
                XElement customerById = (from customer in doc.Root.Elements("Customer")
                                         where int.Parse(customer.Element("CustomerId").Value) == customerId
                                         select customer.Element("Licenses")).First();
                XElement newLicense = new XElement("License");
                newLicense.Add(new XElement("LicenseId", licenseId));
                newLicense.Add(new XElement("Key", key));
                newLicense.Add(new XElement("CreationDate", creationDate));
                newLicense.Add(new XElement("ModificationDate", creationDate));
                customerById.Add(newLicense);

                doc.Save(Source);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in XmlDataModificator while AddNewLicense", e);
            }
        }

        public void ChangeLicense(int id, string key)
        {
            var modificationDate = DateTime.Now.Date.ToString("dd MMMM yyyy");
            try
            {
                var doc = XDocument.Load(Source);
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
                doc.Save(Source);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in XmlDataModificator while ChangeLicense", e);
            }
        }

        public void DeleteLicense(int id)
        {
            try
            {
                var doc = XDocument.Load(Source);
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
                doc.Save(Source);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in XmlDataModificator while DeleteLicense", e);
            }
        }
    }
}
