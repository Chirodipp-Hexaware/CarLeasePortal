using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxBillListRecord
    {
        #region Public Properties

        public string BillNumber { get; set; }
        public string CollectorAccountNumber { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsComplete { get; set; }
        public string IsCompleteString { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string TaxAssessorState { get; set; }
        public int TaxBillId { get; set; }
        public string TaxCollectorName { get; set; }
        public string TaxYear { get; set; }
        public decimal TotalAmountOwed { get; set; }

        #endregion
    }
}