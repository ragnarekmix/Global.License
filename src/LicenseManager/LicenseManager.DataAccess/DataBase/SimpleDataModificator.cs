using LicenseManager.Common;
using LicenseManager.Common.DataAccess;
using Simple.Data;

namespace LicenseManager.DataAccess
{
    public class SimpleDataModificator : IDataModificator
    {
        private TimeContext timeContext;

        public SimpleDataModificator(TimeContext timeContext)
        {
            this.timeContext = timeContext;
        }

        public void CreateLicense(int licenseId, int customerId, string key)
        {
            var currentDate = timeContext.Now().Date;
            var db = Database.Open();
            db.Licenses.Insert(LicenseId: licenseId, CustomerId: customerId, Key: key, CreationDate: currentDate, ModificationDate: currentDate);
        }

        public void UpdateLicense(int id, string key)
        {
            var modificationDate = timeContext.Now().Date;
            var db = Database.Open();
            db.Licenses.UpdateByLicenseId(LicenseId: id, Key: key, ModificationDate: modificationDate);
        }

        public void DeleteLicense(int id)
        {
            var db = Database.Open();
            db.Licenses.DeleteByLicenseId(id);
        }
    }
}