using Global.LicenseManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Interfaces
{
    public interface IDataRepresentator
    {
        void AddNewLicense(int customerId, string key, DateTime creationDate, string modification);
        void ChangeLicense(int id, string key, string modification);
        void DeleteLicense(int id);
        List<Customer> GetAllCustomers();
        List<License> GetAllLicenses();
    }
}
