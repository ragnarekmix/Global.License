using Global.LicenseManager.Data.Interfaces;
using log4net;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Modificators
{
    public class SimpleDataModificator : IDataModificator
    {
        public ILog Log { get; set; }

        public SimpleDataModificator()
        {
            Log = LogManager.GetLogger(typeof(SimpleDataModificator));
        }

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date;

            try
            {
                var db = Database.Open();
                db.Licenses.Insert(LicenseId: licenseId, CustomerId: customerId, Key: key, CreationDate: creationDate, ModificationDate: creationDate);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in SimpleDataModificator while AddNewLicense", e);
            }
        }

        public void ChangeLicense(int id, string key)
        {
            var modificationDate = DateTime.Now.Date;

            try
            {
                var db = Database.Open();
                db.Licenses.UpdateByLicenseId(LicenseId: id, Key: key, ModificationDate: modificationDate);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in SimpleDataModificator while ChangeLicense", e);
            }
        }

        public void DeleteLicense(int id)
        {
            try
            {
                var db = Database.Open();
                db.Licenses.DeleteByLicenseId(id);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException("ERROR in SimpleDataModificator while DeleteLicense", e);
            }
        }
    }
}
