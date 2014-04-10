using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Simple.Data;
using System;
using System.Collections.Generic;

namespace Global.LicenseManager.Data.Representators
{
    public class SimpleDataRepresentator : IDataRepresentator
    {
        ILogger log;

        public SimpleDataRepresentator(ILogger log)
        {
            this.log = log;
        }

        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();
            try
            {
                var db = Database.Open();
                var customers = db.Customers.All().ToList();

                foreach (var customer in customers)
                {
                    customerList.Add(new Customer
                    {
                        Id = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName
                    });
                }
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in SimpleDataRepresentator while GetAllCustomers", e);
            }
            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();
            try
            {
                var db = Database.Open();
                var licenses = db.Licenses.All().ToList();

                foreach (var license in licenses)
                {
                    licenseList.Add(new License
                    {
                        Id = license.LicenseId,
                        CustomerId = license.CustomerId,
                        Key = license.Key,
                        CreationDate = license.CreationDate.ToString("dd MMMM yyyy"),
                        ModificationDate = license.ModificationDate.ToString("dd MMMM yyyy")
                    });
                }
            }
            catch (Exception e)
            {
                log.Error(String.Format("ERROR: {0}", e.Message));
                throw new ApplicationException("ERROR in SimpleDataRepresentator while GetAllLicenses", e);
            }
            return licenseList;
        }
    }
}