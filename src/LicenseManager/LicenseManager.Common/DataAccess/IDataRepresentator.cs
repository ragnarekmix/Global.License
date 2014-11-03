using LicenseManager.Common.Entities;
using System.Collections.Generic;

public interface IDataRepresentator
{
    List<Customer> GetAllCustomers();

    List<License> GetAllLicenses();
}