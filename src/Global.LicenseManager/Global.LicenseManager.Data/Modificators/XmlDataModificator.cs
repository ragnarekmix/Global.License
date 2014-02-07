using Global.LicenseManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Modificators
{
    public class XmlDataModificator : IDataModificator
    {
        public void AddNewLicense(int customerId, string key, DateTime creationDate, string modification)
        {
            throw new NotImplementedException();
        }

        public void ChangeLicense(int id, string key, string modification)
        {
            throw new NotImplementedException();
        }

        public void DeleteLicense(int id)
        {
            throw new NotImplementedException();
        }
    }
}
