using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinalYearProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    "StatesList",
                    "Home/States/List/{type}",
                    new { controller = "Home", action = "StateList", type = "" }
                );

            routes.MapRoute(
                "CountriesList",
                "Home/Countries/List",
                new { controller = "Home", action = "CountryList" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
