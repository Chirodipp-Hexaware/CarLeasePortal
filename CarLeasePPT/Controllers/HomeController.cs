using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Filters;
using CarLeasePPT.Helpers;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    public class HomeController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Index()
        {
            ViewBag.UnworkedItemCount = await WorkItemEngine.GetUnworkedItemCount();

            var warnPeriod = PasswordSecurityPolicy.WarnPeriod;
            var passwordExpiresOn = AuthenticationHelper.GetPasswordExpiration(AuthenticationManager);
            var remain = passwordExpiresOn - DateTime.Now;
            var pem = new PasswordExpiration
            {
                IsDanger = remain.TotalHours < 24,
                TimeRemaining = remain,
                ShowMessage = warnPeriod >= remain.TotalDays
            };
            ViewBag.PasswordExpiration = pem;
            return View();
        }

        #endregion

        public class PasswordExpiration
        {
            #region Public Properties

            public bool IsDanger { get; set; }
            public bool ShowMessage { get; set; }
            public TimeSpan TimeRemaining { get; set; }

            #endregion
        }
    }
}