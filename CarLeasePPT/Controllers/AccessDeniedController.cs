using System;
using System.Web.Mvc;
using CarLeasePPT.Helpers;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    public class AccessDeniedController : Controller
    {
        #region Public Methods and Operators

        public ActionResult AuthenticationError()
        {
            return View();
        }

        // GET: AccessDenied
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RestrictedUser()
        {
            try
            {
                var bannedIpData = AuthenticationHelper.GetBlockCookie(System.Web.HttpContext.Current);
                if (bannedIpData != null) return View(bannedIpData);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }

            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}