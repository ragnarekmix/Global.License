using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.LicenseManager.Data.Interfaces
{
    public interface IDataModificator
    {
        void AddNewLicense(int licenseId, int customerId, string key);
        void ChangeLicense(int id, string key);
        void DeleteLicense(int id);
    }
}
