using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxBillCarLeaseRecord
    {
        #region Public Properties

        public string AccountNumber { get; set; }
        public decimal AssessedValue { get; set; }
        public decimal BaseTax { get; set; }
        public string BillNumber { get; set; }
        public decimal DecalRegistrationFee { get; set; }
        public decimal Discount { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Interest { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal Penalty { get; set; }
        public decimal PptraCredit { get; set; }
        public decimal TaxableValue { get; set; }
        public int TaxBillId { get; set; }
        public int TaxBillCarLeaseId { get; set; }
        public string TaxCollector { get; set; }
        public string TaxYear { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public int? CarLeaseId { get; set; }
        public string Vin { get; set; }

        #endregion
    }
}