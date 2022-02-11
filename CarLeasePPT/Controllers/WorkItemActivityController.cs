using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    public class WorkItemActivityController : BaseController
    {
        #region Properties

        private static SelectList HoursSelectList
        {
            get
            {
                var d = new Dictionary<int, string>();
                for (var i = 0; i < 24; i++)
                {
                    string s;
                    switch (i)
                    {
                        case 0:
                            s = "12 AM";
                            break;
                        case 12:
                            s = "12 PM";
                            break;
                        default:
                            s = $"{i % 12:D2} {(i < 12 ? "AM" : "PM")}";
                            break;
                    }

                    d.Add(i, s);
                }

                return new SelectList(d, "Key", "Value");
            }
        }

        private static SelectList MinutesSelectList
        {
            get
            {
                var d = new Dictionary<int, string>();
                for (var i = 0; i < 60; i = i + 5) d.Add(i, $"{i:D2}");
                return new SelectList(d, "Key", "Value");
            }
        }

        private static string WorkItemActivityBaseFilePath =>
            ConfigurationManager.AppSettings["WorkItemActivityBaseFilePath"] ?? "0197293\\WorkItems\\{0}\\{1}";

        #endregion

        #region Public Methods and Operators

        public async Task<ActionResult> AddAttachment(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            var workItemId = await WorkItemEngine.GetWorkItemIdAsync(id);
            if (workItemId == null) return RedirectToAction("Index", "WorkItem");
            if (!await WorkItemEngine.IsEditableAsync(workItemId.Value))
                return RedirectToAction("Detail", "WorkItem", new {id = workItemId.Value});
            var m = new AddAttachmentViewModel
            {
                WorkItemId = workItemId.Value,
                WorkItemActivityId = id
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAttachment(AddAttachmentViewModel model, int id = -1)
        {
            // Validate the ID is not -1
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            // Validate the ID is the same as the value from the Model
            if (!id.Equals(model.WorkItemActivityId)) return RedirectToAction("Index", "WorkItem");
            // Get the WorkItemId from the WorkItemActivityId
            var workItemId = await WorkItemEngine.GetWorkItemIdAsync(id);
            // Validate the WorkItemId is not null
            if (workItemId == null) return RedirectToAction("Index", "WorkItem");
            // Validate the WorkItemId is the same as the value from the Model
            if (!workItemId.Value.Equals(model.WorkItemId)) return RedirectToAction("Index", "WorkItem");
            // Validate that the Work Item can receive attachments
            if (!await WorkItemEngine.IsEditableAsync(workItemId.Value))
                return RedirectToAction("Detail", "WorkItem", new {id = workItemId.Value});

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

                var workItemAttachmentPath = string.Format(WorkItemActivityBaseFilePath, model.WorkItemId,
                    model.WorkItemActivityId);

                var guid = DocumentEngine.CreateFileStore(workItemAttachmentPath, f, b);

                if (guid.Equals(Guid.Empty))
                {
                    ModelState.AddModelError("",
                        string.Format(Resources.WorkItem_Common_AddAttachment_SaveError_Message, f));
                    continue;
                }

                if (!await WorkItemEngine.AddActivityAttachmentAsync(model.WorkItemId, model.WorkItemActivityId, myId,
                    f, guid))
                    ModelState.AddModelError("",
                        string.Format(Resources.WorkItem_Common_AddAttachment_SaveError_Message, f));
            }

            if (ModelState.IsValid) return RedirectToAction("Detail", "WorkItem", new {id = model.WorkItemId});
            return View(model);
        }

        public async Task<ActionResult> Create(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            if (!await WorkItemEngine.IsEditableAsync(id)) return RedirectToAction("Detail", "WorkItem", new {id});

            var n = General.RoundTimeToNearestN(DateTime.Now, TimeSpan.FromMinutes(5));
            var model = new WorkItemActivityCreateViewModel
            {
                ActivityDateTime = n,
                IsPrivate = false,
                WorkItemActivityTypes = await WorkItemEngine.GetWorkItemActivityTypesAsync(),
                WorkItemId = id,
                ActivityTimeHour = n.Hour,
                ActivityTimeMinute = n.Minute,
                ActivityTimeHours = HoursSelectList,
                ActivityTimeMinutes = MinutesSelectList
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkItemActivityCreateViewModel model, int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            if (!id.Equals(model.WorkItemId)) return RedirectToAction("Index", "WorkItem", new {id});
            if (!await WorkItemEngine.IsEditableAsync(id)) return RedirectToAction("Detail", "WorkItem", new {id});

            model.WorkItemActivityTypes = await WorkItemEngine.GetWorkItemActivityTypesAsync();
            model.ActivityTimeHours = HoursSelectList;
            model.ActivityTimeMinutes = MinutesSelectList;

            var a = new DateTime(model.ActivityDateTime.Year, model.ActivityDateTime.Month, model.ActivityDateTime.Day,
                model.ActivityTimeHour, model.ActivityTimeMinute, 0);

            if (!ModelState.IsValid) return View(model);

            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var i = await WorkItemEngine.CreateActivityAsync(model.WorkItemId, myId, model.WorkItemActivityTypeId, a,
                model.Narrative, model.IsPrivate, model.MarkResolved);

            if (!i.Equals(-1))
                return model.AddAttachments
                    ? RedirectToAction("AddAttachment", new {id = i})
                    : RedirectToAction("Detail", "WorkItem", new {id = model.WorkItemId});
            ModelState.AddModelError("", Resources.WorkItemActivity_Create_SaveFailure_Message);
            return View(model);
        }

        public async Task<ActionResult> Edit(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            var a = await WorkItemEngine.GetActivityAsync(id);
            if (a == null) return RedirectToAction("Index", "WorkItem");

            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            // Only allow original author to edit
            if (!a.CreatedByPersonId.Equals(myId))
                return RedirectToAction("Detail", "WorkItem", new {id = a.WorkItemId});
            var timeSpan = DateTime.Now - a.CreatedOnDateTime;
            // Don't allow editing beyond 60 minutes
            if (timeSpan.TotalMinutes >= 60) return RedirectToAction("Detail", "WorkItem", new {id = a.WorkItemId});

            var model = new WorkItemActivityEditViewModel
            {
                ActivityDateTime = a.ActivityDateTime,
                ActivityTimeHour = a.ActivityDateTime.Hour,
                ActivityTimeHours = HoursSelectList,
                ActivityTimeMinute = a.ActivityDateTime.Minute,
                ActivityTimeMinutes = MinutesSelectList,
                IsPrivate = a.IsPrivate,
                Narrative = a.Narrative,
                WorkItemActivityTypeId = a.WorkItemActivityTypeId,
                WorkItemActivityTypes = await WorkItemEngine.GetWorkItemActivityTypesAsync(),
                WorkItemId = a.WorkItemId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkItemActivityEditViewModel model, int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            var a = await WorkItemEngine.GetActivityAsync(id);
            if (a == null) return RedirectToAction("Index", "WorkItem");

            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            // Only allow original author to edit
            if (!a.CreatedByPersonId.Equals(myId))
                return RedirectToAction("Detail", "WorkItem", new {id = a.WorkItemId});
            var timeSpan = DateTime.Now - a.CreatedOnDateTime;
            // Don't allow editing beyond 60 minutes
            if (timeSpan.TotalMinutes >= 60) return RedirectToAction("Detail", "WorkItem", new {id = a.WorkItemId});

            model.WorkItemActivityTypes = await WorkItemEngine.GetWorkItemActivityTypesAsync();
            model.ActivityTimeHours = HoursSelectList;
            model.ActivityTimeMinutes = MinutesSelectList;

            var adt = new DateTime(model.ActivityDateTime.Year, model.ActivityDateTime.Month,
                model.ActivityDateTime.Day,
                model.ActivityTimeHour, model.ActivityTimeMinute, 0);

            if (!ModelState.IsValid) return View(model);

            var i = await WorkItemEngine.CreateActivityAsync(model.WorkItemId, myId, model.WorkItemActivityTypeId, adt,
                model.Narrative, model.IsPrivate);

            if (!i.Equals(-1))
                return RedirectToAction("Detail", "WorkItem", new {id = model.WorkItemId});
            ModelState.AddModelError("", Resources.WorkItemActivity_Create_SaveFailure_Message);
            return View(model);
        }

        // GET: WorkItemActivity
        public ActionResult Index()
        {
            return RedirectToAction("Index", "WorkItem");
        }

        public async Task<ActionResult> Resolve(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "WorkItem");
            if (!await WorkItemEngine.IsEditableAsync(id)) return RedirectToAction("Detail", "WorkItem", new {id});

            var n = General.RoundTimeToNearestN(DateTime.Now, TimeSpan.FromMinutes(5));
            var model = new WorkItemActivityCreateViewModel
            {
                ActivityDateTime = n,
                IsPrivate = false,
                WorkItemActivityTypes = await WorkItemEngine.GetWorkItemActivityTypesAsync(),
                WorkItemId = id,
                ActivityTimeHour = n.Hour,
                ActivityTimeMinute = n.Minute,
                ActivityTimeHours = HoursSelectList,
                ActivityTimeMinutes = MinutesSelectList,
                MarkResolved = true
            };
            return View("Create", model);
        }

        #endregion
    }
}