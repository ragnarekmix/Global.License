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
        List<Customer> GetAllCustomers();
        List<License> GetAllLicenses();
    }
}
