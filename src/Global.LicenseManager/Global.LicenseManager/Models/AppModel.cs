using System.Collections.Generic;
using Global.LicenseManager.Common.Entities;

namespace Global.LicenseManager.Models
{
    public class AppModel
    {
        public List<Customer> Customers { get; set; }
        public List<License> Licenses { get; set; }
    }
}