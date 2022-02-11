using System;
using System.Collections.Generic;
using CarLeasePPT.DataAccessLayer;

namespace CarLeasePPT.Models
{
    public class TaxBillDetailViewModel
    {
        #region Public Properties

        public DateTime? ApExportDate { get; set; }
        public string BillNumber { get; set; }
        public string CollectorAccountNumber { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public short? MonthsTaxed { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string TaxAssessorName { get; set; }
        public string TaxAssessorState { get; set; }
        public IEnumerable<TaxBillAttachmentRecord> TaxBillAttachmentRecords { get; set; }
        public int TaxBillId { get; set; }
        public IEnumerable<TaxBillCarLeaseRecord> TaxBillCarLeaseRecords { get; set; }
        public int TaxCollectorId { get; set; }
        public string TaxCollectorName { get; set; }
        public int? TaxPaymentId { get; set; }
        public DateTime? TaxPeriodEnd { get; set; }
        public DateTime? TaxPeriodStart { get; set; }
        public string TaxYear { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public decimal TotalTaxableValue { get; set; }

        #endregion
    }
}