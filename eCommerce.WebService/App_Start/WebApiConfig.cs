using System.Linq;
using System.Web.Http;

namespace eCommerce.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            MapRoutes(config);
            SetJsonToDefaultFormatter(config);
            config.EnableSystemDiagnosticsTracing();
        }

        private static void SetJsonToDefaultFormatter(HttpConfiguration config)
        {
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t =>
                    t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );
        }
    }
}
