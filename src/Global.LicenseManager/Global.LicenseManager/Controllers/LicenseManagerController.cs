using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Data.Representators;
using log4net;

namespace Global.LicenseManager.Controllers
{
    public class LicenseManagerController : ApiController
    {
        public IDataRepresentator DataRepresentator { get; set; }
        public ILog Log { get; set; }
        public SimpleDataModificator SimpleDataModificator { get; set; }
        public XmlDataModificator XmlDataModificator { get; set; }

        public LicenseManagerController()
        {
            Log = LogManager.GetLogger(typeof(LicenseManagerController));
            DataRepresentator = new SimpleDataRepresentator();  //This is a spike, beacuse DI don't want to inject properties of this class =\ TO FIX
            SimpleDataModificator = new SimpleDataModificator();
            XmlDataModificator = new XmlDataModificator();
        }

        public ICollection<License> Get()
        {
            var licenses = new List<License>();
            try
            {
                licenses = DataRepresentator.GetAllLicenses();
            }
            catch (Exception e)
            {
                Log.ErrorFormat("There was an ERROR during getting all licenses from source: {0}", e);
            }
            return licenses;
        }

        public string Post(int customerId, string key)
        {
            var msg = "New license has been saved.";

            try
            {
                var id = GetMaxLicenseId() + 1;
                SimpleDataModificator.AddNewLicense(id, customerId, key);
                XmlDataModificator.AddNewLicense(id, customerId, key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during adding new license to source: {0}", e);
                Log.Error(msg);
            }

            return msg;
        }

        public string Put(int licenseId, string key)
        {
            var msg = "License has been changed.";
            try
            {
                SimpleDataModificator.ChangeLicense(licenseId, key);
                XmlDataModificator.ChangeLicense(licenseId, key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during change license in source: {0}", e);
                Log.Error(msg);
            }

            return msg;
        }

        public string Delete(int licenseId)
        {
            var msg = "License has been deleted.";
            try
            {
                SimpleDataModificator.DeleteLicense(licenseId);
                XmlDataModificator.DeleteLicense(licenseId);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during delete license from source: {0}", e);
                Log.Error(msg);
            }

            return msg;
        }

        private int GetMaxLicenseId()
        {
            List<License> licenses = DataRepresentator.GetAllLicenses();
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