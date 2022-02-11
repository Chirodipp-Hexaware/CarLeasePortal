using System.Web.Mvc;
using CarLeasePPT.Filters;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    public class CarController : BaseController
    {
        #region Public Methods and Operators

        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}