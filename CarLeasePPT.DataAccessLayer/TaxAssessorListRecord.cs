namespace CarLeasePPT.DataAccessLayer
{
    public class TaxAssessorListRecord
    {
        public int TaxAssessorId { get; set; }
        public string TaxAssessorName { get; set; }
        public string StateAbbreviation { get; set; }
        public string County { get; set; }
        public bool IsDeleted { get; set; }
        public string IsDeletedString { get; set; }
    }
}