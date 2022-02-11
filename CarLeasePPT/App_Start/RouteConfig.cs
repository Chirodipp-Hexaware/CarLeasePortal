using System.Web.Mvc;
using System.Web.Routing;

namespace CarLeasePPT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("AccessDenied", "AccessDenied/{action}/{id}", new { controller = "AccessDenied", id = UrlParameter.Optional });
            routes.MapRoute("Account", "Account/{action}/{id}", new { controller = "Account", id = UrlParameter.Optional });
            routes.MapRoute("AuditLog", "AuditLog/{action}/{id}", new { controller = "AuditLog", id = UrlParameter.Optional });
            routes.MapRoute("Error", "Error/{action}/{id}", new { controller = "Error", id = UrlParameter.Optional });
            routes.MapRoute("Home", "Home/{action}/{id}", new { controller = "Home", id = UrlParameter.Optional });
            routes.MapRoute("Lease", "Lease/{action}/{id}", new { controller = "Lease", id = UrlParameter.Optional });
            routes.MapRoute("Report", "Report/{action}/{id}", new { controller = "Report", id = UrlParameter.Optional });
            routes.MapRoute("Settings", "Settings/{action}/{id}", new { controller = "Settings", id = UrlParameter.Optional });
            routes.MapRoute("TaxAssessor", "TaxAssessor/{action}/{id}", new { controller = "TaxAssessor", id = UrlParameter.Optional });
            routes.MapRoute("TaxBill", "TaxBill/{action}/{id}", new { controller = "TaxBill", id = UrlParameter.Optional });
            routes.MapRoute("TaxCollector", "TaxCollector/{action}/{id}", new { controller = "TaxCollector", id = UrlParameter.Optional });
            routes.MapRoute("User", "User/{action}/{id}", new { controller = "User", id = UrlParameter.Optional });
            routes.MapRoute("Car", "Car/{action}/{id}", new { controller = "Car", id = UrlParameter.Optional });
            routes.MapRoute("WorkItem", "WorkItem/{action}/{id}", new { controller = "WorkItem", id = UrlParameter.Optional });
            routes.MapRoute("WorkItemActivity", "WorkItemActivity/{action}/{id}", new { controller = "WorkItemActivity", id = UrlParameter.Optional });
            routes.MapRoute("FourOhFour", "FourOhFour/{action}/{id}", new { controller = "Error", action = "FourOhFour", id = UrlParameter.Optional });
            routes.MapRoute("FiveHundred", "FiveHundred/{action}/{id}", new { controller = "Error", action = "FiveHundred", id = UrlParameter.Optional });
            routes.MapRoute("UserLogin", "Login/", new { controller = "Account", action = "Login" });
            routes.MapRoute("Root", string.Empty, new { controller = "Account", action = "Login" });

            // Default "catch all" route to remap to 404.
            routes.MapRoute("catch-all", "{*any}", new { controller = "Error", action = "FourOhFour" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}
