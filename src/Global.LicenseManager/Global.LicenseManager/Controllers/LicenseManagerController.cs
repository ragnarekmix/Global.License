using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data.Modificators;

namespace Global.LicenseManager.Controllers
{
    public class LicenseManagerController : ApiController
    {
        IDataRepresentator dataRepresentator;
        ILogger log;
        SimpleDataModificator simpleDataModificator;
        XmlDataModificator xmlDataModificator;

        public LicenseManagerController(IDataRepresentator dataRepresentator, SimpleDataModificator simpleDataModificator, XmlDataModificator xmlDataModificator, ILogger log)
        {
            this.dataRepresentator = dataRepresentator;
            this.simpleDataModificator = simpleDataModificator;
            this.xmlDataModificator = xmlDataModificator;
            this.log = log;
        }

        public ICollection<License> GetLicenses()
        {
            var licenses = new List<License>();
            try
            {
                licenses = dataRepresentator.GetAllLicenses();
            }
            catch (Exception e)
            {
                log.Error("There was an ERROR during getting all licenses from source: {0}", e);
            }
            return licenses;
        }

        public string PostLicense(int id, string key)
        {
            var msg = "New license has been saved.";

            try
            {
                var licenseId = GetMaxLicenseId() + 1;
                simpleDataModificator.AddNewLicense(licenseId, id, key);
                xmlDataModificator.AddNewLicense(licenseId, id, key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during adding new license to source: {0}", e);
                log.Error(msg);
            }

            return msg;
        }

        public string PutLicense(int id, string key)
        {
            var msg = "License has been changed.";
            try
            {
                simpleDataModificator.ChangeLicense(id, key);
                xmlDataModificator.ChangeLicense(id, key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during change license in source: {0}", e);
                log.Error(msg);
            }

            return msg;
        }

        public string DeleteLicense(int id)
        {
            var msg = "License has been deleted.";
            try
            {
                simpleDataModificator.DeleteLicense(id);
                xmlDataModificator.DeleteLicense(id);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during delete license from source: {0}", e);
                log.Error(msg);
            }

            return msg;
        }

        private int GetMaxLicenseId()
        {
            List<License> licenses = dataRepresentator.GetAllLicenses();
            var firstOrDefault = licenses.OrderByDescending(x => x.Id).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var lastId = firstOrDefault.Id;

                return lastId;
            }
            return 1;
        }
    }
}