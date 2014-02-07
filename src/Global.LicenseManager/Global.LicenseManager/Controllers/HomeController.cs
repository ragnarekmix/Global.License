using Global.LicenseManager.Data.Interfaces;
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

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
