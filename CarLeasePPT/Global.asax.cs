using System;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CarLeasePPT.Utility;

namespace CarLeasePPT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                //AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new CustomViewEngine());

                MvcHandler.DisableMvcResponseHeader = true;

            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Log.Instance.Error(exception);
        }
    }
}
