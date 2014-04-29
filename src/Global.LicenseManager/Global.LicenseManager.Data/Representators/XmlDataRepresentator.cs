using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Global.LicenseManager.Data.Representators
{
    public class XmlDataRepresentator : IDataRepresentator
    {
        FileSystem fileSystem;

        public XmlDataRepresentator(FileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();

            var doc = XDocument.Parse(fileSystem.ReadXmlFile());
            customerList = doc.Root.Elements("Customer")
                .Select(customer => new Customer
                                    {
                                        Id = int.Parse(customer.Element("CustomerId").Value),
                                        FirstName = customer.Element("FirstName").Value,
                                        LastName = customer.Element("LastName").Value
                                    }).ToList();

            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();

            var doc = XDocument.Parse(fileSystem.ReadXmlFile());
            licenseList = doc.Root.Elements("Customer")
                .Select(customer => (customer.Element("Licenses").Elements("License")))
                .SelectMany(licenses => licenses, (licenses, license) => new { licenses, license })
                .Select(@t => new License
                                       {
                                           Id = int.Parse(@t.license.Element("LicenseId").Value),
                                           CustomerId = int.Parse(@t.license.Parent.Parent.Element("CustomerId").Value),
                                           Key = @t.license.Element("Key").Value,
                                           ModificationDate = @t.license.Element("ModificationDate").Value,
                                           CreationDate = @t.license.Element("CreationDate").Value
                                       }).ToList();

            return licenseList;
        }
    }
}