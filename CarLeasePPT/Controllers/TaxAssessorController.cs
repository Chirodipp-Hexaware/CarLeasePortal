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
    [AuthorizeRole(Roles = "ASSESSOR_MANAGEMENT")]
    public class TaxAssessorController : BaseController
    {
        // GET: TaxAssessor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TaxAssessorCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                if (await TaxAssessorEngine.ExistsAsync(model.TaxAssessorName, model.StateAbbreviation, model.County.AsNullIfWhiteSpace()))
                {
                    ModelState.AddModelError("", Resources.TaxAssessor_Common_Exists_Message);
                    return View(model);
                }

                var taxAssessorId =
                    await TaxAssessorEngine.CreateAsync(myId, model.TaxAssessorName, model.StateAbbreviation, model.County.AsNullIfWhiteSpace());

                if (!taxAssessorId.Equals(-1))
                {
                    return RedirectToAction("Detail", taxAssessorId);
                }
                ModelState.AddModelError("", Resources.TaxAssessor_Create_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxAssessor_Create_SaveFailure_Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxAssessorRecord = await TaxAssessorEngine.GetAsync(id);
            if (taxAssessorRecord == null) return RedirectToAction("Index");

            var model = new TaxAssessorDetailViewModel
            {
                StateAbbreviation = taxAssessorRecord.StateAbbreviation,
                TaxAssessorName = taxAssessorRecord.TaxAssessorName,
                County = taxAssessorRecord.County,
                TaxAssessorId = taxAssessorRecord.TaxAssessorId,
                IsDeleted = taxAssessorRecord.IsDeleted,
            };

            return View(model);
        }

        public async Task<ActionResult> Edit(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxAssessorRecord = await TaxAssessorEngine.GetAsync(id);
            if (taxAssessorRecord == null) return RedirectToAction("Index");
            if (taxAssessorRecord.IsDeleted) return RedirectToAction("Detail", id);

            var model = new TaxAssessorEditViewModel
            {
                StateAbbreviation = taxAssessorRecord.StateAbbreviation,
                TaxAssessorName = taxAssessorRecord.TaxAssessorName,
                County = taxAssessorRecord.County,
                TaxAssessorId = taxAssessorRecord.TaxAssessorId,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaxAssessorEditViewModel model, int id = -1)
        {
            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                // Enforce match between form value and url value
                if (!model.TaxAssessorId.Equals(id)) return RedirectToAction("Index");
                // Re-pull database record; ensure exists; ensure not deleted
                var taxAssessorRecord = await TaxAssessorEngine.GetAsync(id);
                if (taxAssessorRecord == null) return RedirectToAction("Index");
                if (taxAssessorRecord.IsDeleted) return RedirectToAction("Detail", id);

                if (!ModelState.IsValid) return View(model);

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                if (await TaxAssessorEngine.ExistsAsync(model.TaxAssessorId, model.TaxAssessorName, model.StateAbbreviation, model.County.AsNullIfWhiteSpace()))
                {
                    ModelState.AddModelError("", Resources.TaxAssessor_Common_Exists_Message);
                    return View(model);
                }

                var editResult =
                    await TaxAssessorEngine.EditAsync(model.TaxAssessorId, myId, model.TaxAssessorName, model.StateAbbreviation, model.County.AsNullIfWhiteSpace());

                if (editResult)
                {
                    return RedirectToAction("Detail", model.TaxAssessorId);
                }
                ModelState.AddModelError("", Resources.TaxAssessor_Edit_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxAssessor_Edit_SaveFailure_Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var taxAssessorRecord = await TaxAssessorEngine.GetAsync(id);
            if (taxAssessorRecord == null) return RedirectToAction("Index");
            if (taxAssessorRecord.IsDeleted) return RedirectToAction("Detail", id);

            var model = new TaxAssessorDeleteViewModel
            {
                StateAbbreviation = taxAssessorRecord.StateAbbreviation,
                TaxAssessorName = taxAssessorRecord.TaxAssessorName,
                TaxAssessorId = taxAssessorRecord.TaxAssessorId,
                ConfirmDelete = false,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TaxAssessorDeleteViewModel model, int id = -1)
        {

            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                // Enforce match between form value and url value
                if (!model.TaxAssessorId.Equals(id)) return RedirectToAction("Index");
                // Re-pull database record; ensure exists; ensure not deleted
                var taxAssessorRecord = await TaxAssessorEngine.GetAsync(id);
                if (taxAssessorRecord == null) return RedirectToAction("Index");
                if (taxAssessorRecord.IsDeleted) return RedirectToAction("Detail", id);

                if (!model.ConfirmDelete)
                {
                    ModelState.AddModelError("ConfirmDelete",
                        Resources.TaxAssessor_Delete_ConfirmDelete_Validation_Message);
                    model.StateAbbreviation = taxAssessorRecord.StateAbbreviation;
                    model.TaxAssessorName = taxAssessorRecord.TaxAssessorName;
                    return View(model);
                }

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                var deleteResult = await TaxAssessorEngine.DeleteAsync(model.TaxAssessorId, myId);

                if (deleteResult)
                {
                    return RedirectToAction("Detail", model.TaxAssessorId);
                }
                ModelState.AddModelError("", Resources.TaxAssessor_Edit_SaveFailure_Message);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.TaxAssessor_Delete_SaveFailure_Message);
                return View(model);
            }
        }
    }
}