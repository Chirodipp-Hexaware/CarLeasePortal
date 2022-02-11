using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
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
    [AuthorizeRole(Roles = "WORKITEM_CRUD")]
    public class WorkItemController : BaseController
    {
        #region Properties

        private static string WorkItemBaseFilePath =>
            ConfigurationManager.AppSettings["WorkItemBaseFilePath"] ?? "0197293\\WorkItems\\{0}";

        #endregion

        #region Public Methods and Operators

        public async Task<ActionResult> AddAttachment(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            if (!await WorkItemEngine.IsEditableAsync(id)) return RedirectToAction("Index");
            var m = new AddAttachmentViewModel
            {
                WorkItemId = id
            };
            return View(m);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddAttachment(AddAttachmentViewModel model, int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            if (!id.Equals(model.WorkItemId)) return RedirectToAction("Index");
            if (!await WorkItemEngine.IsEditableAsync(id)) return RedirectToAction("Index");

            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var pdfFileArray = model.UploadedFiles.Where(f => f != null).Where(f => f.ContentType.Equals(MimeType.Pdf))
                .ToList().AsReadOnly();
            if (pdfFileArray.Count.Equals(0))
            {
                ModelState.AddModelError("UploadedFiles",
                    Resources.WorkItem_Common_UploadFiles_Validation_PdfsOnly_Message);
                return View(model);
            }

            foreach (var p in pdfFileArray)
            {
                var b = General.StreamToByteArray(p.InputStream);
                if (b == null) continue;

                var f = General.CleanFileName(p.FileName);

                var workItemAttachmentPath = string.Format(WorkItemBaseFilePath, model.WorkItemId);

                var guid = DocumentEngine.CreateFileStore(workItemAttachmentPath, f, b);

                if (guid.Equals(Guid.Empty))
                {
                    ModelState.AddModelError("",
                        string.Format(Resources.WorkItem_Common_AddAttachment_SaveError_Message, f));
                    continue;
                }

                if (!await WorkItemEngine.AddAttachmentAsync(model.WorkItemId, myId, f, guid))
                    ModelState.AddModelError("",
                        string.Format(Resources.WorkItem_Common_AddAttachment_SaveError_Message, f));
            }

            if (ModelState.IsValid) return RedirectToAction("Detail", new {id});
            return View(model);
        }

        //public async Task<ActionResult> AssignToClient(int id = -1)
        public ActionResult AssignToClient(int id = -1)
        {
            //TODO: Before enabling this action, need to build out capability of client to return the work item to GT.
            return RedirectToAction("Detail", new {id});

            //TODO: Send email to client
            //if (id.Equals(-1)) return RedirectToAction("Index");
            //var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            //var assignResult = await WorkItemEngine.AssignToClientAsync(id, myId);
            //if (assignResult) return RedirectToAction("Index");
            //ViewBag.WorkItemId = id;
            //return View();
        }

        public async Task<ActionResult> Create()
        {
            var m = new WorkItemCreateViewModel {WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync()};
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkItemCreateViewModel model)
        {
            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            model.WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync();
            if (!ModelState.IsValid) return View(model);

            var newWorkItemId = await WorkItemEngine.CreateAsync(model.CompanyName, myId, model.GarageAddress,
                model.GarageCity, model.GarageCounty, model.GarageState, model.IsUrgent, model.LeaseNumber,
                model.LesseeFirstName, model.LesseeLastName, model.Narrative, model.PlateNumber, model.TaxBillId,
                model.TaxCollectorId, model.TaxCollectorName, model.TaxYear, model.CarLeaseId, model.CarMake,
                model.Vin, model.WorkItemTypeId, model.DueDate, model.NoticeDate, model.NoticeNumber,
                model.TaxBillCarLeaseId);

            if (!newWorkItemId.Equals(-1))
                return RedirectToAction(model.AddAttachments ? "AddAttachment" : "Detail",
                    new {id = newWorkItemId});
            ModelState.AddModelError("", Resources.WorkItem_Create_SaveError_Message);
            return View(model);
        }

        public async Task<ActionResult> CreateFromBill(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var taxBillRecord = await TaxBillEngine.GetTaxBillAsync(id);
            if (taxBillRecord == null) return RedirectToAction("Index");

            var model = new WorkItemCreateViewModel
            {
                TaxBillId = taxBillRecord.TaxBillId,
                TaxBillNumber = taxBillRecord.BillNumber ?? "No Bill Number",
                TaxCollectorId = taxBillRecord.TaxCollectorId,
                TaxCollectorName = taxBillRecord.TaxCollectorName,
                TaxYear = taxBillRecord.TaxYear,
                WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync()
            };
            return View("Create", model);
        }

        public async Task<ActionResult> CreateFromBillLease(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var taxAllocationRecord = await TaxBillEngine.GetTaxBillCarLeaseRecord(id);
            if (taxAllocationRecord == null) return RedirectToAction("Index");

            var model = new WorkItemCreateViewModel
            {
                LeaseNumber = taxAllocationRecord.LeaseNumber,
                LesseeFirstName = taxAllocationRecord.LesseeFirstName,
                LesseeLastName = taxAllocationRecord.LesseeLastName,
                PlateNumber = taxAllocationRecord.PlateNumber,
                TaxBillId = taxAllocationRecord.TaxBillId,
                TaxBillNumber = taxAllocationRecord.BillNumber ?? "No Bill Number",
                TaxBillCarLeaseId = taxAllocationRecord.TaxBillCarLeaseId,
                TaxCollectorId = taxAllocationRecord.TaxCollectorId,
                TaxCollectorName = taxAllocationRecord.TaxCollectorName,
                TaxYear = taxAllocationRecord.TaxYear,
                CarLeaseId = taxAllocationRecord.CarLeaseId,
                Vin = taxAllocationRecord.Vin,
                WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync()
            };
            return View("Create", model);
        }

        public async Task<ActionResult> CreateFromLease(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var leaseRecord = await LeaseEngine.GetCarLeaseAsync(id);
            if (leaseRecord == null) return RedirectToAction("Index");
            var model = new WorkItemCreateViewModel
            {
                CarLeaseId = leaseRecord.CarLeaseId,
                LeaseNumber = leaseRecord.LeaseNumber,
                LesseeFirstName = leaseRecord.LesseeFirstName,
                LesseeLastName = leaseRecord.LesseeLastName,
                PlateNumber = leaseRecord.PlateNumber,
                Vin = leaseRecord.Vin,
                WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync()
            };
            return View("Create", model);
        }

        [AuthorizeRole(Roles = "WORKITEM_CRUD, WORKITEM_VIEW")]
        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");

            var wi = await WorkItemEngine.GetAsync(id);
            if (wi == null) return RedirectToAction("Index");

            var isEditable = await WorkItemEngine.IsEditableAsync(wi.WorkItemId);

            var model = new WorkItemDetailViewModel
            {
                CompanyName = wi.CompanyName,
                CreatedByPersonFullName = wi.CreatedByPersonFullName,
                CreatedOnDateTime = wi.CreatedOnDateTime,
                DueDate = wi.DueDate,
                GarageAddress = wi.GarageAddress,
                GarageCity = wi.GarageCity,
                GarageCounty = wi.GarageCounty,
                GarageState = wi.GarageState,
                IsEditable = isEditable,
                IsUrgent = wi.IsUrgent,
                LeaseNumber = wi.LeaseNumber,
                LesseeFirstName = wi.LesseeFirstName,
                LesseeLastName = wi.LesseeLastName,
                ModifiedOnDateTime = wi.ModifiedOnDateTime,
                ModifiedByPersonFullName = wi.ModifiedByPersonFullName,
                Narrative = wi.Narrative,
                NoticeDate = wi.NoticeDate,
                NoticeNumber = wi.NoticeNumber,
                PlateNumber = wi.PlateNumber,
                TaxBill = wi.TaxBillNumber,
                TaxBillId = wi.TaxBillId,
                TaxCollectorId = wi.TaxCollectorId,
                TaxCollectorName = wi.TaxCollectorName,
                TaxYear = wi.TaxYear,
                CarLeaseId = wi.CarLeaseId,
                CarMake = wi.CarMake,
                Vin = wi.Vin,
                WorkItemId = wi.WorkItemId,
                WorkItemType = wi.WorkItemTypeName,
                WorkItemAttachmentRecords = wi.WorkItemAttachmentRecords,
                WorkItemActivityRecords = wi.WorkItemActivityRecords
            };
            return View(model);
        }

        public async Task<ActionResult> Edit(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            // Validate that the work item exists
            var wi = await WorkItemEngine.GetAsync(id);
            if (wi == null) return RedirectToAction("Index");
            // Validate that we can edit the work item (can attach).
            if (!await WorkItemEngine.IsEditableAsync(wi.WorkItemId))
                return RedirectToAction("Detail", new {id});

            var model = new WorkItemEditViewModel
            {
                CompanyName = wi.CompanyName,
                DueDate = wi.DueDate,
                GarageAddress = wi.GarageAddress,
                GarageCity = wi.GarageCity,
                GarageCounty = wi.GarageCounty,
                GarageState = wi.GarageState,
                IsUrgent = wi.IsUrgent,
                LeaseNumber = wi.LeaseNumber,
                LesseeFirstName = wi.LesseeFirstName,
                LesseeLastName = wi.LesseeLastName,
                Narrative = wi.Narrative,
                NoticeDate = wi.NoticeDate,
                NoticeNumber = wi.NoticeNumber,
                PlateNumber = wi.PlateNumber,
                TaxBill = wi.TaxBillNumber,
                TaxBillId = wi.TaxBillId,
                TaxBillNumber = wi.TaxBillNumber,
                TaxBillCarLeaseId = wi.TaxBillCarLeaseId,
                TaxCollectorId = wi.TaxCollectorId,
                TaxCollectorName = wi.TaxCollectorName,
                TaxYear = wi.TaxYear,
                CarLeaseId = wi.CarLeaseId,
                CarMake = wi.CarMake,
                Vin = wi.Vin,
                WorkItemId = wi.WorkItemId,
                WorkItemTypeId = wi.WorkItemTypeId,
                WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkItemEditViewModel model, int id = -1)
        {
            // Validate the ID is not -1
            if (id.Equals(-1)) return RedirectToAction("Index");
            // Validate the ID is the same as the value from the Model
            if (!id.Equals(model.WorkItemId)) return RedirectToAction("Index");
            // Validate thta the WorkItem actually exists
            var wi = await WorkItemEngine.GetAsync(id);
            if (wi == null) return RedirectToAction("Index");
            if (!await WorkItemEngine.IsEditableAsync(wi.WorkItemId))
                return RedirectToAction("Detail", new {id});

            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            model.WorkItemTypes = await WorkItemEngine.GetWorkItemTypesAsync();

            if (wi.TaxBillId.HasValue)
            {
                model.TaxBill = wi.TaxBillNumber;
                model.TaxCollectorName = wi.TaxCollectorName;
            }

            if (wi.CarLeaseId.HasValue)
            {
                model.LeaseNumber = wi.LeaseNumber;
                model.LesseeFirstName = wi.LesseeFirstName;
                model.LesseeLastName = wi.LesseeLastName;
                model.PlateNumber = wi.PlateNumber;
                model.Vin = wi.Vin;
            }

            if (!ModelState.IsValid) return View(model);

            var updateResult = await WorkItemEngine.UpdateAsync(model.WorkItemId, model.CompanyName, myId,
                model.GarageAddress,
                model.GarageCity, model.GarageCounty, model.GarageState, model.IsUrgent, model.LeaseNumber,
                model.LesseeFirstName, model.LesseeLastName, model.Narrative, model.PlateNumber, model.TaxCollectorName,
                model.TaxYear, model.CarMake,
                model.Vin, model.WorkItemTypeId, model.DueDate, model.NoticeDate, model.NoticeNumber);

            if (updateResult) return RedirectToAction("Detail", new {id});
            ModelState.AddModelError("", Resources.WorkItem_Create_SaveError_Message);
            return View(model);
        }

        [AuthorizeRole(Roles = "WORKITEM_CRUD, WORKITEM_VIEW")]
        public async Task<ActionResult> Index()
        {
            var wis = await WorkItemEngine.GetWorkItemStatusesAsync();
            wis.Add(-1, Resources.WorkItem_Index_WorkItemStatusId_AllStatuses_Value);
            wis = wis.OrderBy(w => !w.Key.Equals(-1)).ThenBy(w => w.Key).ToDictionary(w => w.Key, w => w.Value);
            var wit = await WorkItemEngine.GetWorkItemTypesAsync();
            wit.Add(-1, Resources.WorkItem_Index_WorkItemTypeId_AllTypes_Value);
            wit = wit.OrderBy(w => !w.Key.Equals(-1)).ThenBy(w => w.Key).ToDictionary(w => w.Key, w => w.Value);

            var priorityLevels = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "-1",
                    Text = Resources.WorkItem_Index_PriorityLevel_AllTypes_Value
                },
                new SelectListItem
                {
                    Value = "0",
                    Text = Resources.WorkItem_Index_PriorityLevel_Normal_Value
                },
                new SelectListItem
                {
                    Value = "1",
                    Text = Resources.WorkItem_Index_PriorityLevel_Priority_Value
                }
            };

            var m = new WorkItemIndexViewModel
            {
                WorkItemStatuses = new SelectList(wis, "Key", "Value"),
                WorkItemTypes = new SelectList(wit, "Key", "Value"),
                PriorityLevels = priorityLevels
            };
            return View(m);
        }

        [AuthorizeRole(Roles = "WORKITEM_CRUD, WORKITEM_VIEW")]
        public async Task<ActionResult> ViewAttachment(int id = -1)
        {
            if (id.Equals(-1)) return null;
            var fr = await WorkItemEngine.GetAttachmentAsync(id);
            if (fr == null) return null;

            var contentDispostion = new ContentDisposition
            {
                FileName = $"{fr.FileName}",
                Inline = false
            };
            Response.AddHeader("content-disposition", contentDispostion.ToString());
            Response.AddHeader("content-length", fr.Bytes.Length.ToString());
            return File(fr.Bytes, MimeType.Pdf);
        }

        #endregion
    }
}