using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxCollectorCreateViewModel
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

        [Required]
        [Display(Name = "TaxCollector_Common_TaxAssessorName_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAssessorSelect2 { get; set; }
        
        public int TaxAssessorId { get; set; }

        #endregion
    }
}