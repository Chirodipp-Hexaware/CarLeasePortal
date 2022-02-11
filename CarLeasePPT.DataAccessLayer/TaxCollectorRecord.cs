namespace CarLeasePPT.DataAccessLayer
{
    public class TaxCollectorRecord
    {
        #region Public Properties

        public string County { get; set; }
        public bool IsDeleted { get; set; }
        public string StateAbbreviation { get; set; }
        public int TaxAssessorId { get; set; }
        public string TaxAssessorName { get; set; }
        public int TaxCollectorId { get; set; }
        public string TaxCollectorName { get; set; }
        public string VendorCode { get; set; }

        #endregion
    }
}