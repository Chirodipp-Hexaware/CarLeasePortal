using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Filters;
using CarLeasePPT.Helpers;
using CarLeasePPT.Models;
using CarLeasePPT.Spreadsheet;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    [AuthorizeRole(Roles = "AUDIT_LOG")]
    public class AuditLogController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var a = await AuditLogEngine.GetAsync(id);
            if (a == null) return RedirectToAction("Index");
            var model = new AuditLogViewModel
            {
                Action = a.Action,
                AuditLogId = a.AuditLogId,
                Controller = a.Controller,
                IsReviewed = a.IsReviewed,
                Level = a.LogLevel,
                Logged = a.Logged,
                Message = a.Message,
                PersonFullName = a.PersonFullName,
                PersonId = a.PersonId,
                RecordId = a.RecordId,
                RemoteAddress = a.RemoteAddress,
                ReviewedByPersonName = a.ReviewedByFullName,
                ReviewedOnDateTime = a.ReviewedOnDateTime,
                Url = a.Url
            };
            return View(model);
        }

        public async Task<ActionResult> DetailMarkReviewed(int id = -1)
        {
            if (id.Equals(-1)) return View("MarkReviewedFailed");
            var modifyingPersonId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var result = await AuditLogEngine.MarkIsReviewed(id, modifyingPersonId);
            if (result) return RedirectToAction("Detail", new {id});
            return View("MarkReviewedFailed");
        }

        // GET: AuditLog
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MarkAllReviewed()
        {
            var modifyingPersonId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var result = await AuditLogEngine.MarkIsReviewed(modifyingPersonId);
            if (result) return RedirectToAction("Index");
            return View("MarkAllReviewedFailed");
        }

        public async Task<ActionResult> MarkReviewed(int id = -1)
        {
            if (id.Equals(-1)) return View("MarkReviewedFailed");
            var modifyingPersonId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var result = await AuditLogEngine.MarkIsReviewed(id, modifyingPersonId);
            if (result) return RedirectToAction("Index");
            return View("MarkReviewedFailed");
        }

        public ActionResult RunReport()
        {
            var model = new AuditLogRunReportViewModel
            {
                EndDateTime = DateTime.Now.AddDays(1),
                IncludeInfo = true,
                IncludeReviewed = false,
                StartDateTime = DateTime.Now.AddMonths(-1)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RunReport(AuditLogRunReportViewModel model)
        {
            if (model.StartDateTime > model.EndDateTime)
            {
                ModelState.AddModelError("StartDateTime", Resources.AuditLog_RunReport_StartDate_Validation_Message);
                ModelState.AddModelError("EndDateTime", Resources.AuditLog_RunReport_EndDate_Validation_Message);
                return View(model);
            }

            var auditLogRecords = await AuditLogEngine.GetReportRecords(model.StartDateTime, model.EndDateTime,
                model.IncludeInfo, model.IncludeReviewed);
            if (auditLogRecords.Count.Equals(0))
            {
                ModelState.AddModelError("", Resources.AuditLog_RunReport_NoRecords_Message);
                return View(model);
            }

            var sourceData = Export.ExportRecords(auditLogRecords);
            if (sourceData == null)
                return RedirectToAction("RunReport");

            var contentDispostion = new ContentDisposition
            {
                FileName = $"Hexa Audit Log Report {model.StartDateTime:yyyyMMdd} to {model.EndDateTime:yyyyMMdd}.xlsx",
                Inline = false
            };
            Response.AddHeader("content-disposition", contentDispostion.ToString());
            Response.AddHeader("content-length", sourceData.Length.ToString());
            return File(sourceData, MimeType.Excel);
        }

        #endregion
    }
}