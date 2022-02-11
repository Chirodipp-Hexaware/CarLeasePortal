using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxAssessorEditViewModel
    {
        #region Public Properties

        [Required]
        [Display(Name = "TaxAssessor_Common_TaxAssessorName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(200)]
        public string TaxAssessorName { get; set; }

        [Display(Name = "TaxAssessor_Common_StateAbbreviation_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(2)]
        [Required]
        public string StateAbbreviation { get; set; }

        [Required]
        [Display(Name = "TaxAssessor_Common_County_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(100)]
        public string County { get; set; }

        public int TaxAssessorId { get; set; }

        #endregion
    }
}