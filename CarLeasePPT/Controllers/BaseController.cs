using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using CarLeasePPT.Filters;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [RequireHttps]
    [LogAction]
    public class BaseController : Controller
    {
        #region Properties

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        #endregion

        #region Methods

        protected override void HandleUnknownAction(string actionName)
        {
            // When an Unknown action is called, redirect to Home/Index
            RedirectToAction("Index", "Home").ExecuteResult(ControllerContext);
        }

        protected override void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.ExceptionHandled) return;
            exceptionContext.ExceptionHandled = true;
            Log.Instance.Error(exceptionContext.Exception);
            RedirectToAction("FiveHundred", "Error").ExecuteResult(ControllerContext);
        }

        #endregion
    }
}