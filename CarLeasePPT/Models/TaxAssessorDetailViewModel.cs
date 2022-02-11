using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxAssessorDetailViewModel
    {
        #region Public Properties

        public int TaxAssessorId { get; set; }

        [Display(Name = "TaxAssessor_Common_TaxAssessorName_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAssessorName { get; set; }

        [Display(Name = "TaxAssessor_Common_StateAbbreviation_DisplayName", ResourceType = typeof(Resources))]
        public string StateAbbreviation { get; set; }

        [Display(Name = "TaxAssessor_Common_County_DisplayName", ResourceType = typeof(Resources))]
        public string County { get; set; }

        public bool IsDeleted { get; set; }

        #endregion
    }
}