using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Routing;

namespace CarLeasePPT
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    "DefaultApi",
            //    "api/{controller}/{id}",
            //    new {id = RouteParameter.Optional}
            //);

            // Routing information below based on information/examples found @
            // https://stackoverflow.com/questions/9499794/single-controller-with-multiple-get-methods-in-asp-net-web-api
            // https://stackoverflow.com/users/991863/sky-dev

            //config.Routes.MapHttpRoute(
            //    "DefaultApiWithId",
            //    "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional },
            //    new { id = @"\d+" }
            //);
            config.Routes.MapHttpRoute(
                "DefaultApiWithAction",
                "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                "ApiGetWithActionAndId",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional },
                new
                {
                    id = @"\d+",
                    httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                }
            );

            config.Routes.MapHttpRoute(
                "DefaultApiGet",
                "api/{controller}",
                new { action = "Get" },
                new
                {
                    httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                });
            config.Routes.MapHttpRoute(
                "DefaultApiPost",
                "api/{controller}",
                new { action = "Post" },
                new
                {
                    httpMethod = new HttpMethodConstraint(HttpMethod.Post)
                });

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Consider creating a custom filter to apply this on a per-action basis
            //config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //};
        }
    }
}