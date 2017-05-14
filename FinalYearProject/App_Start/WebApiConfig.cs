using System.Net.Http.Formatting;
using System.Web.Http;

namespace FinalYearProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("output", "json", "application/json"));
        }
    }
}
