using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Global.LicenseManager.Data.Interfaces;
using Global.LicenseManager.Data.Modificators;
using Global.LicenseManager.Data.Representators;
using Global.LicenseManager.Enums;
using System;
using System.Configuration;
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
            
            switch ((DataSourse)Enum.Parse(typeof(DataSourse), ConfigurationManager.AppSettings["DataSource"]))
            {
                case DataSourse.DataBase:
                    builder.Register(item => new SimpleDataRepresentator()).As<IDataRepresentator>();
                    break;
                case DataSourse.Xml:
                    builder.Register(item => new XmlDataRepresentator()).As<IDataRepresentator>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            builder.Register(item => new SimpleDataModificator()).As<SimpleDataModificator>();
            builder.Register(item => new XmlDataModificator()).As<XmlDataModificator>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}