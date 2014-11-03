namespace LicenseManager.Common.DataAccess
{
    public interface IDataModificator
    {
        void CreateLicense(int licenseId, int customerId, string key);

        void UpdateLicense(int id, string key);

        void DeleteLicense(int id);
    }
}