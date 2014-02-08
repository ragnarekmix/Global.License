using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Representators
{
    public class XmlDataRepresentator : IDataRepresentator
    {
        private string source = ConfigurationManager.AppSettings["XmlSourcePath"];
        private readonly ILog _log = LogManager.GetLogger(typeof(XmlDataRepresentator));

        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();
            try
            {
                var doc = XDocument.Load(source);
                customerList = (from customer in doc.Root.Elements("Customer")
                                select new Customer()
                                {
                                    Id = int.Parse(customer.Element("CustomerId").Value),
                                    FirstName = customer.Element("FirstName").Value,
                                    LastName = customer.Element("LastName").Value
                                }).ToList();
            }
            catch (Exception e)
            {
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }

            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();
            try
            {
                var doc = XDocument.Load(source);
                List<XElement> customerList = (from customer in doc.Root.Elements("Customer")
                                               select customer).ToList();
                foreach (var customer in customerList)
                {
                    IEnumerable<License> licenses = from license in customer.Element("Licenses").Elements("License")
                                                    select new License()
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
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }

            return licenseList;
        }
    }
}
