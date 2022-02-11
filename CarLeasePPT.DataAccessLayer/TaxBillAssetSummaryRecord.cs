using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxBillAssetSummaryRecord
    {
        #region Public Properties

        public decimal AssessedValue { get; set; }
        public decimal BaseTax { get; set; }
        public string BillNumber { get; set; }
        public string CollectorAccountNumber { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerState { get; set; }
        public decimal DecalRegistrationFee { get; set; }
        public decimal Discount { get; set; }
        public decimal Interest { get; set; }
        public string LeaseNumber { get; set; }
        public string LesseeFirstName { get; set; }
        public string LesseeLastName { get; set; }
        public string ModelYear { get; set; }
        public decimal? OriginalCost { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal Penalty { get; set; }
        public decimal PptraCredit { get; set; }
        public string TaxAssessorName { get; set; }
        public string TaxCollectorName { get; set; }
        public string TaxYear { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string Vin { get; set; }

        #endregion
    }
}