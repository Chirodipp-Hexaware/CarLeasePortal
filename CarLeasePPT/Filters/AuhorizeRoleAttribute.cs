using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using NLog;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Filters
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        #region Methods

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var personName = filterContext.HttpContext.User.Identity.Name ?? "Unknown";
                var personId = filterContext.HttpContext.User.Identity.GetUserId();

                var logEvent = new LogEventInfo(LogLevel.Warn, "AuditLog", "Invalid Access Attempt");
                logEvent.Properties.Add("PersonId", personId);
                logEvent.Properties.Add("FullName", personName);
                logEvent.Properties.Add("RecordId", filterContext.RouteData.Values["Id"] ?? DBNull.Value);
                Audit.Instance.Log(logEvent);

                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        #endregion
    }
}
// Adapted from: 
// http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-au