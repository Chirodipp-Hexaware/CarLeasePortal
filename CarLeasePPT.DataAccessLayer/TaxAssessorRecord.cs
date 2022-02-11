using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxAssessorRecord
    {
        public int TaxAssessorId { get; set; }
        public string TaxAssessorName { get; set; }
        public string StateAbbreviation { get; set; }
        public string County { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public DateTime? TaxPeriodStart { get; set; }
        public DateTime? TaxPeriodEnd { get; set; }
        public bool IsDeleted { get; set; }
    }
}