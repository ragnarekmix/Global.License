using Global.LicenseManager.Common.Entities;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Global.LicenseManager.Controllers
{
    public class HomeController : Controller
    {
        IDataRepresentator dataRepresentator;
        ILogger log;
        SimpleDataModificator simpleDataModificator;
        XmlDataModificator xmlDataModificator;

        public HomeController(IDataRepresentator dataRepresentator, SimpleDataModificator simpleDataModificator, XmlDataModificator xmlDataModificator, ILogger log)
        {
            this.dataRepresentator = dataRepresentator;
            this.simpleDataModificator = simpleDataModificator;
            this.xmlDataModificator = xmlDataModificator;
            this.log = log;
        }

        public ActionResult Index()
        {
            var customers = new List<Customer>();
            var licenses = new List<License>();
            try
            {
                customers = dataRepresentator.GetAllCustomers();
            }
            catch (Exception e)
            {
                log.Error("There was an ERROR during getting all customers from source: {0}", e);
            }

            try
            {
                licenses = dataRepresentator.GetAllLicenses();
            }
            catch (Exception e)
            {
                log.Error("There was an ERROR during getting all licenses from source: {0}", e);
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
                simpleDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
                xmlDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during adding new license to source: {0}", e);
                log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult UpdateLicense(License license)
        {
            var msg = "License has been changed.";
            try
            {
                simpleDataModificator.ChangeLicense(license.Id, license.Key);
                xmlDataModificator.ChangeLicense(license.Id, license.Key);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during change license in source: {0}", e);
                log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult DeleteLicense(License license)
        {
            var msg = "License has been deleted.";
            try
            {
                simpleDataModificator.DeleteLicense(license.Id);
                xmlDataModificator.DeleteLicense(license.Id);
            }
            catch (Exception e)
            {
                msg = String.Format("There was an ERROR during delete license from source: {0}", e);
                log.Error(msg);
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
            var licenses = dataRepresentator.GetAllLicenses();
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