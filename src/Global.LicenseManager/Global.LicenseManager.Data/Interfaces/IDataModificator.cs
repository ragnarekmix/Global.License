namespace Global.LicenseManager.Data.Interfaces
{
    public interface IDataModificator
    {
        void AddNewLicense(int licenseId, int customerId, string key);
        void ChangeLicense(int id, string key);
        void DeleteLicense(int id);
    }
}
