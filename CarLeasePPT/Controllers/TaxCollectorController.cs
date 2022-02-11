using System.Threading.Tasks;
using System.Web.Mvc;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Filters;
using CarLeasePPT.Helpers;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    [AuthorizeRole(Roles = "COLLECTOR_MANAGEMENT")]
    public class TaxCollectorController : BaseController
    {
        #region Public Methods and Operators

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TaxCollectorCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                if (await TaxCollectorEngine.ExistsAsync(model.TaxAssessorId, model.TaxCollectorName))
                {
                    ModelState.AddModelError("", Resources.TaxCollector_Common_Exists_Message);
                    return View(model);
                }

                if (await TaxCollectorEngine.VendorCodeExistsAsync(model.VendorCode))
                {
                    ModelState.AddModelError("", Resources.TaxCollector_Common_VendorCode_Exists_Message);
                    return View(model);
                }

                var taxCollectorId =
                    await TaxCollectorEngine.CreateAsync(myId, model.TaxAssessorId, model.TaxCollectorName,
                        model.VendorCode);

                if (!taxCollectorId.Equals(-1)) return RedirectToAction("Detail", taxCollectorId);
                ModelState.AddModelError("", Resources.TaxCollector_Create_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxCollector_Create_SaveFailure_Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxCollectorRecord = await TaxCollectorEngine.GetAsync(id);
            if (taxCollectorRecord == null) return RedirectToAction("Index");
            if (taxCollectorRecord.IsDeleted) return RedirectToAction("Detail", id);

            var model = new TaxCollectorDeleteViewModel
            {
                ConfirmDelete = false,
                TaxCollectorId = taxCollectorRecord.TaxCollectorId,
                TaxCollectorName = taxCollectorRecord.TaxCollectorName,
                VendorCode = taxCollectorRecord.VendorCode
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TaxCollectorDeleteViewModel model, int id = -1)
        {
            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                // Enforce match between form value and url value
                if (!model.TaxCollectorId.Equals(id)) return RedirectToAction("Index");
                // Re-pull database record; ensure exists; ensure not deleted
                var taxCollectorRecord = await TaxCollectorEngine.GetAsync(id);
                if (taxCollectorRecord == null) return RedirectToAction("Index");
                if (taxCollectorRecord.IsDeleted) return RedirectToAction("Detail", id);

                if (!model.ConfirmDelete)
                {
                    ModelState.AddModelError("ConfirmDelete",
                        Resources.TaxCollector_Delete_ConfirmDelete_Validation_Message);
                    model.TaxCollectorName = taxCollectorRecord.TaxCollectorName;
                    model.VendorCode = taxCollectorRecord.VendorCode;
                    return View(model);
                }

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                var deleteResult = await TaxCollectorEngine.DeleteAsync(model.TaxCollectorId, myId);

                if (deleteResult) return RedirectToAction("Detail", model.TaxCollectorId);
                ModelState.AddModelError("", Resources.TaxCollector_Edit_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxCollector_Delete_SaveFailure_Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxCollectorRecord = await TaxCollectorEngine.GetAsync(id);
            if (taxCollectorRecord == null) return RedirectToAction("Index");

            var model = new TaxCollectorDetailViewModel
            {
                County = taxCollectorRecord.County,
                IsDeleted = taxCollectorRecord.IsDeleted,
                StateAbbreviation = taxCollectorRecord.StateAbbreviation,
                TaxAssessorName = taxCollectorRecord.TaxAssessorName,
                TaxCollectorId = taxCollectorRecord.TaxCollectorId,
                TaxCollectorName = taxCollectorRecord.TaxCollectorName,
                VendorCode = taxCollectorRecord.VendorCode
            };

            return View(model);
        }

        public async Task<ActionResult> Edit(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxCollectorRecord = await TaxCollectorEngine.GetAsync(id);
            if (taxCollectorRecord == null) return RedirectToAction("Index");
            if (taxCollectorRecord.IsDeleted) return RedirectToAction("Detail", id);

            var model = new TaxCollectorEditViewModel
            {
                County = taxCollectorRecord.County,
                StateAbbreviation = taxCollectorRecord.StateAbbreviation,
                TaxAssessorName = taxCollectorRecord.TaxAssessorName,
                TaxCollectorId = taxCollectorRecord.TaxCollectorId,
                TaxCollectorName = taxCollectorRecord.TaxCollectorName,
                VendorCode = taxCollectorRecord.VendorCode
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaxCollectorEditViewModel model, int id = -1)
        {
            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                // Enforce match between form value and url value
                if (!model.TaxCollectorId.Equals(id)) return RedirectToAction("Index");
                // Re-pull database record; ensure exists; ensure not deleted
                var taxCollectorRecord = await TaxCollectorEngine.GetAsync(id);
                if (taxCollectorRecord == null) return RedirectToAction("Index");
                if (taxCollectorRecord.IsDeleted) return RedirectToAction("Detail", id);

                model.TaxAssessorName = taxCollectorRecord.TaxAssessorName;
                model.County = taxCollectorRecord.County;
                model.StateAbbreviation = taxCollectorRecord.StateAbbreviation;

                if (!ModelState.IsValid) return View(model);

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);

                if (await TaxCollectorEngine.ExistsAsync(model.TaxCollectorId, taxCollectorRecord.TaxAssessorId,
                    model.TaxCollectorName))
                {
                    ModelState.AddModelError("", Resources.TaxCollector_Common_Exists_Message);
                    return View(model);
                }

                if (await TaxCollectorEngine.VendorCodeExistsAsync(model.TaxCollectorId, model.VendorCode))
                {
                    ModelState.AddModelError("", Resources.TaxCollector_Common_VendorCode_Exists_Message);
                    return View(model);
                }

                var editResult =
                    await TaxCollectorEngine.EditAsync(model.TaxCollectorId, myId, model.TaxCollectorName,
                        model.VendorCode);

                if (editResult) return RedirectToAction("Detail", model.TaxCollectorId);
                ModelState.AddModelError("", Resources.TaxCollector_Edit_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxCollector_Edit_SaveFailure_Message);
                return View(model);
            }
        }

        // GET: TaxCollector
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}