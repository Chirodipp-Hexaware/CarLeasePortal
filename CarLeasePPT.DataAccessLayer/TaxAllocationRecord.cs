using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxAllocationRecord
    {
        #region Public Properties

        public string AccountNumber { get; set; }
        public DateTime? ApExportDate { get; set; }
        public decimal AssessedValue { get; set; }
        public decimal BaseTax { get; set; }
        public string BillNumber { get; set; }
        public string CollectorAccountNumber { get; set; }
        public DateTime? CompletedDate { get; set; }
        public decimal DecalRegistrationFee { get; set; }
        public decimal Discount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Interest { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string LeaseNumber { get; set; }
        public string LesseeFirstName { get; set; }
        public string LesseeLastName { get; set; }
        public string ModelYear { get; set; }
        public short? MonthsTaxed { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal Penalty { get; set; }
        public string PlateNumber { get; set; }
        public decimal PptraCredit { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal TaxableValue { get; set; }
        public string TaxAssessorName { get; set; }
        public string TaxAssessorState { get; set; }
        public int TaxBillId { get; set; }
        public int TaxBillCarLeaseId { get; set; }
        public int TaxCollectorId { get; set; }
        public string TaxCollectorName { get; set; }
        public int? TaxPaymentId { get; set; }
        public DateTime? TaxPeriodEnd { get; set; }
        public DateTime? TaxPeriodStart { get; set; }
        public string TaxYear { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public int? CarLeaseId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string Vin { get; set; }

        #endregion
    }
}