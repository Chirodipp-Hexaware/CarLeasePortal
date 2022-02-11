using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CarLeasePPT.Helpers;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Filters
{
    public class VerifyPersonSessionAttribute : ActionFilterAttribute
    {
        #region Public Methods and Operators

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var authenticationManager = filterContext.HttpContext.GetOwinContext().Authentication;
                var personId = AuthenticationHelper.GetPersonIdentifier(authenticationManager);
                var sessionToken = AuthenticationHelper.GetPersonSession(authenticationManager);

                if (!SessionTokenHelper.IsTokenValid(personId, sessionToken))
                    filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "SessionTerminated"
                    }));
                base.OnActionExecuting(filterContext);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                base.OnActionExecuting(filterContext);
            }
        }

        #endregion
    }
}