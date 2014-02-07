using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Representators
{
    public class XmlDataRepresentator: IDataRepresentator
    {
        public List<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();

            return customerList;
        }

        public List<License> GetAllLicenses()
        {
            var licenseList = new List<License>();

            return licenseList;
        }
    }
}
