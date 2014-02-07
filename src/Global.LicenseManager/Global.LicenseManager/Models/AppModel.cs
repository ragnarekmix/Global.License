using Global.LicenseManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Global.LicenseManager.Models
{
    public class AppModel
    {
        public List<Customer> Customers { get; set; }
        public List<License> Licenses { get; set; }
    }
}