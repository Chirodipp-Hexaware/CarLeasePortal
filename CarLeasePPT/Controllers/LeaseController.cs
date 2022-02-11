using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Filters;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    public class LeaseController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var p = await LeaseEngine.GetCarLeaseAsync(id);
            if (p == null) return RedirectToAction("Index");
            var model = new LeaseDetailViewModel
            {
                ActualTerminationDate = p.ActualTerminationDate,
                AssetNumber = p.AssetNumber,
                AssetStatusName = p.AssetStatusName,
                BranchCode = p.BranchCode,
                CommencementDate = p.CommencementDate,
                CompanyCode = p.CompanyCode,
                CompanyName = p.CompanyName,
                ExemptionCode = p.ExemptionCode,
                LeaseNumber = p.LeaseNumber,
                LeaseStatusName = p.LeaseStatusName,
                LesseeFirstName = p.LesseeFirstName,
                LesseeLastName = p.LesseeLastName,
                ModelYear = p.ModelYear,
                OriginalCost = p.OriginalCost,
                PayoffSource = p.PayoffSource,
                PlateNumber = p.PlateNumber,
                ProductUseCode = p.ProductUseCode,
                RegistrationState = p.RegistrationState,
                SaleDate = p.SaleDate,
                SalesPrice = p.SalesPrice,
                ScheduledTerminationDate = p.ScheduledTerminationDate,
                TaxRecoveryCode = p.TaxRecoveryCode,
                TerminationCode = p.TerminationCode,
                CarLeaseId = p.CarLeaseId,
                CarMake = p.CarMake,
                CarMasterId = p.CarMasterId,
                CarModel = p.CarModel,
                Vin = p.Vin,
                TaxBillCarLeaseRecords = p.TaxBillCarLeaseRecords,
                PhysicalAddressRecords = p.PhysicalAddressRecords,
                CustomerAddressRecords = p.CustomerAddressRecords,
                LeaseAttachmentRecords = p.LeaseAttachmentRecords
            };
            return View(model);
        }

        // GET: Lease
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ViewAttachment(int id = -1)
        {
            if (id.Equals(-1)) return null;
            var fr = await LeaseEngine.GetAttachmentAsync(id);
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