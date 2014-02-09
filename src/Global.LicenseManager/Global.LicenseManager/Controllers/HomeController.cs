using Global.LicenseManager.Data.Entities;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Global.LicenseManager.Controllers
{
    public class HomeController : Controller
    {
        public IDataRepresentator DataRepresentator { get; set; }
        public SimpleDataModificator SimpleDataModificator { get; set; }
        public XmlDataModificator XmlDataModificator { get; set; }
        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            List<License> licenses = new List<License>();
            try
            {
                customers = DataRepresentator.GetAllCustomers();
            }
            catch (Exception e)
            {
                _log.Error("There was an ERROR during getting all customers from source");
            }

            try
            {
                licenses = DataRepresentator.GetAllLicenses();
            }
            catch (Exception e)
            {
                _log.Error("There was an ERROR during getting all licenses from source");
            }

            var appModel = new AppModel() { Customers = customers, Licenses = licenses };

            return View(appModel);
        }

        public ActionResult AddNewLicense(License license)
        {
            var msg = "New license has been saved.";

            try
            {
                license.Id = GetMaxLicenseId() + 1;
                //SimpleDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
                XmlDataModificator.AddNewLicense(license.Id, license.CustomerId, license.Key);
            }
            catch (Exception e)
            {
                msg = "There was an ERROR during adding new license to source.";
                _log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult UpdateLicense(License license)
        {
            var msg = "License has been changed.";
            try
            {
                SimpleDataModificator.ChangeLicense(license.Id, license.Key);
                //XmlDataModificator.ChangeLicense(license.Id, license.Key);
            }
            catch (Exception e)
            {
                msg = "There was an ERROR during change license in source";
                _log.Error(msg);
            }

            return Json(new { Message = msg });
        }

        public ActionResult DeleteLicense(License license)
        {
            var msg = "License has been deleted.";
            try
            {
                SimpleDataModificator.DeleteLicense(license.Id);
                //XmlDataModificator.DeleteLicense(license.Id);
            }
            catch (Exception e)
            {
                msg = "There was an ERROR during delete license from source";
                _log.Error(msg);
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
            var lastId = licenses.OrderByDescending(x => x.Id).FirstOrDefault().Id;

            return lastId;
        }s
    }
}
