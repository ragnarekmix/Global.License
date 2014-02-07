using Autofac;
using Autofac.Integration.Mvc;
using Global.LicenseManager.Controllers;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Representators;
using Global.LicenseManager.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Global.LicenseManager.DiConfigurator
{
    public class DiContainerBuilder
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HomeController>().PropertiesAutowired();

            switch((DataSourse) Enum.Parse(typeof(DataSourse), ConfigurationManager.AppSettings["DataSource"]))
            {
                case DataSourse.DataBase:
                    builder.Register(item => new SimpleDataRepresentator()).As<IDataRepresentator>();
                    break;
                case DataSourse.Xml:
                    builder.Register(item => new XmlDataRepresentator()).As<IDataRepresentator>();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}