using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NLog;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        #region Public Methods and Operators

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var personName = filterContext.HttpContext.User.Identity.Name ?? "Unknown";
                var personId = filterContext.HttpContext.User.Identity.GetUserId();

                var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog", "Page Viewed");
                logEvent.Properties.Add("PersonId", personId);
                logEvent.Properties.Add("FullName", personName);
                logEvent.Properties.Add("RecordId", filterContext.RouteData.Values["Id"] ?? DBNull.Value);
                Audit.Instance.Log(logEvent);
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        #endregion
    }
}