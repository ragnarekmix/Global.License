using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Global.LicenseManager.Controllers
{
    public class HomeController : Controller
    {
        public IDataRepresentator DataRepresentator { get; set; }
        public ILog Log { get; set; }
        public SimpleDataModificator SimpleDataModificator { get; set; }
        public XmlDataModificator XmlDataModificator { get; set; }

        public HomeController()
        {
            Log = LogManager.GetLogger(typeof(HomeController));
        }

        public ActionResult Index()
        {
            var customers = new List<Customer>();
            var licenses = new List<License>();
            try
            {
                customers = DataRepresentator.GetAllCustomers();
            }
            catch (Exception e)
            {
                Log.ErrorFormat("There was an ERROR during getting all customers from source: {0}", e);
            }

            try
            {
                licenses = DataRepresentator.GetAllLicenses();
            }
            catch (Exception e)
            {
                Log.ErrorFormat("There was an ERROR during getting all licenses from source: {0}", e);
            }

            var appModel = new AppModel { Customers = customers, Licenses = licenses };

            return View(appModel);
        }

        public ActionResult AddNewLicense(License license)
        {
            var msg = "New license has been saved.";

            try
            {
                license.Id = GetMaxLicenseId() + 1;
                SimpleDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
                XmlDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during adding new license to source: {0}", e);
                Log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult UpdateLicense(License license)
        {
            var msg = "License has been changed.";
            try
            {
                SimpleDataModificator.ChangeLicense(license.Id, license.Key);
                XmlDataModificator.ChangeLicense(license.Id, license.Key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during change license in source: {0}", e);
                Log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult DeleteLicense(License license)
        {
            var msg = "License has been deleted.";
            try
            {
                SimpleDataModificator.DeleteLicense(license.Id);
                XmlDataModificator.DeleteLicense(license.Id);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during delete license from source: {0}", e);
                Log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult About()
        {
            ViewBag.Message = "App description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
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
