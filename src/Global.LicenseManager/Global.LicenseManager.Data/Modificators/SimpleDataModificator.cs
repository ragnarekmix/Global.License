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
        private readonly ILog _log = LogManager.GetLogger(typeof(SimpleDataModificator));

        public void AddNewLicense(int licenseId, int customerId, string key)
        {
            var creationDate = DateTime.Now.Date;

            try
            {
                var db = Database.Open();
                db.Licenses.Insert(CustomerId: customerId, Key: key, CreationDate: creationDate, ModificationDate: creationDate);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
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
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
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
                _log.ErrorFormat("ERROR: {0}", e.Message);
                throw new ApplicationException();
            }
        }
    }
}
