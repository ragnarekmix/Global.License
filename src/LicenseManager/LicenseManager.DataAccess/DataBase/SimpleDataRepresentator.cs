using LicenseManager.Common.Entities;
using Simple.Data;
using System.Collections.Generic;

namespace LicenseManager.DataAccess
{
    public class SimpleDataRepresentator : IDataRepresentator
    {
        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();

            var db = Database.Open();
            var customers = db.Customers.All().ToList();

            foreach (var customer in customers)
                customerList.Add(new Customer
                {
                    Id = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                });

            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();

            var db = Database.Open();
            var licenses = db.Licenses.All().ToList();

            foreach (var license in licenses)
                licenseList.Add(new License
                {
                    Id = license.LicenseId,
                    CustomerId = license.CustomerId,
                    Key = license.Key,
                    CreationDate = license.CreationDate.ToString("dd MMMM yyyy"),
                    ModificationDate = license.ModificationDate.ToString("dd MMMM yyyy")
                });

            return licenseList;
        }
    }
}