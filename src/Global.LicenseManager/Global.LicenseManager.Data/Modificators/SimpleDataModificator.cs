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
        public void AddNewLicense(int customerId, string key, DateTime creationDate, string modification)
        {
            var db = Database.Open();
            db.Licenses.Insert(CustomerId: customerId, Key: key, CreationDate: creationDate, Modification: modification);
        }

        public void ChangeLicense(int id, string key, string modification)
        {
            var db = Database.Open();
            db.Licenses.UpdateByLicenseId(LicenseId: id, Key: key, Modification: modification);
        }

        public void DeleteLicense(int id)
        {
            var db = Database.Open();
            db.Licenses.DeleteByLicenseId(id);
        }
    }
}
