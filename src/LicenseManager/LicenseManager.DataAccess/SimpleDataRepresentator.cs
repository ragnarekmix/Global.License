using LicenseManager.Common.Entities;
using System.Collections.Generic;

namespace LicenseManager.DataAccess
{
    public class SimpleDataRepresentator : IDataRepresentator
    {
        public List<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public List<License> GetAllLicenses()
        {
            throw new System.NotImplementedException();
        }
    }
}