using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Entities
{
    public class License
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Key { get; set; }
        public string CreationDate { get; set; }
        public string Modification { get; set; }
    }
}
