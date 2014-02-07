using Global.LicenseManager.Data.Interfaces;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Modificators
{
    public class SimpleDataModificator: IDataModificator
    {
        public void AddNewLicense(int customerId, string key)
        {
            var db = Database.Open();
            var creationDate = DateTime.Now.Date;
            db.Licenses.Insert(CustomerId: customerId, Key: key, CreationDate: creationDate, ModificationDate: creationDate);
        }

        public void ChangeLicense(int id, string key)
        {
            var db = Database.Open();
            var modificationDate = DateTime.Now.Date;
            db.Licenses.UpdateByLicenseId(LicenseId: id, Key: key, ModificationDate: modificationDate);
        }

        public void DeleteLicense(int id)
        {
            var db = Database.Open();
            db.Licenses.DeleteByLicenseId(id);
        }
    }
}
