#region

using System.Web.Mvc;
using CarLeasePPT.Filters;
using CarLeasePPT.Utility;

#endregion

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    public class ErrorController : BaseController
    {
        #region Public Methods and Operators

        public ActionResult FiveHundred(string aspxerrorpath)
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            Log.Instance.Error(
                $"User encountered a 500 error when attempting to access the following path: {aspxerrorpath}.");
            return View();
        }

        public ActionResult FourOhFour(string aspxerrorpath)
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            Log.Instance.Error($"User attempted to access the following path: {aspxerrorpath}.");
            return View();
        }

        #endregion
    }
}