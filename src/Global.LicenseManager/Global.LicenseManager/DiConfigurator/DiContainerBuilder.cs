using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Enums;
using Global.LicenseManager.Common.Interfaces;
using Global.LicenseManager.Common.Log;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Data.Representators;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace Global.LicenseManager.DiConfigurator
{
    public class DiContainerBuilder
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerHttpRequest();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerHttpRequest();

            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<SimpleDataModificator>();
            builder.RegisterType<XmlDataModificator>();
            builder.RegisterType<Config>();

            var config = new Config(new Logger());

            switch (config.GetDataSource())
            {
                case DataSourse.DataBase:
                    builder.RegisterType<SimpleDataRepresentator>().As<IDataRepresentator>();
                    break;

                case DataSourse.Xml:
                    builder.RegisterType<XmlDataRepresentator>().As<IDataRepresentator>();
                    break;

                default:
                    throw new NotImplementedException();
            }

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}