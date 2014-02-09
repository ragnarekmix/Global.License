using System.Web.Http;
using System.Web.Mvc;

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
                            routeTemplate: "api/{controller}/{action}"
                        );
            config.Routes.MapHttpRoute(
                            name: "DefaultApiWithActionAndId",
                            routeTemplate: "api/{controller}/{action}/{id}",
                            defaults: new { id = UrlParameter.Optional }
                        );
            config.Routes.MapHttpRoute(
                            name: "DefaultApiWithActionAndIdAndKey",
                            routeTemplate: "api/{controller}/{action}/{id}/{key}",
                            defaults: new { id = UrlParameter.Optional, key = UrlParameter.Optional }
                        );
        }
    }
}
