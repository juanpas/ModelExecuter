using System.Web.Http;

namespace BuildingBlock.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerAction",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "ControllerActionId",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { id = @"^(-)?([0-9]+)$" } // id must be all digits
            );

            config.Routes.MapHttpRoute(
                name: "FileByRelated",
                routeTemplate: "api/apiFile/{action}/{fileType}/{fileSubType}/{relatedId}",
                defaults: new { controller = "apiFile" },
                constraints: new { fileType = @"^(-)?([0-9]+)$", fileSubType = @"^(-)?([0-9]+)$", relatedId = @"^(-)?([0-9]+)$" } // must be all digits
            );

        }
    }
}
