using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxCollectorEditViewModel
    {
        #region Public Properties

        [Required]
        [Display(Name = "TaxCollector_Common_TaxCollectorName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(200)]
        public string TaxCollectorName { get; set; }

        [Display(Name = "TaxCollector_Common_VendorCode_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        [Required]
        public string VendorCode { get; set; }

        [Display(Name = "TaxCollector_Common_TaxAssessorName_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAssessorName { get; set; }

        [Display(Name = "TaxCollector_Common_StateAbbreviation_DisplayName", ResourceType = typeof(Resources))]
        public string StateAbbreviation { get; set; }

        [Display(Name = "TaxCollector_Common_County_DisplayName", ResourceType = typeof(Resources))]
        public string County { get; set; }
        public int TaxCollectorId { get; set; }

        #endregion
    }
}