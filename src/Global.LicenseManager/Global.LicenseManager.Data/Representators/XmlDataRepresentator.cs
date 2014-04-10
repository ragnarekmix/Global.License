using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Representators
{
    public class XmlDataRepresentator : IDataRepresentator
    {
        ILogger log;
        Config config;

        public XmlDataRepresentator(ILogger log, Config config)
        {
            this.log = log;
            this.config = config;
        }

        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Load(source);
                customerList = (from customer in doc.Root.Elements("Customer")
                                select new Customer
                                {
                                    Id = int.Parse(customer.Element("CustomerId").Value),
                                    FirstName = customer.Element("FirstName").Value,
                                    LastName = customer.Element("LastName").Value
                                }).ToList();
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataRepresentator while GetAllCustomers", e);
            }

            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();
            var source = config.GetXmlSourcePath();

            try
            {
                var doc = XDocument.Load(source);
                List<XElement> customerList = (from customer in doc.Root.Elements("Customer")
                                               select customer).ToList();
                foreach (var customer in customerList)
                {
                    IEnumerable<License> licenses = from license in customer.Element("Licenses").Elements("License")
                                                    select new License
                                                    {
                                                        Id = int.Parse(license.Element("LicenseId").Value),
                                                        CustomerId = int.Parse(customer.Element("CustomerId").Value),
                                                        Key = license.Element("Key").Value,
                                                        ModificationDate = license.Element("ModificationDate").Value,
                                                        CreationDate = license.Element("CreationDate").Value
                                                    };
                    licenseList.AddRange(licenses);
                }
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in XmlDataRepresentator while GetAllLicenses", e);
            }

            return licenseList;
        }
    }
}