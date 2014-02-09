using Global.LicenseManager.Data.Entities;
using System.Collections.Generic;

namespace Global.LicenseManager.Data.Interfaces
{
    public interface IDataRepresentator
    {
        List<Customer> GetAllCustomers();
        List<License> GetAllLicenses();
    }
}
