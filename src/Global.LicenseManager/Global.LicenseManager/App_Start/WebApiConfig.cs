using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace Global.LicenseManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                            name: "DefaultApiWithAction",
                            routeTemplate: "api/{controller}/{action}/{id}",
                            defaults: new { id = RouteParameter.Optional }
                        );
        }
    }
}
