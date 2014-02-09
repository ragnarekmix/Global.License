using Global.LicenseManager.Data.Entities;
using System.Collections.Generic;

namespace Global.LicenseManager.Models
{
    public class AppModel
    {
        public List<Customer> Customers { get; set; }
        public List<License> Licenses { get; set; }
    }
}