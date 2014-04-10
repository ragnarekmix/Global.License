using System.Collections.Generic;
using Global.LicenseManager.Common.Entities;

namespace Global.LicenseManager.Common.Interfaces
{
    public interface IDataRepresentator
    {
        List<Customer> GetAllCustomers();
        List<License> GetAllLicenses();
    }
}