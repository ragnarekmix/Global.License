using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Global.LicenseManager.Controllers
{
    public class HomeController : Controller
    {
        public IDataRepresentator Data { get; set; }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var customers = Data.GetAllCustomers();
            var licenses = Data.GetAllLicenses();

            var appModel = new AppModel() { Customers = customers, Licenses = licenses };

            return View(appModel);
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
    }
}
