namespace CarLeasePPT.DataAccessLayer
{
    public class TaxCollectorListRecord
    {
        public int TaxCollectorId { get; set; }
        public string TaxAssessorName { get; set; }
        public string TaxCollectorName { get; set; }
        public string VendorCode { get; set; }
        public string StateAbbreviation { get; set; }
        public bool IsDeleted { get; set; }
        public string IsDeletedString { get; set; }
    }
}