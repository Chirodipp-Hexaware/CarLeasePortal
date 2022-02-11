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
    public class TaxBillController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Allocation(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "Home");
            var x = await TaxBillEngine.GetTaxBillCarLeaseRecord(id);

            var model = new AllocationViewModel
            {
                AccountNumber = x.AccountNumber,
                ApExportDate = x.ApExportDate,
                AssessedValue = x.AssessedValue,
                BaseTax = x.BaseTax,
                BillNumber = x.BillNumber ?? "No Bill Number",
                CollectorAccountNumber = x.CollectorAccountNumber,
                CompletedDate = x.CompletedDate,
                DecalRegistrationFee = x.DecalRegistrationFee,
                Discount = x.Discount,
                DiscountDueDate = x.DiscountDueDate,
                DueDate = x.DueDate,
                Interest = x.Interest,
                InvoiceDate = x.InvoiceDate,
                LeaseNumber = x.LeaseNumber,
                LesseeFirstName = x.LesseeFirstName,
                LesseeLastName = x.LesseeLastName,
                ModelYear = x.ModelYear ?? string.Empty,
                MonthsTaxed = x.MonthsTaxed,
                PaymentDate = x.PaidDate,
                Penalty = x.Penalty,
                PptraCredit = x.PptraCredit,
                ReceivedDate = x.ReceivedDate,
                TaxableValue = x.TaxableValue,
                TaxAssessorName = x.TaxAssessorName,
                TaxAssessorState = x.TaxAssessorState,
                TaxBillId = x.TaxBillId,
                TaxBillCarLeaseId = x.TaxBillCarLeaseId,
                TaxCollectorName = x.TaxCollectorName,
                TaxPaymentId = x.TaxPaymentId,
                TaxPeriodEnd = x.TaxPeriodEnd,
                TaxPeriodStart = x.TaxPeriodStart,
                TaxYear = x.TaxYear,
                TotalAmountOwed = x.TotalAmountOwed,
                CarLeaseId = x.CarLeaseId,
                CarMake = x.CarMake,
                CarModel = x.CarModel,
                Vin = x.Vin
            };
            return View(model);
        }

        public async Task<ActionResult> AssetSummary(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index", "Home");

            var x = await TaxBillEngine.GetTaxBillAssetSummaryRecord(id);

            var model = new AllocationSummaryViewModel
            {
                AssessedValue = x.AssessedValue,
                BaseTax = x.BaseTax,
                BillNumber = x.BillNumber ?? "No Bill Number",
                CollectorAccountNumber = x.CollectorAccountNumber,
                CompletedDate = x.CompletedDate,
                CustomerAddress = x.CustomerAddress,
                CustomerCity = x.CustomerCity,
                CustomerPostalCode = x.CustomerPostalCode,
                CustomerState = x.CustomerState,
                DecalRegistrationFee = x.DecalRegistrationFee,
                Discount = x.Discount,
                Interest = x.Interest,
                LeaseNumber = x.LeaseNumber,
                LesseeFirstName = x.LesseeFirstName,
                LesseeLastName = x.LesseeLastName,
                ModelYear = x.ModelYear,
                OriginalCost = x.OriginalCost,
                PaidDate = x.PaidDate,
                Penalty = x.Penalty,
                PptraCredit = x.PptraCredit,
                AssessingJurisdiction = x.TaxAssessorName,
                CollectingJurisdiction = x.TaxCollectorName,
                TaxYear = x.TaxYear,
                TotalAmountOwed = x.TotalAmountOwed,
                CarMake = x.CarMake,
                CarModel = x.CarModel,
                Vin = x.Vin
            };

            return View(model);
        }

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var taxBill = await TaxBillEngine.GetTaxBillAsync(id);
            if (taxBill == null) return RedirectToAction("Index");

            var model = new TaxBillDetailViewModel
            {
                ApExportDate = taxBill.ApExportDate,
                BillNumber = taxBill.BillNumber ?? "No Bill Number",
                CollectorAccountNumber = taxBill.CollectorAccountNumber,
                CompletedDate = taxBill.CompletedDate,
                DiscountDueDate = taxBill.DiscountDueDate,
                DueDate = taxBill.DueDate,
                InvoiceDate = taxBill.InvoiceDate,
                MonthsTaxed = taxBill.MonthsTaxed,
                ReceivedDate = taxBill.ReceivedDate,
                PaymentDate = taxBill.PaymentDate,
                TaxAssessorName = taxBill.TaxAssessorName,
                TaxAssessorState = taxBill.TaxAssessorState,
                TaxBillAttachmentRecords = taxBill.TaxBillAttachments,
                TaxBillId = taxBill.TaxBillId,
                TaxBillCarLeaseRecords = taxBill.TaxBillCarLeaseRecords,
                TaxCollectorId = taxBill.TaxCollectorId,
                TaxCollectorName = taxBill.TaxCollectorName,
                TaxPaymentId = taxBill.TaxPaymentId,
                TaxPeriodEnd = taxBill.TaxPeriodEnd,
                TaxPeriodStart = taxBill.TaxPeriodStart,
                TaxYear = taxBill.TaxYear,
                TotalAmountOwed = taxBill.TotalAmountOwed,
                TotalTaxableValue = taxBill.TotalTaxableValue
            };
            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ViewAttachment(int id = -1)
        {
            if (id.Equals(-1)) return null;
            var fr = await TaxBillEngine.GetAttachmentAsync(id);
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