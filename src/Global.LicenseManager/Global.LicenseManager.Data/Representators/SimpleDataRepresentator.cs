using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Representators
{
    public class SimpleDataRepresentator : IDataRepresentator
    {
        public void AddNewLicense(int customerId, string key, DateTime creationDate, string modification)
        {
            var db = Database.Open();
            db.Licenses.Insert(CustomerId: customerId, Key: key, CreationDate: creationDate, Modification: modification); 
        }

        public void ChangeLicense(int id, string key, string modification)
        {
            var db = Database.Open();
            db.Licenses.UpdateByLicenseId(LicenseId: id, Key: key, Modification: modification);
        }

        public void DeleteLicense(int id)
        {
            var db = Database.Open();
            db.Licenses.DeleteByLicenseId(id);  
        }

        public List<Customer> GetAllCustomers()
        {
            var db = Database.Open();
            var customers = db.Customers.All().ToList();
            var customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer()
                {
                    Id = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                });
            }
            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var db = Database.Open();
            var licenses = db.Licenses.All().ToList();
            var licenseList = new List<License>();
            foreach (var license in licenses)
            {
                licenseList.Add(new License()
                {
                    Id = license.LicenseId,
                    CustomerId = license.CustomerId,
                    Key = license.Key,
                    CreationDate = license.CreationDate,
                    Modification = license.Modification
                });
            }
            return licenseList;
        }
    }
}
